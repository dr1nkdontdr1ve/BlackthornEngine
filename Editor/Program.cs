using System;
using System.Windows.Forms;

class Program
{
    // Usado para manter a aplicação aberta
    public static bool Funcionado = true;

    [STAThread]
    static void Main()
    {
        // Carrega as opções
        Ler.Opções();

        // Inicia a aplicação
        Seleção.Objetos.Visible = true;
        Application.EnableVisualStyles();
        Laço.Principal();
    }

    public static void Sair()
    {
        // Fecha a aplicação
        //Gráficos.Destruir();
        Application.Exit();
    }
}