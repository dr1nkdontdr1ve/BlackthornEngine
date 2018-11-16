using System;
using System.IO;

class Map
{
    ////////////////
    // Numerações //
    ////////////////
    public enum Morais
    {
        Pacífico,
        Perigoso,
        Quantidade
    }

    public enum Camadas
    {
        Chão,
        Telhado,
        Quantidade
    }

    public enum Atributos
    {
        Nenhum,
        Bloqueio,
        Teletransporte,
        Item,
        Quantidade
    }

    public static void Lógica()
    {
        for (byte i = 1; i <= Listas.Mapa.GetUpperBound(0); i++)
        {
            // Não é necessário fazer todos os cálculos se não houver nenhum jogador no mapa
            if (!HáJogadores(i)) continue;

            // Lógica dos NPCs
            NPC.Lógica(i);

            // Faz reaparecer todos os itens do mapa
            if (Environment.TickCount > Tie.Score_Map_Items + 300000)
            {
                Listas.Mapa[i].Temp_Item = new System.Collections.Generic.List<Listas.Estruturas.Mapa_Itens>();
                Aparecer_Itens(i);
                Enviar.Mapa_Itens(i);
            }
        }

        // Reseta as contagens
        if (Environment.TickCount > Tie.Score_NPC_Reneration + 5000) Tie.Score_NPC_Reneration = Environment.TickCount;
        if (Environment.TickCount > Tie.Score_Map_Items + 300000) Tie.Score_Map_Items = Environment.TickCount;
    }

    public static byte HáNPC(short Mapa_Num, short X, short Y)
    {
        // Verifica se há algum npc na cordenada
        for (byte i = 1; i <= Listas.Mapa[Mapa_Num].Temp_NPC.GetUpperBound(0); i++)
            if (Listas.Mapa[Mapa_Num].Temp_NPC[i].Índice > 0)
                if (Listas.Mapa[Mapa_Num].Temp_NPC[i].X == X && Listas.Mapa[Mapa_Num].Temp_NPC[i].Y == Y)
                    return i;

        return 0;
    }

    public static byte HáJogador(short Mapa_Num, short X, short Y)
    {
        // Verifica se há algum Jogador na cordenada
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Player.Personagem(i).X == X && Player.Personagem(i).Y == Y && Player.Personagem(i).Mapa == Mapa_Num)
                    return i;

