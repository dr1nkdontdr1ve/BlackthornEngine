using System;
using System.Windows.Forms;
using System.Drawing;
using Lidgren.Network;

partial class Receiving
{
    // Pacotes do servidor
    public enum Pacotes
    {
        Alerta,
        Conectar,
        CriarCharacter,
        Entrada,
        Classes,
        Personagens,
        Entrar,
        MaiorIndex,
        Player_Data,
        Player_Posição,
        Player_Vital,
        Player_Saiu,
        Player_Atacar,
        Player_Mover,
        Player_Direction,
        Player_Experiência,
        Player_Inventory,
        Player_Equipamentos,
        Player_Hotbar,
        EntrarNoMap,
        Map_Revisão,
        Map,
        Latência,
        Mensagem,
        NPCs,
        Map_NPCs,
        Map_NPC,
        Map_NPC_Movimento,
        Map_NPC_Direction,
        Map_NPC_Vital,
        Map_NPC_Atacar,
        Map_NPC_Morreu,
        Itens,
        Map_Itens
    }

    public static void Data(NetIncomingMessage Data)
    {
        // Manuseia os Data recebidos
        switch ((Pacotes)Data.ReadByte())
        {
            case Pacotes.Alerta: Alerta(Data); break;
            case Pacotes.Conectar: Conectar(Data); break;
            case Pacotes.Entrada: Entrada(Data); break;
            case Pacotes.CriarCharacter: CriarCharacter(Data); break;
            case Pacotes.Entrar: Entrar(Data); break;
            case Pacotes.Classes: Classes(Data); break;
            case Pacotes.Personagens: Personagens(Data); break;
            case Pacotes.MaiorIndex: MaiorIndex(Data); break;
            case Pacotes.Player_Data: Player_Data(Data); break;
            case Pacotes.Player_Posição: Player_Posição(Data); break;
            case Pacotes.Player_Vital: Player_Vital(Data); break;
            case Pacotes.Player_Mover: Player_Mover(Data); break;
            case Pacotes.Player_Saiu: Player_Saiu(Data); break;
            case Pacotes.Player_Direction: Player_Direction(Data); break;
            case Pacotes.Player_Atacar: Player_Atacar(Data); break;
            case Pacotes.Player_Experiência: Player_Experiência(Data); break;
            case Pacotes.Player_Inventory: Player_Inventory(Data); break;
            case Pacotes.Player_Equipamentos: Player_Equipamentos(Data); break;
            case Pacotes.Player_Hotbar: Player_Hotbar(Data); break;
            case Pacotes.Map_Revisão: Map_Revisão(Data); break;
            case Pacotes.Map: Map(Data); break;
            case Pacotes.EntrarNoMap: EntrarNoMap(Data); break;
            case Pacotes.Latência: Latência(Data); break;
            case Pacotes.Mensagem: Mensagem(Data); break;
            case Pacotes.NPCs: NPCs(Data); break;
            case Pacotes.Map_NPCs: Map_NPCs(Data); break;
            case Pacotes.Map_NPC: Map_NPC(Data); break;
            case Pacotes.Map_NPC_Movimento: Map_NPC_Movimento(Data); break;
            case Pacotes.Map_NPC_Direction: Map_NPC_Direction(Data); break;
            case Pacotes.Map_NPC_Vital: Map_NPC_Vital(Data); break;
            case Pacotes.Map_NPC_Atacar: Map_NPC_Atacar(Data); break;
            case Pacotes.Map_NPC_Morreu: Map_NPC_Morreu(Data); break;
            case Pacotes.Itens: Itens(Data); break;
            case Pacotes.Map_Itens: Map_Itens(Data); break;
        }
    }

    private static void Alerta(NetIncomingMessage Data)
    {
        // Mostra a mensagem
        MessageBox.Show(Data.ReadString());
    }

    private static void Conectar(NetIncomingMessage Data)
    {
        // Reseta os valores
        Game.SelecionarCharacter = 1;

        // Abre o painel de seleção de personagens
        Paineis.Menu_Fechar();
        Paineis.Encontrar("SelecionarCharacter").Geral.Visível = true;
    }

    private static void Entrada(NetIncomingMessage Data)
    {
        // Definir os valores que são enviados do servidor
        Player.MyIndex = Data.ReadByte();
        Player.MaiorIndex = Data.ReadByte();

        // Limpa a estrutura dos Playeres
        Lists.Player = new Lists.Structures.Player[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Player.GetUpperBound(0); i++)
            Clean.Player(i);
    }

