using System;
using Lidgren.Network;

partial class Sending
{
    // Pacotes do cliente
    public enum Pacotes
    {
        Latência,
        Conectar,
        Registrar,
        CriarCharacter,
        Character_Usar,
        Character_Criar,
        Character_Deletar,
        Player_Direction,
        Player_Mover,
        Solicitar_Map,
        Mensagem,
        Player_Atacar,
        AdicionarPonto,
        ColetarItem,
        SoltarItem,
        Inventory_Mudar,
        Inventory_Usar,
        Equipamento_Remover,
        Hotbar_Adicionar,
        Hotbar_Mudar,
        Hotbar_Usar
    }

    public static void Pacote(NetOutgoingMessage Data)
    {
        // Envia os Data ao servidor
        Network.Device.SendMessage(Data, NetDeliveryMethod.ReliableOrdered);
    }

    public static void Conectar()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Conectar);
        Data.Write(Digitalizadores.Encontrar("Conectar_Usuário").Texto);
        Data.Write(Digitalizadores.Encontrar("Conectar_Senha").Texto);
        Pacote(Data);
    }

    public static void Registrar()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Registrar);
        Data.Write(Digitalizadores.Encontrar("Registrar_Usuário").Texto);
        Data.Write(Digitalizadores.Encontrar("Registrar_Senha").Texto);
        Pacote(Data);
    }

    public static void CriarCharacter()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.CriarCharacter);
        Data.Write(Digitalizadores.Encontrar("CriarCharacter_Name").Texto);
        Data.Write(Game.CriarCharacter_Classe);
        Data.Write(Marcadores.Encontrar("GêneroMasculino").Estado);
        Pacote(Data);
    }

    public static void Character_Usar()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Character_Usar);
        Data.Write(Game.SelecionarCharacter);
        Pacote(Data);
    }

    public static void Character_Criar()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Character_Criar);
        Pacote(Data);
    }

    public static void Character_Deletar()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Character_Deletar);
        Data.Write(Game.SelecionarCharacter);
        Pacote(Data);
    }

    public static void Solicitar_Map(bool Necessário)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Solicitar_Map);
        Data.Write(Necessário);
        Pacote(Data);
    }

    public static void Latência()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Latência);
        Pacote(Data);

        // Define a contaem na hora do envio
        Game.Latência_Envio = Environment.TickCount;
    }

    public static void Mensagem(string Mensagem, Game.Mensagens Type, string Dado = "")
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Mensagem);
        Data.Write(Mensagem);
        Data.Write((byte)Type);
        Data.Write(Dado);
        Pacote(Data);
    }

    public static void AdicionarPonto(Game.Atributos Atributo)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.AdicionarPonto);
        Data.Write((byte)Atributo);
        Pacote(Data);
    }

    public static void ColetarItem()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.ColetarItem);
        Pacote(Data);
    }

    public static void SoltarItem(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.SoltarItem);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Inventory_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Inventory_Mudar);
        Data.Write(Antigo);
        Data.Write(Novo);
        Pacote(Data);
    }

    public static void Inventory_Usar(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Inventory_Usar);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Equipamento_Remover(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Equipamento_Remover);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Hotbar_Adicionar(byte Hotbar_Slot, byte Type, byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Adicionar);
        Data.Write(Hotbar_Slot);
        Data.Write(Type);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Hotbar_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Mudar);
        Data.Write(Antigo);
        Data.Write(Novo);
        Pacote(Data);
    }

    public static void Hotbar_Usar(byte Slot)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Usar);
        Data.Write(Slot);
        Pacote(Data);
    }
}