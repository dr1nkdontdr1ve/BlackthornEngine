using System.IO;
using Lidgren.Network;

class Receber
{
    // Pacotes do cliente
    public enum Pacotes
    {
        Latência,
        Conectar,
        Registrar,
        CriarPersonagem,
        Personagem_Usar,
        Personagem_Criar,
        Personagem_Deletar,
        Jogador_Direção,
        Jogador_Mover,
        Solicitar_Mapa,
        Mensagem,
        Jogador_Atacar,
        AdicionarPonto,
        ColetarItem,
        SoltarItem,
        Inventário_Mudar,
        Inventário_Usar,
        Equipamento_Remover,
        Hotbar_Adicionar,
        Hotbar_Mudar,
        Hotbar_Usar
    }

    public static void EncaminharDados(byte Índice, NetIncomingMessage Dados)
    {
        // Manuseia os dados recebidos
        switch ((Pacotes)Dados.ReadByte())
        {
            case Pacotes.Latência: Latência(Índice, Dados); break;
            case Pacotes.Conectar: Conectar(Índice, Dados); break;
            case Pacotes.Registrar: Registrar(Índice, Dados); break;
            case Pacotes.CriarPersonagem: CriarPersonagem(Índice, Dados); break;
            case Pacotes.Personagem_Usar: Personagem_Usar(Índice, Dados); break;
            case Pacotes.Personagem_Criar: Personagem_Criar(Índice, Dados); break;
            case Pacotes.Personagem_Deletar: Personagem_Deletar(Índice, Dados); break;
            case Pacotes.Jogador_Direção: Jogador_Direção(Índice, Dados); break;
            case Pacotes.Jogador_Mover: Jogador_Mover(Índice, Dados); break;
            case Pacotes.Solicitar_Mapa: Solicitar_Mapa(Índice, Dados); break;
            case Pacotes.Mensagem: Mensagem(Índice, Dados); break;
            case Pacotes.Jogador_Atacar: Jogador_Atacar(Índice, Dados); break;
            case Pacotes.AdicionarPonto: AdicionarPonto(Índice, Dados); break;
            case Pacotes.ColetarItem: ColetarItem(Índice, Dados); break;
            case Pacotes.SoltarItem: SoltarItem(Índice, Dados); break;
            case Pacotes.Inventário_Mudar: Inventário_Mudar(Índice, Dados); break;
            case Pacotes.Inventário_Usar: Inventário_Usar(Índice, Dados); break;
            case Pacotes.Equipamento_Remover: Equipamento_Remover(Índice, Dados); break;
            case Pacotes.Hotbar_Adicionar: Hotbar_Adicionar(Índice, Dados); break;
            case Pacotes.Hotbar_Mudar: Hotbar_Mudar(Índice, Dados); break;
            case Pacotes.Hotbar_Usar: Hotbar_Usar(Índice, Dados); break;
        }
    }

    private static void Latência(byte Índice, NetIncomingMessage Dados)
    {
        // Envia o pacote para a contagem da latência
        Enviar.Latência(Índice);
    }

    private static void Conectar(byte Índice, NetIncomingMessage Dados)
    {
        // Lê os dados
        string Usuário = Dados.ReadString().Trim();
        string Senha = Dados.ReadString();

        // Verifica se está tudo certo
        if (Usuário.Length < Jogo.Min_Caractere || Usuário.Length > Jogo.Máx_Caractere || Senha.Length < Jogo.Min_Caractere || Senha.Length > Jogo.Máx_Caractere)
        {
            Enviar.Alerta(Índice, "O nome do usuário e a senha devem conter entre " + Jogo.Min_Caractere + " e " + Jogo.Máx_Caractere + " caracteres.");
            return;
        }
        if (!File.Exists(Diretórios.Contas.FullName + Usuário + Diretórios.Formato))
        {
            Enviar.Alerta(Índice, "Esse nome de usuário não está cadastrado.");
            return;
        }
        if (Jogador.MúltiplasContas(Usuário))
        {
            Enviar.Alerta(Índice, "Já há alguém conectado a essa conta.");
            return;
        }
        else if (Senha != Ler.Jogador_Senha(Usuário))
        {
            Enviar.Alerta(Índice, "A senha está incorreta.");
            return;
        }

        // Carrega os dados do jogador
        Ler.Jogador(Índice, Usuário);

        // Envia os dados das classes
        Enviar.Classes(Índice);

        // Se o jogador não tiver nenhum personagem então abrir o painel de criação de personagem
        if (!Jogador.PossuiPersonagens(Índice))
        {
            Enviar.CriarPersonagem(Índice);
            return;
        }

        // Abre a janela de seleção de personagens
        Enviar.Personagens(Índice);
        Enviar.Conectar(Índice);
    }

