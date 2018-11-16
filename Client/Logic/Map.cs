using System;
using System.Drawing;

class Map
{
    // Limitações dos mapas
    public const byte Min_Largura = 24;
    public const byte Min_Altura = 18;

    // Fumaças
    public static int Fumaça_X;
    public static int Fumaça_Y;
    private static int FumaçaX_Tempo = 0;
    private static int FumaçaY_Tempo = 0;

    // Clima
    public const byte Máx_Chuva_Partículas = 100;
    public const short Máx_Neve_Partículas = 635;
    public const byte Máx_Clima_Intensidade = 10;
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
        Quantidade
    }

    public enum Azulejo_Atributos
    {
        Nenhum,
        Bloqueio,
        Teletransporte,
        Quantidade
    }

    public enum Climas
    {
        Normal,
        Chovendo,
        Trovoando,
        Nevando,
        Quantidade
    }

    public enum Morais
    {
        Pacífico,
        Perigoso,
        Quantidade
    }

    public static void Lógica()
    {
        // Toda a lógica do mapa
        Fumaça();
        Clima();
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

    public static bool ForaDoLimite(short x, short y)
    {
        // Verifica se as coordenas estão no limite do mapa
        if (x > Listas.Mapa.Largura || y > Listas.Mapa.Altura || x < 0 || y < 0)
            return true;
        else
            return false;
    }

    public static bool Azulejo_Bloqueado(short Mapa, byte X, byte Y, Game.Direções Direção)
    {
        short Próximo_X = X, Próximo_Y = Y;

        // Próximo azulejo
        PróximoAzulejo(Direção, ref Próximo_X, ref Próximo_Y);

        // Verifica se está indo para uma ligação
        if (ForaDoLimite(Próximo_X, Próximo_Y))
            if (Listas.Mapa.Ligação[(byte)Direção] == 0)
                return true;
            else
                return false;

        // Verifica se o azulejo está bloqueado
        if (Listas.Mapa.Azulejo[Próximo_X, Próximo_Y].Atributo == (byte)Azulejo_Atributos.Bloqueio)
            return true;
        else if (Listas.Mapa.Azulejo[Próximo_X, Próximo_Y].Bloqueio[(byte)Game.DireçãoInversa(Direção)])
            return true;
        else if (Listas.Mapa.Azulejo[X, Y].Bloqueio[(byte)Direção])
            return true;
        else if (HáJogador(Mapa, Próximo_X, Próximo_Y) > 0 || HáNPC(Próximo_X, Próximo_Y) > 0)
            return true;

        return false;
    }

    public static byte HáNPC(short X, short Y)
    {
        // Verifica se há algum npc na cordenada
        for (byte i = 1; i <= Listas.Mapa.Temp_NPC.GetUpperBound(0); i++)
            if (Listas.Mapa.Temp_NPC[i].Índice > 0)
                if (Listas.Mapa.Temp_NPC[i].X == X && Listas.Mapa.Temp_NPC[i].Y == Y)
                    return i;

        return 0;
    }

    public static byte HáJogador(short Num, short X, short Y)
    {
        // Verifica se há algum Jogador na cordenada
        for (byte i = 1; i <= Jogador.MaiorÍndice; i++)
            if (Listas.Jogador[i].X == X && Listas.Jogador[i].Y == Y && Listas.Jogador[i].Mapa == Num)
                return i;

        return 0;
    }

    private static void Fumaça()
    {
        // Faz a movimentação
        if (Jogador.EstáJogando(Jogador.MeuÍndice))
        {
            Cálcular_Fumaça_X();
            Cálcular_Fumaça_Y();
        }
    }

    private static void Cálcular_Fumaça_X()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Listas.Mapa.Fumaça.Textura]);
        int VelocidadeX = Listas.Mapa.Fumaça.VelocidadeX;

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
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Listas.Mapa.Fumaça.Textura]);
        int VelocidadeY = Listas.Mapa.Fumaça.VelocidadeY;

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
        if (!Jogador.EstáJogando(Jogador.MeuÍndice)) return;
        if (Listas.Mapa.Clima.Tipo == 0) return;

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
        for (short i = 1; i <= Listas.Clima_Partículas.GetUpperBound(0); i++)
            if (!Listas.Clima_Partículas[i].Visível)
            {
                if (Game.Aleatório.Next(0, Máx_Clima_Intensidade - Listas.Mapa.Clima.Intensidade) == 0)
                {
                    if (!Parar)
                    {
                        // Cria a partícula
                        Listas.Clima_Partículas[i].Visível = true;

                        // Cria a partícula de acordo com o seu tipo
                        switch ((Climas)Listas.Mapa.Clima.Tipo)
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
                // Movimenta a partícula de acordo com o seu tipo
                switch ((Climas)Listas.Mapa.Clima.Tipo)
                {
                    case Climas.Trovoando:
                    case Climas.Chovendo: Clima_Chuva_Movimentação(i); break;
                    case Climas.Nevando: Clima_Neve_Movimentação(i, Movimentar); break;
                }

                // Reseta a partícula
                if (Listas.Clima_Partículas[i].x > Game.Tela_Largura || Listas.Clima_Partículas[i].y > Game.Tela_Altura)
                    Listas.Clima_Partículas[i] = new Listas.Estruturas.Clima();
            }

        // Trovoadas
        if (Listas.Mapa.Clima.Tipo == (byte)Climas.Trovoando)
            if (Game.Aleatório.Next(0, Máx_Clima_Intensidade * 10 - Listas.Mapa.Clima.Intensidade * 2) == 0)
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
        Listas.Clima_Partículas[i].Velocidade = Game.Aleatório.Next(8, 13);

        if (Game.Aleatório.Next(2) == 0)
        {
            Listas.Clima_Partículas[i].x = -32;
            Listas.Clima_Partículas[i].y = Game.Aleatório.Next(-32, Game.Tela_Altura);
        }
        else
        {
            Listas.Clima_Partículas[i].x = Game.Aleatório.Next(-32, Game.Tela_Largura);
            Listas.Clima_Partículas[i].y = -32;
        }
    }

    private static void Clima_Chuva_Movimentação(int i)
    {
        // Movimenta a partícula
        Listas.Clima_Partículas[i].x += Listas.Clima_Partículas[i].Velocidade;
        Listas.Clima_Partículas[i].y += Listas.Clima_Partículas[i].Velocidade;
    }

    private static void Clima_Neve_Criação(int i)
    {
        // Define a velocidade e a posição da partícula
        Listas.Clima_Partículas[i].Velocidade = Game.Aleatório.Next(1, 3);
        Listas.Clima_Partículas[i].y = -32;
        Listas.Clima_Partículas[i].x = Game.Aleatório.Next(-32, Game.Tela_Largura);
        Listas.Clima_Partículas[i].Inicío = Listas.Clima_Partículas[i].x;

        if (Game.Aleatório.Next(2) == 0)
            Listas.Clima_Partículas[i].Voltar = false;
        else
            Listas.Clima_Partículas[i].Voltar = true;
    }

    private static void Clima_Neve_Movimentação(int i, bool Movimentrar = true)
    {
        int Diferença = Game.Aleatório.Next(0, Neve_Movimento / 3);
        int x1 = Listas.Clima_Partículas[i].Inicío + Neve_Movimento + Diferença;
        int x2 = Listas.Clima_Partículas[i].Inicío - Neve_Movimento - Diferença;

        // Faz com que a partícula volte
        if (x1 <= Listas.Clima_Partículas[i].x)
            Listas.Clima_Partículas[i].Voltar = true;
        else if (x2 >= Listas.Clima_Partículas[i].x)
            Listas.Clima_Partículas[i].Voltar = false;

        // Movimenta a partícula
        Listas.Clima_Partículas[i].y += Listas.Clima_Partículas[i].Velocidade;

        if (Movimentrar)
            if (Listas.Clima_Partículas[i].Voltar)
                Listas.Clima_Partículas[i].x -= 1;
            else
                Listas.Clima_Partículas[i].x += 1;
    }

    public static void Clima_Ajustar()
    {
        // Para todos os sons
        Áudio.Som.Parar_Tudo();

        // Redimensiona a lista
        switch ((Climas)Listas.Mapa.Clima.Tipo)
        {
            case Climas.Trovoando:
            case Climas.Chovendo:
                // Reproduz o som chuva
                Áudio.Som.Reproduzir(Áudio.Sons.Chuva, true);

                // Redimensiona a estrutura
                Listas.Clima_Partículas = new Listas.Estruturas.Clima[Máx_Chuva_Partículas + 1];
                break;
            case Climas.Nevando: Listas.Clima_Partículas = new Listas.Estruturas.Clima[Máx_Neve_Partículas + 1]; break;
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
            for (byte x = 0; x <= Listas.Mapa.Largura; x++)
                for (byte y = 0; y <= Listas.Mapa.Altura; y++)
                    for (byte c = 0; c <= (byte)Camadas.Quantidade - 1; c++)
                        for (byte q = 0; q <= Listas.Mapa.Azulejo[x, y].Dados.GetUpperBound(1); q++)
                            if (Listas.Mapa.Azulejo[x, y].Dados[c, q].Automático)
                                // Faz os cálculos para a autocriação
                                Calcular(x, y, c, q);
        }

        public static void Atualizar(int x, int y, byte Camada_Num, byte Camada_Tipo)
        {
            // Atualiza os azulejos necessários
            for (int x2 = x - 2; x2 <= x + 2; x2++)
                for (int y2 = y - 2; y2 <= y + 2; y2++)
                    if (x2 >= 0 && x2 <= Listas.Mapa.Largura && y2 >= 0 && y2 <= Listas.Mapa.Altura)
                        // Faz os cálculos para a autocriação
                        Calcular((byte)x2, (byte)y2, Camada_Num, Camada_Tipo);
        }

        public static void Definir(byte x, byte y, byte Camada_Num, byte Camada_Tipo, byte Parte, string Letra)
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
            Listas.Estruturas.Azulejo_Dados Dados = Listas.Mapa.Azulejo[x, y].Dados[Camada_Tipo, Camada_Num];
            Listas.Mapa.Azulejo[x, y].Dados[Camada_Tipo, Camada_Num].Mini[Parte].X = Dados.x * Game.Grade + Posição.X;
            Listas.Mapa.Azulejo[x, y].Dados[Camada_Tipo, Camada_Num].Mini[Parte].Y = Dados.y * Game.Grade + Posição.Y;
        }

        public static bool Verificar(int X1, int Y1, int X2, int Y2, byte Camada_Num, byte Camada_Tipo)
        {
            Listas.Estruturas.Azulejo_Dados Dados1, Dados2;

            // Somente se necessário
            if (X2 < 0 || X2 > Listas.Mapa.Largura || Y2 < 0 || Y2 > Listas.Mapa.Altura) return true;

            // Dados
            Dados1 = Listas.Mapa.Azulejo[X1, Y1].Dados[Camada_Tipo, Camada_Num];
            Dados2 = Listas.Mapa.Azulejo[X2, Y2].Dados[Camada_Tipo, Camada_Num];

            // Verifica se são os mesmo azulejos
            if (!Dados2.Automático) return false;
            if (Dados1.Azulejo != Dados2.Azulejo) return false;
            if (Dados1.x != Dados2.x) return false;
            if (Dados1.y != Dados2.y) return false;

            // Não há nada de errado
            return true;
        }

        public static void Calcular(byte x, byte y, byte Camada_Num, byte Camada_Tipo)
        {
            // Calcula as quatros partes do azulejo
            Calcular_NO(x, y, Camada_Num, Camada_Tipo);
            Calcular_NE(x, y, Camada_Num, Camada_Tipo);
            Calcular_SO(x, y, Camada_Num, Camada_Tipo);
            Calcular_SE(x, y, Camada_Num, Camada_Tipo);
        }

        public static void Calcular_NO(byte x, byte y, byte Camada_Num, byte Camada_Tipo)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(x, y, x - 1, y - 1, Camada_Num, Camada_Tipo)) Azulejo[1] = true;
            if (Verificar(x, y, x, y - 1, Camada_Num, Camada_Tipo)) Azulejo[2] = true;
            if (Verificar(x, y, x - 1, y, Camada_Num, Camada_Tipo)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[2] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[2] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[2] && !Azulejo[3]) Forma = Adição.Vertical;
            if (!Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Tipo, 0, "e"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Tipo, 0, "a"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Tipo, 0, "i"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Tipo, 0, "m"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Tipo, 0, "q"); break;
            }
        }

        public static void Calcular_NE(byte x, byte y, byte Camada_Num, byte Camada_Tipo)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(x, y, x, y - 1, Camada_Num, Camada_Tipo)) Azulejo[1] = true;
            if (Verificar(x, y, x + 1, y - 1, Camada_Num, Camada_Tipo)) Azulejo[2] = true;
            if (Verificar(x, y, x + 1, y, Camada_Num, Camada_Tipo)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Tipo, 1, "j"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Tipo, 1, "b"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Tipo, 1, "f"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Tipo, 1, "r"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Tipo, 1, "n"); break;
            }
        }

        public static void Calcular_SO(byte x, byte y, byte Camada_Num, byte Camada_Tipo)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudoeste)
            if (Verificar(x, y, x - 1, y, Camada_Num, Camada_Tipo)) Azulejo[1] = true;
            if (Verificar(x, y, x - 1, y + 1, Camada_Num, Camada_Tipo)) Azulejo[2] = true;
            if (Verificar(x, y, x, y + 1, Camada_Num, Camada_Tipo)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Horizontal;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Tipo, 2, "o"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Tipo, 2, "c"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Tipo, 2, "s"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Tipo, 2, "g"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Tipo, 2, "k"); break;
            }
        }

        public static void Calcular_SE(byte x, byte y, byte Camada_Num, byte Camada_Tipo)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudeste)
            if (Verificar(x, y, x, y + 1, Camada_Num, Camada_Tipo)) Azulejo[1] = true;
            if (Verificar(x, y, x + 1, y + 1, Camada_Num, Camada_Tipo)) Azulejo[2] = true;
            if (Verificar(x, y, x + 1, y, Camada_Num, Camada_Tipo)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(x, y, Camada_Num, Camada_Tipo, 3, "t"); break;
                case Adição.Exterior: Definir(x, y, Camada_Num, Camada_Tipo, 3, "d"); break;
                case Adição.Horizontal: Definir(x, y, Camada_Num, Camada_Tipo, 3, "p"); break;
                case Adição.Vertical: Definir(x, y, Camada_Num, Camada_Tipo, 3, "l"); break;
                case Adição.Preencher: Definir(x, y, Camada_Num, Camada_Tipo, 3, "h"); break;
            }
        }
    }
}

