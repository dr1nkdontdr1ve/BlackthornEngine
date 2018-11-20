public class Lists
{
    // Armazenamento de Data
    public static Structures.Options Options = new Structures.Options();
    public static Structures.Client_Data Client_Data = new Structures.Client_Data();
    public static Structures.Server_Data Server_Data = new Structures.Server_Data();
    public static Structures.Player[] Player;
    public static Structures.Classe[] Classe;
    public static Structures.Character[] Characters;
    public static Structures.Maps Map;
    public static Structures.Climate[] Climate_Particles;
    public static Structures.NPCs[] NPC;
    public static Structures.Items[] Item;

    // Structure dosItems em gerais
    public class Structures
    {
        public struct Options
        {
            public string Jogo_Name;
            public bool SalvarUsuário;
            public bool Sons;
            public bool Músicas;
            public string User;
        }

        public struct Client_Data
        {
            public byte Num_Buttons;
            public byte Num_Panels;
            public byte Num_Markers;
            public byte Num_Scanners;
        }

        public struct Server_Data
        {
            public byte Max_Playeres;
            public byte Max_Characters;
            public byte Num_Classes;
            public byte Num_Tiles;
            public short Num_Maps;
        }

        public class Player
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animation;
            public bool Atacando;
            public int Attack_Time;
            public int Suffering;
            public short[] Max_Vital;
            public int Coletar_Time;
            // Geral
            public string Name;
            public byte Classe;
            public bool Genre;
            public short Level;
            public short Experience;
            public short ExpNecessária;
            public short Pontos;
            public short[] Vital;
            public short[] Attribute;
            public short Map;
            public byte X;
            public byte Y;
            public Jogo.Location Direction;
            public Jogo.Movements Movement;
            public short[] Equipment;
        }

        public class Character
        {
            public string Name;
            public byte Classe;
            public bool Genre;
            public short Level;
        }

        public class Classe
        {
            public string Name;
            public short Texture_Male;
            public short Texture_Female;
            public short Aparecer_Map;
            public byte Aparecer_Direction;
            public byte Aparecer_X;
            public byte Aparecer_Y;
        }
        
        public struct Maps
        {
            public short Review;
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
            public Tile[,] Tile;
            public Light[] Light;
            public short[] NPC;

            // Timerário
            public Map_NPCs[] Temp_NPC;
            public Map_Items[] Temp_Item;
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

        public struct Tile
        {
            public byte Attribute;
            public bool[] Block;
            public Tile_Data[,] Data;
        }

        public struct Tile_Data
        {
            public byte x;
            public byte y;
            public byte Tile;
            public bool Automático;
            public System.Drawing.Point[] Mini;
        }

        public struct Climate
        {
            public bool Visible;
            public int x;
            public int y;
            public int Velocity;
            public int Inicío;
            public bool Voltar;
        }

        public class Light
        {
            public byte X;
            public byte Y;
            public byte Width;
            public byte Height;
        }

        public struct NPCs
        {
            public string Name;
            public short Texture;
            public byte Type;
            public short[] Vital;
        }

        public struct Map_NPCs
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animation;
            public bool Atacando;
            public int Attack_Time;
            public int Suffering;
            // Geral
            public short Index;
            public byte X;
            public byte Y;
            public Jogo.Location Direction;
            public Jogo.Movements Movement;
            public short[] Vital;
        }

        public struct Map_Items
        {
            public short Index;
            public byte X;
            public byte Y;
        }

        public struct Items
        {
            // Geral
            public string Name;
            public string Description;
            public short Texture;
            public byte Type;
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

        public struct Hotbar
        {
            public byte Type;
            public byte Slot;
        }
    }
}