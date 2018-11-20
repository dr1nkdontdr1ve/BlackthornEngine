using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class Tools
{
    // Enabling Tools
    public static bool Habilitação;

    // Position of the mouse pointer
    public static Point Ponteiro;

    // Window that is focused
    public static Windows CurrentWindow;

    // Chat
    public static bool Lines_Visible;
    public const byte Lines_Visíveis = 9;
    public const byte Max_Lines = 50;
    public static byte Line;

    // Tools Rendering Order
    public static Identification[] Ordem = new Identification[0];
    public static List<Chat_Structure> Chat = new List<Chat_Structure>();

    public struct Identification
    {
        public byte Index;
        public Types Type;
    }

    public class Geral
    {
        public string Name;
        public bool Visible;
        public Point Position;
        public bool Habilitado;

        public bool CheckHabilitation()
        {
            // Define a habilitação da ferramenta
            if (!Visible || !Habilitação)
                return Habilitado = false;
            else
                return Habilitado = true;
        }
    }

    public class Chat_Structure
    {
        public string Text;
        public SFML.Graphics.Color Cor;
    }

    // Identificação das Windows do Jogo
    public enum Windows
    {
        Nenhuma,
        Menu,
        Jogo
    }

    // Types de Tools
    public enum Types
    {
        Button,
        Panel,
        Marker,
        Scanner
    }

    public static void DefineHabilitation(string Panel, Windows Window)
    {
        // Define a habilitação
        if (CurrentWindow != Window || Panel != string.Empty && !Panels.Locate(Panel).Geral.Visible)
            Habilitação = false;
        else
            Habilitação = true;
    }

    public static void Listr(Types Type, byte Index)
    {
        int Amount = Ordem.GetUpperBound(0) + 1;

        // Se já estiver Listdo não é necessário Listr de novo
        if (IsListed(Type, Index))
            return;

        // Altera o Size da caixa
        Array.Resize(ref Ordem, Amount + 1);

        // Adiciona à List
        Ordem[Amount].Type = Type;
        Ordem[Amount].Index = Index;
    }

    private static bool IsListed(Types Type, byte Index)
    {
        // Checks whether the tool is already listed
        for (short i = 1; i <= Ordem.GetUpperBound(0); i++)
            if (Ordem[i].Type == Type && Ordem[i].Index == Index)
                return true;

        return false;
    }

    public static bool IsOverlapping(Rectangle Retângulo)
    {
        // Verficia se o mouse está sobre o objeto
        if (Ponteiro.X >= Retângulo.X && Ponteiro.X <= Retângulo.X + Retângulo.Width)
            if (Ponteiro.Y >= Retângulo.Y && Ponteiro.Y <= Retângulo.Y + Retângulo.Height)
                return true;

        // Se não, retornar um valor nulo
        return false;
    }

    public static int Locate(Types Type, byte Index)
    {
        // List os Names dos Buttons
        for (byte i = 1; i <= Ordem.GetUpperBound(0); i++)
            if (Ordem[i].Type == Type && Ordem[i].Index == Index)
                return i;

        return 0;
    }

    public static int MeasureText_Width(string Text)
    {
        // Data do Text
        SFML.Graphics.Text TempText = new SFML.Graphics.Text(Text, Graphics.Fonte);
        TempText.CharacterSize = 10;
        return (int)TempText.GetLocalBounds().Width;
    }

    public static string SmashText(string Text, int Width)
    {
        int Text_Width;

        // Previni sobrecargas
        if (string.IsNullOrEmpty(Text))
            return Text;

        // Usado para fazer alguns calculos
        Text_Width = MeasureText_Width(Text);

        // Diminui o Size do Text até que ele possa caber no Scanner
        while (Text_Width - Width >= 0)
        {
            Text = Text.Substring(1);
            Text_Width = MeasureText_Width(Text);
        }

        return Text;
    }

    public static byte EcontrarLineVazia()
    {
        // Encontra uma Line vazia
        for (byte i = 0; i <= Max_Lines; i++)
            if (Chat[i].Text == string.Empty)
                return i;

        return 0;
    }

    public static void AddLine(string Message, SFML.Graphics.Color Cor)
    {
        Chat.Add(new Chat_Structure());
        int i = Chat.Count - 1;

        // Adiciona a Message em uma Line vazia
        Chat[i].Text = Message;
        Chat[i].Cor = Cor;

        // Remove uma Line se necessário
        if (Chat.Count > Max_Lines) Chat.Remove(Chat[0]);
        if (i + Line > Lines_Visíveis + Line)
            Line = (byte)(i - Lines_Visíveis);

        // Torna as Lines visíveis
        Lines_Visible = true;
    }

    public static void Add(string Message, SFML.Graphics.Color Cor)
    {
        int Message_Width, Caixa_Width = Graphics.MySize(Graphics.Tex_Panel[Panels.Locate("Chat").Texture]).Width - 16;
        string Temp_Message; int Separação;

        // Remove os espaços
        Message = Message.Trim();
        Message_Width = MeasureText_Width(Message);

        // Caso couber, adiciona a Message normalmente
        if (Message_Width < Caixa_Width)
            AddLine(Message, Cor);
        else
        {
            for (int i = 0; i <= Message.Length; i++)
            {
                // Verifica se o Next caráctere é um separável 
                switch (Message[i])
                {
                    case '-':
                    case '_':
                    case ' ': Separação = i; break;
                }

                Temp_Message = Message.Substring(0, i);

                // Adiciona o Text à caixa
                if (MeasureText_Width(Temp_Message) > Caixa_Width)
                {
                    AddLine(Temp_Message, Cor);
                    Add(Message.Substring(Temp_Message.Length), Cor);
                    return;
                }
            }
        }
    }

    public static byte Inventory_Overlapping()
    {
        byte NumColumns = 5;
        Point Panel_Position = Panels.Locate("Menu_Inventory").Geral.Position;

        for (byte i = 1; i <= Jogo.Max_Inventory; i++)
        {
            // Position do item
            byte Line = (byte)((i - 1) / NumColumns);
            int Coluna = i - (Line * 5) - 1;
            Point Position = new Point(Panel_Position.X + 7 + Coluna * 36, Panel_Position.Y + 30 + Line * 36);

            // Retorna o slot em que o mouse está por cima
            if (IsOverlapping(new Rectangle(Position.X, Position.Y, 32, 32)))
                return i;
        }

        return 0;
    }

    public static void Inventory_MouseDown(MouseEventArgs e)
    {
        byte Slot = Inventory_Overlapping();

        // Somente se necessário
        if (Slot == 0) return;
        if (Player.Inventory[Slot].Item_Num == 0) return;

        // Solta item
        if (e.Button == MouseButtons.Right)
        {
            Sending.SoltarItem(Slot);
            return;
        }
        // Seleciona o item
        else if (e.Button == MouseButtons.Left)
        {
            Player.Inventory_Moving = Slot;
            return;
        }
    }

    public static void Equipment_MouseDown(MouseEventArgs e)
    {
        Point Panel_Position = Panels.Locate("Menu_Character").Geral.Position;

        for (byte i = 0; i <= (byte)Jogo.Equipments.Amount - 1; i++)
            if (IsOverlapping(new Rectangle(Panel_Position.X + 7 + i * 36, Panel_Position.Y + 247, 32, 32)))
                // Remove o Equipment
                if (e.Button == MouseButtons.Right)
                {
                    Sending.Equipment_Remove(i);
                    return;
                }
    }

    public static byte Hotbar_Overlapping()
    {
        Point Panel_Position = Panels.Locate("Hotbar").Geral.Position;

        for (byte i = 1; i <= Jogo.Max_Hotbar; i++)
        {
            // Position do slot
            Point Position = new Point(Panel_Position.X + 8 + (i - 1) * 36, Panel_Position.Y + 6);

            // Retorna o slot em que o mouse está por cima
            if (IsOverlapping(new Rectangle(Position.X, Position.Y, 32, 32)))
                return i;
        }

        return 0;
    }

    public static void Hotbar_MouseDown(MouseEventArgs e)
    {
        byte Slot = Hotbar_Overlapping();

        // Somente se necessário
        if (Slot == 0) return;
        if (Player.Hotbar[Slot].Slot == 0) return;

        // Solta item
        if (e.Button == MouseButtons.Right)
        {
            Sending.Hotbar_Add(Slot, 0, 0);
            return;
        }
        // Seleciona o item
        else if (e.Button == MouseButtons.Left)
        {
            Player.Hotbar_Moving = Slot;
            return;
        }
    }
}