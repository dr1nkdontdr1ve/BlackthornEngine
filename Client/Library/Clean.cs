class Clean
{
    public static void Options()
    {
        // Defini os Data das Options
        Lists.Options.Jogo_Name = "CryBits";
        Lists.Options.SalvarUsuário = true;
        Lists.Options.Músicas = true;
        Lists.Options.Sons = true;
        Lists.Options.User = string.Empty;

        // Salva o que foi modificado
        Write.Options();
    }

    public static void Button(int Index)
    {
        // Limpa a Structure
        Buttons.List[Index] = new Buttons.Structure();
        Buttons.List[Index].Geral = new Tools.Geral();
    }

    public static void Scanner(int Index)
    {
        // Limpa a Structure
        Scanners.List[Index] = new Scanners.Structure();
        Scanners.List[Index].Geral = new Tools.Geral();
    }

    public static void Panel(int Index)
    {
        // Limpa a Structure
        Panels.List[Index] = new Panels.Structure();
        Panels.List[Index].Geral = new Tools.Geral();
    }

    public static void Marker(int Index)
    {
        // Limpa a Structure
        Markers.List[Index] = new Markers.Structure();
        Markers.List[Index].Geral = new Tools.Geral();
    }

    public static void Player(byte Index)
    {
        // Limpa a Structure
        Lists.Player[Index] = new Lists.Structures.Player();
        Lists.Player[Index].Vital = new short[(byte)Jogo.Vital.Amount];
        Lists.Player[Index].Max_Vital = new short[(byte)Jogo.Vital.Amount];
        Lists.Player[Index].Attribute = new short[(byte)Jogo.Attributes.Amount];
        Lists.Player[Index].Equipment = new short[(byte)Jogo.Equipments.Amount];

        // Reseta os valores
        Lists.Player[Index].Name = string.Empty;
    }
}