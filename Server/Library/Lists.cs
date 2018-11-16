using System.Collections.Generic;

class Lists
{
    // Data storage
    public static Structures.Servidor_Dados Server_Data = new Structures.Server_Data();
    public static Structures.Player[] Player;
    public static Structures.TempPlayer[] TempJogador;
    public static Structures.Classes[] Classe;
    public static Structures.Mapas[] Mapa;
    public static Structures.NPCs[] NPC;
    public static Structures.Items[] Item;

    // Structure of the items in general
    public class Structures
    {
        public struct Servidor_Dados
        {
            public string Game_Nome;
            public string Mensagem;
            public short Porta;
            public byte Máx_Jogadores;
            public byte Máx_Personagens;
            public byte Num_Classes;
            public byte Num_Azulejos;
            public short Num_Mapas;
            public short Num_NPCs;
            public short Num_Itens;
        }

        public struct Player
        {
            public string Usuário;
            public string Senha;
            public Game.Acessos Acesso;
            public global::Player.Personagem_Estrutura[] Personagem;
        }

        public struct TempPlayer
        {
            public bool Jogando;
            public byte Utilizado;
            public bool ObtendoMapa;
        }

        public struct Classes
        {
            public string Nome;
            public short Textura_Masculina;
            public short Textura_Feminina;
            public short Aparecer_Mapa;
            public byte Aparecer_Direção;
            public byte Aparecer_X;
            public byte Aparecer_Y;
            public short[] Vital;
            public short[] Atributo;
        }

        public struct Mapas
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
            public Map_Climate Clima;
            public Map_Smoke Fumaça;
            public short[] Ligação;
            public byte LightGlobal;
            public byte Iluminação;
            public Light[] Light;
            public Mapa_NPC[] NPC;

            // Temporário
            public Mapa_NPCs[] Temp_NPC;
            public List<Mapa_Itens> Temp_Item;
        }

        public struct Mapa_NPC
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

        public struct Mapa_NPCs
        {
            public short Index;
            public byte X;
            public byte Y;
            public Game.Direções Direção;
            public byte Target_Type;
            public byte Target_Index;
            public short[] Vital;
            public int Appearance_Time;
            public int Attack_Time;
        }

        public struct Mapa_Itens
        {
            public short Index;
            public byte X;
            public byte Y;
            public short Quantidade;
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
            public short Poção_Experiência;
            public short[] Poção_Vital;
            // Equipment
            public byte Equip_Tipo;
            public short[] Equip_Atributo;
            public short Arma_Dano;
        }

        public struct Inventory
        {
            public short Item_Num;
            public short Quantidade;
        }

        public struct NPC_Queda
        {
            public short Item_Num;
            public short Quantidade;
            public byte Chance;
        }

        public struct Hotbar
        {
            public byte Type;
            public byte Slot;
        }
    }
}