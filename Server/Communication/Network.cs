using Lidgren.Network;

class Network
{
    // Protocolo do controle de transmissão
    public static NetServer Device;

    // Lista dos jogadores conectados
    public static NetConnection[] Connection;

    public static void Start()
    {
        NetPeerConfiguration Configuration;
        Connection = new NetConnection[Lists.Server_Data.Max_Players + 1];

        // Sets some network settings
        Configuration = new NetPeerConfiguration("BlackthornEngine");
        Configuration.Port = Lists.Server_Data.Porta;
        Configuration.AcceptIncomingConnections = true;
        Configuration.MaximumConnections = Lists.Server_Data.Max_Players;

        // Cria o Device com as devidas configurações
        Device = new NetServer(Configuration);
        Device.Start();
    }

    public static void ReceivingData()
    {
        NetIncomingMessage Data;
        byte Index;

        // Lê e direciona todos os Data recebidos
        while ((Data = Device.ReadMessage()) != null)
        {
            // Jogador que está a enviar os Data
            Index = EncontrarConnection(Data.SenderConnection);

            switch (Data.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus Status = (NetConnectionStatus)Data.ReadByte();

                    // Nova conexão - Conecta o jogador ao Game
                    if (Status == NetConnectionStatus.Connected)
                        Connect(Data);
                    // Conexão perdida, disconecta o jogador do Game
                    else if (Status == NetConnectionStatus.Disconnected)
                        Disconnect(Index);

                    break;
                // Recebe e manuseia os Data recebidos
                case NetIncomingMessageType.Data:
                    Receiving.ForwardData(Index, Data);
                    break;
            }

            Device.Recycle(Data);
        }
    }

    public static void Connect(NetIncomingMessage IncomingMsg)
    {
        // Define a conexão do jogador
        Connection[EncontrarConnection(null)] = IncomingMsg.SenderConnection;

        // Redefine o Index máximo de jogadores
        Game.ResetBiggerIndex();
    }

    public static void Disconnect(byte Index)
    {
        // Redefine o maior Index dos jogadores
        Game.ResetBiggerIndex();

        // Acaba com a conexão e restabelece os Data do jogador
        Connection[Index] = null;
        Player.Leave(Index);
    }

    public static bool IsConnected(byte Index)
    {
        // Previni sobrecarga
        if (Connection[Index] == null)
            return false;

        // Retorna um valor de acordo com a conexão do jogador
        if (Connection[Index].Status == NetConnectionStatus.Connected)
            return true;
        else
            return false;
    }

    public static byte EncontrarConnection(NetConnection Busca)
    {
        // Encontra uma determinada conexão
        for (byte i = 1; i <= Lists.Server_Data.Max_Players; i++)
            if (Connection[i] == Busca)
                return i;

        return 0;
    }
}
