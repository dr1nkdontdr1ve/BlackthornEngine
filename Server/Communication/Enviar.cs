using System;
using System.Drawing;
using Lidgren.Network;

partial class Submit
{
    // Server Packages
    public enum Packages
    {
        Alerta,
        Conectar,
        CriarPersonagem,
        Entrada,
        Classes,
        Personagens,
        Entrar,
        MaiorÍndice,
        Player_Data,
        Player_Posição,
        Player_Vitais,
        Player_Saiu,
        Player_Atacar,
        Player_Mover,
        Player_Direção,
        Player_Experiência,
        Player_Inventário,
        Player_Equipamentos,
        Player_Hotbar,
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

    public static void Para(byte Índice, NetOutgoingMessage Data)
    {
        // Previni sobrecarga
        if (!Rede.EstáConectado(Índice)) return;

        // Recria o pacote e o envia
        NetOutgoingMessage Data_Enviar = Rede.Dispositivo.CreateMessage(Data.LengthBytes);
        Data_Enviar.Write(Data);
        Rede.Dispositivo.SendMessage(Data_Enviar, Rede.Conexão[Índice], NetDeliveryMethod.ReliableOrdered);
    }

    public static void ParaTodos(NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                Para(i, Data);
    }

    public static void ParaTodosMenos(byte Índice, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do índice
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Índice != i)
                    Para(i, Data);
    }