    private static void CriarCharacter(NetIncomingMessage Data)
    {
        // Reseta os valores
        Digitalizadores.Encontrar("CriarCharacter_Name").Texto = string.Empty;
        Marcadores.Encontrar("GêneroMasculino").Estado = true;
        Marcadores.Encontrar("GêneroFeminino").Estado = false;
        Game.CriarCharacter_Classe = 1;

        // Abre o painel de criação de Character
        Paineis.Menu_Fechar();
        Paineis.Encontrar("CriarCharacter").Geral.Visível = true;
    }

    private static void Classes(NetIncomingMessage Data)
    {
        int Amount = Data.ReadByte();

        // Recebe os Data das classes
        Lists.Classe = new Lists.Structures.Classe[Amount + 1];

        for (byte i = 1; i <= Amount; i++)
        {
            // Recebe os Data do Character
            Lists.Classe[i] = new Lists.Structures.Classe();
            Lists.Classe[i].Name = Data.ReadString();
            Lists.Classe[i].Texture_Masculina = Data.ReadInt16();
            Lists.Classe[i].Texture_Feminina = Data.ReadInt16();
        }
    }

    private static void Personagens(NetIncomingMessage Data)
    {
        byte Amount = Data.ReadByte();

        // Redimensiona a lista
        Lists.Servidor_Data.Max_Personagens = Amount;
        Lists.Personagens = new Lists.Structures.Character[Amount + 1];

        for (byte i = 1; i <= Amount; i++)
        {
            // Recebe os Data do Character
            Lists.Personagens[i] = new Lists.Structures.Character();
            Lists.Personagens[i].Name = Data.ReadString();
            Lists.Personagens[i].Classe = Data.ReadByte();
            Lists.Personagens[i].Gênero = Data.ReadBoolean();
            Lists.Personagens[i].Level = Data.ReadInt16();
        }
    }

    private static void Entrar(NetIncomingMessage Data)
    {
        // Reseta os valores
        Ferramentas.Chat = new System.Collections.Generic.List<Ferramentas.Chat_Estrutura>();
        Digitalizadores.Encontrar("Chat").Texto = string.Empty;
        Paineis.Encontrar("Chat").Geral.Visível = false;
        Ferramentas.Linha = 0;

        // Abre o Game
        Áudio.Música.Parar();
        Ferramentas.JanelaAtual = Ferramentas.Janelas.Game;
    }

    private static void MaiorIndex(NetIncomingMessage Data)
    {
        // Define o número maior de Index
        Player.MaiorIndex = Data.ReadByte();
    }

    public static void Map_Revisão(NetIncomingMessage Data)
    {
        bool Necessário = false;
        int Map = Data.ReadInt16();

        // Limpa todos os outros Playeres
        for (byte i = 1; i <= Player.MaiorIndex; i++)
            if (i != Player.MyIndex)
                Clean.Player(i);

        // Verifica se é necessário baixar os Data do Map

        Necessário = true;

        // Solicita os Data do Map
        Sending.Solicitar_Map(Necessário);
    }

