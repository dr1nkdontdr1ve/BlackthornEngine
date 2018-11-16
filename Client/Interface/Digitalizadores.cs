using System;
using System.Drawing;
using System.Windows.Forms;

public class Digitalizadores
{
    // Armazenamento de dados da ferramenta
    public static Estrutura[] Lista = new Estrutura[1];

    // Digitalizador focado
    public static byte Foco;
    public static bool Sinal;

    // Estrutura da ferramenta
    public class Estrutura
    {
        public string Texto;
        public short Máx_Carácteres;
        public short Largura;
        public bool Senha;
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

    public static void Focalizar()
    {
        // Se o digitalizador não estiver habilitado então isso não é necessário 
        if (Lista[Foco] != null && Lista[Foco].Geral.Habilitado) return;

        // Altera o digitalizador focado para o mais próximo
        for (byte i = 1; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
        {
            if (Ferramentas.Ordem[i].Tipo != Ferramentas.Tipos.Digitalizador)
                continue;
            else if (!Lista[Ferramentas.Ordem[i].Índice].Geral.Habilitado)
                continue;
            else if (i == EncontrarÍndice("Chat"))

                Foco = Ferramentas.Ordem[i].Índice;
            return;
        }
    }

    public static void TrocarFoco()
    {
        // Altera o digitalizador focado para o próximo
        for (byte i = 1; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
        {
            if (Ferramentas.Ordem[i].Tipo != Ferramentas.Tipos.Digitalizador)
                continue;
            else if (!Lista[Ferramentas.Ordem[i].Índice].Geral.Habilitado)
                continue;
            if (Foco != Último() && i <= Ferramentas.Encontrar(Ferramentas.Tipos.Digitalizador, Foco))
                continue;

            Foco = Ferramentas.Ordem[i].Índice;
            return;
        }
    }

    public static byte Último()
    {
        byte Índice = 0;

        // Retorna o último digitalizador habilitado
        for (byte i = 1; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
            if (Ferramentas.Ordem[i].Tipo == Ferramentas.Tipos.Digitalizador)
                if (Lista[Ferramentas.Ordem[i].Índice].Geral.Habilitado)
                    Índice = Ferramentas.Ordem[i].Índice;

        return Índice;
    }

    public static void Chat_Digitar()
    {
        byte Índice = EncontrarÍndice("Chat");

        // Somente se necessário
        if (!Jogador.EstáJogando(Jogador.MeuÍndice)) return;

        // Altera a visiblidade da caixa
        Paineis.Encontrar("Chat").Geral.Visível = !Paineis.Encontrar("Chat").Geral.Visível;

        // Altera o foco do digitalizador
        if (Paineis.Encontrar("Chat").Geral.Visível)
        {
            Ferramentas.Linhas_Visível = true;
            Foco = Índice;
            return;
        }
        else
            Foco = 0;

        // Dados
        string Mensagem = Lista[Índice].Texto;
        string Jogador_Nome = Jogador.Eu.Nome;

        // Somente se necessário
        if (Mensagem.Length < 3)
        {
            Lista[Índice].Texto = string.Empty;
            return;
        }

        // Separa as mensagens em partes
        string[] Partes = Mensagem.Split(' ');

        // Global
        if (Mensagem.Substring(0, 1) == "'")
            Enviar.Mensagem(Mensagem.Substring(1), Game.Mensagens.Global);
        // Particular
        else if (Mensagem.Substring(0, 1) == "!")
        {
            // Previni erros 
            if (Partes.GetUpperBound(0) < 1)
                Ferramentas.Adicionar("Utilize: '!' + Destinatário + 'Mensagem'", SFML.Graphics.Color.White);
            else
            {
                // Dados
                string Destinatário = Mensagem.Substring(1, Partes[0].Length - 1);
                Mensagem = Mensagem.Substring(Partes[0].Length + 1);

                // Envia a mensagem
                Enviar.Mensagem(Mensagem, Game.Mensagens.Particular, Destinatário);
            }
        }
        // Mapa
        else
            Enviar.Mensagem(Mensagem, Game.Mensagens.Mapa);

        // Limpa a caixa de texto
        Lista[Índice].Texto = string.Empty;
    }

    public class Eventos
    {
        public static void MouseUp(MouseEventArgs e, byte Índice)
        {
            // Somente se necessário
            if (!Lista[Índice].Geral.Habilitado) return;
            if (!Ferramentas.EstáSobrepondo(new Rectangle(Lista[Índice].Geral.Posição, new Size(Lista[Índice].Largura, Gráficos.TTamanho(Gráficos.Tex_Digitalizador).Height)))) return;

            // Define o foco no Digitalizador
            Foco = Índice;
        }

        public static void KeyPress(KeyPressEventArgs e)
        {
            // Se não tiver nenhum focado então sair
            if (Foco == 0) return;

            // Altera o foco do digitalizador para o próximo
            if (e.KeyChar == (char)Keys.Tab)
            {
                TrocarFoco();
                return;
            }

            // Texto
            string Texto = Lista[Foco].Texto;

            // Apaga a última letra do texto
            if (!string.IsNullOrEmpty(Texto))
            {
                if (e.KeyChar == '\b' && Texto.Length > 0)
                {
                    Lista[Foco].Texto = Texto.Remove(Texto.Length - 1);
                    return;
                }

                // Não adicionar se já estiver no máximo de caracteres
                if (Lista[Foco].Máx_Carácteres > 0)
                    if (Texto.Length >= Lista[Foco].Máx_Carácteres)
                        return;
            }

            // Adiciona apenas os caractres válidos ao digitalizador
            if (e.KeyChar >= 32 && e.KeyChar <= 126) Lista[Foco].Texto += e.KeyChar.ToString();
        }
    }
}