using System.Drawing;
using System.Windows.Forms;

public class Marcadores
{
    // Armazenamento dos dados da ferramenta
    public static Estrutura[] Lista = new Estrutura[1];

    // Margem da textura até o texto
    public const byte Margem = 4;

    // Estrutura da ferramenta
    public class Estrutura
    {
        public string Texto;
        public bool Estado;
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

    public class Eventos
    {
        public static void MouseUp(MouseEventArgs e, byte Índice)
        {
            int Texto_Largura; Size Textura_Tamanho; Size Caixa;

            // Somente se necessário
            if (!Lista[Índice].Geral.Habilitado) return;

            // Tamanho do marcador
            Textura_Tamanho = Gráficos.TTamanho(Gráficos.Tex_Marcador);
            Texto_Largura = Ferramentas.MedirTexto_Largura(Lista[Índice].Texto);
            Caixa = new Size(Textura_Tamanho.Width / 2 + Texto_Largura + Margem, Textura_Tamanho.Height);

            // Somente se estiver sobrepondo a ferramenta
            if (!Ferramentas.EstáSobrepondo(new Rectangle(Lista[Índice].Geral.Posição, Caixa))) return;

            // Altera o estado do marcador
            Lista[Índice].Estado = !Lista[Índice].Estado;

            // Executa o evento
            Executar(Lista[Índice].Geral.Nome);
            Áudio.Som.Reproduzir(Áudio.Sons.Clique);
        }

        public static void Executar(string Nome)
        {
            // Executa o evento do marcador
            switch (Nome)
            {
                case "Sons": Sons(); break;
                case "Músicas": Músicas(); break;
                case "SalvarUsuário": SalvarUsuário(); break;
                case "GêneroMasculino": GêneroMasculino(); break;
                case "GêneroFeminino": GêneroFeminino(); break;
            }
        }

        public static void Sons()
        {
            // Salva os dados
            Listas.Opções.Sons = Encontrar("Sons").Estado;
            Escrever.Opções();
        }

        public static void Músicas()
        {
            // Salva os dados
            Listas.Opções.Músicas = Encontrar("Músicas").Estado;
            Escrever.Opções();

            // Para ou reproduz a música dependendo do estado do marcador
            if (!Listas.Opções.Músicas)
                Áudio.Música.Parar();
            else
                Áudio.Música.Reproduzir(Áudio.Músicas.Menu);
        }

        public static void SalvarUsuário()
        {
            // Salva os dados
            Listas.Opções.SalvarUsuário = Encontrar("SalvarUsuário").Estado;
            Escrever.Opções();
        }

        public static void GêneroMasculino()
        {
            // Altera o estado do marcador de outro gênero
            Encontrar("GêneroFeminino").Estado = !Encontrar("GêneroMasculino").Estado;
        }

        public static void GêneroFeminino()
        {
            // Altera o estado do marcador de outro gênero
            Encontrar("GêneroMasculino").Estado = !Encontrar("GêneroFeminino").Estado;
        }
    }
}