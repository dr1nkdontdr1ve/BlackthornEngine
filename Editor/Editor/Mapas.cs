using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

partial class Editor_Mapas : Form
{
    #region Dados
    // Usado para acessar os dados da janela
    public static Editor_Mapas Objetos = new Editor_Mapas();

    // Índice do item selecionado
    public static short Selecionado;

    // Dados temporários
    private static bool Mapa_Pressionando;

    // Posição do mouse
    public static Point Azulejos_Mouse;
    public static Point Mapa_Mouse;

    // Seleção retângular
    public static Rectangle Def_Azulejos_Seleção = new Rectangle(0, 0, 1, 1);
    public static Rectangle Def_Mapa_Seleção = new Rectangle(0, 0, 1, 1);

    // Dados dos atributos
    private static short ADado_1;
    private static short ADado_2;
    private static short ADado_3;
    private static short ADado_4;

    // Azulejos copiados
    public static Cópia_Estrutura Cópia = new Cópia_Estrutura();

    public struct Cópia_Estrutura
    {
        public Rectangle Área;
        public Listas.Estruturas.Camada[] Dados;

    }
    #endregion

    #region Base
    public Editor_Mapas()
    {
        InitializeComponent();
    }

    private void Editor_Mapas_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Previni erros
        if (!this.Visible) return;

