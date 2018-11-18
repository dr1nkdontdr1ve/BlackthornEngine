using System;
using Lidgren.Network;

class Network
{
    // Transmission Control Protocol
    public static NetClient Device;

    // Handling of Data
    public static NetIncomingMessage Data;

    // Data for connecting to the server
    public const string IP = "localhost";
    public const short Port = 7001;

    public static void Start()
    {
        NetPeerConfiguration Settings = new NetPeerConfiguration("CryBits");

        // Creates the Device with the appropriate settings
        Device = new NetClient(Settings);
        Device.Start();
    }

    public static void Disconnect()
    {
        // End the connection
        if (Device != null)
            Device.Disconnect(string.Empty);
    }

    public static void ReceivingData()
    {
        // Reads and directs all received Data
        while ((Data = Device.ReadMessage()) != null)
        {
            switch (Data.MessageType)
            {
                // Receive and handle the data
                case NetIncomingMessageType.Data:
                    Receiving.Data(Data);
                    break;
                // Disconnect the player if the server is disconnected
                case NetIncomingMessageType.StatusChanged:
                    if ((NetConnectionStatus)Data.ReadByte() == NetConnectionStatus.Disconnected)
                        Game.Desconectar();

                    break;
            }

            Device.Recycle(Data);
        }
    }

    public static bool IsConnected()
    {
        // Returns a value according to the state of the player connection
        if (Device.ConnectionStatus == NetConnectionStatus.Connected)
            return true;
        else
            return false;
    }

    public static bool TryConnect()
    {
        int Wait = Environment.TickCount;

        // If the player is already logged in, then this is no longer necessary.
        if (IsConnected()) return true;

        // Try to connect
        Device.Connect(IP, Port);

        // Wait until the player connects
        while (!IsConnected() && Environment.TickCount <= Wait + 1000)
            ReceivingData();

        return IsConnected();
    }
}