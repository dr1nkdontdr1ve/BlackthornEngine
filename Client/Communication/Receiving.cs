using System;
using System.Windows.Forms;
using System.Drawing;
using Lidgren.Network;

partial class Receiving
{
    // Packages do servidor
    public enum Packages
    {
        Alert,
        Connect,
        CreateCharacter,
        Entrada,
        Classes,
        Characters,
        Entrar,
        BiggerIndex,
        Player_Data,
        Player_Position,
        Player_Vital,
        Player_Exited,
        Player_Attack,
        Player_Move,
        Player_Direction,
        Player_Experience,
        Player_Inventory,
        Player_Equipments,
        Player_Hotbar,
        EntrarNoMap,
        Map_ReView,
        Map,
        Latency,
        Message,
        NPCs,
        Map_NPCs,
        Map_NPC,
        Map_NPC_Movement,
        Map_NPC_Direction,
        Map_NPC_Vital,
        Map_NPC_Attack,
        Map_NPC_Died,
        Items,
        Map_Items
    }

    public static void Data(NetIncomingMessage Data)
    {
        // Manuseia os Data recebidos
        switch ((Packages)Data.ReadByte())
        {
            case Packages.Alert: Alert(Data); break;
            case Packages.Connect: Connect(Data); break;
            case Packages.Entrada: Entrada(Data); break;
            case Packages.CreateCharacter: CreateCharacter(Data); break;
            case Packages.Entrar: Entrar(Data); break;
            case Packages.Classes: Classes(Data); break;
            case Packages.Characters: Characters(Data); break;
            case Packages.BiggerIndex: BiggerIndex(Data); break;
            case Packages.Player_Data: Player_Data(Data); break;
            case Packages.Player_Position: Player_Position(Data); break;
            case Packages.Player_Vital: Player_Vital(Data); break;
            case Packages.Player_Move: Player_Move(Data); break;
            case Packages.Player_Exited: Player_Exited(Data); break;
            case Packages.Player_Direction: Player_Direction(Data); break;
            case Packages.Player_Attack: Player_Attack(Data); break;
            case Packages.Player_Experience: Player_Experience(Data); break;
            case Packages.Player_Inventory: Player_Inventory(Data); break;
            case Packages.Player_Equipments: Player_Equipments(Data); break;
            case Packages.Player_Hotbar: Player_Hotbar(Data); break;
            case Packages.Map_ReView: Map_Review(Data); break;
            case Packages.Map: Map(Data); break;
            case Packages.EntrarNoMap: EntrarNoMap(Data); break;
            case Packages.Latency: Latency(Data); break;
            case Packages.Message: Message(Data); break;
            case Packages.NPCs: NPCs(Data); break;
            case Packages.Map_NPCs: Map_NPCs(Data); break;
            case Packages.Map_NPC: Map_NPC(Data); break;
            case Packages.Map_NPC_Movement: Map_NPC_Movement(Data); break;
            case Packages.Map_NPC_Direction: Map_NPC_Direction(Data); break;
            case Packages.Map_NPC_Vital: Map_NPC_Vital(Data); break;
            case Packages.Map_NPC_Attack: Map_NPC_Attack(Data); break;
            case Packages.Map_NPC_Died: Map_NPC_Died(Data); break;
            case Packages.Items:Items(Data); break;
            case Packages.Map_Items: Map_Items(Data); break;
        }
    }

    private static void Alert(NetIncomingMessage Data)
    {
        // Mostra a Message
        MessageBox.Show(Data.ReadString());
    }

    private static void Connect(NetIncomingMessage Data)
    {
        // Reseta os valores
        Jogo.SelectCharacter = 1;

        // Abre o Panel de seleção de Characters
        Panels.Menu_Close();
        Panels.Locate("SelectCharacter").Geral.Visible = true;
    }

    private static void Entrada(NetIncomingMessage Data)
    {
        // Definir os valores que são enviados do servidor
        Player.MyIndex = Data.ReadByte();
        Player.BiggerIndex = Data.ReadByte();

        // Limpa a Structure dos Playeres
        Lists.Player = new Lists.Structures.Player[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Player.GetUpperBound(0); i++)
            Clean.Player(i);
    }

