using System;
using Lidgren.Network;

class Network
{
    // Protocolo do controle de transmissão
    public static NetClient Dispositivo;

    // Manuseamento dos Data
    public static NetIncomingMessage Data;

    // Data para a conexão com o servidor
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

    public static void ReceberData()
    {
        // Lê e direciona todos os Data recebidos
        while ((Data = Dispositivo.ReadMessage()) != null)
        {
            switch (Data.MessageType)
            {
                // Recebe e manuseia os Data
                case NetIncomingMessageType.Data:
                    Receber.Data(Data);
                    break;
                // Desconectar o jogador caso o servidor seja desligado
                case NetIncomingMessageType.StatusChanged:
                    if ((NetConnectionStatus)Data.ReadByte() == NetConnectionStatus.Disconnected)
                        Game.Desconectar();

                    break;
            }

            Dispositivo.Recycle(Data);
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
            ReceberData();

        return EstáConectado();
    }
}