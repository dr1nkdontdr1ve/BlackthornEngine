using System;
using System.Drawing;

class Map
{
    // Limitações dos Maps
    public const byte Min_Width = 24;
    public const byte Min_Height = 18;

    // Smokes
    public static int Smoke_X;
    public static int Smoke_Y;
    private static int SmokeX_Time = 0;
    private static int SmokeY_Time = 0;

    // Climate
    public const byte Max_Chuva_Particles = 100;
    public const short Max_Neve_Particles = 635;
    public const byte Max_Climate_Intensity = 10;
    public const byte Neve_Movement = 10;
    public static byte Lightning;
    private static int Neve_Time = 0;
    private static int Lightning_Time = 0;

    ////////////////
    // Numerações //
    ////////////////
    public enum Layers
    {
        Chão,
        Telhado,
        Amount
    }

    public enum Tile_Attributes
    {
        Nenhum,
        Block,
        Teletransporte,
        Amount
    }

    public enum Climates
    {
        Normal,
        Raining,
        Trovoando,
        Nevando,
        Amount
    }

    public enum Morais
    {
        Pacífico,
        Perigoso,
        Amount
    }

    public static void Logic()
    {
        // Toda a Logic do Map
        Smoke();
        Climate();
    }

    public static void NextTile(Game.Location Direction, ref short X, ref short Y)
    {
        // Next Tile
        switch (Direction)
        {
            case Game.Location.Above: Y -= 1; break;
            case Game.Location.Below: Y += 1; break;
            case Game.Location.Right: X += 1; break;
            case Game.Location.Left: X -= 1; break;
        }
    }

    public static bool ForaDoLimite(short x, short y)
    {
        // Verifica se as coordenas estão no limite do Map
        if (x > Lists.Map.Width || y > Lists.Map.Height || x < 0 || y < 0)
            return true;
        else
            return false;
    }

    public static bool Tile_Blocked(short Map, byte X, byte Y, Game.Location Direction)
    {
        short Next_X = X, Next_Y = Y;

        // Next Tile
        NextTile(Direction, ref Next_X, ref Next_Y);

        // Verifica se está indo para uma ligação
        if (ForaDoLimite(Next_X, Next_Y))
            if (Lists.Map.Ligação[(byte)Direction] == 0)
                return true;
            else
                return false;

        // Verifica se o Tile está Blocked
        if (Lists.Map.Tile[Next_X, Next_Y].Attribute == (byte)Tile_Attributes.Block)
            return true;
        else if (Lists.Map.Tile[Next_X, Next_Y].Block[(byte)Game.DirectionInverse(Direction)])
            return true;
        else if (Lists.Map.Tile[X, Y].Block[(byte)Direction])
            return true;
        else if (ThereIsPlayer(Map, Next_X, Next_Y) > 0 || ThereIsNPC(Next_X, Next_Y) > 0)
            return true;

        return false;
    }

    public static byte ThereIsNPC(short X, short Y)
    {
        // Verifica se ThereIs algum npc na cordenada
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
                if (Lists.Map.Temp_NPC[i].X == X && Lists.Map.Temp_NPC[i].Y == Y)
                    return i;

        return 0;
    }

    public static byte ThereIsPlayer(short Num, short X, short Y)
    {
        // Verifica se ThereIs algum Player na cordenada
        for (byte i = 1; i <= Player.BiggerIndex; i++)
            if (Lists.Player[i].X == X && Lists.Player[i].Y == Y && Lists.Player[i].Map == Num)
                return i;

        return 0;
    }

    private static void Smoke()
    {
        // Faz a Movement
        if (Player.IsPlaying(Player.MyIndex))
        {
            Calculate_Smoke_X();
            Calculate_Smoke_Y();
        }
    }

    private static void Calculate_Smoke_X()
    {
        Size Size = Graphics.MySize(Graphics.Tex_Smoke[Lists.Map.Smoke.Texture]);
        int VelocityX = Lists.Map.Smoke.VelocityX;

        // Apenas se necessário
        if (SmokeX_Time >= Environment.TickCount) return;
        if (VelocityX == 0) return;

        // Movement para trás
        if (VelocityX < 0)
        {
            Smoke_X -= 1;
            if (Smoke_X < -Size.Width) Smoke_X = 0;
        }
        // Movement para frente
        else
        {
            Smoke_X += 1;
            if (Smoke_X > Size.Width) Smoke_X = 0;
        }

        // Contagem
        if (VelocityX < 0) VelocityX *= -1;
        SmokeX_Time = Environment.TickCount + 50 - VelocityX;
    }

