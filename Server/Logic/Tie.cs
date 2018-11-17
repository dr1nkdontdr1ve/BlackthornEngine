using System;
using System.Windows.Forms;

class Tie
{
    // Used to keep the application open
    public static bool Working = true;

    // Counts
    public static int Score_1000 = 0, Score_5000 = 0;
    public static int Score_500 = 0;
    public static int Score_Player_Reneration = 0;
    public static int Score_NPC_Reneration = 0;
    public static int Score_Map_Items = 0;

    public static void Principal()
    {
        int CPS = 0;

        while (Working)
        {
            // Handles incoming data
            Network.ReceivingData();

            if (Score_500 < Environment.TickCount)
            {
                //Map Logics
                Map.Logic();
                Player.Logic();

                //Restarts the count
                Score_500 = Environment.TickCount + 500;
            }

            //Causes the application to remain stable
            Application.DoEvents();
            if (Game.CPS_Travado) System.Threading.Thread.Sleep(1);

            //Calculates the CPS
            if (Score_1000 < Environment.TickCount)
            {
                Game.CPS = CPS;
                CPS = 0;
                Score_1000 = Environment.TickCount + 1000;
            }
            else
                CPS += 1;
        }

        // Closes the application
        Program.Close();
    }

    public static void Commands()
    {
        // Tie so that it is possible to use commands by the console
        while (Working)
        {
            Console.Write("Execute: ");
            Program.RunCommand(Console.ReadLine());
        }
    }
}