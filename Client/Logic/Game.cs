using System;
using System.Drawing;
using System.Windows.Forms;

public class Game
{
    // Números aleatórios
    public static Random Aleatório = new Random();

    // Dimension of grids
    public const byte Grade = 32;

    // Game Delay Calculation Measure
    public static short FPS;

    // Interface
    public static byte CreateCharacter_Classe = 1;
    public static byte SelectCharacter = 1;

    // Player
    public const short Attack_Velocity = 750;

    // Pressing the keys
    public static bool HoldKey_Above;
    public static bool HoldKey_Below;
    public static bool HoldKey_Left;
    public static bool HoldKey_Right;
    public static bool HoldKey_Shift;
    public static bool HoldKey_Control;

    // Animation
    public const byte Animation_Amount = 4;
    public const byte Animation_Stop = 1;
    public const byte Animation_Right = 0;
    public const byte Animation_Left = 2;
    public const byte Animation_Attack = 2;

    // Movement
    public const byte Movement_Above = 3;
    public const byte Movement_Below = 0;
    public const byte Movement_Left = 1;
    public const byte Movement_Right = 2;

    // Player view
    public static Rectangle Camera;
    public static Rectangle Tiles_View;

    // Directional lock
    public const byte Max_BloqDirecional = 3;

    // Screen size
    public const short Screen_Width = (Map.Min_Width + 1) * Grade;
    public const short Screen_Height = (Map.Min_Height + 1) * Grade;

    // Limits in general
    public const byte Max_Inventory = 30;
    public const byte Max_Map_Items = 100;
    public const byte Max_Hotbar = 10;

    // Latency
    public static int Latency;
    public static int Latency_Envio;

    #region Numeradores
    public enum Situations
    {
        Connect,
        Register,
        SelectCharacter,
        CreateCharacter
    }

    public enum Attributes
    {
        Force,
        Resistence,
        Intelligence,
        Agility,
        Vitality,
        Amount
    }

    public enum Location
    {
        Above,
        Below,
        Left,
        Right,
        Amount
    }

    public enum Movements
    {
        Parado,
        Andando,
        Correndo
    }

    public enum Acessos
    {
        Nenhum,
        Moderador,
        Editor,
        Administrador
    }

    public enum Mensagens
    {
        Game,
        Map,
        Global,
        Particular
    }

    public enum Vital
    {
        Life,
        Mana,
        Amount
    }

    public enum NPCs
    {
        Passivo,
        Atacado,
        AoVer
    }

    public enum Alvo
    {
        Player = 1,
        NPC
    }

    public enum Items
    {
        Nenhum,
        Equipment,
        Potion
    }

    public enum Equipments
    {
        Arma,
        Armadura,
        Capacete,
        Escudo,
        Amuleto,
        Amount
    }

    public enum Hotbar
    {
        Nenhum, 
        Item
    }
    #endregion

    public static void OpenMenu()
    {
        // Reproduz a Music de fundo
        if (Lists.Options.Músicas)
            Audio.Música.Reproduce(Audio.Músicas.Menu);

        // Abre o menu
        Tools.CurrentWindow = Tools.Windows.Menu;
    }

    public static void Leave()
    {
        // Volta ao menu
        OpenMenu();

        // Termina a conexão
        Network.Disconnect();
    }

    public static void SetLocation(Situations Situação)
    {
        // Verifica se é possível se Connect ao servidor
        if (!Network.TryConnect())
        {
            MessageBox.Show("The server is currently unavailable.");
            return;
        }

        // Envia os Data
        switch (Situação)
        {
            case Situations.Connect: Sending.Connect(); break;
            case Situations.Register: Sending.Register(); break;
            case Situations.CreateCharacter: Sending.CreateCharacter(); break;
        }
    }

    public static void Disconnect()
    {
        // Não Close os Panels se não for necessário
        if (Panels.Locate("Options").General.Visible || Panels.Locate("Connect").General.Visible || Panels.Locate("Register").General.Visible)
            return;

        // Limpa os valores
        Audio.Som.Stop_All();
        Player.MyIndex = 0;

        // Traz o Player de volta ao menu
        Tools.CurrentWindow = Tools.Windows.Menu;
        Panels.Menu_Close();
        Panels.Locate("Connect").General.Visible = true;
    }

    public static void Update_Camera()
    {
        Point Final = new Point(), Start = new Point(), Position = new Point();

        // Centro da Screen
        Position.X = Player.Eu.X2 + Grade;
        Position.Y = Player.Eu.Y2 + Grade;

        // Start da Screen
        Start.X = Player.Eu.X - ((Map.Min_Width + 1) / 2) - 1;
        Start.Y = Player.Eu.Y - ((Map.Min_Height + 1) / 2) - 1;

        // Reajusta a Position horizontal da Screen
        if (Start.X < 0)
        {
            Position.X = 0;
            if (Start.X == -1 && Player.Eu.X2 > 0) Position.X = Player.Eu.X2;
            Start.X = 0;
        }

        // Reajusta a Position vertical da Screen
        if (Start.Y < 0)
        {
            Position.Y = 0;
            if (Start.Y == -1 && Player.Eu.Y2 > 0) Position.Y = Player.Eu.Y2;
            Start.Y = 0;
        }

        // Final da Screen
        Final.X = Start.X + (Map.Min_Width + 1) + 1;
        Final.Y = Start.Y + (Map.Min_Height + 1) + 1;

        // Reajusta a Position horizontal da Screen
        if (Final.X > Lists.Map.Width)
        {
            Position.X = Grade;
            if (Final.X == Lists.Map.Width + 1 && Player.Eu.X2 < 0) Position.X = Player.Eu.X2 + Grade;
            Final.X = Lists.Map.Width;
            Start.X = Final.X - Map.Min_Width - 1;
        }

        // Reajusta a Position vertical da Screen
        if (Final.Y > Lists.Map.Height)
        {
            Position.Y = Grade;
            if (Final.Y == Lists.Map.Height + 1 && Player.Eu.Y2 < 0) Position.Y = Player.Eu.Y2 + Grade;
            Final.Y = Lists.Map.Height;
            Start.Y = Final.Y - Map.Min_Height - 1;
        }

        // Define a dimensão dos Tiles vistos
        Tiles_View.Y = Start.Y;
        Tiles_View.Height = Final.Y;
        Tiles_View.X = Start.X;
        Tiles_View.Width = Final.X;

        // Define a Position da Camera
        Camera.Y = Position.Y;
        Camera.Height = Camera.Y + Screen_Height;
        Camera.X = Position.X;
        Camera.Width = Camera.X + Screen_Width;
    }

    public static int ConvertX(int x)
    {
        // Converte o valor em uma Position adequada à camera
        return x - (Tiles_View.X * Grade) - Camera.X;
    }

    public static int ConvertY(int y)
    {
        // Converte o valor em uma Position adequada à camera
        return y - (Tiles_View.Y * Grade) - Camera.Y;
    }

    public static bool EstáNoLimite_View(int x, int y)
    {
        // Verifica se os valores estão no limite do que se está vendo
        if (x >= Tiles_View.X && y >= Tiles_View.Y && x <= Tiles_View.Width && y <= Tiles_View.Height)
            return true;
        else
            return false;
    }

    public static Location DirectionInverse(Location Direction)
    {
        // Retorna a Direction inversa
        switch (Direction)
        {
            case Location.Above: return Location.Below;
            case Location.Below: return Location.Above;
            case Location.Left: return Location.Right;
            case Location.Right: return Location.Left;
            default: return Location.Amount;
        }
    }
}