        return 0;
    }

    public static bool HáJogadores(short Mapa_Num)
    {
        // Verifica se tem algum jogador no mapa
        for (byte i = 1; i <= Game.MaiorÍndice; i++)
            if (Player.EstáJogando(i))
                if (Player.Personagem(i).Mapa == Mapa_Num)
                    return true;

        return false;
    }

    public static byte HáItem(short Mapa_Num, byte X, byte Y)
    {
        // Verifica se tem algum item nas coordenadas 
        for (byte i = (byte)(Listas.Mapa[Mapa_Num].Temp_Item.Count - 1); i >= 1; i--)
            if (Listas.Mapa[Mapa_Num].Temp_Item[i].X == X && Listas.Mapa[Mapa_Num].Temp_Item[i].Y == Y)
                return i;

        return 0;
    }

    public static bool ForaDoLimite(short Mapa_Num, short X, short Y)
    {
        // Verifica se as coordenas estão no limite do mapa
        if (X > Listas.Mapa[Mapa_Num].Largura || Y > Listas.Mapa[Mapa_Num].Altura || X < 0 || Y < 0)
            return true;
        else
            return false;
    }

    public static void PróximoAzulejo(Game.Direções Direção, ref short X, ref short Y)
    {
        // Próximo azulejo
        switch (Direção)
        {
            case Game.Direções.Acima: Y -= 1; break;
            case Game.Direções.Abaixo: Y += 1; break;
            case Game.Direções.Direita: X += 1; break;
            case Game.Direções.Esquerda: X -= 1; break;
        }
    }

    public static bool Azulejo_Bloqueado(short Mapa_Num, short X, short Y)
    {
        // Verifica se o azulejo está bloqueado
        if (ForaDoLimite(Mapa_Num, X, Y))
            return true;
        else if (Listas.Mapa[Mapa_Num].Azulejo[X, Y].Atributo == (byte)Atributos.Bloqueio)
            return true;

        return false;
    }

    public static bool Azulejo_Bloqueado(short Mapa_Num, short X, short Y, Game.Direções Direção, bool ContarPersonagens = true)
    {
        short Próximo_X = X, Próximo_Y = Y;

        // Próximo azulejo
        PróximoAzulejo(Direção, ref Próximo_X, ref Próximo_Y);

        // Verifica se o azulejo está bloqueado
        if (Azulejo_Bloqueado(Mapa_Num, (byte)Próximo_X, (byte)Próximo_Y))
            return true;
        else if (Listas.Mapa[Mapa_Num].Azulejo[Próximo_X, Próximo_Y].Bloqueio[(byte)Game.DireçãoInversa(Direção)])
            return true;
        else if (Listas.Mapa[Mapa_Num].Azulejo[X, Y].Bloqueio[(byte)Direção])
            return true;
        else if (ContarPersonagens && (HáJogador(Mapa_Num, Próximo_X, Próximo_Y) > 0 || HáNPC(Mapa_Num, Próximo_X, Próximo_Y) > 0))
            return true;

        return false;
    }

    public static void Aparecer_Itens(short Mapa_Num)
    {
        Listas.Estruturas.Mapas Dados = Listas.Mapa[Mapa_Num];
        Listas.Estruturas.Mapa_Itens Item = new Listas.Estruturas.Mapa_Itens();

        // Verifica se tem algum atributo de item no mapa
        for (byte x = 0; x <= Dados.Largura; x++)
            for (byte y = 0; y <= Dados.Altura; y++)
                if (Dados.Azulejo[x, y].Atributo == (byte)Atributos.Item)
                {
                    // Faz o item aparecer
                    Item.Índice = Dados.Azulejo[x, y].Dado_1;
                    Item.Quantidade = Dados.Azulejo[x, y].Dado_2;
                    Item.X = x;
                    Item.Y = y;
                    Listas.Mapa[Mapa_Num].Temp_Item.Add(Item);
                }
    }
}

partial class Ler
{
    public static void Mapas()
    {
        Listas.Mapa = new Listas.Estruturas.Mapas[Listas.Servidor_Dados.Num_Mapas + 1];

        // Lê os dados
        for (short i = 1; i <= Listas.Mapa.GetUpperBound(0); i++)
            Mapa(i);
    }

