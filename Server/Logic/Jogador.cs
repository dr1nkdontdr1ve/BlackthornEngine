using System;
using System.Drawing;

class Player
{
    public static Personagem_Estrutura Personagem(byte Índice)
    {
        // Retorna com os valores do personagem atual
        return Listas.Jogador[Índice].Personagem[Listas.TempJogador[Índice].Utilizado];
    }

    public class Personagem_Estrutura
    {
        // Dados básicos
        public byte Índice;
        public string Nome = string.Empty;
        public byte Classe;
        public bool Gênero;
        public short Level;
        private short experiência;
        public byte Pontos;
        public short[] Vital = new short[(byte)Game.Vitais.Quantidade];
        public short[] Atributo = new short[(byte)Game.Atributos.Quantidade];
        public short Mapa;
        public byte X;
        public byte Y;
        public Game.Direções Direção;
        public int Ataque_Tempo;
        public Listas.Estruturas.Inventário[] Inventário;
        public short[] Equipamento;
        public Listas.Estruturas.Hotbar[] Hotbar;

        public short Experiência
        {
            get
            {
                return experiência;
            }
            set
            {
                experiência = value;
                VerificarLevel(Índice);
            }
        }

        // Cálcula o dano do jogador
        public short Dano
        {
            get
            {
                return (short)(Atributo[(byte)Game.Atributos.Força] + Listas.Item[Equipamento[(byte)Game.Equipamentos.Arma]].Arma_Dano);
            }
        }

        // Cálcula o dano do jogador
        public short Jogador_Defesa
        {
            get
            {
                return Atributo[(byte)Game.Atributos.Resistência];
            }
        }

        public short MáxVital(byte Vital)
        {
            short[] Base = Listas.Classe[Classe].Vital;

            // Cálcula o máximo de vital que um jogador possui
            switch ((Game.Vitais)Vital)
            {
                case Game.Vitais.Vida:
                    return (short)(Base[Vital] + (Atributo[(byte)Game.Atributos.Vitalidade] * 1.50 * (Level * 0.75)));
                case Game.Vitais.Mana:
                    return (short)(Base[Vital] + (Atributo[(byte)Game.Atributos.Inteligência] * 1.25 * (Level * 0.5)));
            }

            return 0;
        }

        public short Regeneração(byte Vital)
        {
            // Cálcula o máximo de vital que um jogador possui
            switch ((Game.Vitais)Vital)
            {
                case Game.Vitais.Vida:
                    return (short)(MáxVital(Vital) * 0.05 + Atributo[(byte)Game.Atributos.Vitalidade] * 0.3);
                case Game.Vitais.Mana:
                    return (short)(MáxVital(Vital) * 0.05 + Atributo[(byte)Game.Atributos.Inteligência] * 0.1);
            }

            return 0;
        }

        public short ExpNecessária
        {
            get
            {
                short Soma = 0;
                // Quantidade de experiência para passar para o próximo level
                for (byte i = 0; i <= (byte)(Game.Atributos.Quantidade - 1); i++) Soma += Atributo[i];
                return (short)((Level + 1) * 2.5 + (Soma + Pontos) / 2);
            }
        }
    }

    public static void Entrar(byte Índice)
    {
        // Previni que alguém que já está online de logar
        if (EstáJogando(Índice))
            return;

        // Define que o jogador está dentro do Game
        Listas.TempJogador[Índice].Jogando = true;

        // Envia todos os dados necessários
        Enviar.Entrada(Índice);
        Enviar.Jogadores_Dados_Mapa(Índice);
        Enviar.Jogador_Experiência(Índice);
        Enviar.Jogador_Inventário(Índice);
        Enviar.Jogador_Hotbar(Índice);
        Enviar.Itens(Índice);
        Enviar.NPCs(Índice);
        Enviar.Mapa_Itens(Índice, Personagem(Índice).Mapa);

        // Transporta o jogador para a sua determinada Posição
        Transportar(Índice, Personagem(Índice).Mapa, Personagem(Índice).X, Personagem(Índice).Y);

        // Entra no Game
        Enviar.Entrar(Índice);
        Enviar.Mensagem(Índice, Listas.Servidor_Dados.Mensagem, Color.Blue);
    }

