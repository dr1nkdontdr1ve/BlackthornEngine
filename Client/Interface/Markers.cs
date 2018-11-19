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

    public static byte LocateIndex(string Name)
    {
        // List the names of the tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return i;

        return 0;
    }

    public static Structure Locate(string Name)
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
            int Text_Width; Size Texture_Size; Size Caixa;

            // Only if necessary
            if (!List[Index].General.Habilitado) return;

            // Marker size
            Texture_Size = Graphics.MySize(Graphics.Tex_Marker);
            Text_Width = Tools.MeasureText_Width(List[Index].Text);
            Caixa = new Size(Texture_Size.Width / 2 + Text_Width + Margin, Texture_Size.Height);

            // Somente se estiver Overlapping a ferramenta
            if (!Tools.EstáOverlapping(new Rectangle(List[Index].General.Position, Caixa))) return;

            // Altera o State do Marker
            List[Index].State = !List[Index].State;

            // Executa o evento
            Run(List[Index].General.Name);
            Audio.Som.Reproduce(Audio.Sons.Click);
        }

        public static void Run(string Name)
        {
            // Executa o evento do Marker
            switch (Name)
            {
                case "Sons": Sons(); break;
                case "Músicas": Músicas(); break;
                case "SaveUser": SaveUser(); break;
                case "GenreMasculino": GenreMale(); break;
                case "GenreFeminino": GenreFemale(); break;
            }
        }

        public static void Sons()
        {
            // Salva os Data
            Lists.Options.Sons = Locate("Sons").State;
            Write.Options();
        }

        public static void Músicas()
        {
            // Save the Data
            Lists.Options.Músicas = Locate("Músicas").State;
            Write.Options();

            // To or Play Music depending on Marker State
            if (!Lists.Options.Músicas)
                Audio.Música.Stop();
            else
                Audio.Música.Reproduce(Audio.Músicas.Menu);
        }

        public static void SaveUser()
        {
            // Salva os Data
            Lists.Options.SaveUser = Locate("SaveUser").State;
            Write.Options();
        }

        public static void GenreMale()
        {
            // Changes marker status of another genre
            Locate("GenreFeminino").State = !Locate("GenreMasculino").State;
        }

        public static void GenreFemale()
        {
            // Changes marker status of another genre
            Locate("GenreMasculino").State = !Locate("GenreFeminino").State;
        }
    }
}