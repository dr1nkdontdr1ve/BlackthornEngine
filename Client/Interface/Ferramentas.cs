using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Ferramentas
{
    // Habilitação das ferramentas
    public static bool Habilitação;

    // Posição do ponteiro do mouse
    public static Point Ponteiro;

    // Janela que está focada
    public static Janelas JanelaAtual;

    // Chat
    public static bool Linhas_Visível;
    public const byte Linhas_Visíveis = 9;
    public const byte Máx_Linhas = 50;
    public static byte Linha;

    // Ordem da renderização das ferramentas
    public static Identificação[] Ordem = new Identificação[0];
    public static List<Chat_Estrutura> Chat = new List<Chat_Estrutura>();

    public struct Identificação
    {
        public byte Índice;
        public Tipos Tipo;
    }

    public class Geral
    {
        public string Nome;
        public bool Visível;
        public Point Posição;
        public bool Habilitado;

        public bool VerificarHabilitação()
        {
            // Define a habilitação da ferramenta
            if (!Visível || !Habilitação)
                return Habilitado = false;
            else
                return Habilitado = true;
        }
    }

    public class Chat_Estrutura
    {
        public string Texto;
        public SFML.Graphics.Color Cor;
    }

    // Identificação das janelas do Game
    public enum Janelas
    {
        Nenhuma,
        Menu,
        Game
    }

    // Tipos de ferramentas
    public enum Tipos
    {
        Botão,
        Painel,
        Marcador,
        Digitalizador
    }

    public static void DefinirHabilitação(string Painel, Janelas Janela)
    {
        // Define a habilitação
        if (JanelaAtual != Janela || Painel != string.Empty && !Paineis.Encontrar(Painel).Geral.Visível)
            Habilitação = false;
        else
            Habilitação = true;
    }

    public static void Listar(Tipos Tipo, byte Índice)
    {
        int Quantidade = Ordem.GetUpperBound(0) + 1;

        // Se já estiver listado não é necessário listar de novo
        if (EstáListado(Tipo, Índice))
            return;

        // Altera o tamanho da caixa
        Array.Resize(ref Ordem, Quantidade + 1);

        // Adiciona à lista
        Ordem[Quantidade].Tipo = Tipo;
        Ordem[Quantidade].Índice = Índice;
    }

    private static bool EstáListado(Tipos Tipo, byte Índice)
    {
        // Verifica se a ferramenta já está listada
        for (short i = 1; i <= Ordem.GetUpperBound(0); i++)
            if (Ordem[i].Tipo == Tipo && Ordem[i].Índice == Índice)
                return true;

        return false;
    }

    public static bool EstáSobrepondo(Rectangle Retângulo)
    {
        // Verficia se o mouse está sobre o objeto
        if (Ponteiro.X >= Retângulo.X && Ponteiro.X <= Retângulo.X + Retângulo.Width)
            if (Ponteiro.Y >= Retângulo.Y && Ponteiro.Y <= Retângulo.Y + Retângulo.Height)
                return true;

        // Se não, retornar um valor nulo
        return false;
    }

    public static int Encontrar(Tipos Tipo, byte Índice)
    {
        // Lista os nomes dos botões
        for (byte i = 1; i <= Ordem.GetUpperBound(0); i++)
            if (Ordem[i].Tipo == Tipo && Ordem[i].Índice == Índice)
                return i;

        return 0;
    }

    public static int MedirTexto_Largura(string Texto)
    {
        // Dados do texto
        SFML.Graphics.Text TempTexto = new SFML.Graphics.Text(Texto, Gráficos.Fonte);
        TempTexto.CharacterSize = 10;
        return (int)TempTexto.GetLocalBounds().Width;
    }

    public static string QuebraTexto(string Texto, int Largura)
    {
        int Texto_Largura;

        // Previni sobrecargas
        if (string.IsNullOrEmpty(Texto))
            return Texto;

        // Usado para fazer alguns calculos
        Texto_Largura = MedirTexto_Largura(Texto);

        // Diminui o tamanho do texto até que ele possa caber no digitalizador
        while (Texto_Largura - Largura >= 0)
        {
            Texto = Texto.Substring(1);
            Texto_Largura = MedirTexto_Largura(Texto);
        }

        return Texto;
    }

    public static byte EcontrarLinhaVazia()
    {
        // Encontra uma linha vazia
        for (byte i = 0; i <= Máx_Linhas; i++)
            if (Chat[i].Texto == string.Empty)
                return i;

        return 0;
    }

    public static void AdicionarLinha(string Mensagem, SFML.Graphics.Color Cor)
    {
        Chat.Add(new Chat_Estrutura());
        int i = Chat.Count - 1;

        // Adiciona a mensagem em uma linha vazia
        Chat[i].Texto = Mensagem;
        Chat[i].Cor = Cor;

        // Remove uma linha se necessário
        if (Chat.Count > Máx_Linhas) Chat.Remove(Chat[0]);
        if (i + Linha > Linhas_Visíveis + Linha)
            Linha = (byte)(i - Linhas_Visíveis);

        // Torna as linhas visíveis
        Linhas_Visível = true;
    }

    public static void Adicionar(string Mensagem, SFML.Graphics.Color Cor)
    {
        int Mensagem_Largura, Caixa_Largura = Gráficos.TTamanho(Gráficos.Tex_Painel[Paineis.Encontrar("Chat").Textura]).Width - 16;
        string Temp_Mensagem; int Separação;

        // Remove os espaços
        Mensagem = Mensagem.Trim();
        Mensagem_Largura = MedirTexto_Largura(Mensagem);

        // Caso couber, adiciona a mensagem normalmente
        if (Mensagem_Largura < Caixa_Largura)
            AdicionarLinha(Mensagem, Cor);
        else
        {
            for (int i = 0; i <= Mensagem.Length; i++)
            {
                // Verifica se o próximo caráctere é um separável 
                switch (Mensagem[i])
                {
                    case '-':
                    case '_':
                    case ' ': Separação = i; break;
                }

                Temp_Mensagem = Mensagem.Substring(0, i);

                // Adiciona o texto à caixa
                if (MedirTexto_Largura(Temp_Mensagem) > Caixa_Largura)
                {
                    AdicionarLinha(Temp_Mensagem, Cor);
                    Adicionar(Mensagem.Substring(Temp_Mensagem.Length), Cor);
                    return;
                }
            }
        }
    }

    public static byte Inventário_Sobrepondo()
    {
        byte NumColunas = 5;
        Point Painel_Posição = Paineis.Encontrar("Menu_Inventário").Geral.Posição;

        for (byte i = 1; i <= Game.Máx_Inventário; i++)
        {
            // Posição do item
            byte Linha = (byte)((i - 1) / NumColunas);
            int Coluna = i - (Linha * 5) - 1;
            Point Posição = new Point(Painel_Posição.X + 7 + Coluna * 36, Painel_Posição.Y + 30 + Linha * 36);

            // Retorna o slot em que o mouse está por cima
            if (EstáSobrepondo(new Rectangle(Posição.X, Posição.Y, 32, 32)))
                return i;
        }

        return 0;
    }

    public static void Inventário_MouseDown(MouseEventArgs e)
    {
        byte Slot = Inventário_Sobrepondo();

        // Somente se necessário
        if (Slot == 0) return;
        if (Jogador.Inventário[Slot].Item_Num == 0) return;

        // Solta item
        if (e.Button == MouseButtons.Right)
        {
            Enviar.SoltarItem(Slot);
            return;
        }
        // Seleciona o item
        else if (e.Button == MouseButtons.Left)
        {
            Jogador.Inventário_Movendo = Slot;
            return;
        }
    }

    public static void Equipamento_MouseDown(MouseEventArgs e)
    {
        Point Painel_Posição = Paineis.Encontrar("Menu_Personagem").Geral.Posição;

        for (byte i = 0; i <= (byte)Game.Equipamentos.Quantidade - 1; i++)
            if (EstáSobrepondo(new Rectangle(Painel_Posição.X + 7 + i * 36, Painel_Posição.Y + 247, 32, 32)))
                // Remove o equipamento
                if (e.Button == MouseButtons.Right)
                {
                    Enviar.Equipamento_Remover(i);
                    return;
                }
    }

    public static byte Hotbar_Sobrepondo()
    {
        Point Painel_Posição = Paineis.Encontrar("Hotbar").Geral.Posição;

        for (byte i = 1; i <= Game.Máx_Hotbar; i++)
        {
            // Posição do slot
            Point Posição = new Point(Painel_Posição.X + 8 + (i - 1) * 36, Painel_Posição.Y + 6);

            // Retorna o slot em que o mouse está por cima
            if (EstáSobrepondo(new Rectangle(Posição.X, Posição.Y, 32, 32)))
                return i;
        }

        return 0;
    }

    public static void Hotbar_MouseDown(MouseEventArgs e)
    {
        byte Slot = Hotbar_Sobrepondo();

        // Somente se necessário
        if (Slot == 0) return;
        if (Jogador.Hotbar[Slot].Slot == 0) return;

        // Solta item
        if (e.Button == MouseButtons.Right)
        {
            Enviar.Hotbar_Adicionar(Slot, 0, 0);
            return;
        }
        // Seleciona o item
        else if (e.Button == MouseButtons.Left)
        {
            Jogador.Hotbar_Movendo = Slot;
            return;
        }
    }
}