using System;
using System.Windows.Forms;

public partial class Seleção : Form
{
    // Usado para acessar os dados da janela
    public static Seleção Objetos = new Seleção();

    public Seleção()
    {
        InitializeComponent();
    }

    private void Seleção_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Fecha a aplicação
        Program.Funcionado = false;
    }

    private void butSelecionarDiretório_Cliente_Click(object sender, EventArgs e)
    {
        // Seleciona o diretório atual
        Diretório_Cliente.SelectedPath = Listas.Opções.Diretório_Cliente;

        // Apenas se já estiver selecionado um diretório
        if (Diretório_Cliente.ShowDialog() != DialogResult.OK) return;

        // Salva os dados
        Listas.Opções.Diretório_Cliente = Diretório_Cliente.SelectedPath;
        Escrever.Opções();

        // Define e cria os diretórios
        Diretórios.Definir_Cliente();
    }

    private void butSelecionarDiretório_Servidor_Click(object sender, EventArgs e)
    {
        // Seleciona o diretório atual
        Diretório_Servidor.SelectedPath = Listas.Opções.Diretório_Servidor;

        // Apenas se já estiver selecionado um diretório
        if (Diretório_Servidor.ShowDialog() != DialogResult.OK) return;

        // Salva os dados
        Listas.Opções.Diretório_Servidor = Diretório_Servidor.SelectedPath;
        Escrever.Opções();

        // Define e cria os diretórios
        Diretórios.Definir_Servidor();
    }

    private void butDados_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Dados.Abrir();
    }

    private void butFerramentas_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Ferramentas.Abrir();
    }

    private void butClasses_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Classes.Abrir();
    }

    private void butMapas_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Mapas.Abrir();
    }

    private void butAzulejos_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Azulejos.Abrir();
    }

    private void butNPCs_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_NPCs.Abrir();
    }

    private void butItens_Click(object sender, EventArgs e)
    {
        // Verifica se os diretórios foram selecionados
        if (Listas.Opções.Diretório_Servidor == String.Empty || Listas.Opções.Diretório_Cliente == String.Empty)
            MessageBox.Show("Selecione o diretório do servidor e/ou do cliente.");
        else
            Editor_Itens.Abrir();
    }
}