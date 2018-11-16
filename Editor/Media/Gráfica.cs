using System;
using System.IO;
using System.Drawing;
using SFML.Graphics;
using SFML.Window;

partial class Gráficos
{
    // Locais de renderização
    public static RenderWindow Jan_PréVisualizar;
    public static RenderWindow Jan_Azulejo = new RenderWindow(Editor_Azulejos.Objetos.picAzulejo.Handle);
    public static RenderWindow Jan_Mapa = new RenderWindow(Editor_Mapas.Objetos.picMapa.Handle);
    public static RenderWindow Jan_Mapa_Azulejo = new RenderWindow(Editor_Mapas.Objetos.picAzulejo.Handle);
    public static RenderTexture Jan_Mapa_Iluminação = new RenderTexture((uint)Editor_Mapas.Objetos.Width, (uint)Editor_Mapas.Objetos.Height);
    public static RenderWindow Jan_Ferramentas;

    // Fonte principal
    public static SFML.Graphics.Font Fonte;

    // Texturas
    public static Texture[] Tex_Personagem;
    public static Texture[] Tex_Azulejo;
    public static Texture[] Tex_Face;
    public static Texture[] Tex_Painel;
    public static Texture[] Tex_Botão;
    public static Texture[] Tex_Panorama;
    public static Texture[] Tex_Fumaça;
    public static Texture[] Tex_Luz;
    public static Texture[] Tex_Item;
    public static Texture Tex_Marcador;
    public static Texture Tex_Digitalizador;
    public static Texture Tex_Grade;
    public static Texture Tex_Clima;
    public static Texture Tex_Preenchido;
    public static Texture Tex_Direções;
    public static Texture Tex_Transparente;
    public static Texture Tex_Iluminação;

    // Formato das texturas
    public const string Formato = ".png";

    #region Motor
    public static Texture[] AdicionarTexturas(string Diretório)
    {
        short i = 1;
        Texture[] TempTex = new Texture[0];

        while (File.Exists(Diretório + i + Formato))
        {
            // Carrega todas do diretório e as adiciona a lista
            Array.Resize(ref TempTex, i + 1);
            TempTex[i] = new Texture(Diretório + i + Formato);
            i += 1;
        }

        // Retorna o cache da textura
        return TempTex;
    }

    public static Size TTamanho(Texture Textura)
    {
        // Retorna com o tamanho da textura
        if (Textura != null)
            return new Size((int)Textura.Size.X, (int)Textura.Size.Y);
        else
            return new Size(0, 0);
    }

    public static SFML.Graphics.Color CCor(byte R = 255, byte G = 255, byte B = 255, byte A = 255)
    {
        // Retorna com a cor
        return new SFML.Graphics.Color(R, G, B, A);
    }

    public static void Desenhar(RenderWindow Alvo, Texture Textura, Rectangle Fonte, Rectangle Destino, object Cor = null, object Modo = null)
    {
        Sprite TmpImage = new Sprite(Textura);

        // Define os dados
        TmpImage.TextureRect = new IntRect(Fonte.X, Fonte.Y, Fonte.Width, Fonte.Height);
        TmpImage.Position = new Vector2f(Destino.X, Destino.Y);
        TmpImage.Scale = new Vector2f(Destino.Width / (float)Fonte.Width, Destino.Height / (float)Fonte.Height);
        if (Cor != null) TmpImage.Color = (SFML.Graphics.Color)Cor;

        // Renderiza a textura em forma de retângulo
        if (Modo == null) Modo = RenderStates.Default;
        Alvo.Draw(TmpImage, (RenderStates)Modo);
    }

    public static void Desenhar(RenderTexture Alvo, Texture Textura, Rectangle Destino, object Cor = null, object Modo = null)
    {
        Sprite TmpImage = new Sprite(Textura);

        // Define os dados
        TmpImage.Position = new Vector2f(Destino.X, Destino.Y);
        TmpImage.Scale = new Vector2f(Destino.Width / (float)TTamanho(Textura).Width, Destino.Height / (float)TTamanho(Textura).Height);
        if (Cor != null) TmpImage.Color = (SFML.Graphics.Color)Cor;

        // Renderiza a textura em forma de retângulo
        if (Modo == null) Modo = RenderStates.Default;
        Alvo.Draw(TmpImage, (RenderStates)Modo);
    }

