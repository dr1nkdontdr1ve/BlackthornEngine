class Limpar
{
    public static void Opções()
    {
        // Defini os dados das opções
        Listas.Opções.Game_Nome = "CryBits";
        Listas.Opções.SalvarUsuário = true;
        Listas.Opções.Músicas = true;
        Listas.Opções.Sons = true;
        Listas.Opções.Usuário = string.Empty;

        // Salva o que foi modificado
        Escrever.Opções();
    }

    public static void Botão(int Índice)
    {
        // Limpa a estrutura
        Botões.Lista[Índice] = new Botões.Estrutura();
        Botões.Lista[Índice].Geral = new Ferramentas.Geral();
    }

    public static void Digitalizador(int Índice)
    {
        // Limpa a estrutura
        Digitalizadores.Lista[Índice] = new Digitalizadores.Estrutura();
        Digitalizadores.Lista[Índice].Geral = new Ferramentas.Geral();
    }

    public static void Painel(int Índice)
    {
        // Limpa a estrutura
        Paineis.Lista[Índice] = new Paineis.Estrutura();
        Paineis.Lista[Índice].Geral = new Ferramentas.Geral();
    }

    public static void Marcador(int Índice)
    {
        // Limpa a estrutura
        Marcadores.Lista[Índice] = new Marcadores.Estrutura();
        Marcadores.Lista[Índice].Geral = new Ferramentas.Geral();
    }

    public static void Jogador(byte Índice)
    {
        // Limpa a estrutura
        Listas.Jogador[Índice] = new Listas.Estruturas.Jogador();
        Listas.Jogador[Índice].Vital = new short[(byte)Game.Vitais.Quantidade];
        Listas.Jogador[Índice].Máx_Vital = new short[(byte)Game.Vitais.Quantidade];
        Listas.Jogador[Índice].Atributo = new short[(byte)Game.Atributos.Quantidade];
        Listas.Jogador[Índice].Equipamento = new short[(byte)Game.Equipamentos.Quantidade];

        // Reseta os valores
        Listas.Jogador[Índice].Nome = string.Empty;
    }
}