    public static void Mapa(short Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Mapa[Índice].Revisão = Binário.ReadInt16();
        Listas.Mapa[Índice].Nome = Binário.ReadString();
        Listas.Mapa[Índice].Largura = Binário.ReadByte();
        Listas.Mapa[Índice].Altura = Binário.ReadByte();
        Listas.Mapa[Índice].Moral = Binário.ReadByte();
        Listas.Mapa[Índice].Panorama = Binário.ReadByte();
        Listas.Mapa[Índice].Música = Binário.ReadByte();
        Listas.Mapa[Índice].Coloração = Binário.ReadInt32();
        Listas.Mapa[Índice].Clima.Tipo = Binário.ReadByte();
        Listas.Mapa[Índice].Clima.Intensidade = Binário.ReadByte();
        Listas.Mapa[Índice].Fumaça.Textura = Binário.ReadByte();
        Listas.Mapa[Índice].Fumaça.VelocidadeX = Binário.ReadSByte();
        Listas.Mapa[Índice].Fumaça.VelocidadeY = Binário.ReadSByte();
        Listas.Mapa[Índice].Fumaça.Transparência = Binário.ReadByte();
        Listas.Mapa[Índice].LuzGlobal = Binário.ReadByte();
        Listas.Mapa[Índice].Iluminação = Binário.ReadByte();

        // Ligações
        Listas.Mapa[Índice].Ligação = new short[(byte)Game.Direções.Quantidade];
        for (short i = 0; i <= (short)Game.Direções.Quantidade - 1; i++)
            Listas.Mapa[Índice].Ligação[i] = Binário.ReadInt16();

        // Azulejos
        Mapa_Azulejos(Índice, Binário);

        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
            {
                Listas.Mapa[Índice].Azulejo[x, y].Atributo = Binário.ReadByte();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_1 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_2 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_3 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_4 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Zona = Binário.ReadByte();

                // Bloqueio direcional
                Listas.Mapa[Índice].Azulejo[x, y].Bloqueio = new bool[(byte)Game.Direções.Quantidade];
                for (byte i = 0; i <= (byte)Game.Direções.Quantidade - 1; i++)
                    Listas.Mapa[Índice].Azulejo[x, y].Bloqueio[i] = Binário.ReadBoolean();
            }

        // Luzes
        Listas.Mapa[Índice].Luz = new Listas.Estruturas.Luz[Binário.ReadByte()];
        if (Listas.Mapa[Índice].Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Listas.Mapa[Índice].Luz.GetUpperBound(0); i++)
            {
                Listas.Mapa[Índice].Luz[i].X = Binário.ReadByte();
                Listas.Mapa[Índice].Luz[i].Y = Binário.ReadByte();
                Listas.Mapa[Índice].Luz[i].Largura = Binário.ReadByte();
                Listas.Mapa[Índice].Luz[i].Altura = Binário.ReadByte();
            }

        // NPCs
        Listas.Mapa[Índice].NPC = new Listas.Estruturas.Mapa_NPC[Binário.ReadByte() + 1];
        Listas.Mapa[Índice].Temp_NPC = new Listas.Estruturas.Mapa_NPCs[Listas.Mapa[Índice].NPC.GetUpperBound(0) + 1];
        if (Listas.Mapa[Índice].NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Listas.Mapa[Índice].NPC.GetUpperBound(0); i++)
            {
                Listas.Mapa[Índice].NPC[i].Índice = Binário.ReadInt16();
                Listas.Mapa[Índice].NPC[i].Zona = Binário.ReadByte();
                Listas.Mapa[Índice].NPC[i].Aparecer = Binário.ReadBoolean();
                Listas.Mapa[Índice].NPC[i].X = Binário.ReadByte();
                Listas.Mapa[Índice].NPC[i].Y = Binário.ReadByte();
                global::NPC.Aparecer(i, Índice);
            }

        // Itens
        Listas.Mapa[Índice].Temp_Item = new System.Collections.Generic.List<Listas.Estruturas.Mapa_Itens>();
        Listas.Mapa[Índice].Temp_Item.Add(new Listas.Estruturas.Mapa_Itens()); // Nulo
        global::Map.Aparecer_Itens(Índice);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Mapa_Azulejos(short Índice, BinaryReader Binário)
    {
        byte Num_Camadas = Binário.ReadByte();

        // Redimensiona os dados
        Listas.Mapa[Índice].Azulejo = new Listas.Estruturas.Azulejo[Listas.Mapa[Índice].Largura + 1, Listas.Mapa[Índice].Altura + 1];
        for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
                Listas.Mapa[Índice].Azulejo[x, y].Dados = new Listas.Estruturas.Azulejo_Dados[(byte)global::Map.Camadas.Quantidade, Num_Camadas + 1];

        // Lê os azulejos
        for (byte i = 0; i <= Num_Camadas; i++)
        {
            // Dados básicos
            Binário.ReadString(); // Nome
            byte t = Binário.ReadByte(); // Tipo

            // Azulejos
            for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
                for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
                {
                    Listas.Mapa[Índice].Azulejo[x, y].Dados[t, i].x = Binário.ReadByte();
                    Listas.Mapa[Índice].Azulejo[x, y].Dados[t, i].y = Binário.ReadByte();
                    Listas.Mapa[Índice].Azulejo[x, y].Dados[t, i].Azulejo = Binário.ReadByte();
                    Listas.Mapa[Índice].Azulejo[x, y].Dados[t, i].Automático = Binário.ReadBoolean();
                }
        }
    }
}