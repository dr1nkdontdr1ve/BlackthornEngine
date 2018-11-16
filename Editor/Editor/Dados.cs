using System.Windows.Forms;

public partial class Editor_Dados : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Dados Objetos = new Editor_Dados();

    public Editor_Dados()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados e lista os itens
        Ler.Servidor_Dados();

        // Define os valores
        Objetos.txtJogo_Nome.Text = Listas.Servidor_Dados.Jogo_Nome;
        Objetos.txtMensagem.Text = Listas.Servidor_Dados.Mensagem;
        Objetos.numPorta.Value = Listas.Servidor_Dados.Porta;
        Objetos.numMáx_Jogadores.Value = Listas.Servidor_Dados.Máx_Jogadores;
        Objetos.numMáx_Personagens.Value = Listas.Servidor_Dados.Máx_Personagens;

        // Abre a janela
        Seleção.Objetos.Visible = false;
        Objetos.Visible = true;
    }

    private void butSalvar_Click(object sender, System.EventArgs e)
    {
        // Salva os dados
        Listas.Servidor_Dados.Jogo_Nome = Objetos.txtJogo_Nome.Text;
        Listas.Servidor_Dados.Mensagem = Objetos.txtMensagem.Text;
        Listas.Servidor_Dados.Porta = (short)Objetos.numPorta.Value;
        Listas.Servidor_Dados.Máx_Jogadores = (byte)Objetos.numMáx_Jogadores.Value;
        Listas.Servidor_Dados.Máx_Personagens = (byte)Objetos.numMáx_Personagens.Value;
        Escrever.Servidor_Dados();

        // Volta à janela de seleção
         Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butCancelar_Click(object sender, System.EventArgs e)
    {
        // Volta à janela de seleção
         Visible = false;
        Seleção.Objetos.Visible = true;
    }
}