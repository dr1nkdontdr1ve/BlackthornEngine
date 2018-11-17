public class Lists
{
    // Armazenamento de Data
    public static Structures.Options Options = new Structures.Options();
    public static Structures.Cliente_Data Cliente_Data = new Structures.Client_Data();
    public static Structures.Servidor_Data Servidor_Data = new Structures.Server_Data();
    public static Structures.Player[] Player;
    public static Structures.Classe[] Classe;
    public static Structures.Character[] Characters;
    public static Structures.Maps Map;
    public static Structures.Climate[] Climate_Particles;
    public static Structures.NPCs[] NPC;
    public static Structures.Items[] Item;

    // Estrutura dos itens em gerais
    public class Structures
    {
        public struct Options
        {
            public string Game_Name;
            public bool SalvarUsuário;
            public bool Sons;
            public bool Músicas;
            public string Usuário;
        }

        public struct Client_Data
        {
            public byte Num_Botões;
            public byte Num_Paineis;
            public byte Num_Marcadores;
            public byte Num_Digitalizadores;
        }

        public struct Server_Data
        {
            public byte Máx_Jogadores;
            public byte Máx_Personagens;
            public byte Num_Classes;
            public byte Num_Azulejos;
            public short Num_Mapas;
        }

        public class Player
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animação;
            public bool Atacando;
            public int Ataque_Tempo;
            public int Sofrendo;
            public short[] Máx_Vital;
            public int Coletar_Tempo;
            // Geral
            public string Name;
            public byte Classe;
            public bool Gênero;
            public short Level;
            public short Experiência;
            public short ExpNecessária;
            public short Pontos;
            public short[] Vital;
            public short[] Atributo;
            public short Mapa;
            public byte X;
            public byte Y;
            public Game.Direções Direção;
            public Game.Movimentos Movimento;
            public short[] Equipamento;
        }

        public class Character
        {
            public string Name;
            public byte Classe;
            public bool Gênero;
            public short Level;
        }

        public class Classe
        {
            public string Name;
            public short Textura_Masculina;
            public short Textura_Feminina;
            public short Aparecer_Mapa;
            public byte Aparecer_Direção;
            public byte Aparecer_X;
            public byte Aparecer_Y;
        }
        
        public struct Maps
        {
            public short Revisão;
            public string Name;
            public byte Largura;
            public byte Altura;
            public byte Moral;
            public byte Panorama;
            public byte Música;
            public int Coloração;
            public Mapa_Clima Clima;
            public Mapa_Fumaça Fumaça;
            public short[] Ligação;
            public Azulejo[,] Azulejo;
            public Luz[] Luz;
            public short[] NPC;

            // Temporário
            public Mapa_NPCs[] Temp_NPC;
            public Mapa_Itens[] Temp_Item;
        }

        public struct Map_Climate
        {
            public byte Tipo;
            public byte Intensidade;
        }

        public struct Map_Smoke
        {
            public byte Textura;
            public sbyte VelocidadeX;
            public sbyte VelocidadeY;
            public byte Transparência;
        }

        public struct Tile
        {
            public byte Atributo;
            public bool[] Bloqueio;
            public Azulejo_Data[,] Data;
        }

        public struct Tile_Data
        {
            public byte x;
            public byte y;
            public byte Azulejo;
            public bool Automático;
            public System.Drawing.Point[] Mini;
        }

        public struct Climate
        {
            public bool Visível;
            public int x;
            public int y;
            public int Velocidade;
            public int Inicío;
            public bool Voltar;
        }

        public class Light
        {
            public byte X;
            public byte Y;
            public byte Largura;
            public byte Altura;
        }

        public struct NPCs
        {
            public string Name;
            public short Textura;
            public byte Tipo;
            public short[] Vital;
        }

        public struct Map_NPCs
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animação;
            public bool Atacando;
            public int Ataque_Tempo;
            public int Sofrendo;
            // Geral
            public short Índice;
            public byte X;
            public byte Y;
            public Game.Direções Direção;
            public Game.Movimentos Movimento;
            public short[] Vital;
        }

        public struct Map_Items
        {
            public short Índice;
            public byte X;
            public byte Y;
        }

        public struct Items
        {
            // General
            public string Name;
            public string Descrição;
            public short Textura;
            public byte Tipo;
            // Requirements
            public short Req_Level;
            public byte Req_Classe;
            // Potion
            public short Poção_Experiência;
            public short[] Poção_Vital;
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