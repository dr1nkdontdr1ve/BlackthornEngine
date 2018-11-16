using System;

class Jogo
{
    // Números aleatórios
    public static Random Aleatório = new Random();

    // O maior índice dos jogadores conectados
    public static byte MaiorÍndice;

    // CPS do servidor
    public static int CPS;
    public static bool CPS_Travado;

    // Bloqueio direcional
    public const byte Máx_BloqDirecional = 3;

    // Máximo e mínimo de caracteres permidos em alguns texto
    public const byte Máx_Caractere = 12;
    public const byte Min_Caractere = 3;

    // Limites em geral
    public const byte Máx_NPC_Queda = 4;
    public const byte Máx_Inventário = 30;
    public const byte Máx_Mapa_Itens = 100;
    public const byte Máx_Hotbar = 10;

    #region Númerações
    public enum Direções
    {
        Acima,
        Abaixo,
        Esquerda,
        Direita,
        Quantidade
    }

    public enum Acessos
    {
        Nenhum,
        Moderador,
        Editor,
        Administrador
    }

    public enum Gêneros
    {
        Masculino,
        Feminino
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

    public enum Mensagens
    {
        Jogo,
        Mapa,
        Global,
        Particular
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

    public static void RedefinirMaiorÍndice()
    {
        // Redefine o índice máximo de jogadores
        MaiorÍndice = 0;

        for (byte i = (byte)Listas.Jogador.GetUpperBound(0); i >= 1; i -= 1)
            if (Rede.EstáConectado(i))
            {
                MaiorÍndice = i;
                break;
            }

        // Envia os dados para os jogadores
        Enviar.MaiorÍndice();
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