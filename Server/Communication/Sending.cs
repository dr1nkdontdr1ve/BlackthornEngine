using System;
using System.Drawing;
using Lidgren.Network;

partial class Sending
{
    // Server Packages
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
        Player_Equipment,
        Player_Hotbar,
        Enter_Map,
        Map_Revisão,
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
        Map_Items,
    }

    public static void Para(byte Index, NetOutgoingMessage Data)
    {
        // Previni sobrecarga
        if (!Network.IsConnected(Index)) return;

        // Recria o pacote e o envia
        NetOutgoingMessage Data_Sending = Network.Device.CreateMessage(Data.LengthBytes);
        Data_Sending.Write(Data);
        Network.Device.SendMessage(Data_Sending, Network.Connection[Index], NetDeliveryMethod.ReliableOrdered);
    }

    public static void ParaTodos(NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                Para(i, Data);
    }

    public static void ParaTodosMenos(byte Index, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do Index
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Index != i)
                    Para(i, Data);
    }

    public static void ParaMap(short Map, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do Index
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Player.Character(i).Map == Map)
                    Para(i, Data);
    }

    public static void ParaMapMenos(short Map, byte Index, NetOutgoingMessage Data)
    {
        // Envia os Data para todos conectados, com excessão do Index
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Player.Character(i).Map == Map)
                    if (Index != i)
                        Para(i, Data);
    }

    public static void Alert(byte Index, string Message, bool Desconectar = true)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Alert);
        Data.Write(Message);
        Para(Index, Data);

        // Desconecta o Player
        if (Desconectar)
            Network.Connection[Index].Disconnect(string.Empty);
    }

    public static void Message(string Text)
    {
        // Send the alert to everyone
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Network.Connection[i] != null)
                if (Network.Connection[i].Status == NetConnectionStatus.Connected)
                    Alert(i, Text);
    }

    public static void Conectar(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Connect);
        Data.Write(Index);
        Para(Index, Data);
    }

    public static void CreateCharacter(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.CreateCharacter);
        Para(Index, Data);
    }

    public static void Entrada(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Entrada);
        Data.Write(Index);
        Data.Write(Game.BiggerIndex);
        Data.Write(Lists.Server_Data.Max_Players);
        Para(Index, Data);
    }

    public static void Characters(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Send the data
        Data.Write((byte)Packages.Characters);
        Data.Write(Lists.Server_Data.Max_Characters);

        for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
        {
            Data.Write(Lists.Player[Index].Character[i].Name);
            Data.Write(Lists.Player[Index].Character[i].Classe);
            Data.Write(Lists.Player[Index].Character[i].Genre);
            Data.Write(Lists.Player[Index].Character[i].Level);
        }

        Para(Index, Data);
    }

    public static void Classes(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Send the data
        Data.Write((byte)Packages.Classes);
        Data.Write(Lists.Server_Data.Num_Classes);

        for (byte i = 1; i <= Lists.Classe.GetUpperBound(0); i++)
        {
            Data.Write(Lists.Classe[i].Name);
            Data.Write(Lists.Classe[i].Texture_Male);
            Data.Write(Lists.Classe[i].Texture_Female);
        }

        // Send the data
        Para(Index, Data);
    }

    public static void Entrar(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Entrar);
        Para(Index, Data);
    }

    public static NetOutgoingMessage Player_Data_Cache(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Escreve os Data
        Data.Write((byte)Packages.Player_Data);
        Data.Write(Index);
        Data.Write(Player.Character(Index).Name);
        Data.Write(Player.Character(Index).Classe);
        Data.Write(Player.Character(Index).Genre);
        Data.Write(Player.Character(Index).Level);
        Data.Write(Player.Character(Index).Map);
        Data.Write(Player.Character(Index).X);
        Data.Write(Player.Character(Index).Y);
        Data.Write((byte)Player.Character(Index).Direction);
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
        {
            Data.Write(Player.Character(Index).Vital[n]);
            Data.Write(Player.Character(Index).MaxVital(n));
        }
        for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Data.Write(Player.Character(Index).Attribute[n]);
        for (byte n = 0; n <= (byte)Game.Equipment.Amount - 1; n++) Data.Write(Player.Character(Index).Equipment[n]);

        return Data;
    }

    public static void Player_Position(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Position);
        Data.Write(Index);
        Data.Write(Player.Character(Index).X);
        Data.Write(Player.Character(Index).Y);
        Data.Write((byte)Player.Character(Index).Direction);
        ParaMap(Player.Character(Index).Map, Data);
    }

    public static void Player_Vital(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Vital);
        Data.Write(Index);
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++)
        {
            Data.Write(Player.Character(Index).Vital[i]);
            Data.Write(Player.Character(Index).MaxVital(i));
        }

        ParaMap(Player.Character(Index).Map, Data);
    }

    public static void Player_Exited(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Exited);
        Data.Write(Index);
        ParaTodosMenos(Index, Data);
    }

    public static void BiggerIndex()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.BiggerIndex);
        Data.Write(Game.BiggerIndex);
        ParaTodos(Data);
    }

    public static void Player_Move(byte Index, byte Movement)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Move);
        Data.Write(Index);
        Data.Write(Player.Character(Index).X);
        Data.Write(Player.Character(Index).Y);
        Data.Write((byte)Player.Character(Index).Direction);
        Data.Write(Movement);
        ParaMapMenos(Player.Character(Index).Map, Index, Data);
    }

    public static void Player_Direction(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Direction);
        Data.Write(Index);
        Data.Write((byte)Player.Character(Index).Direction);
        ParaMapMenos(Player.Character(Index).Map, Index, Data);
    }

    public static void Player_Experience(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Experience);
        Data.Write(Player.Character(Index).Experience);
        Data.Write(Player.Character(Index).ExpRequired);
        Data.Write(Player.Character(Index).Points);
        Para(Index, Data);
    }

    public static void Player_Equipment(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Equipment);
        Data.Write(Index);
        for (byte i = 0; i <= (byte)Game.Equipment.Amount - 1; i++) Data.Write(Player.Character(Index).Equipment[i]);
        ParaMap(Player.Character(Index).Map, Data);
    }

    public static void Players_Data_Map(byte Index)
    {
        // Envia os Data dos outros Playeres 
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Index != i)
                    if (Player.Character(i).Map == Player.Character(Index).Map)
                        Para(Index, Player_Data_Cache(i));

        // Envia os Data do Player
        ParaMap(Player.Character(Index).Map, Player_Data_Cache(Index));
    }

    public static void Enter_Map(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Enter_Map);
        Para(Index, Data);
    }

    public static void Leave_Map(byte Index, short Map)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Exited);
        Data.Write(Index);
        ParaMapMenos(Map, Index, Data);
    }

    public static void Map_Revisão(byte Index, short Map)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_Revisão);
        Data.Write(Map);
        Data.Write(Lists.Map[Map].Revisão);
        Para(Index, Data);
    }

    public static void Map(byte Index, short Map)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map);
        Data.Write(Map);
        Data.Write(Lists.Map[Map].Revisão);
        Data.Write(Lists.Map[Map].Name);
        Data.Write(Lists.Map[Map].Width);
        Data.Write(Lists.Map[Map].Height);
        Data.Write(Lists.Map[Map].Moral);
        Data.Write(Lists.Map[Map].Panorama);
        Data.Write(Lists.Map[Map].Music);
        Data.Write(Lists.Map[Map].Coloração);
        Data.Write(Lists.Map[Map].Climate.Type);
        Data.Write(Lists.Map[Map].Climate.Intensity);
        Data.Write(Lists.Map[Map].Smoke.Texture);
        Data.Write(Lists.Map[Map].Smoke.VelocityX);
        Data.Write(Lists.Map[Map].Smoke.VelocityY);
        Data.Write(Lists.Map[Map].Smoke.Transparency);

        // Ligações
        for (short i = 0; i <= (short)Game.Location.Amount - 1; i++)
            Data.Write(Lists.Map[Map].Ligação[i]);

        // Tiles
        Data.Write((byte)Lists.Map[Map].Tile[0, 0].Data.GetUpperBound(1));
        for (byte x = 0; x <= Lists.Map[Map].Width; x++)
            for (byte y = 0; y <= Lists.Map[Map].Height; y++)
                for (byte c = 0; c <= (byte)global::Map.Layers.Amount - 1; c++)
                    for (byte q = 0; q <= Lists.Map[Map].Tile[x, y].Data.GetUpperBound(1); q++)
                    {
                        Data.Write(Lists.Map[Map].Tile[x, y].Data[c, q].x);
                        Data.Write(Lists.Map[Map].Tile[x, y].Data[c, q].y);
                        Data.Write(Lists.Map[Map].Tile[x, y].Data[c, q].Tile);
                        Data.Write(Lists.Map[Map].Tile[x, y].Data[c, q].Automatic);
                    }

        // Data específicos dos Tiles
        for (byte x = 0; x <= Lists.Map[Map].Width; x++)
            for (byte y = 0; y <= Lists.Map[Map].Height; y++)
            {
                Data.Write(Lists.Map[Map].Tile[x, y].Attribute);
                for (byte i = 0; i <= (byte)Game.Location.Amount - 1; i++)
                    Data.Write(Lists.Map[Map].Tile[x, y].Block[i]);
            }

        // Lights
        Data.Write(Lists.Map[Map].Light.GetUpperBound(0));
        if (Lists.Map[Map].Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map[Map].Light.GetUpperBound(0); i++)
            {
                Data.Write(Lists.Map[Map].Light[i].X);
                Data.Write(Lists.Map[Map].Light[i].Y);
                Data.Write(Lists.Map[Map].Light[i].Width);
                Data.Write(Lists.Map[Map].Light[i].Height);
            }

        // NPCs
        Data.Write((short)Lists.Map[Map].NPC.GetUpperBound(0));
        if (Lists.Map[Map].NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Map[Map].NPC.GetUpperBound(0); i++)
                Data.Write(Lists.Map[Map].NPC[i].Index);

        Para(Index, Data);
    }

    public static void Latency(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Latency);
        Para(Index, Data);
    }

    public static void Message(byte Index, string Message, Color Cor)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Message);
        Data.Write(Message);
        Data.Write(Cor.ToArgb());
        Para(Index, Data);
    }

    public static void Message_Map(byte Index, string Text)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();
        string Message = "[Map] " + Player.Character(Index).Name + ": " + Text;

        // Envia os Data
        Data.Write((byte)Packages.Message);
        Data.Write(Message);
        Data.Write(Color.White.ToArgb());
        ParaMap(Player.Character(Index).Map, Data);
    }

    public static void Global_Message(byte Index, string Text)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();
        string Message = "[Global] " + Player.Character(Index).Name + ": " + Text;

        // Envia os Data
        Data.Write((byte)Packages.Message);
        Data.Write(Message);
        Data.Write(Color.Yellow.ToArgb());
        ParaTodos(Data);
    }

    public static void Global_Message(string Message)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Message);
        Data.Write(Message);
        Data.Write(Color.Yellow.ToArgb());
        ParaTodos(Data);
    }

    public static void Private_Message(byte Index, string Destinatário_Name, string Text)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();
        byte Destinatário = Player.Encontrar(Destinatário_Name);

        // Verifica se o Player está conectado
        if (Destinatário == 0)
        {
            Message(Index, Destinatário_Name + " não está conectado no momento.", Color.Blue);
            return;
        }

        // Envia as mensagens
        Message(Index, "[Para] " + Destinatário_Name + ": " + Text, Color.Pink);
        Message(Destinatário, "[De] " + Player.Character(Index).Name + ": " + Text, Color.Pink);
    }

    public static void Player_Attack(byte Index, byte Victim, byte Victim_Type)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Attack);
        Data.Write(Index);
        Data.Write(Victim);
        Data.Write(Victim_Type);
        ParaMap(Player.Character(Index).Map, Data);
    }

    public static void Items(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Items);
        Data.Write((byte)Lists.Item.GetUpperBound(0));

        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
        {
            // Geral
            Data.Write(Lists.Item[i].Name);
            Data.Write(Lists.Item[i].Description);
            Data.Write(Lists.Item[i].Texture);
            Data.Write(Lists.Item[i].Type);
            Data.Write(Lists.Item[i].Req_Level);
            Data.Write(Lists.Item[i].Req_Classe);
            Data.Write(Lists.Item[i].Potion_Experience);
            for (byte n = 0;n<= (byte)Game.Vital.Amount - 1; n++) Data.Write(Lists.Item[i].Potion_Vital[n]);
            Data.Write(Lists.Item[i].Equip_Type );
            for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Data.Write(Lists.Item[i].Equip_Attribute[n]);
            Data.Write(Lists.Item[i].Weapon_Damage);
        }

        // Envia os Data
        Para(Index, Data);
    }

    public static void Map_Items(byte Index, short Map_Num)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_Items);
        Data.Write((short)(Lists.Map[Map_Num].Temp_Item.Count - 1));

        for (byte i = 1; i <= Lists.Map[Map_Num].Temp_Item.Count - 1; i++)
        {
            // Geral
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].Index);
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].X);
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].Y);
        }

        // Envia os Data
        Para(Index, Data);
    }

    public static void Map_Items(short Map_Num)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_Items);
        Data.Write((short)(Lists.Map[Map_Num].Temp_Item.Count - 1));
        for (byte i = 1; i <= Lists.Map[Map_Num].Temp_Item.Count - 1; i++)
        {
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].Index);
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].X);
            Data.Write(Lists.Map[Map_Num].Temp_Item[i].Y);
        }
        ParaMap(Map_Num, Data);
    }

    public static void Player_Inventory(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Inventory);
        for (byte i = 1; i <= Game.Max_Inventory; i++)
        {
            Data.Write(Player.Character(Index).Inventory[i].Item_Num);
            Data.Write(Player.Character(Index).Inventory[i].Amount);
        }
        Para(Index, Data);
    }

    public static void Player_Hotbar(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Hotbar);
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
        {
            Data.Write(Player.Character(Index).Hotbar[i].Type);
            Data.Write(Player.Character(Index).Hotbar[i].Slot);
        }
        Para(Index, Data);
    }
}