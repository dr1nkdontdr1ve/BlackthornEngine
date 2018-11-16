using System;
using System.Windows.Forms;

public partial class Editor_Quantidade : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Quantidade Objetos = new Editor_Quantidade();

    public Editor_Quantidade()
    {
        InitializeComponent();
    }

    public static void Abrir(int Quantidade)
    {
        // Abre a janela de alteração
        Objetos.numQuantidade.Value = Quantidade;
        Objetos.ShowDialog();
    }

    private void Editor_Quantidade_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Volta ao editor sem salvar as alterações
        Cancelar();
        e.Cancel = true;
    }

    private void butOk_Click(object sender, EventArgs e)
    {
        // Define o nova quantidade
        if (Editor_Classes.Objetos.Visible) Editor_Classes.AlterarQuantidade();
        if (Editor_Mapas.Objetos.Visible) Editor_Mapas.AlterarQuantidade();
        if (Editor_Ferramentas.Objetos.Visible) Editor_Ferramentas.AlterarQuantidade();
        if (Editor_NPCs.Objetos.Visible) Editor_NPCs.AlterarQuantidade();
        if (Editor_Itens.Objetos.Visible) Editor_Itens.AlterarQuantidade();

        // Fecha a janela
        Visible = false;
    }

    private void butCancelar_Click(object sender, EventArgs e)
    {
        // Volta ao editor sem salvar as alterações
        Cancelar();
    }

    private void Cancelar()
    {
        // Define o nova quantidade
        if (Editor_Classes.Objetos.Visible) Editor_Classes.Objetos.Enabled = true;
        if (Editor_Mapas.Objetos.Visible) Editor_Mapas.Objetos.Enabled = true;
        if (Editor_Ferramentas.Objetos.Visible) Editor_Ferramentas.Objetos.Enabled = true;
        if (Editor_NPCs.Objetos.Visible) Editor_NPCs.Objetos.Enabled = true;
        if (Editor_Itens.Objetos.Visible) Editor_Itens.Objetos.Enabled = true;

        // Fecha a janela
        Visible = false;
    }
}