using System;
using System.Windows.Forms;

public partial class Editor_Ferramentas : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Ferramentas Objetos = new Editor_Ferramentas();

    // Índice do item selecionado
    public static byte Selecionado;

    public Editor_Ferramentas()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        // Lê os dados
        Ler.Ferramentas();

        // Define os limites
        Objetos.numX.Maximum = (Globais.Min_Mapa_Largura + 1) * Globais.Grade;
        Objetos.numY.Maximum = (Globais.Min_Mapa_Altura + 1) * Globais.Grade;

        // Adiciona os tipos de ferramentas à lista
        Objetos.cmbFerramentas.Items.Clear();

        for (byte i = 0; i < (byte)Globais.Ferramentas_Tipos.Quantidade; i++)
            Objetos.cmbFerramentas.Items.Add((Globais.Ferramentas_Tipos)i);

        Objetos.cmbFerramentas.SelectedIndex = 0;

        // Abre a janela
        Seleção.Objetos.Visible = false;
        Objetos.Visible = true;
    }

    private void cmbFerramentas_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Atualiza a lista
        Listar();
    }

    #region "Listar"
    private static void Listar_Botões()
    {
        // Adiciona os itens à lista
        for (byte i = 1; i <= Listas.Botão.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Botão.GetUpperBound(0)) + ":" + Listas.Botão[i].Geral.Nome);

        // Abre o painel
        Objetos.panBotão.Visible = true;
    }

    private static void Listar_Digitalizadores()
    {
        // Adiciona os itens à lista
        for (byte i = 1; i <= Listas.Digitalizador.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Digitalizador.GetUpperBound(0)) + ":" + Listas.Digitalizador[i].Geral.Nome);

        // Abre o painel
        Objetos.panDigitalizador.Visible = true;
    }

    private static void Listar_Paineis()
    {
        // Adiciona os itens à lista
        for (byte i = 1; i <= Listas.Painel.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Painel.GetUpperBound(0)) + ":" + Listas.Painel[i].Geral.Nome);

        // Abre o painel
        Objetos.panPainel.Visible = true;
    }

    private static void Listar_Marcadores()
    {
        // Adiciona os itens à lista
        for (byte i = 1; i <= Listas.Marcador.GetUpperBound(0); i++)
            Objetos.lstLista.Items.Add(Globais.Numeração(i, Listas.Marcador.GetUpperBound(0)) + ":" + Listas.Marcador[i].Geral.Nome);

        // Abre o painel
        Objetos.panMarcador.Visible = true;
    }
    #endregion

    private static void Listar()
    {
        Selecionado = (byte)(Objetos.lstLista.SelectedIndex + 1);

        // Limpa a lista
        Objetos.lstLista.Items.Clear();

        // Fehca todos os paineis
        Objetos.panDigitalizador.Visible = false;
        Objetos.panBotão.Visible = false;
        Objetos.panPainel.Visible = false;
        Objetos.panMarcador.Visible = false;

        // Lista as ferramentas e suas propriedades
        switch ((Globais.Ferramentas_Tipos)Objetos.cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Listar_Botões(); break;
            case Globais.Ferramentas_Tipos.Digitalizador: Listar_Digitalizadores(); break;
            case Globais.Ferramentas_Tipos.Painel: Listar_Paineis(); break;
            case Globais.Ferramentas_Tipos.Marcador: Listar_Marcadores(); break;
        }

        // Seleciona o primeiro item
        if (Objetos.lstLista.Items.Count != 0) Objetos.lstLista.SelectedIndex = 0;
    }

    #region "Atualizar"
    private void Atualizar_Botão()
    {
        // Lista as propriedades do botão
        txtNome.Text = Listas.Botão[Selecionado].Geral.Nome;
        numX.Value = Listas.Botão[Selecionado].Geral.Posição.X;
        numY.Value = Listas.Botão[Selecionado].Geral.Posição.Y;
        chkVisível.Checked = Listas.Botão[Selecionado].Geral.Visível;
        lblBotão_Textura.Text = "Textura: " + Listas.Botão[Selecionado].Textura;
    }

    private void Atualizar_Digitalizador()
    {
        // Lista as propriedades do digitalizador
        txtNome.Text = Listas.Digitalizador[Selecionado].Geral.Nome;
        numX.Value = Listas.Digitalizador[Selecionado].Geral.Posição.X;
        numY.Value = Listas.Digitalizador[Selecionado].Geral.Posição.Y;
        chkVisível.Checked = Listas.Digitalizador[Selecionado].Geral.Visível;
        scrlDigitalizador_MáxCaracteres.Value = Listas.Digitalizador[Selecionado].Máx_Carácteres;
        scrlDigitalizador_Largura.Value = Listas.Digitalizador[Selecionado].Largura;
    }

    private void Atualizar_Marcador()
    {
        // Lista as propriedades do marcador
        txtNome.Text = Listas.Marcador[Selecionado].Geral.Nome;
        numX.Value = Listas.Marcador[Selecionado].Geral.Posição.X;
        numY.Value = Listas.Marcador[Selecionado].Geral.Posição.Y;
        chkVisível.Checked = Listas.Marcador[Selecionado].Geral.Visível;
        txtMarcador_Texto.Text = Listas.Marcador[Selecionado].Texto;
    }

    private void Atualizar_Painel()
    {
        // Lista as propriedades do painel
        txtNome.Text = Listas.Painel[Selecionado].Geral.Nome;
        numX.Value = Listas.Painel[Selecionado].Geral.Posição.X;
        numY.Value = Listas.Painel[Selecionado].Geral.Posição.Y;
        chkVisível.Checked = Listas.Painel[Selecionado].Geral.Visível;
        lblPainel_Textura.Text = "Textura: " + Listas.Painel[Selecionado].Textura;
    }

    private void Atualizar()
    {
        Selecionado = (byte)(lstLista.SelectedIndex + 1);

        // Previni erros
        if (Selecionado == 0) return;

        // Lista as ferramentas e suas propriedades
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Atualizar_Botão(); break;
            case Globais.Ferramentas_Tipos.Digitalizador: Atualizar_Digitalizador(); break;
            case Globais.Ferramentas_Tipos.Painel: Atualizar_Painel(); break;
            case Globais.Ferramentas_Tipos.Marcador: Atualizar_Marcador(); break;
        }
    }
    #endregion

    private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Atualiza a lista
        Atualizar();
    }

    #region "AlterarQuantidade"
    private static void AlterarQuantidade_Botão()
    {
        byte Quantidade = (byte)Editor_Quantidade.Objetos.numQuantidade.Value;
        byte Antes = (byte)Listas.Botão.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Botão, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Botão(i);
    }

    private static void AlterarQuantidade_Digitalizador()
    {
        byte Quantidade = (byte)Editor_Quantidade.Objetos.numQuantidade.Value;
        byte Antes = (byte)Listas.Digitalizador.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Digitalizador, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Digitalizador(i);
    }

    private static void AlterarQuantidade_Marcador()
    {
        byte Quantidade = (byte)Editor_Quantidade.Objetos.numQuantidade.Value;
        byte Antes = (byte)Listas.Marcador.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Marcador, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Marcador(i);
    }

    private static void AlterarQuantidade_Painel()
    {
        byte Quantidade = (byte)Editor_Quantidade.Objetos.numQuantidade.Value;
        byte Antes = (byte)Listas.Painel.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Painel, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (byte i = (byte)(Antes + 1); i <= Quantidade; i++)
                Limpar.Painel(i);
    }
    #endregion

    public static void AlterarQuantidade()
    {
        // Altera a quantidade de ferramentas
        switch ((Globais.Ferramentas_Tipos)Objetos.cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: AlterarQuantidade_Botão(); break;
            case Globais.Ferramentas_Tipos.Digitalizador: AlterarQuantidade_Digitalizador(); break;
            case Globais.Ferramentas_Tipos.Painel: AlterarQuantidade_Painel(); break;
            case Globais.Ferramentas_Tipos.Marcador: AlterarQuantidade_Marcador(); break;
        }

        Listar();
    }

    private void butQuantidade_Click(object sender, EventArgs e)
    {
        // Abre a janela de alteração de quantidade
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Editor_Quantidade.Abrir(Listas.Botão.GetUpperBound(0)); break;
            case Globais.Ferramentas_Tipos.Digitalizador: Editor_Quantidade.Abrir(Listas.Digitalizador.GetUpperBound(0)); break;
            case Globais.Ferramentas_Tipos.Painel: Editor_Quantidade.Abrir(Listas.Painel.GetUpperBound(0)); break;
            case Globais.Ferramentas_Tipos.Marcador: Editor_Quantidade.Abrir(Listas.Marcador.GetUpperBound(0)); break;
        }
    }

    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Salva a dimensão da estrutura
        Listas.Cliente_Dados.Num_Botões = (byte)Listas.Botão.GetUpperBound(0);
        Listas.Cliente_Dados.Num_Digitalizadores = (byte)Listas.Digitalizador.GetUpperBound(0);
        Listas.Cliente_Dados.Num_Paineis = (byte)Listas.Painel.GetUpperBound(0);
        Listas.Cliente_Dados.Num_Marcadores = (byte)Listas.Marcador.GetUpperBound(0);
        Escrever.Cliente_Dados();
        Escrever.Ferramentas();

        // Volta à janela de seleção
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    private void butLimpar_Click(object sender, EventArgs e)
    {
        // Reseta os valores
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Limpar.Botão(Selecionado); break;
            case Globais.Ferramentas_Tipos.Digitalizador: Limpar.Digitalizador(Selecionado); break;
            case Globais.Ferramentas_Tipos.Painel: Limpar.Painel(Selecionado); break;
            case Globais.Ferramentas_Tipos.Marcador: Limpar.Marcador(Selecionado); break;
        }

        Atualizar();
    }

    private void butCancelar_Click(object sender, EventArgs e)
    {
        // Volta ao menu
        Visible = false;
        Seleção.Objetos.Visible = true;
    }

    #region "Valores"
    private void txtNome_Validated(object sender, EventArgs e)
    {
        // Previni erros
        if (Selecionado == 0) return;

        // Define os valores
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Listas.Botão[Selecionado].Geral.Nome = txtNome.Text; break;
            case Globais.Ferramentas_Tipos.Digitalizador: Listas.Digitalizador[Selecionado].Geral.Nome = txtNome.Text; break;
            case Globais.Ferramentas_Tipos.Marcador: Listas.Marcador[Selecionado].Geral.Nome = txtNome.Text; break;
            case Globais.Ferramentas_Tipos.Painel: Listas.Painel[Selecionado].Geral.Nome = txtNome.Text; break;
        }

        lstLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, lstLista.Items.Count) + ":" + txtNome.Text;
    }

    private void numX_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Listas.Botão[Selecionado].Geral.Posição.X = (short)numX.Value; break;
            case Globais.Ferramentas_Tipos.Digitalizador: Listas.Digitalizador[Selecionado].Geral.Posição.X = (short)numX.Value; break;
            case Globais.Ferramentas_Tipos.Marcador: Listas.Marcador[Selecionado].Geral.Posição.X = (short)numX.Value; break;
            case Globais.Ferramentas_Tipos.Painel: Listas.Painel[Selecionado].Geral.Posição.X = (short)numX.Value; break;
        }
    }

    private void numY_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Listas.Botão[Selecionado].Geral.Posição.Y = (short)numY.Value; break;
            case Globais.Ferramentas_Tipos.Digitalizador: Listas.Digitalizador[Selecionado].Geral.Posição.Y = (short)numY.Value; break;
            case Globais.Ferramentas_Tipos.Marcador: Listas.Marcador[Selecionado].Geral.Posição.Y = (short)numY.Value; break;
            case Globais.Ferramentas_Tipos.Painel: Listas.Painel[Selecionado].Geral.Posição.Y = (short)numY.Value; break;
        }
    }

    private void chkVisível_CheckedChanged(object sender, EventArgs e)
    {
        // Define os valores
        switch ((Globais.Ferramentas_Tipos)cmbFerramentas.SelectedIndex)
        {
            case Globais.Ferramentas_Tipos.Botão: Listas.Botão[Selecionado].Geral.Visível = chkVisível.Checked; break;
            case Globais.Ferramentas_Tipos.Digitalizador: Listas.Digitalizador[Selecionado].Geral.Visível = chkVisível.Checked; break;
            case Globais.Ferramentas_Tipos.Marcador: Listas.Marcador[Selecionado].Geral.Visível = chkVisível.Checked; break;
            case Globais.Ferramentas_Tipos.Painel: Listas.Painel[Selecionado].Geral.Visível = chkVisível.Checked; break;
        }
    }

    private void scrlDigitalizador_MáxCaracteres_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        if (scrlDigitalizador_MáxCaracteres.Value > 0)
            lblDigitalizador_MáxCaracteres.Text = "Máx. de caracteres: " + scrlDigitalizador_MáxCaracteres.Value;
        else
            lblDigitalizador_MáxCaracteres.Text = "Máx. de caracteres: Infinitos";

        Listas.Digitalizador[Selecionado].Máx_Carácteres = (short)scrlDigitalizador_MáxCaracteres.Value;
    }

    private void scrlDigitalizador_Largura_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        lblDigitalizador_Largura.Text = "Largura: " + scrlDigitalizador_Largura.Value;
        Listas.Digitalizador[Selecionado].Largura = (short)scrlDigitalizador_Largura.Value;
    }

    private void chkDigitalizador_Senha_CheckedChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Digitalizador[Selecionado].Senha = chkDigitalizador_Senha.Checked;
    }

    private void txtMarcador_Texto_Validated(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Marcador[Selecionado].Texto = txtMarcador_Texto.Text;
    }

    private void butPainel_Textura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.Painel[Selecionado].Textura = (byte)PréVisualizar.Selecionar(Gráficos.Tex_Painel, Listas.Painel[Selecionado].Textura);
        lblPainel_Textura.Text = "Textura: " + Listas.Painel[Selecionado].Textura;
    }

    private void butBotão_Textura_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        Listas.Botão[Selecionado].Textura = (byte)PréVisualizar.Selecionar(Gráficos.Tex_Botão, Listas.Botão[Selecionado].Textura);
        lblBotão_Textura.Text = "Textura: " + Listas.Botão[Selecionado].Textura;
    }
    #endregion
}