using System;
using System.Drawing;
using Lidgren.Network;

partial class Enviar
{
    // Pacotes do servidor
    public enum Pacotes
    {
        Alerta,
        Conectar,
        CriarPersonagem,
        Entrada,
        Classes,
        Personagens,
        Entrar,
        MaiorÍndice,
        Jogador_Dados,
        Jogador_Posição,
        Jogador_Vitais,
        Jogador_Saiu,
        Jogador_Atacar,
        Jogador_Mover,
        Jogador_Direção,
        Jogador_Experiência,
        Jogador_Inventário,
        Jogador_Equipamentos,
        Jogador_Hotbar,
        EntrarNoMapa,
        Mapa_Revisão,
        Mapa,
        Latência,
        Mensagem,
        NPCs,
        Mapa_NPCs,
        Mapa_NPC,
        Mapa_NPC_Movimento,
        Mapa_NPC_Direção,
        Mapa_NPC_Vitais,
        Mapa_NPC_Atacar,
        Mapa_NPC_Morreu,
        Itens,
        Mapa_Itens,
    }

    public static void Para(byte Índice, NetOutgoingMessage Dados)
    {
        // Previni sobrecarga
        if (!Rede.EstáConectado(Índice)) return;

        // Recria o pacote e o envia
        NetOutgoingMessage Dados_Enviar = Rede.Dispositivo.CreateMessage(Dados.LengthBytes);
        Dados_Enviar.Write(Dados);
        Rede.Dispositivo.SendMessage(Dados_Enviar, Rede.Conexão[Índice], NetDeliveryMethod.ReliableOrdered);
    }

