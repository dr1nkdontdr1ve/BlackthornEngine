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

    // Structure da ferramenta
    public class Structure
    {
        public string Text;
        public short Max_Carácteres;
        public short Width;
        public bool Senha;
        public Tools.Geral Geral;
    }

    public static byte LocateIndex(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return i;

        return 0;
    }

    public static Structure Locate(string Name)
    {
        // List os Names das Tools
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return List[i];

        return null;
    }

    public static void Focalizar()
    {
        // Se o Scanner não estiver habilitado então isso não é necessário 
        if (List[Foco] != null && List[Foco].Geral.Habilitado) return;

        // Altera o Scanner focado para o mais Next
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
        {
            if (Tools.Ordem[i].Type != Tools.Types.Scanner)
                continue;
            else if (!List[Tools.Ordem[i].Index].Geral.Habilitado)
                continue;
            else if (i == LocateIndex("Chat"))

                Foco = Tools.Ordem[i].Index;
            return;
        }
    }

    public static void ExchangeFoco()
    {
        // Altera o Scanner focado para o Next
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
        {
            if (Tools.Ordem[i].Type != Tools.Types.Scanner)
                continue;
            else if (!List[Tools.Ordem[i].Index].Geral.Habilitado)
                continue;
            if (Foco != Último() && i <= Tools.Locate(Tools.Types.Scanner, Foco))
                continue;

            Foco = Tools.Ordem[i].Index;
            return;
        }
    }

    public static byte Último()
    {
        byte Index = 0;

        // Retorna o último Scanner habilitado
        for (byte i = 1; i <= Tools.Ordem.GetUpperBound(0); i++)
            if (Tools.Ordem[i].Type == Tools.Types.Scanner)
                if (List[Tools.Ordem[i].Index].Geral.Habilitado)
                    Index = Tools.Ordem[i].Index;

        return Index;
    }

    public static void Chat_Digitar()
    {
        byte Index = LocateIndex("Chat");

        // Somente se necessário
        if (!Player.IsPlaying(Player.MyIndex)) return;

        // Altera a visiblidade da caixa
        Panels.Locate("Chat").Geral.Visible = !Panels.Locate("Chat").Geral.Visible;

        // Altera o foco do Scanner
        if (Panels.Locate("Chat").Geral.Visible)
        {
            Tools.Lines_Visible = true;
            Foco = Index;
            return;
        }
        else
            Foco = 0;

        // Data
        string Message = List[Index].Text;
        string Player_Name = Player.Eu.Name;

        // Somente se necessário
        if (Message.Length < 3)
        {
            List[Index].Text = string.Empty;
            return;
        }

        // Separa as mensagens em partes
        string[] Partes = Message.Split(' ');

        // Global
        if (Message.Substring(0, 1) == "'")
            Sending.Message(Message.Substring(1), Jogo.Mensagens.Global);
        // Particular
        else if (Message.Substring(0, 1) == "!")
        {
            // Previni erros 
            if (Partes.GetUpperBound(0) < 1)
                Tools.Add("Use: '!' + Destination + 'Message'", SFML.Graphics.Color.White);
            else
            {
                // Data
                string Destinatário = Message.Substring(1, Partes[0].Length - 1);
                Message = Message.Substring(Partes[0].Length + 1);

                // Envia a Message
                Sending.Message(Message, Jogo.Mensagens.Particular, Destinatário);
            }
        }
        // Map
        else
            Sending.Message(Message, Jogo.Mensagens.Map);

        // Limpa a caixa de Text
        List[Index].Text = string.Empty;
    }

    public class Events
    {
        public static void MouseUp(MouseEventArgs e, byte Index)
        {
            // Somente se necessário
            if (!List[Index].Geral.Habilitado) return;
            if (!Tools.IsOverlapping(new Rectangle(List[Index].Geral.Position, new Size(List[Index].Width, Graphics.MySize(Graphics.Tex_Scanner).Height)))) return;

            // Define o foco no Scanner
            Foco = Index;
        }

        public static void KeyPress(KeyPressEventArgs e)
        {
            // Se não tiver nenhum focado então sair
            if (Foco == 0) return;

            // Altera o foco do Scanner para o Next
            if (e.KeyChar == (char)Keys.Tab)
            {
                ExchangeFoco();
                return;
            }

            // Text
            string Text = List[Foco].Text;

            // Apaga a última letra do Text
            if (!string.IsNullOrEmpty(Text))
            {
                if (e.KeyChar == '\b' && Text.Length > 0)
                {
                    List[Foco].Text = Text.Remove(Text.Length - 1);
                    return;
                }

                // Não Add se já estiver no Maximo de caracteres
                if (List[Foco].Max_Carácteres > 0)
                    if (Text.Length >= List[Foco].Max_Carácteres)
                        return;
            }

            // Adiciona apenas os caractres válidos ao Scanner
            if (e.KeyChar >= 32 && e.KeyChar <= 126) List[Foco].Text += e.KeyChar.ToString();
        }
    }
}