    private static void Calculate_Smoke_Y()
    {
        Size Size = Graphics.MySize(Graphics.Tex_Smoke[Lists.Map.Smoke.Texture]);
        int VelocityY = Lists.Map.Smoke.VelocityY;

        // Apenas se necessário
        if (SmokeY_Time >= Environment.TickCount) return;
        if (VelocityY == 0) return;

        // Movement para trás
        if (VelocityY < 0)
        {
            Smoke_Y -= 1;
            if (Smoke_Y < -Size.Height) Smoke_Y = 0;
        }
        // Movement para frente
        else
        {
            Smoke_Y += 1;
            if (Smoke_Y > Size.Height) Smoke_Y = 0;
        }

        // Contagem
        if (VelocityY < 0) VelocityY *= -1;
        SmokeY_Time = Environment.TickCount + 50 - VelocityY;
    }

    private static void Climate()
    {
        bool Stop = false, Moving = true;
        byte Primerio_Trovão = (byte)Audio.Sons.Trovão_1;
        byte Último_Trovão = (byte)Audio.Sons.Trovão_4;

        // Somente se necessário
        if (!Player.IsPlaying(Player.MyIndex)) return;
        if (Lists.Map.Climate.Type == 0) return;

        // Contagem da neve
        if (Neve_Time < Environment.TickCount)
        {
            Moving = true;
            Neve_Time = Environment.TickCount + 35;
        }
        else
            Moving = false;

        // Contagem dos Lightnings
        if (Lightning > 0)
            if (Lightning_Time < Environment.TickCount)
            {
                Lightning -= 10;
                Lightning_Time = Environment.TickCount + 25;
            }

        // Adiciona uma nova partícula
        for (short i = 1; i <= Lists.Climate_Particles.GetUpperBound(0); i++)
            if (!Lists.Climate_Particles[i].Visible)
            {
                if (Game.Aleatório.Next(0, Max_Climate_Intensity - Lists.Map.Climate.Intensity) == 0)
                {
                    if (!Stop)
                    {
                        // Cria a partícula
                        Lists.Climate_Particles[i].Visible = true;

                        // Cria a partícula de acordo com o seu Type
                        switch ((Climates)Lists.Map.Climate.Type)
                        {
                            case Climates.Trovoando:
                            case Climates.Raining: Climate_Chuva_Criação(i); break;
                            case Climates.Nevando: Climate_Neve_Criação(i); break;
                        }
                    }
                }

                Stop = true;
            }
            else
            {
                // Movimenta a partícula de acordo com o seu Type
                switch ((Climates)Lists.Map.Climate.Type)
                {
                    case Climates.Trovoando:
                    case Climates.Raining: Climate_Chuva_Movement(i); break;
                    case Climates.Nevando: Climate_Neve_Movement(i, Moving); break;
                }

                // Reseta a partícula
                if (Lists.Climate_Particles[i].x > Game.Screen_Width || Lists.Climate_Particles[i].y > Game.Screen_Height)
                    Lists.Climate_Particles[i] = new Lists.Structures.Climate();
            }

        // Trovoadas
        if (Lists.Map.Climate.Type == (byte)Climates.Trovoando)
            if (Game.Aleatório.Next(0, Max_Climate_Intensity * 10 - Lists.Map.Climate.Intensity * 2) == 0)
            {
                // Som do trovão
                int Trovão = Game.Aleatório.Next(Primerio_Trovão, Último_Trovão);
                Audio.Som.Reproduce((Audio.Sons)Trovão);

                // Lightning
                if (Trovão < 6) Lightning = 190;
            }
    }

    private static void Climate_Chuva_Criação(int i)
    {
        // Define a Velocity e a Position da partícula
        Lists.Climate_Particles[i].Velocity = Game.Aleatório.Next(8, 13);

        if (Game.Aleatório.Next(2) == 0)
        {
            Lists.Climate_Particles[i].x = -32;
            Lists.Climate_Particles[i].y = Game.Aleatório.Next(-32, Game.Screen_Height);
        }
        else
        {
            Lists.Climate_Particles[i].x = Game.Aleatório.Next(-32, Game.Screen_Width);
            Lists.Climate_Particles[i].y = -32;
        }
    }

