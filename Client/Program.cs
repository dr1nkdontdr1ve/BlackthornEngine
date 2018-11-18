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

        // Inicializa todos os Devices
        Gráficos.LerTextures();
        Áudio.Som.Ler();
        Network.Iniciar();

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

        // Elimina todos os Devices que estão sendo usados
        Network.Desconectar();

        // Espera até que o Player seja desconectado
        while (Network.EstáConectado())
            Application.DoEvents();

        // Fecha a aplicação
        Application.Exit();
    }
}