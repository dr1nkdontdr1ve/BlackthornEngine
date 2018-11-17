using System.Collections.Generic;

class Lists
{
    // Data storage
    public static Structures.Server_Data Server_Data = new Structures.Server_Data();
    public static Structures.Player[] Player;
    public static Structures.TempPlayer[] TempPlayer;
    public static Structures.Classes[] Classe;
    public static Structures.Maps[] Map;
    public static Structures.NPCs[] NPC;
    public static Structures.Items[] Item;

    // Structure of the items in general
    public class Structures
    {
        public struct Server_Data
        {
            public string Game_Name;
            public string Message;
            public short Porta;
            public byte Max_Players;
            public byte Max_Characters;
            public byte Num_Classes;
            public byte Num_Tiles;
            public short Num_Maps;
            public short Num_NPCs;
            public short Num_Items;
        }

        public struct Player
        {
            public string User;
            public string Password;
            public Game.Rank Access;
            public global::Player.Character_Structure[] Character;
        }

        public struct TempPlayer
        {
            public bool Playing;
            public byte Used;
            public bool GettingMap;
        }

        public struct Classes
        {
            public string Name;
            public short Texture_Male;
            public short Texture_Female;
            public short Appearance_Map;
            public byte Appearance_Direction;
            public byte Appearance_X;
            public byte Appearance_Y;
            public short[] Vital;
            public short[] Attribute;
        }

        public struct Maps
        {
            public short Revisão;
            public Tile[,] Tile;
            public string Name;
            public byte Width;
            public byte Height;
            public byte Moral;
            public byte Panorama;
            public byte Music;
            public int Coloração;
            public Map_Climate Climate;
            public Map_Smoke Smoke;
            public short[] Ligação;
            public byte LightGlobal;
            public byte Iluminação;
            public Light[] Light;
            public Map_NPC[] NPC;

            // Temporary
            public Map_NPCs[] Temp_NPC;
            public List<Map_Items> Temp_Item;
        }

        public struct Map_NPC
        {
            public short Index;
            public byte Zone;
            public bool Aparecer;
            public byte X;
            public byte Y;
        }

        public struct Tile
        {
            public byte Zone;
            public byte Attribute;
            public short Dado_1;
            public short Dado_2;
            public short Dado_3;
            public short Dado_4;
            public bool[] Block;
            public Tile_Data[,] Data;
        }

        public struct Tile_Data
        {
            public byte x;
            public byte y;
            public byte Tile;
            public bool Automatic;
        }

        public class Light
        {
            public byte X;
            public byte Y;
            public byte Width;
            public byte Height;
        }

        public struct Map_Climate
        {
            public byte Type;
            public byte Intensity;
        }

        public struct Map_Smoke
        {
            public byte Texture;
            public sbyte VelocityX;
            public sbyte VelocityY;
            public byte Transparency;
        }

        public struct NPCs
        {
            public string Name;
            public short Texture;
            public byte Aggressiveness;
            public byte Appearance;
            public byte View;
            public byte Experience;
            public short[] Vital;
            public short[] Attribute;
            public NPC_Queda[] Queda;
        }

        public struct Map_NPCs
        {
            public short Index;
            public byte X;
            public byte Y;
            public Game.Location Direction;
            public byte Target_Type;
            public byte Target_Index;
            public short[] Vital;
            public int Appearance_Time;
            public int Attack_Time;
        }

        public struct Map_Items
        {
            public short Index;
            public byte X;
            public byte Y;
            public short Amount;
        }

        public struct Items
        {
            // General
            public string Name;
            public string Description;
            public short Texture;
            public byte Type;
            public short Price;
            public bool Empilhável;
            public bool NãoDropável;
            // Requirements
            public short Req_Level;
            public byte Req_Classe;
            // Potion
            public short Potion_Experience;
            public short[] Potion_Vital;
            // Equipment
            public byte Equip_Type;
            public short[] Equip_Attribute;
            public short Weapon_Damage;
        }

        public struct Inventory
        {
            public short Item_Num;
            public short Amount;
        }

        public struct NPC_Queda
        {
            public short Item_Num;
            public short Amount;
            public byte Chance;
        }

        public struct Hotbar
        {
            public byte Type;
            public byte Slot;
        }
    }
}