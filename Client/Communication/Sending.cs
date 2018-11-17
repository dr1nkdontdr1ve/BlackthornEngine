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

    public static void Pacote(NetOutgoingMessage Dados)
    {
        // Envia os dados ao servidor
        Rede.Dispositivo.SendMessage(Dados, NetDeliveryMethod.ReliableOrdered);
    }

    public static void Conectar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Conectar);
        Dados.Write(Digitalizadores.Encontrar("Conectar_Usuário").Texto);
        Dados.Write(Digitalizadores.Encontrar("Conectar_Senha").Texto);
        Pacote(Dados);
    }

    public static void Registrar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Registrar);
        Dados.Write(Digitalizadores.Encontrar("Registrar_Usuário").Texto);
        Dados.Write(Digitalizadores.Encontrar("Registrar_Senha").Texto);
        Pacote(Dados);
    }

    public static void CriarPersonagem()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.CriarPersonagem);
        Dados.Write(Digitalizadores.Encontrar("CriarPersonagem_Nome").Texto);
        Dados.Write(Game.CriarPersonagem_Classe);
        Dados.Write(Marcadores.Encontrar("GêneroMasculino").Estado);
        Pacote(Dados);
    }

    public static void Personagem_Usar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Personagem_Usar);
        Dados.Write(Game.SelecionarPersonagem);
        Pacote(Dados);
    }

    public static void Personagem_Criar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Personagem_Criar);
        Pacote(Dados);
    }

    public static void Personagem_Deletar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Personagem_Deletar);
        Dados.Write(Game.SelecionarPersonagem);
        Pacote(Dados);
    }

    public static void Solicitar_Mapa(bool Necessário)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Solicitar_Mapa);
        Dados.Write(Necessário);
        Pacote(Dados);
    }

    public static void Latência()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Latência);
        Pacote(Dados);

        // Define a contaem na hora do envio
        Game.Latência_Envio = Environment.TickCount;
    }

    public static void Mensagem(string Mensagem, Game.Mensagens Tipo, string Dado = "")
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mensagem);
        Dados.Write(Mensagem);
        Dados.Write((byte)Tipo);
        Dados.Write(Dado);
        Pacote(Dados);
    }

    public static void AdicionarPonto(Game.Atributos Atributo)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.AdicionarPonto);
        Dados.Write((byte)Atributo);
        Pacote(Dados);
    }

    public static void ColetarItem()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.ColetarItem);
        Pacote(Dados);
    }

    public static void SoltarItem(byte Slot)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.SoltarItem);
        Dados.Write(Slot);
        Pacote(Dados);
    }

    public static void Inventário_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Inventário_Mudar);
        Dados.Write(Antigo);
        Dados.Write(Novo);
        Pacote(Dados);
    }

    public static void Inventário_Usar(byte Slot)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Inventário_Usar);
        Dados.Write(Slot);
        Pacote(Dados);
    }

    public static void Equipamento_Remover(byte Slot)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Equipamento_Remover);
        Dados.Write(Slot);
        Pacote(Dados);
    }

    public static void Hotbar_Adicionar(byte Hotbar_Slot, byte Tipo, byte Slot)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Hotbar_Adicionar);
        Dados.Write(Hotbar_Slot);
        Dados.Write(Tipo);
        Dados.Write(Slot);
        Pacote(Dados);
    }

    public static void Hotbar_Mudar(byte Antigo, byte Novo)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Hotbar_Mudar);
        Dados.Write(Antigo);
        Dados.Write(Novo);
        Pacote(Dados);
    }

    public static void Hotbar_Usar(byte Slot)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Hotbar_Usar);
        Dados.Write(Slot);
        Pacote(Dados);
    }
}