using System;
using System.Drawing;

class Map
{
    // Limitações dos Maps
    public const byte Min_Largura = 24;
    public const byte Min_Altura = 18;

    // Fumaças
    public static int Fumaça_X;
    public static int Fumaça_Y;
    private static int FumaçaX_Tempo = 0;
    private static int FumaçaY_Tempo = 0;

    // Clima
    public const byte Max_Chuva_Partículas = 100;
    public const short Max_Neve_Partículas = 635;
    public const byte Max_Clima_Intensidade = 10;
    public const byte Neve_Movimento = 10;
    public static byte Relâmpago;
    private static int Neve_Tempo = 0;
    private static int Relâmpago_Tempo = 0;

    ////////////////
    // Numerações //
    ////////////////
    public enum Camadas
    {
        Chão,
        Telhado,
        Amount
    }

    public enum Azulejo_Atributos
    {
        Nenhum,
        Bloqueio,
        Teletransporte,
        Amount
    }

    public enum Climas
    {
        Normal,
        Chovendo,
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

    public static void Lógica()
    {
        // Toda a lógica do Map
        Fumaça();
        Clima();
    }

    public static void PróximoAzulejo(Game.Direções Direction, ref short X, ref short Y)
    {
        // Próximo azulejo
        switch (Direction)
        {
            case Game.Direções.Acima: Y -= 1; break;
            case Game.Direções.Abaixo: Y += 1; break;
            case Game.Direções.Direita: X += 1; break;
            case Game.Direções.Esquerda: X -= 1; break;
        }
    }

    public static bool ForaDoLimite(short x, short y)
    {
        // Verifica se as coordenas estão no limite do Map
        if (x > Lists.Map.Largura || y > Lists.Map.Altura || x < 0 || y < 0)
            return true;
        else
            return false;
    }

    public static bool Azulejo_Bloqueado(short Map, byte X, byte Y, Game.Direções Direction)
    {
        short Próximo_X = X, Próximo_Y = Y;

        // Próximo azulejo
        PróximoAzulejo(Direction, ref Próximo_X, ref Próximo_Y);

        // Verifica se está indo para uma ligação
        if (ForaDoLimite(Próximo_X, Próximo_Y))
            if (Lists.Map.Ligação[(byte)Direction] == 0)
                return true;
            else
                return false;

        // Verifica se o azulejo está bloqueado
        if (Lists.Map.Azulejo[Próximo_X, Próximo_Y].Atributo == (byte)Azulejo_Atributos.Bloqueio)
            return true;
        else if (Lists.Map.Azulejo[Próximo_X, Próximo_Y].Bloqueio[(byte)Game.DirectionInversa(Direction)])
            return true;
        else if (Lists.Map.Azulejo[X, Y].Bloqueio[(byte)Direction])
            return true;
        else if (HáPlayer(Map, Próximo_X, Próximo_Y) > 0 || HáNPC(Próximo_X, Próximo_Y) > 0)
            return true;

        return false;
    }

    public static byte HáNPC(short X, short Y)
    {
        // Verifica se há algum npc na cordenada
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
                if (Lists.Map.Temp_NPC[i].X == X && Lists.Map.Temp_NPC[i].Y == Y)
                    return i;

        return 0;
    }

    public static byte HáPlayer(short Num, short X, short Y)
    {
        // Verifica se há algum Player na cordenada
        for (byte i = 1; i <= Player.MaiorIndex; i++)
            if (Lists.Player[i].X == X && Lists.Player[i].Y == Y && Lists.Player[i].Map == Num)
                return i;

        return 0;
    }

    private static void Fumaça()
    {
        // Faz a movimentação
        if (Player.EstáJogando(Player.MyIndex))
        {
            Cálcular_Fumaça_X();
            Cálcular_Fumaça_Y();
        }
    }

