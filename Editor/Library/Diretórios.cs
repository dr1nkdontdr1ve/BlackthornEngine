using System.IO;
using System.Windows.Forms;

class Diretórios
{
    // Formato de todos os arquivos de dados
    public const string Formato = ".dat";

    // Editor
    public static FileInfo Opções = new FileInfo(Application.StartupPath + @"\Opções" + Formato);
    public static DirectoryInfo Capturas = new DirectoryInfo(Application.StartupPath + @"\Capturas\");

    // Servidor
    public static FileInfo Servidor_Dados;
    public static DirectoryInfo Classes_Dados;
    public static DirectoryInfo Azulejos_Dados;
    public static DirectoryInfo Mapas_Dados;
    public static DirectoryInfo NPCs_Dados;
    public static DirectoryInfo Itens_Dados;

    // Cliente
    public static DirectoryInfo Fontes;
    public static DirectoryInfo Sons;
    public static DirectoryInfo Músicas;
    public static FileInfo Cliente_Dados;
    public static DirectoryInfo Botões_Dados;
    public static DirectoryInfo Digitalizadores_Dados;
    public static DirectoryInfo Paineis_Dados;
    public static DirectoryInfo Marcadores_Dados;
    public static DirectoryInfo Tex_Paineis;
    public static FileInfo Tex_Marcador;
    public static FileInfo Tex_Digitalizador;
    public static DirectoryInfo Tex_Botões;
    public static DirectoryInfo Tex_Personagens;
    public static DirectoryInfo Tex_Faces;
    public static DirectoryInfo Tex_Panoramas;
    public static DirectoryInfo Tex_Fumaças;
    public static DirectoryInfo Tex_Azulejos;
    public static DirectoryInfo Tex_Luzes;
    public static DirectoryInfo Tex_Itens;
    public static FileInfo Tex_Grade;
    public static FileInfo Tex_Clima;
    public static FileInfo Tex_Preenchido;
    public static FileInfo Tex_Direções;
    public static FileInfo Tex_Transparente;
    public static FileInfo Tex_Iluminação;

    public static void Definir_Cliente()
    {
        string Diretório = Listas.Opções.Diretório_Cliente;

        // Previni erros
        if (!Directory.Exists(Diretório))
        {
            Listas.Opções.Diretório_Cliente = string.Empty;
            Escrever.Opções();
            return;
        }

        // Demonstra o diretório
        Seleção.Objetos.txtCliente_Diretório.Text = Diretório;

        // Cliente
        Fontes = new DirectoryInfo(Diretório + @"\Fontes\");
        Sons = new DirectoryInfo(Diretório + @"\Aúdio\Sons\");
        Músicas = new DirectoryInfo(Diretório + @"\Aúdio\Músicas\");
        Cliente_Dados = new FileInfo(Diretório + @"\Dados\Gerais" + Formato);
        Botões_Dados = new DirectoryInfo(Diretório + @"\Dados\Ferramentas\Botões\");
        Digitalizadores_Dados = new DirectoryInfo(Diretório + @"\Dados\Ferramentas\Digitalizadores\");
        Paineis_Dados = new DirectoryInfo(Diretório + @"\Dados\Ferramentas\Paineis\");
        Marcadores_Dados = new DirectoryInfo(Diretório + @"\Dados\Ferramentas\Marcadores\");
        Tex_Panoramas = new DirectoryInfo(Diretório + @"\Gráficos\Panoramas\");
        Tex_Luzes = new DirectoryInfo(Diretório + @"\Gráficos\Luzes\");
        Tex_Fumaças = new DirectoryInfo(Diretório + @"\Gráficos\Fumaças\");
        Tex_Personagens = new DirectoryInfo(Diretório + @"\Gráficos\Personagens\");
        Tex_Faces = new DirectoryInfo(Diretório + @"\Gráficos\Faces\");
        Tex_Paineis = new DirectoryInfo(Diretório + @"\Gráficos\Interface\Ferramentas\Paineis\");
        Tex_Botões = new DirectoryInfo(Diretório + @"\Gráficos\Interface\Ferramentas\Botões\");
        Tex_Marcador = new FileInfo(Diretório + @"\Gráficos\Interface\Ferramentas\Marcador");
        Tex_Digitalizador = new FileInfo(Diretório + @"\Gráficos\Interface\Ferramentas\Digitalizador");
        Tex_Azulejos = new DirectoryInfo(Diretório + @"\Gráficos\Azulejos\");
        Tex_Grade = new FileInfo(Diretório + @"\Gráficos\Grade");
        Tex_Clima = new FileInfo(Diretório + @"\Gráficos\Clima");
        Tex_Preenchido = new FileInfo(Diretório + @"\Gráficos\Preenchido");
        Tex_Direções = new FileInfo(Diretório + @"\Gráficos\Direções");
        Tex_Transparente = new FileInfo(Diretório + @"\Gráficos\Transparente");
        Tex_Iluminação = new FileInfo(Diretório + @"\Gráficos\Iluminação");
        Tex_Itens = new DirectoryInfo(Diretório + @"\Gráficos\Itens\");

        // Cria os diretórios
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
        Tex_Luzes.Create();
        Tex_Personagens.Create();
        Tex_Faces.Create();
        Tex_Paineis.Create();
        Tex_Botões.Create();
        Tex_Marcador.Directory.Create();
        Tex_Digitalizador.Directory.Create();
        Tex_Azulejos.Create();
        Tex_Grade.Directory.Create();
        Tex_Clima.Directory.Create();
        Tex_Preenchido.Directory.Create();
        Tex_Direções.Directory.Create();
        Tex_Transparente.Directory.Create();
        Tex_Iluminação.Directory.Create();
        Tex_Itens.Create();

        // Lê os dados gerais do cliente
        Ler.Cliente_Dados();
        Gráficos.LerTexturas();
        Áudio.Som.Ler();
    }

    public static void Definir_Servidor()
    {
        string Diretório = Listas.Opções.Diretório_Servidor;

        // Previni erros
        if (!Directory.Exists(Diretório))
        {
            Listas.Opções.Diretório_Servidor = string.Empty;
            Escrever.Opções();
            return;
        }

        // Demonstra o diretório
        Seleção.Objetos.txtServidor_Diretório.Text = Diretório;

        // Define os diretórios
        Servidor_Dados = new FileInfo(Diretório + @"\Dados\Gerais" + Formato);
        Classes_Dados = new DirectoryInfo(Diretório + @"\Dados\Classes\");
        Azulejos_Dados = new DirectoryInfo(Diretório + @"\Dados\Azulejos\");
        Mapas_Dados = new DirectoryInfo(Diretório + @"\Dados\Mapas\");
        NPCs_Dados = new DirectoryInfo(Diretório + @"\Dados\NPCs\");
       Itens_Dados = new DirectoryInfo(Diretório + @"\Dados\Itens\");

        // Cria os diretórios
        Servidor_Dados.Directory.Create();
        Classes_Dados.Create();
        Azulejos_Dados.Create();
        NPCs_Dados.Create();
        Mapas_Dados.Create();
        Itens_Dados.Create();

        // Lê os dados gerais do servidor
        Ler.Servidor_Dados();
    }
}