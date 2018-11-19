using System.Windows.Forms;

public partial class Window : Form
{
    // Usado para acessar os Data da Window
    public static Window Objects = new Window();

    public Window()
    {
        InitializeComponent();
    }

    private void Window_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Fecha o Game
        if (Tools.CurrentWindow == Tools.Windows.Game)
        {
            e.Cancel = true;
            Game.Leave();
        }
        else
           Program.Functional = false;
    }

    private void Window_MouseDown(object sender, MouseEventArgs e)
    {
        // Executa o evento de acordo a sobrePosition do ponteiro
        for (byte i = 0; i <= Tools.Ordem.GetUpperBound(0); i++)
            switch (Tools.Ordem[i].Type)
            {
                case Tools.Types.Button: Buttons.Events.MouseDown(e, Tools.Ordem[i].Index); break;
            }

        // Events em Game
        if (Tools.CurrentWindow == Tools.Windows.Game)
        {
            Tools.Inventory_MouseDown(e);
            Tools.Equipment_MouseDown(e);
            Tools.Hotbar_MouseDown(e);
        }
    }

    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        // Define a Position do mouse à váriavel
        Tools.Ponteiro.X = e.X;
        Tools.Ponteiro.Y = e.Y;

        // Executa o evento de acordo a sobrePosition do ponteiro
        for (byte i = 0; i <= Tools.Ordem.GetUpperBound(0); i++)
            switch (Tools.Ordem[i].Type)
            {
                case Tools.Types.Button: Buttons.Events.MouseMove(e, Tools.Ordem[i].Index); break;
            }
    }

    private void Window_MouseUp(object sender, MouseEventArgs e)
    {
        // Executa o evento de acordo a sobrePosition do ponteiro
        for (byte i = 0; i <= Tools.Ordem.GetUpperBound(0); i++)
            switch (Tools.Ordem[i].Type)
            {
                case Tools.Types.Button: Buttons.Events.MouseUp(e, Tools.Ordem[i].Index); break;
                case Tools.Types.Marker: Markers.Events.MouseUp(e, Tools.Ordem[i].Index); break;
                case Tools.Types.Scanner: Scanners.Events.MouseUp(e, Tools.Ordem[i].Index); break;
            }

        // Events em Game
        if (Tools.CurrentWindow == Tools.Windows.Game)
        {
            // Muda o slot do item
            if (Player.Inventory_Moving > 0)
                if (Tools.Inventory_Overlapping() > 0)
                    Sending.Inventory_Change(Player.Inventory_Moving, Tools.Inventory_Overlapping());

            // Muda o slot da hotbar
            if (Tools.Hotbar_Overlapping() > 0)
            {
                if (Player.Hotbar_Moving > 0) Sending.Hotbar_Change(Player.Hotbar_Moving, Tools.Hotbar_Overlapping());
                if (Player.Inventory_Moving > 0) Sending.Hotbar_Add(Tools.Hotbar_Overlapping(), (byte)Game.Hotbar.Item, Player.Inventory_Moving);
            }

            // Reseta a Movement
            Player.Inventory_Moving = 0;
            Player.Hotbar_Moving = 0;
        }
    }

    private void Window_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Executa os Events
        Scanners.Events.KeyPress(e);
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        // Define se um Button está sendo HoldKey
        switch (e.KeyCode)
        {
            case Keys.Up: Game.HoldKey_Above = true; break;
            case Keys.Down: Game.HoldKey_Below = true; break;
            case Keys.Left: Game.HoldKey_Left = true; break;
            case Keys.Right: Game.HoldKey_Right = true; break;
            case Keys.ShiftKey: Game.HoldKey_Shift = true; break;
            case Keys.ControlKey: Game.HoldKey_Control = true; break;
            case Keys.Enter: Scanners.Chat_Digitar(); break;
        }

        // Em Game
        if (Tools.CurrentWindow == Tools.Windows.Game)
            if (!Panels.Locate("Chat").General.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space: Player.CollectItem(); break;
                    case Keys.D1: Sending.Hotbar_Use(1); break;
                    case Keys.D2: Sending.Hotbar_Use(2); break;
                    case Keys.D3: Sending.Hotbar_Use(3); break;
                    case Keys.D4: Sending.Hotbar_Use(4); break;
                    case Keys.D5: Sending.Hotbar_Use(5); break;
                    case Keys.D6: Sending.Hotbar_Use(6); break;
                    case Keys.D7: Sending.Hotbar_Use(7); break;
                    case Keys.D8: Sending.Hotbar_Use(8); break;
                    case Keys.D9: Sending.Hotbar_Use(9); break;
                    case Keys.D0: Sending.Hotbar_Use(0); break;
                }
            }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        // Define se um Button está sendo HoldKey
        switch (e.KeyCode)
        {
            case Keys.Up: Game.HoldKey_Above = false; break;
            case Keys.Down: Game.HoldKey_Below = false; break;
            case Keys.Left: Game.HoldKey_Left = false; break;
            case Keys.Right: Game.HoldKey_Right = false; break;
            case Keys.ShiftKey: Game.HoldKey_Shift = false; break;
            case Keys.ControlKey: Game.HoldKey_Control = false; break;
        }
    }

    private void Window_Paint(object sender, PaintEventArgs e)
    {
        // Atualiza a Window
        Graphics.Apresentar();
    }

    private void Window_DoubleClick(object sender, System.EventArgs e)
    {
        // Events em Game
        if (Tools.CurrentWindow == Tools.Windows.Game)
        {
            // Use item
            byte Slot = Tools.Inventory_Overlapping();
            if (Slot > 0)
                if (Player.Inventory[Slot].Item_Num > 0)
                    Sending.Inventory_Use(Slot);

            // Use o que estiver na hotbar
            Slot = Tools.Hotbar_Overlapping();
            if (Slot > 0)
                if (Player.Hotbar[Slot].Slot > 0)
                    Sending.Hotbar_Use(Slot);
        }
    }
}
