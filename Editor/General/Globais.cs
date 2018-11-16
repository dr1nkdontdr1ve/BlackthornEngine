using System;
using System.Drawing;

class Globais
{
    // Dimensão das grades 
    public const byte Grade = 32;
    public static Size Grade_Tamanho = new Size(Grade, Grade);

    // Medida de calculo do atraso do Game
    public static short FPS;

    // Captura de tela
    public static bool Captura;

    // Limitações dos mapas
    public const byte Máx_Mapa_Camadas = 3;
    public const byte Min_Mapa_Largura = 24;
    public const byte Min_Mapa_Altura = 18;
    public const byte Num_Zonas = 20;

    // Fumaças
    public static int Fumaça_X;
    public static int Fumaça_Y;

    // Clima
    public const byte Máx_Chuva_Partículas = 100;
    public const short Máx_Neve_Partículas = 635;
    public const byte Máx_Clima_Intensidade = 10;
    public const byte Neve_Movimento = 10;
    public static byte Relâmpago;

    // Limites em geral
    public const byte Máx_NPC_Queda = 4;

    // Números aleatórios
    public static Random Aleatório = new Random();

    public enum Ferramentas_Tipos
    {
        Botão,
        Painel,
        Marcador,
        Digitalizador,
        Quantidade
    }

    public enum Azulejo_Atributos
    {
        Nenhum,
        Bloqueio,
        Teletransporte,
        Item,
        Quantidade
    }

    public enum Camadas
    {
        Chão,
        Telhado,
        Quantidade
    }

    public enum Vitais
    {
        Vida,
        Mana,
        Quantidade
    }

    public enum Atributos
    {
        Força,
        Resistência,
        Inteligência,
        Agilidade,
        Vitalidade,
        Quantidade
    }

    public enum Mapa_Morais
    {
        Pacífico,
        Perigoso,
        Quantidade
    }

    public enum Climas
    {
        Normal,
        Chovendo,
        Trovoando,
        Nevando,
        Quantidade
    }

    public enum Direções
    {
        Acima,
        Abaixo,
        Esquerda,
        Direita,
        Quantidade
    }

    public enum Azulejos_Formas
    {
        Normal,
        Automática,
        Automático_Animado
    }

    public enum Itens
    {
        Nenhum,
        Equipamento,
        Poção
    }

    public enum Equipamentos
    {
        Arma,
        Armadura,
        Capacete,
        Escudo,
        Amuleto,
        Quantidade
    }

    public static void Redimensionar_Clima()
    {
        // Redimensiona a lista
        switch ((Climas)Listas.Mapa[Editor_Mapas.Selecionado].Clima.Tipo)
        {
            case Climas.Trovoando:
            case Climas.Chovendo: Listas.Clima_Partículas = new Listas.Estruturas.Clima[Máx_Chuva_Partículas + 1]; break;
            case Climas.Nevando: Listas.Clima_Partículas = new Listas.Estruturas.Clima[Máx_Neve_Partículas + 1]; break;
        }
    }

    public static string Numeração(int Número, int Limite)
    {
        int Quantidade = Limite.ToString().Length - Número.ToString().Length;

        // Retorna com a numeração
        if (Quantidade > 0)
            return new string('0', Quantidade) + Número;
        else
            return Número.ToString();
    }

    public static Point Bloqueio_Posição(byte Direção)
    {
        // Retorna a posição de cada seta do bloqueio direcional
        switch ((Direções)Direção)
        {
            case Direções.Acima: return new Point(Grade / 2 - 4, 0);
            case Direções.Abaixo: return new Point(Grade / 2 - 4, Grade - 9);
            case Direções.Esquerda: return new Point(0, Grade / 2 - 4);
            case Direções.Direita: return new Point(Grade - 9, Grade / 2 - 4);
            default: return new Point(0);
        }
    }

    public static byte Grade_Zoom
    {
        // Tamanho da grade com o zoom
        get
        {
            return (byte)(Grade / Editor_Mapas.Zoom());
        }
    }

    public static int Zoom(int Valor)
    {
        // Tamanho da grade com o zoom
        return Valor * Grade_Zoom;
    }

    public static Point Zoom(int X, int Y)
    {
        // Tamanho da grade com o zoom
        return new Point(X * Grade_Zoom, Y * Grade_Zoom);
    }

    public static Rectangle Zoom(Rectangle Retângulo)
    {
        // Tamanho da grade com o zoom
        return new Rectangle(Retângulo.X * Grade_Zoom, Retângulo.Y * Grade_Zoom, Retângulo.Width * Grade_Zoom, Retângulo.Height * Grade_Zoom);
    }

    public static bool EstáSobrepondo(Point Ponteiro, Rectangle Retângulo)
    {
        // Verficia se o mouse está sobre o objeto
        if (Ponteiro.X >= Retângulo.X && Ponteiro.X <= Retângulo.X + Retângulo.Width)
            if (Ponteiro.Y >= Retângulo.Y && Ponteiro.Y <= Retângulo.Y + Retângulo.Height)
                return true;

        // Se não, retornar um valor nulo
        return false;
    }
}