    public static void ParaTodos(NetOutgoingMessage Dados)
    {
        // Envia os dados para todos conectados
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Jogador.EstáJogando(i))
                Para(i, Dados);
    }

    public static void ParaTodosMenos(byte Índice, NetOutgoingMessage Dados)
    {
        // Envia os dados para todos conectados, com excessão do índice
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Jogador.EstáJogando(i))
                if (Índice != i)
                    Para(i, Dados);
    }

    public static void ParaMapa(short Mapa, NetOutgoingMessage Dados)
    {
        // Envia os dados para todos conectados, com excessão do índice
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Jogador.EstáJogando(i))
                if (Jogador.Personagem(i).Mapa == Mapa)
                    Para(i, Dados);
    }

    public static void ParaMapaMenos(short Mapa, byte Índice, NetOutgoingMessage Dados)
    {
        // Envia os dados para todos conectados, com excessão do índice
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Jogador.EstáJogando(i))
                if (Jogador.Personagem(i).Mapa == Mapa)
                    if (Índice != i)
                        Para(i, Dados);
    }

    public static void Alerta(byte Índice, string Mensagem, bool Desconectar = true)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Alerta);
        Dados.Write(Mensagem);
        Para(Índice, Dados);

        // Desconecta o jogador
        if (Desconectar)
            Rede.Conexão[Índice].Disconnect(string.Empty);
    }

    public static void Mensagem(string Texto)
    {
        // Envia o alerta para todos
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Rede.Conexão[i] != null)
                if (Rede.Conexão[i].Status == NetConnectionStatus.Connected)
                    Alerta(i, Texto);
    }

    public static void Conectar(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Conectar);
        Dados.Write(Índice);
        Para(Índice, Dados);
    }

    public static void CriarPersonagem(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.CriarPersonagem);
        Para(Índice, Dados);
    }

    public static void Entrada(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Entrada);
        Dados.Write(Índice);
        Dados.Write(Jogo.MaiorÍndice);
        Dados.Write(Listas.Servidor_Dados.Máx_Jogadores);
        Para(Índice, Dados);
    }

    public static void Personagens(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Personagens);
        Dados.Write(Listas.Servidor_Dados.Máx_Personagens);

        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
        {
            Dados.Write(Listas.Jogador[Índice].Personagem[i].Nome);
            Dados.Write(Listas.Jogador[Índice].Personagem[i].Classe);
            Dados.Write(Listas.Jogador[Índice].Personagem[i].Gênero);
            Dados.Write(Listas.Jogador[Índice].Personagem[i].Level);
        }

        Para(Índice, Dados);
    }

    public static void Classes(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Classes);
        Dados.Write(Listas.Servidor_Dados.Num_Classes);

        for (byte i = 1; i <= Listas.Classe.GetUpperBound(0); i++)
        {
            Dados.Write(Listas.Classe[i].Nome);
            Dados.Write(Listas.Classe[i].Textura_Masculina);
            Dados.Write(Listas.Classe[i].Textura_Feminina);
        }

        // Envia os dados
        Para(Índice, Dados);
    }

    public static void Entrar(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Entrar);
        Para(Índice, Dados);
    }

    public static NetOutgoingMessage Jogador_Dados_Cache(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Escreve os dados
        Dados.Write((byte)Pacotes.Jogador_Dados);
        Dados.Write(Índice);
        Dados.Write(Jogador.Personagem(Índice).Nome);
        Dados.Write(Jogador.Personagem(Índice).Classe);
        Dados.Write(Jogador.Personagem(Índice).Gênero);
        Dados.Write(Jogador.Personagem(Índice).Level);
        Dados.Write(Jogador.Personagem(Índice).Mapa);
        Dados.Write(Jogador.Personagem(Índice).X);
        Dados.Write(Jogador.Personagem(Índice).Y);
        Dados.Write((byte)Jogador.Personagem(Índice).Direção);
        for (byte n = 0; n <= (byte)Jogo.Vitais.Quantidade - 1; n++)
        {
            Dados.Write(Jogador.Personagem(Índice).Vital[n]);
            Dados.Write(Jogador.Personagem(Índice).MáxVital(n));
        }
        for (byte n = 0; n <= (byte)Jogo.Atributos.Quantidade - 1; n++) Dados.Write(Jogador.Personagem(Índice).Atributo[n]);
        for (byte n = 0; n <= (byte)Jogo.Equipamentos.Quantidade - 1; n++) Dados.Write(Jogador.Personagem(Índice).Equipamento[n]);

        return Dados;
    }

    public static void Jogador_Posição(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Posição);
        Dados.Write(Índice);
        Dados.Write(Jogador.Personagem(Índice).X);
        Dados.Write(Jogador.Personagem(Índice).Y);
        Dados.Write((byte)Jogador.Personagem(Índice).Direção);
        ParaMapa(Jogador.Personagem(Índice).Mapa, Dados);
    }

    public static void Jogador_Vitais(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Vitais);
        Dados.Write(Índice);
        for (byte i = 0; i <= (byte)Jogo.Vitais.Quantidade - 1; i++)
        {
            Dados.Write(Jogador.Personagem(Índice).Vital[i]);
            Dados.Write(Jogador.Personagem(Índice).MáxVital(i));
        }

        ParaMapa(Jogador.Personagem(Índice).Mapa, Dados);
    }

    public static void Jogador_Saiu(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Saiu);
        Dados.Write(Índice);
        ParaTodosMenos(Índice, Dados);
    }

    public static void MaiorÍndice()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.MaiorÍndice);
        Dados.Write(Jogo.MaiorÍndice);
        ParaTodos(Dados);
    }

    public static void Jogador_Mover(byte Índice, byte Movimento)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Mover);
        Dados.Write(Índice);
        Dados.Write(Jogador.Personagem(Índice).X);
        Dados.Write(Jogador.Personagem(Índice).Y);
        Dados.Write((byte)Jogador.Personagem(Índice).Direção);
        Dados.Write(Movimento);
        ParaMapaMenos(Jogador.Personagem(Índice).Mapa, Índice, Dados);
    }

    public static void Jogador_Direção(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Direção);
        Dados.Write(Índice);
        Dados.Write((byte)Jogador.Personagem(Índice).Direção);
        ParaMapaMenos(Jogador.Personagem(Índice).Mapa, Índice, Dados);
    }

    public static void Jogador_Experiência(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Experiência);
        Dados.Write(Jogador.Personagem(Índice).Experiência);
        Dados.Write(Jogador.Personagem(Índice).ExpNecessária);
        Dados.Write(Jogador.Personagem(Índice).Pontos);
        Para(Índice, Dados);
    }

    public static void Jogador_Equipamentos(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Equipamentos);
        Dados.Write(Índice);
        for (byte i = 0; i <= (byte)Jogo.Equipamentos.Quantidade - 1; i++) Dados.Write(Jogador.Personagem(Índice).Equipamento[i]);
        ParaMapa(Jogador.Personagem(Índice).Mapa, Dados);
    }

    public static void Jogadores_Dados_Mapa(byte Índice)
    {
        // Envia os dados dos outros jogadores 
        for (byte i = 1; i <= Jogo.MaiorÍndice; i++)
            if (Jogador.EstáJogando(i))
                if (Índice != i)
                    if (Jogador.Personagem(i).Mapa == Jogador.Personagem(Índice).Mapa)
                        Para(Índice, Jogador_Dados_Cache(i));

        // Envia os dados do jogador
        ParaMapa(Jogador.Personagem(Índice).Mapa, Jogador_Dados_Cache(Índice));
    }

    public static void EntrarNoMapa(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.EntrarNoMapa);
        Para(Índice, Dados);
    }

    public static void SairDoMapa(byte Índice, short Mapa)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Saiu);
        Dados.Write(Índice);
        ParaMapaMenos(Mapa, Índice, Dados);
    }

    public static void Mapa_Revisão(byte Índice, short Mapa)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_Revisão);
        Dados.Write(Mapa);
        Dados.Write(Listas.Mapa[Mapa].Revisão);
        Para(Índice, Dados);
    }

    public static void Mapa(byte Índice, short Mapa)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa);
        Dados.Write(Mapa);
        Dados.Write(Listas.Mapa[Mapa].Revisão);
        Dados.Write(Listas.Mapa[Mapa].Nome);
        Dados.Write(Listas.Mapa[Mapa].Largura);
        Dados.Write(Listas.Mapa[Mapa].Altura);
        Dados.Write(Listas.Mapa[Mapa].Moral);
        Dados.Write(Listas.Mapa[Mapa].Panorama);
        Dados.Write(Listas.Mapa[Mapa].Música);
        Dados.Write(Listas.Mapa[Mapa].Coloração);
        Dados.Write(Listas.Mapa[Mapa].Clima.Tipo);
        Dados.Write(Listas.Mapa[Mapa].Clima.Intensidade);
        Dados.Write(Listas.Mapa[Mapa].Fumaça.Textura);
        Dados.Write(Listas.Mapa[Mapa].Fumaça.VelocidadeX);
        Dados.Write(Listas.Mapa[Mapa].Fumaça.VelocidadeY);
        Dados.Write(Listas.Mapa[Mapa].Fumaça.Transparência);

        // Ligações
        for (short i = 0; i <= (short)Jogo.Direções.Quantidade - 1; i++)
            Dados.Write(Listas.Mapa[Mapa].Ligação[i]);

        // Azulejos
        Dados.Write((byte)Listas.Mapa[Mapa].Azulejo[0, 0].Dados.GetUpperBound(1));
        for (byte x = 0; x <= Listas.Mapa[Mapa].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Mapa].Altura; y++)
                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= Listas.Mapa[Mapa].Azulejo[x, y].Dados.GetUpperBound(1); q++)
                    {
                        Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Dados[c, q].x);
                        Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Dados[c, q].y);
                        Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Dados[c, q].Azulejo);
                        Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Dados[c, q].Automático);
                    }

        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa[Mapa].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Mapa].Altura; y++)
            {
                Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Atributo);
                for (byte i = 0; i <= (byte)Jogo.Direções.Quantidade - 1; i++)
                    Dados.Write(Listas.Mapa[Mapa].Azulejo[x, y].Bloqueio[i]);
            }

        // Luzes
        Dados.Write(Listas.Mapa[Mapa].Luz.GetUpperBound(0));
        if (Listas.Mapa[Mapa].Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Listas.Mapa[Mapa].Luz.GetUpperBound(0); i++)
            {
                Dados.Write(Listas.Mapa[Mapa].Luz[i].X);
                Dados.Write(Listas.Mapa[Mapa].Luz[i].Y);
                Dados.Write(Listas.Mapa[Mapa].Luz[i].Largura);
                Dados.Write(Listas.Mapa[Mapa].Luz[i].Altura);
            }

        // NPCs
        Dados.Write((short)Listas.Mapa[Mapa].NPC.GetUpperBound(0));
        if (Listas.Mapa[Mapa].NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Listas.Mapa[Mapa].NPC.GetUpperBound(0); i++)
                Dados.Write(Listas.Mapa[Mapa].NPC[i].Índice);

        Para(Índice, Dados);
    }

    public static void Latência(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Latência);
        Para(Índice, Dados);
    }

    public static void Mensagem(byte Índice, string Mensagem, Color Cor)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mensagem);
        Dados.Write(Mensagem);
        Dados.Write(Cor.ToArgb());
        Para(Índice, Dados);
    }

    public static void Mensagem_Mapa(byte Índice, string Texto)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();
        string Mensagem = "[Mapa] " + Jogador.Personagem(Índice).Nome + ": " + Texto;

        // Envia os dados
        Dados.Write((byte)Pacotes.Mensagem);
        Dados.Write(Mensagem);
        Dados.Write(Color.White.ToArgb());
        ParaMapa(Jogador.Personagem(Índice).Mapa, Dados);
    }

    public static void Mensagem_Global(byte Índice, string Texto)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();
        string Mensagem = "[Global] " + Jogador.Personagem(Índice).Nome + ": " + Texto;

        // Envia os dados
        Dados.Write((byte)Pacotes.Mensagem);
        Dados.Write(Mensagem);
        Dados.Write(Color.Yellow.ToArgb());
        ParaTodos(Dados);
    }

    public static void Mensagem_Global(string Mensagem)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mensagem);
        Dados.Write(Mensagem);
        Dados.Write(Color.Yellow.ToArgb());
        ParaTodos(Dados);
    }

    public static void Mensagem_Particular(byte Índice, string Destinatário_Nome, string Texto)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();
        byte Destinatário = Jogador.Encontrar(Destinatário_Nome);

        // Verifica se o jogador está conectado
        if (Destinatário == 0)
        {
            Mensagem(Índice, Destinatário_Nome + " não está conectado no momento.", Color.Blue);
            return;
        }

        // Envia as mensagens
        Mensagem(Índice, "[Para] " + Destinatário_Nome + ": " + Texto, Color.Pink);
        Mensagem(Destinatário, "[De] " + Jogador.Personagem(Índice).Nome + ": " + Texto, Color.Pink);
    }

    public static void Jogador_Atacar(byte Índice, byte Vítima, byte Vítima_Tipo)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Atacar);
        Dados.Write(Índice);
        Dados.Write(Vítima);
        Dados.Write(Vítima_Tipo);
        ParaMapa(Jogador.Personagem(Índice).Mapa, Dados);
    }

    public static void Itens(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Itens);
        Dados.Write((byte)Listas.Item.GetUpperBound(0));

        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++)
        {
            // Geral
            Dados.Write(Listas.Item[i].Nome);
            Dados.Write(Listas.Item[i].Descrição);
            Dados.Write(Listas.Item[i].Textura);
            Dados.Write(Listas.Item[i].Tipo);
            Dados.Write(Listas.Item[i].Req_Level);
            Dados.Write(Listas.Item[i].Req_Classe);
            Dados.Write(Listas.Item[i].Poção_Experiência);
            for (byte n = 0;n<= (byte)Jogo.Vitais.Quantidade - 1; n++) Dados.Write(Listas.Item[i].Poção_Vital[n]);
            Dados.Write(Listas.Item[i].Equip_Tipo );
            for (byte n = 0; n <= (byte)Jogo.Atributos.Quantidade - 1; n++) Dados.Write(Listas.Item[i].Equip_Atributo[n]);
            Dados.Write(Listas.Item[i].Arma_Dano);
        }

        // Envia os dados
        Para(Índice, Dados);
    }

    public static void Mapa_Itens(byte Índice, short Mapa_Num)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_Itens);
        Dados.Write((short)(Listas.Mapa[Mapa_Num].Temp_Item.Count - 1));

        for (byte i = 1; i <= Listas.Mapa[Mapa_Num].Temp_Item.Count - 1; i++)
        {
            // Geral
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].Índice);
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].X);
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].Y);
        }

        // Envia os dados
        Para(Índice, Dados);
    }

    public static void Mapa_Itens(short Mapa_Num)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_Itens);
        Dados.Write((short)(Listas.Mapa[Mapa_Num].Temp_Item.Count - 1));
        for (byte i = 1; i <= Listas.Mapa[Mapa_Num].Temp_Item.Count - 1; i++)
        {
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].Índice);
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].X);
            Dados.Write(Listas.Mapa[Mapa_Num].Temp_Item[i].Y);
        }
        ParaMapa(Mapa_Num, Dados);
    }

    public static void Jogador_Inventário(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Inventário);
        for (byte i = 1; i <= Jogo.Máx_Inventário; i++)
        {
            Dados.Write(Jogador.Personagem(Índice).Inventário[i].Item_Num);
            Dados.Write(Jogador.Personagem(Índice).Inventário[i].Quantidade);
        }
        Para(Índice, Dados);
    }

    public static void Jogador_Hotbar(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Hotbar);
        for (byte i = 1; i <= Jogo.Máx_Hotbar; i++)
        {
            Dados.Write(Jogador.Personagem(Índice).Hotbar[i].Tipo);
            Dados.Write(Jogador.Personagem(Índice).Hotbar[i].Slot);
        }
        Para(Índice, Dados);
    }
}