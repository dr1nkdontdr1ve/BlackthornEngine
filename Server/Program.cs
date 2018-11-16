using System;
using System.Windows.Forms;
using System.Threading;

class Program
{
    public static void Main()
    {
        // Opens the server and sets its settings
        Console.Title = "Server";
        Logo();
        Console.WriteLine("[Initialization]");

        // Checks if all directories exist, if they do not exist then create them
        Diretórios.Criar();
        Console.WriteLine("Directories created.");

        // Clears and loads all required data
        Ler.Necessário();
        Limpar.Necessário();

        // Creates network devices
        Rede.Iniciar();
        Console.WriteLine("Network started.");

        // Calculates how long it took to initialize the server
        Console.WriteLine("\r\n" + "Servidor iniciado. Digite 'Ajuda' para ver os comandos." + "\r\n");

        // Starts the ties
        Thread Comandos_Laço = new Thread(Tie.Commands);
        Comandos_Laço.Start();
        Tie.Principal();
    }

    public static void Logo()
    {
        Console.WriteLine(@"  ______              _____     _
 |   ___|            |     \   | |
 |  |     _ ____   _ |   __/ _ | |_  ___
 |  |    | '__/\\ // |   \_ | || __|/ __|
 |  |___ | |    | |  |     \| || |_ \__ \
 |______||_|    |_|  |_____/|_| \__||___/
                      orpg's 2d engine" + "\r\n");
    }

    public static void Close()
    {
        // Disconnect all players and close the server
        Rede.Dispositivo.Shutdown("The server has been shut down.");
        Application.Exit();
    }

    public static void ExecutarComando(string Comando)
    {
        // Previni sobrecargas
        if (string.IsNullOrEmpty(Comando))
            return;

        // Transforms the command into lowercase letters
        Comando = Comando.ToLower();

        // Separate commands into parts
        string[] Partes = Comando.Split(' ');

        // Execute the given command
        switch (Partes[0].ToLower())
        {
            case "ajuda":
                Console.WriteLine(@"    List of available commands:
    definiracesso                  - defines a level of access for a given player
    cps                            - shows the current server cps
    recarregar                     - reloads certain data");
                break;
            case "cps":
                Console.WriteLine("CPS: " + Game.CPS);
                break;
            case "definiracesso":
                byte Acesso;

                // Checks if what is typed correctly
                if (Partes.GetUpperBound(0) < 2 || string.IsNullOrEmpty(Partes[1]) || !Byte.TryParse(Partes[2], out Acesso))
                {
                    Console.WriteLine("Utilize: definiracesso 'Nome do Jogador' 'Acesso'");
                    return;
                }

                // Find the player
                byte Índice = Player.Encontrar(Partes[1]);

                if (Índice == 0)
                {
                    Console.WriteLine("This player is not online or does not exist.");
                    return;
                }

                // Sets player access
                Listas.Jogador[Índice].Acesso = (Game.Acessos)Convert.ToByte(Partes[2]);

                // Save the data
                Escrever.Jogador(Índice);
                Console.WriteLine("Access from " + (Game.Acessos)Convert.ToByte(Partes[2]) + " granted to " + Partes[1] + ".");
                break;
            case "recarregar":
                // Checks if what is typed correctly
                if (Partes.GetUpperBound(0) < 1 || string.IsNullOrEmpty(Partes[1]))
                {
                    Console.WriteLine("Use: recarregar 'Desired item");
                    return;
                }

                switch (Partes[1])
                {
                    // Reload the maps
                    case "mapas":
                        Ler.Mapas();
                        Console.WriteLine("Reloaded maps");
                        break;
                    // Recharges classes
                    case "classes":
                        Ler.Classes();
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