    public static void ParaMapa(short Mapa, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do índice
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Player.Personagem(i).Mapa == Mapa)
                    Para(i, Data);
    }

    public static void ParaMapaMenos(short Mapa, byte Índice, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do índice
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Player.Personagem(i).Mapa == Mapa)
                    if (Índice != i)
                        Para(i, Data);
    }

    public static void Alerta(byte Índice, string Mensagem, bool Desconectar = true)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Alerta);
        Data.Write(Mensagem);
        Para(Índice, Data);

        // Desconecta o Player
        if (Desconectar)
            Rede.Conexão[Índice].Disconnect(string.Empty);
    }

    public static void Mensagem(string Text)
    {
        // Envia o alerta para todos
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Rede.Conexão[i] != null)
                if (Rede.Conexão[i].Status == NetConnectionStatus.Connected)
                    Alerta(i, Text);
    }

    public static void Conectar(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Conectar);
        Data.Write(Índice);
        Para(Índice, Data);
    }

    public static void CriarPersonagem(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.CriarPersonagem);
        Para(Índice, Data);
    }

    public static void Entrada(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Entrada);
        Data.Write(Índice);
        Data.Write(Game.MaiorÍndice);
        Data.Write(Lists.Server_Data.Max_Playeres);
        Para(Índice, Data);
    }

    public static void Personagens(byte Índice)
    {
        NetOutgoingMessage Data = Network.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Personagens);
        Data.Write(Lists.Server_Data.Máx_Personagens);

        for (byte i = 1; i <= Lists.Server_Data.Máx_Personagens; i++)
        {
            Data.Write(Lists.Player[Índice].Personagem[i].Nome);
            Data.Write(Lists.Player[Índice].Personagem[i].Classe);
            Data.Write(Lists.Player[Índice].Personagem[i].Gênero);
            Data.Write(Lists.Player[Índice].Personagem[i].Level);
        }

        Para(Índice, Data);
    }

    public static void Classes(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Send the data
        Data.Write((byte)Packages.Classes);
        Data.Write(Lists.Server_Data.Num_Classes);

        for (byte i = 1; i <= Lists.Classe.GetUpperBound(0); i++)
        {
            Data.Write(Lists.Classe[i].Nome);
            Data.Write(Lists.Classe[i].Textura_Masculina);
            Data.Write(Lists.Classe[i].Textura_Feminina);
        }

        // Envia os Data
        Para(Índice, Data);
    }

    public static void Entrar(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Entrar);
        Para(Índice, Data);
    }

    public static NetOutgoingMessage Player_Data_Cache(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Escreve os Data
        Data.Write((byte)Packages.Player_Data);
        Data.Write(Índice);
        Data.Write(Player.Personagem(Índice).Nome);
        Data.Write(Player.Personagem(Índice).Classe);
        Data.Write(Player.Personagem(Índice).Gênero);
        Data.Write(Player.Personagem(Índice).Level);
        Data.Write(Player.Personagem(Índice).Mapa);
        Data.Write(Player.Personagem(Índice).X);
        Data.Write(Player.Personagem(Índice).Y);
        Data.Write((byte)Player.Personagem(Índice).Direção);
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
        {
            Data.Write(Player.Personagem(Índice).Vital[n]);
            Data.Write(Player.Personagem(Índice).MáxVital(n));
        }
        for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Data.Write(Player.Personagem(Índice).Atributo[n]);
        for (byte n = 0; n <= (byte)Game.Equipamentos.Quantidade - 1; n++) Data.Write(Player.Personagem(Índice).Equipamento[n]);

        return Data;
    }

    public static void Player_Posição(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Posição);
        Data.Write(Índice);
        Data.Write(Player.Personagem(Índice).X);
        Data.Write(Player.Personagem(Índice).Y);
        Data.Write((byte)Player.Personagem(Índice).Direção);
        ParaMapa(Player.Personagem(Índice).Mapa, Data);
    }

    public static void Player_Vitais(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Vitais);
        Data.Write(Índice);
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++)
        {
            Data.Write(Player.Personagem(Índice).Vital[i]);
            Data.Write(Player.Personagem(Índice).MáxVital(i));
        }

        ParaMapa(Player.Personagem(Índice).Mapa, Data);
    }

    public static void Player_Saiu(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Saiu);
        Data.Write(Índice);
        ParaTodosMenos(Índice, Data);
    }

    public static void MaiorÍndice()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.MaiorÍndice);
        Data.Write(Game.MaiorÍndice);
        ParaTodos(Data);
    }

    public static void Player_Mover(byte Índice, byte Movimento)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Mover);
        Data.Write(Índice);
        Data.Write(Player.Personagem(Índice).X);
        Data.Write(Player.Personagem(Índice).Y);
        Data.Write((byte)Player.Personagem(Índice).Direção);
        Data.Write(Movimento);
        ParaMapaMenos(Player.Personagem(Índice).Mapa, Índice, Data);
    }

    public static void Player_Direção(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Direção);
        Data.Write(Índice);
        Data.Write((byte)Player.Personagem(Índice).Direção);
        ParaMapaMenos(Player.Personagem(Índice).Mapa, Índice, Data);
    }

    public static void Player_Experiência(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Experiência);
        Data.Write(Player.Personagem(Índice).Experiência);
        Data.Write(Player.Personagem(Índice).ExpNecessária);
        Data.Write(Player.Personagem(Índice).Pontos);
        Para(Índice, Data);
    }

    public static void Player_Equipamentos(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Equipamentos);
        Data.Write(Índice);
        for (byte i = 0; i <= (byte)Game.Equipamentos.Quantidade - 1; i++) Data.Write(Player.Personagem(Índice).Equipamento[i]);
        ParaMapa(Player.Personagem(Índice).Mapa, Data);
    }

    public static void Playeres_Data_Mapa(byte Índice)
    {
        // Envia os Data dos outros Playeres 
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Índice != i)
                    if (Player.Personagem(i).Mapa == Player.Personagem(Índice).Mapa)
                        Para(Índice, Player_Data_Cache(i));

        // Envia os Data do Player
        ParaMapa(Player.Personagem(Índice).Mapa, Player_Data_Cache(Índice));
    }

    public static void EntrarNoMapa(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.EntrarNoMapa);
        Para(Índice, Data);
    }

    public static void SairDoMapa(byte Índice, short Mapa)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Saiu);
        Data.Write(Índice);
        ParaMapaMenos(Mapa, Índice, Data);
    }

    public static void Mapa_Revisão(byte Índice, short Mapa)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mapa_Revisão);
        Data.Write(Mapa);
        Data.Write(Lists.Mapa[Mapa].Revisão);
        Para(Índice, Data);
    }

    public static void Mapa(byte Índice, short Mapa)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mapa);
        Data.Write(Mapa);
        Data.Write(Lists.Mapa[Mapa].Revisão);
        Data.Write(Lists.Mapa[Mapa].Name);
        Data.Write(Lists.Mapa[Mapa].Width);
        Data.Write(Lists.Mapa[Mapa].Height);
        Data.Write(Lists.Mapa[Mapa].Moral);
        Data.Write(Lists.Mapa[Mapa].Panorama);
        Data.Write(Lists.Mapa[Mapa].Music);
        Data.Write(Lists.Mapa[Mapa].Coloração);
        Data.Write(Lists.Mapa[Mapa].Clima.Type);
        Data.Write(Lists.Mapa[Mapa].Clima.Intensidade);
        Data.Write(Lists.Mapa[Mapa].Fumaça.Texture);
        Data.Write(Lists.Mapa[Mapa].Fumaça.VelocidadeX);
        Data.Write(Lists.Mapa[Mapa].Fumaça.VelocidadeY);
        Data.Write(Lists.Mapa[Mapa].Fumaça.Transparency);

        // Ligações
        for (short i = 0; i <= (short)Game.Direções.Quantidade - 1; i++)
            Data.Write(Lists.Mapa[Mapa].Ligação[i]);

        // Azulejos
        Data.Write((byte)Lists.Mapa[Mapa].Azulejo[0, 0].Data.GetUpperBound(1));
        for (byte x = 0; x <= Lists.Mapa[Mapa].Width; x++)
            for (byte y = 0; y <= Lists.Mapa[Mapa].Height; y++)
                for (byte c = 0; c <= (byte)global::Map.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= Lists.Mapa[Mapa].Azulejo[x, y].Data.GetUpperBound(1); q++)
                    {
                        Data.Write(Lists.Mapa[Mapa].Azulejo[x, y].Data[c, q].x);
                        Data.Write(Lists.Mapa[Mapa].Azulejo[x, y].Data[c, q].y);
                        Data.Write(Lists.Mapa[Mapa].Azulejo[x, y].Data[c, q].Azulejo);
                        Data.Write(Lists.Mapa[Mapa].Azulejo[x, y].Data[c, q].Automático);
                    }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Mapa[Mapa].Width; x++)
            for (byte y = 0; y <= Lists.Mapa[Mapa].Height; y++)
            {
                Data.Write(Lists.Mapa[Mapa].Tile[x, y].Attribute);
                for (byte i = 0; i <= (byte)Game.Direções.Quantidade - 1; i++)
                    Data.Write(Lists.Mapa[Mapa].Tile[x, y].Block[i]);
            }

        // Luzes
        Data.Write(Lists.Mapa[Mapa].Light.GetUpperBound(0));
        if (Lists.Mapa[Mapa].Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Mapa[Mapa].Light.GetUpperBound(0); i++)
            {
                Data.Write(Lists.Mapa[Mapa].Light[i].X);
                Data.Write(Lists.Mapa[Mapa].Light[i].Y);
                Data.Write(Lists.Mapa[Mapa].Light[i].Width);
                Data.Write(Lists.Mapa[Mapa].Light[i].Height);
            }

        // NPCs
        Data.Write((short)Lists.Mapa[Mapa].NPC.GetUpperBound(0));
        if (Listas.Mapa[Mapa].NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Mapa[Mapa].NPC.GetUpperBound(0); i++)
                Data.Write(Lists.Mapa[Mapa].NPC[i].Index);

        Para(Índice, Data);
    }

    public static void Latência(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Latência);
        Para(Índice, Data);
    }

    public static void Mensagem(byte Índice, string Mensagem, Color Cor)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mensagem);
        Data.Write(Mensagem);
        Data.Write(Cor.ToArgb());
        Para(Índice, Data);
    }

    public static void Mensagem_Mapa(byte Índice, string Texto)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();
        string Mensagem = "[Mapa] " + Player.Personagem(Índice).Nome + ": " + Texto;

        // Envia os Data
        Data.Write((byte)Packages.Mensagem);
        Data.Write(Mensagem);
        Data.Write(Color.White.ToArgb());
        ParaMapa(Player.Personagem(Índice).Mapa, Data);
    }

    public static void Mensagem_Global(byte Índice, string Texto)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();
        string Mensagem = "[Global] " + Player.Personagem(Índice).Nome + ": " + Texto;

        // Envia os Data
        Data.Write((byte)Packages.Mensagem);
        Data.Write(Mensagem);
        Data.Write(Color.Yellow.ToArgb());
        ParaTodos(Data);
    }

    public static void Mensagem_Global(string Mensagem)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mensagem);
        Data.Write(Mensagem);
        Data.Write(Color.Yellow.ToArgb());
        ParaTodos(Data);
    }

    public static void Mensagem_Particular(byte Índice, string Destinatário_Nome, string Texto)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();
        byte Destinatário = Player.Encontrar(Destinatário_Nome);

        // Verifica se o Player está conectado
        if (Destinatário == 0)
        {
            Mensagem(Índice, Destinatário_Nome + " não está conectado no momento.", Color.Blue);
            return;
        }

        // Envia as mensagens
        Mensagem(Índice, "[Para] " + Destinatário_Nome + ": " + Texto, Color.Pink);
        Mensagem(Destinatário, "[De] " + Player.Personagem(Índice).Nome + ": " + Texto, Color.Pink);
    }

    public static void Player_Atacar(byte Índice, byte Vítima, byte Vítima_Tipo)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Atacar);
        Data.Write(Índice);
        Data.Write(Vítima);
        Data.Write(Vítima_Tipo);
        ParaMapa(Player.Personagem(Índice).Mapa, Data);
    }

    public static void Itens(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Itens);
        Data.Write((byte)Lists.Item.GetUpperBound(0));

        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
        {
            // Geral
            Data.Write(Lists.Item[i].Name);
            Data.Write(Lists.Item[i].Descrição);
            Data.Write(Lists.Item[i].Texture);
            Data.Write(Lists.Item[i].Type);
            Data.Write(Lists.Item[i].Req_Level);
            Data.Write(Lists.Item[i].Req_Classe);
            Data.Write(Lists.Item[i].Poção_Experiência);
            for (byte n = 0;n<= (byte)Game.Vitais.Quantidade - 1; n++) Data.Write(Lists.Item[i].Poção_Vital[n]);
            Data.Write(Lists.Item[i].Equip_Tipo );
            for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Data.Write(Lists.Item[i].Equip_Atributo[n]);
            Data.Write(Lists.Item[i].Arma_Dano);
        }

        // Envia os Data
        Para(Índice, Data);
    }

    public static void Mapa_Itens(byte Índice, short Mapa_Num)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mapa_Itens);
        Data.Write((short)(Lists.Mapa[Mapa_Num].Temp_Item.Count - 1));

        for (byte i = 1; i <= Lists.Mapa[Mapa_Num].Temp_Item.Count - 1; i++)
        {
            // Geral
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].Index);
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].X);
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].Y);
        }

        // Envia os Data
        Para(Índice, Data);
    }

    public static void Mapa_Itens(short Mapa_Num)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Mapa_Itens);
        Data.Write((short)(Lists.Mapa[Mapa_Num].Temp_Item.Count - 1));
        for (byte i = 1; i <= Lists.Mapa[Mapa_Num].Temp_Item.Count - 1; i++)
        {
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].Index);
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].X);
            Data.Write(Lists.Mapa[Mapa_Num].Temp_Item[i].Y);
        }
        ParaMapa(Mapa_Num, Data);
    }

    public static void Player_Inventário(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Inventário);
        for (byte i = 1; i <= Game.Max_Inventário; i++)
        {
            Data.Write(Player.Personagem(Índice).Inventário[i].Item_Num);
            Data.Write(Player.Personagem(Índice).Inventário[i].Quantidade);
        }
        Para(Índice, Data);
    }

    public static void Player_Hotbar(byte Índice)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Hotbar);
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
        {
            Data.Write(Player.Personagem(Índice).Hotbar[i].Tipo);
            Data.Write(Player.Personagem(Índice).Hotbar[i].Slot);
        }
        Para(Índice, Data);
    }
}