    private static void Climate_Chuva_Movement(int i)
    {
        // Movimenta a partícula
        Lists.Climate_Particles[i].x += Lists.Climate_Particles[i].Velocity;
        Lists.Climate_Particles[i].y += Lists.Climate_Particles[i].Velocity;
    }

    private static void Climate_Neve_Criação(int i)
    {
        // Define a Velocity e a Position da partícula
        Lists.Climate_Particles[i].Velocity = Game.Aleatório.Next(1, 3);
        Lists.Climate_Particles[i].y = -32;
        Lists.Climate_Particles[i].x = Game.Aleatório.Next(-32, Game.Screen_Width);
        Lists.Climate_Particles[i].Inicío = Lists.Climate_Particles[i].x;

        if (Game.Aleatório.Next(2) == 0)
            Lists.Climate_Particles[i].Voltar = false;
        else
            Lists.Climate_Particles[i].Voltar = true;
    }

    private static void Climate_Neve_Movement(int i, bool Movimentrar = true)
    {
        int Diferença = Game.Aleatório.Next(0, Neve_Movement / 3);
        int x1 = Lists.Climate_Particles[i].Inicío + Neve_Movement + Diferença;
        int x2 = Lists.Climate_Particles[i].Inicío - Neve_Movement - Diferença;

        // Faz com que a partícula volte
        if (x1 <= Lists.Climate_Particles[i].x)
            Lists.Climate_Particles[i].Voltar = true;
        else if (x2 >= Lists.Climate_Particles[i].x)
            Lists.Climate_Particles[i].Voltar = false;

        // Movimenta a partícula
        Lists.Climate_Particles[i].y += Lists.Climate_Particles[i].Velocity;

        if (Movimentrar)
            if (Lists.Climate_Particles[i].Voltar)
                Lists.Climate_Particles[i].x -= 1;
            else
                Lists.Climate_Particles[i].x += 1;
    }

    public static void Climate_Ajustar()
    {
        // Para todos os sons
        Audio.Som.Stop_All();

        // Redimensiona a List
        switch ((Climates)Lists.Map.Climate.Type)
        {
            case Climates.Trovoando:
            case Climates.Raining:
                // Reproduz o som chuva
                Audio.Som.Reproduce(Audio.Sons.Chuva, true);

                // Redimensiona a Structure
                Lists.Climate_Particles = new Lists.Structures.Climate[Max_Chuva_Particles + 1];
                break;
            case Climates.Nevando: Lists.Climate_Particles = new Lists.Structures.Climate[Max_Neve_Particles + 1]; break;
        }
    }

    //////////////
    // Autotile //
    //////////////
    public class AutoCriação
    {
        // Formas de Add o mini Tile
        public enum Adição
        {
            Nenhum,
            Interior,
            Exterior,
            Horizontal,
            Vertical,
            Preencher
        }

        public static void Update()
        {
            // Atualiza os Tiles necessários
            for (byte x = 0; x <= Lists.Map.Width; x++)
                for (byte y = 0; y <= Lists.Map.Height; y++)
                    for (byte c = 0; c <= (byte)Layers.Amount - 1; c++)
                        for (byte q = 0; q <= Lists.Map.Tile[x, y].Data.GetUpperBound(1); q++)
                            if (Lists.Map.Tile[x, y].Data[c, q].Automático)
                                // Faz os cálculos para a autocriação
                                Calcular(x, y, c, q);
        }

        public static void Update(int x, int y, byte Camada_Num, byte Camada_Type)
        {
            // Atualiza os Tiles necessários
            for (int x2 = x - 2; x2 <= x + 2; x2++)
                for (int y2 = y - 2; y2 <= y + 2; y2++)
                    if (x2 >= 0 && x2 <= Lists.Map.Width && y2 >= 0 && y2 <= Lists.Map.Height)
                        // Faz os cálculos para a autocriação
                        Calcular((byte)x2, (byte)y2, Camada_Num, Camada_Type);
        }

