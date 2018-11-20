public class Panels
{
    // Armazenamento dos Data da ferramenta
    public static Structure[] List = new Structure[1];

    // Tool structure
    public class Structure
    {
        public byte Texture;
        public Tools.Geral Geral;
    }

    public static byte LocateIndex(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return i;

        return 0;
    }

    public static Structure Locate(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return List[i];

        return null;
    }

    public static void Menu_Close()
    {
        // Fecha todos os Panels abertos
        Locate("Connect").Geral.Visible = false;
        Locate("Register").Geral.Visible = false;
        Locate("Options").Geral.Visible = false;
        Locate("SelectCharacter").Geral.Visible = false;
        Locate("CreateCharacter").Geral.Visible = false;
    }
}