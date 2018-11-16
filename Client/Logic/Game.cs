using System;
using System.Drawing;
using System.Windows.Forms;

public class Game
{
    // Números aleatórios
    public static Random Aleatório = new Random();

    // Dimension of grids
    public const byte Grade = 32;

    // Game Delay Calculation Measure
    public static short FPS;

    // Interface
    public static byte CriarPersonagem_Classe = 1;
    public static byte SelecionarPersonagem = 1;

    // Player
    public const short Ataque_Velocidade = 750;

    // Pressing the keys
    public static bool Pressionado_Acima;
    public static bool Pressionado_Abaixo;
    public static bool Pressionado_Esquerda;
    public static bool Pressionado_Direita;
    public static bool Pressionado_Shift;
    public static bool Pressionado_Control;

    // Animation
    public const byte Animação_Quantidade = 4;
    public const byte Animação_Parada = 1;
    public const byte Animação_Direita = 0;
    public const byte Animação_Esquerda = 2;
    public const byte Animação_Ataque = 2;

    // Movement
    public const byte Movimentação_Acima = 3;
    public const byte Movimentação_Abaixo = 0;
    public const byte Movimentação_Esquerda = 1;
    public const byte Movimentação_Direita = 2;

    // Player view
    public static Rectangle Câmera;
    public static Rectangle Azulejos_Visão;

    // Directional lock
    public const byte Máx_BloqDirecional = 3;

    // Screen size
    public const short Tela_Largura = (Map.Min_Largura + 1) * Grade;
    public const short Tela_Altura = (Map.Min_Altura + 1) * Grade;

    // Limits in general
    public const byte Máx_Inventário = 30;
    public const byte Máx_Mapa_Itens = 100;
    public const byte Máx_Hotbar = 10;

    // Latency
    public static int Latência;
    public static int Latência_Envio;

    #region Numeradores
    public enum Situações
    {
        Conectar,
        Registrar,
        SelecionarPersonagem,
        CriarPersonagem
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

    public enum Direções
    {
        Acima,
        Abaixo,
        Esquerda,
        Direita,
        Quantidade
    }

    public enum Movimentos
    {
        Parado,
        Andando,
        Correndo
    }

    public enum Acessos
    {
        Nenhum,
        Moderador,
        Editor,
        Administrador
    }

    public enum Mensagens
    {
        Game,
        Mapa,
        Global,
        Particular
    }

    public enum Vitais
    {
        Vida,
        Mana,
        Quantidade
    }

    public enum NPCs
    {
        Passivo,
        Atacado,
        AoVer
    }

    public enum Alvo
    {
        Jogador = 1,
        NPC
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

    public enum Hotbar
    {
        Nenhum, 
        Item
    }
    #endregion

    public static void AbrirMenu()
    {
        // Reproduz a música de fundo
        if (Listas.Opções.Músicas)
            Áudio.Música.Reproduzir(Áudio.Músicas.Menu);

        // Abre o menu
        Tools.JanelaAtual = Tools.Janelas.Menu;
    }

    public static void Sair()
    {
        // Volta ao menu
        AbrirMenu();

        // Termina a conexão
        Rede.Desconectar();
    }

    public static void DefinirSituação(Situações Situação)
    {
        // Verifica se é possível se conectar ao servidor
        if (!Rede.TentarConectar())
        {
            MessageBox.Show("The server is currently unavailable.");
            return;
        }

        // Envia os dados
        switch (Situação)
        {
            case Situações.Conectar: Enviar.Conectar(); break;
            case Situações.Registrar: Enviar.Registrar(); break;
            case Situações.CriarPersonagem: Enviar.CriarPersonagem(); break;
        }
    }