    private static void Registrar(byte Índice, NetIncomingMessage Dados)
    {
        // Lê os dados
        string Usuário = Dados.ReadString().Trim();
        string Senha = Dados.ReadString();

        // Verifica se está tudo certo
        if (Usuário.Length < Jogo.Min_Caractere || Usuário.Length > Jogo.Máx_Caractere || Senha.Length < Jogo.Min_Caractere || Senha.Length > Jogo.Máx_Caractere)
        {
            Enviar.Alerta(Índice, "O nome do usuário e a senha devem conter entre " + Jogo.Min_Caractere + " e " + Jogo.Máx_Caractere + " caracteres.");
            return;
        }
        else if (File.Exists(Diretórios.Contas.FullName + Usuário + Diretórios.Formato))
        {
            Enviar.Alerta(Índice, "Já existe alguém registrado com esse nome.");
            return;
        }

        // Cria a conta
        Listas.Jogador[Índice].Usuário = Usuário;
        Listas.Jogador[Índice].Senha = Senha;

        // Salva a conta
        Escrever.Jogador(Índice);

        // Abre a janela de seleção de personagens
        Enviar.Classes(Índice);
        Enviar.CriarPersonagem(Índice);
    }

    private static void CriarPersonagem(byte Índice, NetIncomingMessage Dados)
    {
        byte Personagem = Jogador.EncontrarPersonagem(Índice, string.Empty);

        // Lê os dados
        string Nome = Dados.ReadString().Trim();

        // Verifica se está tudo certo
        if (Nome.Length < Jogo.Min_Caractere || Nome.Length > Jogo.Máx_Caractere)
        {
            Enviar.Alerta(Índice, "O nome do personagem deve conter entre " + Jogo.Min_Caractere + " e " + Jogo.Máx_Caractere + " caracteres.", false);
            return;
        }
        if (Nome.Contains(";") || Nome.Contains(":"))
        {
            Enviar.Alerta(Índice, "Não pode conter ';' e ':' no nome do personagem.", false);
            return;
        }
        if (Ler.Personagens_Nomes().Contains(";" + Nome + ":"))
        {
            Enviar.Alerta(Índice, "Já existe um personagem com esse nome.", false);
            return;
        }

        // Define o personagem que será usado
        Listas.TempJogador[Índice].Utilizado = Personagem;

        // Define os valores iniciais do personagem
        Jogador.Personagem(Índice).Nome = Nome;
        Jogador.Personagem(Índice).Level = 1;
        Jogador.Personagem(Índice).Classe = Dados.ReadByte();
        Jogador.Personagem(Índice).Gênero = Dados.ReadBoolean();
        Jogador.Personagem(Índice).Atributo = Listas.Classe[Jogador.Personagem(Índice).Classe].Atributo;
        Jogador.Personagem(Índice).Mapa = Listas.Classe[Jogador.Personagem(Índice).Classe].Aparecer_Mapa;
        Jogador.Personagem(Índice).Direção = (Jogo.Direções)Listas.Classe[Jogador.Personagem(Índice).Classe].Aparecer_Direção;
        Jogador.Personagem(Índice).X = Listas.Classe[Jogador.Personagem(Índice).Classe].Aparecer_X;
        Jogador.Personagem(Índice).Y = Listas.Classe[Jogador.Personagem(Índice).Classe].Aparecer_Y;
        for (byte i = 0; i <= (byte)Jogo.Vitais.Quantidade - 1; i++) Jogador.Personagem(Índice).Vital[i] = Jogador.Personagem(Índice).MáxVital(i);

        // Salva a conta
        Escrever.Personagem(Nome);
        Escrever.Jogador(Índice);

        // Entra no jogo
        Jogador.Entrar(Índice);
    }

