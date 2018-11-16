using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Editor_Mapas_Propriedades : Form
{
    // Usado para acessar os dados da janela
    public static Editor_Mapas_Propriedades Objetos = new Editor_Mapas_Propriedades();

    // Dados temporários
    public static int Selecionado;

    public Editor_Mapas_Propriedades()
    {
        InitializeComponent();
    }

    public static void Abrir()
    {
        Selecionado = Editor_Mapas.Selecionado;

        // Lista todos os valores
        Listar();

        // Limites
        Objetos.numPanorama.Maximum = Gráficos.Tex_Panorama.GetUpperBound(0);
        Objetos.numFumaça_Textura.Maximum = Gráficos.Tex_Fumaça.GetUpperBound(0);
        Objetos.numLargura.Minimum = Globais.Min_Mapa_Largura;
        Objetos.numAltura.Minimum = Globais.Min_Mapa_Altura;
        Objetos.numClima_Intensidade.Maximum = Globais.Máx_Clima_Intensidade;
        Objetos.numLigação_Acima.Maximum = Listas.Servidor_Dados.Num_Mapas;
        Objetos.numLigação_Abaixo.Maximum = Listas.Servidor_Dados.Num_Mapas;
        Objetos.numLigação_Esquerda.Maximum = Listas.Servidor_Dados.Num_Mapas;
        Objetos.numLigação_Direita.Maximum = Listas.Servidor_Dados.Num_Mapas;
        Objetos.numLuzGlobal.Maximum = Gráficos.Tex_Luz.GetUpperBound(0);

        // Define os valores
        Objetos.txtNome.Text = Listas.Mapa[Selecionado].Nome;
        Objetos.numLargura.Value = Listas.Mapa[Selecionado].Largura;
        Objetos.numAltura.Value = Listas.Mapa[Selecionado].Altura;
        Objetos.cmbMoral.SelectedIndex = Listas.Mapa[Selecionado].Moral;
        Objetos.cmbMúsica.SelectedIndex = Listas.Mapa[Selecionado].Música;
        Objetos.numPanorama.Value = Listas.Mapa[Selecionado].Panorama;
        Objetos.numColoração_Vermelho.Value = Color.FromArgb(Listas.Mapa[Selecionado].Coloração).R;
        Objetos.numColoração_Verde.Value = Color.FromArgb(Listas.Mapa[Selecionado].Coloração).G;
        Objetos.numColoração_Azul.Value = Color.FromArgb(Listas.Mapa[Selecionado].Coloração).B;
        Objetos.cmbClima.SelectedIndex = Listas.Mapa[Selecionado].Clima.Tipo;
        Objetos.numClima_Intensidade.Value = Listas.Mapa[Selecionado].Clima.Intensidade;
        Objetos.numFumaça_Textura.Value = Listas.Mapa[Selecionado].Fumaça.Textura;
        Objetos.numFumaça_VelocidadeX.Value = Listas.Mapa[Selecionado].Fumaça.VelocidadeX;
        Objetos.numFumaça_VelocidadeY.Value = Listas.Mapa[Selecionado].Fumaça.VelocidadeY;
        Objetos.numFumaça_Transparência.Value = Listas.Mapa[Selecionado].Fumaça.Transparência;
        Objetos.numLigação_Acima.Value = Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Acima];
        Objetos.numLigação_Abaixo.Value = Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Abaixo];
        Objetos.numLigação_Esquerda.Value = Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Esquerda];
        Objetos.numLigação_Direita.Value = Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Direita];
        Objetos.numLuzGlobal.Value = Listas.Mapa[Selecionado].LuzGlobal;
        Objetos.numIluminação.Value = Listas.Mapa[Selecionado].Iluminação;

        // Abre a janela
        Objetos.ShowDialog();
    }

    private static void Listar()
    {
        // Limpa
        Objetos.cmbMoral.Items.Clear();
        Objetos.cmbClima.Items.Clear();
        Objetos.cmbMúsica.Items.Clear();
        Objetos.cmbMúsica.Items.Add("Nenhuma");

        // Lista os valores
        for (byte i = 0; i <= (byte)Globais.Mapa_Morais.Quantidade - 1; i++)
            Objetos.cmbMoral.Items.Add((Globais.Mapa_Morais)i);
        for (byte i = 0; i <= (byte)Globais.Climas.Quantidade - 1; i++)
            Objetos.cmbClima.Items.Add((Globais.Climas)i);
        for (byte i = 1; i <= (byte)Áudio.Músicas.Quantidade - 1; i++)
            Objetos.cmbMúsica.Items.Add((Áudio.Músicas)i);
    }

    private void Redimensionar()
    {
        byte Largura_Nova = (byte)numLargura.Value, Altura_Nova = (byte)numAltura.Value;
        int Largura_Diferença, Altura_Diferença;

        // Somente se necessário
        if (Listas.Mapa[Selecionado].Largura == Largura_Nova && Listas.Mapa[Selecionado].Altura == Altura_Nova) return;

        // Redimensiona os azulejos
        Listas.Estruturas.Azulejo_Dados[,] TempAzulejo;
        Listas.Estruturas.Mapas_Azulejo_Dados[,] TempAzulejo2;

        // Calcula a diferença
        Largura_Diferença = Largura_Nova - Listas.Mapa[Selecionado].Largura;
        Altura_Diferença = Altura_Nova - Listas.Mapa[Selecionado].Altura;

        // Azulejo1
        for (byte c = 0; c <= Listas.Mapa[Selecionado].Camada.Count - 1; c++)
        {
            TempAzulejo = new Listas.Estruturas.Azulejo_Dados[Largura_Nova + 1, Altura_Nova + 1];

            for (byte x = 0; x <= Largura_Nova; x++)
                for (byte y = 0; y <= Altura_Nova; y++)
                {
                    // Redimensiona para frente
                    if (!chkInvesamente.Checked)
                        if (x <= Listas.Mapa[Selecionado].Largura && y <= Listas.Mapa[Selecionado].Altura)
                            TempAzulejo[x, y] = Listas.Mapa[Selecionado].Camada[c].Azulejo[x, y];
                        else
                        {
                            TempAzulejo[x, y] = new Listas.Estruturas.Azulejo_Dados();
                            TempAzulejo[x, y].Mini = new Point[4];
                        }
                    // Redimensiona para trás
                    else
                    {
                        if (x < Largura_Diferença || y < Altura_Diferença)
                        {
                            TempAzulejo[x, y] = new Listas.Estruturas.Azulejo_Dados();
                            TempAzulejo[x, y].Mini = new Point[4];
                        }
                        else
                            TempAzulejo[x, y] = Listas.Mapa[Selecionado].Camada[c].Azulejo[x - Largura_Diferença, y - Altura_Diferença];
                    }
                }

            // Define os dados
            Listas.Mapa[Selecionado].Camada[c].Azulejo = TempAzulejo;
        }

        // Dados do azulejo
        TempAzulejo2 = new Listas.Estruturas.Mapas_Azulejo_Dados[Largura_Nova + 1, Altura_Nova + 1];
        for (byte x = 0; x <= Largura_Nova; x++)
            for (byte y = 0; y <= Altura_Nova; y++)
            {
                // Redimensiona para frente
                if (!chkInvesamente.Checked)
                    if (x <= Listas.Mapa[Selecionado].Largura && y <= Listas.Mapa[Selecionado].Altura)
                        TempAzulejo2[x, y] = Listas.Mapa[Selecionado].Azulejo[x, y];
                    else
                    {
                        TempAzulejo2[x, y] = new Listas.Estruturas.Mapas_Azulejo_Dados();
                        TempAzulejo2[x, y].Bloqueio = new bool[4];
                    }
                // Redimensiona para trás
                else
                {
                    if (x < Largura_Diferença || y < Altura_Diferença)
                    {
                        TempAzulejo2[x, y] = new Listas.Estruturas.Mapas_Azulejo_Dados();
                        TempAzulejo2[x, y].Bloqueio = new bool[4];
                    }
                    else
                        TempAzulejo2[x, y] = Listas.Mapa[Selecionado].Azulejo[x - Largura_Diferença, y - Altura_Diferença];
                }
            }

        // Define os dados
        Listas.Mapa[Selecionado].Azulejo = TempAzulejo2;
    }

    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Redimensiona os azulejos
        Redimensionar();

        // Salva os valores
        Listas.Mapa[Selecionado].Nome = txtNome.Text;
        Listas.Mapa[Selecionado].Largura = (byte)numLargura.Value;
        Listas.Mapa[Selecionado].Altura = (byte)numAltura.Value;
        Listas.Mapa[Selecionado].Moral = (byte)Objetos.cmbMoral.SelectedIndex;
        Listas.Mapa[Selecionado].Música = (byte)Objetos.cmbMúsica.SelectedIndex;
        Listas.Mapa[Selecionado].Panorama = (byte)Objetos.numPanorama.Value;
        Listas.Mapa[Selecionado].Clima.Tipo = (byte)Objetos.cmbClima.SelectedIndex;
        Listas.Mapa[Selecionado].Clima.Intensidade = (byte)Objetos.numClima_Intensidade.Value;
        Listas.Mapa[Selecionado].Fumaça.Textura = (byte)Objetos.numFumaça_Textura.Value;
        Listas.Mapa[Selecionado].Fumaça.VelocidadeX = (sbyte)Objetos.numFumaça_VelocidadeX.Value;
        Listas.Mapa[Selecionado].Fumaça.VelocidadeY = (sbyte)Objetos.numFumaça_VelocidadeY.Value;
        Listas.Mapa[Selecionado].Fumaça.Transparência = (byte)Objetos.numFumaça_Transparência.Value;
        Listas.Mapa[Selecionado].Coloração = Color.FromArgb((byte)Objetos.numColoração_Vermelho.Value, (int)Objetos.numColoração_Verde.Value, (int)Objetos.numColoração_Azul.Value).ToArgb();
        Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Acima] = (short)Objetos.numLigação_Acima.Value;
        Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Abaixo] = (short)Objetos.numLigação_Abaixo.Value;
        Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Esquerda] = (short)Objetos.numLigação_Esquerda.Value;
        Listas.Mapa[Selecionado].Ligação[(byte)Globais.Direções.Direita] = (short)Objetos.numLigação_Direita.Value;
        Listas.Mapa[Selecionado].LuzGlobal = (byte)Objetos.numLuzGlobal.Value;
        Listas.Mapa[Selecionado].Iluminação = (byte)Objetos.numIluminação.Value;

        // Define a nova dimensão dos azulejos
        Editor_Mapas.Atualizar();

        // Altera o nome na lista
        Editor_Mapas.Objetos.cmbLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, Editor_Mapas.Objetos.cmbLista.Items.Count) + ":" + txtNome.Text;

        // Reseta os valores
        Globais.Redimensionar_Clima();
        Editor_Mapas.Objetos.numIluminação.Value = Listas.Mapa[Selecionado].Iluminação;
        Editor_Mapas.Objetos.numLuzGlobal.Value = Listas.Mapa[Selecionado].LuzGlobal;

        // Volta ao editor de mapas
        Visible = false;
        Editor_Mapas.Objetos.Enabled = true;
        Editor_Mapas.Objetos.Visible = true;
    }

    private void butCancelar_Click(object sender, EventArgs e)
    {
        // Se estiver tocando uma música, para-la
        Áudio.Música.Parar();

        // Volta ao editor
        Visible = false;
    }

    private void butMúsica_Ouvir_Click(object sender, EventArgs e)
    {
        // Reproduz a música
        Áudio.Música.Reproduzir((Áudio.Músicas)cmbMúsica.SelectedIndex);
    }

    private void butMúsica_Parar_Click(object sender, EventArgs e)
    {
        // Para a música
        Áudio.Música.Parar();
    }

    private void butLuzGlobal_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        numLuzGlobal.Value = PréVisualizar.Selecionar(Gráficos.Tex_Luz, (short)numLuzGlobal.Value);
    }

    private void butPanorama_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        numPanorama.Value = PréVisualizar.Selecionar(Gráficos.Tex_Panorama, (short)numPanorama.Value);
    }

    private void butFumaça_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        numFumaça_Textura.Value = PréVisualizar.Selecionar(Gráficos.Tex_Fumaça, (short)numFumaça_Textura.Value);
    }
}