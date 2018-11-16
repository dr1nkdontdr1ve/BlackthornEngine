using System;
using System.Windows.Forms;

public partial class Editor_Classes : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Classes Objetos = new Editor_Classes();

    // Índice do item selecionado
    public byte Selecionado;

    public Editor_Classes()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados e lista os itens
        Ler.Classes();
        Listar();

        // Abre a janela
        Seleção.Objetos.Visible = false;
        Objetos.Visible = true;
    }

    private static void Listar()
    {
        // Limpa a lista
        Objetos.lstLista.Items.Clear();

        // Adiciona os itens à lista
        for (byte i = 1; i <= Listas.Classe.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Classe.GetUpperBound(0)) + ":" + Listas.Classe[i].Nome);

        // Seleciona o primeiro item
        Objetos.lstLista.SelectedIndex = 0;
    }

    private void Atualizar()
    {
        Selecionado = (byte)(lstLista.SelectedIndex + 1);

        // Previni erros
        if (Selecionado == 0) return;

        // Lista os dados
        txtNome.Text = Listas.Classe[Selecionado].Nome;
        lblMTextura.Text = "Masculina: " + Listas.Classe[Selecionado].Textura_Masculina;
        lblFTextura.Text = "Feminina: " + Listas.Classe[Selecionado].Textura_Feminina;
        numVida.Value = Listas.Classe[Selecionado].Vital[(byte)Globais.Vitais.Vida];
        numMana.Value = Listas.Classe[Selecionado].Vital[(byte)Globais.Vitais.Mana];
        numForça.Value = Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Força];
        numResistência.Value = Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Resistência];
        numInteligência.Value = Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Inteligência];
        numAgilidade.Value = Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Agilidade];
        numVitalidade.Value = Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Vitalidade];
        numAparecer_Mapa.Value = Listas.Classe[Selecionado].Aparecer_Mapa;
        cmbAparecer_Direção.SelectedIndex = Listas.Classe[Selecionado].Aparecer_Direção;
        numAparecer_X.Value = Listas.Classe[Selecionado].Aparecer_X;
        numAparecer_Y.Value = Listas.Classe[Selecionado].Aparecer_Y;
    }

    public static void AlterarQuantidade()
    {
        int Quantidade = (int)Editor_Quantidade.Objetos.numQuantidade.Value;
        int Antes = Listas.Classe.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Classe, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Classe(i);

        Listar();
    }

    #region 
    private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Atualiza a lista
        Atualizar();
    }

    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Salva a dimensão da estrutura
        Listas.Servidor_Dados.Num_Classes = (byte)Listas.Classe.GetUpperBound(0);
        Escrever.Servidor_Dados();
        Escrever.Classes();

        // Volta à janela de seleção
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butLimpar_Click(object sender, EventArgs e)
    {
        // Limpa os dados
        Limpar.Classe(Selecionado);

        // Atualiza os valores
        lstLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, lstLista.Items.Count) + ":";
        Atualizar();
    }

    private void butCancelar_Click(object sender, EventArgs e)
    {
        // Volta ao menu
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butQuantidade_Click(object sender, EventArgs e)
    {
        // Abre a janela de alteração
        Editor_Quantidade.Abrir(Listas.Classe.GetUpperBound(0));
    }

    private void txtNome_Validated(object sender, EventArgs e)
    {
        // Atualiza a lista
        if (Selecionado > 0)
        {
            Listas.Classe[Selecionado].Nome = txtNome.Text;
            lstLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, lstLista.Items.Count) + ":" + txtNome.Text;
        }
    }

    private void numVida_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Vital[(byte)Globais.Vitais.Vida] = (short)numVida.Value;
    }

    private void numMana_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Vital[(byte)Globais.Vitais.Mana] = (short)numMana.Value;
    }

    private void numForça_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Força] = (short)numForça.Value;
    }

    private void numResistência_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Resistência] = (short)numResistência.Value;
    }

    private void numInteligência_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Inteligência] = (short)numInteligência.Value;
    }

    private void numAgilidade_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Agilidade] = (short)numAgilidade.Value;
    }

    private void numVitalidade_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Atributo[(byte)Globais.Atributos.Vitalidade] = (short)numVitalidade.Value;
    }

    private void butMTextura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.Classe[Selecionado].Textura_Masculina = PréVisualizar.Selecionar(Gráficos.Tex_Personagem, Listas.Classe[Selecionado].Textura_Masculina);
        lblMTextura.Text = "Masculina: " + Listas.Classe[Selecionado].Textura_Masculina;
    }

    private void butFTextura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.Classe[Selecionado].Textura_Feminina = PréVisualizar.Selecionar(Gráficos.Tex_Personagem, Listas.Classe[Selecionado].Textura_Feminina);
        lblFTextura.Text = "Feminina: " + Listas.Classe[Selecionado].Textura_Feminina;
    }

    private void numAparecer_Mapa_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Aparecer_Mapa = (short)numAparecer_Mapa.Value;
    }

    private void cmbAparecer_Direção_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Aparecer_Direção = (byte)cmbAparecer_Direção.SelectedIndex;
    }

    private void numAparecer_X_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Aparecer_X = (byte)numAparecer_X.Value;
    }

    private void numAparecer_Y_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Classe[Selecionado].Aparecer_Y = (byte)numAparecer_Y.Value;
    }
    #endregion
}