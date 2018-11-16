using System.IO;
using System.Windows.Forms;

class Diretórios
{
    // Formato de todos os arquivos de dados
    public const string Formato = ".dat";

    // Diretório dos arquivos
    public static FileInfo Servidor_Dados = new FileInfo(Application.StartupPath + @"\Dados\Gerais" + Formato);
    public static DirectoryInfo Contas = new DirectoryInfo(Application.StartupPath + @"\Dados\Contas\");
    public static FileInfo Personagens = new FileInfo(Application.StartupPath + @"\Dados\Personagens" + Formato);
    public static DirectoryInfo Classes = new DirectoryInfo(Application.StartupPath + @"\Dados\Classes\");
    public static DirectoryInfo Azulejos = new DirectoryInfo(Application.StartupPath + @"\Dados\Azulejos\");
    public static DirectoryInfo Mapas = new DirectoryInfo(Application.StartupPath + @"\Dados\Mapas\");
    public static DirectoryInfo NPCs = new DirectoryInfo(Application.StartupPath + @"\Dados\NPCs\");
    public static DirectoryInfo Itens = new DirectoryInfo(Application.StartupPath + @"\Dados\Itens\");

    public static void Criar()
    {
        // Cria todos os diretórios do Game
        Servidor_Dados.Directory.Create();
        Contas.Create();
        Personagens.Directory.Create();
        Classes.Create();
        Azulejos.Create();
        Mapas.Create();
        NPCs.Create();
        Itens.Create();
    }
}