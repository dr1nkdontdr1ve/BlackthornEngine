using System;
using System.Windows.Forms;
using System.Threading;

class Program
{
    public static void Main()
    {
        // Abre o servidor e define suas configurações
        Console.Title = "Server";
        Logo();
        Console.WriteLine("[Inicialização]");

        // Verifica se todos os diretórios existem, se não existirem então criá-los
        Diretórios.Criar();
        Console.WriteLine("Diretórios criados.");

        // Limpa e carrega todos os dados necessários
        Ler.Necessário();
        Limpar.Necessário();

        // Cria os dispositivos da rede
        Rede.Iniciar();
        Console.WriteLine("Rede iniciada.");

        // Calcula quanto tempo demorou para inicializar o servidor
        Console.WriteLine("\r\n" + "Servidor iniciado. Digite 'Ajuda' para ver os comandos." + "\r\n");

        // Inicia os laços
        Thread Comandos_Laço = new Thread(Laço.Comandos);
        Comandos_Laço.Start();
        Laço.Principal();
    }

    public static void Logo()
    {
        Console.WriteLine(@"  ______              _____     _
 |   ___|            |     \   | |
 |  |     _ ____   _ |   __/ _ | |_  ___
 |  |    | '__/\\ // |   \_ | || __|/ __|
 |  |___ | |    | |  |     \| || |_ \__ \
 |______||_|    |_|  |_____/|_| \__||___/
                      motor de orpg's 2d" + "\r\n");
    }

    public static void Fechar()
    {
        // Disconeta todos os jogadores e fecha o servidor
        Rede.Dispositivo.Shutdown("O servidor foi desligado.");
        Application.Exit();
    }

    public static void ExecutarComando(string Comando)
    {
        // Previni sobrecargas
        if (string.IsNullOrEmpty(Comando))
            return;

        // Transforma o comando em letras minúsculas
        Comando = Comando.ToLower();

        // Separa os comandos em partes
        string[] Partes = Comando.Split(' ');

        // Executa o determinado comando
        switch (Partes[0].ToLower())
        {
            case "ajuda":
                Console.WriteLine(@"    Lista de comandos disponíveis:
    definiracesso                  - define um nível de acesso para determinado jogador
    cps                            - mostra o atual cps do servidor
    recarregar                     - recarrega determinados dados");
                break;
            case "cps":
                Console.WriteLine("CPS: " + Jogo.CPS);
                break;
            case "definiracesso":
                byte Acesso;

                // Verifica se o que está digitado corretamente
                if (Partes.GetUpperBound(0) < 2 || string.IsNullOrEmpty(Partes[1]) || !Byte.TryParse(Partes[2], out Acesso))
                {
                    Console.WriteLine("Utilize: definiracesso 'Nome do Jogador' 'Acesso'");
                    return;
                }

                // Encontra o jogador
                byte Índice = Jogador.Encontrar(Partes[1]);

                if (Índice == 0)
                {
                    Console.WriteLine("Esse jogador não está conectado ou não existe.");
                    return;
                }

                // Define o acesso do jogador
                Listas.Jogador[Índice].Acesso = (Jogo.Acessos)Convert.ToByte(Partes[2]);

                // Salva os dados
                Escrever.Jogador(Índice);
                Console.WriteLine("Acesso de " + (Jogo.Acessos)Convert.ToByte(Partes[2]) + " concedido a " + Partes[1] + ".");
                break;
            case "recarregar":
                // Verifica se o que está digitado corretamente
                if (Partes.GetUpperBound(0) < 1 || string.IsNullOrEmpty(Partes[1]))
                {
                    Console.WriteLine("Utilize: recarregar 'Item desejado");
                    return;
                }

                switch (Partes[1])
                {
                    // Recarrega os mapas
                    case "mapas":
                        Ler.Mapas();
                        Console.WriteLine("Mapas recarregados");
                        break;
                    // Recarrega as classes
                    case "classes":
                        Ler.Classes();
                        Console.WriteLine("Classes recarregadas");
                        break;
                }
                break;
            // Se o comando não existir mandar uma mensagem de ajuda
            default:
                Console.WriteLine("Esse comando não existe.");
                break;
        }
    }
}