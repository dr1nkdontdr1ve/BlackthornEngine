using System.Drawing;
using System.Collections.Generic;

class Listas
{
    // Armazenamento de dados
    public static Estruturas.Opções Opções = new Estruturas.Opções();
    public static Estruturas.Cliente_Dados Cliente_Dados = new Estruturas.Cliente_Dados();
    public static Estruturas.Servidor_Dados Servidor_Dados = new Estruturas.Servidor_Dados();
    public static Estruturas.Botões[] Botão;
    public static Estruturas.Digitalizadores[] Digitalizador;
    public static Estruturas.Marcadores[] Marcador;
    public static Estruturas.Paineis[] Painel;
    public static Estruturas.Classes[] Classe;
    public static Estruturas.Azulejos_Azulejo[] Azulejo;
    public static Estruturas.Mapas[] Mapa;
    public static Estruturas.Clima[] Clima_Partículas;
    public static Estruturas.NPCs[] NPC;
    public static Estruturas.Itens[] Item;

    // Estrutura dos itens em gerais
    public class Estruturas
    {
        public struct Opções
        {
            public string Diretório_Cliente;
            public string Diretório_Servidor;
            public bool Pre_Mapa_Grades;
            public bool Pre_Mapa_Visualização;
            public bool Pre_Mapa_Áudio;
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
            public string Jogo_Nome;
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

        public struct Ferramentas
        {
            public string Nome;
            public bool Visível;
            public Point Posição;
        }

        public struct Botões
        {
            public byte Textura;
            public Ferramentas Geral;
        }

        public struct Digitalizadores
        {
            public string Texto;
            public short Máx_Carácteres;
            public short Largura;
            public bool Senha;
            public Ferramentas Geral;
        }

        public struct Marcadores
        {
            public string Texto;
            public bool Estado;
            public Ferramentas Geral;
        }

        public struct Paineis
        {
            public byte Textura;
            public Ferramentas Geral;
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

        public struct Azulejos_Azulejo
        {
            public byte Largura;
            public byte Altura;
            public Azulejos_Azulejo_Dados[,] Azulejo;
        }

        public struct Azulejos_Azulejo_Dados
        {
            public byte Atributo;
            public bool[] Bloqueio;
        }

        public struct Mapas
        {
            public short Revisão;
            public List<Camada> Camada;
            public Mapas_Azulejo_Dados[,] Azulejo;
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
            public List<Luz_Estrutura> Luz;
            public List<Mapa_NPC> NPC;
        }

        public struct Mapas_Azulejo_Dados
        {
            public byte Atributo;
            public short Dado_1;
            public short Dado_2;
            public short Dado_3;
            public short Dado_4;
            public byte Zona;
            public bool[] Bloqueio;
        }

        public class Luz_Estrutura
        {
            public byte X;
            public byte Y;
            public byte Largura;
            public byte Altura;

            public Luz_Estrutura(Rectangle Retângulo)
            {
                // Define os dados da estrutura
                X = (byte)Retângulo.X;
                Y = (byte)Retângulo.Y;
                Largura = (byte)Retângulo.Width;
                Altura = (byte)Retângulo.Height;
            }

            public Rectangle Retângulo
            {
                get
                {
                    return new Rectangle(X, Y, Largura, Altura);
                }
            }
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

        public struct Azulejo_Dados
        {
            public byte x;
            public byte y;
            public byte Azulejo;
            public bool Automático;
            public Point[] Mini;
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

        public class Camada
        {
            public string Nome;
            public byte Tipo;
            public Azulejo_Dados[,] Azulejo;
        }

        public struct Mapa_NPC
        {
            public short Índice;
            public byte Zona;
            public bool Aparecer;
            public byte X;
            public byte Y;
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

        public struct NPC_Queda
        {
            public short Item_Num;
            public short Quantidade;
            public byte Chance;
        }
    }
}