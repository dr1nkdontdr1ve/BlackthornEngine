using System;
using System.Drawing;
using System.Windows.Forms;

class Tie
{
    // Contagens
    private static int Digitalizador_Contagem = 0;
    private static int Chat_Contagem = 0;

    public static void Principal()
    {
        int Contagem = 0;
        int Contagem_1000 = 0;
        int Contagem_30 = 0;
        short FPS = 0;

        while (Program.Funcionado)
        {
            Contagem = Environment.TickCount;

            // Handles incoming data
            Rede.ReceberData();

            // Displays graphics to screen
            Gráficos.Apresentar();

            // Events
            Digitalizador();
            Map.Lógica();

            if (Player.MeuÍndice > 0 && Tools.JanelaAtual == Tools.Janelas.Game)
                if (Contagem_30 < Environment.TickCount)
                {
                    // Logic
                    Player.Lógica();
                    NPC.Lógica();

                    // Restarts the count
                    Contagem_30 = Environment.TickCount + 30;
                }

            // Causes the application to remain stable
            Application.DoEvents();
            while (Environment.TickCount < Contagem + 15) System.Threading.Thread.Sleep(1);

            // Calculate the FPS
            if (Contagem_1000 < Environment.TickCount)
            {
                Enviar.Latência();
                Game.FPS = FPS;
                FPS = 0;
                Contagem_1000 = Environment.TickCount + 1000;
            }
            else
                FPS += 1;
        }

        // Close the game
        Program.Leave();
    }

    private static void Digitalizador()
    {
        // Contagem para a renderização da referência do último texto
        if (Digitalizador_Contagem < Environment.TickCount)
        {
            Digitalizador_Contagem = Environment.TickCount + 500;
            Scanners.Sinal = !Scanners.Sinal;

            // Se necessário foca o digitalizador de novo
            Scanners.Focalizar();
        }

        // Chat
        if (Tools.Linhas_Visível && !Panels.Encontrar("Chat").General.Visível)
        {
            if (Chat_Contagem < Environment.TickCount)
                Tools.Linhas_Visível = false;
        }
        else
            Chat_Contagem = Chat_Contagem = Environment.TickCount + 10000;
    }
}