using System;
using System.Windows.Forms;

class Program
{
    // Usado para manter a aplicação aberta
    public static bool Functional = true;
    [STAThread]

    public static void Main()
    {
        // Verifica se todos os Directories existem, se não existirem então criá-los
        Directories.Create();

        // Carrega todos os Data
        Read.Data();

        // Inicializa todos os Devices
        Graphics.LerTextures();
        Audio.Som.Read();
        Network.Start();

        // Abre a Window
        Window.Objects.Text = Lists.Options.Game_Name;
        Window.Objects.Visible = true;
        Game.OpenMenu();

        // Inicia a aplicação
        Tie.Principal();
    }

    public static void Leave()
    {
        int Espera = Environment.TickCount;

        // Elimina todos os Devices que estão sendo usados
        Network.Disconnect();

        // Espera até que o Player seja desconectado
        while (Network.IsConnected())
            Application.DoEvents();

        // Fecha a aplicação
        Application.Exit();
    }
}