    public static void Desconectar()
    {
        // Não fechar os paineis se não for necessário
        if (Panels.Encontrar("Opções").General.Visível || Panels.Encontrar("Conectar").General.Visível || Panels.Encontrar("Registrar").General.Visível)
            return;

        // Limpa os valores
        Áudio.Som.Parar_Tudo();
        Player.MeuÍndice = 0;

        // Traz o jogador de volta ao menu
        Tools.JanelaAtual = Tools.Janelas.Menu;
        Panels.Menu_Fechar();
        Panels.Encontrar("Conectar").General.Visível = true;
    }

    public static void Atualizar_Câmera()
    {
        Point Final = new Point(), Início = new Point(), Posição = new Point();

        // Centro da tela
        Posição.X = Player.Eu.X2 + Grade;
        Posição.Y = Player.Eu.Y2 + Grade;

        // Início da tela
        Início.X = Player.Eu.X - ((Map.Min_Largura + 1) / 2) - 1;
        Início.Y = Player.Eu.Y - ((Map.Min_Altura + 1) / 2) - 1;

        // Reajusta a posição horizontal da tela
        if (Início.X < 0)
        {
            Posição.X = 0;
            if (Início.X == -1 && Player.Eu.X2 > 0) Posição.X = Player.Eu.X2;
            Início.X = 0;
        }

        // Reajusta a posição vertical da tela
        if (Início.Y < 0)
        {
            Posição.Y = 0;
            if (Início.Y == -1 && Player.Eu.Y2 > 0) Posição.Y = Player.Eu.Y2;
            Início.Y = 0;
        }

        // Final da tela
        Final.X = Início.X + (Map.Min_Largura + 1) + 1;
        Final.Y = Início.Y + (Map.Min_Altura + 1) + 1;

        // Reajusta a posição horizontal da tela
        if (Final.X > Listas.Mapa.Largura)
        {
            Posição.X = Grade;
            if (Final.X == Listas.Mapa.Largura + 1 && Player.Eu.X2 < 0) Posição.X = Player.Eu.X2 + Grade;
            Final.X = Listas.Mapa.Largura;
            Início.X = Final.X - Map.Min_Largura - 1;
        }

        // Reajusta a posição vertical da tela
        if (Final.Y > Listas.Mapa.Altura)
        {
            Posição.Y = Grade;
            if (Final.Y == Listas.Mapa.Altura + 1 && Player.Eu.Y2 < 0) Posição.Y = Player.Eu.Y2 + Grade;
            Final.Y = Listas.Mapa.Altura;
            Início.Y = Final.Y - Map.Min_Altura - 1;
        }

        // Define a dimensão dos azulejos vistos
        Azulejos_Visão.Y = Início.Y;
        Azulejos_Visão.Height = Final.Y;
        Azulejos_Visão.X = Início.X;
        Azulejos_Visão.Width = Final.X;

        // Define a posição da câmera
        Câmera.Y = Posição.Y;
        Câmera.Height = Câmera.Y + Tela_Altura;
        Câmera.X = Posição.X;
        Câmera.Width = Câmera.X + Tela_Largura;
    }

    public static int ConverterX(int x)
    {
        // Converte o valor em uma posição adequada à camera
        return x - (Azulejos_Visão.X * Grade) - Câmera.X;
    }

    public static int ConverterY(int y)
    {
        // Converte o valor em uma posição adequada à camera
        return y - (Azulejos_Visão.Y * Grade) - Câmera.Y;
    }

    public static bool EstáNoLimite_Visão(int x, int y)
    {
        // Verifica se os valores estão no limite do que se está vendo
        if (x >= Azulejos_Visão.X && y >= Azulejos_Visão.Y && x <= Azulejos_Visão.Width && y <= Azulejos_Visão.Height)
            return true;
        else
            return false;
    }

    public static Direções DireçãoInversa(Direções Direção)
    {
        // Retorna a direção inversa
        switch (Direção)
        {
            case Direções.Acima: return Direções.Abaixo;
            case Direções.Abaixo: return Direções.Acima;
            case Direções.Esquerda: return Direções.Direita;
            case Direções.Direita: return Direções.Esquerda;
            default: return Direções.Quantidade;
        }
    }
}