partial class Gráficos
{
    public static void Mapa_Azulejos(byte c)
    {
        // Previni erros
        if (Listas.Mapa.Nome == null) return;

        // Dados
        System.Drawing.Color TempCor = System.Drawing.Color.FromArgb(Listas.Mapa.Coloração);
        SFML.Graphics.Color Cor = CCor(TempCor.R, TempCor.G, TempCor.B);

        // Desenha todas as camadas dos azulejos
        for (short x = (short)Game.Azulejos_Visão.X; x <= Game.Azulejos_Visão.Width; x++)
            for (short y = (short)Game.Azulejos_Visão.Y; y <= Game.Azulejos_Visão.Height; y++)
                if (!Map.ForaDoLimite(x, y))
                    for (byte q = 0; q <= Listas.Mapa.Azulejo[x, y].Dados.GetUpperBound(1); q++)
                        if (Listas.Mapa.Azulejo[x, y].Dados[c, q].Azulejo > 0)
                        {
                            int x2 = Listas.Mapa.Azulejo[x, y].Dados[c, q].x * Game.Grade;
                            int y2 = Listas.Mapa.Azulejo[x, y].Dados[c, q].y * Game.Grade;

                            // Desenha o azulejo
                            if (!Listas.Mapa.Azulejo[x, y].Dados[c, q].Automático)
                                Desenhar(Tex_Azulejo[Listas.Mapa.Azulejo[x, y].Dados[c, q].Azulejo], Game.ConverterX(x * Game.Grade), Game.ConverterY(y * Game.Grade), x2, y2, Game.Grade, Game.Grade, Cor);
                            else
                                Mapas_AutoCriação(new Point(Game.ConverterX(x * Game.Grade), Game.ConverterY(y * Game.Grade)), Listas.Mapa.Azulejo[x, y].Dados[c, q], Cor);
                        }
    }