    private static void Personagem_Usar(byte Índice, NetIncomingMessage Dados)
    {
        // Define o personagem que será usado
        Listas.TempJogador[Índice].Utilizado = Dados.ReadByte();

        // Entra no jogo
        Jogador.Entrar(Índice);
    }

    private static void Personagem_Criar(byte Índice, NetIncomingMessage Dados)
    {
        // Verifica se o jogador já criou o máximo de personagens possíveis
        if (Jogador.EncontrarPersonagem(Índice, string.Empty) == 0)
        {
            Enviar.Alerta(Índice, "Você só pode ter " + Listas.Servidor_Dados.Máx_Personagens + " personagens.", false);
            return;
        }

        // Abre a janela de seleção de personagens
        Enviar.Classes(Índice);
        Enviar.CriarPersonagem(Índice);
    }

    private static void Personagem_Deletar(byte Índice, NetIncomingMessage Dados)
    {
        byte Personagem = Dados.ReadByte();
        string Nome = Listas.Jogador[Índice].Personagem[Personagem].Nome;

        // Verifica se o personagem existe
        if (string.IsNullOrEmpty(Nome))
            return;

        // Deleta o personagem
        Enviar.Alerta(Índice, "O personagem '" + Nome + "' foi deletado.", false);
        Escrever.Personagens(Ler.Personagens_Nomes().Replace(":;" + Nome + ":", ":"));
        Limpar.Jogador_Personagem(Índice, Personagem);

        // Salva o personagem
        Enviar.Personagens(Índice);
        Escrever.Jogador(Índice);
    }

    private static void Jogador_Direção(byte Índice, NetIncomingMessage Dados)
    {
        Jogo.Direções Direção = (Jogo.Direções)Dados.ReadByte();

        // Previni erros
        if (Direção < Jogo.Direções.Acima || Direção > Jogo.Direções.Direita) return;
        if (Listas.TempJogador[Índice].ObtendoMapa) return;

        // Defini a direção do jogador
        Jogador.Personagem(Índice).Direção = Direção;
        Enviar.Jogador_Direção(Índice);
    }

    private static void Jogador_Mover(byte Índice, NetIncomingMessage Dados)
    {
        byte X = Dados.ReadByte(), Y = Dados.ReadByte();

        // Move o jogador se necessário
        if (Jogador.Personagem(Índice).X != X || Jogador.Personagem(Índice).Y != Y)
            Enviar.Jogador_Posição(Índice);
        else
            Jogador.Mover(Índice, Dados.ReadByte());
    }

    private static void Solicitar_Mapa(byte Índice, NetIncomingMessage Dados)
    {
        // Se necessário enviar as informações do mapa ao jogador
        if (Dados.ReadBoolean()) Enviar.Mapa(Índice, Jogador.Personagem(Índice).Mapa);

        // Envia a informação aos outros jogadores
        Enviar.Jogadores_Dados_Mapa(Índice);

        // Entra no mapa
        Listas.TempJogador[Índice].ObtendoMapa = false;
        Enviar.EntrarNoMapa(Índice);
    }

    private static void Mensagem(byte Índice, NetIncomingMessage Dados)
    {
        string Mensagem = Dados.ReadString();

        // Evita caracteres inválidos
        for (byte i = 0; i >= Mensagem.Length; i++)
            if ((Mensagem[i] < 32 && Mensagem[i] > 126))
                return;

        // Envia a mensagem para os outros jogadores
        switch ((Jogo.Mensagens)Dados.ReadByte())
        {
            case Jogo.Mensagens.Mapa: Enviar.Mensagem_Mapa(Índice, Mensagem); break;
            case Jogo.Mensagens.Global: Enviar.Mensagem_Global(Índice, Mensagem); break;
            case Jogo.Mensagens.Particular: Enviar.Mensagem_Particular(Índice, Dados.ReadString(), Mensagem); break;
        }
    }

    private static void Jogador_Atacar(byte Índice, NetIncomingMessage Dados)
    {
        // Ataca
        Jogador.Atacar(Índice);
    }

