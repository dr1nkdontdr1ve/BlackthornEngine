using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Editor_Azulejos : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Azulejos Objetos = new Editor_Azulejos();

    // Atributo selecionado
    private Globais.Azulejo_Atributos Atributo;

    public Editor_Azulejos()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados e lista os itens
        Ler.Azulejos();

        // Reseta os valores
        Objetos.scrlAzulejo.Value =1;

        // Define os limites
        Objetos.scrlAzulejo.Maximum = Gráficos.Tex_Azulejo.GetUpperBound(0);
        Azulejo_Limitações();

        // Abre a janela
        Seleção.Objetos.Visible = false;
        Objetos.Visible = true;
    }

    private static void Azulejo_Limitações()
    {
        int x = Gráficos.TTamanho(Gráficos.Tex_Azulejo[Objetos.scrlAzulejo.Value]).Width / Globais.Grade - Objetos.picAzulejo.Width / Globais.Grade;
        int y = Gráficos.TTamanho(Gráficos.Tex_Azulejo[Objetos.scrlAzulejo.Value]).Height / Globais.Grade - Objetos.picAzulejo.Height / Globais.Grade;

        // Verifica se nada passou do limite minímo
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        // Define os limites
        Objetos.scrlAzulejoX.Maximum = x;
        Objetos.scrlAzulejoY.Maximum = y;
    }

    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Salva os dados
        Escrever.Azulejos();

        // Volta à janela de seleção
         Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butLimpar_Click(object sender, EventArgs e)
    {
        // Limpa os dados
        Limpar.Azulejo((byte)scrlAzulejo.Value);
    }

    private void butCancelar_Click(object sender, EventArgs e)
    {
        // Volta ao menu
         Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void scrlAzulejo_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        grpAzulejo.Text = "Azulejo: " + scrlAzulejo.Value;
        scrlAzulejoX.Value = 0;
        scrlAzulejoY.Value = 0;
        Azulejo_Limitações();
    }

    private void picAzulejo_MouseDown(object sender, MouseEventArgs e)
    {
        Point Localização = new Point((e.X + scrlAzulejoX.Value * Globais.Grade) / Globais.Grade, (e.Y + scrlAzulejoY.Value * Globais.Grade) / Globais.Grade);
        Point Azulejo_Dif = new Point(e.X - e.X / Globais.Grade * Globais.Grade, e.Y - e.Y / Globais.Grade * Globais.Grade);

        // Previni erros
        if (Localização.X > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(0)) return;
        if (Localização.Y > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(1)) return;

        // Atributos
        if (optAtributos.Checked)
        {
            // Define
            if (e.Button == MouseButtons.Left)
                Listas.Azulejo[scrlAzulejo.Value].Azulejo[Localização.X, Localização.Y].Atributo = (byte)Atributo;
            // Remove
            else if (e.Button == MouseButtons.Right)
                Listas.Azulejo[scrlAzulejo.Value].Azulejo[Localização.X, Localização.Y].Atributo = 0;
        }
        // Bloqueio direcional
        else if (optBloqDirecional.Checked)
        {
            for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                if (Azulejo_Dif.X >= Globais.Bloqueio_Posição(i).X && Azulejo_Dif.X <= Globais.Bloqueio_Posição(i).X + 8)
                    if (Azulejo_Dif.Y >= Globais.Bloqueio_Posição(i).Y && Azulejo_Dif.Y <= Globais.Bloqueio_Posição(i).Y + 8)
                        // Altera o valor de bloqueio
                        Listas.Azulejo[scrlAzulejo.Value].Azulejo[Localização.X, Localização.Y].Bloqueio[i] = !Listas.Azulejo[scrlAzulejo.Value].Azulejo[Localização.X, Localização.Y].Bloqueio[i];
        }

    }

    private void optBloqueio_CheckedChanged(object sender, EventArgs e)
    {
        // Define o atributo
        Atributo = Globais.Azulejo_Atributos.Bloqueio;
    }

    private void optAtributos_CheckedChanged(object sender, EventArgs e)
    {
        // Abre a janela de atributos
        grpAtributos.Visible = true;
    }

    private void optBloqDirecional_CheckedChanged(object sender, EventArgs e)
    {
        // Abre a janela de atributos
        grpAtributos.Visible = false;
    }
}