using System.Drawing;
using System.Windows.Forms;

public class Buttons
{
    // Aramazenamento de Data da ferramenta
    public static Structure[] List = new Structure[1];

    // Estrutura das ferramentas
    public class Structure
    {
        public byte Texture;
        public Estados Estado;
        public Tools.General Geral;
    }

    // Button states
    public enum Estados
    {
        Normal,
        Clique,
        Sobrepor,
    }

    public static byte EncontrarIndex(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return i;

        return 0;
    }

    public static Structure Encontrar(string Name)
    {
        // Lista os Names das ferramentas
        for (byte i = 1; i <= List.GetUpperBound(0); i++)
            if (List[i].Geral.Name == Name)
                return List[i];

        return null;
    }

    public class Events
    {
        public static void MouseUp(MouseEventArgs e, byte Index)
        {
            SFML.Graphics.Texture Texture = Gráficos.Tex_Botão[List[Index].Texture];

            // Somente se necessário
            if (!List[Index].Geral.Habilitado) return;
            if (!Tools.EstáSobrepondo(new Rectangle(List[Index].Geral.Posição, Gráficos.TTamanho(Texture)))) return;

            // Altera o estado do botão
            Áudio.Som.Reproduzir(Áudio.Sons.Clique);
            List[Index].Estado = Estados.Sobrepor;

            // Executa o evento
            Executar(List[Index].Geral.Name);
        }

        public static void MouseDown(MouseEventArgs e, byte Index)
        {
            SFML.Graphics.Texture Texture = Gráficos.Tex_Botão[List[Index].Texture];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!List[Index].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não executar o evento
            if (!Tools.EstáSobrepondo(new Rectangle(List[Index].Geral.Posição, Gráficos.TTamanho(Texture))))
                return;

            // Altera o estado do botão
            List[Index].Estado = Estados.Clique;
        }

        public static void MouseMove(MouseEventArgs e, byte i)
        {
            SFML.Graphics.Texture Texture = Gráficos.Tex_Botão[List[i].Texture];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!List[i].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não executar o evento
            if (!Tools.EstáSobrepondo(new Rectangle(List[i].Geral.Posição, Gráficos.TTamanho(Texture))))
            {
                List[i].Estado = Estados.Normal;
                return;
            }

            // Se o botão já estiver no estado normal, isso não é necessário
            if (List[i].Estado != Estados.Normal)
                return;

            // Altera o estado do botão
            List[i].Estado = Estados.Sobrepor;
            Áudio.Som.Reproduzir(Áudio.Sons.Sobrepor);
        }

        public static void Executar(string Name)
        {
            // Executa o evento do botão
            switch (Name)
            {
                case "Conectar": Conectar(); break;
                case "Registrar": Registrar(); break;
                case "Opções": Opções(); break;
                case "Opções_Retornar": Menu_Retornar(); break;
                case "Conectar_Pronto": Conectar_Pronto(); break;
                case "Registrar_Pronto": Registrar_Pronto(); break;
                case "CriarCharacter": CriarCharacter(); break;
                case "CriarCharacter_TrocarDireita": CriarCharacter_TrocarDireita(); break;
                case "CriarCharacter_TrocarEsquerda": CriarCharacter_TrocarEsquerda(); break;
                case "CriarCharacter_Retornar": CriarCharacter_Retornar(); break;
                case "Character_Usar": Character_Usar(); break;
                case "Character_Criar": Character_Criar(); break;
                case "Character_Deletar": Character_Deletar(); break;
                case "Character_TrocarDireita": Character_TrocarDireita(); break;
                case "Character_TrocarEsquerda": Character_TrocarEsquerda(); break;
                case "Chat_Subir": Chat_Subir(); break;
                case "Chat_Descer": Chat_Descer(); break;
                case "Menu_Character": Menu_Character(); break;
                case "Atributos_Força": Atributos_Força(); break;
                case "Atributos_Resistência": Atributos_Resistência(); break;
                case "Atributos_Inteligência": Atributos_Inteligência(); break;
                case "Atributos_Agilidade": Atributos_Agilidade(); break;
                case "Atributos_Vitalidade": Atributos_Vitalidade(); break;
                case "Menu_Inventory": Menu_Inventory(); break;
            }
        }

        public static void Mudar_Personagens_Botões()
        {
            bool Visibilidade = false;

            // Verifica apenas se o painel for visível
            if (!Panels.Encontrar("SelecionarCharacter").General.Visível)
                return;

            if (Lists.Personagens[Game.SelecionarCharacter].Classe != 0)
                Visibilidade = true;

            // Altera os botões visíveis
            Encontrar("Character_Criar").Geral.Visível = !Visibilidade;
            Encontrar("Character_Deletar").Geral.Visível = Visibilidade;
            Encontrar("Character_Usar").Geral.Visível = Visibilidade;
        }

        public static void Conectar()
        {
            // Termina a conexão
            Network.Desconectar();

            // Abre o painel
            Panels.Menu_Fechar();
            Panels.Encontrar("Conectar").General.Visível = true;
        }

