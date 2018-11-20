using System.Drawing;
using System.Windows.Forms;

public class Buttons
{
    // Aramazenamento de Data da ferramenta
    public static Structure[] List = new Structure[1];

    // Tools Structure
    public class Structure
    {
        public byte Texture;
        public States State;
        public Tools.Geral Geral;
    }

    // Button states
    public enum States
    {
        Normal,
        Clique,
        Sobrepor,
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

    public class Events
    {
        public static void MouseUp(MouseEventArgs e, byte Index)
        {
            SFML.Graphics.Texture Texture = Graphics.Tex_Button[List[Index].Texture];

            // Somente se necessário
            if (!List[Index].Geral.Habilitado) return;
            if (!Tools.IsOverlapping(new Rectangle(List[Index].Geral.Position, Graphics.MySize(Texture)))) return;

            // Altera o State do Button
            Audio.Som.Reproduce(Audio.Sons.Click);
            List[Index].State = States.Sobrepor;

            // Executa o evento
            Run(List[Index].Geral.Name);
        }

        public static void MouseDown(MouseEventArgs e, byte Index)
        {
            SFML.Graphics.Texture Texture = Graphics.Tex_Button[List[Index].Texture];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!List[Index].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não Run o evento
            if (!Tools.IsOverlapping(new Rectangle(List[Index].Geral.Position, Graphics.MySize(Texture))))
                return;

            // Altera o State do Button
            List[Index].State = States.Clique;
        }

        public static void MouseMove(MouseEventArgs e, byte i)
        {
            SFML.Graphics.Texture Texture = Graphics.Tex_Button[List[i].Texture];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!List[i].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não Run o evento
            if (!Tools.IsOverlapping(new Rectangle(List[i].Geral.Position, Graphics.MySize(Texture))))
            {
                List[i].State = States.Normal;
                return;
            }

            // Se o Button já estiver no State normal, isso não é necessário
            if (List[i].State != States.Normal)
                return;

            // Altera o State do Button
            List[i].State = States.Sobrepor;
            Audio.Som.Reproduce(Audio.Sons.Overlap);
        }

        public static void Run(string Name)
        {
            // Run the Button event
            switch (Name)
            {
                case "Connect": Connect(); break;
                case "Register": Register(); break;
                case "Options": Options(); break;
                case "Options_Retornar": Menu_Retornar(); break;
                case "Connect_Ready": Connect_Ready(); break;
                case "Register_Ready": Register_Ready(); break;
                case "CreateCharacter": CreateCharacter(); break;
                case "CreateCharacter_ExchangeRight": CreateCharacter_ExchangeRight(); break;
                case "CreateCharacter_ExchangeLeft": CreateCharacter_ExchangeLeft(); break;
                case "CreateCharacter_Retornar": CreateCharacter_Retornar(); break;
                case "Character_Use": Character_Use(); break;
                case "Character_Create": Character_Create(); break;
                case "Character_Delete": Character_Delete(); break;
                case "Character_ExchangeRight": Character_ExchangeRight(); break;
                case "Character_ExchangeLeft": Character_ExchangeLeft(); break;
                case "Chat_Up": Chat_Up(); break;
                case "Chat_Down": Chat_Down(); break;
                case "Menu_Character": Menu_Character(); break;
                case "Attributes_Force": Attributes_Force(); break;
                case "Attributes_Resistence": Attributes_Resistence(); break;
                case "Attributes_Intelligence": Attributes_Intelligence(); break;
                case "Attributes_Agility": Attributes_Agility(); break;
                case "Attributes_Vitality": Attributes_Vitality(); break;
                case "Menu_Inventory": Menu_Inventory(); break;
            }
        }

        public static void Change_Characters_Buttons()
        {
            bool Visibilidade = false;

            // Verifica apenas se o Panel for Visible
            if (!Panels.Locate("SelectCharacter").Geral.Visible)
                return;

            if (Lists.Characters[Jogo.SelectCharacter].Classe != 0)
                Visibilidade = true;

            // Altera os Buttons visíveis
            Locate("Character_Create").Geral.Visible = !Visibilidade;
            Locate("Character_Delete").Geral.Visible = Visibilidade;
            Locate("Character_Use").Geral.Visible = Visibilidade;
        }

        public static void Connect()
        {
            // Termina a conexão
            Network.Disconnect();

            // Abre o Panel
            Panels.Menu_Close();
            Panels.Locate("Connect").Geral.Visible = true;
        }

        public static void Register()
        {
            // Termina a conexão
            Network.Disconnect();

            // Abre o Panel
            Panels.Menu_Close();
            Panels.Locate("Register").Geral.Visible = true;
        }

