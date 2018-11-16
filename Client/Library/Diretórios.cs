using System.IO;
using System.Windows.Forms;

class Diretórios
{
    // Formato de todos os arquivos de dados
    public const string Formato = ".dat";

    // Diretório dos arquivos
    public static DirectoryInfo Sons = new DirectoryInfo(Application.StartupPath + @"\Aúdio\Sons\");
    public static DirectoryInfo Músicas = new DirectoryInfo(Application.StartupPath + @"\Aúdio\Músicas\");
    public static FileInfo Opções = new FileInfo(Application.StartupPath + @"\Dados\Opções" + Formato);
    public static FileInfo Cliente_Dados = new FileInfo(Application.StartupPath + @"\Dados\Gerais" + Formato);
    public static DirectoryInfo Mapas_Dados = new DirectoryInfo(Application.StartupPath + @"\Dados\Mapas\");
    public static DirectoryInfo Botões_Dados = new DirectoryInfo(Application.StartupPath + @"\Dados\Ferramentas\Botões\");
    public static DirectoryInfo Digitalizadores_Dados = new DirectoryInfo(Application.StartupPath + @"\Dados\Ferramentas\Digitalizadores\");
    public static DirectoryInfo Paineis_Dados = new DirectoryInfo(Application.StartupPath + @"\Dados\Ferramentas\Paineis\");
    public static DirectoryInfo Marcadores_Dados = new DirectoryInfo(Application.StartupPath + @"\Dados\Ferramentas\Marcadores\");
    public static DirectoryInfo Tex_Paineis = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Interface\Ferramentas\Paineis\");
    public static DirectoryInfo Tex_Botões = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Interface\Ferramentas\Botões\");
    public static FileInfo Tex_Marcador = new FileInfo(Application.StartupPath + @"\Gráficos\Interface\Ferramentas\Marcador");
    public static FileInfo Tex_Digitalizador = new FileInfo(Application.StartupPath + @"\Gráficos\Interface\Ferramentas\Digitalizador");
    public static DirectoryInfo Tex_Personagens = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Personagens\");
    public static DirectoryInfo Tex_Azulejos = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Azulejos\");
    public static DirectoryInfo Tex_Faces = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Faces\");
    public static FileInfo Tex_Fundo = new FileInfo(Application.StartupPath + @"\Gráficos\Interface\Fundo");
    public static DirectoryInfo Tex_Panoramas = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Panoramas\");
    public static DirectoryInfo Tex_Fumaças = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Fumaças\");
    public static FileInfo Tex_Chat = new FileInfo(Application.StartupPath + @"\Gráficos\Interface\Chat");
    public static FileInfo Tex_Clima = new FileInfo(Application.StartupPath + @"\Gráficos\Clima");
    public static FileInfo Tex_Preenchido = new FileInfo(Application.StartupPath + @"\Gráficos\Preenchido");
    public static FileInfo Tex_Direções = new FileInfo(Application.StartupPath + @"\Gráficos\Direções");
    public static FileInfo Tex_Sombra = new FileInfo(Application.StartupPath + @"\Gráficos\Sombra");
    public static FileInfo Tex_Barras = new FileInfo(Application.StartupPath + @"\Gráficos\Barras");
    public static FileInfo Tex_Barras_Painel = new FileInfo(Application.StartupPath + @"\Gráficos\Barras_Painel");
    public static DirectoryInfo Tex_Luzes = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Luzes\");
    public static DirectoryInfo Fontes = new DirectoryInfo(Application.StartupPath + @"\Fontes\");
    public static DirectoryInfo Tex_Itens = new DirectoryInfo(Application.StartupPath + @"\Gráficos\Itens\");
    public static FileInfo Tex_Grade = new FileInfo(Application.StartupPath + @"\Gráficos\Grade");
    public static FileInfo Tex_Equipamentos = new FileInfo(Application.StartupPath + @"\Gráficos\Interface\Equipamentos");

    public static void Criar()
    {
        // Cria todos os diretórios do jogo
        Fontes.Create();
        Sons.Create();
        Músicas.Create();
        Cliente_Dados.Directory.Create();
        Botões_Dados.Create();
        Digitalizadores_Dados.Create();
        Paineis_Dados.Create();
        Marcadores_Dados.Create();
        Tex_Panoramas.Create();
        Tex_Fumaças.Create();
        Tex_Personagens.Create();
        Tex_Faces.Create();
        Tex_Paineis.Create();
        Tex_Botões.Create();
        Tex_Marcador.Directory.Create();
        Tex_Digitalizador.Directory.Create();
        Tex_Chat.Directory.Create();
        Tex_Fundo.Directory.Create();
        Tex_Azulejos.Create();
        Tex_Luzes.Create();
        Tex_Clima.Directory.Create();
        Tex_Preenchido.Directory.Create();
        Tex_Direções.Directory.Create();
        Tex_Sombra.Directory.Create();
        Tex_Barras.Directory.Create();
        Tex_Barras_Painel.Directory.Create();
        Tex_Itens.Create();
        Tex_Grade.Directory.Create();
        Tex_Equipamentos.Directory.Create();
    }
}