    private static void AdicionarPonto(byte Índice, NetIncomingMessage Dados)
    {
        byte Atributo = Dados.ReadByte();

        // Adiciona um ponto a determinado atributo
        if (Jogador.Personagem(Índice).Pontos > 0)
        {
            Jogador.Personagem(Índice).Atributo[Atributo] += 1;
            Jogador.Personagem(Índice).Pontos -= 1;
            Enviar.Jogador_Experiência(Índice);
            Enviar.Jogadores_Dados_Mapa(Índice);
        }
    }

    private static void ColetarItem(byte Índice, NetIncomingMessage Dados)
    {
        short Mapa_Num = Jogador.Personagem(Índice).Mapa;
        byte Mapa_Item = Mapa.HáItem(Mapa_Num, Jogador.Personagem(Índice).X, Jogador.Personagem(Índice).Y);
        short Mapa_Item_Num = Listas.Mapa[Mapa_Num].Temp_Item[Mapa_Item].Índice;

        // Somente se necessário
        if (Mapa_Item == 0) return;

        // Dá o item ao jogador
        if (Jogador.DarItem(Índice, Mapa_Item_Num, Listas.Mapa[Mapa_Num].Temp_Item[Mapa_Item].Quantidade))
        {
            // Retira o item do mapa
            Listas.Mapa[Mapa_Num].Temp_Item.RemoveAt(Mapa_Item);
            Enviar.Mapa_Itens(Mapa_Num);
        }
    }

    private static void SoltarItem(byte Índice, NetIncomingMessage Dados)
    {
        Jogador.SoltarItem(Índice, Dados.ReadByte());
    }

    private static void Inventário_Mudar(byte Índice, NetIncomingMessage Dados)
    {
        byte Slot_Antigo = Dados.ReadByte(), Slot_Novo = Dados.ReadByte();
        byte Hotbar_Antigo = Jogador.EncontrarHotbar(Índice, (byte)Jogo.Hotbar.Item, Slot_Antigo), Hotbar_Novo = Jogador.EncontrarHotbar(Índice, (byte)Jogo.Hotbar.Item, Slot_Novo);
        Listas.Estruturas.Inventário Antigo = Jogador.Personagem(Índice).Inventário[Slot_Antigo];

        // Somente se necessário
        if (Jogador.Personagem(Índice).Inventário[Slot_Antigo].Item_Num == 0) return;
        if (Slot_Antigo == Slot_Novo) return;

        // Caso houver um item no novo slot, trocar ele para o velho
        if (Jogador.Personagem(Índice).Inventário[Slot_Novo].Item_Num > 0)
        {
            // Inventário
            Jogador.Personagem(Índice).Inventário[Slot_Antigo].Item_Num = Jogador.Personagem(Índice).Inventário[Slot_Novo].Item_Num;
            Jogador.Personagem(Índice).Inventário[Slot_Antigo].Quantidade = Jogador.Personagem(Índice).Inventário[Slot_Novo].Quantidade;
            Jogador.Personagem(Índice).Hotbar[Hotbar_Novo].Slot = Slot_Antigo;
        }
        else
        {
            Jogador.Personagem(Índice).Inventário[Slot_Antigo].Item_Num = 0;
            Jogador.Personagem(Índice).Inventário[Slot_Antigo].Quantidade = 0;
        }

        // Muda o item de slot
        Jogador.Personagem(Índice).Inventário[Slot_Novo].Item_Num = Antigo.Item_Num;
        Jogador.Personagem(Índice).Inventário[Slot_Novo].Quantidade = Antigo.Quantidade;
        Jogador.Personagem(Índice).Hotbar[Hotbar_Antigo].Slot = Slot_Novo;
        Enviar.Jogador_Inventário(Índice);
        Enviar.Jogador_Hotbar(Índice);
    }

    private static void Inventário_Usar(byte Índice, NetIncomingMessage Dados)
    {
        Jogador.UsarItem(Índice, Dados.ReadByte());
    }

