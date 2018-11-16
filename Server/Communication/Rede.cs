using Lidgren.Network;

class Rede
{
    // Protocolo do controle de transmissão
    public static NetServer Dispositivo;

    // Lista dos jogadores conectados
    public static NetConnection[] Conexão;

    public static void Iniciar()
    {
        NetPeerConfiguration Configuração;
        Conexão = new NetConnection[Listas.Servidor_Dados.Máx_Jogadores + 1];

        // Define algumas configurações da rede
        Configuração = new NetPeerConfiguration("CryBits");
        Configuração.Port = Listas.Servidor_Dados.Porta;
        Configuração.AcceptIncomingConnections = true;
        Configuração.MaximumConnections = Listas.Servidor_Dados.Máx_Jogadores;

        // Cria o dispositivo com as devidas configurações
        Dispositivo = new NetServer(Configuração);
        Dispositivo.Start();
    }

    public static void ReceberDados()
    {
        NetIncomingMessage Dados;
        byte Índice;

        // Lê e direciona todos os dados recebidos
        while ((Dados = Dispositivo.ReadMessage()) != null)
        {
            // Jogador que está a enviar os dados
            Índice = EncontrarConexão(Dados.SenderConnection);

            switch (Dados.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                    NetConnectionStatus Status = (NetConnectionStatus)Dados.ReadByte();

                    // Nova conexão - Conecta o jogador ao Game
                    if (Status == NetConnectionStatus.Connected)
                        Conectar(Dados);
                    // Conexão perdida, disconecta o jogador do Game
                    else if (Status == NetConnectionStatus.Disconnected)
                        Desconectar(Índice);

                    break;
                // Recebe e manuseia os dados recebidos
                case NetIncomingMessageType.Data:
                    Receber.EncaminharDados(Índice, Dados);
                    break;
            }

            Dispositivo.Recycle(Dados);
        }
    }

    public static void Conectar(NetIncomingMessage IncomingMsg)
    {
        // Define a conexão do jogador
        Conexão[EncontrarConexão(null)] = IncomingMsg.SenderConnection;

        // Redefine o índice máximo de jogadores
        Game.RedefinirMaiorÍndice();
    }

    public static void Desconectar(byte Índice)
    {
        // Redefine o maior índice dos jogadores
        Game.RedefinirMaiorÍndice();

        // Acaba com a conexão e restabelece os dados do jogador
        Conexão[Índice] = null;
        Jogador.Sair(Índice);
    }

    public static bool EstáConectado(byte Índice)
    {
        // Previni sobrecarga
        if (Conexão[Índice] == null)
            return false;

        // Retorna um valor de acordo com a conexão do jogador
        if (Conexão[Índice].Status == NetConnectionStatus.Connected)
            return true;
        else
            return false;
    }

    public static byte EncontrarConexão(NetConnection Busca)
    {
        // Encontra uma determinada conexão
        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Jogadores; i++)
            if (Conexão[i] == Busca)
                return i;

        return 0;
    }
}
