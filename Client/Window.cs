using System.Windows.Forms;

public partial class Window : Form
{
    // Usado para acessar os Data da janela
    public static Window Objects = new Window();

    public Window()
    {
        InitializeComponent();
    }

    private void Janela_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Fecha o Game
        if (Ferramentas.JanelaAtual == Ferramentas.Janelas.Game)
        {
            e.Cancel = true;
            Game.Sair();
        }
        else
            Aplicação.Funcionado = false;
    }

    private void Janela_MouseDown(object sender, MouseEventArgs e)
    {
        // Executa o evento de acordo a sobreposição do ponteiro
        for (byte i = 0; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
            switch (Ferramentas.Ordem[i].Tipo)
            {
                case Ferramentas.Tipos.Botão: Botões.Eventos.MouseDown(e, Ferramentas.Ordem[i].Índice); break;
            }

        // Eventos em Game
        if (Ferramentas.JanelaAtual == Ferramentas.Janelas.Game)
        {
            Ferramentas.Inventário_MouseDown(e);
            Ferramentas.Equipamento_MouseDown(e);
            Ferramentas.Hotbar_MouseDown(e);
        }
    }

    private void Janela_MouseMove(object sender, MouseEventArgs e)
    {
        // Define a Posição do mouse à váriavel
        Ferramentas.Ponteiro.X = e.X;
        Ferramentas.Ponteiro.Y = e.Y;

        // Executa o evento de acordo a sobreposição do ponteiro
        for (byte i = 0; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
            switch (Ferramentas.Ordem[i].Tipo)
            {
                case Ferramentas.Tipos.Botão: Botões.Eventos.MouseMove(e, Ferramentas.Ordem[i].Índice); break;
            }
    }

    private void Janela_MouseUp(object sender, MouseEventArgs e)
    {
        // Executa o evento de acordo a sobreposição do ponteiro
        for (byte i = 0; i <= Ferramentas.Ordem.GetUpperBound(0); i++)
            switch (Ferramentas.Ordem[i].Tipo)
            {
                case Ferramentas.Tipos.Botão: Botões.Eventos.MouseUp(e, Ferramentas.Ordem[i].Índice); break;
                case Ferramentas.Tipos.Marcador: Marcadores.Eventos.MouseUp(e, Ferramentas.Ordem[i].Índice); break;
                case Ferramentas.Tipos.Digitalizador: Digitalizadores.Eventos.MouseUp(e, Ferramentas.Ordem[i].Índice); break;
            }

        // Eventos em Game
        if (Ferramentas.JanelaAtual == Ferramentas.Janelas.Game)
        {
            // Muda o slot do item
            if (Jogador.Inventário_Movendo > 0)
                if (Ferramentas.Inventário_Sobrepondo() > 0)
                    Enviar.Inventário_Mudar(Jogador.Inventário_Movendo, Ferramentas.Inventário_Sobrepondo());

            // Muda o slot da hotbar
            if (Ferramentas.Hotbar_Sobrepondo() > 0)
            {
                if (Jogador.Hotbar_Movendo > 0) Enviar.Hotbar_Mudar(Jogador.Hotbar_Movendo, Ferramentas.Hotbar_Sobrepondo());
                if (Jogador.Inventário_Movendo > 0) Enviar.Hotbar_Adicionar(Ferramentas.Hotbar_Sobrepondo(), (byte)Game.Hotbar.Item, Jogador.Inventário_Movendo);
            }

            // Reseta a movimentação
            Jogador.Inventário_Movendo = 0;
            Jogador.Hotbar_Movendo = 0;
        }
    }

    private void Janela_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Executa os eventos
        Digitalizadores.Eventos.KeyPress(e);
    }

    private void Janela_KeyDown(object sender, KeyEventArgs e)
    {
        // Define se um botão está sendo pressionado
        switch (e.KeyCode)
        {
            case Keys.Up: Game.Pressionado_Acima = true; break;
            case Keys.Down: Game.Pressionado_Abaixo = true; break;
            case Keys.Left: Game.Pressionado_Esquerda = true; break;
            case Keys.Right: Game.Pressionado_Direita = true; break;
            case Keys.ShiftKey: Game.Pressionado_Shift = true; break;
            case Keys.ControlKey: Game.Pressionado_Control = true; break;
            case Keys.Enter: Digitalizadores.Chat_Digitar(); break;
        }

        // Em Game
        if (Ferramentas.JanelaAtual == Ferramentas.Janelas.Game)
            if (!Paineis.Encontrar("Chat").Geral.Visível)
            {
                switch (e.KeyCode)
                {
                    case Keys.Space: Jogador.ColetarItem(); break;
                    case Keys.D1: Enviar.Hotbar_Usar(1); break;
                    case Keys.D2: Enviar.Hotbar_Usar(2); break;
                    case Keys.D3: Enviar.Hotbar_Usar(3); break;
                    case Keys.D4: Enviar.Hotbar_Usar(4); break;
                    case Keys.D5: Enviar.Hotbar_Usar(5); break;
                    case Keys.D6: Enviar.Hotbar_Usar(6); break;
                    case Keys.D7: Enviar.Hotbar_Usar(7); break;
                    case Keys.D8: Enviar.Hotbar_Usar(8); break;
                    case Keys.D9: Enviar.Hotbar_Usar(9); break;
                    case Keys.D0: Enviar.Hotbar_Usar(0); break;
                }
            }
    }

    private void Janela_KeyUp(object sender, KeyEventArgs e)
    {
        // Define se um botão está sendo pressionado
        switch (e.KeyCode)
        {
            case Keys.Up: Game.Pressionado_Acima = false; break;
            case Keys.Down: Game.Pressionado_Abaixo = false; break;
            case Keys.Left: Game.Pressionado_Esquerda = false; break;
            case Keys.Right: Game.Pressionado_Direita = false; break;
            case Keys.ShiftKey: Game.Pressionado_Shift = false; break;
            case Keys.ControlKey: Game.Pressionado_Control = false; break;
        }
    }

    private void Janela_Paint(object sender, PaintEventArgs e)
    {
        // Atualiza a janela
        Gráficos.Apresentar();
    }

    private void Janela_DoubleClick(object sender, System.EventArgs e)
    {
        // Eventos em Game
        if (Ferramentas.JanelaAtual == Ferramentas.Janelas.Game)
        {
            // Usar item
            byte Slot = Ferramentas.Inventário_Sobrepondo();
            if (Slot > 0)
                if (Jogador.Inventário[Slot].Item_Num > 0)
                    Enviar.Inventário_Usar(Slot);

            // Usar o que estiver na hotbar
            Slot = Ferramentas.Hotbar_Sobrepondo();
            if (Slot > 0)
                if (Jogador.Hotbar[Slot].Slot > 0)
                    Enviar.Hotbar_Usar(Slot);
        }
    }
}
