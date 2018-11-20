using System;
using System.Drawing;
using System.Windows.Forms;

class Laço
{
    // Contadores
    private static int FumaçaX_Tempo = 0;
    private static int FumaçaY_Tempo = 0;
    private static int Neve_Tempo = 0;
    private static int Relâmpago_Tempo = 0;
    public static byte Anim = 0;

    public static void Principal()
    {
        int Contagem = 0;
        int Contagem_1000 = 0;
        short FPS = 0;

        while (Program.Funcionado)
        {
            Contagem = Environment.TickCount;

            // Eventos
            Editor_Mapas_Fumaça();
            Editor_Mapas_Clima();
            Editor_Mapas_Música();

            // Desenha os gráficos
            Gráficos.Apresentar();

            // Faz com que a aplicação se mantenha estável
            Application.DoEvents();
            while (Environment.TickCount < Contagem + 10) System.Threading.Thread.Sleep(1);

            // FPS
            if (Contagem_1000 < Environment.TickCount)
            {
                // Cálcula o FPS
                Globais.FPS = FPS;
                FPS = 0;

                // Reinicia a contagem
                Contagem_1000 = Environment.TickCount + 1000;
            }
            else
                FPS += 1;
        }

        // Fecha a aplicação
        Program.Sair();
    }

    private static void Editor_Mapas_Fumaça()
    {
        // Faz a movimentação
        if (Editor_Mapas.Objetos.Visible)
        {
            Editor_Mapas_Fumaça_X();
            Editor_Mapas_Fumaça_Y();
        }
    }

