using System;

class Game
{
    // Random numbers
    public static Random Aleatório = new Random();

    // The highest index of connected players
    public static byte BiggerIndex;

    // Server CPS
    public static int CPS;
    public static bool CPS_Travado;

    // Directional lock
    public const byte Max_LockDirectional = 3;

    // Maximum and minimum characters allowed in some text
    public const byte Max_Character = 12;
    public const byte Min_Character = 3;

    // Limits in general
    public const byte Max_NPC_Queda = 4;
    public const byte Max_Inventory = 30;
    public const byte Max_Map_Items = 100;
    public const byte Max_Hotbar = 10;

    #region Numbers
    public enum Location
    {
        Above,
        Below,
        Left,
        Right,
        Amount
    }

    public enum Rank
    {
        Normal,
        Moderator,
        Editor,
        Administrator
    }

    public enum Genre
    {
        Male,
        Female
    }

    public enum Vital
    {
        Life,
        Mana,
        Amount
    }

    public enum Attributes
    {
        Force,
        Resistance,
        Intelligence,
        Agility,
        Vitality,
        Amount
    }

    public enum Mensagens
    {
        Game,
        Map,
        Global,
        Private
    }

    public enum Target
    {
        Player = 1,
        NPC
    }

    public enum Items
    {
        Normal,
        Equipment,
        Potion
    }

    public enum Equipment
    {
        Weapon,
        Armor,
        Helmet,
        Shield,
        Amulet,
        Amount
    }

    public enum Hotbar
    {
        Normal,
        Item
    }
    #endregion

    public static void ResetBiggerIndex()
    {
        // Redefine the maximum number of players
        BiggerIndex = 0;

        for (byte i = (byte)Lists.Player.GetUpperBound(0); i >= 1; i -= 1)
            if (Network.IsConnected(i))
            {
                BiggerIndex = i;
                break;
            }

        // Send data to players
        Sending.BiggerIndex();
    }

    public static Location InverseDirection(Location Direction)
    {
        // Returns the inverse direction
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