using System;
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
    public static Texture[] Tex_Tile;
    public static Texture[] Tex_Face;
    public static Texture[] Tex_Panel;
    public static Texture[] Tex_Button;
    public static Texture[] Tex_Panorama;
    public static Texture[] Tex_Smoke;
    public static Texture[] Tex_Light;
    public static Texture[] Tex_Item;
    public static Texture Tex_Marker;
    public static Texture Tex_Scanner;
    public static Texture Tex_Fundo;
    public static Texture Tex_Climate;
    public static Texture Tex_Preenchido;
    public static Texture Tex_Location;
    public static Texture Tex_Sombra;
    public static Texture Tex_Bars;
    public static Texture Tex_Bars_Panel;
    public static Texture Tex_Grade;
    public static Texture Tex_Equipments;

    // Format das Textures
    public const string Format = ".png";

    #region Motor
    public static Texture[] AddTextures(string Diretório)
    {
        short i = 1;
        Texture[] TempTex = new Texture[0];

        while (File.Exists(Diretório + i + Format))
        {
            // Carrega todas do diretório e as adiciona a List
            Array.Resize(ref TempTex, i + 1);
            TempTex[i] = new Texture(Diretório + i + Format);
            i += 1;
        }

        // Retorna o cache da Texture
        return TempTex;
    }

    public static Size MySize(Texture Texture)
    {
        // Retorna com o Size da Texture
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

    public static void Desenhar(Texture Texture, int X, int Y, int Fonte_X, int Fonte_Y, int Fonte_Width, int Fonte_Height, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(Fonte_X, Fonte_Y), new Size(Fonte_Width, Fonte_Height));
        Rectangle Destino = new Rectangle(new Point(X, Y), new Size(Fonte_Width, Fonte_Height));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    public static void Desenhar(Texture Texture, Rectangle Destino, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), MySize(Texture));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    public static void Desenhar(Texture Texture, Point Position, object Cor = null, RenderStates Modo = new RenderStates())
    {
        // Define as propriedades dos retângulos
        Rectangle Fonte = new Rectangle(new Point(0), MySize(Texture));
        Rectangle Destino = new Rectangle(Position, MySize(Texture));

        // Desenha a Texture
        Desenhar(Texture, Fonte, Destino, Cor);
    }

    private static void Desenhar(string Text, int X, int Y, SFML.Graphics.Color Cor)
    {
        Text TempText = new Text(Text, Fonte);

        // Define os Data
        TempText.CharacterSize = 10;
        TempText.Color = Cor;
        TempText.Position = new Vector2f(X, Y);

        // Desenha
        Device.Draw(TempText);
    }

    public static void Renderizar_Caixa(Texture Texture, byte Margin, Point Position, Size Size)
    {
        int Texture_Width = MySize(Texture).Width;
        int Texture_Height = MySize(Texture).Height;

        // Borda Left
        Desenhar(Texture, new Rectangle(new Point(0), new Size(Margin, Texture_Width)), new Rectangle(Position, new Size(Margin, Texture_Height)));
        // Borda Right
        Desenhar(Texture, new Rectangle(new Point(Texture_Width - Margin, 0), new Size(Margin, Texture_Height)), new Rectangle(new Point(Position.X + Size.Width - Margin, Position.Y), new Size(Margin, Texture_Height)));
        // Centro
        Desenhar(Texture, new Rectangle(new Point(Margin, 0), new Size(Margin, Texture_Height)), new Rectangle(new Point(Position.X + Margin, Position.Y), new Size(Size.Width - Margin * 2, Texture_Height)));
    }

    public static void Renderizar_Caixa2(Texture Texture, byte Margin, Point Position, Size Size)
    {
        int Texture_Width = MySize(Texture).Width;
        int Texture_Height = MySize(Texture).Height;

        // Borda Left
        Desenhar(Texture, new Rectangle(new Point(0), new Size(Margin, Margin)), new Rectangle(Position, new Size(Margin, Margin)));
        Desenhar(Texture, new Rectangle(new Point(0, Texture_Height - Margin), new Size(Margin, Margin)), new Rectangle(new Point(Position.X, Position.Y + Size.Height - Margin), new Size(Margin, Margin)));
        Desenhar(Texture, new Rectangle(new Point(0, Margin), new Size(Margin, Texture_Height - Margin * 2)), new Rectangle(new Point(Position.X, Position.Y + Margin), new Size(Margin, Size.Height - Margin * 2)));

        // Borda Right
        Desenhar(Texture, new Rectangle(new Point(Texture_Width - Margin, 0), new Size(Margin, Margin)), new Rectangle(new Point(Position.X + Size.Width - Margin, Position.Y), new Size(Margin, Margin)));
        Desenhar(Texture, new Rectangle(new Point(Texture_Width - Margin, Texture_Height - Margin), new Size(Margin, Margin)), new Rectangle(new Point(Position.X + Size.Width - Margin, Position.Y + Size.Height - Margin), new Size(Margin, Margin)));
        Desenhar(Texture, new Rectangle(new Point(Texture_Width - Margin, Margin), new Size(Texture_Width - Margin, Texture_Height - Margin)), new Rectangle(new Point(Position.X + Size.Width + Margin, Position.Y + Margin), new Size(Margin, Size.Height - Margin * 2)));

        // Centro
        Desenhar(Texture, new Rectangle(new Point(Margin, Margin), new Size(3, 3)), new Rectangle(new Point(Position.X + Margin, Position.Y + Margin), new Size(Size.Width - Margin * 2, Size.Height - Margin * 2)));
    }

    #endregion

    public static void LerTextures()
    {
        // Inicia os Devices
        Device = new RenderWindow(Window.Objects.Handle);
        Fonte = new SFML.Graphics.Font(Directories.Fontes.FullName + "Georgia.ttf");

        // Conjuntos
        Tex_Character = AddTextures(Directories.Tex_Characters.FullName);
        Tex_Tile = AddTextures(Directories.Tex_Tiles.FullName);
        Tex_Face = AddTextures(Directories.Tex_Faces.FullName);
        Tex_Panel = AddTextures(Directories.Tex_Panels.FullName);
        Tex_Button = AddTextures(Directories.Tex_Buttons.FullName);
        Tex_Panorama = AddTextures(Directories.Tex_Panoramas.FullName);
        Tex_Smoke = AddTextures(Directories.Tex_Smokes.FullName);
        Tex_Light = AddTextures(Directories.Tex_Lightes.FullName);
        Tex_Item = AddTextures(Directories.Tex_Items.FullName);

        // Únicas
        Tex_Climate = new Texture(Directories.Tex_Climate.FullName + Format);
        Tex_Preenchido = new Texture(Directories.Tex_Preenchido.FullName + Format);
        Tex_Location = new Texture(Directories.Tex_Location.FullName + Format);
        Tex_Marker = new Texture(Directories.Tex_Marker.FullName + Format);
        Tex_Scanner = new Texture(Directories.Tex_Scanner.FullName + Format);
        Tex_Fundo = new Texture(Directories.Tex_Fundo.FullName + Format);
        Tex_Location = new Texture(Directories.Tex_Location.FullName + Format);
        Tex_Sombra = new Texture(Directories.Tex_Sombra.FullName + Format);
        Tex_Bars = new Texture(Directories.Tex_Bars.FullName + Format);
        Tex_Bars_Panel = new Texture(Directories.Tex_Bars_Panel.FullName + Format);
        Tex_Grade = new Texture(Directories.Tex_Grade.FullName + Format);
        Tex_Equipments = new Texture(Directories.Tex_Equipments.FullName + Format);
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
        Desenhar("Latency: " + Game.Latency.ToString(), 8, 83, SFML.Graphics.Color.White);

        // Exibe o que foi renderizado
        Device.Display();
    }

    public static void EmGame()
    {
        // Não desenhar se não estiver em Game
        if (Tools.CurrentWindow != Tools.Windows.Game)
            return;

        // Atualiza a Camera
        Game.Update_Camera();

        // Desenhos Below do Player
        Map_Panorama();
        Map_Tiles((byte)Map.Layers.Chão);
        Map_Items();

        // Desenha os NPCs
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
                NPC(i);

        // Desenha os Playeres
        for (byte i = 1; i <= Player.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (i != Player.MyIndex)
                    if (Lists.Player[i].Map == Player.Eu.Map)
                        Player_Character(i);

        // Desenha o próprio Player
        Player_Character(Player.MyIndex);

        // Desenhos Above do Player
        Map_Tiles((byte)Map.Layers.Telhado);
        Map_Climate();
        Map_Smoke();
        Map_Name();

        // Interface do Game
        Game_Interface();
    }

    #region Tools
    public static void Button(string Name)
    {
        byte Transparency = 225;
        byte Index = Buttons.LocateIndex(Name);

        // List a ordem de renderização da ferramenta
        Tools.Listr(Tools.Types.Button, Index);

        // Não desenha a ferramenta se ela não for Visible
        if (!Buttons.List[Index].General.CheckHabilitação())
            return;

        // Define a Transparency do Button pelo seu State
        switch (Buttons.List[Index].State)
        {
            case Buttons.States.Overlap:
                Transparency = 250;
                break;
            case Buttons.States.Click:
                Transparency = 200;
                break;
        }

        // Desenha o Button
        Desenhar(Tex_Button[Buttons.List[Index].Texture], Buttons.List[Index].General.Position, new SFML.Graphics.Color(255, 255, 225, Transparency));
    }

    public static void Panel(string Name)
    {
        byte Index = Panels.LocateIndex(Name);

        // List a ordem de renderização da ferramenta
        Tools.Listr(Tools.Types.Panel, Index);

        // Não desenha a ferramenta se ela não for Visible
        if (!Panels.List[Index].General.CheckHabilitação())
            return;

        // Desenha o Panel
        Desenhar(Tex_Panel[Panels.List[Index].Texture], Panels.List[Index].General.Position);
    }

    public static void Marker(string Name)
    {
        Rectangle Fonte = new Rectangle(), Destino = new Rectangle();
        byte Index = Markers.LocateIndex(Name);

        // List a ordem de renderização da ferramenta
        Tools.Listr(Tools.Types.Marker, Index);

        // Não desenha a ferramenta se ela não for Visible
        if (!Markers.List[Index].General.CheckHabilitação())
            return;

        // Define as propriedades dos retângulos
        Fonte.Size = new Size(MySize(Tex_Marker).Width / 2, MySize(Tex_Marker).Height);
        Destino = new Rectangle(Markers.Locate(Name).General.Position, Fonte.Size);

        // Desenha a Texture do Marker pelo seu State 
        if (Markers.List[Index].State)
            Fonte.Location = new Point(MySize(Tex_Marker).Width / 2, 0);

        // Desenha o Marker 
        Desenhar(Tex_Marker, Fonte, Destino);
        Desenhar(Markers.Locate(Name).Text, Destino.Location.X + MySize(Tex_Marker).Width / 2 + Markers.Margin, Destino.Location.Y + 1, SFML.Graphics.Color.White);
    }

    public static void Scanner(string Name)
    {
        byte Index = Scanners.LocateIndex(Name);

        // List a ordem de renderização da ferramenta
        Tools.Listr(Tools.Types.Scanner, Index);

        // Não desenha a ferramenta se ela não for Visible
        if (!Scanners.List[Index].General.CheckHabilitação())
            return;

        // Desenha a ferramenta
        Renderizar_Caixa(Tex_Scanner, 3, Scanners.List[Index].General.Position, new Size(Scanners.List[Index].Width, MySize(Tex_Scanner).Height));

        // Desenha o Text do Scanner
        Scanner_Text(Index);
    }

    public static void Scanner_Text(byte i)
    {
        Point Position = Scanners.List[i].General.Position;
        string Text = Scanners.List[i].Text;

        // Altera todos os caracteres do Text para um em especifico, se for necessário
        if (Scanners.List[i].Senha && !string.IsNullOrEmpty(Text))
            Text = new String('•', Text.Length);

        // Smash o Text para que caiba no Scanner, se for necessário
        Text = Tools.SmashText(Text, Scanners.List[i].Width - 10);

        // Desenha o Text do Scanner
        if (Scanners.Foco == i && Scanners.Sinal)
            Desenhar(Text + "|", Position.X + 4, Position.Y + 2, SFML.Graphics.Color.White);
        else
            Desenhar(Text, Position.X + 4, Position.Y + 2, SFML.Graphics.Color.White);
    }
    #endregion

    #region Menu
    public static void Menu()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação(string.Empty, Tools.Windows.Menu);

        // Desenha o menu
        Menu_Tools();
        Menu_Connect();
        Menu_Register();
        Menu_Options();
        Menu_SelectCharacter();
        Menu_CreateCharacter();
    }

    public static void Menu_Tools()
    {
        // Desenha as Tools básicas do menu
        if (Tools.Habilitação) Desenhar(Tex_Fundo, new Point(0));
        Button("Options");
    }

    public static void Menu_Connect()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação("Connect", Tools.Windows.Menu);

        // Desenha o conjunto das Tools
        Panel("Connect");
        Scanner("Connect_User");
        Scanner("Connect_Senha");
        Button("Connect_Ready");
        Button("Register");
        Marker("SaveUser");
    }

    public static void Menu_Register()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação("Register", Tools.Windows.Menu);

        // Desenha o conjunto das Tools
        Panel("Register");
        Scanner("Register_User");
        Scanner("Register_Senha");
        Scanner("Register_RepetirSenha");
        Button("Register_Ready");
        Button("Connect");
    }

    public static void Menu_Options()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação("Options", Tools.Windows.Menu);

        // Desenha o conjunto das Tools
        Panel("Options");
        Marker("Sons");
        Marker("Músicas");
        Button("Options_Retornar");
    }

    public static void Menu_SelectCharacter()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação("SelectCharacter", Tools.Windows.Menu);

        // Desenha o conjunto das Tools
        Panel("SelectCharacter");
        SelectCharacter_Classe();
        Button("Character_Create");
        Button("Character_Use");
        Button("Character_Delete");
        Button("Character_ExchangeRight");
        Button("Character_ExchangeLeft");

        // Events
        Buttons.Events.Change_Characters_Buttons();
    }

    public static void Menu_CreateCharacter()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação("CreateCharacter", Tools.Windows.Menu);

        // Desenha o conjunto das Tools
        Panel("CreateCharacter");
        Button("CreateCharacter");
        Scanner("CreateCharacter_Name");
        CreateCharacter_Classe();
        Button("CreateCharacter_ExchangeRight");
        Button("CreateCharacter_ExchangeLeft");
        Button("CreateCharacter_Retornar");
        Marker("GenreMasculino");
        Marker("GenreFeminino");
    }

    public static void SelectCharacter_Classe()
    {
        short Texture;
        string Text = "Nenhum";

        // Somente se necessário
        if (!Panels.Locate("SelectCharacter").General.Habilitado) return;
        if (Lists.Characters == null) return;

        // Data
        int Classe = Lists.Characters[Game.SelectCharacter].Classe;
        Point Text_Position = new Point(399, 425);

        // Verifica se o Character existe
        if (Classe == 0)
        {
            Desenhar(Text, Text_Position.X - Tools.MeasureText_Width(Text) / 2, Text_Position.Y, SFML.Graphics.Color.White);
            return;
        }

        // Texture do Character
        if (Lists.Characters[Game.SelectCharacter].Genre)
            Texture = Lists.Classe[Classe].Texture_Male;
        else
            Texture = Lists.Classe[Classe].Texture_Female;

        // Desenha o Character
        if (Texture > 0)
        {
            Desenhar(Tex_Face[Texture], new Point(353, 442));
            Character(Texture, new Point(356, 534 - MySize(Tex_Character[Texture]).Height / 4), Game.Location.Below, Game.Animation_Stop);
        }

        // Desenha o Name da classe
        Text = Lists.Characters[Game.SelectCharacter].Name;
        Desenhar(Text, Text_Position.X - Tools.MeasureText_Width(Text) / 2, Text_Position.Y, SFML.Graphics.Color.White);
    }

    public static void CreateCharacter_Classe()
    {
        short Texture;

        // Não desenhar se o Panel não for Visible
        if (!Panels.Locate("CreateCharacter").General.Habilitado)
            return;

        // Texture do Character
        if (Markers.Locate("GenreMasculino").State)
            Texture = Lists.Classe[Game.CreateCharacter_Classe].Texture_Male;
        else
            Texture = Lists.Classe[Game.CreateCharacter_Classe].Texture_Female;

        // Desenha o Character
        if (Texture > 0)
        {
            Desenhar(Tex_Face[Texture], new Point(425, 467));
            Character(Texture, new Point(430, 527), Game.Location.Below, Game.Animation_Stop);
        }

        // Desenha o Name da classe
        string Text = Lists.Classe[Game.CreateCharacter_Classe].Name;
        Desenhar(Text, 471 - Tools.MeasureText_Width(Text) / 2, 449, SFML.Graphics.Color.White);
    }
    #endregion

    #region Game
    public static void Game_Interface()
    {
        // Define a habilitação das Tools
        Tools.DefinirHabilitação(string.Empty, Tools.Windows.Game);

        // Desenha o conjunto das Tools

        Game_Menu();
        Game_Chat();
        Game_Bars();
        Game_Hotbar();
        Game_Menu_Character();
        Game_Menu_Inventory();
    }

    public static void Game_Hotbar()
    {
        string Indicador = string.Empty;
        Point Panel_Position = Panels.Locate("Hotbar").General.Position;

        // Desenha o Panel 
        Panel("Hotbar");

        // Desenha osItems da hotbar
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
        {
            byte Slot = Player.Hotbar[i].Slot;
            if (Slot > 0)
            {
                //Items
                if (Player.Hotbar[i].Type == (byte)Game.Hotbar.Item)
                {
                    // Desenha as visualizações do item
                    Point Position = new Point(Panel_Position.X + 8 + (i - 1) * 36, Panel_Position.Y + 6);
                    Desenhar(Tex_Item[Lists.Item[Player.Inventory[Slot].Item_Num].Texture], Position);
                    if (Tools.EstáOverlapping(new Rectangle(Position.X, Position.Y, 32, 32))) Panel_Informações(Player.Inventory[Slot].Item_Num, Panel_Position.X, Panel_Position.Y + 42);
                }
            }

            // Números da hotbar
            if (i < 10)
                Indicador = i.ToString();
            else if (i == 10)
                Indicador = "0";

            // Desenha os números
            Desenhar(Indicador, Panel_Position.X + 16 + 36 * (i - 1), Panel_Position.Y + 22, SFML.Graphics.Color.White);
        }

        // Moving slot
        if (Player.Hotbar_Moving > 0)
            if (Player.Hotbar[Player.Hotbar_Moving].Type == (byte)Game.Hotbar.Item)
                Desenhar(Tex_Item[Lists.Item[Player.Inventory[Player.Hotbar[Player.Hotbar_Moving].Slot].Item_Num].Texture], new Point(Tools.Ponteiro.X + 6, Tools.Ponteiro.Y + 6));
    }

    public static void Game_Menu()
    {
        // Desenha o conjunto das Tools
        Panel("Menu");
        Button("Menu_Character");
        Button("Menu_Inventory");
        Button("Menu_Feitiços");
        Button("Menu_1");
        Button("Menu_2");
        Button("Menu_Options");
    }

    public static void Game_Menu_Character()
    {
        Point Position = Panels.Locate("Menu_Character").General.Position;

        // Somente se necessário
        if (!Panels.Locate("Menu_Character").General.Visible) return;

        // Desenha o Panel 
        Panel("Menu_Character");

        // Data básicos
        Desenhar(Player.Eu.Name, Position.X + 18, Position.Y + 52, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Level.ToString(), Position.X + 18, Position.Y + 79, SFML.Graphics.Color.White);
        Desenhar(Tex_Face[Lists.Classe[Player.Eu.Classe].Texture_Male], new Point(Position.X + 82, Position.Y + 37));

        // Add Attributes
        if (Player.Eu.Pontos > 0)
        {
            Button("Attributes_Force");
            Button("Attributes_Resistence");
            Button("Attributes_Intelligence");
            Button("Attributes_Agility");
            Button("Attributes_Vitality");
        }

        // Attributes
        Desenhar("Force: " + Player.Eu.Attribute[(byte)Game.Attributes.Force], Position.X + 32, Position.Y + 146, SFML.Graphics.Color.White);
        Desenhar("Resistence: " + Player.Eu.Attribute[(byte)Game.Attributes.Resistence], Position.X + 32, Position.Y + 162, SFML.Graphics.Color.White);
        Desenhar("Intelligence: " + Player.Eu.Attribute[(byte)Game.Attributes.Intelligence], Position.X + 32, Position.Y + 178, SFML.Graphics.Color.White);
        Desenhar("Agility: " + +Player.Eu.Attribute[(byte)Game.Attributes.Agility], Position.X + 32, Position.Y + 194, SFML.Graphics.Color.White);
        Desenhar("Vitality: " + +Player.Eu.Attribute[(byte)Game.Attributes.Vitality], Position.X + 32, Position.Y + 210, SFML.Graphics.Color.White);
        Desenhar("Pontos: " + Player.Eu.Pontos, Position.X + 14, Position.Y + 228, SFML.Graphics.Color.White);

        // Equipments 
        for (byte i = 0; i <= (byte)Game.Equipments.Amount - 1; i++)
        {
            if (Player.Eu.Equipment[i] == 0)
                Desenhar(Tex_Equipments, Position.X + 7 + i * 34, Position.Y + 247, i * 34, 0, 34, 34);
            else
            {
                Desenhar(Tex_Item[Lists.Item[Player.Eu.Equipment[i]].Texture], Position.X + 8 + i * 35, Position.Y + 247, 0, 0, 34, 34);
                if (Tools.EstáOverlapping(new Rectangle(Position.X + 7 + i * 36, Position.Y + 247, 32, 32))) Panel_Informações(Player.Eu.Equipment[i], Position.X - 186, Position.Y + 5);
            }
        }
    }

    public static void Game_Menu_Inventory()
    {
        byte NumColumns = 5;
        Point Panel_Position = Panels.Locate("Menu_Inventory").General.Position;

        // Somente se necessário
        if (!Panels.Locate("Menu_Inventory").General.Visible) return;

        // Desenha o Panel 
        Panel("Menu_Inventory");

        // Desenha todos osItems do Inventory
        for (byte i = 1; i <= Game.Max_Inventory; i++)
            if (Player.Inventory[i].Item_Num > 0)
            {
                byte Line = (byte)((i - 1) / NumColumns);
                int Coluna = i - (Line * 5) - 1;
                Point Position = new Point(Panel_Position.X + 7 + Coluna * 36, Panel_Position.Y + 30 + Line * 36);

                // Desenha as visualizações do item
                Desenhar(Tex_Item[Lists.Item[Player.Inventory[i].Item_Num].Texture], Position);
                if (Tools.EstáOverlapping(new Rectangle(Position.X, Position.Y, 32, 32))) Panel_Informações(Player.Inventory[i].Item_Num, Panel_Position.X - 186, Panel_Position.Y + 3);

                // Amount
                if (Player.Inventory[i].Amount > 1) Desenhar(Player.Inventory[i].Amount.ToString(), Position.X + 2, Position.Y + 17, SFML.Graphics.Color.White);
            }

        // Moving item
        if (Player.Inventory_Moving > 0)
            Desenhar(Tex_Item[Lists.Item[Player.Inventory[Player.Inventory_Moving].Item_Num].Texture], new Point(Tools.Ponteiro.X + 6, Tools.Ponteiro.Y + 6));
    }

    public static void Panel_Informações(short Item_Num, int X, int Y)
    {
        // Desenha o Panel 
        Panels.Locate("Menu_Informação").General.Position.X = X;
        Panels.Locate("Menu_Informação").General.Position.Y = Y;
        Panel("Menu_Informação");

        // Informações
        Point Position = Panels.Locate("Menu_Informação").General.Position;
        Desenhar(Lists.Item[Item_Num].Name, Position.X + 9, Position.Y + 6, SFML.Graphics.Color.Yellow);
        Desenhar(Tex_Item[Lists.Item[Item_Num].Texture], new Rectangle(Position.X + 9, Position.Y + 21, 64, 64));

        // Requerimentos
        if (Lists.Item[Item_Num].Type != (byte)Game.Items.Nenhum)
        {
            Desenhar("Req level: " + Lists.Item[Item_Num].Req_Level, Position.X + 9, Position.Y + 90, SFML.Graphics.Color.White);
            if (Lists.Item[Item_Num].Req_Classe > 0)
                Desenhar("Req classe: " + Lists.Classe[Lists.Item[Item_Num].Req_Classe].Name, Position.X + 9, Position.Y + 102, SFML.Graphics.Color.White);
            else
                Desenhar("Req classe: Nenhuma", Position.X + 9, Position.Y + 102, SFML.Graphics.Color.White);
        }

        // Específicas 
        if (Lists.Item[Item_Num].Type == (byte)Game.Items.Potion)
        {
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Desenhar(((Game.Vital)n).ToString() + ": " + Lists.Item[Item_Num].Potion_Vital[n], Position.X + 100, Position.Y + 18 + 12 * n, SFML.Graphics.Color.White);
            Desenhar("Exp: " + Lists.Item[Item_Num].Potion_Experience, Position.X + 100, Position.Y + 42, SFML.Graphics.Color.White);
        }
        else if (Lists.Item[Item_Num].Type == (byte)Game.Items.Equipment)
        {
            for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Desenhar(((Game.Attributes)n).ToString() + ": " + Lists.Item[Item_Num].Equip_Attribute[n], Position.X + 100, Position.Y + 18 + 12 * n, SFML.Graphics.Color.White);
            if (Lists.Item[Item_Num].Equip_Type == (byte)Game.Equipments.Arma) Desenhar("Dano: " + Lists.Item[Item_Num].Weapon_Damage, Position.X + 100, Position.Y + 18 + 60, SFML.Graphics.Color.White);
        }
    }

    public static void Game_Bars()
    {
        decimal Life_Porcentagem = Player.Eu.Vital[(byte)Game.Vital.Life] / (decimal)Player.Eu.Max_Vital[(byte)Game.Vital.Life];
        decimal Mana_Porcentagem = Player.Eu.Vital[(byte)Game.Vital.Mana] / (decimal)Player.Eu.Max_Vital[(byte)Game.Vital.Mana];
        decimal Exp_Porcentagem = Player.Eu.Experience / (decimal)Player.Eu.ExpNecessária;

        // Panel
        Panel("Bars");

        // Bars
        Desenhar(Tex_Bars_Panel, 14, 14, 0, 0, (int)(Tex_Bars_Panel.Size.X * Life_Porcentagem), 17);
        Desenhar(Tex_Bars_Panel, 14, 32, 0, 18, (int)(Tex_Bars_Panel.Size.X * Mana_Porcentagem), 17);
        Desenhar(Tex_Bars_Panel, 14, 50, 0, 36, (int)(Tex_Bars_Panel.Size.X * Exp_Porcentagem), 17);

        // Texts 
        Desenhar("Life", 18, 11, SFML.Graphics.Color.White);
        Desenhar("Mana", 18, 29, SFML.Graphics.Color.White);
        Desenhar("Exp", 18, 47, SFML.Graphics.Color.White);

        // Indicadores
        Desenhar(Player.Eu.Vital[(byte)Game.Vital.Life] + "/" + Player.Eu.Max_Vital[(byte)Game.Vital.Life], 70, 15, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Vital[(byte)Game.Vital.Mana] + "/" + Player.Eu.Max_Vital[(byte)Game.Vital.Mana], 70, 33, SFML.Graphics.Color.White);
        Desenhar(Player.Eu.Experience + "/" + Player.Eu.ExpNecessária, 70, 51, SFML.Graphics.Color.White);
        Desenhar("Position: " + Player.Eu.X + "/" + Player.Eu.Y, 8, 93, SFML.Graphics.Color.White);
    }

    public static void Game_Chat()
    {
        // Define a bisiblidade da caixa
        Panels.Locate("Chat").General.Visible = Scanners.Foco == Scanners.LocateIndex("Chat");

        // Renderiza as caixas
        Panel("Chat");
        Scanner("Chat");

        // Renderiza as mensagens
        if (Tools.Lines_Visible)
            for (byte i = Tools.Line; i <= Tools.Lines_Visíveis + Tools.Line; i++)
                if (Tools.Chat.Count > i)
                    Desenhar(Tools.Chat[i].Text, 16, 461 + 11 * (i - Tools.Line), Tools.Chat[i].Cor);

        // Dica de como Open o chat
        if (!Panels.Locate("Chat").General.Visible)
            Desenhar("Aperte [Enter] para Open o chat.", Scanners.Locate("Chat").General.Position.X + 5, Scanners.Locate("Chat").General.Position.Y + 3, SFML.Graphics.Color.White);
        else
        {
            Button("Chat_Up");
            Button("Chat_Down");
        }
    }
    #endregion

    public static void Character(short Texture, Point Position, Game.Location Direction, byte Coluna, bool Suffering = false)
    {
        Rectangle Fonte = new Rectangle(), Destino = new Rectangle();
        Size Size = MySize(Tex_Character[Texture]);
        SFML.Graphics.Color Cor = new SFML.Graphics.Color(255, 255, 255);
        byte Line = 0;

        // Direction
        switch (Direction)
        {
            case Game.Location.Above: Line = Game.Movement_Above; break;
            case Game.Location.Below: Line = Game.Movement_Below; break;
            case Game.Location.Left: Line = Game.Movement_Left; break;
            case Game.Location.Right: Line = Game.Movement_Right; break;
        }

        // Define as propriedades dos retângulos
        Fonte.X = Coluna * Size.Width / Game.Animation_Amount;
        Fonte.Y = Line * Size.Height / Game.Animation_Amount;
        Fonte.Width = Size.Width / Game.Animation_Amount;
        Fonte.Height = Size.Height / Game.Animation_Amount;
        Destino = new Rectangle(Position, Fonte.Size);

        // Demonstra que o Character está Suffering dano
        if (Suffering) Cor = new SFML.Graphics.Color(205, 125, 125);

        // Desenha o Character e sua sombra
        Desenhar(Tex_Sombra, Destino.Location.X, Destino.Location.Y + Size.Height / Game.Animation_Amount - MySize(Tex_Sombra).Height + 5, 0, 0, Size.Width / Game.Animation_Amount, MySize(Tex_Sombra).Height);
        Desenhar(Tex_Character[Texture], Fonte, Destino, Cor);
    }
}