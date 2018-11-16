using System;
using System.Windows.Forms;

class Program
{
    // Usado para manter a aplicação aberta
    public static bool Funcionado = true;
    [STAThread]

    public static void Main()
    {
        // Verifica se todos os diretórios existem, se não existirem então criá-los
        Diretórios.Criar();

        // Carrega todos os dados
        Ler.Dados();

        // Inicializa todos os dispositivos
        Gráficos.LerTexturas();
        Áudio.Som.Ler();
        Rede.Iniciar();

        // Abre a janela
        Janela.Objetos.Text = Listas.Opções.Jogo_Nome;
        Janela.Objetos.Visible = true;
        Jogo.AbrirMenu();

        // Inicia a aplicação
        Laço.Principal();
    }

    public static void Sair()
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