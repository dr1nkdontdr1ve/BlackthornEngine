using System.Collections.Generic;

class Listas
{
    // Armazenamento de dados
    public static Estruturas.Servidor_Dados Servidor_Dados = new Estruturas.Servidor_Dados();
    public static Estruturas.Jogador[] Jogador;
    public static Estruturas.TempJogador[] TempJogador;
    public static Estruturas.Classes[] Classe;
    public static Estruturas.Mapas[] Mapa;
    public static Estruturas.NPCs[] NPC;
    public static Estruturas.Itens[] Item;

    // Estrutura dos itens em gerais
    public class Estruturas
    {
        public struct Servidor_Dados
        {
            public string Game_Nome;
            public string Mensagem;
            public short Porta;
            public byte Máx_Jogadores;
            public byte Máx_Personagens;
            public byte Num_Classes;
            public byte Num_Azulejos;
            public short Num_Mapas;
            public short Num_NPCs;
            public short Num_Itens;
        }

        public struct Jogador
        {
            public string Usuário;
            public string Senha;
            public Game.Acessos Acesso;
            public global::Jogador.Personagem_Estrutura[] Personagem;
        }

        public struct TempJogador
        {
            public bool Jogando;
            public byte Utilizado;
            public bool ObtendoMapa;
        }

        public struct Classes
        {
            public string Nome;
            public short Textura_Masculina;
            public short Textura_Feminina;
            public short Aparecer_Mapa;
            public byte Aparecer_Direção;
            public byte Aparecer_X;
            public byte Aparecer_Y;
            public short[] Vital;
            public short[] Atributo;
        }

        public struct Mapas
        {
            public short Revisão;
            public Azulejo[,] Azulejo;
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
            public byte LuzGlobal;
            public byte Iluminação;
            public Luz[] Luz;
            public Mapa_NPC[] NPC;

            // Temporário
            public Mapa_NPCs[] Temp_NPC;
            public List<Mapa_Itens> Temp_Item;
        }

        public struct Mapa_NPC
        {
            public short Índice;
            public byte Zona;
            public bool Aparecer;
            public byte X;
            public byte Y;
        }

        public struct Azulejo
        {
            public byte Zona;
            public byte Atributo;
            public short Dado_1;
            public short Dado_2;
            public short Dado_3;
            public short Dado_4;
            public bool[] Bloqueio;
            public Azulejo_Dados[,] Dados;
        }

        public struct Azulejo_Dados
        {
            public byte x;
            public byte y;
            public byte Azulejo;
            public bool Automático;
        }

        public class Luz
        {
            public byte X;
            public byte Y;
            public byte Largura;
            public byte Altura;
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

        public struct NPCs
        {
            public string Nome;
            public short Textura;
            public byte Agressividade;
            public byte Aparecimento;
            public byte Visão;
            public byte Experiência;
            public short[] Vital;
            public short[] Atributo;
            public NPC_Queda[] Queda;
        }

        public struct Mapa_NPCs
        {
            public short Índice;
            public byte X;
            public byte Y;
            public Game.Direções Direção;
            public byte Alvo_Tipo;
            public byte Alvo_Índice;
            public short[] Vital;
            public int Aparecimento_Tempo;
            public int Ataque_Tempo;
        }

        public struct Mapa_Itens
        {
            public short Índice;
            public byte X;
            public byte Y;
            public short Quantidade;
        }

        public struct Itens
        {
            // Geral
            public string Nome;
            public string Descrição;
            public short Textura;
            public byte Tipo;
            public short Preço;
            public bool Empilhável;
            public bool NãoDropável;
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

        public struct NPC_Queda
        {
            public short Item_Num;
            public short Quantidade;
            public byte Chance;
        }

        public struct Hotbar
        {
            public byte Tipo;
            public byte Slot;
        }
    }
}