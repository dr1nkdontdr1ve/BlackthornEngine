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
        CriarPersonagem,
        Personagem_Usar,
        Personagem_Criar,
        Personagem_Deletar,
        Jogador_Direção,
        Jogador_Mover,
        Solicitar_Mapa,
        Mensagem,
        Jogador_Atacar,
        AdicionarPonto,
        ColetarItem,
        SoltarItem,
        Inventário_Mudar,
        Inventário_Usar,
        Equipamento_Remover,
        Hotbar_Adicionar,
        Hotbar_Mudar,
        Hotbar_Usar
    }

    public static void Pacote(NetOutgoingMessage Data)
    {
        // Envia os Data ao servidor
        Rede.Dispositivo.SendMessage(Data, NetDeliveryMethod.ReliableOrdered);
    }

    public static void Conectar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Conectar);
        Data.Write(Digitalizadores.Encontrar("Conectar_Usuário").Texto);
        Data.Write(Digitalizadores.Encontrar("Conectar_Senha").Texto);
        Pacote(Data);
    }

    public static void Registrar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Registrar);
        Data.Write(Digitalizadores.Encontrar("Registrar_Usuário").Texto);
        Data.Write(Digitalizadores.Encontrar("Registrar_Senha").Texto);
        Pacote(Data);
    }

    public static void CriarPersonagem()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.CriarPersonagem);
        Data.Write(Digitalizadores.Encontrar("CriarPersonagem_Name").Texto);
        Data.Write(Game.CriarPersonagem_Classe);
        Data.Write(Marcadores.Encontrar("GêneroMasculino").Estado);
        Pacote(Data);
    }

    public static void Personagem_Usar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Personagem_Usar);
        Data.Write(Game.SelecionarPersonagem);
        Pacote(Data);
    }

    public static void Personagem_Criar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Personagem_Criar);
        Pacote(Data);
    }

    public static void Personagem_Deletar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Personagem_Deletar);
        Data.Write(Game.SelecionarPersonagem);
        Pacote(Data);
    }

    public static void Solicitar_Mapa(bool Necessário)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Solicitar_Mapa);
        Data.Write(Necessário);
        Pacote(Data);
    }

    public static void Latência()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Latência);
        Pacote(Data);

        // Define a contaem na hora do envio
        Game.Latência_Envio = Environment.TickCount;
    }

    public static void Mensagem(string Mensagem, Game.Mensagens Tipo, string Dado = "")
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Mensagem);
        Data.Write(Mensagem);
        Data.Write((byte)Tipo);
        Data.Write(Dado);
        Pacote(Data);
    }

    public static void AdicionarPonto(Game.Atributos Atributo)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.AdicionarPonto);
        Data.Write((byte)Atributo);
        Pacote(Data);
    }

    public static void ColetarItem()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.ColetarItem);
        Pacote(Data);
    }

    public static void SoltarItem(byte Slot)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.SoltarItem);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Inventário_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Inventário_Mudar);
        Data.Write(Antigo);
        Data.Write(Novo);
        Pacote(Data);
    }

    public static void Inventário_Usar(byte Slot)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Inventário_Usar);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Equipamento_Remover(byte Slot)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Equipamento_Remover);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Hotbar_Adicionar(byte Hotbar_Slot, byte Tipo, byte Slot)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Adicionar);
        Data.Write(Hotbar_Slot);
        Data.Write(Tipo);
        Data.Write(Slot);
        Pacote(Data);
    }

    public static void Hotbar_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Mudar);
        Data.Write(Antigo);
        Data.Write(Novo);
        Pacote(Data);
    }

    public static void Hotbar_Usar(byte Slot)
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Hotbar_Usar);
        Data.Write(Slot);
        Pacote(Data);
    }
}