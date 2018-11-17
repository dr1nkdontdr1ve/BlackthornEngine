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
        Jogador_Dados,
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

    public static void Dados(NetIncomingMessage Dados)
    {
        // Manuseia os dados recebidos
        switch ((Pacotes)Dados.ReadByte())
        {
            case Pacotes.Alerta: Alerta(Dados); break;
            case Pacotes.Conectar: Conectar(Dados); break;
            case Pacotes.Entrada: Entrada(Dados); break;
            case Pacotes.CriarPersonagem: CriarPersonagem(Dados); break;
            case Pacotes.Entrar: Entrar(Dados); break;
            case Pacotes.Classes: Classes(Dados); break;
            case Pacotes.Personagens: Personagens(Dados); break;
            case Pacotes.MaiorÍndice: MaiorÍndice(Dados); break;
            case Pacotes.Jogador_Dados: Jogador_Dados(Dados); break;
            case Pacotes.Jogador_Posição: Jogador_Posição(Dados); break;
            case Pacotes.Jogador_Vitais: Jogador_Vitais(Dados); break;
            case Pacotes.Jogador_Mover: Jogador_Mover(Dados); break;
            case Pacotes.Jogador_Saiu: Jogador_Saiu(Dados); break;
            case Pacotes.Jogador_Direção: Jogador_Direção(Dados); break;
            case Pacotes.Jogador_Atacar: Jogador_Atacar(Dados); break;
            case Pacotes.Jogador_Experiência: Jogador_Experiência(Dados); break;
            case Pacotes.Jogador_Inventário: Jogador_Inventário(Dados); break;
            case Pacotes.Jogador_Equipamentos: Jogador_Equipamentos(Dados); break;
            case Pacotes.Jogador_Hotbar: Jogador_Hotbar(Dados); break;
            case Pacotes.Mapa_Revisão: Mapa_Revisão(Dados); break;
            case Pacotes.Mapa: Mapa(Dados); break;
            case Pacotes.EntrarNoMapa: EntrarNoMapa(Dados); break;
            case Pacotes.Latência: Latência(Dados); break;
            case Pacotes.Mensagem: Mensagem(Dados); break;
            case Pacotes.NPCs: NPCs(Dados); break;
            case Pacotes.Mapa_NPCs: Mapa_NPCs(Dados); break;
            case Pacotes.Mapa_NPC: Mapa_NPC(Dados); break;
            case Pacotes.Mapa_NPC_Movimento: Mapa_NPC_Movimento(Dados); break;
            case Pacotes.Mapa_NPC_Direção: Mapa_NPC_Direção(Dados); break;
            case Pacotes.Mapa_NPC_Vitais: Mapa_NPC_Vitais(Dados); break;
            case Pacotes.Mapa_NPC_Atacar: Mapa_NPC_Atacar(Dados); break;
            case Pacotes.Mapa_NPC_Morreu: Mapa_NPC_Morreu(Dados); break;
            case Pacotes.Itens: Itens(Dados); break;
            case Pacotes.Mapa_Itens: Mapa_Itens(Dados); break;
        }
    }

    private static void Alerta(NetIncomingMessage Dados)
    {
        // Mostra a mensagem
        MessageBox.Show(Dados.ReadString());
    }

    private static void Conectar(NetIncomingMessage Dados)
    {
        // Reseta os valores
        Game.SelecionarPersonagem = 1;

        // Abre o painel de seleção de personagens
        Paineis.Menu_Fechar();
        Paineis.Encontrar("SelecionarPersonagem").Geral.Visível = true;
    }

    private static void Entrada(NetIncomingMessage Dados)
    {
        // Definir os valores que são enviados do servidor
        Jogador.MeuÍndice = Dados.ReadByte();
        Jogador.MaiorÍndice = Dados.ReadByte();

        // Limpa a estrutura dos jogadores
        Listas.Jogador = new Listas.Estruturas.Jogador[Dados.ReadByte() + 1];

        for (byte i = 1; i <= Listas.Jogador.GetUpperBound(0); i++)
            Limpar.Jogador(i);
    }

    private static void CriarPersonagem(NetIncomingMessage Dados)
    {
        // Reseta os valores
        Digitalizadores.Encontrar("CriarPersonagem_Nome").Texto = string.Empty;
        Marcadores.Encontrar("GêneroMasculino").Estado = true;
        Marcadores.Encontrar("GêneroFeminino").Estado = false;
        Game.CriarPersonagem_Classe = 1;

        // Abre o painel de criação de personagem
        Paineis.Menu_Fechar();
        Paineis.Encontrar("CriarPersonagem").Geral.Visível = true;
    }

    private static void Classes(NetIncomingMessage Dados)
    {
        int Quantidade = Dados.ReadByte();

        // Recebe os dados das classes
        Listas.Classe = new Listas.Estruturas.Classe[Quantidade + 1];

        for (byte i = 1; i <= Quantidade; i++)
        {
            // Recebe os dados do personagem
            Listas.Classe[i] = new Listas.Estruturas.Classe();
            Listas.Classe[i].Nome = Dados.ReadString();
            Listas.Classe[i].Textura_Masculina = Dados.ReadInt16();
            Listas.Classe[i].Textura_Feminina = Dados.ReadInt16();
        }
    }

    private static void Personagens(NetIncomingMessage Dados)
    {
        byte Quantidade = Dados.ReadByte();

        // Redimensiona a lista
        Listas.Servidor_Dados.Máx_Personagens = Quantidade;
        Listas.Personagens = new Listas.Estruturas.Personagem[Quantidade + 1];

        for (byte i = 1; i <= Quantidade; i++)
        {
            // Recebe os dados do personagem
            Listas.Personagens[i] = new Listas.Estruturas.Personagem();
            Listas.Personagens[i].Nome = Dados.ReadString();
            Listas.Personagens[i].Classe = Dados.ReadByte();
            Listas.Personagens[i].Gênero = Dados.ReadBoolean();
            Listas.Personagens[i].Level = Dados.ReadInt16();
        }
    }

    private static void Entrar(NetIncomingMessage Dados)
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

    private static void MaiorÍndice(NetIncomingMessage Dados)
    {
        // Define o número maior de índice
        Jogador.MaiorÍndice = Dados.ReadByte();
    }

    public static void Mapa_Revisão(NetIncomingMessage Dados)
    {
        bool Necessário = false;
        int Mapa = Dados.ReadInt16();

        // Limpa todos os outros jogadores
        for (byte i = 1; i <= Jogador.MaiorÍndice; i++)
            if (i != Jogador.MeuÍndice)
                Limpar.Jogador(i);

        // Verifica se é necessário baixar os dados do mapa

        Necessário = true;

        // Solicita os dados do mapa
        Enviar.Solicitar_Mapa(Necessário);
    }

    public static void Mapa(NetIncomingMessage Dados)
    {
        // Define os dados
        short Mapa_Num = Dados.ReadInt16();
        Listas.Mapa.Revisão = Dados.ReadInt16();
        Listas.Mapa.Nome = Dados.ReadString();
        Listas.Mapa.Largura = Dados.ReadByte();
        Listas.Mapa.Altura = Dados.ReadByte();
        Listas.Mapa.Moral = Dados.ReadByte();
        Listas.Mapa.Panorama = Dados.ReadByte();
        Listas.Mapa.Música = Dados.ReadByte();
        Listas.Mapa.Coloração = Dados.ReadInt32();
        Listas.Mapa.Clima.Tipo = Dados.ReadByte();
        Listas.Mapa.Clima.Intensidade = Dados.ReadByte();
        Listas.Mapa.Fumaça.Textura = Dados.ReadByte();
        Listas.Mapa.Fumaça.VelocidadeX = Dados.ReadSByte();
        Listas.Mapa.Fumaça.VelocidadeY = Dados.ReadSByte();
        Listas.Mapa.Fumaça.Transparência = Dados.ReadByte();

        // Redimensiona as ligações
        Listas.Mapa.Ligação = new short[(byte)Game.Direções.Quantidade];

        for (short i = 0; i <= (short)Game.Direções.Quantidade - 1; i++)
            Listas.Mapa.Ligação[i] = Dados.ReadInt16();

        // Redimensiona os azulejos
        Listas.Mapa.Azulejo = new Listas.Estruturas.Azulejo[Listas.Mapa.Largura + 1, Listas.Mapa.Altura + 1];

        // Lê os dados
        byte NumCamadas = Dados.ReadByte();
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
            {
                // Redimensiona os dados dos azulejos
                Listas.Mapa.Azulejo[x, y].Dados = new Listas.Estruturas.Azulejo_Dados[(byte)global::Mapa.Camadas.Quantidade, NumCamadas + 1];

                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= NumCamadas; q++)
                    {
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].x = Dados.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].y = Dados.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Azulejo = Dados.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Automático = Dados.ReadBoolean();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Mini = new Point[4];
                    }
            }

        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
            {
                Listas.Mapa.Azulejo[x, y].Atributo = Dados.ReadByte();
                Listas.Mapa.Azulejo[x, y].Bloqueio = new bool[(byte)Game.Direções.Quantidade];
                for (byte i = 0; i <= (byte)Game.Direções.Quantidade - 1; i++)
                    Listas.Mapa.Azulejo[x, y].Bloqueio[i] = Dados.ReadBoolean();
            }

        // Luzes
        Listas.Mapa.Luz = new Listas.Estruturas.Luz[Dados.ReadInt32() + 1];
        if (Listas.Mapa.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Listas.Mapa.Luz.GetUpperBound(0); i++)
            {
                Listas.Mapa.Luz[i].X = Dados.ReadByte();
                Listas.Mapa.Luz[i].Y = Dados.ReadByte();
                Listas.Mapa.Luz[i].Largura = Dados.ReadByte();
                Listas.Mapa.Luz[i].Altura = Dados.ReadByte();
            }

        // NPCs
        Listas.Mapa.NPC = new short[Dados.ReadInt16() + 1];
        if (Listas.Mapa.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Listas.Mapa.NPC.GetUpperBound(0); i++)
                Listas.Mapa.NPC[i] = Dados.ReadInt16();

        // Salva o mapa
        Escrever.Mapa(Mapa_Num);

        // Redimensiona as partículas do clima
        global::Mapa.Clima_Ajustar();
        global::Mapa.AutoCriação.Atualizar();
    }

    public static void EntrarNoMapa(NetIncomingMessage Dados)
    {
        // Se tiver, reproduz a música de fundo do mapa
        if (Listas.Mapa.Música > 0)
            Áudio.Música.Reproduzir((Áudio.Músicas)Listas.Mapa.Música);
        else
            Áudio.Música.Parar();
    }

    public static void Latência(NetIncomingMessage Dados)
    {
        // Define a latência
        Game.Latência = Environment.TickCount - Game.Latência_Envio;
    }

    public static void Mensagem(NetIncomingMessage Dados)
    {
        // Adiciona a mensagem
        string Texto = Dados.ReadString();
        Color Cor = Color.FromArgb(Dados.ReadInt32());
        Ferramentas.Adicionar(Texto, new SFML.Graphics.Color(Cor.R, Cor.G, Cor.B));
    }

    public static void Itens(NetIncomingMessage Dados)
    {
        // Quantidade
        Listas.Item = new Listas.Estruturas.Itens[Dados.ReadByte() + 1];

        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++)
        {
            // Redimensiona os valores necessários 
            Listas.Item[i].Poção_Vital = new short[(byte)Game.Vitais.Quantidade];
            Listas.Item[i].Equip_Atributo = new short[(byte)Game.Atributos.Quantidade];

            // Lê os dados
            Listas.Item[i].Nome = Dados.ReadString();
            Listas.Item[i].Descrição = Dados.ReadString();
            Listas.Item[i].Textura = Dados.ReadInt16();
            Listas.Item[i].Tipo = Dados.ReadByte();
            Listas.Item[i].Req_Level = Dados.ReadInt16();
            Listas.Item[i].Req_Classe = Dados.ReadByte();
            Listas.Item[i].Poção_Experiência = Dados.ReadInt16();
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Listas.Item[i].Poção_Vital[n] = Dados.ReadInt16();
            Listas.Item[i].Equip_Tipo = Dados.ReadByte();
            for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Listas.Item[i].Equip_Atributo[n] = Dados.ReadInt16();
            Listas.Item[i].Arma_Dano = Dados.ReadInt16();
        }
    }

    public static void Mapa_Itens(NetIncomingMessage Dados)
    {
        // Quantidade
        Listas.Mapa.Temp_Item = new Listas.Estruturas.Mapa_Itens[Dados.ReadInt16() + 1];

        // Lê os dados de todos
        for (byte i = 1; i <= Listas.Mapa.Temp_Item.GetUpperBound(0); i++)
        {
            // Geral
            Listas.Mapa.Temp_Item[i].Índice = Dados.ReadInt16();
            Listas.Mapa.Temp_Item[i].X = Dados.ReadByte();
            Listas.Mapa.Temp_Item[i].Y = Dados.ReadByte();
        }
    }
}