        public static void Registrar()
        {
            // Termina a conexão
            Network.Desconectar();

            // Abre o painel
            Panels.Menu_Fechar();
            Panels.Encontrar("Registrar").General.Visível = true;
        }

        public static void Opções()
        {
            // Termina a conexão
            Network.Desconectar();

            // Abre o painel
            Panels.Menu_Fechar();
            Panels.Encontrar("Opções").General.Visível = true;
        }

        public static void Menu_Retornar()
        {
            // Termina a conexão
            Network.Desconectar();

            // Abre o painel
            Panels.Menu_Fechar();
            Panels.Encontrar("Conectar").General.Visível = true;
        }

        public static void Conectar_Pronto()
        {
            // Saves the user name
            Lists.Opções.Usuário = Scanners.Encontrar("Conectar_Usuário").Texto;
            Escrever.Opções();

            // Connect with Game
            Game.DefinirSituação(Game.Situações.Conectar);
        }

        public static void Registrar_Pronto()
        {
            // Regras de segurança
            if (Scanners.Encontrar("Registrar_Senha").Texto != Scanners.Encontrar("Registrar_RepetirSenha").Texto)
            {
                MessageBox.Show("The passwords you entered are not the same.");
                return;
            }

            // Registra o Player, se estiver tudo certo
            Game.DefinirSituação(Game.Situações.Registrar);
        }

        public static void CriarCharacter()
        {
            // Abre a criação de Character
            Game.DefinirSituação(Game.Situações.CriarCharacter);
        }

        public static void CriarCharacter_TrocarDireita()
        {
            // Altera a classe selecionada pelo Player
            if (Game.CriarCharacter_Classe == Lists.Classe.GetUpperBound(0))
                Game.CriarCharacter_Classe = 1;
            else
                Game.CriarCharacter_Classe += 1;
        }

        public static void CriarCharacter_TrocarEsquerda()
        {
            // Altera a classe selecionada pelo Player
            if (Game.CriarCharacter_Classe == 1)
                Game.CriarCharacter_Classe = (byte)Lists.Classe.GetUpperBound(0);
            else
                Game.CriarCharacter_Classe -= 1;
        }

        public static void CriarCharacter_Retornar()
        {
            // Abre o painel de personagens
            Panels.Menu_Fechar();
            Panels.Encontrar("SelecionarCharacter").General.Visível = true;
        }

        public static void Character_Usar()
        {
            // Usa o Character selecionado
            Sending.Character_Usar();
        }

        public static void Character_Deletar()
        {
            // Deleta o Character selecionado
            Sending.Character_Deletar();
        }

        public static void Character_Criar()
        {
            // Abre a criação de Character
            Sending.Character_Criar();
        }

        public static void Character_TrocarDireita()
        {
            // Altera o Character selecionado pelo Player
            if (Game.SelecionarCharacter == Lists.Servidor_Data.Max_Personagens)
                Game.SelecionarCharacter = 1;
            else
                Game.SelecionarCharacter += 1;
        }

        public static void Character_TrocarEsquerda()
        {
            // Altera o Character selecionado pelo Player
            if (Game.SelecionarCharacter == 1)
                Game.SelecionarCharacter = Lists.Servidor_Data.Max_Personagens;
            else
                Game.SelecionarCharacter -= 1;
        }

        public static void Chat_Subir()
        {
            // Sobe as linhas do chat
            if (Tools.Linha > 0)
                Tools.Linha -= 1;
        }

        public static void Chat_Descer()
        {
            // Sobe as linhas do chat
            if (Tools.Chat.Count - 1 - Tools.Linha - Tools.Linhas_Visíveis > 0)
                Tools.Linha += 1;
        }

        public static void Menu_Character()
        {
            // Altera a visibilidade do painel e fecha os outros
            Panels.Encontrar("Menu_Character").General.Visível = !Panels.Encontrar("Menu_Character").General.Visível;
            Panels.Encontrar("Menu_Inventory").General.Visível = false;
        }

        public static void Atributos_Força()
        {
            Sending.AdicionarPonto(Game.Atributos.Força);
        }

        public static void Atributos_Resistência()
        {
            Sending.AdicionarPonto(Game.Atributos.Resistência);
        }

        public static void Atributos_Inteligência()
        {
            Sending.AdicionarPonto(Game.Atributos.Inteligência);
        }

        public static void Atributos_Agilidade()
        {
            Sending.AdicionarPonto(Game.Atributos.Agilidade);
        }

        public static void Atributos_Vitalidade()
        {
            Sending.AdicionarPonto(Game.Atributos.Vitalidade);
        }

        public static void Menu_Inventory()
        {
            // Altera a visibilidade do painel e fecha os outros
            Panels.Encontrar("Menu_Inventory").General.Visível = !Panels.Encontrar("Menu_Inventory").General.Visível;
            Panels.Encontrar("Menu_Character").General.Visível = false;
        }
    }
}