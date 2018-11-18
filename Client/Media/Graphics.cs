﻿using System;
using System.IO;
using System.Drawing;
using SFML.Graphics;
using SFML.Window;

partial class Graphics
{
    // Locais de renderização
    public static RenderWindow Device;

    // Fonte principal
    public static SFML.Graphics.Font Fonte;

    // Textures
    public static Texture[] Tex_Character;
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
    public static Texture Tex_Fundo;
    public static Texture Tex_Clima;
    public static Texture Tex_Preenchido;
    public static Texture Tex_Direções;
    public static Texture Tex_Sombra;
    public static Texture Tex_Barras;
    public static Texture Tex_Barras_Painel;
    public static Texture Tex_Grade;
    public static Texture Tex_Equipamentos;

    // Formato das Textures
    public const string Formato = ".png";

    #region Motor
    public static Texture[] AdicionarTextures(string Diretório)
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

        // Retorna o cache da Texture
        return TempTex;
    }

    public static Size TTamanho(Texture Texture)
    {
        // Retorna com o tamanho da Texture
        if (Texture != null)
            return new Size((int)Texture.Size.X, (int)Texture.Size.Y);
        else
            return new Size(0, 0);
    }

    public static SFML.Graphics.Color CCor(byte R = 255, byte G = 255, byte B = 255, byte A = 255)
    {
        // Retorna com a cor
        return new SFML.Graphics.Color(R, G, B, A);
    }

    public static void Desenhar(Texture Texture, Rectangle Fonte, Rectangle Destino, object Cor = null, object Modo = null)
    {
        Sprite TmpImage = new Sprite(Texture);

        // Define os Data
        TmpImage.TextureRect = new IntRect(Fonte.X, Fonte.Y, Fonte.Width, Fonte.Height);
        TmpImage.Position = new Vector2f(Destino.X, Destino.Y);
        TmpImage.Scale = new Vector2f(Destino.Width / (float)Fonte.Width, Destino.Height / (float)Fonte.Height);
        if (Cor != null)
            //    if ((int)Cor != -1)
            TmpImage.Color = (SFML.Graphics.Color)Cor;

        // Renderiza a Texture em forma de retângulo
        if (Modo == null) Modo = RenderStates.Default;
        Device.Draw(TmpImage, (RenderStates)Modo);
    }

    public static void Desenhar(Texture Texture, int X, int Y, int Fonte_X, int Fonte_Y, int Fonte_Largura, int Fonte_Altura, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(Fonte_X, Fonte_Y), new Size(Fonte_Largura, Fonte_Altura));
        Rectangle Destino = new Rectangle(new Point(X, Y), new Size(Fonte_Largura, Fonte_Altura));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    public static void Desenhar(Texture Texture, Rectangle Destino, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), TTamanho(Texture));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    public static void Desenhar(Texture Texture, Point Posição, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), TTamanho(Texture));
        Rectangle Destino = new Rectangle(Posição, TTamanho(Texture));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    private static void Desenhar(string Texto, int X, int Y, SFML.Graphics.Color Cor)
    {
        Text TempTexto = new Text(Texto, Fonte);

        // Define os Data
        TempTexto.CharacterSize = 10;
        TempTexto.Color = Cor;
        TempTexto.Position = new Vector2f(X, Y);

        // Desenha
        Device.Draw(TempTexto);
    }

    public static void Renderizar_Caixa(Texture Texture, byte Margem, Point Posição, Size Tamanho)
    {
        int Texture_Largura = TTamanho(Texture).Width;
        int Texture_Altura = TTamanho(Texture).Height;

        // Borda esquerda
        Desenhar(Texture, new Rectangle(new Point(0), new Size(Margem, Texture_Largura)), new Rectangle(Posição, new Size(Margem, Texture_Altura)));
        // Borda direita
        Desenhar(Texture, new Rectangle(new Point(Texture_Largura - Margem, 0), new Size(Margem, Texture_Altura)), new Rectangle(new Point(Posição.X + Tamanho.Width - Margem, Posição.Y), new Size(Margem, Texture_Altura)));
        // Centro
        Desenhar(Texture, new Rectangle(new Point(Margem, 0), new Size(Margem, Texture_Altura)), new Rectangle(new Point(Posição.X + Margem, Posição.Y), new Size(Tamanho.Width - Margem * 2, Texture_Altura)));
    }

    public static void Renderizar_Caixa2(Texture Texture, byte Margem, Point Posição, Size Tamanho)
    {
        int Texture_Largura = TTamanho(Texture).Width;
        int Texture_Altura = TTamanho(Texture).Height;

        // Borda esquerda
        Desenhar(Texture, new Rectangle(new Point(0), new Size(Margem, Margem)), new Rectangle(Posição, new Size(Margem, Margem)));
        Desenhar(Texture, new Rectangle(new Point(0, Texture_Altura - Margem), new Size(Margem, Margem)), new Rectangle(new Point(Posição.X, Posição.Y + Tamanho.Height - Margem), new Size(Margem, Margem)));
        Desenhar(Texture, new Rectangle(new Point(0, Margem), new Size(Margem, Texture_Altura - Margem * 2)), new Rectangle(new Point(Posição.X, Posição.Y + Margem), new Size(Margem, Tamanho.Height - Margem * 2)));

        // Borda direita
        Desenhar(Texture, new Rectangle(new Point(Texture_Largura - Margem, 0), new Size(Margem, Margem)), new Rectangle(new Point(Posição.X + Tamanho.Width - Margem, Posição.Y), new Size(Margem, Margem)));
        Desenhar(Texture, new Rectangle(new Point(Texture_Largura - Margem, Texture_Altura - Margem), new Size(Margem, Margem)), new Rectangle(new Point(Posição.X + Tamanho.Width - Margem, Posição.Y + Tamanho.Height - Margem), new Size(Margem, Margem)));
        Desenhar(Texture, new Rectangle(new Point(Texture_Largura - Margem, Margem), new Size(Texture_Largura - Margem, Texture_Altura - Margem)), new Rectangle(new Point(Posição.X + Tamanho.Width + Margem, Posição.Y + Margem), new Size(Margem, Tamanho.Height - Margem * 2)));

        // Centro
        Desenhar(Texture, new Rectangle(new Point(Margem, Margem), new Size(3, 3)), new Rectangle(new Point(Posição.X + Margem, Posição.Y + Margem), new Size(Tamanho.Width - Margem * 2, Tamanho.Height - Margem * 2)));
    }

    #endregion

    public static void LerTextures()
    {
        // Inicia os Devices
        Device = new RenderWindow(Janela.Objetos.Handle);
        Fonte = new SFML.Graphics.Font(Diretórios.Fontes.FullName + "Georgia.ttf");

        // Conjuntos
        Tex_Character = AdicionarTextures(Diretórios.Tex_Personagens.FullName);
        Tex_Azulejo = AdicionarTextures(Diretórios.Tex_Azulejos.FullName);
        Tex_Face = AdicionarTextures(Diretórios.Tex_Faces.FullName);
        Tex_Painel = AdicionarTextures(Diretórios.Tex_Paineis.FullName);
        Tex_Botão = AdicionarTextures(Diretórios.Tex_Botões.FullName);
        Tex_Panorama = AdicionarTextures(Diretórios.Tex_Panoramas.FullName);
        Tex_Fumaça = AdicionarTextures(Diretórios.Tex_Fumaças.FullName);
        Tex_Luz = AdicionarTextures(Diretórios.Tex_Luzes.FullName);
        Tex_Item = AdicionarTextures(Diretórios.Tex_Itens.FullName);

        // Únicas
        Tex_Clima = new Texture(Diretórios.Tex_Clima.FullName + Formato);
        Tex_Preenchido = new Texture(Diretórios.Tex_Preenchido.FullName + Formato);
        Tex_Direções = new Texture(Diretórios.Tex_Direções.FullName + Formato);
        Tex_Marcador = new Texture(Diretórios.Tex_Marcador.FullName + Formato);
        Tex_Digitalizador = new Texture(Diretórios.Tex_Digitalizador.FullName + Formato);
        Tex_Fundo = new Texture(Diretórios.Tex_Fundo.FullName + Formato);
        Tex_Direções = new Texture(Diretórios.Tex_Direções.FullName + Formato);
        Tex_Sombra = new Texture(Diretórios.Tex_Sombra.FullName + Formato);
        Tex_Barras = new Texture(Diretórios.Tex_Barras.FullName + Formato);
        Tex_Barras_Painel = new Texture(Diretórios.Tex_Barras_Painel.FullName + Formato);
        Tex_Grade = new Texture(Diretórios.Tex_Grade.FullName + Formato);
        Tex_Equipamentos = new Texture(Diretórios.Tex_Equipamentos.FullName + Formato);
    }

    public static void Apresentar()
    {
        // Limpa a área com um fundo preto
        Device.Clear(SFML.Graphics.Color.Black);

        // Desenha o menu
        Menu();

        // Desenha as coisas em Game
        EmGame();

        // Desenha os Data do Game
        Desenhar("FPS: " + Game.FPS.ToString(), 8, 73, SFML.Graphics.Color.White);
        Desenhar("Latência: " + Game.Latência.ToString(), 8, 83, SFML.Graphics.Color.White);

        // Exibe o que foi renderizado
        Device.Display();
    }

    public static void EmGame()
    {
        // Não desenhar se não estiver em Game
        if (Ferramentas.JanelaAtual != Ferramentas.Janelas.Game)
            return;

        // Atualiza a câmera
        Game.Atualizar_Câmera();

        // Desenhos abaixo do Player
        Map_Panorama();
        Map_Azulejos((byte)Map.Camadas.Chão);
        Map_Itens();

        // Desenha os NPCs
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
                NPC(i);

        // Desenha os Playeres
        for (byte i = 1; i <= Player.MaiorIndex; i++)
            if (Player.EstáJogando(i))
                if (i != Player.MyIndex)
                    if (Lists.Player[i].Map == Player.Eu.Map)
                        Player_Character(i);

        // Desenha o próprio Player
        Player_Character(Player.MyIndex);

        // Desenhos acima do Player
        Map_Azulejos((byte)Map.Camadas.Telhado);
        Map_Clima();
        Map_Fumaça();
        Map_Name();

        // Interface do Game
        Game_Interface();
    }

    #region Ferramentas
    public static void Botão(string Name)
    {
        byte Transparência = 225;
        byte Index = Botões.EncontrarIndex(Name);

        // Lista a ordem de renderização da ferramenta
        Ferramentas.Listar(Ferramentas.Types.Botão, Index);

        // Não desenha a ferramenta se ela não for visível
        if (!Botões.Lista[Index].Geral.VerificarHabilitação())
            return;

        // Define a transparência do botão pelo seu estado
        switch (Botões.Lista[Index].Estado)
        {
            case Botões.Estados.Sobrepor:
                Transparência = 250;
                break;
            case Botões.Estados.Clique:
                Transparência = 200;
                break;
        }

        // Desenha o botão
        Desenhar(Tex_Botão[Botões.Lista[Index].Texture], Botões.Lista[Index].Geral.Posição, new SFML.Graphics.Color(255, 255, 225, Transparência));
    }

    public static void Painel(string Name)
    {
        byte Index = Paineis.EncontrarIndex(Name);

        // Lista a ordem de renderização da ferramenta
        Ferramentas.Listar(Ferramentas.Types.Painel, Index);

        // Não desenha a ferramenta se ela não for visível
        if (!Paineis.Lista[Index].Geral.VerificarHabilitação())
            return;

        // Desenha o painel
        Desenhar(Tex_Painel[Paineis.Lista[Index].Texture], Paineis.Lista[Index].Geral.Posição);
    }

    public static void Marcador(string Name)
    {
        Rectangle Fonte = new Rectangle(), Destino = new Rectangle();
        byte Index = Marcadores.EncontrarIndex(Name);

        // Lista a ordem de renderização da ferramenta
        Ferramentas.Listar(Ferramentas.Types.Marcador, Index);

        // Não desenha a ferramenta se ela não for visível
        if (!Marcadores.Lista[Index].Geral.VerificarHabilitação())
            return;

        // Define as propriedades dos retângulos
        Fonte.Size = new Size(TTamanho(Tex_Marcador).Width / 2, TTamanho(Tex_Marcador).Height);
        Destino = new Rectangle(Marcadores.Encontrar(Name).Geral.Posição, Fonte.Size);

        // Desenha a Texture do marcador pelo seu estado 
        if (Marcadores.Lista[Index].Estado)
            Fonte.Location = new Point(TTamanho(Tex_Marcador).Width / 2, 0);

        // Desenha o marcador 
        Desenhar(Tex_Marcador, Fonte, Destino);
        Desenhar(Marcadores.Encontrar(Name).Texto, Destino.Location.X + TTamanho(Tex_Marcador).Width / 2 + Marcadores.Margem, Destino.Location.Y + 1, SFML.Graphics.Color.White);
    }

    public static void Digitalizador(string Name)
    {
        byte Index = Digitalizadores.EncontrarIndex(Name);

        // Lista a ordem de renderização da ferramenta
        Ferramentas.Listar(Ferramentas.Types.Digitalizador, Index);

        // Não desenha a ferramenta se ela não for visível
        if (!Digitalizadores.Lista[Index].Geral.VerificarHabilitação())
            return;

        // Desenha a ferramenta
        Renderizar_Caixa(Tex_Digitalizador, 3, Digitalizadores.Lista[Index].Geral.Posição, new Size(Digitalizadores.Lista[Index].Largura, TTamanho(Tex_Digitalizador).Height));

        // Desenha o texto do digitalizador
        Digitalizador_Texto(Index);
    }

    public static void Digitalizador_Texto(byte i)
    {
        Point Posição = Digitalizadores.Lista[i].Geral.Posição;
        string Texto = Digitalizadores.Lista[i].Texto;

        // Altera todos os caracteres do texto para um em especifico, se for necessário
        if (Digitalizadores.Lista[i].Senha && !string.IsNullOrEmpty(Texto))
            Texto = new String('•', Texto.Length);

        // Quebra o texto para que caiba no digitalizador, se for necessário
        Texto = Ferramentas.QuebraTexto(Texto, Digitalizadores.Lista[i].Largura - 10);

        // Desenha o texto do digitalizador
        if (Digitalizadores.Foco == i && Digitalizadores.Sinal)
            Desenhar(Texto + "|", Posição.X + 4, Posição.Y + 2, SFML.Graphics.Color.White);
        else
            Desenhar(Texto, Posição.X + 4, Posição.Y + 2, SFML.Graphics.Color.White);
    }
    #endregion

    #region Menu
    public static void Menu()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação(string.Empty, Ferramentas.Janelas.Menu);

        // Desenha o menu
        Menu_Ferramentas();
        Menu_Conectar();
        Menu_Registrar();
        Menu_Opções();
        Menu_SelecionarCharacter();
        Menu_CriarCharacter();
    }

    public static void Menu_Ferramentas()
    {
        // Desenha as ferramentas básicas do menu
        if (Ferramentas.Habilitação) Desenhar(Tex_Fundo, new Point(0));
        Botão("Opções");
    }

    public static void Menu_Conectar()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação("Conectar", Ferramentas.Janelas.Menu);

        // Desenha o conjunto das ferramentas
        Painel("Conectar");
        Digitalizador("Conectar_Usuário");
        Digitalizador("Conectar_Senha");
        Botão("Conectar_Pronto");
        Botão("Registrar");
        Marcador("SalvarUsuário");
    }

    public static void Menu_Registrar()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação("Registrar", Ferramentas.Janelas.Menu);

        // Desenha o conjunto das ferramentas
        Painel("Registrar");
        Digitalizador("Registrar_Usuário");
        Digitalizador("Registrar_Senha");
        Digitalizador("Registrar_RepetirSenha");
        Botão("Registrar_Pronto");
        Botão("Conectar");
    }

    public static void Menu_Opções()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação("Opções", Ferramentas.Janelas.Menu);

        // Desenha o conjunto das ferramentas
        Painel("Opções");
        Marcador("Sons");
        Marcador("Músicas");
        Botão("Opções_Retornar");
    }

    public static void Menu_SelecionarCharacter()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação("SelecionarCharacter", Ferramentas.Janelas.Menu);

        // Desenha o conjunto das ferramentas
        Painel("SelecionarCharacter");
        SelecionarCharacter_Classe();
        Botão("Character_Criar");
        Botão("Character_Usar");
        Botão("Character_Deletar");
        Botão("Character_TrocarDireita");
        Botão("Character_TrocarEsquerda");

        // Eventos
        Botões.Eventos.Mudar_Personagens_Botões();
    }

    public static void Menu_CriarCharacter()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação("CriarCharacter", Ferramentas.Janelas.Menu);

        // Desenha o conjunto das ferramentas
        Painel("CriarCharacter");
        Botão("CriarCharacter");
        Digitalizador("CriarCharacter_Name");
        CriarCharacter_Classe();
        Botão("CriarCharacter_TrocarDireita");
        Botão("CriarCharacter_TrocarEsquerda");
        Botão("CriarCharacter_Retornar");
        Marcador("GêneroMasculino");
        Marcador("GêneroFeminino");
    }

    public static void SelecionarCharacter_Classe()
    {
        short Texture;
        string Texto = "Nenhum";

        // Somente se necessário
        if (!Paineis.Encontrar("SelecionarCharacter").Geral.Habilitado) return;
        if (Lists.Personagens == null) return;

        // Data
        int Classe = Lists.Personagens[Game.SelecionarCharacter].Classe;
        Point Texto_Posição = new Point(399, 425);

        // Verifica se o Character existe
        if (Classe == 0)
        {
            Desenhar(Texto, Texto_Posição.X - Ferramentas.MedirTexto_Largura(Texto) / 2, Texto_Posição.Y, SFML.Graphics.Color.White);
            return;
        }

        // Texture do Character
        if (Lists.Personagens[Game.SelecionarCharacter].Gênero)
            Texture = Lists.Classe[Classe].Texture_Masculina;
        else
            Texture = Lists.Classe[Classe].Texture_Feminina;

        // Desenha o Character
        if (Texture > 0)
        {
            Desenhar(Tex_Face[Texture], new Point(353, 442));
            Character(Texture, new Point(356, 534 - TTamanho(Tex_Character[Texture]).Height / 4), Game.Direções.Abaixo, Game.Animação_Parada);
        }

        // Desenha o Name da classe
        Texto = Lists.Personagens[Game.SelecionarCharacter].Name;
        Desenhar(Texto, Texto_Posição.X - Ferramentas.MedirTexto_Largura(Texto) / 2, Texto_Posição.Y, SFML.Graphics.Color.White);
    }

    public static void CriarCharacter_Classe()
    {
        short Texture;

        // Não desenhar se o painel não for visível
        if (!Paineis.Encontrar("CriarCharacter").Geral.Habilitado)
            return;

        // Texture do Character
        if (Marcadores.Encontrar("GêneroMasculino").Estado)
            Texture = Lists.Classe[Game.CriarCharacter_Classe].Texture_Masculina;
        else
            Texture = Lists.Classe[Game.CriarCharacter_Classe].Texture_Feminina;

        // Desenha o Character
        if (Texture > 0)
        {
            Desenhar(Tex_Face[Texture], new Point(425, 467));
            Character(Texture, new Point(430, 527), Game.Direções.Abaixo, Game.Animação_Parada);
        }

        // Desenha o Name da classe
        string Texto = Lists.Classe[Game.CriarCharacter_Classe].Name;
        Desenhar(Texto, 471 - Ferramentas.MedirTexto_Largura(Texto) / 2, 449, SFML.Graphics.Color.White);
    }
    #endregion

    #region Game
    public static void Game_Interface()
    {
        // Define a habilitação das ferramentas
        Ferramentas.DefinirHabilitação(string.Empty, Ferramentas.Janelas.Game);

        // Desenha o conjunto das ferramentas

        Game_Menu();
        Game_Chat();
        Game_Barras();
        Game_Hotbar();
        Game_Menu_Character();
        Game_Menu_Inventory();
    }

    public static void Game_Hotbar()
    {
        string Indicador = string.Empty;
        Point Painel_Posição = Paineis.Encontrar("Hotbar").Geral.Posição;

        // Desenha o painel 
        Painel("Hotbar");

        // Desenha os itens da hotbar
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
        {
            byte Slot = Player.Hotbar[i].Slot;
            if (Slot > 0)
            {
                // Itens
                if (Player.Hotbar[i].Type == (byte)Game.Hotbar.Item)
                {
                    // Desenha as visualizações do item
                    Point Posição = new Point(Painel_Posição.X + 8 + (i - 1) * 36, Painel_Posição.Y + 6);
                    Desenhar(Tex_Item[Lists.Item[Player.Inventory[Slot].Item_Num].Texture], Posição);
                    if (Ferramentas.EstáSobrepondo(new Rectangle(Posição.X, Posição.Y, 32, 32))) Painel_Informações(Player.Inventory[Slot].Item_Num, Painel_Posição.X, Painel_Posição.Y + 42);
                }
            }

            // Números da hotbar
            if (i < 10)
                Indicador = i.ToString();
            else if (i == 10)
                Indicador = "0";

            // Desenha os números
            Desenhar(Indicador, Painel_Posição.X + 16 + 36 * (i - 1), Painel_Posição.Y + 22, SFML.Graphics.Color.White);
        }

        // Movendo slot
        if (Player.Hotbar_Movendo > 0)
            if (Player.Hotbar[Player.Hotbar_Movendo].Type == (byte)Game.Hotbar.Item)
                Desenhar(Tex_Item[Lists.Item[Player.Inventory[Player.Hotbar[Player.Hotbar_Movendo].Slot].Item_Num].Texture], new Point(Ferramentas.Ponteiro.X + 6, Ferramentas.Ponteiro.Y + 6));
    }

    public static void Game_Menu()
    {
        // Desenha o conjunto das ferramentas
        Painel("Menu");
        Botão("Menu_Character");
        Botão("Menu_Inventory");
        Botão("Menu_Feitiços");
        Botão("Menu_1");
        Botão("Menu_2");
        Botão("Menu_Opções");
    }

    public static void Game_Menu_Character()
    {
        Point Posição = Paineis.Encontrar("Menu_Character").Geral.Posição;

        // Somente se necessário
        if (!Paineis.Encontrar("Menu_Character").Geral.Visível) return;

        // Desenha o painel 
        Painel("Menu_Character");

        // Data básicos
        Desenhar(Player.Eu.Name, Posição.X + 18, Posição.Y + 52, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Level.ToString(), Posição.X + 18, Posição.Y + 79, SFML.Graphics.Color.White);
        Desenhar(Tex_Face[Lists.Classe[Player.Eu.Classe].Texture_Masculina], new Point(Posição.X + 82, Posição.Y + 37));

        // Adicionar atributos
        if (Player.Eu.Pontos > 0)
        {
            Botão("Atributos_Força");
            Botão("Atributos_Resistência");
            Botão("Atributos_Inteligência");
            Botão("Atributos_Agilidade");
            Botão("Atributos_Vitalidade");
        }

        // Atributos
        Desenhar("Força: " + Player.Eu.Atributo[(byte)Game.Atributos.Força], Posição.X + 32, Posição.Y + 146, SFML.Graphics.Color.White);
        Desenhar("Resistência: " + Player.Eu.Atributo[(byte)Game.Atributos.Resistência], Posição.X + 32, Posição.Y + 162, SFML.Graphics.Color.White);
        Desenhar("Inteligência: " + Player.Eu.Atributo[(byte)Game.Atributos.Inteligência], Posição.X + 32, Posição.Y + 178, SFML.Graphics.Color.White);
        Desenhar("Agilidade: " + +Player.Eu.Atributo[(byte)Game.Atributos.Agilidade], Posição.X + 32, Posição.Y + 194, SFML.Graphics.Color.White);
        Desenhar("Vitalidade: " + +Player.Eu.Atributo[(byte)Game.Atributos.Vitalidade], Posição.X + 32, Posição.Y + 210, SFML.Graphics.Color.White);
        Desenhar("Pontos: " + Player.Eu.Pontos, Posição.X + 14, Posição.Y + 228, SFML.Graphics.Color.White);

        // Equipamentos 
        for (byte i = 0; i <= (byte)Game.Equipamentos.Amount - 1; i++)
        {
            if (Player.Eu.Equipamento[i] == 0)
                Desenhar(Tex_Equipamentos, Posição.X + 7 + i * 34, Posição.Y + 247, i * 34, 0, 34, 34);
            else
            {
                Desenhar(Tex_Item[Lists.Item[Player.Eu.Equipamento[i]].Texture], Posição.X + 8 + i * 35, Posição.Y + 247, 0, 0, 34, 34);
                if (Ferramentas.EstáSobrepondo(new Rectangle(Posição.X + 7 + i * 36, Posição.Y + 247, 32, 32))) Painel_Informações(Player.Eu.Equipamento[i], Posição.X - 186, Posição.Y + 5);
            }
        }
    }

    public static void Game_Menu_Inventory()
    {
        byte NumColunas = 5;
        Point Painel_Posição = Paineis.Encontrar("Menu_Inventory").Geral.Posição;

        // Somente se necessário
        if (!Paineis.Encontrar("Menu_Inventory").Geral.Visível) return;

        // Desenha o painel 
        Painel("Menu_Inventory");

        // Desenha todos os itens do Inventory
        for (byte i = 1; i <= Game.Max_Inventory; i++)
            if (Player.Inventory[i].Item_Num > 0)
            {
                byte Linha = (byte)((i - 1) / NumColunas);
                int Coluna = i - (Linha * 5) - 1;
                Point Posição = new Point(Painel_Posição.X + 7 + Coluna * 36, Painel_Posição.Y + 30 + Linha * 36);

                // Desenha as visualizações do item
                Desenhar(Tex_Item[Lists.Item[Player.Inventory[i].Item_Num].Texture], Posição);
                if (Ferramentas.EstáSobrepondo(new Rectangle(Posição.X, Posição.Y, 32, 32))) Painel_Informações(Player.Inventory[i].Item_Num, Painel_Posição.X - 186, Painel_Posição.Y + 3);

                // Amount
                if (Player.Inventory[i].Amount > 1) Desenhar(Player.Inventory[i].Amount.ToString(), Posição.X + 2, Posição.Y + 17, SFML.Graphics.Color.White);
            }

        // Movendo item
        if (Player.Inventory_Movendo > 0)
            Desenhar(Tex_Item[Lists.Item[Player.Inventory[Player.Inventory_Movendo].Item_Num].Texture], new Point(Ferramentas.Ponteiro.X + 6, Ferramentas.Ponteiro.Y + 6));
    }

    public static void Painel_Informações(short Item_Num, int X, int Y)
    {
        // Desenha o painel 
        Paineis.Encontrar("Menu_Informação").Geral.Posição.X = X;
        Paineis.Encontrar("Menu_Informação").Geral.Posição.Y = Y;
        Painel("Menu_Informação");

        // Informações
        Point Posição = Paineis.Encontrar("Menu_Informação").Geral.Posição;
        Desenhar(Lists.Item[Item_Num].Name, Posição.X + 9, Posição.Y + 6, SFML.Graphics.Color.Yellow);
        Desenhar(Tex_Item[Lists.Item[Item_Num].Texture], new Rectangle(Posição.X + 9, Posição.Y + 21, 64, 64));

        // Requerimentos
        if (Lists.Item[Item_Num].Type != (byte)Game.Itens.Nenhum)
        {
            Desenhar("Req level: " + Lists.Item[Item_Num].Req_Level, Posição.X + 9, Posição.Y + 90, SFML.Graphics.Color.White);
            if (Lists.Item[Item_Num].Req_Classe > 0)
                Desenhar("Req classe: " + Lists.Classe[Lists.Item[Item_Num].Req_Classe].Name, Posição.X + 9, Posição.Y + 102, SFML.Graphics.Color.White);
            else
                Desenhar("Req classe: Nenhuma", Posição.X + 9, Posição.Y + 102, SFML.Graphics.Color.White);
        }

        // Específicas 
        if (Lists.Item[Item_Num].Type == (byte)Game.Itens.Poção)
        {
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Desenhar(((Game.Vital)n).ToString() + ": " + Lists.Item[Item_Num].Poção_Vital[n], Posição.X + 100, Posição.Y + 18 + 12 * n, SFML.Graphics.Color.White);
            Desenhar("Exp: " + Lists.Item[Item_Num].Poção_Experiência, Posição.X + 100, Posição.Y + 42, SFML.Graphics.Color.White);
        }
        else if (Lists.Item[Item_Num].Type == (byte)Game.Itens.Equipamento)
        {
            for (byte n = 0; n <= (byte)Game.Atributos.Amount - 1; n++) Desenhar(((Game.Atributos)n).ToString() + ": " + Lists.Item[Item_Num].Equip_Atributo[n], Posição.X + 100, Posição.Y + 18 + 12 * n, SFML.Graphics.Color.White);
            if (Lists.Item[Item_Num].Equip_Type == (byte)Game.Equipamentos.Arma) Desenhar("Dano: " + Lists.Item[Item_Num].Arma_Dano, Posição.X + 100, Posição.Y + 18 + 60, SFML.Graphics.Color.White);
        }
    }

    public static void Game_Barras()
    {
        decimal Vida_Porcentagem = Player.Eu.Vital[(byte)Game.Vital.Vida] / (decimal)Player.Eu.Max_Vital[(byte)Game.Vital.Vida];
        decimal Mana_Porcentagem = Player.Eu.Vital[(byte)Game.Vital.Mana] / (decimal)Player.Eu.Max_Vital[(byte)Game.Vital.Mana];
        decimal Exp_Porcentagem = Player.Eu.Experiência / (decimal)Player.Eu.ExpNecessária;

        // Painel
        Painel("Barras");

        // Barras
        Desenhar(Tex_Barras_Painel, 14, 14, 0, 0, (int)(Tex_Barras_Painel.Size.X * Vida_Porcentagem), 17);
        Desenhar(Tex_Barras_Painel, 14, 32, 0, 18, (int)(Tex_Barras_Painel.Size.X * Mana_Porcentagem), 17);
        Desenhar(Tex_Barras_Painel, 14, 50, 0, 36, (int)(Tex_Barras_Painel.Size.X * Exp_Porcentagem), 17);

        // Textos 
        Desenhar("Vida", 18, 11, SFML.Graphics.Color.White);
        Desenhar("Mana", 18, 29, SFML.Graphics.Color.White);
        Desenhar("Exp", 18, 47, SFML.Graphics.Color.White);

        // Indicadores
        Desenhar(Player.Eu.Vital[(byte)Game.Vital.Vida] + "/" + Player.Eu.Max_Vital[(byte)Game.Vital.Vida], 70, 15, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Vital[(byte)Game.Vital.Mana] + "/" + Player.Eu.Max_Vital[(byte)Game.Vital.Mana], 70, 33, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Experiência + "/" + Player.Eu.ExpNecessária, 70, 51, SFML.Graphics.Color.White);
        Desenhar("Posição: " + Player.Eu.X + "/" + Player.Eu.Y, 8, 93, SFML.Graphics.Color.White);
    }

    public static void Game_Chat()
    {
        // Define a bisiblidade da caixa
        Paineis.Encontrar("Chat").Geral.Visível = Digitalizadores.Foco == Digitalizadores.EncontrarIndex("Chat");

        // Renderiza as caixas
        Painel("Chat");
        Digitalizador("Chat");

        // Renderiza as mensagens
        if (Ferramentas.Linhas_Visível)
            for (byte i = Ferramentas.Linha; i <= Ferramentas.Linhas_Visíveis + Ferramentas.Linha; i++)
                if (Ferramentas.Chat.Count > i)
                    Desenhar(Ferramentas.Chat[i].Texto, 16, 461 + 11 * (i - Ferramentas.Linha), Ferramentas.Chat[i].Cor);

        // Dica de como abrir o chat
        if (!Paineis.Encontrar("Chat").Geral.Visível)
            Desenhar("Aperte [Enter] para abrir o chat.", Digitalizadores.Encontrar("Chat").Geral.Posição.X + 5, Digitalizadores.Encontrar("Chat").Geral.Posição.Y + 3, SFML.Graphics.Color.White);
        else
        {
            Botão("Chat_Subir");
            Botão("Chat_Descer");
        }
    }
    #endregion

    public static void Character(short Texture, Point Posição, Game.Direções Direction, byte Coluna, bool Sofrendo = false)
    {
        Rectangle Fonte = new Rectangle(), Destino = new Rectangle();
        Size Tamanho = TTamanho(Tex_Character[Texture]);
        SFML.Graphics.Color Cor = new SFML.Graphics.Color(255, 255, 255);
        byte Linha = 0;

        // Direction
        switch (Direction)
        {
            case Game.Direções.Acima: Linha = Game.Movimentação_Acima; break;
            case Game.Direções.Abaixo: Linha = Game.Movimentação_Abaixo; break;
            case Game.Direções.Esquerda: Linha = Game.Movimentação_Esquerda; break;
            case Game.Direções.Direita: Linha = Game.Movimentação_Direita; break;
        }

        // Define as propriedades dos retângulos
        Fonte.X = Coluna * Tamanho.Width / Game.Animação_Amount;
        Fonte.Y = Linha * Tamanho.Height / Game.Animação_Amount;
        Fonte.Width = Tamanho.Width / Game.Animação_Amount;
        Fonte.Height = Tamanho.Height / Game.Animação_Amount;
        Destino = new Rectangle(Posição, Fonte.Size);

        // Demonstra que o Character está sofrendo dano
        if (Sofrendo) Cor = new SFML.Graphics.Color(205, 125, 125);

        // Desenha o Character e sua sombra
        Desenhar(Tex_Sombra, Destino.Location.X, Destino.Location.Y + Tamanho.Height / Game.Animação_Amount - TTamanho(Tex_Sombra).Height + 5, 0, 0, Tamanho.Width / Game.Animação_Amount, TTamanho(Tex_Sombra).Height);
        Desenhar(Tex_Character[Texture], Fonte, Destino, Cor);
    }
}