        // Volta ao menu
        e.Cancel = true;
        this.Visible = false;
        global::Seleção.Objetos.Visible = true;
    }

    private void Editor_Mapas_SizeChanged(object sender, EventArgs e)
    {
        // Atualiza os limites
        Mapa_Limitações();
        Azulejo_Limitações();
    }

    public static void Abrir()
    {
        // Lê os dados e lista os itens
        Ler.Mapas();
        Ler.NPCs();
        Ler.Azulejos();
        Ler.Itens();
        Listar();
        ListarCamadas();

        // Limpa as listas
        Objetos.cmbCamadas_Tipo.Items.Clear();
        Objetos.cmbAzulejos.Items.Clear();
        Objetos.cmbNPC.Items.Clear();
        Objetos.cmbA_IItem.Items.Clear();

        // Lista os itens
        for (byte i = 0; i <= (byte)Globais.Camadas.Quantidade - 1; i++)   Objetos.cmbCamadas_Tipo.Items.Add(((Globais.Camadas)i).ToString());
        for (byte i = 1; i <= Gráficos.Tex_Azulejo.GetUpperBound(0); i++)  Objetos.cmbAzulejos.Items.Add(i.ToString());
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)  Objetos.cmbNPC.Items.Add(Listas.NPC[i].Nome);
        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++) Objetos.cmbA_IItem.Items.Add(Listas.Item[i].Nome);

        // Reseta os valores
        Objetos.cmbA_TDireção.SelectedIndex = 0;
        Objetos.cmbNPC.SelectedIndex = -1;
        Objetos.cmbA_IItem.SelectedIndex = 0;
        Objetos.cmbAzulejos.SelectedIndex = -1;
        Objetos.cmbCamadas_Tipo.SelectedIndex = 0;
        Objetos.picAzulejo.BringToFront();
        Objetos.grpZonas.BringToFront();
        Objetos.grpNPCs.BringToFront();
        Objetos.grpAtributos.BringToFront();
        Objetos.grpIluminação.BringToFront();
        Objetos.butMNormal.Checked = true;
        Objetos.butMZonas.Checked = false;
        Objetos.butMNPCs.Checked = false;
        Objetos.butMAtributos.Checked = false;
        Objetos.butMIluminação.Checked = false;
        Objetos.numIluminação.Value = Listas.Mapa[Selecionado].Iluminação;
        Objetos.numLuzGlobal.Maximum = Gráficos.Tex_Luz.GetUpperBound(0);
        Objetos.numLuzGlobal.Value = Listas.Mapa[Selecionado].LuzGlobal;

        // Define os limites
        Objetos.scrlZona.Maximum = Globais.Num_Zonas;
        Objetos.numNPC_Zona.Maximum = Globais.Num_Zonas;
        Azulejo_Limitações();
        Atualizar();

        // Abre a janela
        global::Seleção.Objetos.Visible = false;
        Objetos.Visible = true;
    }

    private static void Listar()
    {
        // Limpa as listas
        Objetos.cmbLista.Items.Clear();
        Objetos.cmbA_TMapa.Items.Clear();

        // Adiciona os itens às listas
        for (byte i = 1; i <= Listas.Mapa.GetUpperBound(0); i++)
        {
            Objetos.cmbLista.Items.Add(Globais.Numeração(i, Listas.Mapa.GetUpperBound(0)) + ":" + Listas.Mapa[i].Nome);
            Objetos.cmbA_TMapa.Items.Add(Globais.Numeração(i, Listas.Mapa.GetUpperBound(0)) + ":" + Listas.Mapa[i].Nome);
        }

        // Seleciona o primeiro item
        Objetos.cmbLista.SelectedIndex = 0;
        Objetos.cmbA_TMapa.SelectedIndex = 0;
    }

    public static void Atualizar()
    {
        Selecionado = (short)(Objetos.cmbLista.SelectedIndex + 1);

        // Reseta o clima
        Globais.Redimensionar_Clima();

        // Faz os cálculos da autocriação
        AutoCriação.Atualizar(Selecionado);

        // Atualiza os dados
        Mapa_Limitações();
        ListarCamadas();
        AtualizarNPCs();
    }

    private void Atualizar_Azulejos_Seleção()
    {
        // Altera o tamanho do azulejo selecionado
        switch (chkAutomático.Checked)
        {
            case false: Def_Azulejos_Seleção.Size = new Size(1, 1); break;
            case true: Def_Azulejos_Seleção.Size = new Size(2, 3); break;
        }
    }

    public static void AlterarQuantidade()
    {
        short Quantidade = (short)Editor_Quantidade.Objetos.numQuantidade.Value;
        short Antes = (short)Listas.Mapa.GetUpperBound(0);

        // Altera a quantidade de itens
        Array.Resize(ref Listas.Mapa, Quantidade + 1);

        // Limpa os novos itens
        if (Quantidade > Antes)
            for (short i = (short)(Antes + 1); i <= Quantidade; i++)
                Limpar.Mapa(i);

        Listar();
    }

    private static void AtualizarBarra()
    {
        // Atualiza as informações da barra
        Objetos.Barra.Items[0].Text = "FPS: " + Globais.FPS;
        Objetos.Barra.Items[2].Text = "Revisão: " + Listas.Mapa[Selecionado].Revisão;
        Objetos.Barra.Items[4].Text = "Posição: {" + Mapa_Mouse.X + ";" + Mapa_Mouse.Y + "}"; ;
    }

    private void cmbLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Atualiza a lista
        Atualizar();
    }

    private static void Azulejo_Limitações()
    {
        Size Tamanho = Gráficos.TTamanho(Gráficos.Tex_Azulejo[Objetos.cmbAzulejos.SelectedIndex + 1]);
        int Largura = Tamanho.Width - Objetos.picAzulejo.Width;
        int Altura = Tamanho.Height - Objetos.picAzulejo.Height;

        // Verifica se nada passou do limite minímo
        if (Largura < 0) Largura = 0;
        if (Altura < 0) Altura = 0;
        if (Largura > 0) Largura += Globais.Grade;
        if (Altura > 0) Altura += Globais.Grade;

        // Define os limites
        Objetos.scrlAzulejoX.Maximum = Largura;
        Objetos.scrlAzulejoY.Maximum = Altura;
    }

    private static void Mapa_Limitações()
    {
        int MáxX = (Listas.Mapa[Selecionado].Largura / Zoom() * Globais.Grade - Objetos.picMapa.Width) / Globais.Grade + 1;
        int MáxY = (Listas.Mapa[Selecionado].Altura / Zoom() * Globais.Grade - Objetos.picMapa.Height) / Globais.Grade + 1;

        // Valor máximo
        if (MáxX > 0) Objetos.scrlMapaX.Maximum = MáxX; else Objetos.scrlMapaX.Maximum = 0;
        if (MáxY > 0) Objetos.scrlMapaY.Maximum = MáxY; else Objetos.scrlMapaY.Maximum = 0;

        // Reseta os valores
        Objetos.scrlMapaX.Value = 0;
        Objetos.scrlMapaY.Value = 0;
    }
    #endregion

    #region Componentes
    private void butPropriedades_Click(object sender, EventArgs e)
    {
        // Para os áudios
        Audio.Som.Parar_Tudo();
        Audio.Music.Parar();

        // Abre as propriedades
        Editor_Mapas_Propriedades.Abrir();
    }

    private void butQuantidade_Click(object sender, EventArgs e)
    {
        // Abre a janela de alteração
        Editor_Quantidade.Abrir(Listas.Mapa.GetUpperBound(0));
    }

    private void chkAutomático_CheckedChanged(object sender, EventArgs e)
    {
        Atualizar_Azulejos_Seleção();
    }

    private void butZona_Limpar_Click(object sender, EventArgs e)
    {
        // Reseta todas as zonas
        for (byte x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (byte y = 0; x <= Listas.Mapa[Selecionado].Altura; y++)
                Listas.Mapa[Selecionado].Azulejo[x, y].Zona = 0;
    }

    private void butLuz_Limpar_Click(object sender, EventArgs e)
    {
        // Reseta todas as zonas
        Listas.Mapa[Selecionado].Luz = new List<Listas.Estruturas.Luz_Estrutura>();
    }

    private void scrlZona_ValueChanged(object sender, EventArgs e)
    {
        // Atualiza os valores
        if (scrlZona.Value == 0)
            grpZonas.Text = "Zona: Nula";
        else
            grpZonas.Text = "Zona: " + scrlZona.Value;
    }

    private void butAtributos_Limpar_Click(object sender, EventArgs e)
    {
        // Reseta todas os atributos
        for (byte x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
            {
                Listas.Mapa[Selecionado].Azulejo[x, y].Atributo = 0;
                Listas.Mapa[Selecionado].Azulejo[x, y].Bloqueio = new bool[(byte)Globais.Direções.Quantidade];
            }
    }

    private void butAtributos_Importar_Click(object sender, EventArgs e)
    {
        // Importa os dados padrões dos azulejos
        for (byte x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
                for (byte c = 0; c <= Listas.Mapa[Selecionado].Camada.Count - 1; c++)
                {
                    // Dados do azulejo
                    Listas.Estruturas.Azulejo_Dados Dados = Listas.Mapa[Selecionado].Camada[c].Azulejo[x, y];

                    if (Dados.Azulejo > 0)
                    {
                        // Atributos
                        if (Listas.Azulejo[Dados.Azulejo].Azulejo[Dados.x, Dados.y].Atributo > 0)
                            Listas.Mapa[Selecionado].Azulejo[x, y].Atributo = Listas.Azulejo[Dados.Azulejo].Azulejo[Dados.x, Dados.y].Atributo;

                        // Bloqueio direcional
                        for (byte b = 0; b <= (byte)Globais.Direções.Quantidade - 1; b++)
                            if (Listas.Azulejo[Dados.Azulejo].Azulejo[Dados.x, Dados.y].Bloqueio[b])
                                Listas.Mapa[Selecionado].Azulejo[x, y].Bloqueio[b] = Listas.Azulejo[Dados.Azulejo].Azulejo[Dados.x, Dados.y].Bloqueio[b];
                    }
                }
    }

    private void numIluminação_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Mapa[Selecionado].Iluminação = (byte)numIluminação.Value;
    }

    private void numLuzGlobal_ValueChanged(object sender, EventArgs e)
    {
        // Define os valores
        Listas.Mapa[Selecionado].LuzGlobal = (byte)numLuzGlobal.Value;
    }

    private void butLuzGlobal_Click(object sender, EventArgs e)
    {
        // Abre a pré visualização
        numLuzGlobal.Value = PréVisualizar.Selecionar(Gráficos.Tex_Luz, (short)numLuzGlobal.Value);
    }
    #endregion

    #region Caixa de Ferramentas
    private void butSalvar_Click(object sender, EventArgs e)
    {
        // Salva a dimensão da estrutura
        Listas.Servidor_Dados.Num_Mapas = (short)Listas.Mapa.GetUpperBound(0);
        Escrever.Servidor_Dados();

        // Salva os dados
        Escrever.Mapa(Selecionado);
    }

    private void butSalvarTudo_Click(object sender, EventArgs e)
    {
        // Salva a dimensão da estrutura
        Listas.Servidor_Dados.Num_Mapas = (short)Listas.Mapa.GetUpperBound(0);
        Escrever.Servidor_Dados();

        // Salva todos os dados
        Escrever.Mapas();
    }

    private void butRecarregar_Click(object sender, EventArgs e)
    {
        // Recarrega o mapa
        Ler.Mapa(Selecionado);
        ListarCamadas();
        AutoCriação.Atualizar(Selecionado);
    }

    private void butExcluir_Click(object sender, EventArgs e)
    {
        // Limpa os dados
        Limpar.Mapa(Selecionado);

        // Atualiza os valores
        cmbLista.Items[Selecionado - 1] = Globais.Numeração(Selecionado, cmbLista.Items.Count) + ":";
    }

    private void Copiar()
    {
        Cópia.Dados = new Listas.Estruturas.Camada[Listas.Mapa[Selecionado].Camada.Count];

        // Seleção
        Cópia.Área = Mapa_Seleção;

        // Copia os dados das camadas
        for (byte c = 0; c <= Cópia.Dados.GetUpperBound(0); c++)
        {
            Cópia.Dados[c] = new Listas.Estruturas.Camada();
            Cópia.Dados[c].Nome = Listas.Mapa[Selecionado].Camada[c].Nome;

            // Tamanho da estrutura
            Cópia.Dados[c].Azulejo = new Listas.Estruturas.Azulejo_Dados[Listas.Mapa[Selecionado].Largura + 1, Listas.Mapa[Selecionado].Altura + 1];

            // Copia os dados dos azulejos
            for (byte x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
                for (byte y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
                    Cópia.Dados[c].Azulejo[x, y] = Listas.Mapa[Selecionado].Camada[c].Azulejo[x, y];
        }
    }

    private void butCopiar_Click(object sender, EventArgs e)
    {
        // Copia os dados
        Copiar();
    }

    private void butRecortar_Click(object sender, EventArgs e)
    {
        // Copia os dados
        Copiar();

        // Remove os azulejos copiados
        for (int x = Mapa_Seleção.X; x <= Mapa_Seleção.X + Mapa_Seleção.Width - 1; x++)
            for (int y = Mapa_Seleção.Y; y <= Mapa_Seleção.Y + Mapa_Seleção.Height - 1; y++)
                for (byte c = 0; c <= Listas.Mapa[Selecionado].Camada.Count - 1; c++)
                {
                    Listas.Mapa[Selecionado].Camada[c].Azulejo[x, y] = new Listas.Estruturas.Azulejo_Dados();
                    Listas.Mapa[Selecionado].Camada[c].Azulejo[x, y].Mini = new Point[4];
                }

        // Atualiza os azulejos automáticos 
        AutoCriação.Atualizar(Selecionado);
    }

    private void butColar_Click(object sender, EventArgs e)
    {
        // Cola os azulejos
        for (int x = Cópia.Área.X; x <= Cópia.Área.X + Cópia.Área.Width - 1; x++)
            for (int y = Cópia.Área.Y; y <= Cópia.Área.Y + Cópia.Área.Height - 1; y++)
                for (byte c = 0; c <= Cópia.Dados.GetUpperBound(0); c++)
                {
                    // Dados
                    int Camada = EncontrarCamada(Cópia.Dados[c].Nome);
                    int x2 = Mapa_Seleção.X + x - Cópia.Área.X;
                    int y2 = y + Mapa_Seleção.Y - Cópia.Área.Y;

                    // Previni erros
                    if (Camada < 0) continue;
                    if (x2 > Listas.Mapa[Selecionado].Largura) continue;
                    if (y2 > Listas.Mapa[Selecionado].Altura) continue;

                    // Cola
                    Listas.Mapa[Selecionado].Camada[Camada].Azulejo[x2, y2] = Cópia.Dados[c].Azulejo[x, y];
                }

        // Atualiza os azulejos automáticos 
        AutoCriação.Atualizar(Selecionado);
    }

    private void butLápis_Click(object sender, EventArgs e)
    {
        // Reseta as outras ferramentas e escolhe essa
        if (butLápis.Checked)
        {
            butRetângulo.Checked = false;
            butÁrea.Checked = false;
            butDescobrir.Checked = false;
        }
        else
            butLápis.Checked = true;

        // Reseta o tamanho da seleção
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butRetângulo_Click(object sender, EventArgs e)
    {
        // Reseta as outras ferramentas e escolhe essa
        if (butRetângulo.Checked)
        {
            butLápis.Checked = false;
            butÁrea.Checked = false;
            butDescobrir.Checked = false;
        }
        else
            butRetângulo.Checked = true;

        // Reseta o tamanho da seleção
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butÁrea_Click(object sender, EventArgs e)
    {
        // Reseta as outras ferramentas e escolhe essa
        if (butÁrea.Checked)
        {
            butRetângulo.Checked = false;
            butLápis.Checked = false;
            butDescobrir.Checked = false;
        }
        else
            butÁrea.Checked = true;

        // Reseta o tamanho da seleção
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butDescobrir_Click(object sender, EventArgs e)
    {
        // Reseta as outras ferramentas e escolhe essa
        if (butDescobrir.Checked)
        {
            butRetângulo.Checked = false;
            butÁrea.Checked = false;
            butLápis.Checked = false;
        }
        else
            butDescobrir.Checked = true;

        // Reseta o tamanho da seleção
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butLata_Click(object sender, EventArgs e)
    {
        // Somente se necessário
        if (lstCamadas.SelectedItems.Count == 0) return;

        // Preenche todos os azulejos iguais ao selecionado com o mesmo azulejo
        for (int x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (int y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
                Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Azulejo[x, y] = Definir_Azulejo();

        // Faz os cálculos da autocriação
        AutoCriação.Atualizar(Selecionado);
    }

    private void butBorracha_Click(object sender, EventArgs e)
    {
        // Somente se necessário
        if (lstCamadas.SelectedItems.Count == 0) return;

        // Preenche todos os azulejos iguais ao selecionado com o mesmo azulejo
        for (int x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (int y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
            {
                Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Azulejo[x, y] = new Listas.Estruturas.Azulejo_Dados();
                Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Azulejo[x, y].Mini = new Point[4];
            }
    }

    private void butEdição_Click(object sender, EventArgs e)
    {
        // Reseta as outras ferramentas e escolhe essa
        if (butEdição.Checked)
            butVisualização.Checked = false;
        else
            butEdição.Checked = true;

        // Salva a preferência
        Listas.Opções.Pre_Mapa_Visualização = butVisualização.Checked;
        Escrever.Opções();
    }

    private void butVisualização_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butVisualização.Checked)
            butEdição.Checked = false;
        else
            butVisualização.Checked = true;

        // Salva a preferência
        Listas.Opções.Pre_Mapa_Visualização = butVisualização.Checked;
        Escrever.Opções();
    }

    private void butGrades_Click(object sender, EventArgs e)
    {
        // Salva a preferência
        Listas.Opções.Pre_Mapa_Grades = butGrades.Checked;
        Escrever.Opções();
    }

    private void butÁudio_Click(object sender, EventArgs e)
    {
        // Salva a preferência
        Listas.Opções.Pre_Mapa_Áudio = butÁudio.Checked;
        Escrever.Opções();

        // Desativa os áudios
        if (!butÁudio.Checked)
        {
            Áudio.Música.Parar();
            Áudio.Som.Parar_Tudo();
        }
    }

    private void butZoom_Normal_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butZoom_Normal.Checked)
        {
            butZoom_2x.Checked = false;
            butZoom_4x.Checked = false;
        }
        else
            butZoom_Normal.Checked = true;

        // Atualiza os limites
        Objetos.picMapa.Image = null;
        Mapa_Limitações();
    }

    private void butZoom_2x_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butZoom_2x.Checked)
        {
            butZoom_Normal.Checked = false;
            butZoom_4x.Checked = false;
        }
        else
            butZoom_2x.Checked = true;

        // Atualiza os limites
        Objetos.picMapa.Image = null;
        Mapa_Limitações();
    }

    private void butZoom_4x_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butZoom_4x.Checked)
        {
            butZoom_Normal.Checked = false;
            butZoom_2x.Checked = false;
        }
        else
            butZoom_4x.Checked = true;

        // Atualiza os limites
        Objetos.picMapa.Image = null;
        Mapa_Limitações();
    }

    private void butMNormal_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butMNormal.Checked)
        {
            butMZonas.Checked = false;
            butMIluminação.Checked = false;
            butMAtributos.Checked = false;
            butMNPCs.Checked = false;
        }
        else
            butMNormal.Checked = true;
    }

    private void butMAtributos_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butMAtributos.Checked)
        {
            butMNormal.Checked = false;
            butMIluminação.Checked = false;
            butMZonas.Checked = false;
            butMNPCs.Checked = false;
        }
        else
            butMAtributos.Checked = true;
    }

    private void butMZonas_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butMZonas.Checked)
        {
            butMNormal.Checked = false;
            butMIluminação.Checked = false;
            butMAtributos.Checked = false;
            butMNPCs.Checked = false;
        }
        else
            butMZonas.Checked = true;
    }

    private void butMIluminação_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butMIluminação.Checked)
        {
            butMZonas.Checked = false;
            butMNormal.Checked = false;
            butMAtributos.Checked = false;
            butMNPCs.Checked = false;
        }
        else
            butMIluminação.Checked = true;
    }

    private void butMNPCs_Click(object sender, EventArgs e)
    {
        // Reseta a marcação
        if (butMNPCs.Checked)
        {
            butMZonas.Checked = false;
            butMNormal.Checked = false;
            butMAtributos.Checked = false;
            butMIluminação.Checked = false;
        }
        else
            butMNPCs.Checked = true;
    }

    private void butMNormal_CheckedChanged(object sender, EventArgs e)
    {
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butMIluminação_CheckedChanged(object sender, EventArgs e)
    {
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void butMZonas_CheckedChanged(object sender, EventArgs e)
    {
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void tmrAtualizar_Tick(object sender, EventArgs e)
    {
        // Apenas se necessário
        if (!Objetos.Visible) return;

        // Ferramentas em geral
        if (butMNormal.Checked)
        {
            grpZonas.Visible = false;
            grpIluminação.Visible = false;
            grpAtributos.Visible = false;
            grpAtributos_Definir.Visible = false;
            grpNPCs.Visible = false;
            butLápis.Enabled = true;
            butRetângulo.Enabled = true;
            butÁrea.Enabled = true;
            butDescobrir.Enabled = true;
            butBorracha.Enabled = true;
            butLata.Enabled = true;
            butGrades.Enabled = true;
            butEdição.Enabled = true;
            butVisualização.Enabled = true;
        }
        else
        {
            butLápis.Enabled = false;
            butRetângulo.Enabled = false;
            butÁrea.Enabled = false;
            butDescobrir.Enabled = false;
            butBorracha.Enabled = false;
            butLata.Enabled = false;
            butGrades.Enabled = false;
            butEdição.Checked = true;
            butVisualização.Checked = false;
            butEdição.Enabled = false;
            butVisualização.Enabled = false;
        }

        // Grupos
        // Iluminação
        if (butMIluminação.Checked)
        {
            grpIluminação.Visible = true;
            grpZonas.Visible = false;
            grpAtributos.Visible = false;
            grpAtributos_Definir.Visible = false;
            grpNPCs.Visible = false;
        }
        // Zonas
        if (butMZonas.Checked)
        {
            grpZonas.Visible = true;
            grpIluminação.Visible = false;
            grpAtributos.Visible = false;
            grpAtributos_Definir.Visible = false;
            grpNPCs.Visible = false;
        }
        // Atributos
        if (butMAtributos.Checked)
        {
            grpAtributos.Visible = true;
            grpZonas.Visible = false;
            grpIluminação.Visible = false;
            grpNPCs.Visible = false;
        }
        // NPCs
        if (butMNPCs.Checked)
        {
            grpNPCs.Visible = true;
            grpZonas.Visible = false;
            grpAtributos.Visible = false;
            grpAtributos_Definir.Visible = false;
            grpIluminação.Visible = false;
        }

        // Ferramentas de recorte e colagem
        if (!butMNormal.Checked || !butMNormal.Enabled || !butÁrea.Checked || !butÁrea.Enabled)
        {
            butColar.Enabled = false;
            butCopiar.Enabled = false;
            butRecortar.Enabled = false;
        }
        else
        {
            butColar.Enabled = true;
            butCopiar.Enabled = true;
            butRecortar.Enabled = true;
        }
        // Sem cópias
        if (Cópia.Dados == null) butColar.Enabled = false;

        // Atualiza os dados da faixa
        AtualizarBarra();
    }
    #endregion

    #region Tela de Azulejos
    private void picAzulejo_MouseWheel(object sender, MouseEventArgs e)
    {
        // Previni erros
        if (Objetos.picAzulejo_Fundo.Size != picAzulejo.Size) return;
        if (scrlAzulejoY.Maximum <= 1) return;

        // Movimenta para baixo
        if (e.Delta > 0)
            if (scrlAzulejoY.Value - Globais.Grade > 0)
                scrlAzulejoY.Value -= Globais.Grade;
            else
                scrlAzulejoY.Value = 0;
        // Movimenta para cima
        else if (e.Delta < 0)
            if (scrlAzulejoY.Value < scrlAzulejoY.Maximum - Globais.Grade)
                scrlAzulejoY.Value += Globais.Grade;
            else
                scrlAzulejoY.Value = scrlAzulejoY.Maximum - Globais.Grade;
    }

    private void picAzulejo_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            // Previni erros
            if (e.X + scrlAzulejoX.Value > Gráficos.TTamanho(Gráficos.Tex_Azulejo[cmbAzulejos.SelectedIndex + 1]).Width) return;
            if (e.Y + scrlAzulejoY.Value > Gráficos.TTamanho(Gráficos.Tex_Azulejo[cmbAzulejos.SelectedIndex + 1]).Height) return;

            // Seleciona o azulejo;
            Def_Azulejos_Seleção.Location = new Point((e.X + scrlAzulejoX.Value) / Globais.Grade, (e.Y + scrlAzulejoY.Value) / Globais.Grade);
            Atualizar_Azulejos_Seleção();
        }
    }

    private void picAzulejo_MouseMove(object sender, MouseEventArgs e)
    {
        int x = (e.X + scrlAzulejoX.Value) / Globais.Grade;
        int y = (e.Y + scrlAzulejoY.Value) / Globais.Grade;
        Size Textura_Tamanho = Gráficos.TTamanho(Gráficos.Tex_Azulejo[cmbAzulejos.SelectedIndex + 1]);

        // Define os valores
        Azulejos_Mouse = new Point(x * Globais.Grade - scrlAzulejoX.Value, y * Globais.Grade - scrlAzulejoY.Value);

        // Somente se necessário
        if (e.Button != MouseButtons.Left) return;
        if (chkAutomático.Checked) return;

        // Verifica se não passou do limite
        if (x < 0) x = 0;
        if (x > Textura_Tamanho.Width / Globais.Grade - 1) x = Textura_Tamanho.Width / Globais.Grade - 1;
        if (y < 0) y = 0;
        if (y > Textura_Tamanho.Height / Globais.Grade - 1) y = Textura_Tamanho.Height / Globais.Grade - 1;

        // Tamanho da grade
        Def_Azulejos_Seleção.Width = x - Def_Azulejos_Seleção.X + 1;
        Def_Azulejos_Seleção.Height = y - Def_Azulejos_Seleção.Y + 1;

        // Altera o tamanho da tela de azulejos
        if (picAzulejo.Width < Textura_Tamanho.Width - scrlAzulejoX.Value) picAzulejo.Width = Textura_Tamanho.Width - scrlAzulejoX.Value;
        if (picAzulejo.Height < Textura_Tamanho.Height - scrlAzulejoY.Value) picAzulejo.Height = Textura_Tamanho.Height - scrlAzulejoY.Value;
    }

    private void picAzulejo_MouseLeave(object sender, EventArgs e)
    {
        // Reseta o tamanho da tela
        picAzulejo.Size = Objetos.picAzulejo_Fundo.Size;
        Azulejo_Limitações();
    }

    private void picAzulejo_MouseUp(object sender, MouseEventArgs e)
    {
        // Reseta o tamanho da tela
        picAzulejo.Size = Objetos.picAzulejo_Fundo.Size;
        Azulejo_Limitações();
    }

    private void cmbAzulejos_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reseta alguns valores
        scrlAzulejoX.Value = 0;
        scrlAzulejoY.Value = 0;
        chkAutomático.Checked = false;
        Azulejo_Limitações();

        // Verifica se a seleção de azulejos passou do limite
        Azulejos_Mouse = new Point(0);
        Def_Azulejos_Seleção = new Rectangle(0, 0, 1, 1);
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }
    #endregion

    #region Tela do Mapa
    private void picMapa_SizeChanged(object sender, EventArgs e)
    {
        // Recria as janelas de acordo com o novo tamanho
        Gráficos.Jan_Mapa.Dispose();
        Gráficos.Jan_Mapa = new SFML.Graphics.RenderWindow(picMapa.Handle);
        Gráficos.Jan_Mapa_Iluminação.Dispose();
        Gráficos.Jan_Mapa_Iluminação = new SFML.Graphics.RenderTexture((uint)picMapa.Width, (uint)picMapa.Height);
    }

    private void picMapa_MouseWheel(object sender, MouseEventArgs e)
    {
        // Movimenta para baixo
        if (e.Delta > 0)
            if (scrlMapaY.Value - 1 > 0)
                scrlMapaY.Value -= 1;
            else
                scrlMapaY.Value = 0;
        // Movimenta para cima
        else if (e.Delta < 0)
            if (scrlMapaY.Value + 1 < scrlMapaY.Maximum)
                scrlMapaY.Value += 1;
            else
                scrlMapaY.Value = scrlMapaY.Maximum;
    }

    private void picMapa_MouseDown(object sender, MouseEventArgs e)
    {
        Point Azulejo_Dif = new Point(e.X - e.X / Globais.Grade * Globais.Grade, e.Y - e.Y / Globais.Grade * Globais.Grade);

        // Previni erros
        if (Selecionado <= 0) return;
        if (Mapa_Seleção.X > Listas.Mapa[Selecionado].Largura || Mapa_Seleção.Y > Listas.Mapa[Selecionado].Altura) return;

        // Executa um evento de acordo com a ferramenta selecionada
        if (butMNormal.Checked)
        {
            Azulejo_Eventos(e.Button);

            // Ferramentas
            if (butÁrea.Checked) Def_Mapa_Seleção = new Rectangle(Mapa_Mouse, new Size(1, 1));
        }
        else if (butMAtributos.Checked && optABloqueioDir.Checked)
        {
            // Define o bloqueio direcional
            for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                if (Azulejo_Dif.X >= Globais.Bloqueio_Posição(i).X && Azulejo_Dif.X <= Globais.Bloqueio_Posição(i).X + 8)
                    if (Azulejo_Dif.Y >= Globais.Bloqueio_Posição(i).Y && Azulejo_Dif.Y <= Globais.Bloqueio_Posição(i).Y + 8)
                        // Altera o valor de bloqueio
                        Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Bloqueio[i] = !Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Bloqueio[i];
        }
        else if (butMAtributos.Checked && !optABloqueioDir.Checked)
            Definir_Atributo(e);
        else if (butMZonas.Checked)
        {
            // Define as zonas
            if (e.Button == MouseButtons.Left)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Zona = (byte)scrlZona.Value;
            else if (e.Button == MouseButtons.Right)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Zona = 0;
        }
        else if (butMIluminação.Checked)
        {
            // Remove as luzes
            if (e.Button == MouseButtons.Right)
                Iluminação_Remover();
        }
        else if (butMNPCs.Checked)
            // Adiciona o NPC
            if (e.Button == MouseButtons.Left)
                AdicionarNPC(true, (byte)Mapa_Seleção.X, (byte)Mapa_Seleção.Y);
    }

    private static void Iluminação_Remover()
    {
        // Encontra a luz que está nessa camada
        if (Listas.Mapa[Selecionado].Luz.Count > 0)
            for (byte i = 0; i <= Listas.Mapa[Selecionado].Luz.Count - 1; i++)
                if (Listas.Mapa[Selecionado].Luz[i].X == Mapa_Seleção.X)
                    if (Listas.Mapa[Selecionado].Luz[i].Y == Mapa_Seleção.Y)
                        Listas.Mapa[Selecionado].Luz.RemoveAt(i);
    }

    private void picMapa_MouseUp(object sender, MouseEventArgs e)
    {
        Mapa_Pressionando = false;

        // Somente se necessário
        if (e.Button != MouseButtons.Left) return;
        if (lstCamadas.SelectedIndices.Count == 0) return;
        if (Mapa_Seleção.X > Listas.Mapa[Selecionado].Largura || Mapa_Seleção.Y > Listas.Mapa[Selecionado].Altura) return;

        // Camada selecionada
        byte Camada = (byte)EncontrarCamada(lstCamadas.SelectedItems[0].SubItems[2].Text);

        // Retângulo
        if (butRetângulo.Checked)
        {
            if (Mapa_Seleção.Width > 1 || Mapa_Seleção.Height > 1)
                // Define os azulejos
                for (int x = Mapa_Seleção.X; x <= Mapa_Seleção.X + Mapa_Seleção.Width - 1; x++)
                    for (int y = Mapa_Seleção.Y; y <= Mapa_Seleção.Y + Mapa_Seleção.Height - 1; y++)
                    {
                        Listas.Mapa[Selecionado].Camada[Camada].Azulejo[x, y] = Definir_Azulejo();
                        AutoCriação.Atualizar(Selecionado, x, y, Camada);
                    }
        }
        // Iluminação
        else if (butMIluminação.Checked)
            Listas.Mapa[Selecionado].Luz.Add(new Listas.Estruturas.Luz_Estrutura(Mapa_Seleção));
        // Nenhum
        else
            return;

        // Reseta o tamanho da seleção
        Def_Mapa_Seleção.Size = new Size(1, 1);
    }

    private void picMapa_MouseMove(object sender, MouseEventArgs e)
    {
        // Mouse
        Mapa_Mouse.X = e.X / Globais.Grade_Zoom + scrlMapaX.Value;
        Mapa_Mouse.Y = e.Y / Globais.Grade_Zoom + scrlMapaY.Value;

        // Impede que saia do limite da tela
        if (Mapa_Mouse.X < 0) Mapa_Mouse.X = 0;
        if (Mapa_Mouse.Y < 0) Mapa_Mouse.Y = 0;
        if (Mapa_Mouse.X > Listas.Mapa[Selecionado].Largura) Mapa_Mouse.X = Listas.Mapa[Selecionado].Largura;
        if (Mapa_Mouse.Y > Listas.Mapa[Selecionado].Altura) Mapa_Mouse.Y = Listas.Mapa[Selecionado].Altura;

        // Previni erros
        if (Selecionado <= 0) return;

        // Cria um retângulo
        if (Mapa_Retângulo(e)) return;

        // Não mover e nem executar nada caso for a ferramenta de área 
        if (butÁrea.Checked && butÁrea.Enabled) return;

        // Define a posição da seleção
        Def_Mapa_Seleção.Location = Mapa_Mouse;

        // Executa um evento de acordo com a ferramenta selecionada
        if (butMNormal.Checked)
            Azulejo_Eventos(e.Button);
        else if (butMZonas.Checked)
        {
            // Define as zonas
            if (e.Button == MouseButtons.Left)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Mouse.X, Mapa_Mouse.Y].Zona = (byte)scrlZona.Value;
            else if (e.Button == MouseButtons.Right)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Zona = 0;
        }
        else if (butMAtributos.Checked && !optABloqueioDir.Checked)
        {
            // Define as zonas
            if (e.Button == MouseButtons.Left)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Mouse.X, Mapa_Mouse.Y].Atributo = (byte)AtributoSelecionado();
            else if (e.Button == MouseButtons.Right)
                Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Atributo = 0;
        }
    }

    private void Azulejo_Eventos(MouseButtons Botão)
    {
        // Previni erros
        if (lstCamadas.SelectedIndices.Count == 0) return;

        // Camada selecionada
        byte Camada = (byte)EncontrarCamada(lstCamadas.SelectedItems[0].SubItems[2].Text);

        // Executa um evento de acordo com a ferramenta selecionada
        if (Botão == MouseButtons.Left)
        {
            if (butLápis.Checked) Azulejo_Definir(Camada);
            if (butDescobrir.Checked) Azulejo_Descobrir(Camada);
        }
        else if (Botão == MouseButtons.Right)
        {
            if (butLápis.Checked) Azulejo_Limpar(Camada);
        }
    }

    private bool Mapa_Retângulo(MouseEventArgs e)
    {
        int x = e.X / Globais.Grade_Zoom + scrlMapaX.Value, y = e.Y / Globais.Grade_Zoom + scrlMapaY.Value;

        // Somente se necessário
        if (e.Button != MouseButtons.Left) return false;
        if (butMIluminação.Checked) goto Continuação;
        if (butRetângulo.Checked && butRetângulo.Enabled) goto Continuação;
        if (butÁrea.Checked && butÁrea.Enabled) goto Continuação;
        return false;
    Continuação:

        // Cria um retângulo
        if (!Mapa_Pressionando) Def_Mapa_Seleção.Size = new Size(1, 1);

        // Verifica se não passou do limite
        if (x < 0) x = 0;
        if (x > Listas.Mapa[Selecionado].Largura) x = Listas.Mapa[Selecionado].Largura;
        if (y < 0) y = 0;
        if (y > Listas.Mapa[Selecionado].Altura) y = Listas.Mapa[Selecionado].Altura;

        // Define o tamanho
        Def_Mapa_Seleção.Width = x - Def_Mapa_Seleção.X + 1;
        Def_Mapa_Seleção.Height = y - Def_Mapa_Seleção.Y + 1;
        Mapa_Pressionando = true;
        return true;
    }

    private void Azulejo_Descobrir(byte Camada_Num)
    {
        Listas.Estruturas.Azulejo_Dados Dados;

        for (int c = Listas.Mapa[Selecionado].Camada.Count - 1; c >= 0; c--)
        {
            Dados = Listas.Mapa[Selecionado].Camada[c].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y];

            // Somente se necessário
            if (!Objetos.lstCamadas.Items[c].Checked) continue;
            if (Dados.Azulejo == 0) continue;

            // Define o azulejo
            cmbAzulejos.SelectedIndex = Dados.Azulejo - 1;
            chkAutomático.Checked = Dados.Automático;
            Def_Azulejos_Seleção = new Rectangle(Dados.x, Dados.y, 1, 1);
            return;
        }
    }

    private Listas.Estruturas.Azulejo_Dados Definir_Azulejo(byte x = 0, byte y = 0)
    {
        Listas.Estruturas.Azulejo_Dados Temp_Azulejo = new Listas.Estruturas.Azulejo_Dados();

        // Posição padrão
        if (x == 0) x = (byte)Azulejos_Seleção.X;
        if (y == 0) y = (byte)Azulejos_Seleção.Y;

        // Define os valores da camada
        Temp_Azulejo.Mini = new Point[4];
        Temp_Azulejo.x = x;
        Temp_Azulejo.y = y;
        Temp_Azulejo.Azulejo = (byte)(cmbAzulejos.SelectedIndex + 1);
        Temp_Azulejo.Automático = chkAutomático.Checked;

        // Retorna o azulejo
        return Temp_Azulejo;
    }

    private void Azulejo_Definir(byte Camada_Num)
    {
        // Define múltiplos azulejos
        if (Azulejos_Seleção.Width > 1 || Azulejos_Seleção.Height > 1)
            Azulejo_Definir_Múltiplos(Camada_Num);

        // Defini um único azulejo
        Listas.Mapa[Selecionado].Camada[Camada_Num].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y] = Definir_Azulejo();
        AutoCriação.Atualizar(Selecionado, Mapa_Seleção.X, Mapa_Seleção.Y, Camada_Num);
    }

    private void Azulejo_Definir_Múltiplos(byte Camada_Num)
    {
        byte x2 = 0, y2;
        Size Tamanho = new Size(Mapa_Seleção.X + Azulejos_Seleção.Width - 1, Mapa_Seleção.Y + Azulejos_Seleção.Height - 1);

        // Apenas se necessário
        if (chkAutomático.Checked) return;

        // Define todos os azulejos selecionados
        for (int x = Mapa_Seleção.X; x <= Tamanho.Width; x++)
        {
            y2 = 0;
            for (int y = Mapa_Seleção.Y; y <= Tamanho.Height; y++)
            {
                // Define os azulejos
                if (!ForaDoLimite(Selecionado, (short)x, (short)y))
                {
                    Listas.Mapa[Selecionado].Camada[Camada_Num].Azulejo[x, y] = Definir_Azulejo((byte)(Azulejos_Seleção.X + x2), (byte)(Azulejos_Seleção.Y + y2));
                    AutoCriação.Atualizar(Selecionado, x, y, Camada_Num);
                }
                y2++;
            }
            x2++;
        }
    }

    private void Azulejo_Limpar(byte Camada_Num)
    {
        // Limpa a camada
        Listas.Mapa[Selecionado].Camada[Camada_Num].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y] = new Listas.Estruturas.Azulejo_Dados();
        Listas.Mapa[Selecionado].Camada[Camada_Num].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Mini = new Point[4];
        AutoCriação.Atualizar(Selecionado, Mapa_Seleção.X, Mapa_Seleção.Y, Camada_Num);
    }
    #endregion

    #region Camadas
    private static void ListarCamadas()
    {
        // Limpa a lista
        Objetos.lstCamadas.Items.Clear();

        // Adiciona os itens à lista
        for (byte i = 0; i <= Listas.Mapa[Selecionado].Camada.Count - 1; i++)
        {
            Objetos.lstCamadas.Items.Add(string.Empty);
            Objetos.lstCamadas.Items[i].Checked = true;
            Objetos.lstCamadas.Items[i].SubItems.Add((i + 1).ToString());
            Objetos.lstCamadas.Items[i].SubItems.Add(Listas.Mapa[Selecionado].Camada[i].Nome);
            Objetos.lstCamadas.Items[i].SubItems.Add(((Globais.Camadas)Listas.Mapa[Selecionado].Camada[i].Tipo).ToString());
        }

        // Seleciona o primeiro item
        Objetos.lstCamadas.Items[0].Selected = true;
    }

    private void butCamada_Adicionar_Click(object sender, EventArgs e)
    {
        Listas.Estruturas.Camada Camada = new Listas.Estruturas.Camada();

        // Verifica se o nome é válido
        if (txtCamada_Nome.Text.Length < 1 || txtCamada_Nome.Text.Length > 12) return;
        if (EncontrarCamada(txtCamada_Nome.Text) >= 0)
        {
            MessageBox.Show("Já existe uma camada com esse nome.");
            return;
        }

        // Define os dados
        Camada.Nome = txtCamada_Nome.Text;
        Camada.Tipo = (byte)cmbCamadas_Tipo.SelectedIndex;
        Camada.Azulejo = new Listas.Estruturas.Azulejo_Dados[Listas.Mapa[Selecionado].Largura + 1, Listas.Mapa[Selecionado].Altura + 1];
        for (byte x = 0; x <= Listas.Mapa[Selecionado].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Selecionado].Altura; y++)
                Camada.Azulejo[x, y].Mini = new Point[4];

        // Adiciona a camada
        Listas.Mapa[Selecionado].Camada.Add(Camada);

        // Atualiza a lista
        AtualizarCamadas();
        grpCamada_Adicionar.Visible = false;
    }

    private void butCamada_Editar_Click(object sender, EventArgs e)
    {
        Listas.Estruturas.Camada Camada = new Listas.Estruturas.Camada();

        // Evita erros
        if (lstCamadas.SelectedItems.Count == 0) return;
        if (txtCamada_Nome.Text.Length < 1 || txtCamada_Nome.Text.Length > 12) return;
        if (lstCamadas.SelectedItems[0].SubItems[2].Text != txtCamada_Nome.Text)
            if (EncontrarCamada(txtCamada_Nome.Text) >= 0)
            {
                MessageBox.Show("Já existe uma camada com esse nome.");
                return;
            }

        // Define os dados
        Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Nome = txtCamada_Nome.Text;
        Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Tipo = (byte)cmbCamadas_Tipo.SelectedIndex;

        // Atualiza a lista
        AtualizarCamadas();
        grpCamada_Adicionar.Visible = false;
    }

    public void AtualizarCamadas()
    {
        List<Listas.Estruturas.Camada> Temp = new List<Listas.Estruturas.Camada>();

        // Reordena as camadas
        for (byte n = 0; n <= (byte)Globais.Camadas.Quantidade - 1; n++)
            for (byte i = 0; i <= Listas.Mapa[Selecionado].Camada.Count - 1; i++)
                if (Listas.Mapa[Selecionado].Camada[i].Tipo == n)
                    Temp.Add(Listas.Mapa[Selecionado].Camada[i]);

        // Atualiza os valores
        Listas.Mapa[Selecionado].Camada = Temp;
        ListarCamadas();
    }

    private void butCamadas_Adicionar_Click(object sender, EventArgs e)
    {
        // Reseta os valores
        txtCamada_Nome.Text = string.Empty;
        cmbCamadas_Tipo.SelectedIndex = 0;

        // Abre a janela em modo de criação
        butCamada_Adicionar.Visible = true;
        butCamada_Editar.Visible = false;
        grpCamada_Adicionar.Text = "Adicionar Camada";
        grpCamada_Adicionar.Visible = true;
    }

    private void butCamadas_Remover_Click(object sender, EventArgs e)
    {
        // Apenas se necessário
        if (Objetos.lstCamadas.Items.Count == 1) return;
        if (lstCamadas.SelectedItems.Count == 0) return;

        // Índice
        int Índice = EncontrarCamada(lstCamadas.SelectedItems[0].SubItems[2].Text);

        // Remove a camada
        if (Índice >= 0)
        {
            Listas.Mapa[Selecionado].Camada.RemoveAt(Índice);
            ListarCamadas();
        }
    }

    public static int EncontrarCamada(string Nome)
    {
        // Encontra a camada
        for (byte i = 0; i <= Listas.Mapa[Selecionado].Camada.Count - 1; i++)
            if (Listas.Mapa[Selecionado].Camada[i].Nome == Nome)
                return i;

        return -1;
    }

    private void butCamada_Cancelar_Click(object sender, EventArgs e)
    {
        // Fecha a janela
        grpCamada_Adicionar.Visible = false;
    }

    private void butCamadas_Acima_Click(object sender, EventArgs e)
    {
        // Somente se necessário
        if (Objetos.lstCamadas.Items.Count == 1) return;
        if (lstCamadas.SelectedItems.Count == 0) return;
        if (lstCamadas.SelectedItems[0].Index == 0) return;

        // Dados
        List<Listas.Estruturas.Camada> Temp = new List<Listas.Estruturas.Camada>(Listas.Mapa[Selecionado].Camada);
        int CamadaNum = lstCamadas.SelectedItems[0].Index;

        if (Temp[CamadaNum - 1].Tipo == Temp[CamadaNum].Tipo)
        {
            // Altera as posições
            Temp[CamadaNum - 1] = Listas.Mapa[Selecionado].Camada[CamadaNum];
            Temp[CamadaNum] = Listas.Mapa[Selecionado].Camada[CamadaNum - 1];
            Listas.Mapa[Selecionado].Camada = Temp;

            // Atualiza a lista
            ListarCamadas();
        }
    }

    private void butCamadas_Abaixo_Click(object sender, EventArgs e)
    {
        // Somente se necessário
        if (Objetos.lstCamadas.Items.Count == 1) return;
        if (lstCamadas.SelectedItems.Count == 0) return;
        if (lstCamadas.SelectedItems[0].Index == lstCamadas.Items.Count - 1) return;

        // Dados
        List<Listas.Estruturas.Camada> Temp = new List<Listas.Estruturas.Camada>(Listas.Mapa[Selecionado].Camada);
        int CamadaNum = lstCamadas.SelectedItems[0].Index;

        if (Temp[CamadaNum + 1].Tipo == Temp[CamadaNum].Tipo)
        {
            // Altera as posições
            Temp[CamadaNum + 1] = Listas.Mapa[Selecionado].Camada[CamadaNum];
            Temp[CamadaNum] = Listas.Mapa[Selecionado].Camada[CamadaNum + 1];
            Listas.Mapa[Selecionado].Camada = Temp;

            // Atualiza a lista
            ListarCamadas();
        }
    }

    private void butCamadas_Editar_Click(object sender, EventArgs e)
    {
        // Previni erros
        if (lstCamadas.SelectedItems.Count == 0) return;

        // Define os valores
        txtCamada_Nome.Text = Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Nome;
        cmbCamadas_Tipo.SelectedIndex = Listas.Mapa[Selecionado].Camada[lstCamadas.SelectedItems[0].Index].Tipo;

        // Abre a janela em modo de edição
        butCamada_Adicionar.Visible = false;
        butCamada_Editar.Visible = true;
        grpCamada_Adicionar.Text = "Editar Camada";
        grpCamada_Adicionar.Visible = true;
    }
    #endregion

    #region Cálculos
    #region Zoom
    // Retângulo da seleção de azulejos
    public static Rectangle Azulejos_Seleção // Somente para obter
    {
        get
        {
            return Retângulo(Def_Azulejos_Seleção);
        }
    }

    // Retângulo do mapa
    public static Rectangle Mapa_Seleção
    {
        get
        {
            if (Objetos.chkAutomático.Checked)
                return new Rectangle(Mapa_Mouse, new Size(1, 1));
            else if (Objetos.butMNormal.Checked && Objetos.butLápis.Checked)
                return new Rectangle(Mapa_Mouse, Azulejos_Seleção.Size);
            else
                return Retângulo(Def_Mapa_Seleção);
        }
    }

    public static byte Zoom()
    {
        // Retorna o valor do zoom
        if (Objetos.butZoom_2x.Checked) return 2;
        else if (Objetos.butZoom_4x.Checked) return 4;
        else return 1;
    }

    public static int Zoom(int Valor)
    {
        // Retorna o valor com o zoom
        return Valor /= Zoom();
    }

    public static Rectangle Zoom(Rectangle Valor)
    {
        // Retorna o valor com o zoom
        return new Rectangle(new Point(Valor.X / Zoom(), Valor.Y / Zoom()), new Size(Valor.Width / Zoom(), Valor.Height / Zoom()));
    }

    public static byte Grade_Zoom
    {
        // Tamanho da grade com o zoom
        get
        {
            return (byte)(Globais.Grade / Zoom());
        }
    }

    public static int Zoom_Grade(int Valor)
    {
        // Tamanho da grade com o zoom
        return Valor * Grade_Zoom;
    }

    public static Point Zoom(int X, int Y)
    {
        // Tamanho da grade com o zoom
        return new Point(X * Grade_Zoom, Y * Grade_Zoom);
    }

    public static Rectangle Zoom_Grade(Rectangle Retângulo)
    {
        // Tamanho da grade com o zoom
        return new Rectangle(Retângulo.X * Grade_Zoom, Retângulo.Y * Grade_Zoom, Retângulo.Width * Grade_Zoom, Retângulo.Height * Grade_Zoom);
    }
    #endregion

    public static Size Mapa_Tamanho
    {
        // Tamanho da tela
        get
        {
            return new Size((Listas.Mapa[Selecionado].Largura + 1) * Globais.Grade, (Listas.Mapa[Selecionado].Altura + 1) * Globais.Grade);
        }
    }

    public static Rectangle Retângulo(Rectangle Temp)
    {
        // Largura
        if (Temp.Width <= 0)
        {
            Temp.X += Temp.Width - 1;
            Temp.Width = (Temp.Width - 2) * -1;
        }
        // Altura
        if (Temp.Height <= 0)
        {
            Temp.Y += Temp.Height - 1;
            Temp.Height = (Temp.Height - 2) * -1;
        }

        // Retorna o valor do retângulo
        return Temp;
    }

    public bool ForaDoLimite(short Mapa, short x, short y)
    {
        // Verifica se as coordenas estão no limite do mapa
        if (x > Listas.Mapa[Mapa].Largura || y > Listas.Mapa[Mapa].Altura || x < 0 || y < 0)
            return true;
        else
            return false;
    }

    public static Size AzulejosVisíveis
    {
        // Quantidade de azulejos visíveis
        get
        {
            return new Size(Listas.Mapa[Selecionado].Largura, Listas.Mapa[Selecionado].Altura);
        }
    }

    public static Rectangle Azulejo_Fonte
    {
        // Retorna com o retângulo do azulejo em relação à fonte
        get
        {
            return new Rectangle(Azulejos_Seleção.X * Globais.Grade, Azulejos_Seleção.Y * Globais.Grade, Azulejos_Seleção.Width * Globais.Grade, Azulejos_Seleção.Height * Globais.Grade);
        }
    }
    #endregion

    class AutoCriação
    {
        // Formas de adicionar o mini azulejo
        public enum Adição
        {
            Nenhum,
            Interior,
            Exterior,
            Horizontal,
            Vertical,
            Preencher
        }

        public static void Atualizar(short Mapa)
        {
            // Atualiza os azulejos necessários
            for (byte x = 0; x <= Listas.Mapa[Mapa].Largura; x++)
                for (byte y = 0; y <= Listas.Mapa[Mapa].Altura; y++)
                    for (byte c = 0; c <= Listas.Mapa[Mapa].Camada.Count - 1; c++)
                        if (Listas.Mapa[Mapa].Camada[c].Azulejo[x, y].Automático)
                            // Faz os cálculos para a autocriação
                            Calcular(Mapa, x, y, c);
        }

        public static void Atualizar(short Mapa, int x, int y, byte Camada_Num)
        {
            // Atualiza os azulejos necessários
            for (int x2 = x - 2; x2 <= x + 2; x2++)
                for (int y2 = y - 2; y2 <= y + 2; y2++)
                    if (x2 >= 0 && x2 <= Listas.Mapa[Mapa].Largura && y2 >= 0 && y2 <= Listas.Mapa[Mapa].Altura)
                        // Faz os cálculos para a autocriação
                        Calcular(Mapa, (byte)x2, (byte)y2, Camada_Num);
        }

        public static void Definir(short Mapa, byte x, byte y, byte Camada_Num, byte Parte, string Letra)
        {
            Point Posição = new Point(0);

            // Posições exatas dos mini azulejos (16x16)
            switch (Letra)
            {
                // Quinas
                case "a": Posição = new Point(32, 0); break;
                case "b": Posição = new Point(48, 0); break;
                case "c": Posição = new Point(32, 16); break;
                case "d": Posição = new Point(48, 16); break;

                // Noroeste
                case "e": Posição = new Point(0, 32); break;
                case "f": Posição = new Point(16, 32); break;
                case "g": Posição = new Point(0, 48); break;
                case "h": Posição = new Point(16, 48); break;

                // Nordeste
                case "i": Posição = new Point(32, 32); break;
                case "j": Posição = new Point(48, 32); break;
                case "k": Posição = new Point(32, 48); break;
                case "l": Posição = new Point(48, 48); break;

                // Sudoeste
                case "m": Posição = new Point(0, 64); break;
                case "n": Posição = new Point(16, 64); break;
                case "o": Posição = new Point(0, 80); break;
                case "p": Posição = new Point(16, 80); break;

                // Sudeste
                case "q": Posição = new Point(32, 64); break;
                case "r": Posição = new Point(48, 64); break;
                case "s": Posição = new Point(32, 80); break;
                case "t": Posição = new Point(48, 80); break;
            }

            // Define a posição do mini azulejo
            Listas.Estruturas.Azulejo_Dados Dados = Listas.Mapa[Mapa].Camada[Camada_Num].Azulejo[x, y];
            Listas.Mapa[Mapa].Camada[Camada_Num].Azulejo[x, y].Mini[Parte].X = Dados.x * Globais.Grade + Posição.X;
            Listas.Mapa[Mapa].Camada[Camada_Num].Azulejo[x, y].Mini[Parte].Y = Dados.y * Globais.Grade + Posição.Y;
        }

        public static bool Verificar(short Mapa, int X1, int Y1, int X2, int Y2, byte Camada_Num)
        {
            Listas.Estruturas.Azulejo_Dados Dados1, Dados2;

            // Somente se necessário
            if (X2 < 0 || X2 > Listas.Mapa[Mapa].Largura || Y2 < 0 || Y2 > Listas.Mapa[Mapa].Altura) return true;

            // Dados
            Dados1 = Listas.Mapa[Mapa].Camada[Camada_Num].Azulejo[X1, Y1];
            Dados2 = Listas.Mapa[Mapa].Camada[Camada_Num].Azulejo[X2, Y2];

            // Verifica se são os mesmo azulejos
            if (!Dados2.Automático) return false;
            if (Dados1.Azulejo != Dados2.Azulejo) return false;
            if (Dados1.x != Dados2.x) return false;
            if (Dados1.y != Dados2.y) return false;

            // Não há nada de errado
            return true;
        }

        public static void Calcular(short Mapa, byte x, byte y, byte Camada_Num)
        {
            // Calcula as quatros partes do azulejo
            Calcular_NO(Mapa, x, y, Camada_Num);
            Calcular_NE(Mapa, x, y, Camada_Num);
            Calcular_SO(Mapa, x, y, Camada_Num);
            Calcular_SE(Mapa, x, y, Camada_Num);
        }

        public static void Calcular_NO(short Mapa, byte x, byte y, byte Camada_Num)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(Mapa, x, y, x - 1, y - 1, Camada_Num)) Azulejo[1] = true;
            if (Verificar(Mapa, x, y, x, y - 1, Camada_Num)) Azulejo[2] = true;
            if (Verificar(Mapa, x, y, x - 1, y, Camada_Num)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[2] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[2] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[2] && !Azulejo[3]) Forma = Adição.Vertical;
            if (!Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(Mapa, x, y, Camada_Num, 0, "e"); break;
                case Adição.Exterior: Definir(Mapa, x, y, Camada_Num, 0, "a"); break;
                case Adição.Horizontal: Definir(Mapa, x, y, Camada_Num, 0, "i"); break;
                case Adição.Vertical: Definir(Mapa, x, y, Camada_Num, 0, "m"); break;
                case Adição.Preencher: Definir(Mapa, x, y, Camada_Num, 0, "q"); break;
            }
        }

        public static void Calcular_NE(short Mapa, byte x, byte y, byte Camada_Num)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Norte, Oeste, Noroeste)
            if (Verificar(Mapa, x, y, x, y - 1, Camada_Num)) Azulejo[1] = true;
            if (Verificar(Mapa, x, y, x + 1, y - 1, Camada_Num)) Azulejo[2] = true;
            if (Verificar(Mapa, x, y, x + 1, y, Camada_Num)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(Mapa, x, y, Camada_Num, 1, "j"); break;
                case Adição.Exterior: Definir(Mapa, x, y, Camada_Num, 1, "b"); break;
                case Adição.Horizontal: Definir(Mapa, x, y, Camada_Num, 1, "f"); break;
                case Adição.Vertical: Definir(Mapa, x, y, Camada_Num, 1, "r"); break;
                case Adição.Preencher: Definir(Mapa, x, y, Camada_Num, 1, "n"); break;
            }
        }

        public static void Calcular_SO(short Mapa, byte x, byte y, byte Camada_Num)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudoeste)
            if (Verificar(Mapa, x, y, x - 1, y, Camada_Num)) Azulejo[1] = true;
            if (Verificar(Mapa, x, y, x - 1, y + 1, Camada_Num)) Azulejo[2] = true;
            if (Verificar(Mapa, x, y, x, y + 1, Camada_Num)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Horizontal;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(Mapa, x, y, Camada_Num, 2, "o"); break;
                case Adição.Exterior: Definir(Mapa, x, y, Camada_Num, 2, "c"); break;
                case Adição.Horizontal: Definir(Mapa, x, y, Camada_Num, 2, "s"); break;
                case Adição.Vertical: Definir(Mapa, x, y, Camada_Num, 2, "g"); break;
                case Adição.Preencher: Definir(Mapa, x, y, Camada_Num, 2, "k"); break;
            }
        }

        public static void Calcular_SE(short Mapa, byte x, byte y, byte Camada_Num)
        {
            bool[] Azulejo = new bool[4];
            Adição Forma = Adição.Nenhum;

            // Verifica se existe algo para modificar nos azulejos em volta (Sul, Oeste, Sudeste)
            if (Verificar(Mapa, x, y, x, y + 1, Camada_Num)) Azulejo[1] = true;
            if (Verificar(Mapa, x, y, x + 1, y + 1, Camada_Num)) Azulejo[2] = true;
            if (Verificar(Mapa, x, y, x + 1, y, Camada_Num)) Azulejo[3] = true;

            // Forma que será adicionado o mini azulejo
            if (!Azulejo[1] && !Azulejo[3]) Forma = Adição.Interior;
            if (!Azulejo[1] && Azulejo[3]) Forma = Adição.Horizontal;
            if (Azulejo[1] && !Azulejo[3]) Forma = Adição.Vertical;
            if (Azulejo[1] && !Azulejo[2] && Azulejo[3]) Forma = Adição.Exterior;
            if (Azulejo[1] && Azulejo[2] && Azulejo[3]) Forma = Adição.Preencher;

            // Define o mini azulejo
            switch (Forma)
            {
                case Adição.Interior: Definir(Mapa, x, y, Camada_Num, 3, "t"); break;
                case Adição.Exterior: Definir(Mapa, x, y, Camada_Num, 3, "d"); break;
                case Adição.Horizontal: Definir(Mapa, x, y, Camada_Num, 3, "p"); break;
                case Adição.Vertical: Definir(Mapa, x, y, Camada_Num, 3, "l"); break;
                case Adição.Preencher: Definir(Mapa, x, y, Camada_Num, 3, "h"); break;
            }
        }
    }

    private void butNPC_Remover_Click(object sender, EventArgs e)
    {
        // Previni erros
        if (Objetos.lstNPC.SelectedIndex < 0) return;

        // Limpa todos os NPCs do mapa
        Listas.Mapa[Selecionado].NPC.RemoveAt(Objetos.lstNPC.SelectedIndex);
        AtualizarNPCs();
    }

    private void butNPC_Limpar_Click(object sender, EventArgs e)
    {
        // Limpa todos os NPCs do mapa
        Listas.Mapa[Selecionado].NPC = new List<Listas.Estruturas.Mapa_NPC>();
        AtualizarNPCs();
    }

    private void butNPC_Adicionar_Click(object sender, EventArgs e)
    {
        // Adiciona o NPC
        AdicionarNPC();
    }

    private static void AdicionarNPC(bool Posição = false, byte X = 0, byte Y = 0)
    {
        Listas.Estruturas.Mapa_NPC Dados = new Listas.Estruturas.Mapa_NPC();

        // Define os dados
        Dados.Índice = (short)(Objetos.cmbNPC.SelectedIndex + 1);
        Dados.Zona = (byte)Objetos.numNPC_Zona.Value;
        Dados.Aparecer = Posição;
        Dados.X = X;
        Dados.Y = Y;

        // Adiciona o NPC
        Listas.Mapa[Selecionado].NPC.Add(Dados);
        AtualizarNPCs();

        // Limpa os valores
        Objetos.cmbNPC.SelectedIndex = 0;
        Objetos.numNPC_Zona.Value = 0;
    }

    private static void AtualizarNPCs()
    {
        // Limpa a lista
        Objetos.lstNPC.Items.Clear();

        // Atualiza a lista de NPCs do mapa
        if (Listas.Mapa[Selecionado].NPC.Count > 0)
        {
            for (byte i = 0; i <= Listas.Mapa[Selecionado].NPC.Count - 1; i++) Objetos.lstNPC.Items.Add(i + 1 + ":" + Listas.NPC[Listas.Mapa[Selecionado].NPC[i].Índice].Nome);
            Objetos.lstNPC.SelectedIndex = 0;
        }
    }

    private static void Definir_Atributo(MouseEventArgs e)
    {
        // Define os Atributos
        if (e.Button == MouseButtons.Left)
        {
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_1 = ADado_1;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_2 = ADado_2;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_3 = ADado_3;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_4 = ADado_4;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Atributo = (byte)AtributoSelecionado();
        }
        // Limpa os dados
        else if (e.Button == MouseButtons.Right)
        {
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_1 = 0;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_2 = 0;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_3 = 0;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Dado_4 = 0;
            Listas.Mapa[Selecionado].Azulejo[Mapa_Seleção.X, Mapa_Seleção.Y].Atributo = 0;
        }
    }

    private static Globais.Azulejo_Atributos AtributoSelecionado()
    {
        // Retorna com o atributo selecionado
        if (Objetos.optABloqueio.Checked) return Globais.Azulejo_Atributos.Bloqueio;
        else if (Objetos.optATeletransporte.Checked) return Globais.Azulejo_Atributos.Teletransporte;
        else if (Objetos.optAItem.Checked) return Globais.Azulejo_Atributos.Item;
        else return Globais.Azulejo_Atributos.Nenhum;
    }

    private void optABloqueioDir_CheckedChanged(object sender, EventArgs e)
    {
        // Define a visibilidade do painel
        if (optABloqueioDir.Checked) grpAtributos_Definir.Visible = false;
    }

    private void optABloqueio_CheckedChanged(object sender, EventArgs e)
    {
        // Define a visibilidade do painel
        if (optABloqueio.Checked) grpAtributos_Definir.Visible = false;
    }

    private void optATeletransporte_CheckedChanged(object sender, EventArgs e)
    {
        // Define a visibilidade do painel
        if (optATeletransporte.Checked)
        {
            grpATeletransporte.Visible = true;
            grpAtributos_Definir.Visible = true;
        }
        else
            grpATeletransporte.Visible = false;
    }

    private void optAItem_CheckedChanged(object sender, EventArgs e)
    {
        // Define a visibilidade do painel
        if (optAItem.Checked)
        {
            grpAItem.Visible = true;
            grpAtributos_Definir.Visible = true;
        }
        else
            grpAItem.Visible = false;
    }

    private void cmbA_TMapa_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Define os limites
        numA_TX.Maximum = Listas.Mapa[cmbA_TMapa.SelectedIndex + 1].Largura;
        numA_TY.Maximum = Listas.Mapa[cmbA_TMapa.SelectedIndex + 1].Altura;
    }

    private void butATeletransporte_Click(object sender, EventArgs e)
    {
        // Fecha a janela e define os dodos
        grpAtributos_Definir.Visible = false;
        ADado_1 = (short)(cmbA_TMapa.SelectedIndex + 1);
        ADado_2 = (short)numA_TX.Value;
        ADado_3 = (short)numA_TY.Value;
        ADado_4 = (short)(cmbA_TDireção.SelectedIndex);

        // Reseta as ferramentas
        cmbA_TMapa.SelectedIndex = 0;
        numA_TX.Value = 0;
        numA_TY.Value = 0;
        cmbA_TDireção.SelectedIndex = 0;
    }

    private void butAItem_Click(object sender, EventArgs e)
    {
        // Fecha a janela e define os dodos
        grpAtributos_Definir.Visible = false;
        ADado_1 = (short)(cmbA_IItem.SelectedIndex + 1);
        ADado_2 = (short)numA_IQuantidade.Value;

        // Reseta as ferramentas
        cmbA_TMapa.SelectedIndex = 0;
        numA_TX.Value = 0;
        numA_TY.Value = 0;
        cmbA_TDireção.SelectedIndex = 0;
    }
}