    private static void CreateCharacter(NetIncomingMessage Data)
    {
        // Reseta os valores
        Scanners.Locate("CreateCharacter_Name").Text = string.Empty;
        Markers.Locate("GenreMasculino").State = true;
        Markers.Locate("GenreFeminino").State = false;
        Jogo.CreateCharacter_Classe = 1;

        // Abre o Panel de criação de Character
        Panels.Menu_Close();
        Panels.Locate("CreateCharacter").Geral.Visible = true;
    }

    private static void Classes(NetIncomingMessage Data)
    {
        int Amount = Data.ReadByte();

        // Recebe os Data das classes
        Lists.Classe = new Lists.Structures.Classe[Amount + 1];

        for (byte i = 1; i <= Amount; i++)
        {
            // Recebe os Data do Character
            Lists.Classe[i] = new Lists.Structures.Classe();
            Lists.Classe[i].Name = Data.ReadString();
            Lists.Classe[i].Texture_Male = Data.ReadInt16();
            Lists.Classe[i].Texture_Female = Data.ReadInt16();
        }
    }

    private static void Characters(NetIncomingMessage Data)
    {
        byte Amount = Data.ReadByte();

        // Redimensiona a List
        Lists.Server_Data.Max_Characters = Amount;
        Lists.Characters = new Lists.Structures.Character[Amount + 1];

        for (byte i = 1; i <= Amount; i++)
        {
            // Recebe os Data do Character
            Lists.Characters[i] = new Lists.Structures.Character();
            Lists.Characters[i].Name = Data.ReadString();
            Lists.Characters[i].Classe = Data.ReadByte();
            Lists.Characters[i].Genre = Data.ReadBoolean();
            Lists.Characters[i].Level = Data.ReadInt16();
        }
    }

    private static void Entrar(NetIncomingMessage Data)
    {
        // Reseta os valores
        Tools.Chat = new System.Collections.Generic.List<Tools.Chat_Structure>();
        Scanners.Locate("Chat").Text = string.Empty;
        Panels.Locate("Chat").Geral.Visible = false;
        Tools.Line = 0;

        // Abre o Jogo
        Audio.Música.Stop();
        Tools.CurrentWindow = Tools.Windows.Jogo;
    }

    private static void BiggerIndex(NetIncomingMessage Data)
    {
        // Define o número maior de Index
        Player.BiggerIndex = Data.ReadByte();
    }

    public static void Map_Review(NetIncomingMessage Data)
    {
        bool Necessário = false;
        int Map = Data.ReadInt16();

        // Limpa todos os outros Playeres
        for (byte i = 1; i <= Player.BiggerIndex; i++)
            if (i != Player.MyIndex)
                Clean.Player(i);

        // Verifica se é necessário baixar os Data do Map

        Necessário = true;

        // Solicita os Data do Map
        Sending.Request_Map(Necessário);
    }

