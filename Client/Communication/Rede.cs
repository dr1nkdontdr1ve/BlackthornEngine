using System;
using Lidgren.Network;

class Rede
{
    // Protocolo do controle de transmissão
    public static NetClient Dispositivo;

    // Manuseamento dos dados
    public static NetIncomingMessage Dados;

    // Dados para a conexão com o servidor
    public const string IP = "localhost";
    public const short Porta = 7001;

    public static void Iniciar()
    {
        NetPeerConfiguration Configurações = new NetPeerConfiguration("CryBits");

        // Cria o dispositivo com as devidas configurações
        Dispositivo = new NetClient(Configurações);
        Dispositivo.Start();
    }

    public static void Desconectar()
    {
        // Acaba com a conexão
        if (Dispositivo != null)
            Dispositivo.Disconnect(string.Empty);
    }

    public static void ReceberDados()
    {
        // Lê e direciona todos os dados recebidos
        while ((Dados = Dispositivo.ReadMessage()) != null)
        {
            switch (Dados.MessageType)
            {
                // Recebe e manuseia os dados
                case NetIncomingMessageType.Data:
                    Receber.Dados(Dados);
                    break;
                // Desconectar o jogador caso o servidor seja desligado
                case NetIncomingMessageType.StatusChanged:
                    if ((NetConnectionStatus)Dados.ReadByte() == NetConnectionStatus.Disconnected)
                        Game.Desconectar();

                    break;
            }

            Dispositivo.Recycle(Dados);
        }
    }

    public static bool EstáConectado()
    {
        // Retorna um valor de acordo com o estado da conexão do jogador
        if (Dispositivo.ConnectionStatus == NetConnectionStatus.Connected)
            return true;
        else
            return false;
    }

    public static bool TentarConectar()
    {
        int Espera = Environment.TickCount;

        // Se o jogador já estiver conectado, então isso não é mais necessário
        if (EstáConectado()) return true;

        // Tenta se conectar
        Dispositivo.Connect(IP, Porta);

        // Espere até que o jogador se conecte
        while (!EstáConectado() && Environment.TickCount <= Espera + 1000)
            ReceberDados();

        return EstáConectado();
    }
}