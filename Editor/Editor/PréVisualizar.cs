using System.Drawing;
using System.Windows.Forms;

public partial class PréVisualizar : Form
{
    // Usado para acessar os dados da janela
    public static PréVisualizar Objetos = new PréVisualizar();

    // Imagens
    public static SFML.Graphics.Texture[] Imagem;
    public static short Padrão;

    public PréVisualizar()
    {
        InitializeComponent();
    }

    public static short Selecionar(SFML.Graphics.Texture[] Texturas, short Selecionado)
    {
        // Previni erros
        if (Texturas == null) return Selecionado;

        // Lista os itens
        Objetos.lstLista.Items.Clear();
        Objetos.lstLista.Items.Add("Nenhuma");
        for (byte i = 1; i <= Texturas.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(i.ToString());

        // Define os dados
        Imagem = Texturas;
        Padrão = Selecionado;
        Objetos.lstLista.SelectedIndex = Selecionado;
        Gráficos.Jan_PréVisualizar = new SFML.Graphics.RenderWindow(Objetos.picImagem.Handle);

        // Abre a janela
        Objetos.ShowDialog();

        // Retorna o valor selecionado
        return (short)Objetos.lstLista.SelectedIndex;
    }

    private void butSelecionar_Click(object sender, System.EventArgs e)
    {
        // Fecha a janela
        Padrão = (short)Objetos.lstLista.SelectedIndex;
        this.Visible = false;
    }

    private void Limitações()
    {
        // Previni erros
        if (lstLista.SelectedIndex > 0)
        {
            Objetos.scrlImagemX.Maximum = 0;
            Objetos.scrlImagemY.Maximum = 0;
        }

        // Dados
        Size Tamanho = Gráficos.TTamanho(Imagem[lstLista.SelectedIndex]);
        int Largura = Tamanho.Width - Objetos.picImagem.Width;
        int Altura = Tamanho.Height - Objetos.picImagem.Height;

        // Verifica se nada passou do limite minímo
        if (Largura < 0) Largura = 0;
        if (Altura < 0) Altura = 0;

        // Define os limites
        Objetos.scrlImagemX.Maximum = Largura;
        Objetos.scrlImagemY.Maximum = Altura;
    }

    private void lstLista_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Limitações();
    }

    private void tmpRenderizar_Tick(object sender, System.EventArgs e)
    {
        // Renderiza as imagens
        Gráficos.PréVisualizar_Imagem();
    }

    private void PréVisualizar_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Define o padrão
        Objetos.lstLista.SelectedIndex = Padrão;
    }
}