    public static void Sair(byte Índice)
    {
        // Salva os dados do jogador
        Escrever.Jogador(Índice);
        Limpar.Jogador(Índice);

        // Envia a  todos a desconexão do jogador
        Enviar.Jogador_Saiu(Índice);
    }

    public static bool EstáJogando(byte Índice)
    {
        // Verifica se o jogador está dentro do Game
        if (Rede.EstáConectado(Índice))
            if (Listas.TempJogador[Índice].Jogando)
                return true;

        return false;
    }

    public static byte Encontrar(string Nome)
    {
        // Encontra o usuário
        for (byte i = 1; i <= Listas.Jogador.GetUpperBound(0); i++)
            if (Personagem(i).Nome == Nome)
                return i;

        return 0;
    }

    public static byte EncontrarPersonagem(byte Índice, string Nome)
    {
        // Encontra o personagem
        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
            if (Listas.Jogador[Índice].Personagem[i].Nome == Nome)
                return i;

        return 0;
    }

    public static bool PossuiPersonagens(byte Índice)
    {
        // Verifica se o jogador tem algum personagem
        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
            if (Listas.Jogador[Índice].Personagem[i].Nome != string.Empty)
                return true;

        return false;
    }

    public static bool MúltiplasContas(string Usuário)
    {
        // Verifica se já há alguém conectado com essa conta
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Rede.EstáConectado(i))
                if (Listas.Jogador[i].Usuário == Usuário)
                    return true;

