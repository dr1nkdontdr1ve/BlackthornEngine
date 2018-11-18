using System;
using System.Drawing;
using System.Windows.Forms;

public class Scanners
{
    // Armazenamento de Data da ferramenta
    public static Structure[] List = new Structure[1];

    // Focused Scanner
    public static byte Foco;
    public static bool Sinal;

    // Estrutura da ferramenta
    public class Structure
    {
        public string Texto;
        public short Max_Carácteres;
        public short Largura;
        public bool Senha;
        public Tools.General General;
    }

    public static byte EncontrarIndex(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].General.Name == Name)
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
            if (Tools.Ordem[i].Type != Tools.Types.Digitalizador)
                continue;
            else if (!List[Tools.Ordem[i].Index].General.Habilitado)
                continue;
            else if (i == EncontrarIndex("Chat"))

                Foco = Tools.Ordem[i].Index;
            return;
        }
    }

    public static void TrocarFoco()
    {
        // Altera o digitalizador focado para o próximo
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
        {
            if (Tools.Ordem[i].Type != Tools.Types.Digitalizador)
                continue;
            else if (!List[Tools.Ordem[i].Index].General.Habilitado)
                continue;
            if (Foco != Último() && i <= Tools.Encontrar(Tools.Types.Digitalizador, Foco))
                continue;

            Foco = Tools.Ordem[i].Index;
            return;
        }
    }

    public static byte Último()
    {
        byte Index = 0;

        // Retorna o último digitalizador habilitado
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
            if (Tools.Ordem[i].Type == Tools.Types.Digitalizador)
                if (List[Tools.Ordem[i].Index].General.Habilitado)
                    Index = Tools.Ordem[i].Index;

        return Index;
    }

    public static void Chat_Digitar()
    {
        byte Index = EncontrarIndex("Chat");

        // Somente se necessário
        if (!Player.EstáJogando(Player.MyIndex)) return;

        // Altera a visiblidade da caixa
        Panels.Encontrar("Chat").General.Visível = !Panels.Encontrar("Chat").General.Visível;

        // Altera o foco do digitalizador
        if (Panels.Encontrar("Chat").General.Visível)
        {
            Tools.Linhas_Visível = true;
            Foco = Index;
            return;
        }
        else
            Foco = 0;

        // Data
        string Mensagem = List[Index].Texto;
        string Player_Name = Player.Eu.Name;

        // Somente se necessário
        if (Mensagem.Length < 3)
        {
            List[Index].Texto = string.Empty;
            return;
        }

        // Separa as mensagens em partes
        string[] Partes = Mensagem.Split(' ');

        // Global
        if (Mensagem.Substring(0, 1) == "'")
            Sending.Mensagem(Mensagem.Substring(1), Game.Mensagens.Global);
        // Particular
        else if (Mensagem.Substring(0, 1) == "!")
        {
            // Previni erros 
            if (Partes.GetUpperBound(0) < 1)
                Tools.Adicionar("Use: '!' + Destination + 'Message'", SFML.Graphics.Color.White);
            else
            {
                // Data
                string Destinatário = Mensagem.Substring(1, Partes[0].Length - 1);
                Mensagem = Mensagem.Substring(Partes[0].Length + 1);

                // Envia a mensagem
                Sending.Mensagem(Mensagem, Game.Mensagens.Particular, Destinatário);
            }
        }
        // Map
        else
            Sending.Mensagem(Mensagem, Game.Mensagens.Map);

        // Limpa a caixa de texto
        List[Index].Texto = string.Empty;
    }

    public class Eventos
    {
        public static void MouseUp(MouseEventArgs e, byte Index)
        {
            // Somente se necessário
            if (!List[Index].General.Habilitado) return;
            if (!Tools.EstáSobrepondo(new Rectangle(List[Index].General.Posição, new Size(List[Index].Largura, Gráficos.TTamanho(Gráficos.Tex_Digitalizador).Height)))) return;

            // Define o foco no Digitalizador
            Foco = Index;
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

                // Não adicionar se já estiver no Maximo de caracteres
                if (List[Foco].Max_Carácteres > 0)
                    if (Texto.Length >= List[Foco].Max_Carácteres)
                        return;
            }

            // Adiciona apenas os caractres válidos ao digitalizador
            if (e.KeyChar >= 32 && e.KeyChar <= 126) List[Foco].Texto += e.KeyChar.ToString();
        }
    }
}