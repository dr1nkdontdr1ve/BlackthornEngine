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
        CriarPersonagem,
        Entrada,
        Classes,
        Personagens,
        Entrar,
        MaiorÍndice,
        Jogador_Data,
        Jogador_Posição,
        Jogador_Vitais,
        Jogador_Saiu,
        Jogador_Atacar,
        Jogador_Mover,
        Jogador_Direção,
        Jogador_Experiência,
        Jogador_Inventário,
        Jogador_Equipamentos,
        Jogador_Hotbar,
        EntrarNoMapa,
        Mapa_Revisão,
        Mapa,
        Latência,
        Mensagem,
        NPCs,
        Mapa_NPCs,
        Mapa_NPC,
        Mapa_NPC_Movimento,
        Mapa_NPC_Direção,
        Mapa_NPC_Vitais,
        Mapa_NPC_Atacar,
        Mapa_NPC_Morreu,
        Itens,
        Mapa_Itens
    }

    public static void Data(NetIncomingMessage Data)
    {
        // Manuseia os Data recebidos
        switch ((Pacotes)Data.ReadByte())
        {
            case Pacotes.Alerta: Alerta(Data); break;
            case Pacotes.Conectar: Conectar(Data); break;
            case Pacotes.Entrada: Entrada(Data); break;
            case Pacotes.CriarPersonagem: CriarPersonagem(Data); break;
            case Pacotes.Entrar: Entrar(Data); break;
            case Pacotes.Classes: Classes(Data); break;
            case Pacotes.Personagens: Personagens(Data); break;
            case Pacotes.MaiorÍndice: MaiorÍndice(Data); break;
            case Pacotes.Jogador_Data: Jogador_Data(Data); break;
            case Pacotes.Jogador_Posição: Jogador_Posição(Data); break;
            case Pacotes.Jogador_Vitais: Jogador_Vitais(Data); break;
            case Pacotes.Jogador_Mover: Jogador_Mover(Data); break;
            case Pacotes.Jogador_Saiu: Jogador_Saiu(Data); break;
            case Pacotes.Jogador_Direção: Jogador_Direção(Data); break;
            case Pacotes.Jogador_Atacar: Jogador_Atacar(Data); break;
            case Pacotes.Jogador_Experiência: Jogador_Experiência(Data); break;
            case Pacotes.Jogador_Inventário: Jogador_Inventário(Data); break;
            case Pacotes.Jogador_Equipamentos: Jogador_Equipamentos(Data); break;
            case Pacotes.Jogador_Hotbar: Jogador_Hotbar(Data); break;
            case Pacotes.Mapa_Revisão: Mapa_Revisão(Data); break;
            case Pacotes.Mapa: Mapa(Data); break;
            case Pacotes.EntrarNoMapa: EntrarNoMapa(Data); break;
            case Pacotes.Latência: Latência(Data); break;
            case Pacotes.Mensagem: Mensagem(Data); break;
            case Pacotes.NPCs: NPCs(Data); break;
            case Pacotes.Mapa_NPCs: Mapa_NPCs(Data); break;
            case Pacotes.Mapa_NPC: Mapa_NPC(Data); break;
            case Pacotes.Mapa_NPC_Movimento: Mapa_NPC_Movimento(Data); break;
            case Pacotes.Mapa_NPC_Direção: Mapa_NPC_Direção(Data); break;
            case Pacotes.Mapa_NPC_Vitais: Mapa_NPC_Vitais(Data); break;
            case Pacotes.Mapa_NPC_Atacar: Mapa_NPC_Atacar(Data); break;
            case Pacotes.Mapa_NPC_Morreu: Mapa_NPC_Morreu(Data); break;
            case Pacotes.Itens: Itens(Data); break;
            case Pacotes.Mapa_Itens: Mapa_Itens(Data); break;
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
        Game.SelecionarPersonagem = 1;

        // Abre o painel de seleção de personagens
        Paineis.Menu_Fechar();
        Paineis.Encontrar("SelecionarPersonagem").Geral.Visível = true;
    }

    private static void Entrada(NetIncomingMessage Data)
    {
        // Definir os valores que são enviados do servidor
        Jogador.MeuÍndice = Data.ReadByte();
        Jogador.MaiorÍndice = Data.ReadByte();

        // Limpa a estrutura dos jogadores
        Lists.Jogador = new Lists.Estruturas.Jogador[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Jogador.GetUpperBound(0); i++)
            Limpar.Jogador(i);
    }

    private static void CriarPersonagem(NetIncomingMessage Data)
    {
        // Reseta os valores
        Digitalizadores.Encontrar("CriarPersonagem_Name").Texto = string.Empty;
        Marcadores.Encontrar("GêneroMasculino").Estado = true;
        Marcadores.Encontrar("GêneroFeminino").Estado = false;
        Game.CriarPersonagem_Classe = 1;

        // Abre o painel de criação de personagem
        Paineis.Menu_Fechar();
        Paineis.Encontrar("CriarPersonagem").Geral.Visível = true;
    }

    private static void Classes(NetIncomingMessage Data)
    {
        int Quantidade = Data.ReadByte();

        // Recebe os Data das classes
        Lists.Classe = new Lists.Estruturas.Classe[Quantidade + 1];

        for (byte i = 1; i <= Quantidade; i++)
        {
            // Recebe os Data do personagem
            Lists.Classe[i] = new Lists.Estruturas.Classe();
            Lists.Classe[i].Name = Data.ReadString();
            Lists.Classe[i].Textura_Masculina = Data.ReadInt16();
            Lists.Classe[i].Textura_Feminina = Data.ReadInt16();
        }
    }

    private static void Personagens(NetIncomingMessage Data)
    {
        byte Quantidade = Data.ReadByte();

        // Redimensiona a lista
        Lists.Servidor_Data.Máx_Personagens = Quantidade;
        Lists.Personagens = new Lists.Estruturas.Personagem[Quantidade + 1];

        for (byte i = 1; i <= Quantidade; i++)
        {
            // Recebe os Data do personagem
            Lists.Personagens[i] = new Lists.Estruturas.Personagem();
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

    private static void MaiorÍndice(NetIncomingMessage Data)
    {
        // Define o número maior de índice
        Jogador.MaiorÍndice = Data.ReadByte();
    }

    public static void Mapa_Revisão(NetIncomingMessage Data)
    {
        bool Necessário = false;
        int Mapa = Data.ReadInt16();

        // Limpa todos os outros jogadores
        for (byte i = 1; i <= Jogador.MaiorÍndice; i++)
            if (i != Jogador.MeuÍndice)
                Limpar.Jogador(i);

        // Verifica se é necessário baixar os Data do mapa

        Necessário = true;

        // Solicita os Data do mapa
        Enviar.Solicitar_Mapa(Necessário);
    }

    public static void Mapa(NetIncomingMessage Data)
    {
        // Define os Data
        short Mapa_Num = Data.ReadInt16();
        Lists.Mapa.Revisão = Data.ReadInt16();
        Lists.Mapa.Name = Data.ReadString();
        Lists.Mapa.Largura = Data.ReadByte();
        Lists.Mapa.Altura = Data.ReadByte();
        Lists.Mapa.Moral = Data.ReadByte();
        Lists.Mapa.Panorama = Data.ReadByte();
        Lists.Mapa.Música = Data.ReadByte();
        Lists.Mapa.Coloração = Data.ReadInt32();
        Lists.Mapa.Clima.Tipo = Data.ReadByte();
        Lists.Mapa.Clima.Intensidade = Data.ReadByte();
        Lists.Mapa.Fumaça.Textura = Data.ReadByte();
        Lists.Mapa.Fumaça.VelocidadeX = Data.ReadSByte();
        Lists.Mapa.Fumaça.VelocidadeY = Data.ReadSByte();
        Lists.Mapa.Fumaça.Transparência = Data.ReadByte();

        // Redimensiona as ligações
        Lists.Mapa.Ligação = new short[(byte)Game.Direções.Quantidade];

        for (short i = 0; i <= (short)Game.Direções.Quantidade - 1; i++)
            Lists.Mapa.Ligação[i] = Data.ReadInt16();

        // Redimensiona os azulejos
        Lists.Mapa.Azulejo = new Lists.Estruturas.Azulejo[Lists.Mapa.Largura + 1, Lists.Mapa.Altura + 1];

        // Lê os Data
        byte NumCamadas = Data.ReadByte();
        for (byte x = 0; x <= Lists.Mapa.Largura; x++)
            for (byte y = 0; y <= Lists.Mapa.Altura; y++)
            {
                // Redimensiona os Data dos azulejos
                Lists.Mapa.Azulejo[x, y].Data = new Lists.Estruturas.Azulejo_Data[(byte)global::Mapa.Camadas.Quantidade, NumCamadas + 1];

                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= NumCamadas; q++)
                    {
                        Lists.Mapa.Azulejo[x, y].Data[c, q].x = Data.ReadByte();
                        Lists.Mapa.Azulejo[x, y].Data[c, q].y = Data.ReadByte();
                        Lists.Mapa.Azulejo[x, y].Data[c, q].Azulejo = Data.ReadByte();
                        Lists.Mapa.Azulejo[x, y].Data[c, q].Automático = Data.ReadBoolean();
                        Lists.Mapa.Azulejo[x, y].Data[c, q].Mini = new Point[4];
                    }
            }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Mapa.Largura; x++)
            for (byte y = 0; y <= Lists.Mapa.Altura; y++)
            {
                Lists.Mapa.Azulejo[x, y].Atributo = Data.ReadByte();
                Lists.Mapa.Azulejo[x, y].Bloqueio = new bool[(byte)Game.Direções.Quantidade];
                for (byte i = 0; i <= (byte)Game.Direções.Quantidade - 1; i++)
                    Lists.Mapa.Azulejo[x, y].Bloqueio[i] = Data.ReadBoolean();
            }

        // Luzes
        Lists.Mapa.Luz = new Lists.Estruturas.Luz[Data.ReadInt32() + 1];
        if (Lists.Mapa.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Mapa.Luz.GetUpperBound(0); i++)
            {
                Lists.Mapa.Luz[i].X = Data.ReadByte();
                Lists.Mapa.Luz[i].Y = Data.ReadByte();
                Lists.Mapa.Luz[i].Largura = Data.ReadByte();
                Lists.Mapa.Luz[i].Altura = Data.ReadByte();
            }

        // NPCs
        Lists.Mapa.NPC = new short[Data.ReadInt16() + 1];
        if (Lists.Mapa.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Mapa.NPC.GetUpperBound(0); i++)
                Lists.Mapa.NPC[i] = Data.ReadInt16();

        // Salva o mapa
        Escrever.Mapa(Mapa_Num);

        // Redimensiona as partículas do clima
        global::Mapa.Clima_Ajustar();
        global::Mapa.AutoCriação.Atualizar();
    }

    public static void EntrarNoMapa(NetIncomingMessage Data)
    {
        // Se tiver, reproduz a música de fundo do mapa
        if (Lists.Mapa.Música > 0)
            Áudio.Música.Reproduzir((Áudio.Músicas)Lists.Mapa.Música);
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
        // Quantidade
        Lists.Item = new Lists.Estruturas.Itens[Data.ReadByte() + 1];

        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
        {
            // Redimensiona os valores necessários 
            Lists.Item[i].Poção_Vital = new short[(byte)Game.Vitais.Quantidade];
            Lists.Item[i].Equip_Atributo = new short[(byte)Game.Atributos.Quantidade];

            // Lê os Data
            Lists.Item[i].Name = Data.ReadString();
            Lists.Item[i].Descrição = Data.ReadString();
            Lists.Item[i].Textura = Data.ReadInt16();
            Lists.Item[i].Tipo = Data.ReadByte();
            Lists.Item[i].Req_Level = Data.ReadInt16();
            Lists.Item[i].Req_Classe = Data.ReadByte();
            Lists.Item[i].Poção_Experiência = Data.ReadInt16();
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Lists.Item[i].Poção_Vital[n] = Data.ReadInt16();
            Lists.Item[i].Equip_Tipo = Data.ReadByte();
            for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Lists.Item[i].Equip_Atributo[n] = Data.ReadInt16();
            Lists.Item[i].Arma_Dano = Data.ReadInt16();
        }
    }

    public static void Mapa_Itens(NetIncomingMessage Data)
    {
        // Quantidade
        Lists.Mapa.Temp_Item = new Lists.Estruturas.Mapa_Itens[Data.ReadInt16() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.Mapa.Temp_Item.GetUpperBound(0); i++)
        {
            // Geral
            Lists.Mapa.Temp_Item[i].Índice = Data.ReadInt16();
            Lists.Mapa.Temp_Item[i].X = Data.ReadByte();
            Lists.Mapa.Temp_Item[i].Y = Data.ReadByte();
        }
    }
}