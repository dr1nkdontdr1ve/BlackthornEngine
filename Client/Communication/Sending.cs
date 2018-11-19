using System;
using Lidgren.Network;

partial class Sending
{
    // Packages do cliente
    public enum Packages
    {
        Latency,
        Connect,
        Register,
        CreateCharacter,
        Character_Use,
        Character_Create,
        Character_Delete,
        Player_Direction,
        Player_Move,
        Request_Map,
        Message,
        Player_Attack,
        AddPoints,
        CollectItem,
        SoltarItem,
        Inventory_Change,
        Inventory_Use,
        Equipment_Remove,
        Hotbar_Add,
        Hotbar_Change,
        Hotbar_Use
    }

    public static void Package(NetOutgoingMessage Data)
    {
        // Envia os Data ao servidor
        Network.Device.SendMessage(Data, NetDeliveryMethod.ReliableOrdered);
    }

    public static void Connect()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Connect);
        Data.Write(Scanners.Locate("Connect_User").Text);
        Data.Write(Scanners.Locate("Connect_Senha").Text);
        Package(Data);
    }

    public static void Register()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Register);
        Data.Write(Scanners.Locate("Register_User").Text);
        Data.Write(Scanners.Locate("Register_Senha").Text);
        Package(Data);
    }

    public static void CreateCharacter()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.CreateCharacter);
        Data.Write(Scanners.Locate("CreateCharacter_Name").Text);
        Data.Write(Game.CreateCharacter_Classe);
        Data.Write(Markers.Locate("GenreMasculino").State);
        Package(Data);
    }

    public static void Character_Use()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Character_Use);
        Data.Write(Game.SelectCharacter);
        Package(Data);
    }

    public static void Character_Create()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Character_Create);
        Package(Data);
    }

    public static void Character_Delete()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Character_Delete);
        Data.Write(Game.SelectCharacter);
        Package(Data);
    }

    public static void Request_Map(bool Necessário)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Request_Map);
        Data.Write(Necessário);
        Package(Data);
    }

    public static void Latency()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Latency);
        Package(Data);

        // Define a contaem na hora do envio
        Game.Latency_Envio = Environment.TickCount;
    }

    public static void Message(string Message, Game.Mensagens Type, string Dado = "")
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Message);
        Data.Write(Message);
        Data.Write((byte)Type);
        Data.Write(Dado);
        Package(Data);
    }

    public static void AddPoints(Game.Attributes Attribute)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.AddPoints);
        Data.Write((byte)Attribute);
        Package(Data);
    }

    public static void CollectItem()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.CollectItem);
        Package(Data);
    }

    public static void SoltarItem(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.SoltarItem);
        Data.Write(Slot);
        Package(Data);
    }

    public static void Inventory_Change(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Inventory_Change);
        Data.Write(Antigo);
        Data.Write(Novo);
        Package(Data);
    }

    public static void Inventory_Use(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Inventory_Use);
        Data.Write(Slot);
        Package(Data);
    }

    public static void Equipment_Remove(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Equipment_Remove);
        Data.Write(Slot);
        Package(Data);
    }

    public static void Hotbar_Add(byte Hotbar_Slot, byte Type, byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Hotbar_Add);
        Data.Write(Hotbar_Slot);
        Data.Write(Type);
        Data.Write(Slot);
        Package(Data);
    }

    public static void Hotbar_Change(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Hotbar_Change);
        Data.Write(Antigo);
        Data.Write(Novo);
        Package(Data);
    }

    public static void Hotbar_Use(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Hotbar_Use);
        Data.Write(Slot);
        Package(Data);
    }
}