        public static void Options()
        {
            // Termina a conexão
            Network.Disconnect();

            // Abre o Panel
            Panels.Menu_Close();
            Panels.Locate("Options").Geral.Visible = true;
        }

        public static void Menu_Retornar()
        {
            // Termina a conexão
            Network.Disconnect();

            // Abre o Panel
            Panels.Menu_Close();
            Panels.Locate("Connect").Geral.Visible = true;
        }

        public static void Connect_Ready()
        {
            // Saves the user name
            Lists.Options.User = Scanners.Locate("Conectar_Usuário").Text;
            Write.Options();

            // Connect with Jogo
            Jogo.SetLocation(Jogo.Situations.Connect);
        }

        public static void Register_Ready()
        {
            // Regras de segurança
            if (Scanners.Locate("Register_Senha").Text != Scanners.Locate("Register_RepetirSenha").Text)
            {
                MessageBox.Show("The passwords you entered are not the same.");
                return;
            }

            // Registra o Player, se estiver All certo
            Jogo.SetLocation(Jogo.Situations.Register);
        }

        public static void CreateCharacter()
        {
            // Abre a criação de Character
            Jogo.SetLocation(Jogo.Situations.CreateCharacter);
        }

        public static void CreateCharacter_ExchangeRight()
        {
            // Altera a classe selecionada pelo Player
            if (Jogo.CreateCharacter_Classe == Lists.Classe.GetUpperBound(0))
                Jogo.CreateCharacter_Classe = 1;
            else
                Jogo.CreateCharacter_Classe += 1;
        }

        public static void CreateCharacter_ExchangeLeft()
        {
            // Altera a classe selecionada pelo Player
            if (Jogo.CreateCharacter_Classe == 1)
                Jogo.CreateCharacter_Classe = (byte)Lists.Classe.GetUpperBound(0);
            else
                Jogo.CreateCharacter_Classe -= 1;
        }

        public static void CreateCharacter_Retornar()
        {
            // Abre o Panel de Characters
            Panels.Menu_Close();
            Panels.Locate("SelectCharacter").Geral.Visible = true;
        }

        public static void Character_Use()
        {
            // Usa o Character selecionado
            Sending.Character_Use();
        }

        public static void Character_Delete()
        {
            // Deleta o Character selecionado
            Sending.Character_Delete();
        }

        public static void Character_Create()
        {
            // Abre a criação de Character
            Sending.Character_Create();
        }

        public static void Character_ExchangeRight()
        {
            // Altera o Character selecionado pelo Player
            if (Jogo.SelectCharacter == Lists.Server_Data.Max_Characters)
                Jogo.SelectCharacter = 1;
            else
                Jogo.SelectCharacter += 1;
        }

        public static void Character_ExchangeLeft()
        {
            // Altera o Character selecionado pelo Player
            if (Jogo.SelectCharacter == 1)
                Jogo.SelectCharacter = Lists.Server_Data.Max_Characters;
            else
                Jogo.SelectCharacter -= 1;
        }

        public static void Chat_Up()
        {
            // Sobe as Lines do chat
            if (Tools.Line > 0)
                Tools.Line -= 1;
        }

        public static void Chat_Down()
        {
            // Sobe as Lines do chat
            if (Tools.Chat.Count - 1 - Tools.Line - Tools.Lines_Visíveis > 0)
                Tools.Line += 1;
        }

        public static void Menu_Character()
        {
            // Altera a visibilidade do Panel e fecha os outros
            Panels.Locate("Menu_Character").Geral.Visible = !Panels.Locate("Menu_Character").Geral.Visible;
            Panels.Locate("Menu_Inventory").Geral.Visible = false;
        }

        public static void Attributes_Force()
        {
            Sending.AddPoints(Jogo.Attributes.Force);
        }

        public static void Attributes_Resistence()
        {
            Sending.AddPoints(Jogo.Attributes.Resistence);
        }

        public static void Attributes_Intelligence()
        {
            Sending.AddPoints(Jogo.Attributes.Intelligence);
        }

        public static void Attributes_Agility()
        {
            Sending.AddPoints(Jogo.Attributes.Agility);
        }

        public static void Attributes_Vitality()
        {
            Sending.AddPoints(Jogo.Attributes.Vitality);
        }

        public static void Menu_Inventory()
        {
            // Altera a visibilidade do Panel e fecha os outros
            Panels.Locate("Menu_Inventory").Geral.Visible = !Panels.Locate("Menu_Inventory").Geral.Visible;
            Panels.Locate("Menu_Character").Geral.Visible = false;
        }
    }
}