        return false;
    }

    public static void Mover(byte Índice, byte Movimento)
    {
        byte x = Personagem(Índice).X, y = Personagem(Índice).Y;
        short Mapa_Num = Personagem(Índice).Mapa;
        short Próximo_X = x, Próximo_Y = y;
        short Ligação = Listas.Mapa[Mapa_Num].Ligação[(byte)Personagem(Índice).Direção];
        bool OutroMovimento = false;

        // Previni erros
        if (Movimento < 1 || Movimento > 2) return;
        if (Listas.TempJogador[Índice].ObtendoMapa) return;

        // Próximo azulejo
        Map.PróximoAzulejo(Personagem(Índice).Direção, ref Próximo_X, ref Próximo_Y);

        // Ponto de ligação
        if (Map.ForaDoLimite(Mapa_Num, Próximo_X, Próximo_Y))
        {
            if (Ligação > 0)
                switch (Personagem(Índice).Direção)
                {
                    case Game.Direções.Acima: Transportar(Índice, Ligação, x, Listas.Mapa[Mapa_Num].Altura); break;
                    case Game.Direções.Abaixo: Transportar(Índice, Ligação, x, 0); break;
                    case Game.Direções.Direita: Transportar(Índice, Ligação, 0, y); break;
                    case Game.Direções.Esquerda: Transportar(Índice, Ligação, Listas.Mapa[Mapa_Num].Largura, y); break;
                }
            else
            {
                Enviar.Jogador_Posição(Índice);
                return;
            }
        }
        // Bloqueio
        else if (!Map.Azulejo_Bloqueado(Mapa_Num, x, y, Personagem(Índice).Direção))
        {
            Personagem(Índice).X = (byte)Próximo_X;
            Personagem(Índice).Y = (byte)Próximo_Y;
        }

        // Atributos
        Listas.Estruturas.Azulejo Azulejo = Listas.Mapa[Mapa_Num].Azulejo[Próximo_X, Próximo_Y];

        switch ((Map.Atributos)Azulejo.Atributo)
        {
            // Teletransporte
            case Map.Atributos.Teletransporte:
                if (Azulejo.Dado_4 > 0) Personagem(Índice).Direção = (Game.Direções)Azulejo.Dado_4 - 1;
                Transportar(Índice, Azulejo.Dado_1, (byte)Azulejo.Dado_2, (byte)Azulejo.Dado_3);
                OutroMovimento = true;
                break;
        }

        // Envia os dados
        if (!OutroMovimento && (x != Personagem(Índice).X || y != Personagem(Índice).Y))
            Enviar.Jogador_Mover(Índice, Movimento);
        else
            Enviar.Jogador_Posição(Índice);
    }

    public static void Transportar(byte Índice, short Mapa, byte x, byte y)
    {
        short Mapa_Antigo = Personagem(Índice).Mapa;

        // Evita que o jogador seja transportado para fora do limite
        if (Mapa == 0) return;
        if (x > Listas.Mapa[Mapa].Largura) x = Listas.Mapa[Mapa].Largura;
        if (y > Listas.Mapa[Mapa].Altura) y = Listas.Mapa[Mapa].Altura;
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        // Define a Posição do jogador
        Personagem(Índice).Mapa = Mapa;
        Personagem(Índice).X = x;
        Personagem(Índice).Y = y;

        // Envia os dados dos NPCs
        Enviar.Mapa_NPCs(Índice, Mapa);

        // Envia os dados para os outros jogadores
        if (Mapa_Antigo != Mapa)
            Enviar.SairDoMapa(Índice, Mapa_Antigo);

        Enviar.Jogador_Posição(Índice);

        // Atualiza os valores
        Listas.TempJogador[Índice].ObtendoMapa = true;

        // Verifica se será necessário enviar os dados do mapa para o jogador
        Enviar.Mapa_Revisão(Índice, Mapa);
    }

    public static void Atacar(byte Índice)
    {
        short Próx_X = Personagem(Índice).X, Próx_Y = Personagem(Índice).Y;
        byte Vítima_Índice;

        // Próximo azulejo
        Map.PróximoAzulejo(Personagem(Índice).Direção, ref Próx_X, ref Próx_Y);

        // Apenas se necessário
        if (Environment.TickCount < Personagem(Índice).Ataque_Tempo + 750) return;
        if (Map.Azulejo_Bloqueado(Personagem(Índice).Mapa, Personagem(Índice).X, Personagem(Índice).Y, Personagem(Índice).Direção, false)) goto continuar;

        // Ataca um jogador
        Vítima_Índice = Map.HáJogador(Personagem(Índice).Mapa, Próx_X, Próx_Y);
        if (Vítima_Índice > 0)
        {
            Atacar_Jogador(Índice, Vítima_Índice);
            return;
        }

        // Ataca um NPC
        Vítima_Índice = Map.HáNPC(Personagem(Índice).Mapa, Próx_X, Próx_Y);
        if (Vítima_Índice > 0)
        {
            Atacar_NPC(Índice, Vítima_Índice);
            return;
        }

        continuar:
        // Demonstra que aos outros jogadores o ataque
        Enviar.Jogador_Atacar(Índice, 0, 0);
        Personagem(Índice).Ataque_Tempo = Environment.TickCount;
    }

    public static void Atacar_Jogador(byte Índice, byte Vítima)
    {
        short Dano;
        short x = Personagem(Índice).X, y = Personagem(Índice).Y;

        // Define o azujelo a frente do jogador
        Map.PróximoAzulejo(Personagem(Índice).Direção, ref x, ref y);

        // Verifica se a vítima pode ser atacada
        if (!EstáJogando(Vítima)) return;
        if (Listas.TempJogador[Vítima].ObtendoMapa) return;
        if (Personagem(Índice).Mapa != Personagem(Vítima).Mapa) return;
        if (Personagem(Vítima).X != x || Personagem(Vítima).Y != y) return;
        if (Listas.Mapa[Personagem(Índice).Mapa].Moral == (byte)Map.Morais.Pacífico)
        {
            Enviar.Mensagem(Índice, "Essa é uma área pacífica.", Color.White);
            return;
        }

        // Demonstra o ataque aos outros jogadores
        Enviar.Jogador_Atacar(Índice, Vítima, (byte)Game.Alvo.Jogador);

        // Tempo de ataque 
        Personagem(Índice).Ataque_Tempo = Environment.TickCount;

        // Cálculo de dano
        Dano = (short)(Personagem(Índice).Dano - Personagem(Vítima).Jogador_Defesa);

        // Dano não fatal
        if (Dano <= Personagem(Vítima).MáxVital((byte)Game.Vitais.Vida))
        {
            Personagem(Vítima).Vital[(byte)Game.Vitais.Vida] -= Dano;
            Enviar.Jogador_Vitais(Vítima);
        }
        // FATALITY
        else
        {
            // Dá 10% da experiência da vítima ao atacante
            Personagem(Índice).Experiência += (short)(Personagem(Vítima).Experiência / 10);

            // Mata a vítima
            Morreu(Vítima);
        }
    }

    public static void Atacar_NPC(byte Índice, byte Vítima)
    {
        short Dano;
        short x = Personagem(Índice).X, y = Personagem(Índice).Y;
        Listas.Estruturas.Mapa_NPCs NPC = Listas.Mapa[Personagem(Índice).Mapa].Temp_NPC[Vítima];

        // Define o azujelo a frente do jogador
        Map.PróximoAzulejo(Personagem(Índice).Direção, ref x, ref y);

        // Verifica se a vítima pode ser atacada
        if (NPC.X != x || NPC.Y != y) return;
        if (Listas.NPC[NPC.Índice].Agressividade == (byte)global::NPC.Aggressiveness.Passive) return;

        // Define o alvo do NPC
        Listas.Mapa[Personagem(Índice).Mapa].Temp_NPC[Vítima].Alvo_Índice = Índice;
        Listas.Mapa[Personagem(Índice).Mapa].Temp_NPC[Vítima].Alvo_Tipo = (byte)Game.Alvo.Jogador;

        // Demonstra o ataque aos outros jogadores
        Enviar.Jogador_Atacar(Índice, Vítima, (byte)Game.Alvo.NPC);

        // Tempo de ataque 
        Personagem(Índice).Ataque_Tempo = Environment.TickCount;

        // Cálculo de dano
        Dano = (short)(Personagem(Índice).Dano - Listas.NPC[NPC.Índice].Atributo[(byte)Game.Atributos.Resistência]);

        // Dano não fatal
        if (Dano < Listas.Mapa[Personagem(Índice).Mapa].Temp_NPC[Vítima].Vital[(byte)Game.Vitais.Vida])
        {
            Listas.Mapa[Personagem(Índice).Mapa].Temp_NPC[Vítima].Vital[(byte)Game.Vitais.Vida] -= Dano;
            Enviar.Mapa_NPC_Vitais(Personagem(Índice).Mapa, Vítima);
        }
        // FATALITY
        else
        {
            // Experiência ganhada
            Personagem(Índice).Experiência += Listas.NPC[NPC.Índice].Experiência;

            // Reseta os dados do NPC 
            global::NPC.Morreu(Personagem(Índice).Mapa, Vítima);
        }
    }

    public static void Morreu(byte Índice)
    {
        Listas.Estruturas.Classes Dados = Listas.Classe[Personagem(Índice).Classe];

        // Recupera os vitais
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
            Personagem(Índice).Vital[n] = Personagem(Índice).MáxVital(n);

        // Perde 10% da experiência
        Personagem(Índice).Experiência /= 10;
        Enviar.Jogador_Experiência(Índice);

        // Retorna para o ínicio
        Personagem(Índice).Direção = (Game.Direções)Dados.Aparecer_Direção;
        Transportar(Índice, Dados.Aparecer_Mapa, Dados.Aparecer_X, Dados.Aparecer_Y);
    }

    public static void Lógica()
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
        {
            // Não é necessário
            if (!EstáJogando(i)) continue;

            ///////////////
            // Reneração // 
            ///////////////
            if (Environment.TickCount > Tie.Score_Player_Reneration + 5000)
                for (byte v = 0; v <= (byte)Game.Vitais.Quantidade - 1; v++)
                    if (Personagem(i).Vital[v] < Personagem(i).MáxVital(v))
                    {
                        // Renera a vida do jogador
                        Personagem(i).Vital[v] += Personagem(i).Regeneração(v);
                        if (Personagem(i).Vital[v] > Personagem(i).MáxVital(v)) Personagem(i).Vital[v] = Personagem(i).MáxVital(v);

                        // Envia os dados aos jogadores
                        Enviar.Jogador_Vitais(i);
                    }
        }

        // Reseta as contagens
        if (Environment.TickCount > Tie.Score_Player_Reneration + 5000) Tie.Score_Player_Reneration = Environment.TickCount;
    }

    public static void VerificarLevel(byte Índice)
    {
        byte NumLevel = 0; short ExpSobrando;

        // Previni erros
        if (!EstáJogando(Índice)) return;

        while (Personagem(Índice).Experiência >= Personagem(Índice).ExpNecessária)
        {
            NumLevel += 1;
            ExpSobrando = (short)(Personagem(Índice).Experiência - Personagem(Índice).ExpNecessária);

            // Define os dados
            Personagem(Índice).Level += 1;
            Personagem(Índice).Pontos += 3;
            Personagem(Índice).Experiência = ExpSobrando;
        }

        // Envia os dados
        Enviar.Jogador_Experiência(Índice);
        if (NumLevel > 0) Enviar.Jogadores_Dados_Mapa(Índice);
    }

    public static bool DarItem(byte Índice, short Item_Num, short Quantidade)
    {
        byte Slot_Item = EncontrarInventário(Índice, Item_Num);
        byte Slot_Vazio = EncontrarInventário(Índice, 0);

        // Somente se necessário
        if (Item_Num == 0) return false;
        if (Slot_Vazio == 0) return false;
        if (Quantidade == 0) Quantidade = 1;

        // Empilhável
        if (Slot_Item > 0 && Listas.Item[Item_Num].Empilhável)
            Personagem(Índice).Inventário[Slot_Item].Quantidade += Quantidade;
        // Não empilhável
        else
        {
            Personagem(Índice).Inventário[Slot_Vazio].Item_Num = Item_Num;
            Personagem(Índice).Inventário[Slot_Vazio].Quantidade = Quantidade;
        }

        // Envia os dados ao jogador
        Enviar.Jogador_Inventário(Índice);
        return true;
    }

    public static void SoltarItem(byte Índice, byte Slot)
    {
        short Mapa_Num = Personagem(Índice).Mapa;
        Listas.Estruturas.Mapa_Itens Mapa_Item = new Listas.Estruturas.Mapa_Itens();

        // Somente se necessário
        if (Listas.Mapa[Mapa_Num].Temp_Item.Count == Game.Max_Mapa_Itens) return;
        if (Personagem(Índice).Inventário[Slot].Item_Num == 0) return;
        if (Listas.Item[Personagem(Índice).Inventário[Slot].Item_Num].NãoDropável) return;

        // Solta o item no chão
        Mapa_Item.Índice = Personagem(Índice).Inventário[Slot].Item_Num;
        Mapa_Item.Quantidade = Personagem(Índice).Inventário[Slot].Quantidade;
        Mapa_Item.X = Personagem(Índice).X;
        Mapa_Item.Y = Personagem(Índice).Y;
        Listas.Mapa[Mapa_Num].Temp_Item.Add(Mapa_Item);
        Enviar.Mapa_Itens(Mapa_Num);

        // Retira o item do inventário do jogador 
        Personagem(Índice).Inventário[Slot].Item_Num = 0;
        Personagem(Índice).Inventário[Slot].Quantidade = 0;
        Enviar.Jogador_Inventário(Índice);
    }

    public static void UsarItem(byte Índice, byte Slot)
    {
        short Item_Num = Personagem(Índice).Inventário[Slot].Item_Num;

        // Somente se necessário
        if (Item_Num == 0) return;

        // Requerimentos
        if (Personagem(Índice).Level < Listas.Item[Item_Num].Req_Level)
        {
            Enviar.Mensagem(Índice, "Você não tem o level necessário para utilizar esse item.", Color.White);
            return;
        }
        if (Listas.Item[Item_Num].Req_Classe > 0)
            if (Personagem(Índice).Classe != Listas.Item[Item_Num].Req_Classe)
            {
                Enviar.Mensagem(Índice, "Você não pode utilizar esse item.", Color.White);
                return;
            }

        if (Listas.Item[Item_Num].Tipo == (byte)Game.Itens.Equipamento)
        {
            // Retira o item da hotbar
            byte HotbarSlot = EncontrarHotbar(Índice, (byte)Game.Hotbar.Item, Slot);
            Personagem(Índice).Hotbar[HotbarSlot].Tipo = 0;
            Personagem(Índice).Hotbar[HotbarSlot].Slot = 0;

            // Retira o item do inventário
            Personagem(Índice).Inventário[Slot].Item_Num = 0;
            Personagem(Índice).Inventário[Slot].Quantidade = 0;

            // Caso já estiver com algum equipamento, desequipa ele
            if (Personagem(Índice).Equipamento[Listas.Item[Item_Num].Equip_Tipo] > 0) DarItem(Índice, Item_Num, 1);

            // Equipa o item
            Personagem(Índice).Equipamento[Listas.Item[Item_Num].Equip_Tipo] = Item_Num;
            for (byte i = 0; i <= (byte)Game.Atributos.Quantidade - 1; i++) Personagem(Índice).Atributo[i] += Listas.Item[Item_Num].Equip_Atributo[i];

            // Envia os dados
            Enviar.Jogador_Inventário(Índice);
            Enviar.Jogador_Equipamentos(Índice);
            Enviar.Jogador_Hotbar(Índice);
        }
        else if (Listas.Item[Item_Num].Tipo == (byte)Game.Itens.Poção)
        {
            // Efeitos
            bool TeveEfeito = false;
            Personagem(Índice).Experiência += Listas.Item[Item_Num].Poção_Experiência;
            if (Personagem(Índice).Experiência < 0) Personagem(Índice).Experiência = 0;
            for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++)
            {
                // Verifica se o item causou algum efeito 
                if (Personagem(Índice).Vital[i] < Personagem(Índice).MáxVital(i) && Listas.Item[Item_Num].Poção_Vital[i] != 0) TeveEfeito = true;

                // Efeito
                Personagem(Índice).Vital[i] += Listas.Item[Item_Num].Poção_Vital[i];

                // Impede que passe dos limites
                if (Personagem(Índice).Vital[i] < 0) Personagem(Índice).Vital[i] = 0;
                if (Personagem(Índice).Vital[i] > Personagem(Índice).MáxVital(i)) Personagem(Índice).Vital[i] = Personagem(Índice).MáxVital(i);
            }

            // Foi fatal
            if (Personagem(Índice).Vital[(byte)Game.Vitais.Vida] == 0) Morreu(Índice);

            // Remove o item caso tenha tido algum efeito
            if (Listas.Item[Item_Num].Poção_Experiência > 0 || TeveEfeito)
            {
                Personagem(Índice).Inventário[Slot].Item_Num = 0;
                Personagem(Índice).Inventário[Slot].Quantidade = 0;
                Enviar.Jogador_Inventário(Índice);
                Enviar.Jogador_Vitais(Índice);
            }
        }
    }

    public static byte EncontrarHotbar(byte Índice, byte Tipo, byte Slot)
    {
        // Encontra algo especifico na hotbar
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
            if (Personagem(Índice).Hotbar[i].Tipo == Tipo && Personagem(Índice).Hotbar[i].Slot == Slot)
                return i;

        return 0;
    }

    public static byte EncontrarInventário(byte Índice, short Item_Num)
    {
        // Encontra algo especifico na hotbar
        for (byte i = 1; i <= Game.Max_Inventário; i++)
            if (Personagem(Índice).Inventário[i].Item_Num == Item_Num)
                return i;

        return 0;
    }
}