    public static void Map(NetIncomingMessage Data)
    {
        // Define os Data
        short Map_Num = Data.ReadInt16();
        Lists.Map.Revisão = Data.ReadInt16();
        Lists.Map.Name = Data.ReadString();
        Lists.Map.Largura = Data.ReadByte();
        Lists.Map.Altura = Data.ReadByte();
        Lists.Map.Moral = Data.ReadByte();
        Lists.Map.Panorama = Data.ReadByte();
        Lists.Map.Música = Data.ReadByte();
        Lists.Map.Coloração = Data.ReadInt32();
        Lists.Map.Clima.Type = Data.ReadByte();
        Lists.Map.Clima.Intensidade = Data.ReadByte();
        Lists.Map.Fumaça.Texture = Data.ReadByte();
        Lists.Map.Fumaça.VelocidadeX = Data.ReadSByte();
        Lists.Map.Fumaça.VelocidadeY = Data.ReadSByte();
        Lists.Map.Fumaça.Transparência = Data.ReadByte();

        // Redimensiona as ligações
        Lists.Map.Ligação = new short[(byte)Game.Direções.Amount];

        for (short i = 0; i <= (short)Game.Direções.Amount - 1; i++)
            Lists.Map.Ligação[i] = Data.ReadInt16();

        // Redimensiona os azulejos
        Lists.Map.Azulejo = new Lists.Structures.Azulejo[Lists.Map.Largura + 1, Lists.Map.Altura + 1];

        // Lê os Data
        byte NumCamadas = Data.ReadByte();
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
            {
                // Redimensiona os Data dos azulejos
                Lists.Map.Azulejo[x, y].Data = new Lists.Structures.Azulejo_Data[(byte)global::Map.Camadas.Amount, NumCamadas + 1];

                for (byte c = 0; c <= (byte)global::Map.Camadas.Amount - 1; c++)
                    for (byte q = 0; q <= NumCamadas; q++)
                    {
                        Lists.Map.Azulejo[x, y].Data[c, q].x = Data.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].y = Data.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].Azulejo = Data.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].Automático = Data.ReadBoolean();
                        Lists.Map.Azulejo[x, y].Data[c, q].Mini = new Point[4];
                    }
            }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
            {
                Lists.Map.Azulejo[x, y].Atributo = Data.ReadByte();
                Lists.Map.Azulejo[x, y].Bloqueio = new bool[(byte)Game.Direções.Amount];
                for (byte i = 0; i <= (byte)Game.Direções.Amount - 1; i++)
                    Lists.Map.Azulejo[x, y].Bloqueio[i] = Data.ReadBoolean();
            }

        // Luzes
        Lists.Map.Luz = new Lists.Structures.Luz[Data.ReadInt32() + 1];
        if (Lists.Map.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Luz.GetUpperBound(0); i++)
            {
                Lists.Map.Luz[i].X = Data.ReadByte();
                Lists.Map.Luz[i].Y = Data.ReadByte();
                Lists.Map.Luz[i].Largura = Data.ReadByte();
                Lists.Map.Luz[i].Altura = Data.ReadByte();
            }

        // NPCs
        Lists.Map.NPC = new short[Data.ReadInt16() + 1];
        if (Lists.Map.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Map.NPC.GetUpperBound(0); i++)
                Lists.Map.NPC[i] = Data.ReadInt16();

        // Salva o Map
        Escrever.Map(Map_Num);

        // Redimensiona as partículas do clima
        global::Map.Clima_Ajustar();
        global::Map.AutoCriação.Atualizar();
    }

    public static void EntrarNoMap(NetIncomingMessage Data)
    {
        // Se tiver, reproduz a música de fundo do Map
        if (Lists.Map.Música > 0)
            Áudio.Música.Reproduzir((Áudio.Músicas)Lists.Map.Música);
        else
            Áudio.Música.Parar();
    }

    public static void Latência(NetIncomingMessage Data)
    {
        // Define a latência
        Game.Latência = Environment.TickCount - Game.Latência_Envio;
    }

    public static void Mensagem(NetIncomingMessage Data)
    {
        // Adiciona a mensagem
        string Texto = Data.ReadString();
        Color Cor = Color.FromArgb(Data.ReadInt32());
        Ferramentas.Adicionar(Texto, new SFML.Graphics.Color(Cor.R, Cor.G, Cor.B));
    }

    public static void Itens(NetIncomingMessage Data)
    {
        // Amount
        Lists.Item = new Lists.Structures.Itens[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
        {
            // Redimensiona os valores necessários 
            Lists.Item[i].Poção_Vital = new short[(byte)Game.Vital.Amount];
            Lists.Item[i].Equip_Atributo = new short[(byte)Game.Atributos.Amount];

            // Lê os Data
            Lists.Item[i].Name = Data.ReadString();
            Lists.Item[i].Descrição = Data.ReadString();
            Lists.Item[i].Texture = Data.ReadInt16();
            Lists.Item[i].Type = Data.ReadByte();
            Lists.Item[i].Req_Level = Data.ReadInt16();
            Lists.Item[i].Req_Classe = Data.ReadByte();
            Lists.Item[i].Poção_Experiência = Data.ReadInt16();
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Lists.Item[i].Poção_Vital[n] = Data.ReadInt16();
            Lists.Item[i].Equip_Type = Data.ReadByte();
            for (byte n = 0; n <= (byte)Game.Atributos.Amount - 1; n++) Lists.Item[i].Equip_Atributo[n] = Data.ReadInt16();
            Lists.Item[i].Arma_Dano = Data.ReadInt16();
        }
    }

    public static void Map_Itens(NetIncomingMessage Data)
    {
        // Amount
        Lists.Map.Temp_Item = new Lists.Structures.Map_Itens[Data.ReadInt16() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.Map.Temp_Item.GetUpperBound(0); i++)
        {
            // Geral
            Lists.Map.Temp_Item[i].Index = Data.ReadInt16();
            Lists.Map.Temp_Item[i].X = Data.ReadByte();
            Lists.Map.Temp_Item[i].Y = Data.ReadByte();
        }
    }
}