    public static void Mapas_AutoCriação(Point Posição, Listas.Estruturas.Azulejo_Dados Dados, SFML.Graphics.Color Cor)
    {
        // Desenha os 4 mini azulejos
        for (byte i = 0; i <= 3; i++)
        {
            Point Destino = Posição, Fonte = Dados.Mini[i];

            // Partes do azulejo
            switch (i)
            {
                case 1: Destino.X += 16; break;
                case 2: Destino.Y += 16; break;
                case 3: Destino.X += 16; Destino.Y += 16; break;
            }

            // Renderiza o mini azulejo
            Desenhar(Tex_Azulejo[Dados.Azulejo], new Rectangle(Fonte.X, Fonte.Y, 16, 16), new Rectangle(Destino, new Size(16, 16)), Cor);
        }
    }

    public static void Mapa_Panorama()
    {
        // Desenha o panorama
        if (Listas.Mapa.Panorama > 0)
            Desenhar(Tex_Panorama[Listas.Mapa.Panorama], new Point(0));
    }

    public static void Mapa_Fumaça()
    {
        Listas.Estruturas.Mapa_Fumaça Dados = Listas.Mapa.Fumaça;
        Size Textura_Tamanho = TTamanho(Tex_Fumaça[Dados.Textura]);

        // Previni erros
        if (Dados.Textura <= 0) return;

        // Desenha a fumaça
        for (int x = -1; x <= Listas.Mapa.Largura * Game.Grade / Textura_Tamanho.Width + 1; x++)
            for (int y = -1; y <= Listas.Mapa.Altura * Game.Grade / Textura_Tamanho.Height + 1; y++)
                Desenhar(Tex_Fumaça[Dados.Textura], new Point(x * Textura_Tamanho.Width + Map.Fumaça_X, y * Textura_Tamanho.Height + Map.Fumaça_Y), new SFML.Graphics.Color(255, 255, 255, Dados.Transparência));
    }

