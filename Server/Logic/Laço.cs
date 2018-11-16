using System;
using System.Windows.Forms;

class Laço
{
    // Usado para manter a application aberta
    public static bool Funcionando = true;

    // Contagens
    public static int Contagem_1000 = 0, Contagem_5000 = 0;
    public static int Contagem_500 = 0;
    public static int Contagem_Jogador_Reneração = 0;
    public static int Contagem_NPC_Reneração = 0;
    public static int Contagem_Mapa_Itens = 0;

    public static void Principal()
    {
        int CPS = 0;

        while (Funcionando)
        {
            // Manuseia os dados recebidos
            Rede.ReceberDados();

            if (Contagem_500 < Environment.TickCount)
            {
                // Lógicas do mapa
                Mapa.Lógica();
                Jogador.Lógica();

                // Reinicia a contagem
                Contagem_500 = Environment.TickCount + 500;
            }

            // Faz com que a aplicação se mantenha estável
            Application.DoEvents();
            if (Jogo.CPS_Travado) System.Threading.Thread.Sleep(1);

            // Calcula o CPS
            if (Contagem_1000 < Environment.TickCount)
            {
                Jogo.CPS = CPS;
                CPS = 0;
                Contagem_1000 = Environment.TickCount + 1000;
            }
            else
                CPS += 1;
        }

        // Fecha a aplicação
        Aplicação.Fechar();
    }

    public static void Comandos()
    {
        // Laço para que seja possível a utilização de comandos pelo console
        while (Funcionando)
        {
            Console.Write("Executar: ");
            Aplicação.ExecutarComando(Console.ReadLine());
        }
    }
}