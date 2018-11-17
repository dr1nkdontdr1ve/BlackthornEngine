using System;
using System.Windows.Forms;
using System.Threading;

class Program
{
    public static void Main()
    {
        // Opens the server and sets its settings
        Console.Title = "Blackthorn Server";
        Logo();
        Console.WriteLine("[Initialize Server Startup]");

        // Checks if all directories exist, if they do not exist then create them
        Directories.Create();
        Console.WriteLine("Directories created.");

        // Clears and loads all required data
        Read.Required();
        Clean.Required();

        // Creates network devices
        Network.Start();
        Console.WriteLine("Network started.");

        // Calculates how long it took to initialize the server
        Console.WriteLine("\r\n" + "Server started. Type 'Help' to see the commands." + "\r\n");

        // Starts the ties
        Thread Comandos_Laço = new Thread(Tie.Commands);
        Comandos_Laço.Start();
        Tie.Principal();
    }

    public static void Logo()
    {
        Console.WriteLine(@"BlackthornEngine rpg 2d engine" + "\r\n");
    }

    public static void Close()
    {
        // Disconnect all players and close the server
        Network.Device.Shutdown("The server has been shut down.");
        Application.Exit();
    }

    public static void RunCommand(string Command)
    {
        // Previni sobrecargas
        if (string.IsNullOrEmpty(Command))
            return;

        // Transforms the command into lowercase letters
        Command = Command.ToLower();

        // Separate commands into parts
        string[] Parts = Command.Split(' ');

        // Execute the given command
        switch (Parts[0].ToLower())
        {
            case "help":
                Console.WriteLine(@"    List of available commands:
    changerank                  - defines a level of access for a given player
    cps                            - shows the current server cps
    reload                     - reloads certain data");
                break;
            case "cps":
                Console.WriteLine("CPS: " + Game.CPS);
                break;
            case "changerank":
                byte Access;

                // Checks if what is typed correctly
                if (Parts.GetUpperBound(0) < 2 || string.IsNullOrEmpty(Parts[1]) || !Byte.TryParse(Parts[2], out Access))
                {
                    Console.WriteLine("Use: definiracesso 'Player's name' 'Rank'");
                    return;
                }

                // Find the player
                byte Index = Player.Encontrar(Parts[1]);

                if (Index == 0)
                {
                    Console.WriteLine("This player is not online or does not exist.");
                    return;
                }

                // Sets player access
                Lists.Player[Index].Access = (Game.Rank)Convert.ToByte(Parts[2]);

                // Save the data
                Write.Player(Index);
                Console.WriteLine("Access from " + (Game.Rank)Convert.ToByte(Parts[2]) + " granted to " + Parts[1] + ".");
                break;
            case "reload":
                // Checks if what is typed correctly
                if (Parts.GetUpperBound(0) < 1 || string.IsNullOrEmpty(Parts[1]))
                {
                    Console.WriteLine("Use: reload 'Desired item");
                    return;
                }

                switch (Parts[1])
                {
                    // Reload the maps
                    case "maps":
                        Read.Maps();
                        Console.WriteLine("Reloaded maps");
                        break;
                    // Recharges classes
                    case "classes":
                        Read.Classes();
                        Console.WriteLine("Recharged classes");
                        break;
                }
                break;
            // If the command does not exist send a help message
            default:
                Console.WriteLine("This command does not exist.");
                break;
        }
    }
}