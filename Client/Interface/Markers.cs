using System.Drawing;
using System.Windows.Forms;

public class Markers
{
    // Tool data storage
    public static Structure[] List = new Structure[1];

    // Margin from texture to text
    public const byte Margin = 4;

    // Tool structure
    public class Structure
    {
        public string Text;
        public bool State;
        public Tools.General General;
    }

    public static byte EncontrarIndex(string Name)
    {
        // List the names of the tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Name)
    {
        // List the names of the tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return List[i];

        return null;
    }

    public class Events
    {
        public static void MouseUp(MouseEventArgs e, byte Index)
        {
            int Texto_Largura; Size Texture_Tamanho; Size Caixa;

            // Only if necessary
            if (!List[Index].General.Habilitado) return;

            // Marker size
            Texture_Tamanho = Gráficos.TTamanho(Gráficos.Tex_Marcador);
            Texto_Largura = Tools.MedirTexto_Largura(List[Index].Text);
            Caixa = new Size(Texture_Tamanho.Width / 2 + Texto_Largura + Margin, Texture_Tamanho.Height);

            // Somente se estiver sobrepondo a ferramenta
            if (!Tools.EstáSobrepondo(new Rectangle(List[Index].General.Posição, Caixa))) return;

            // Altera o estado do marcador
            List[Index].State = !List[Index].State;

            // Executa o evento
            Executar(List[Index].General.Name);
            Áudio.Som.Reproduzir(Áudio.Sons.Clique);
        }

        public static void Executar(string Name)
        {
            // Executa o evento do marcador
            switch (Name)
            {
                case "Sons": Sons(); break;
                case "Músicas": Músicas(); break;
                case "SalvarUsuário": SaveUser(); break;
                case "GêneroMasculino": GenreMale(); break;
                case "GêneroFeminino": GenreFemale(); break;
            }
        }

        public static void Sons()
        {
            // Salva os Data
            Lists.Opções.Sons = Encontrar("Sons").State;
            Escrever.Opções();
        }

        public static void Músicas()
        {
            // Salva os Data
            Lists.Opções.Músicas = Encontrar("Músicas").State;
            Escrever.Opções();

            // Para ou reproduz a música dependendo do estado do marcador
            if (!Lists.Opções.Músicas)
                Áudio.Música.Parar();
            else
                Áudio.Música.Reproduzir(Áudio.Músicas.Menu);
        }

        public static void SaveUser()
        {
            // Salva os Data
            Lists.Opções.SalvarUsuário = Encontrar("SaveUser").State;
            Escrever.Opções();
        }

        public static void GenreMale()
        {
            // Changes marker status of another genre
            Encontrar("GêneroFeminino").State = !Encontrar("GêneroMasculino").State;
        }

        public static void GenreFemale()
        {
            // Changes marker status of another genre
            Encontrar("GêneroMasculino").State = !Encontrar("GêneroFeminino").State;
        }
    }
}