public class Paineis
{
    // Armazenamento dos dados da ferramenta
    public static Estrutura[] Lista = new Estrutura[1];

    // Estrutura da ferramenta
    public class Estrutura
    {
        public byte Textura;
        public Ferramentas.Geral Geral;
    }

    public static byte EncontrarÍndice(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= Lista.GetUpperBound(0); i++)
            if (Lista[i].Geral.Nome == Nome)
                return i;

        return 0;
    }

    public static Estrutura Encontrar(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= Lista.GetUpperBound(0); i++)
            if (Lista[i].Geral.Nome == Nome)
                return Lista[i];

        return null;
    }

    public static void Menu_Fechar()
    {
        // Fecha todos os paineis abertos
        Encontrar("Conectar").Geral.Visível = false;
        Encontrar("Registrar").Geral.Visível = false;
        Encontrar("Opções").Geral.Visível = false;
        Encontrar("SelecionarPersonagem").Geral.Visível = false;
        Encontrar("CriarPersonagem").Geral.Visível = false;
    }
}