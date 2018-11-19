using System.IO;
using System.Windows.Forms;

class Directories
{
    // Format de todos os arquivos de Data
    public const string Format = ".dat";

    // Diretório dos arquivos
    public static DirectoryInfo Sons = new DirectoryInfo(Application.StartupPath + @"\Audio\Sons\");
    public static DirectoryInfo Músicas = new DirectoryInfo(Application.StartupPath + @"\Audio\Músicas\");
    public static FileInfo Options = new FileInfo(Application.StartupPath + @"\Data\Options" + Format);
    public static FileInfo Client_Data = new FileInfo(Application.StartupPath + @"\Data\General" + Format);
    public static DirectoryInfo Maps_Data = new DirectoryInfo(Application.StartupPath + @"\Data\Maps\");
    public static DirectoryInfo Buttons_Data = new DirectoryInfo(Application.StartupPath + @"\Data\Tools\Buttons\");
    public static DirectoryInfo Scanners_Data = new DirectoryInfo(Application.StartupPath + @"\Data\Tools\Scanners\");
    public static DirectoryInfo Panels_Data = new DirectoryInfo(Application.StartupPath + @"\Data\Tools\Panels\");
    public static DirectoryInfo Markers_Data = new DirectoryInfo(Application.StartupPath + @"\Data\Tools\Markers\");
    public static DirectoryInfo Tex_Panels = new DirectoryInfo(Application.StartupPath + @"\Graphics\Interface\Tools\Panels\");
    public static DirectoryInfo Tex_Buttons = new DirectoryInfo(Application.StartupPath + @"\Graphics\Interface\Tools\Buttons\");
    public static FileInfo Tex_Marker = new FileInfo(Application.StartupPath + @"\Graphics\Interface\Tools\Marker");
    public static FileInfo Tex_Scanner = new FileInfo(Application.StartupPath + @"\Graphics\Interface\Tools\Scanner");
    public static DirectoryInfo Tex_Characters = new DirectoryInfo(Application.StartupPath + @"\Graphics\Characters\");
    public static DirectoryInfo Tex_Tiles = new DirectoryInfo(Application.StartupPath + @"\Graphics\Tiles\");
    public static DirectoryInfo Tex_Faces = new DirectoryInfo(Application.StartupPath + @"\Graphics\Faces\");
    public static FileInfo Tex_Fundo = new FileInfo(Application.StartupPath + @"\Graphics\Interface\Fundo");
    public static DirectoryInfo Tex_Panoramas = new DirectoryInfo(Application.StartupPath + @"\Graphics\Panoramas\");
    public static DirectoryInfo Tex_Smokes = new DirectoryInfo(Application.StartupPath + @"\Graphics\Smokes\");
    public static FileInfo Tex_Chat = new FileInfo(Application.StartupPath + @"\Graphics\Interface\Chat");
    public static FileInfo Tex_Climate = new FileInfo(Application.StartupPath + @"\Graphics\Climate");
    public static FileInfo Tex_Preenchido = new FileInfo(Application.StartupPath + @"\Graphics\Preenchido");
    public static FileInfo Tex_Location = new FileInfo(Application.StartupPath + @"\Graphics\Location");
    public static FileInfo Tex_Sombra = new FileInfo(Application.StartupPath + @"\Graphics\Sombra");
    public static FileInfo Tex_Bars = new FileInfo(Application.StartupPath + @"\Graphics\Bars");
    public static FileInfo Tex_Bars_Panel = new FileInfo(Application.StartupPath + @"\Graphics\Bars_Panel");
    public static DirectoryInfo Tex_Lightes = new DirectoryInfo(Application.StartupPath + @"\Graphics\Lightes\");
    public static DirectoryInfo Fontes = new DirectoryInfo(Application.StartupPath + @"\Fontes\");
    public static DirectoryInfo Tex_Items = new DirectoryInfo(Application.StartupPath + @"\Graphics\Items\");
    public static FileInfo Tex_Grade = new FileInfo(Application.StartupPath + @"\Graphics\Grade");
    public static FileInfo Tex_Equipments = new FileInfo(Application.StartupPath + @"\Graphics\Interface\Equipments");

    public static void Create()
    {
        // Cria todos os Directories do Game
        Fontes.Create();
        Sons.Create();
        Músicas.Create();
        Client_Data.Directory.Create();
        Buttons_Data.Create();
        Scanners_Data.Create();
        Panels_Data.Create();
        Markers_Data.Create();
        Tex_Panoramas.Create();
        Tex_Smokes.Create();
        Tex_Characters.Create();
        Tex_Faces.Create();
        Tex_Panels.Create();
        Tex_Buttons.Create();
        Tex_Marker.Directory.Create();
        Tex_Scanner.Directory.Create();
        Tex_Chat.Directory.Create();
        Tex_Fundo.Directory.Create();
        Tex_Tiles.Create();
        Tex_Lightes.Create();
        Tex_Climate.Directory.Create();
        Tex_Preenchido.Directory.Create();
        Tex_Location.Directory.Create();
        Tex_Sombra.Directory.Create();
        Tex_Bars.Directory.Create();
        Tex_Bars_Panel.Directory.Create();
        Tex_Items.Create();
        Tex_Grade.Directory.Create();
        Tex_Equipments.Directory.Create();
    }
}