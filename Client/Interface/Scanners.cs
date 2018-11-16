using System;
using System.Drawing;
using System.Windows.Forms;

public class Scanners
{
    // Armazenamento de dados da ferramenta
    public static Structure[] List = new Structure[1];

    // Focused Scanner
    public static byte Foco;
    public static bool Sinal;

    // Estrutura da ferramenta
    public class Structure
    {
        public string Texto;
        public short Máx_Carácteres;
        public short Largura;
        public bool Senha;
        public Tools.General General;
    }

    public static byte EncontrarÍndice(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Nome == Nome)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Nome)
    {
        // Lista os nomes das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Nome == Nome)
                return List[i];

        return null;
    }

    public static void Focalizar()
    {
        // Se o digitalizador não estiver habilitado então isso não é necessário 
        if (List[Foco] != null && List[Foco].General.Habilitado) return;

        // Altera o digitalizador focado para o mais próximo
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
        {
            if (Tools.Ordem[i].Tipo != Tools.Tipos.Digitalizador)
                continue;
            else if (!List[Tools.Ordem[i].Índice].General.Habilitado)
                continue;
            else if (i == EncontrarÍndice("Chat"))

                Foco = Tools.Ordem[i].Índice;
            return;
        }
    }

    public static void TrocarFoco()
    {
        // Altera o digitalizador focado para o próximo
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
        {
            if (Tools.Ordem[i].Tipo != Tools.Tipos.Digitalizador)
                continue;
            else if (!List[Tools.Ordem[i].Índice].General.Habilitado)
                continue;
            if (Foco != Último() && i <= Tools.Encontrar(Tools.Tipos.Digitalizador, Foco))
                continue;

            Foco = Tools.Ordem[i].Índice;
            return;
        }
    }

    public static byte Último()
    {
        byte Índice = 0;

        // Retorna o último digitalizador habilitado
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
            if (Tools.Ordem[i].Tipo == Tools.Tipos.Digitalizador)
                if (List[Tools.Ordem[i].Índice].General.Habilitado)
                    Índice = Tools.Ordem[i].Índice;

        return Índice;
    }

    public static void Chat_Digitar()
    {
        byte Índice = EncontrarÍndice("Chat");

        // Somente se necessário
        if (!Player.EstáJogando(Player.MeuÍndice)) return;

        // Altera a visiblidade da caixa
        Panels.Encontrar("Chat").General.Visível = !Panels.Encontrar("Chat").General.Visível;

        // Altera o foco do digitalizador
        if (Panels.Encontrar("Chat").General.Visível)
        {
            Tools.Linhas_Visível = true;
            Foco = Índice;
            return;
        }
        else
            Foco = 0;

        // Dados
        string Mensagem = List[Índice].Texto;
        string Jogador_Nome = Player.Eu.Nome;

        // Somente se necessário
        if (Mensagem.Length < 3)
        {
            List[Índice].Texto = string.Empty;
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
                Tools.Adicionar("Use: '!' + Destination + 'Message'", SFML.Graphics.Color.White);
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
        List[Índice].Texto = string.Empty;
    }

    public class Eventos
    {
        public static void MouseUp(MouseEventArgs e, byte Índice)
        {
            // Somente se necessário
            if (!List[Índice].General.Habilitado) return;
            if (!Tools.EstáSobrepondo(new Rectangle(List[Índice].General.Posição, new Size(List[Índice].Largura, Gráficos.TTamanho(Gráficos.Tex_Digitalizador).Height)))) return;

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
            string Texto = List[Foco].Texto;

            // Apaga a última letra do texto
            if (!string.IsNullOrEmpty(Texto))
            {
                if (e.KeyChar == '\b' && Texto.Length > 0)
                {
                    List[Foco].Texto = Texto.Remove(Texto.Length - 1);
                    return;
                }

                // Não adicionar se já estiver no máximo de caracteres
                if (List[Foco].Máx_Carácteres > 0)
                    if (Texto.Length >= List[Foco].Máx_Carácteres)
                        return;
            }

            // Adiciona apenas os caractres válidos ao digitalizador
            if (e.KeyChar >= 32 && e.KeyChar <= 126) List[Foco].Texto += e.KeyChar.ToString();
        }
    }
}