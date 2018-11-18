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

    public static void Botão(int Index)
    {
        // Limpa a estrutura
        Botões.Lista[Index] = new Botões.Estrutura();
        Botões.Lista[Index].Geral = new Ferramentas.Geral();
    }

    public static void Digitalizador(int Index)
    {
        // Limpa a estrutura
        Digitalizadores.Lista[Index] = new Digitalizadores.Estrutura();
        Digitalizadores.Lista[Index].Geral = new Ferramentas.Geral();
    }

    public static void Painel(int Index)
    {
        // Limpa a estrutura
        Paineis.Lista[Index] = new Paineis.Estrutura();
        Paineis.Lista[Index].Geral = new Ferramentas.Geral();
    }

    public static void Marcador(int Index)
    {
        // Limpa a estrutura
        Marcadores.Lista[Index] = new Marcadores.Estrutura();
        Marcadores.Lista[Index].Geral = new Ferramentas.Geral();
    }

    public static void Player(byte Index)
    {
        // Limpa a estrutura
        Lists.Player[Index] = new Lists.Structures.Player();
        Lists.Player[Index].Vital = new short[(byte)Game.Vital.Amount];
        Lists.Player[Index].Max_Vital = new short[(byte)Game.Vital.Amount];
        Lists.Player[Index].Atributo = new short[(byte)Game.Atributos.Amount];
        Lists.Player[Index].Equipamento = new short[(byte)Game.Equipamentos.Amount];

        // Reseta os valores
        Lists.Player[Index].Name = string.Empty;
    }
}