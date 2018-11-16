using System;
using System.Windows.Forms;

public partial class Editor_NPCs : Form
{
    // Usado para acessar os dados da janela
    public static Editor_NPCs Objetos = new Editor_NPCs();

    // Índice do item selecionado
    public byte Selecionado;

    public Editor_NPCs()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados  e lista os itens
        Ler.NPCs();
        Ler.Itens();

        // Lista de itens
        Objetos.cmbQItem.Items.Clear();
        Objetos.cmbQItem.Items.Add("Nenhum");
        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++) Objetos.cmbQItem.Items.Add(Listas.Item[i].Nome);

        // Define os limites
        Objetos.numTextura.Maximum = Gráficos.Tex_Personagem.GetUpperBound(0);
        Objetos.scrlQueda.Maximum = Globais.Máx_NPC_Queda - 1;
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
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.NPC.GetUpperBound(0)) + ":" + Listas.NPC[i].Nome);

        // Seleciona o primeiro item
        Objetos.lstLista.SelectedIndex = -1;
    }

    private void Atualizar()
    {
        Selecionado = (byte)(lstLista.SelectedIndex + 1);

        // Previni erros
        if (Selecionado == 0) return;

        // Lista os dados
        txtNome.Text = Listas.NPC[Selecionado].Nome;
        numTextura.Value = Listas.NPC[Selecionado].Textura;
        cmbAgressividade.SelectedIndex = Listas.NPC[Selecionado].Agressividade;
        numAparecimento.Value = Listas.NPC[Selecionado].Aparecimento;
        numVisão.Value = Listas.NPC[Selecionado].Visão;
        numExperiência.Value = Listas.NPC[Selecionado].Experiência;
        numVida.Value = Listas.NPC[Selecionado].Vital[(byte)Globais.Vitais.Vida];
        numMana.Value = Listas.NPC[Selecionado].Vital[(byte)Globais.Vitais.Mana];
        numForça.Value = Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Força];
        numResistência.Value = Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Resistência];
        numInteligência.Value = Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Inteligência];
        numAgilidade.Value = Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Agilidade];
        numVitalidade.Value = Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Vitalidade];
        if (cmbQItem.Items.Count > 0) cmbQItem.SelectedIndex = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Item_Num;
        numQQuantidade.Value = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Quantidade;
        numQChance.Value = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Chance;
    }

    public static void AlterarQuantidade()
    {
        int Quantidade = (int)Editor_Quantidade.Objetos.numQuantidade.Value;
        int Antes = Listas.NPC.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.NPC, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.NPC(i);

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
        Listas.Servidor_Dados.Num_NPCs = (byte)Listas.NPC.GetUpperBound(0);
        Escrever.Servidor_Dados();
        Escrever.NPCs();

        // Volta à janela de seleção
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butLimpar_Click(object sender, EventArgs e)
    {
        // Limpa os dados
        Limpar.NPC(Selecionado);

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
        Editor_Quantidade.Abrir(Listas.NPC.GetUpperBound(0));
    }

    private void txtNome_Validated(object sender, EventArgs e)
    {
        // Atualiza a lista
        if (Selecionado > 0)
        {
            Listas.NPC[Selecionado].Nome = txtNome.Text;
            lstLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, lstLista.Items.Count) + ":" + txtNome.Text;
        }
    }

    private void butTextura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.NPC[Selecionado].Textura = PréVisualizar.Selecionar(Gráficos.Tex_Personagem, Listas.NPC[Selecionado].Textura);
        numTextura.Value = Listas.NPC[Selecionado].Textura;
    }

    private void numTextura_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Textura = (byte)numTextura.Value;
    }

    private void numVisão_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Visão = (byte)numVisão.Value;
    }

    private void cmbAgressividade_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Agressividade = (byte)cmbAgressividade.SelectedIndex;
    }

    private void numVida_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Vital[(byte)Globais.Vitais.Vida] = (short)numVida.Value;
    }

    private void numMana_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Vital[(byte)Globais.Vitais.Mana] = (short)numMana.Value;
    }

    private void numForça_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Força] = (short)numForça.Value;
    }

    private void numResistência_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Resistência] = (short)numResistência.Value;
    }

    private void numInteligência_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Inteligência] = (short)numInteligência.Value;
    }

    private void numAgilidade_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Agilidade] = (short)numAgilidade.Value;
    }

    private void numVitalidade_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Atributo[(byte)Globais.Atributos.Vitalidade] = (short)numVitalidade.Value;
    }

    private void numAparecimento_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Aparecimento = (byte)numAparecimento.Value;
    }

    private void numExperiência_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Experiência = (byte)numExperiência.Value;
    }

    private void scrlQueda_ValueChanged(object sender, EventArgs e)
    {
        // Previni erros
        if (Selecionado <= 0) return;

        // Atualiza os valores
        grpQueda.Text = "Queda - " + (scrlQueda.Value + 1);
        cmbQItem.SelectedIndex = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Item_Num;
        numQQuantidade.Value = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Quantidade;
        numQChance.Value = Listas.NPC[Selecionado].Queda[scrlQueda.Value].Chance;
    }

    private void cmbQItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Selecionado > 0) Listas.NPC[Selecionado].Queda[scrlQueda.Value].Item_Num = (short)cmbQItem.SelectedIndex;
    }

    private void numQQuantidade_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Queda[scrlQueda.Value].Quantidade = (short)numQQuantidade.Value;
    }

    private void numQChance_ValueChanged(object sender, EventArgs e)
    {
        Listas.NPC[Selecionado].Queda[scrlQueda.Value].Chance = (byte)numQChance.Value;
    }
    #endregion
}