    public static void Mapa_Clima()
    {
        byte x = 0;

        // Somente se necessário
        if (Listas.Mapa.Clima.Tipo == 0) return;

        // Textura
        switch ((Map.Climas)Listas.Mapa.Clima.Tipo)
        {
            case Map.Climas.Nevando: x = 32; break;
        }

        // Desenha as partículas
        for (int i = 1; i <= Listas.Clima_Partículas.GetUpperBound(0); i++)
            if (Listas.Clima_Partículas[i].Visível)
                Desenhar(Tex_Clima, new Rectangle(x, 0, 32, 32), new Rectangle(Listas.Clima_Partículas[i].x, Listas.Clima_Partículas[i].y, 32, 32), CCor(255, 255, 255, 150));

        // Trovoadas
        Desenhar(Tex_Preenchido, 0, 0, 0, 0, Game.Tela_Largura, Game.Tela_Altura, new SFML.Graphics.Color(255, 255, 255, Map.Relâmpago));
    }

    public static void Mapa_Nome()
    {
        SFML.Graphics.Color Cor;

        // Somente se necessário
        if (string.IsNullOrEmpty(Listas.Mapa.Nome)) return;

        // A cor do texto vária de acordo com a moral do mapa
        switch (Listas.Mapa.Moral)
        {
            case (byte)Map.Morais.Perigoso: Cor = SFML.Graphics.Color.Red; break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o nome do mapa
        Desenhar(Listas.Mapa.Nome, 463, 48, Cor);
    }

    public static void Mapa_Itens()
    {
        // Desenha todos os itens que estão no chão
        for (byte i = 1; i <= Listas.Mapa.Temp_Item.GetUpperBound(0); i++)
        {
            Listas.Estruturas.Mapa_Itens Dados = Listas.Mapa.Temp_Item[i];

            // Somente se necessário
            if (Dados.Índice == 0) continue;

            // Desenha o item
            Point Posição = new Point(Game.ConverterX(Dados.X * Game.Grade), Game.ConverterY(Dados.Y * Game.Grade));
            Desenhar(Tex_Item[Listas.Item[Dados.Índice].Textura], Posição);
        }
    }
}