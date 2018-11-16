using System;
using System.Drawing;
using System.Windows.Forms;

class Laço
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

        while (Aplicação.Funcionado)
        {
            Contagem = Environment.TickCount;

            // Manuseia os dados recebidos
            Rede.ReceberDados();

            // Apresenta os gráficos à tela
            Gráficos.Apresentar();

            // Eventos
            Digitalizador();
            Mapa.Lógica();

            if (Jogador.MeuÍndice > 0 && Ferramentas.JanelaAtual == Ferramentas.Janelas.Jogo)
                if (Contagem_30 < Environment.TickCount)
                {
                    // Lógicas
                    Jogador.Lógica();
                    NPC.Lógica();

                    // Reinicia a contagem
                    Contagem_30 = Environment.TickCount + 30;
                }

            // Faz com que a aplicação se mantenha estável
            Application.DoEvents();
            while (Environment.TickCount < Contagem + 15) System.Threading.Thread.Sleep(1);

            // Cálcula o FPS
            if (Contagem_1000 < Environment.TickCount)
            {
                Enviar.Latência();
                Jogo.FPS = FPS;
                FPS = 0;
                Contagem_1000 = Environment.TickCount + 1000;
            }
            else
                FPS += 1;
        }

        // Fecha o jogo
        Aplicação.Sair();
    }

    private static void Digitalizador()
    {
        // Contagem para a renderização da referência do último texto
        if (Digitalizador_Contagem < Environment.TickCount)
        {
            Digitalizador_Contagem = Environment.TickCount + 500;
            Digitalizadores.Sinal = !Digitalizadores.Sinal;

            // Se necessário foca o digitalizador de novo
            Digitalizadores.Focalizar();
        }

        // Chat
        if (Ferramentas.Linhas_Visível && !Paineis.Encontrar("Chat").Geral.Visível)
        {
            if (Chat_Contagem < Environment.TickCount)
                Ferramentas.Linhas_Visível = false;
        }
        else
            Chat_Contagem = Chat_Contagem = Environment.TickCount + 10000;
    }
}