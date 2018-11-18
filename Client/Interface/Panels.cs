public class Panels
{
    // Armazenamento dos Data da ferramenta
    public static Structure[] List = new Structure[1];

    // Tool structure
    public class Structure
    {
        public byte Texture;
        public Tools.General General;
    }

    public static byte EncontrarIndex(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return List[i];

        return null;
    }

    public static void Menu_Fechar()
    {
        // Fecha todos os paineis abertos
        Encontrar("Conectar").General.Visível = false;
        Encontrar("Registrar").General.Visível = false;
        Encontrar("Opções").General.Visível = false;
        Encontrar("SelecionarCharacter").General.Visível = false;
        Encontrar("CriarCharacter").General.Visível = false;
    }
}