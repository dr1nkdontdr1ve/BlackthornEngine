using System;
using System.Drawing;
using System.Windows.Forms;

class Tie
{
    // Contagens
    private static int Scanner_Contagem = 0;
    private static int Chat_Contagem = 0;

    public static void Principal()
    {
        int Contagem = 0;
        int Contagem_1000 = 0;
        int Contagem_30 = 0;
        short FPS = 0;

        while (Program.Functional)
        {
            Contagem = Environment.TickCount;

            // Handles incoming data
            Network.ReceivingData();

            // Displays graphics to screen
            Graphics.Apresentar();

            // Events
            Scanner();
            Map.Logic();

            if (Player.MyIndex > 0 && Tools.CurrentWindow == Tools.Windows.Game)
                if (Contagem_30 < Environment.TickCount)
                {
                    // Logic
                    Player.Logic();
                    NPC.Logic();

                    // Restarts the count
                    Contagem_30 = Environment.TickCount + 30;
                }

            // Causes the application to remain stable
            Application.DoEvents();
            while (Environment.TickCount < Contagem + 15) System.Threading.Thread.Sleep(1);

            // Calculate the FPS
            if (Contagem_1000 < Environment.TickCount)
            {
                Sending.Latency();
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

    private static void Scanner()
    {
        // Contagem para a renderização da referência do último Text
        if (Scanner_Contagem < Environment.TickCount)
        {
            Scanner_Contagem = Environment.TickCount + 500;
            Scanners.Sinal = !Scanners.Sinal;

            // Se necessário foca o Scanner de novo
            Scanners.Focalizar();
        }

        // Chat
        if (Tools.Lines_Visible && !Panels.Locate("Chat").General.Visible)
        {
            if (Chat_Contagem < Environment.TickCount)
                Tools.Lines_Visible = false;
        }
        else
            Chat_Contagem = Chat_Contagem = Environment.TickCount + 10000;
    }
}