    private static void Cálcular_Fumaça_X()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Lists.Map.Fumaça.Texture]);
        int VelocidadeX = Lists.Map.Fumaça.VelocidadeX;

        // Apenas se necessário
        if (FumaçaX_Tempo >= Environment.TickCount) return;
        if (VelocidadeX == 0) return;

        // Movimento para trás
        if (VelocidadeX < 0)
        {
            Fumaça_X -= 1;
            if (Fumaça_X < -Tamanho.Width) Fumaça_X = 0;
        }
        // Movimento para frente
        else
        {
            Fumaça_X += 1;
            if (Fumaça_X > Tamanho.Width) Fumaça_X = 0;
        }

        // Contagem
        if (VelocidadeX < 0) VelocidadeX *= -1;
        FumaçaX_Tempo = Environment.TickCount + 50 - VelocidadeX;
    }

    private static void Cálcular_Fumaça_Y()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Lists.Map.Fumaça.Texture]);
        int VelocidadeY = Lists.Map.Fumaça.VelocidadeY;

        // Apenas se necessário
        if (FumaçaY_Tempo >= Environment.TickCount) return;
        if (VelocidadeY == 0) return;

        // Movimento para trás
        if (VelocidadeY < 0)
        {
            Fumaça_Y -= 1;
            if (Fumaça_Y < -Tamanho.Height) Fumaça_Y = 0;
        }
        // Movimento para frente
        else
        {
            Fumaça_Y += 1;
            if (Fumaça_Y > Tamanho.Height) Fumaça_Y = 0;
        }

        // Contagem
        if (VelocidadeY < 0) VelocidadeY *= -1;
        FumaçaY_Tempo = Environment.TickCount + 50 - VelocidadeY;
    }

    private static void Clima()
    {
        bool Parar = false, Movimentar = true;
        byte Primerio_Trovão = (byte)Áudio.Sons.Trovão_1;
        byte Último_Trovão = (byte)Áudio.Sons.Trovão_4;

        // Somente se necessário
        if (!Player.EstáJogando(Player.MyIndex)) return;
        if (Lists.Map.Clima.Type == 0) return;

        // Contagem da neve
        if (Neve_Tempo < Environment.TickCount)
        {
            Movimentar = true;
            Neve_Tempo = Environment.TickCount + 35;
        }
        else
            Movimentar = false;

        // Contagem dos relâmpagos
        if (Relâmpago > 0)
            if (Relâmpago_Tempo < Environment.TickCount)
            {
                Relâmpago -= 10;
                Relâmpago_Tempo = Environment.TickCount + 25;
            }

        // Adiciona uma nova partícula
        for (short i = 1; i <= Lists.Clima_Partículas.GetUpperBound(0); i++)
            if (!Lists.Clima_Partículas[i].Visível)
            {
                if (Game.Aleatório.Next(0, Max_Clima_Intensidade - Lists.Map.Clima.Intensidade) == 0)
                {
                    if (!Parar)
                    {
                        // Cria a partícula
                        Lists.Clima_Partículas[i].Visível = true;

                        // Cria a partícula de acordo com o seu Type
                        switch ((Climas)Lists.Map.Clima.Type)
                        {
                            case Climas.Trovoando:
                            case Climas.Chovendo: Clima_Chuva_Criação(i); break;
                            case Climas.Nevando: Clima_Neve_Criação(i); break;
                        }
                    }
                }

                Parar = true;
            }
            else
            {
                // Movimenta a partícula de acordo com o seu Type
                switch ((Climas)Lists.Map.Clima.Type)
                {
                    case Climas.Trovoando:
                    case Climas.Chovendo: Clima_Chuva_Movimentação(i); break;
                    case Climas.Nevando: Clima_Neve_Movimentação(i, Movimentar); break;
                }

                // Reseta a partícula
                if (Lists.Clima_Partículas[i].x > Game.Tela_Largura || Lists.Clima_Partículas[i].y > Game.Tela_Altura)
                    Lists.Clima_Partículas[i] = new Lists.Structures.Clima();
            }

        // Trovoadas
        if (Lists.Map.Clima.Type == (byte)Climas.Trovoando)
            if (Game.Aleatório.Next(0, Max_Clima_Intensidade * 10 - Lists.Map.Clima.Intensidade * 2) == 0)
            {
                // Som do trovão
                int Trovão = Game.Aleatório.Next(Primerio_Trovão, Último_Trovão);
                Áudio.Som.Reproduzir((Áudio.Sons)Trovão);

                // Relâmpago
                if (Trovão < 6) Relâmpago = 190;
            }
    }

    private static void Clima_Chuva_Criação(int i)
    {
        // Define a velocidade e a posição da partícula
        Lists.Clima_Partículas[i].Velocidade = Game.Aleatório.Next(8, 13);

        if (Game.Aleatório.Next(2) == 0)
        {
            Lists.Clima_Partículas[i].x = -32;
            Lists.Clima_Partículas[i].y = Game.Aleatório.Next(-32, Game.Tela_Altura);
        }
        else
        {
            Lists.Clima_Partículas[i].x = Game.Aleatório.Next(-32, Game.Tela_Largura);
            Lists.Clima_Partículas[i].y = -32;
        }
    }

    private static void Clima_Chuva_Movimentação(int i)
    {
        // Movimenta a partícula
        Lists.Clima_Partículas[i].x += Lists.Clima_Partículas[i].Velocidade;
        Lists.Clima_Partículas[i].y += Lists.Clima_Partículas[i].Velocidade;
    }

    private static void Clima_Neve_Criação(int i)
    {
        // Define a velocidade e a posição da partícula
        Lists.Clima_Partículas[i].Velocidade = Game.Aleatório.Next(1, 3);
        Lists.Clima_Partículas[i].y = -32;
        Lists.Clima_Partículas[i].x = Game.Aleatório.Next(-32, Game.Tela_Largura);
        Lists.Clima_Partículas[i].Inicío = Lists.Clima_Partículas[i].x;

        if (Game.Aleatório.Next(2) == 0)
            Lists.Clima_Partículas[i].Voltar = false;
        else
            Lists.Clima_Partículas[i].Voltar = true;
    }

    private static void Clima_Neve_Movimentação(int i, bool Movimentrar = true)
    {
        int Diferença = Game.Aleatório.Next(0, Neve_Movimento / 3);
        int x1 = Lists.Clima_Partículas[i].Inicío + Neve_Movimento + Diferença;
        int x2 = Lists.Clima_Partículas[i].Inicío - Neve_Movimento - Diferença;

        // Faz com que a partícula volte
        if (x1 <= Lists.Clima_Partículas[i].x)
            Lists.Clima_Partículas[i].Voltar = true;
        else if (x2 >= Lists.Clima_Partículas[i].x)
            Lists.Clima_Partículas[i].Voltar = false;

        // Movimenta a partícula
        Lists.Clima_Partículas[i].y += Lists.Clima_Partículas[i].Velocidade;

        if (Movimentrar)
            if (Lists.Clima_Partículas[i].Voltar)
                Lists.Clima_Partículas[i].x -= 1;
            else
                Lists.Clima_Partículas[i].x += 1;
    }

    public static void Clima_Ajustar()
    {
        // Para todos os sons
        Áudio.Som.Parar_Tudo();

        // Redimensiona a lista
        switch ((Climas)Lists.Map.Clima.Type)
        {
            case Climas.Trovoando:
            case Climas.Chovendo:
                // Reproduz o som chuva
                Áudio.Som.Reproduzir(Áudio.Sons.Chuva, true);

                // Redimensiona a estrutura
                Lists.Clima_Partículas = new Lists.Structures.Clima[Max_Chuva_Partículas + 1];
                break;
            case Climas.Nevando: Lists.Clima_Partículas = new Lists.Structures.Clima[Max_Neve_Partículas + 1]; break;
        }
    }

    //////////////
    // Autotile //
    //////////////
    public class AutoCriação
    {
        // Formas de adicionar o mini azulejo
        public enum Adição
        {
            Nenhum,
            Interior,
            Exterior,
            Horizontal,
            Vertical,
            Preencher
        }

        public static void Atualizar()
        {
            // Atualiza os azulejos necessários
            for (byte x = 0; x <= Lists.Map.Largura; x++)
                for (byte y = 0; y <= Lists.Map.Altura; y++)
                    for (byte c = 0; c <= (byte)Camadas.Amount - 1; c++)
                        for (byte q = 0; q <= Lists.Map.Azulejo[x, y].Data.GetUpperBound(1); q++)
                            if (Lists.Map.Azulejo[x, y].Data[c, q].Automático)
                                // Faz os cálculos para a autocriação
                                Calcular(x, y, c, q);
        }

        public static void Atualizar(int x, int y, byte Camada_Num, byte Camada_Type)
        {
            // Atualiza os azulejos necessários
            for (int x2 = x - 2; x2 <= x + 2; x2++)
                for (int y2 = y - 2; y2 <= y + 2; y2++)
                    if (x2 >= 0 && x2 <= Lists.Map.Largura && y2 >= 0 && y2 <= Lists.Map.Altura)
                        // Faz os cálculos para a autocriação
                        Calcular((byte)x2, (byte)y2, Camada_Num, Camada_Type);
        }

        public static void Definir(byte x, byte y, byte Camada_Num, byte Camada_Type, byte Parte, string Letra)
        {
            Point Posição = new Point(0);

            // Posições exatas dos mini azulejos (16x16)
            switch (Letra)
            {
                // Quinas
                case "a": Posição = new Point(32, 0); break;
                case "b": Posição = new Point(48, 0); break;
                case "c": Posição = new Point(32, 16); break;
                case "d": Posição = new Point(48, 16); break;

                // Noroeste
                case "e": Posição = new Point(0, 32); break;
                case "f": Posição = new Point(16, 32); break;
                case "g": Posição = new Point(0, 48); break;
                case "h": Posição = new Point(16, 48); break;

                // Nordeste
                case "i": Posição = new Point(32, 32); break;
                case "j": Posição = new Point(48, 32); break;
                case "k": Posição = new Point(32, 48); break;
                case "l": Posição = new Point(48, 48); break;

                // Sudoeste
                case "m": Posição = new Point(0, 64); break;
                case "n": Posição = new Point(16, 64); break;
                case "o": Posição = new Point(0, 80); break;
                case "p": Posição = new Point(16, 80); break;

                // Sudeste
                case "q": Posição = new Point(32, 64); break;
                case "r": Posição = new Point(48, 64); break;
                case "s": Posição = new Point(32, 80); break;
                case "t": Posição = new Point(48, 80); break;
            }

            // Define a posição do mini azulejo
            Lists.Structures.Azulejo_Data Data = Lists.Map.Azulejo[x, y].Data[Camada_Type, Camada_Num];
            Lists.Map.Azulejo[x, y].Data[Camada_Type, Camada_Num].Mini[Parte].X = Data.x * Game.Grade + Posição.X;
            Lists.Map.Azulejo[x, y].Data[Camada_Type, Camada_Num].Mini[Parte].Y = Data.y * Game.Grade + Posição.Y;
        }

        public static bool Verificar(int X1, int Y1, int X2, int Y2, byte Camada_Num, byte Camada_Type)
        {
            Lists.Structures.Azulejo_Data Data1, Data2;

            // Somente se necessário
            if (X2 < 0 || X2 > Lists.Map.Largura || Y2 < 0 || Y2 > Lists.Map.Altura) return true;

            // Data
            Data1 = Lists.Map.Azulejo[X1, Y1].Data[Camada_Type, Camada_Num];
            Data2 = Lists.Map.Azulejo[X2, Y2].Data[Camada_Type, Camada_Num];

            // Verifica se são os mesmo azulejos
            if (!Data2.Automático) return false;
            if (Data1.Azulejo != Data2.Azulejo) return false;
            if (Data1.x != Data2.x) return false;
            if (Data1.y != Data2.y) return false;

            // Não há nada de errado
            return true;
        }

        public static void Calcular(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            // Calcula as quatros partes do azulejo
            Calcular_NO(x, y, Camada_Num, Camada_Type);
            Calcular_NE(x, y, Camada_Num, Camada_Type);
            Calcular_SO(x, y, Camada_Num, Camada_Type);
            Calcular_SE(x, y, Camada_Num, Camada_Type);
        }

        public static void Calcular_NO(byte x, byte y, byte Camada_Num, byte Camada_Type)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(x, y, x - 1, y - 1, Camada_Num, Camada_Type)) Azulejo[1] = true;
            if (Verificar(x, y, x, y - 1, Camada_Num, Camada_Type)) Azulejo[2] = true;
            if (Verificar(x, y, x - 1, y, Camada_Num, Camada_Type)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[2] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[2] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[2] && !Azulejo[3]) Forma = Adição.Vertical;
            if (!Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
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
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(x, y, x, y - 1, Camada_Num, Camada_Type)) Azulejo[1] = true;
            if (Verificar(x, y, x + 1, y - 1, Camada_Num, Camada_Type)) Azulejo[2] = true;
            if (Verificar(x, y, x + 1, y, Camada_Num, Camada_Type)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
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
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudoeste)
            if (Verificar(x, y, x - 1, y, Camada_Num, Camada_Type)) Azulejo[1] = true;
            if (Verificar(x, y, x - 1, y + 1, Camada_Num, Camada_Type)) Azulejo[2] = true;
            if (Verificar(x, y, x, y + 1, Camada_Num, Camada_Type)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Horizontal;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
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
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudeste)
            if (Verificar(x, y, x, y + 1, Camada_Num, Camada_Type)) Azulejo[1] = true;
            if (Verificar(x, y, x + 1, y + 1, Camada_Num, Camada_Type)) Azulejo[2] = true;
            if (Verificar(x, y, x + 1, y, Camada_Num, Camada_Type)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
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

partial class Gráficos
{
    public static void Map_Azulejos(byte c)
    {
        // Previni erros
        if (Lists.Map.Name == null) return;

        // Data
        System.Drawing.Color TempCor = System.Drawing.Color.FromArgb(Lists.Map.Coloração);
        SFML.Graphics.Color Cor = CCor(TempCor.R, TempCor.G, TempCor.B);

        // Desenha todas as camadas dos azulejos
        for (short x = (short)Game.Azulejos_Visão.X; x <= Game.Azulejos_Visão.Width; x++)
            for (short y = (short)Game.Azulejos_Visão.Y; y <= Game.Azulejos_Visão.Height; y++)
                if (!Map.ForaDoLimite(x, y))
                    for (byte q = 0; q <= Lists.Map.Azulejo[x, y].Data.GetUpperBound(1); q++)
                        if (Lists.Map.Azulejo[x, y].Data[c, q].Azulejo > 0)
                        {
                            int x2 = Lists.Map.Azulejo[x, y].Data[c, q].x * Game.Grade;
                            int y2 = Lists.Map.Azulejo[x, y].Data[c, q].y * Game.Grade;

                            // Desenha o azulejo
                            if (!Lists.Map.Azulejo[x, y].Data[c, q].Automático)
                                Desenhar(Tex_Azulejo[Lists.Map.Azulejo[x, y].Data[c, q].Azulejo], Game.ConverterX(x * Game.Grade), Game.ConverterY(y * Game.Grade), x2, y2, Game.Grade, Game.Grade, Cor);
                            else
                                Maps_AutoCriação(new Point(Game.ConverterX(x * Game.Grade), Game.ConverterY(y * Game.Grade)), Lists.Map.Azulejo[x, y].Data[c, q], Cor);
                        }
    }

    public static void Maps_AutoCriação(Point Posição, Lists.Structures.Azulejo_Data Data, SFML.Graphics.Color Cor)
    {
        // Desenha os 4 mini azulejos
        for (byte i = 0; i <= 3; i++)
        {
            Point Destino = Posição, Fonte = Data.Mini[i];

            // Partes do azulejo
            switch (i)
            {
                case 1: Destino.X += 16; break;
                case 2: Destino.Y += 16; break;
                case 3: Destino.X += 16; Destino.Y += 16; break;
            }

            // Renderiza o mini azulejo
            Desenhar(Tex_Azulejo[Data.Azulejo], new Rectangle(Fonte.X, Fonte.Y, 16, 16), new Rectangle(Destino, new Size(16, 16)), Cor);
        }
    }

    public static void Map_Panorama()
    {
        // Desenha o panorama
        if (Lists.Map.Panorama > 0)
            Desenhar(Tex_Panorama[Lists.Map.Panorama], new Point(0));
    }

    public static void Map_Fumaça()
    {
        Lists.Structures.Map_Fumaça Data = Lists.Map.Fumaça;
        Size Texture_Tamanho = TTamanho(Tex_Fumaça[Data.Texture]);

        // Previni erros
        if (Data.Texture <= 0) return;

        // Desenha a fumaça
        for (int x = -1; x <= Lists.Map.Largura * Game.Grade / Texture_Tamanho.Width + 1; x++)
            for (int y = -1; y <= Lists.Map.Altura * Game.Grade / Texture_Tamanho.Height + 1; y++)
                Desenhar(Tex_Fumaça[Data.Texture], new Point(x * Texture_Tamanho.Width + Map.Fumaça_X, y * Texture_Tamanho.Height + Map.Fumaça_Y), new SFML.Graphics.Color(255, 255, 255, Data.Transparência));
    }

    public static void Map_Clima()
    {
        byte x = 0;

        // Somente se necessário
        if (Lists.Map.Clima.Type == 0) return;

        // Texture
        switch ((Map.Climas)Lists.Map.Clima.Type)
        {
            case Map.Climas.Nevando: x = 32; break;
        }

        // Desenha as partículas
        for (int i = 1; i <= Lists.Clima_Partículas.GetUpperBound(0); i++)
            if (Lists.Clima_Partículas[i].Visível)
                Desenhar(Tex_Clima, new Rectangle(x, 0, 32, 32), new Rectangle(Lists.Clima_Partículas[i].x, Lists.Clima_Partículas[i].y, 32, 32), CCor(255, 255, 255, 150));

        // Trovoadas
        Desenhar(Tex_Preenchido, 0, 0, 0, 0, Game.Tela_Largura, Game.Tela_Altura, new SFML.Graphics.Color(255, 255, 255, Map.Relâmpago));
    }

    public static void Map_Name()
    {
        SFML.Graphics.Color Cor;

        // Somente se necessário
        if (string.IsNullOrEmpty(Lists.Map.Name)) return;

        // A cor do texto vária de acordo com a moral do Map
        switch (Lists.Map.Moral)
        {
            case (byte)Map.Morais.Perigoso: Cor = SFML.Graphics.Color.Red; break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o Name do Map
        Desenhar(Lists.Map.Name, 463, 48, Cor);
    }

    public static void Map_Itens()
    {
        // Desenha todos os itens que estão no chão
        for (byte i = 1; i <= Lists.Map.Temp_Item.GetUpperBound(0); i++)
        {
            Lists.Structures.Map_Itens Data = Lists.Map.Temp_Item[i];

            // Somente se necessário
            if (Data.Index == 0) continue;

            // Desenha o item
            Point Posição = new Point(Game.ConverterX(Data.X * Game.Grade), Game.ConverterY(Data.Y * Game.Grade));
            Desenhar(Tex_Item[Lists.Item[Data.Index].Texture], Posição);
        }
    }
}