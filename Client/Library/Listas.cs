public class Listas
{
    // Armazenamento de dados
    public static Estruturas.Opções Opções = new Estruturas.Opções();
    public static Estruturas.Cliente_Dados Cliente_Dados = new Estruturas.Cliente_Dados();
    public static Estruturas.Servidor_Dados Servidor_Dados = new Estruturas.Servidor_Dados();
    public static Estruturas.Jogador[] Jogador;
    public static Estruturas.Classe[] Classe;
    public static Estruturas.Personagem[] Personagens;
    public static Estruturas.Mapas Mapa;
    public static Estruturas.Clima[] Clima_Partículas;
    public static Estruturas.NPCs[] NPC;
    public static Estruturas.Itens[] Item;

    // Estrutura dos itens em gerais
    public class Estruturas
    {
        public struct Opções
        {
            public string Jogo_Nome;
            public bool SalvarUsuário;
            public bool Sons;
            public bool Músicas;
            public string Usuário;
        }

        public struct Cliente_Dados
        {
            public byte Num_Botões;
            public byte Num_Paineis;
            public byte Num_Marcadores;
            public byte Num_Digitalizadores;
        }

        public struct Servidor_Dados
        {
            public byte Máx_Jogadores;
            public byte Máx_Personagens;
            public byte Num_Classes;
            public byte Num_Azulejos;
            public short Num_Mapas;
        }

        public class Jogador
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animação;
            public bool Atacando;
            public int Ataque_Tempo;
            public int Sofrendo;
            public short[] Máx_Vital;
            public int Coletar_Tempo;
            // Geral
            public string Nome;
            public byte Classe;
            public bool Gênero;
            public short Level;
            public short Experiência;
            public short ExpNecessária;
            public short Pontos;
            public short[] Vital;
            public short[] Atributo;
            public short Mapa;
            public byte X;
            public byte Y;
            public Jogo.Direções Direção;
            public Jogo.Movimentos Movimento;
            public short[] Equipamento;
        }

        public class Personagem
        {
            public string Nome;
            public byte Classe;
            public bool Gênero;
            public short Level;
        }

        public class Classe
        {
            public string Nome;
            public short Textura_Masculina;
            public short Textura_Feminina;
            public short Aparecer_Mapa;
            public byte Aparecer_Direção;
            public byte Aparecer_X;
            public byte Aparecer_Y;
        }
        
        public struct Mapas
        {
            public short Revisão;
            public string Nome;
            public byte Largura;
            public byte Altura;
            public byte Moral;
            public byte Panorama;
            public byte Música;
            public int Coloração;
            public Mapa_Clima Clima;
            public Mapa_Fumaça Fumaça;
            public short[] Ligação;
            public Azulejo[,] Azulejo;
            public Luz[] Luz;
            public short[] NPC;

            // Temporário
            public Mapa_NPCs[] Temp_NPC;
            public Mapa_Itens[] Temp_Item;
        }

        public struct Mapa_Clima
        {
            public byte Tipo;
            public byte Intensidade;
        }

        public struct Mapa_Fumaça
        {
            public byte Textura;
            public sbyte VelocidadeX;
            public sbyte VelocidadeY;
            public byte Transparência;
        }

        public struct Azulejo
        {
            public byte Atributo;
            public bool[] Bloqueio;
            public Azulejo_Dados[,] Dados;
        }

        public struct Azulejo_Dados
        {
            public byte x;
            public byte y;
            public byte Azulejo;
            public bool Automático;
            public System.Drawing.Point[] Mini;
        }

        public struct Clima
        {
            public bool Visível;
            public int x;
            public int y;
            public int Velocidade;
            public int Inicío;
            public bool Voltar;
        }

        public class Luz
        {
            public byte X;
            public byte Y;
            public byte Largura;
            public byte Altura;
        }

        public struct NPCs
        {
            public string Nome;
            public short Textura;
            public byte Tipo;
            public short[] Vital;
        }

        public struct Mapa_NPCs
        {
            // Apenas na parte do cliente
            public short X2;
            public short Y2;
            public byte Animação;
            public bool Atacando;
            public int Ataque_Tempo;
            public int Sofrendo;
            // Geral
            public short Índice;
            public byte X;
            public byte Y;
            public Jogo.Direções Direção;
            public Jogo.Movimentos Movimento;
            public short[] Vital;
        }

        public struct Mapa_Itens
        {
            public short Índice;
            public byte X;
            public byte Y;
        }

        public struct Itens
        {
            // Geral
            public string Nome;
            public string Descrição;
            public short Textura;
            public byte Tipo;
            // Requerimentos
            public short Req_Level;
            public byte Req_Classe;
            // Poção
            public short Poção_Experiência;
            public short[] Poção_Vital;
            // Equipamento
            public byte Equip_Tipo;
            public short[] Equip_Atributo;
            public short Arma_Dano;
        }

        public struct Inventário
        {
            public short Item_Num;
            public short Quantidade;
        }

        public struct Hotbar
        {
            public byte Tipo;
            public byte Slot;
        }
    }
}