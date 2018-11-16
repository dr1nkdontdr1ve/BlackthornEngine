public class Panels
{
    // Armazenamento dos dados da ferramenta
    public static Structure[] List = new Structure[1];

    // Tool structure
    public class Structure
    {
        public byte Texture;
        public Tools.General General;
    }

    public static byte EncontrarÍndice(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Nome == Nome)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Nome == Nome)
                return List[i];

        return null;
    }

    public static void Menu_Fechar()
    {
        // Fecha todos os paineis abertos
        Encontrar("Conectar").General.Visível = false;
        Encontrar("Registrar").General.Visível = false;
        Encontrar("Opções").General.Visível = false;
        Encontrar("SelecionarPersonagem").General.Visível = false;
        Encontrar("CriarPersonagem").General.Visível = false;
    }
}