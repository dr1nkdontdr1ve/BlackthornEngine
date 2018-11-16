using System;
using System.Windows.Forms;

public partial class Editor_Itens : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Itens Objetos = new Editor_Itens();

    // Índice do item selecionado
    public byte Selecionado;

    public Editor_Itens()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados
        Ler.Itens();
        Ler.Classes();

        // Lista de classes
        Objetos.cmbReq_Classe.Items.Clear();
        Objetos.cmbReq_Classe.Items.Add("Nenhuma");
        for (byte i = 1; i <= Listas.Classe.GetUpperBound(0); i++) Objetos.cmbReq_Classe.Items.Add(Listas.Classe[i].Nome);

        // Lista os itens
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
        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Item.GetUpperBound(0)) + ":" + Listas.Item[i].Nome);

        // Seleciona o primeiro item
        Objetos.lstLista.SelectedIndex = 0;
    }

    private void Atualizar()
    {
        Selecionado = (byte)(lstLista.SelectedIndex + 1);

        // Previni erros
        if (Selecionado == 0) return;

        // Define os limites
        numTextura.Maximum = Gráficos.Tex_Item.GetUpperBound(0);

        // Lista os dados
        txtNome.Text = Listas.Item[Selecionado].Nome;
        txtDescrição.Text = Listas.Item[Selecionado].Descrição;
        numTextura.Value = Listas.Item[Selecionado].Textura;
        cmbTipo.SelectedIndex = Listas.Item[Selecionado].Tipo;
        numPreço.Value = Listas.Item[Selecionado].Preço;
        chkEmpilhável.Checked = Listas.Item[Selecionado].Empilhável;
        chkNãoDropável.Checked = Listas.Item[Selecionado].NãoDropável;
        numReq_Level.Value = Listas.Item[Selecionado].Req_Level;
        cmbReq_Classe.SelectedIndex = Listas.Item[Selecionado].Req_Classe;
        numPoção_Experiência.Value = Listas.Item[Selecionado].Poção_Experiência;
        numPoção_Vida.Value = Listas.Item[Selecionado].Poção_Vital[(byte)Globais.Vitais.Vida];
        numPoção_Mana.Value = Listas.Item[Selecionado].Poção_Vital[(byte)Globais.Vitais.Mana];
        cmbEquipamento_Tipo.SelectedIndex = Listas.Item[Selecionado].Equip_Tipo;
        numEquip_Força.Value = Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Força];
        numEquip_Resistência.Value = Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Resistência];
        numEquip_Inteligência.Value = Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Inteligência];
        numEquip_Agilidade.Value = Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Agilidade];
        numEquip_Vitalidade.Value = Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Vitalidade];
        numArma_Dano.Value = Listas.Item[Selecionado].Arma_Dano;
    }

    public static void AlterarQuantidade()
    {
        int Quantidade = (int)Editor_Quantidade.Objetos.numQuantidade.Value;
        int Antes = Listas.Item.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Item, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Item(i);

        Listar();
    }

    private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Atualiza a lista
        Atualizar();
    }

    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Salva a dimensão da estrutura
        Listas.Servidor_Dados.Num_Itens = (byte)Listas.Item.GetUpperBound(0);
        Escrever.Servidor_Dados();
        Escrever.Itens();

        // Volta à janela de seleção
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butLimpar_Click(object sender, EventArgs e)
    {
        // Limpa os dados
        Limpar.Item(Selecionado);

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
        Editor_Quantidade.Abrir(Listas.Item.GetUpperBound(0));
    }

    private void txtNome_Validated(object sender, EventArgs e)
    {
        // Atualiza a lista
        if (Selecionado > 0)
        {
            Listas.Item[Selecionado].Nome = txtNome.Text;
            lstLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, lstLista.Items.Count) + ":" + txtNome.Text;
        }
    }

    private void numTextura_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Textura = (short)numTextura.Value;
    }

    private void butTextura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.Item[Selecionado].Textura = PréVisualizar.Selecionar(Gráficos.Tex_Item, Listas.Item[Selecionado].Textura);
        numTextura.Value = Listas.Item[Selecionado].Textura;
    }

    private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Tipo = (byte)cmbTipo.SelectedIndex;

        // Visibilidade dos paineis
        if (cmbTipo.SelectedIndex == (byte)Globais.Itens.Equipamento)
            grpEquipamento.Visible = true;
        else
            grpEquipamento.Visible = false;

        if (cmbTipo.SelectedIndex == (byte)Globais.Itens.Poção)
            grpPoção.Visible = true;
        else
            grpPoção.Visible = false;
    }

    private void numReq_Level_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Req_Level = (short)numReq_Level.Value;
    }

    private void cmbReq_Classe_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Req_Classe = (byte)cmbReq_Classe.SelectedIndex;
    }

    private void numEquip_Vida_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Poção_Vital[(byte)Globais.Vitais.Vida] = (short)numPoção_Vida.Value;
    }

    private void numEquip_Mana_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Poção_Vital[(byte)Globais.Vitais.Mana] = (short)numPoção_Mana.Value;
    }

    private void numEquip_Experiência_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Poção_Experiência = (short)numPoção_Experiência.Value;
    }

    private void numEquip_Força_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Força] = (short)numEquip_Força.Value;
    }

    private void numEquip_Resistência_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Resistência] = (short)numEquip_Resistência.Value;
    }

    private void numEquip_Inteligência_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Inteligência] = (short)numEquip_Inteligência.Value;
    }

    private void numEquip_Agilidade_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Agilidade] = (short)numEquip_Agilidade.Value;
    }

    private void numEquip_Vitalidade_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Atributo[(byte)Globais.Atributos.Vitalidade] = (short)numEquip_Vitalidade.Value;
    }

    private void chkNãoDropável_CheckedChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].NãoDropável = chkNãoDropável.Checked;
    }

    private void numPreço_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Preço = (short)numPreço.Value;
    }

    private void chkEmpilhável_CheckedChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Empilhável = chkEmpilhável.Checked;
    }

    private void numArma_Dano_ValueChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Arma_Dano = (short)numArma_Dano.Value;
    }

    private void cmbEquipamento_Tipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Equip_Tipo = (byte)cmbEquipamento_Tipo.SelectedIndex;

        // Visibilidade dos paineis
        if (cmbEquipamento_Tipo.SelectedIndex == (byte)Globais.Equipamentos.Arma)
            grpArma.Visible = true;
        else
            grpArma.Visible = false;
    }

    private void txtDescrição_Validated(object sender, EventArgs e)
    {
        Listas.Item[Selecionado].Descrição = txtDescrição.Text;
    }
}