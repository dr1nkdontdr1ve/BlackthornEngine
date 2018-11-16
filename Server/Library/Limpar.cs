using System;

class Limpar
{
    public static void Necessário()
    {
        // Limpa todos os dados necessários
        Jogadores();
    }

    public static void Jogadores()
    {
        // Redimensiona a lista
        Listas.Jogador = new Listas.Estruturas.Jogador[Listas.Servidor_Dados.Máx_Jogadores + 1];
        Listas.TempJogador = new Listas.Estruturas.TempJogador[Listas.Servidor_Dados.Máx_Jogadores + 1];

        // Limpa os dados de todos jogadores
        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Jogadores; i++)
            Jogador(i);
    }

    public static void Jogador(byte Índice)
    {
        // Limpa os dados do jogador
        Listas.Jogador[Índice] = new Listas.Estruturas.Jogador();
        Listas.TempJogador[Índice] = new Listas.Estruturas.TempJogador();
        Listas.Jogador[Índice].Usuário = string.Empty;
        Listas.Jogador[Índice].Senha = string.Empty;
        Jogador_Personagens(Índice);
    }

    public static void Jogador_Personagens(byte Índice)
    {
        Listas.Jogador[Índice].Personagem = new Jogador.Personagem_Estrutura[Listas.Servidor_Dados.Máx_Personagens + 1];

        // Limpa os dados
        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
            Jogador_Personagem(Índice, i);
    }

    public static void Jogador_Personagem(byte Índice, byte Personagem)
    {
        // Limpa os dados
        Listas.Jogador[Índice].Personagem[Personagem] = new Jogador.Personagem_Estrutura();
        Listas.Jogador[Índice].Personagem[Personagem].Índice = Índice;
        Listas.Jogador[Índice].Personagem[Personagem].Inventário = new Listas.Estruturas.Inventário[Jogo.Máx_Inventário + 1];
        Listas.Jogador[Índice].Personagem[Personagem].Equipamento = new short[(byte)Jogo.Equipamentos.Quantidade];
        Listas.Jogador[Índice].Personagem[Personagem].Hotbar = new Listas.Estruturas.Hotbar[Jogo.Máx_Hotbar + 1];
    }
}