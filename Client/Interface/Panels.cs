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

    public static byte LocateIndex(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return i;

        return 0;
    }

    public static Structure Locate(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return List[i];

        return null;
    }

    public static void Menu_Close()
    {
        // Fecha todos os Panels abertos
        Locate("Connect").General.Visible = false;
        Locate("Register").General.Visible = false;
        Locate("Options").General.Visible = false;
        Locate("SelectCharacter").General.Visible = false;
        Locate("CreateCharacter").General.Visible = false;
    }
}