    public static void Desenhar(RenderWindow Alvo, Texture Textura, int X, int Y, int Fonte_X, int Fonte_Y, int Fonte_Largura, int Fonte_Altura, object Cor = null, object Modo = null)
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(Fonte_X, Fonte_Y), new Size(Fonte_Largura, Fonte_Altura));
        Rectangle Destino = new Rectangle(new Point(X, Y), new Size(Fonte_Largura, Fonte_Altura));

        // Desenha a textura
        Desenhar(Alvo, Textura, Fonte, Destino, Cor, Modo);
    }

    public static void Desenhar(RenderWindow Alvo, Texture Textura, Rectangle Destino, object Cor = null, object Modo = null)
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), TTamanho(Textura));

        // Desenha a textura
        Desenhar(Alvo, Textura, Fonte, Destino, Cor, Modo);
    }

    public static void Desenhar(RenderWindow Alvo, Texture Textura, Point Localização, object Cor = null, object Modo = null)
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), TTamanho(Textura));
        Rectangle Destino = new Rectangle(Localização, TTamanho(Textura));

        // Desenha a textura
        Desenhar(Alvo, Textura, Fonte, Destino, Cor, Modo);
    }

    public static void DesenharRetângulo(RenderWindow Alvo, Rectangle Retângulo, object Cor = null)
    {
        // Desenha a caixa
        Desenhar(Alvo, Tex_Grade, Retângulo.X, Retângulo.Y, 0, 0, Retângulo.Width, 1, Cor);
        Desenhar(Alvo, Tex_Grade, Retângulo.X, Retângulo.Y, 0, 0, 1, Retângulo.Height, Cor);
        Desenhar(Alvo, Tex_Grade, Retângulo.X, Retângulo.Y + Retângulo.Height - 1, 0, 0, Retângulo.Width, 1, Cor);
        Desenhar(Alvo, Tex_Grade, Retângulo.X + Retângulo.Width - 1, Retângulo.Y, 0, 0, 1, Retângulo.Height, Cor);
    }

    public static void DesenharRetângulo(RenderWindow Alvo, int x, int y, int Largura, int Altura, object Cor = null)
    {
        // Desenha a caixa
        DesenharRetângulo(Alvo, new Rectangle(x, y, Largura, Altura), Cor);
    }

    private static void Desenhar(RenderWindow Alvo, string Texto, int X, int Y, SFML.Graphics.Color Cor)
    {
        Text TempTexto = new Text(Texto, Fonte);

        // Define os dados
        TempTexto.CharacterSize = 10;
        TempTexto.Color = Cor;
        TempTexto.Position = new Vector2f(X, Y);

        // Desenha
        Alvo.Draw(TempTexto);
    }
    #endregion

    public static void LerTexturas()
    {
        // Conjuntos
        Tex_Personagem = AdicionarTexturas(Diretórios.Tex_Personagens.FullName);
        Tex_Azulejo = AdicionarTexturas(Diretórios.Tex_Azulejos.FullName);
        Tex_Face = AdicionarTexturas(Diretórios.Tex_Faces.FullName);
        Tex_Painel = AdicionarTexturas(Diretórios.Tex_Paineis.FullName);
        Tex_Botão = AdicionarTexturas(Diretórios.Tex_Botões.FullName);
        Tex_Panorama = AdicionarTexturas(Diretórios.Tex_Panoramas.FullName);
        Tex_Fumaça = AdicionarTexturas(Diretórios.Tex_Fumaças.FullName);
        Tex_Luz = AdicionarTexturas(Diretórios.Tex_Luzes.FullName);
        Tex_Item = AdicionarTexturas(Diretórios.Tex_Itens.FullName);

        // Únicas
        Tex_Clima = new Texture(Diretórios.Tex_Clima.FullName + Formato);
        Tex_Preenchido = new Texture(Diretórios.Tex_Preenchido.FullName + Formato);
        Tex_Direções = new Texture(Diretórios.Tex_Direções.FullName + Formato);
        Tex_Transparente = new Texture(Diretórios.Tex_Transparente.FullName + Formato);
        Tex_Grade = new Texture(Diretórios.Tex_Grade.FullName + Formato);
        Tex_Marcador = new Texture(Diretórios.Tex_Marcador.FullName + Formato);
        Tex_Digitalizador = new Texture(Diretórios.Tex_Digitalizador.FullName + Formato);
        Tex_Iluminação = new Texture(Diretórios.Tex_Iluminação.FullName + Formato);

        // Fontes
        Fonte = new SFML.Graphics.Font(Diretórios.Fontes.FullName + "Georgia.ttf");
    }

    public static void Apresentar()
    {
        // Desenha 
        PréVisualizar_Imagem();
        Editor_Azulejo();
        Editor_Mapas_Azulejo();
        Editor_Mapas_Mapa();
    }

    public static void Transparente(RenderWindow Janela)
    {
        Size Tamanho = TTamanho(Tex_Transparente);

        // Desenha uma textura transparente na janela inteira
        for (int x = 0; x <= Janela.Size.X / Tamanho.Width; x++)
            for (int y = 0; y <= Janela.Size.Y / Tamanho.Height; y++)
                Desenhar(Janela, Tex_Transparente, new Point(Tamanho.Width * x, Tamanho.Height * y));
    }

    #region PréVisualizar
    public static void PréVisualizar_Imagem()
    {
        PréVisualizar Objetos = PréVisualizar.Objetos;

        // Apenas se necessário
        if (!Objetos.Visible) return;
        if (Objetos.lstLista.SelectedIndex < 0) return;

        // Dados
        Texture Textura = PréVisualizar.Imagem[Objetos.lstLista.SelectedIndex];

        // Limpa a área
        Jan_PréVisualizar.Clear();

        // Desenha a textura
        if (Objetos.chkTransparente.Checked) Transparente(Jan_PréVisualizar);
        if (Objetos.lstLista.SelectedIndex > 0) Desenhar(Jan_PréVisualizar, Textura, new Rectangle(new Point(Objetos.scrlImagemX.Value, Objetos.scrlImagemY.Value), TTamanho(Textura)), new Rectangle(new Point(0), TTamanho(Textura)));

        // Exibe o que foi renderizado
        Jan_PréVisualizar.Display();
    }
    #endregion

    #region Editor de Azulejos
    public static void Editor_Azulejo()
    {
        Editor_Azulejos Objetos = Editor_Azulejos.Objetos;

        // Somente se necessário
        if (!Objetos.Visible) return;

        // Limpa a área com um fundo preto
        Jan_Azulejo.Clear(SFML.Graphics.Color.Black);

        // Dados
        Texture Textura = Tex_Azulejo[Objetos.scrlAzulejo.Value];
        Size Tamanho = TTamanho(Textura);
        Point Localização = new Point(Objetos.scrlAzulejoX.Value * Globais.Grade, Objetos.scrlAzulejoY.Value * Globais.Grade);

        // Desenha o azulejo e as grades
        Transparente(Jan_Azulejo);
        Desenhar(Jan_Azulejo, Textura, new Rectangle(Localização, Tamanho), new Rectangle(new Point(0), Tamanho));

        for (byte x = 0; x <= Objetos.picAzulejo.Width / Globais.Grade; x++)
            for (byte y = 0; y <= Objetos.picAzulejo.Height / Globais.Grade; y++)
            {
                // Desenha os atributos
                if (Objetos.optAtributos.Checked)
                    Editor_Azulejo_Atributos(x, y);
                // Bloqueios direcionais
                else if (Objetos.optBloqDirecional.Checked)
                    Editor_Azulejo_BloqDirecional(x, y);

                // Grades
                DesenharRetângulo(Jan_Azulejo, x * Globais.Grade, y * Globais.Grade, Globais.Grade, Globais.Grade, CCor(25, 25, 25, 70));
            }

        // Exibe o que foi renderizado
        Jan_Azulejo.Display();
    }

    public static void Editor_Azulejo_Atributos(byte x, byte y)
    {
        Editor_Azulejos Objetos = Editor_Azulejos.Objetos;
        Point Azulejo = new Point(Objetos.scrlAzulejoX.Value + x, Objetos.scrlAzulejoY.Value + y);
        Point Localização = new Point(x * Globais.Grade + Globais.Grade / 2 - 5, y * Globais.Grade + Globais.Grade / 2 - 6);

        // Previni erros
        if (Azulejo.X > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(0)) return;
        if (Azulejo.Y > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(1)) return;

        // Desenha uma letra e colore o azulejo referente ao atributo
        switch ((Globais.Azulejo_Atributos)Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo[Azulejo.X, Azulejo.Y].Atributo)
        {
            case Globais.Azulejo_Atributos.Bloqueio:
                Desenhar(Jan_Azulejo, Tex_Preenchido, x * Globais.Grade, y * Globais.Grade, 0, 0, Globais.Grade, Globais.Grade, CCor(225, 0, 0, 75));
                Desenhar(Jan_Azulejo, "B", Localização.X, Localização.Y, SFML.Graphics.Color.Red);
                break;
        }
    }

    public static void Editor_Azulejo_BloqDirecional(byte x, byte y)
    {
        Editor_Azulejos Objetos = Editor_Azulejos.Objetos;
        Point Azulejo = new Point(Objetos.scrlAzulejoX.Value + x, Objetos.scrlAzulejoY.Value + y);
        byte Y;

        // Previni erros
        if (Azulejo.X > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(0)) return;
        if (Azulejo.Y > Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo.GetUpperBound(1)) return;

        for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
        {
            // Estado do bloqueio
            if (Listas.Azulejo[Objetos.scrlAzulejo.Value].Azulejo[Azulejo.X, Azulejo.Y].Bloqueio[i])
                Y = 8;
            else
                Y = 0;

            // Renderiza
            Desenhar(Jan_Azulejo, Tex_Direções, x * Globais.Grade + Globais.Bloqueio_Posição(i).X, y * Globais.Grade + Globais.Bloqueio_Posição(i).Y, i * 8, Y, 6, 6);
        }
    }
    #endregion

    #region Editor de Mapas
    public static void Editor_Mapas_Azulejo()
    {
        Editor_Mapas Objetos = Editor_Mapas.Objetos;

        // Somente se necessário
        if (!Objetos.Visible || !Editor_Mapas.Objetos.butMNormal.Checked) return;

        // Reinicia o dispositivo caso haja alguma alteração no tamanho da tela
        if (new Size((int)Jan_Mapa_Azulejo.Size.X, (int)Jan_Mapa_Azulejo.Size.Y) != Objetos.picAzulejo.Size)
        {
            Jan_Mapa_Azulejo.Dispose();
            Jan_Mapa_Azulejo = new RenderWindow(Editor_Mapas.Objetos.picAzulejo.Handle);
        }

        // Limpa a área com um fundo preto
        Jan_Mapa_Azulejo.Clear(SFML.Graphics.Color.Black);

        // Dados
        Texture Textura = Tex_Azulejo[Objetos.cmbAzulejos.SelectedIndex + 1];
        Size Tamanho = TTamanho(Textura);
        Point Localização = new Point(Objetos.scrlAzulejoX.Value, Objetos.scrlAzulejoY.Value);

        // Desenha o azulejo e as grades
        Transparente(Jan_Mapa_Azulejo);
        Desenhar(Jan_Mapa_Azulejo, Textura, new Rectangle(Localização, Tamanho), new Rectangle(new Point(0), Tamanho));
        DesenharRetângulo(Jan_Mapa_Azulejo, new Rectangle(new Point(Editor_Mapas.Azulejo_Fonte.X - Localização.X, Editor_Mapas.Azulejo_Fonte.Y - Localização.Y), Editor_Mapas.Azulejo_Fonte.Size), CCor(165, 42, 42, 250));
        DesenharRetângulo(Jan_Mapa_Azulejo, Editor_Mapas.Azulejos_Mouse.X, Editor_Mapas.Azulejos_Mouse.Y, Globais.Grade, Globais.Grade, CCor(65, 105, 225, 250));

        // Exibe o que foi renderizado
        Jan_Mapa_Azulejo.Display();
    }

    public static void Editor_Mapas_Mapa()
    {
        short Índice = Editor_Mapas.Selecionado;

        // Previni erros
        if (!Editor_Mapas.Objetos.Visible) return;
        if (Listas.Mapa.GetUpperBound(0) <= 0) return;

        // Limpa a área com um fundo preto
        Jan_Mapa.Clear(SFML.Graphics.Color.Black);

        // Desenha o mapa
        Editor_Mapas_Mapa_Panorama(Índice);
        Editor_Mapas_Mapa_Azulejos(Índice);
        Editor_Mapas_Mapa_Clima(Índice);
        Editor_Mapas_Mapa_Iluminação(Índice);
        Editor_Mapas_Mapa_Fumaça(Índice);
        Editor_Mapas_Mapa_Grades(Índice);
        Editor_Mapas_Mapa_NPCs(Índice);

        // Exibe o que foi renderizado
        Jan_Mapa.Display();
    }

    public static void Editor_Mapas_Mapa_Panorama(short Índice)
    {
        short Textura = Listas.Mapa[Índice].Panorama;
        Size Tamanho = TTamanho(Tex_Panorama[Textura]);
        Size TempTamanho = new Size();

        // Limitações
        TempTamanho.Width = (Listas.Mapa[Índice].Largura + 1 - Editor_Mapas.Objetos.scrlMapaX.Value) * Globais.Grade_Zoom;
        TempTamanho.Height = (Listas.Mapa[Índice].Altura + 1 - Editor_Mapas.Objetos.scrlMapaY.Value) * Globais.Grade_Zoom;

        // Não permite que o tamanho ultrapasse a tela de Game
        if (Tamanho.Width > TempTamanho.Width) Tamanho.Width = TempTamanho.Width;
        if (Tamanho.Height > TempTamanho.Height) Tamanho.Height = TempTamanho.Height;

        // Desenha o panorama
        if (Editor_Mapas.Objetos.butVisualização.Checked && Listas.Mapa[Índice].Panorama > 0)
            Desenhar(Jan_Mapa, Tex_Panorama[Textura], Editor_Mapas.Zoom(new Rectangle(new Point(0), Tamanho)));
    }

    public static void Editor_Mapas_Mapa_Azulejos(short Índice)
    {
        Editor_Mapas Objetos = Editor_Mapas.Objetos;
        Listas.Estruturas.Mapas Mapa = Listas.Mapa[Índice];
        Listas.Estruturas.Azulejo_Dados Dados;
        int Início_X = Objetos.scrlMapaX.Value, Início_Y = Objetos.scrlMapaY.Value;
        SFML.Graphics.Color Cor; System.Drawing.Color TempCor = System.Drawing.Color.FromArgb(Mapa.Coloração);

        // Desenha todos os azulejos
        for (byte c = 0; c <= Mapa.Camada.Count - 1; c++)
        {
            // Somente se necessário
            if (!Objetos.lstCamadas.Items[c].Checked) continue;

            // Transparência da camada
            Cor = CCor(255, 255, 255);
            if (Objetos.butEdição.Checked && Objetos.butMNormal.Checked)
            {
                if (Editor_Mapas.Objetos.lstCamadas.SelectedIndices.Count > 0)
                    if (c != Editor_Mapas.Objetos.lstCamadas.SelectedItems[0].Index)
                        Cor = CCor(255, 255, 255, 150);
            }
            else
                Cor = CCor(TempCor.R, TempCor.G, TempCor.B);

            // Continua
            for (int x = Início_X; x <= Editor_Mapas.AzulejosVisíveis.Width; x++)
                for (int y = Início_Y; y <= Editor_Mapas.AzulejosVisíveis.Height; y++)
                    if (Mapa.Camada[c].Azulejo[x, y].Azulejo > 0)
                    {
                        // Dados
                        Dados = Mapa.Camada[c].Azulejo[x, y];
                        Rectangle Fonte = new Rectangle(new Point(Dados.x * Globais.Grade, Dados.y * Globais.Grade), Globais.Grade_Tamanho);
                        Rectangle Destino = new Rectangle(new Point((x - Início_X) * Globais.Grade, (y - Início_Y) * Globais.Grade), Globais.Grade_Tamanho);

                        // Desenha o azulejo
                        if (!Mapa.Camada[c].Azulejo[x, y].Automático)
                            Desenhar(Jan_Mapa, Tex_Azulejo[Dados.Azulejo], Fonte, Editor_Mapas.Zoom(Destino), Cor);
                        else
                            Editor_Mapas_AutoCriação(Destino.Location, Dados, Cor);
                    }
        }
    }

    public static void Editor_Mapas_AutoCriação(Point Localização, Listas.Estruturas.Azulejo_Dados Dados, SFML.Graphics.Color Cor)
    {
        // Desenha os 4 mini azulejos
        for (byte i = 0; i <= 3; i++)
        {
            Point Destino = Localização, Fonte = Dados.Mini[i];

            // Partes do azulejo
            switch (i)
            {
                case 1: Destino.X += 16; break;
                case 2: Destino.Y += 16; break;
                case 3: Destino.X += 16; Destino.Y += 16; break;
            }

            // Renderiza o mini azulejo
            Desenhar(Jan_Mapa, Tex_Azulejo[Dados.Azulejo], new Rectangle(Fonte.X, Fonte.Y, 16, 16), Editor_Mapas.Zoom(new Rectangle(Destino, new Size(16, 16))), Cor);
        }
    }

    public static void Editor_Mapas_Mapa_Fumaça(short Índice)
    {
        Listas.Estruturas.Mapa_Fumaça Dados = Listas.Mapa[Índice].Fumaça;
        Point Posição;

        // Somente se necessário
        if (Dados.Textura <= 0) return;
        if (!Editor_Mapas.Objetos.butVisualização.Checked) return;

        // Dados
        Size Textura_Tamanho = TTamanho(Tex_Fumaça[Dados.Textura]);
        for (int x = -1; x <= Editor_Mapas.AzulejosVisíveis.Width * Globais.Grade / Textura_Tamanho.Width + 1; x++)
            for (int y = -1; y <= Editor_Mapas.AzulejosVisíveis.Height * Globais.Grade / Textura_Tamanho.Height + 1; y++)
            {
                // Desenha a fumaça
                Posição = new Point(x * Textura_Tamanho.Width + Globais.Fumaça_X, y * Textura_Tamanho.Height + Globais.Fumaça_Y);
                Desenhar(Jan_Mapa, Tex_Fumaça[Dados.Textura], Editor_Mapas.Zoom(new Rectangle(Posição, Textura_Tamanho)), CCor(255, 255, 255, Dados.Transparência));
            }
    }

    public static void Editor_Mapas_Mapa_Clima(short Índice)
    {
        // Somente se necessário
        if (!Editor_Mapas.Objetos.butVisualização.Checked || Listas.Mapa[Índice].Clima.Tipo == (byte)Globais.Climas.Normal) return;

        // Dados
        byte x = 0;
        Size Tamanho = new Size(Editor_Mapas.Zoom((Listas.Mapa[Índice].Largura + 1) * Globais.Grade), Editor_Mapas.Zoom((Listas.Mapa[Índice].Altura + 1) * Globais.Grade));
        if (Listas.Mapa[Índice].Clima.Tipo == (byte)Globais.Climas.Nevando) x = 32;

        // Desenha as partículas
        for (int i = 1; i <= Listas.Clima_Partículas.GetUpperBound(0); i++)
            if (Listas.Clima_Partículas[i].Visível)
                Desenhar(Jan_Mapa, Tex_Clima, new Rectangle(x, 0, 32, 32), Editor_Mapas.Zoom(new Rectangle(Listas.Clima_Partículas[i].x, Listas.Clima_Partículas[i].y, 32, 32)), CCor(255, 255, 255, 150));
    }

    public static void Editor_Mapas_Mapa_Iluminação(short Índice)
    {
        Editor_Mapas Objetos = Editor_Mapas.Objetos;
        byte LuzGlobal_Tex = Listas.Mapa[Índice].LuzGlobal;
        Point Início = Globais.Zoom(Editor_Mapas.Objetos.scrlMapaX.Value, Editor_Mapas.Objetos.scrlMapaY.Value);
        byte Luz = (byte)((255 * ((decimal)Listas.Mapa[Índice].Iluminação / 100) - 255) * -1);

        // Somente se necessário
        if (!Editor_Mapas.Objetos.butVisualização.Checked) return;

        // Escuridão
        Jan_Mapa_Iluminação.Clear(CCor(0, 0, 0, Luz));

        // Desenha o ponto iluminado
        if (Listas.Mapa[Índice].Luz.Count > 0)
            for (byte i = 0; i <= Listas.Mapa[Índice].Luz.Count - 1; i++)
                Desenhar(Jan_Mapa_Iluminação, Tex_Iluminação, Editor_Mapas.Zoom_Grade(Listas.Mapa[Índice].Luz[i].Retângulo), null, new RenderStates(BlendMode.Multiply));

        // Pré visualização
        if (Editor_Mapas.Objetos.butMIluminação.Checked)
            Desenhar(Jan_Mapa_Iluminação, Tex_Iluminação, Globais.Zoom(Editor_Mapas.Mapa_Seleção), null, new RenderStates(BlendMode.Multiply));

        // Apresenta o que foi renderizado
        Jan_Mapa_Iluminação.Display();
        Jan_Mapa.Draw(new Sprite(Jan_Mapa_Iluminação.Texture));

        // Luz global
        if (LuzGlobal_Tex > 0)
            Desenhar(Jan_Mapa, Tex_Luz[LuzGlobal_Tex], Editor_Mapas.Zoom(new Rectangle(new Point(-Início.X, -Início.Y), TTamanho(Tex_Luz[LuzGlobal_Tex]))), CCor(255, 255, 255, 175), new RenderStates(BlendMode.Add));

        // Ponto de remoção da luz
        if (Objetos.butMIluminação.Checked)
            if (Listas.Mapa[Índice].Luz.Count > 0)
                for (byte i = 0; i <= Listas.Mapa[Índice].Luz.Count - 1; i++)
                    DesenharRetângulo(Jan_Mapa, Listas.Mapa[Índice].Luz[i].Retângulo.X * Globais.Grade_Zoom, Listas.Mapa[Índice].Luz[i].Retângulo.Y * Globais.Grade_Zoom, Globais.Grade_Zoom, Globais.Grade_Zoom, CCor(175, 42, 42, 175));

        // Trovoadas
        Size Tamanho = new Size(Editor_Mapas.Zoom((Listas.Mapa[Índice].Largura + 1) * Globais.Grade), Editor_Mapas.Zoom((Listas.Mapa[Índice].Altura + 1) * Globais.Grade));
        Desenhar(Jan_Mapa, Tex_Preenchido, 0, 0, 0, 0, Tamanho.Width, Tamanho.Height, CCor(255, 255, 255, Globais.Relâmpago));
    }

    public static void Editor_Mapas_Mapa_Grades(short Índice)
    {
        Editor_Mapas Objetos = Editor_Mapas.Objetos;
        Rectangle Fonte = Editor_Mapas.Azulejo_Fonte, Destino = new Rectangle();
        Point Início = new Point(Editor_Mapas.Mapa_Seleção.X - Objetos.scrlMapaX.Value, Editor_Mapas.Mapa_Seleção.Y - Objetos.scrlMapaY.Value); 

        // Dados
        Destino.Location = Globais.Zoom(Início.X, Início.Y);
        Destino.Size = new Size(Fonte.Width / Editor_Mapas.Zoom(), Fonte.Height / Editor_Mapas.Zoom());

        // Desenha as grades
        if (Objetos.butGrades.Checked || !Objetos.butGrades.Enabled)
            for (byte x = 0; x <= Editor_Mapas.AzulejosVisíveis.Width; x++)
                for (byte y = 0; y <= Editor_Mapas.AzulejosVisíveis.Height; y++)
                {
                    DesenharRetângulo(Jan_Mapa, x * Globais.Grade_Zoom, y * Globais.Grade_Zoom, Globais.Grade_Zoom, Globais.Grade_Zoom, CCor(25, 25, 25, 70));
                    Editor_Mapas_Mapa_Zonas(Índice, x, y);
                    Editor_Mapas_Mapa_Atributos(Índice, x, y);
                    Editor_Mapas_Mapa_BloqDirecional(Índice, x, y);
                }

        if (!Objetos.chkAutomático.Checked && Objetos.butMNormal.Checked)
            // Normal
            if (Objetos.butLápis.Checked)
                Desenhar(Jan_Mapa, Tex_Azulejo[Objetos.cmbAzulejos.SelectedIndex + 1], Fonte, Destino);
            // Retângulo
            else if (Objetos.butRetângulo.Checked)
                for (int x = Início.X; x <= Início.X + Editor_Mapas.Mapa_Seleção.Width - 1; x++)
                    for (int y = Início.Y; y <= Início.Y + Editor_Mapas.Mapa_Seleção.Height - 1; y++)
                        Desenhar(Jan_Mapa, Tex_Azulejo[Objetos.cmbAzulejos.SelectedIndex + 1], Fonte, new Rectangle(Globais.Zoom(x, y), Destino.Size));
    
        // Desenha a grade
        if (!Objetos.butMAtributos.Checked || !Editor_Mapas.Objetos.optABloqueioDir.Checked)
            DesenharRetângulo(Jan_Mapa, Destino.X, Destino.Y, Editor_Mapas.Mapa_Seleção.Width * Globais.Grade_Zoom, Editor_Mapas.Mapa_Seleção.Height * Globais.Grade_Zoom);
    }

    public static void Editor_Mapas_Mapa_Zonas(short Índice, byte x, byte y)
    {
        Point Posição = new Point((x - Editor_Mapas.Objetos.scrlMapaX.Value) * Globais.Grade_Zoom, (y - Editor_Mapas.Objetos.scrlMapaY.Value) * Globais.Grade_Zoom);
        byte Zona_Num = Listas.Mapa[Índice].Azulejo[x, y].Zona;
        SFML.Graphics.Color Cor;

        // Apenas se necessário
        if (!Editor_Mapas.Objetos.butMZonas.Checked) return;
        if (Zona_Num == 0) return;

        // Define a cor
        if (Zona_Num % 2 == 0)
            Cor = CCor((byte)((Zona_Num * 42) ^ 3), (byte)(Zona_Num * 22), (byte)(Zona_Num * 33), 150);
        else
            Cor = CCor((byte)(Zona_Num * 33), (byte)(Zona_Num * 22), (byte)(Zona_Num * 42), 150 ^ 3);

        // Desenha as zonas
        Desenhar(Jan_Mapa, Tex_Preenchido, new Rectangle(Posição, new Size(Globais.Grade_Zoom, Globais.Grade_Zoom)), Cor);
        Desenhar(Jan_Mapa, Zona_Num.ToString(), Posição.X, Posição.Y, SFML.Graphics.Color.White);
    }

    public static void Editor_Mapas_Mapa_Atributos(short Índice, byte x, byte y)
    {
        Point Posição = new Point((x - Editor_Mapas.Objetos.scrlMapaX.Value) * Globais.Grade_Zoom, (y - Editor_Mapas.Objetos.scrlMapaY.Value) * Globais.Grade_Zoom);
        Globais.Azulejo_Atributos Atributo = (Globais.Azulejo_Atributos )Listas.Mapa[Índice].Azulejo[x, y].Atributo;
        SFML.Graphics.Color Cor; string Letra = String.Empty;

        // Apenas se necessário
        if (!Editor_Mapas.Objetos.butMAtributos.Checked) return;
        if (Editor_Mapas.Objetos.optABloqueioDir.Checked) return;
        if (Atributo == Globais.Azulejo_Atributos.Nenhum) return;

        // Define a cor e a letra
        switch (Atributo)
        {
            case Globais.Azulejo_Atributos.Bloqueio: Letra = "B"; Cor = SFML.Graphics.Color.Red; break;
            case Globais.Azulejo_Atributos.Teletransporte: Letra = "T"; Cor = SFML.Graphics.Color.Blue; break;
            case Globais.Azulejo_Atributos.Item: Letra = "I"; Cor = SFML.Graphics.Color.Green; break;
            default: return;
        }
        Cor = new SFML.Graphics.Color(Cor.R, Cor.G, Cor.B, 100);

        // Desenha as Atributos
        Desenhar(Jan_Mapa, Tex_Preenchido, new Rectangle(Posição, new Size(Globais.Grade_Zoom, Globais.Grade_Zoom)), Cor);
        Desenhar(Jan_Mapa, Letra, Posição.X, Posição.Y, SFML.Graphics.Color.White);
    }

    public static void Editor_Mapas_Mapa_BloqDirecional(short Índice, byte x, byte y)
    {
        Point Azulejo = new Point(Editor_Mapas.Objetos.scrlMapaX.Value + x, Editor_Mapas.Objetos.scrlMapaY.Value + y);
        byte Y;

        // Apenas se necessário
        if (!Editor_Mapas.Objetos.butMAtributos.Checked) return;
        if (!Editor_Mapas.Objetos.optABloqueioDir.Checked) return;

        // Previni erros
        if (Azulejo.X > Listas.Mapa[Índice].Azulejo.GetUpperBound(0)) return;
        if (Azulejo.Y > Listas.Mapa[Índice].Azulejo.GetUpperBound(1)) return;

        for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
        {
            // Estado do bloqueio
            if (Listas.Mapa[Índice].Azulejo[Azulejo.X, Azulejo.Y].Bloqueio[i])
                Y = 8;
            else
                Y = 0;

            // Renderiza
            Desenhar(Jan_Mapa, Tex_Direções, x * Globais.Grade + Globais.Bloqueio_Posição(i).X, y * Globais.Grade + Globais.Bloqueio_Posição(i).Y, i * 8, Y, 6, 6);
        }
    }

    public static void Editor_Mapas_Mapa_NPCs(short Índice)
    {
        if (Editor_Mapas.Objetos.butMNPCs.Checked)
            for (byte i = 0; i <= Listas.Mapa[Índice].NPC.Count-1; i++)
                if (Listas.Mapa[Índice].NPC[i].Aparecer)
                {
                    Point Posição = new Point((Listas.Mapa[Índice].NPC[i].X - Editor_Mapas.Objetos.scrlMapaX.Value) * Globais.Grade_Zoom, (Listas.Mapa[Índice].NPC[i].Y - Editor_Mapas.Objetos.scrlMapaY.Value) * Globais.Grade_Zoom);

                    // Desenha uma sinalização de onde os NPCs estão
                    Desenhar(Jan_Mapa, Tex_Preenchido, new Rectangle(Posição, new Size(Globais.Grade_Zoom, Globais.Grade_Zoom)), CCor(0, 220, 0, 150));
                    Desenhar(Jan_Mapa, (i+1 ).ToString(), Posição.X+10, Posição.Y+10,  SFML.Graphics.Color.White);
                }
    }
    #endregion
}