    private static void Equipamento_Remover(byte Índice, NetIncomingMessage Dados)
    {
        byte Slot = Dados.ReadByte();
        byte Slot_Vazio = Jogador.EncontrarInventário(Índice, 0);
        short Mapa_Num = Jogador.Personagem(Índice).Mapa;
        Listas.Estruturas.Mapa_Itens Mapa_Item = new Listas.Estruturas.Mapa_Itens();

        // Apenas se necessário
        if (Jogador.Personagem(Índice).Equipamento[Slot] == 0) return;

        // Adiciona o equipamento ao inventário
        if (!Jogador.DarItem(Índice, Jogador.Personagem(Índice).Equipamento[Slot], 1))
        {
            // Somente se necessário
            if (Listas.Mapa[Mapa_Num].Temp_Item.Count == Jogo.Máx_Mapa_Itens) return;

            // Solta o item no chão
            Mapa_Item.Índice = Jogador.Personagem(Índice).Equipamento[Slot];
            Mapa_Item.Quantidade = 1;
            Mapa_Item.X = Jogador.Personagem(Índice).X;
            Mapa_Item.Y = Jogador.Personagem(Índice).Y;
            Listas.Mapa[Mapa_Num].Temp_Item.Add(Mapa_Item);

            // Envia os dados
            Enviar.Mapa_Itens(Mapa_Num);
            Enviar.Jogador_Inventário(Índice);
        }

        // Remove o equipamento
        for (byte i = 0; i <= (byte)Jogo.Atributos.Quantidade - 1; i++) Jogador.Personagem(Índice).Atributo[i] -= Listas.Item[Jogador.Personagem(Índice).Equipamento[Slot]].Equip_Atributo[i];
        Jogador.Personagem(Índice).Equipamento[Slot] = 0;

        // Envia os dados
        Enviar.Jogador_Equipamentos(Índice);
    }

    private static void Hotbar_Adicionar(byte Índice, NetIncomingMessage Dados)
    {
        byte Hotbar_Slot = Dados.ReadByte();
        byte Tipo = Dados.ReadByte();
        byte Slot = Dados.ReadByte();

        // Somente se necessário
        if (Slot != 0 && Jogador.EncontrarHotbar(Índice, Tipo, Slot) > 0) return;

        // Define os dados
        Jogador.Personagem(Índice).Hotbar[Hotbar_Slot].Slot = Slot;
        Jogador.Personagem(Índice).Hotbar[Hotbar_Slot].Tipo = Tipo;

        // Envia os dados
        Enviar.Jogador_Hotbar(Índice);
    }

    private static void Hotbar_Mudar(byte Índice, NetIncomingMessage Dados)
    {
        byte Slot_Antigo = Dados.ReadByte(), Slot_Novo = Dados.ReadByte();
        Listas.Estruturas.Hotbar Antigo = Jogador.Personagem(Índice).Hotbar[Slot_Antigo];

        // Somente se necessário
        if (Jogador.Personagem(Índice).Hotbar[Slot_Antigo].Slot == 0) return;
        if (Slot_Antigo == Slot_Novo) return;

        // Caso houver um item no novo slot, trocar ele para o velho
        if (Jogador.Personagem(Índice).Hotbar[Slot_Novo].Slot > 0)
        {
            Jogador.Personagem(Índice).Hotbar[Slot_Antigo].Slot = Jogador.Personagem(Índice).Hotbar[Slot_Novo].Slot;
            Jogador.Personagem(Índice).Hotbar[Slot_Antigo].Tipo = Jogador.Personagem(Índice).Hotbar[Slot_Novo].Tipo;
        }
        else
        {
            Jogador.Personagem(Índice).Hotbar[Slot_Antigo].Slot = 0;
            Jogador.Personagem(Índice).Hotbar[Slot_Antigo].Tipo = 0;
        }

        // Muda o item de slot
        Jogador.Personagem(Índice).Hotbar[Slot_Novo].Slot = Antigo.Slot;
        Jogador.Personagem(Índice).Hotbar[Slot_Novo].Tipo = Antigo.Tipo;
        Enviar.Jogador_Hotbar(Índice);
    }

    private static void Hotbar_Usar(byte Índice, NetIncomingMessage Dados)
    {
        byte Hotbar_Slot = Dados.ReadByte();

        // Usa o item
        if (Jogador.Personagem(Índice).Hotbar[Hotbar_Slot].Tipo == (byte)Jogo.Hotbar.Item)
            Jogador.UsarItem(Índice, Jogador.Personagem(Índice).Hotbar[Hotbar_Slot].Slot);
    }
}