        public static void Definir(byte x, byte y, byte Camada_Num, byte Camada_Type, byte Parte, string Letra)
        {
            Point Position = new Point(0);

            // Posições exatas dos mini Tiles (16x16)
            switch (Letra)
            {
                // Quinas
                case "a": Position = new Point(32, 0); break;
                case "b": Position = new Point(48, 0); break;
                case "c": Position = new Point(32, 16); break;
                case "d": Position = new Point(48, 16); break;

                // Noroeste
                case "e": Position = new Point(0, 32); break;
                case "f": Position = new Point(16, 32); break;
                case "g": Position = new Point(0, 48); break;
                case "h": Position = new Point(16, 48); break;

                // Nordeste
                case "i": Position = new Point(32, 32); break;
                case "j": Position = new Point(48, 32); break;
                case "k": Position = new Point(32, 48); break;
                case "l": Position = new Point(48, 48); break;

                // Sudoeste
                case "m": Position = new Point(0, 64); break;
                case "n": Position = new Point(16, 64); break;
                case "o": Position = new Point(0, 80); break;
                case "p": Position = new Point(16, 80); break;

                // Sudeste
                case "q": Position = new Point(32, 64); break;
                case "r": Position = new Point(48, 64); break;
                case "s": Position = new Point(32, 80); break;
                case "t": Position = new Point(48, 80); break;
            }

            // Define a Position do mini Tile
            Lists.Structures.Tile_Data Data = Lists.Map.Tile[x, y].Data[Camada_Type, Camada_Num];
            Lists.Map.Tile[x, y].Data[Camada_Type, Camada_Num].Mini[Parte].X = Data.x * Game.Grade + Position.X;
            Lists.Map.Tile[x, y].Data[Camada_Type, Camada_Num].Mini[Parte].Y = Data.y * Game.Grade + Position.Y;
        }

        public static bool Check(int X1, int Y1, int X2, int Y2, byte Camada_Num, byte Camada_Type)
        {
            Lists.Structures.Tile_Data Data1, Data2;

            // Somente se necessário
            if (X2 < 0 || X2 > Lists.Map.Width || Y2 < 0 || Y2 > Lists.Map.Height) return true;

            // Data
            Data1 = Lists.Map.Tile[X1, Y1].Data[Camada_Type, Camada_Num];
            Data2 = Lists.Map.Tile[X2, Y2].Data[Camada_Type, Camada_Num];

            // Verifica se são os mesmo Tiles
            if (!Data2.Automático) return false;
            if (Data1.Tile != Data2.Tile) return false;
            if (Data1.x != Data2.x) return false;
            if (Data1.y != Data2.y) return false;

            // Não ThereIs nada de errado
            return true;
        }

        public static void Calcular(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            // Calcula as quatros partes do Tile
            Calcular_NO(x, y, Camada_Num, Camada_Type);
            Calcular_NE(x, y, Camada_Num, Camada_Type);
            Calcular_SO(x, y, Camada_Num, Camada_Type);
            Calcular_SE(x, y, Camada_Num, Camada_Type);
        }

