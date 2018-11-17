class Clean
{
    public static void Opções()
    {
        // Defini os Data das opções
        Lists.Opções.Game_Name = "CryBits";
        Lists.Opções.SalvarUsuário = true;
        Lists.Opções.Músicas = true;
        Lists.Opções.Sons = true;
        Lists.Opções.Usuário = string.Empty;

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
        Lists.Jogador[Índice] = new Lists.Estruturas.Jogador();
        Lists.Jogador[Índice].Vital = new short[(byte)Game.Vitais.Quantidade];
        Lists.Jogador[Índice].Máx_Vital = new short[(byte)Game.Vitais.Quantidade];
        Lists.Jogador[Índice].Atributo = new short[(byte)Game.Atributos.Quantidade];
        Lists.Jogador[Índice].Equipamento = new short[(byte)Game.Equipamentos.Quantidade];

        // Reseta os valores
        Lists.Jogador[Índice].Name = string.Empty;
    }
}