    public static void Map(NetIncomingMessage Data)
    {
        // Define os Data
        short Map_Num = Data.ReadInt16();
        Lists.Map.Review = Data.ReadInt16();
        Lists.Map.Name = Data.ReadString();
        Lists.Map.Width = Data.ReadByte();
        Lists.Map.Height = Data.ReadByte();
        Lists.Map.Moral = Data.ReadByte();
        Lists.Map.Panorama = Data.ReadByte();
        Lists.Map.Music = Data.ReadByte();
        Lists.Map.Coloração = Data.ReadInt32();
        Lists.Map.Climate.Type = Data.ReadByte();
        Lists.Map.Climate.Intensity = Data.ReadByte();
        Lists.Map.Smoke.Texture = Data.ReadByte();
        Lists.Map.Smoke.VelocityX = Data.ReadSByte();
        Lists.Map.Smoke.VelocityY = Data.ReadSByte();
        Lists.Map.Smoke.Transparency = Data.ReadByte();

        // Redimensiona as ligações
        Lists.Map.Ligação = new short[(byte)Jogo.Location.Amount];

        for (short i = 0; i <= (short)Jogo.Location.Amount - 1; i++)
            Lists.Map.Ligação[i] = Data.ReadInt16();

        // Redimensiona os Tiles
        Lists.Map.Tile = new Lists.Structures.Tile[Lists.Map.Width + 1, Lists.Map.Height + 1];

        // Lê os Data
        byte NumLayers = Data.ReadByte();
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
            {
                // Redimensiona os Data dos Tiles
                Lists.Map.Tile[x, y].Data = new Lists.Structures.Tile_Data[(byte)global::Map.Layers.Amount, NumLayers + 1];

                for (byte c = 0; c <= (byte)global::Map.Layers.Amount - 1; c++)
                    for (byte q = 0; q <= NumLayers; q++)
                    {
                        Lists.Map.Tile[x, y].Data[c, q].x = Data.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].y = Data.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].Tile = Data.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].Automático = Data.ReadBoolean();
                        Lists.Map.Tile[x, y].Data[c, q].Mini = new Point[4];
                    }
            }

        // Data específicos dos Tiles
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
            {
                Lists.Map.Tile[x, y].Attribute = Data.ReadByte();
                Lists.Map.Tile[x, y].Block = new bool[(byte)Jogo.Location.Amount];
                for (byte i = 0; i <= (byte)Jogo.Location.Amount - 1; i++)
                    Lists.Map.Tile[x, y].Block[i] = Data.ReadBoolean();
            }

        // Lightes
        Lists.Map.Light = new Lists.Structures.Light[Data.ReadInt32() + 1];
        if (Lists.Map.Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Light.GetUpperBound(0); i++)
            {
                Lists.Map.Light[i].X = Data.ReadByte();
                Lists.Map.Light[i].Y = Data.ReadByte();
                Lists.Map.Light[i].Width = Data.ReadByte();
                Lists.Map.Light[i].Height = Data.ReadByte();
            }

        // NPCs
        Lists.Map.NPC = new short[Data.ReadInt16() + 1];
        if (Lists.Map.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Map.NPC.GetUpperBound(0); i++)
                Lists.Map.NPC[i] = Data.ReadInt16();

        // Salva o Map
        Write.Map(Map_Num);

        // Redimensiona as Particles do Climate
        global::Map.Climate_Ajustar();
        global::Map.AutoCriação.Update();
    }

    public static void EntrarNoMap(NetIncomingMessage Data)
    {
        // Se tiver, reproduz a Music de fundo do Map
        if (Lists.Map.Music > 0)
            Audio.Música.Reproduce((Audio.Músicas)Lists.Map.Music);
        else
            Audio.Música.Stop();
    }

    public static void Latency(NetIncomingMessage Data)
    {
        // Define a Latency
        Jogo.Latency = Environment.TickCount - Jogo.Latency_Envio;
    }

    public static void Message(NetIncomingMessage Data)
    {
        // Adiciona a Message
        string Text = Data.ReadString();
        Color Cor = Color.FromArgb(Data.ReadInt32());
        Tools.Add(Text, new SFML.Graphics.Color(Cor.R, Cor.G, Cor.B));
    }

    public static void Items(NetIncomingMessage Data)
    {
        // Amount
        Lists.Item = new Lists.Structures.Items[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
        {
            // Redimensiona os valores necessários 
            Lists.Item[i].Potion_Vital = new short[(byte)Jogo.Vital.Amount];
            Lists.Item[i].Equip_Attribute = new short[(byte)Jogo.Attributes.Amount];

            // Lê os Data
            Lists.Item[i].Name = Data.ReadString();
            Lists.Item[i].Description = Data.ReadString();
            Lists.Item[i].Texture = Data.ReadInt16();
            Lists.Item[i].Type = Data.ReadByte();
            Lists.Item[i].Req_Level = Data.ReadInt16();
            Lists.Item[i].Req_Classe = Data.ReadByte();
            Lists.Item[i].Potion_Experience = Data.ReadInt16();
            for (byte n = 0; n <= (byte)Jogo.Vital.Amount - 1; n++) Lists.Item[i].Potion_Vital[n] = Data.ReadInt16();
            Lists.Item[i].Equip_Type = Data.ReadByte();
            for (byte n = 0; n <= (byte)Jogo.Attributes.Amount - 1; n++) Lists.Item[i].Equip_Attribute[n] = Data.ReadInt16();
            Lists.Item[i].Weapon_Damage = Data.ReadInt16();
        }
    }

    public static void Map_Items(NetIncomingMessage Data)
    {
        // Amount
        Lists.Map.Temp_Item = new Lists.Structures.Map_Items[Data.ReadInt16() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.Map.Temp_Item.GetUpperBound(0); i++)
        {
            // Geral
            Lists.Map.Temp_Item[i].Index = Data.ReadInt16();
            Lists.Map.Temp_Item[i].X = Data.ReadByte();
            Lists.Map.Temp_Item[i].Y = Data.ReadByte();
        }
    }
}