        public static void Calcular_NO(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            bool[] Tile = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos Tiles em volta (Norte, Oeste, Noroeste)
            if (Check(x, y, x - 1, y - 1, Camada_Num, Camada_Type)) Tile[1] = true;
            if (Check(x, y, x, y - 1, Camada_Num, Camada_Type)) Tile[2] = true;
            if (Check(x, y, x - 1, y, Camada_Num, Camada_Type)) Tile[3] = true;

            // Forma que será adicionado o mini Tile
            if (!Tile[2] && !Tile[3]) Forma = Adição.Interior;
            if (!Tile[2] && Tile[3]) Forma = Adição.Horizontal;
            if (Tile[2] && !Tile[3]) Forma = Adição.Vertical;
            if (!Tile[1] && Tile[2] && Tile[3]) Forma = Adição.Exterior;
            if (Tile[1] && Tile[2] && Tile[3]) Forma = Adição.Preencher;

            // Define o mini Tile
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Type, 0, "e"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Type, 0, "a"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Type, 0, "i"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Type, 0, "m"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Type, 0, "q"); break;
            }
        }

        public static void Calcular_NE(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            bool[] Tile = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos Tiles em volta (Norte, Oeste, Noroeste)
            if (Check(x, y, x, y - 1, Camada_Num, Camada_Type)) Tile[1] = true;
            if (Check(x, y, x + 1, y - 1, Camada_Num, Camada_Type)) Tile[2] = true;
            if (Check(x, y, x + 1, y, Camada_Num, Camada_Type)) Tile[3] = true;

            // Forma que será adicionado o mini Tile
            if (!Tile[1] && !Tile[3]) Forma = Adição.Interior;
            if (!Tile[1] && Tile[3]) Forma = Adição.Horizontal;
            if (Tile[1] && !Tile[3]) Forma = Adição.Vertical;
            if (Tile[1] && !Tile[2] && Tile[3]) Forma = Adição.Exterior;
            if (Tile[1] && Tile[2] && Tile[3]) Forma = Adição.Preencher;

            // Define o mini Tile
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Type, 1, "j"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Type, 1, "b"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Type, 1, "f"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Type, 1, "r"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Type, 1, "n"); break;
            }
        }

        public static void Calcular_SO(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            bool[] Tile = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos Tiles em volta (Sul, Oeste, Sudoeste)
            if (Check(x, y, x - 1, y, Camada_Num, Camada_Type)) Tile[1] = true;
            if (Check(x, y, x - 1, y + 1, Camada_Num, Camada_Type)) Tile[2] = true;
            if (Check(x, y, x, y + 1, Camada_Num, Camada_Type)) Tile[3] = true;

            // Forma que será adicionado o mini Tile
            if (!Tile[1] && !Tile[3]) Forma = Adição.Interior;
            if (Tile[1] && !Tile[3]) Forma = Adição.Horizontal;
            if (!Tile[1] && Tile[3]) Forma = Adição.Vertical;
            if (Tile[1] && !Tile[2] && Tile[3]) Forma = Adição.Exterior;
            if (Tile[1] && Tile[2] && Tile[3]) Forma = Adição.Preencher;

            // Define o mini Tile
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Type, 2, "o"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Type, 2, "c"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Type, 2, "s"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Type, 2, "g"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Type, 2, "k"); break;
            }
        }

        public static void Calcular_SE(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            bool[] Tile = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos Tiles em volta (Sul, Oeste, Sudeste)
            if (Check(x, y, x, y + 1, Camada_Num, Camada_Type)) Tile[1] = true;
            if (Check(x, y, x + 1, y + 1, Camada_Num, Camada_Type)) Tile[2] = true;
            if (Check(x, y, x + 1, y, Camada_Num, Camada_Type)) Tile[3] = true;

            // Forma que será adicionado o mini Tile
            if (!Tile[1] && !Tile[3]) Forma = Adição.Interior;
            if (!Tile[1] && Tile[3]) Forma = Adição.Horizontal;
            if (Tile[1] && !Tile[3]) Forma = Adição.Vertical;
            if (Tile[1] && !Tile[2] && Tile[3]) Forma = Adição.Exterior;
            if (Tile[1] && Tile[2] && Tile[3]) Forma = Adição.Preencher;

            // Define o mini Tile
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Type, 3, "t"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Type, 3, "d"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Type, 3, "p"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Type, 3, "l"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Type, 3, "h"); break;
            }
        }
    }
}

partial class Graphics
{
    public static void Map_Tiles(byte c)
    {
        // Previni erros
        if (Lists.Map.Name == null) return;

        // Data
        System.Drawing.Color TempCor = System.Drawing.Color.FromArgb(Lists.Map.Coloração);
        SFML.Graphics.Color Cor = CCor(TempCor.R, TempCor.G, TempCor.B);

        // Desenha todas as Layers dos Tiles
        for (short x = (short)Game.Tiles_View.X; x <= Game.Tiles_View.Width; x++)
            for (short y = (short)Game.Tiles_View.Y; y <= Game.Tiles_View.Height; y++)
                if (!Map.ForaDoLimite(x, y))
                    for (byte q = 0; q <= Lists.Map.Tile[x, y].Data.GetUpperBound(1); q++)
                        if (Lists.Map.Tile[x, y].Data[c, q].Tile > 0)
                        {
                            int x2 = Lists.Map.Tile[x, y].Data[c, q].x * Game.Grade;
                            int y2 = Lists.Map.Tile[x, y].Data[c, q].y * Game.Grade;

                            // Desenha o Tile
                            if (!Lists.Map.Tile[x, y].Data[c, q].Automático)
                                Desenhar(Tex_Tile[Lists.Map.Tile[x, y].Data[c, q].Tile], Game.ConvertX(x * Game.Grade), Game.ConvertY(y * Game.Grade), x2, y2, Game.Grade, Game.Grade, Cor);
                            else
                                Maps_AutoCriação(new Point(Game.ConvertX(x * Game.Grade), Game.ConvertY(y * Game.Grade)), Lists.Map.Tile[x, y].Data[c, q], Cor);
                        }
    }