    private static void Editor_Mapas_Fumaça_X()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Listas.Mapa[Editor_Mapas.Selecionado].Fumaça.Textura]);
        int VelocidadeX = Listas.Mapa[Editor_Mapas.Selecionado].Fumaça.VelocidadeX;

        // Apenas se necessário
        if (FumaçaX_Tempo >= Environment.TickCount) return;
        if (VelocidadeX == 0) return;

        // Movimento para trás
        if (VelocidadeX < 0)
        {
            Globais.Fumaça_X -= 1;
            if (Globais.Fumaça_X < -Tamanho.Width) Globais.Fumaça_X = 0;
        }
        // Movimento para frente
        else
        {
            Globais.Fumaça_X += 1;
            if (Globais.Fumaça_X > Tamanho.Width) Globais.Fumaça_X = 0;
        }

        // Contagem
        if (VelocidadeX < 0) VelocidadeX *= -1;
        FumaçaX_Tempo = Environment.TickCount + 50 - VelocidadeX;
    }

    private static void Editor_Mapas_Fumaça_Y()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Fumaça[Listas.Mapa[Editor_Mapas.Selecionado].Fumaça.Textura]);
        int VelocidadeY = Listas.Mapa[Editor_Mapas.Selecionado].Fumaça.VelocidadeY;

        // Apenas se necessário
        if (FumaçaY_Tempo >= Environment.TickCount) return;
        if (VelocidadeY == 0) return;

        // Movimento para trás
        if (VelocidadeY < 0)
        {
            Globais.Fumaça_Y -= 1;
            if (Globais.Fumaça_Y < -Tamanho.Height) Globais.Fumaça_Y = 0;
        }
        // Movimento para frente
        else
        {
            Globais.Fumaça_Y += 1;
            if (Globais.Fumaça_Y > Tamanho.Height) Globais.Fumaça_Y = 0;
        }

        // Contagem
        if (VelocidadeY < 0) VelocidadeY *= -1;
        FumaçaY_Tempo = Environment.TickCount + 50 - VelocidadeY;
    }

    private static void Editor_Mapas_Clima()
    {
        bool Parar = false, Movimentar = true;
        byte Primerio_Trovão = (byte)Áudio.Sons.Trovão_1;
        byte Último_Trovão = (byte)Áudio.Sons.Trovão_4;

        // Somente se necessário
        if (!Editor_Mapas.Objetos.Visible || Listas.Mapa[Editor_Mapas.Selecionado].Clima.Tipo == 0 || !Editor_Mapas.Objetos.butVisualização.Checked)
        {
            if (Áudio.Som.Lista != null)
                if (Áudio.Som.Lista[(byte)Áudio.Sons.Chuva].Status == SFML.Audio.SoundStatus.Playing) Áudio.Som.Parar_Tudo();
            return;
        }

        // Clima do mapa
        Listas.Estruturas.Mapa_Clima Clima = Listas.Mapa[Editor_Mapas.Selecionado].Clima;

        // Reproduz o som chuva
        if ((Globais.Climas)Clima.Tipo == Globais.Climas.Chovendo || (Globais.Climas)Clima.Tipo == Globais.Climas.Trovoando)
        {
            if (Áudio.Som.Lista[(byte)Áudio.Sons.Chuva].Status != SFML.Audio.SoundStatus.Playing)
                Áudio.Som.Reproduzir(Áudio.Sons.Chuva);
        }
        else
          if (Áudio.Som.Lista[(byte)Áudio.Sons.Chuva].Status == SFML.Audio.SoundStatus.Playing) Áudio.Som.Parar_Tudo();

        // Contagem da neve
        if (Neve_Tempo < Environment.TickCount)
        {
            Movimentar = true;
            Neve_Tempo = Environment.TickCount + 35;
        }
        else
            Movimentar = false;

        // Contagem dos relâmpagos
        if (Globais.Relâmpago > 0)
            if (Relâmpago_Tempo < Environment.TickCount)
            {
                Globais.Relâmpago -= 10;
                Relâmpago_Tempo = Environment.TickCount + 25;
            }

        // Adiciona uma nova partícula
        for (int i = 1; i <= Listas.Clima_Partículas.GetUpperBound(0); i++)
            if (!Listas.Clima_Partículas[i].Visível)
            {
                if (Globais.Aleatório.Next(0, Globais.Máx_Clima_Intensidade - Clima.Intensidade) == 0)
                {
                    if (!Parar)
                    {
                        // Cria a partícula
                        Listas.Clima_Partículas[i].Visível = true;

                        // Cria a partícula de acordo com o seu tipo
                        switch ((Globais.Climas)Clima.Tipo)
                        {
                            case Globais.Climas.Trovoando:
                            case Globais.Climas.Chovendo: Clima_Chuva_Criação(i); break;
                            case Globais.Climas.Nevando: Clima_Neve_Criação(i); break;
                        }
                    }
                }

                Parar = true;
            }
            else
            {
                // Movimenta a partícula de acordo com o seu tipo
                switch ((Globais.Climas)Clima.Tipo)
                {
                    case Globais.Climas.Trovoando:
                    case Globais.Climas.Chovendo: Clima_Chuva_Movimentação(i); break;
                    case Globais.Climas.Nevando: Clima_Neve_Movimentação(i, Movimentar); break;
                }

                // Reseta a partícula
                if (Listas.Clima_Partículas[i].x > Editor_Mapas.AzulejosVisíveis.Width * Globais.Grade || Listas.Clima_Partículas[i].y > Editor_Mapas.AzulejosVisíveis.Height * Globais.Grade)
                    Listas.Clima_Partículas[i] = new Listas.Estruturas.Clima();
            }

        // Trovoadas
        if (Clima.Tipo == (byte)Globais.Climas.Trovoando)
            if (Globais.Aleatório.Next(0, Globais.Máx_Clima_Intensidade * 10 - Clima.Intensidade * 2) == 0)
            {
                // Som do trovão
                int Trovão = Globais.Aleatório.Next(Primerio_Trovão, Último_Trovão);
                Áudio.Som.Reproduzir((Áudio.Sons)Trovão);

                // Relâmpago
                if (Trovão < 6) Globais.Relâmpago = 190;
            }
    }

    private static void Parar_Chuva_Som()
    {
        // Para o som da chuva se estiver reproduzindo
        if (Áudio.Som.Lista[(byte)Áudio.Sons.Chuva].Status == SFML.Audio.SoundStatus.Playing)
        {
            Áudio.Som.Lista[(byte)Áudio.Sons.Chuva].Stop();
        }
    }

    private static void Clima_Chuva_Criação(int i)
    {
        // Define a velocidade e a posição da partícula
        Listas.Clima_Partículas[i].Velocidade = Globais.Aleatório.Next(8, 13);

        if (Globais.Aleatório.Next(2) == 0)
        {
            Listas.Clima_Partículas[i].x = -32;
            Listas.Clima_Partículas[i].y = Globais.Aleatório.Next(-32, Editor_Mapas.Objetos.picMapa.Height);
        }
        else
        {
            Listas.Clima_Partículas[i].x = Globais.Aleatório.Next(-32, Editor_Mapas.Objetos.picMapa.Width);
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
        Listas.Clima_Partículas[i].Velocidade = Globais.Aleatório.Next(1, 3);
        Listas.Clima_Partículas[i].y = -32;
        Listas.Clima_Partículas[i].x = Globais.Aleatório.Next(-32, Editor_Mapas.Objetos.picMapa.Width);
        Listas.Clima_Partículas[i].Inicío = Listas.Clima_Partículas[i].x;

        if (Globais.Aleatório.Next(2) == 0)
            Listas.Clima_Partículas[i].Voltar = false;
        else
            Listas.Clima_Partículas[i].Voltar = true;
    }

    private static void Clima_Neve_Movimentação(int i, bool Movimentrar = true)
    {
        int Diferença = Globais.Aleatório.Next(0, Globais.Neve_Movimento / 3);
        int x1 = Listas.Clima_Partículas[i].Inicío + Globais.Neve_Movimento + Diferença;
        int x2 = Listas.Clima_Partículas[i].Inicío - Globais.Neve_Movimento - Diferença;

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

    private static void Editor_Mapas_Música()
    {
        // Apenas se necessário
        if (!Editor_Mapas.Objetos.Visible) goto parar;
        if (!Editor_Mapas.Objetos.butÁudio.Checked) goto parar;
        if (!Editor_Mapas.Objetos.butVisualização.Checked) goto parar;
        if (Listas.Mapa[Editor_Mapas.Selecionado].Música == 0) goto parar;

        // Inicia a música
        if (Áudio.Música.Reprodutor == null || Áudio.Música.Atual != Listas.Mapa[Editor_Mapas.Selecionado].Música)
            Áudio.Música.Reproduzir((Áudio.Músicas)Listas.Mapa[Editor_Mapas.Selecionado].Música);
        return;
    parar:
        // Para a música
        if (Áudio.Música.Reprodutor != null) Áudio.Música.Parar();
    }
}
