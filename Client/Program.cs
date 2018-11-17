using System;
using System.Windows.Forms;

class Program
{
    // Usado para manter a aplicação aberta
    public static bool Functional = true;
    [STAThread]

    public static void Main()
    {
        // Verifica se todos os diretórios existem, se não existirem então criá-los
        Directories.Create();

        // Carrega todos os Data
        Read.Data();

        // Inicializa todos os dispositivos
        Gráficos.LerTexturas();
        Áudio.Som.Ler();
        Rede.Iniciar();

        // Abre a janela
        Window.Objects.Text = Lists.Opções.Game_Name;
        Window.Objects.Visible = true;
        Game.AbrirMenu();

        // Inicia a aplicação
        Tie.Principal();
    }

    public static void Leave()
    {
        int Espera = Environment.TickCount;

        // Elimina todos os dispositivos que estão sendo usados
        Rede.Desconectar();

        // Espera até que o jogador seja desconectado
        while (Rede.EstáConectado())
            Application.DoEvents();

        // Fecha a aplicação
        Application.Exit();
    }
}