    public static void Maps_AutoCriação(Point Position, Lists.Structures.Tile_Data Data, SFML.Graphics.Color Cor)
    {
        // Desenha os 4 mini Tiles
        for (byte i = 0; i <= 3; i++)
        {
            Point Destino = Position, Fonte = Data.Mini[i];

            // Partes do Tile
            switch (i)
            {
                case 1: Destino.X += 16; break;
                case 2: Destino.Y += 16; break;
                case 3: Destino.X += 16; Destino.Y += 16; break;
            }

            // Renderiza o mini Tile
            Desenhar(Tex_Tile[Data.Tile], new Rectangle(Fonte.X, Fonte.Y, 16, 16), new Rectangle(Destino, new Size(16, 16)), Cor);
        }
    }

    public static void Map_Panorama()
    {
        // Desenha o panorama
        if (Lists.Map.Panorama > 0)
            Desenhar(Tex_Panorama[Lists.Map.Panorama], new Point(0));
    }

    public static void Map_Smoke()
    {
        Lists.Structures.Map_Smoke Data = Lists.Map.Smoke;
        Size Texture_Size = MySize(Tex_Smoke[Data.Texture]);

        // Previni erros
        if (Data.Texture <= 0) return;

        // Desenha a Smoke
        for (int x = -1; x <= Lists.Map.Width * Game.Grade / Texture_Size.Width + 1; x++)
            for (int y = -1; y <= Lists.Map.Height * Game.Grade / Texture_Size.Height + 1; y++)
                Desenhar(Tex_Smoke[Data.Texture], new Point(x * Texture_Size.Width + Map.Smoke_X, y * Texture_Size.Height + Map.Smoke_Y), new SFML.Graphics.Color(255, 255, 255, Data.Transparency));
    }

    public static void Map_Climate()
    {
        byte x = 0;

        // Somente se necessário
        if (Lists.Map.Climate.Type == 0) return;

        // Texture
        switch ((Map.Climates)Lists.Map.Climate.Type)
        {
            case Map.Climates.Nevando: x = 32; break;
        }

        // Desenha as Particles
        for (int i = 1; i <= Lists.Climate_Particles.GetUpperBound(0); i++)
            if (Lists.Climate_Particles[i].Visible)
                Desenhar(Tex_Climate, new Rectangle(x, 0, 32, 32), new Rectangle(Lists.Climate_Particles[i].x, Lists.Climate_Particles[i].y, 32, 32), CCor(255, 255, 255, 150));

        // Trovoadas
        Desenhar(Tex_Preenchido, 0, 0, 0, 0, Game.Screen_Width, Game.Screen_Height, new SFML.Graphics.Color(255, 255, 255, Map.Lightning));
    }

    public static void Map_Name()
    {
        SFML.Graphics.Color Cor;

        // Somente se necessário
        if (string.IsNullOrEmpty(Lists.Map.Name)) return;

        // A cor do Text vária de acordo com a moral do Map
        switch (Lists.Map.Moral)
        {
            case (byte)Map.Morais.Perigoso: Cor = SFML.Graphics.Color.Red; break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o Name do Map
        Desenhar(Lists.Map.Name, 463, 48, Cor);
    }

    public static void Map_Items()
    {
        // Desenha todos osItems que estão no chão
        for (byte i = 1; i <= Lists.Map.Temp_Item.GetUpperBound(0); i++)
        {
            Lists.Structures.Map_Items Data = Lists.Map.Temp_Item[i];

            // Somente se necessário
            if (Data.Index == 0) continue;

            // Desenha o item
            Point Position = new Point(Game.ConvertX(Data.X * Game.Grade), Game.ConvertY(Data.Y * Game.Grade));
            Desenhar(Tex_Item[Lists.Item[Data.Index].Texture], Position);
        }
    }
}