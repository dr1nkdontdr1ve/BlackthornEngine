﻿using System.Drawing;
using System.Windows.Forms;

public class Botões
{
    // Aramazenamento de dados da ferramenta
    public static Estrutura[] Lista = new Estrutura[1];

    // Estrutura das ferramentas
    public class Estrutura
    {
        public byte Textura;
        public Estados Estado;
        public Ferramentas.Geral Geral;
    }

    // Estados dos botões
    public enum Estados
    {
        Normal,
        Clique,
        Sobrepor,
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

    public class Eventos
    {
        public static void MouseUp(MouseEventArgs e, byte Índice)
        {
            SFML.Graphics.Texture Textura = Gráficos.Tex_Botão[Lista[Índice].Textura];

            // Somente se necessário
            if (!Lista[Índice].Geral.Habilitado) return;
            if (!Ferramentas.EstáSobrepondo(new Rectangle(Lista[Índice].Geral.Posição, Gráficos.TTamanho(Textura)))) return;

            // Altera o estado do botão
            Áudio.Som.Reproduzir(Áudio.Sons.Clique);
            Lista[Índice].Estado = Estados.Sobrepor;

            // Executa o evento
            Executar(Lista[Índice].Geral.Nome);
        }

        public static void MouseDown(MouseEventArgs e, byte Índice)
        {
            SFML.Graphics.Texture Textura = Gráficos.Tex_Botão[Lista[Índice].Textura];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!Lista[Índice].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não executar o evento
            if (!Ferramentas.EstáSobrepondo(new Rectangle(Lista[Índice].Geral.Posição, Gráficos.TTamanho(Textura))))
                return;

            // Altera o estado do botão
            Lista[Índice].Estado = Estados.Clique;
        }

        public static void MouseMove(MouseEventArgs e, byte i)
        {
            SFML.Graphics.Texture Textura = Gráficos.Tex_Botão[Lista[i].Textura];

            // Somente se necessário
            if (e.Button == MouseButtons.Right) return;
            if (!Lista[i].Geral.Habilitado) return;

            // Se o mouse não estiver sobre a ferramenta, então não executar o evento
            if (!Ferramentas.EstáSobrepondo(new Rectangle(Lista[i].Geral.Posição, Gráficos.TTamanho(Textura))))
            {
                Lista[i].Estado = Estados.Normal;
                return;
            }

            // Se o botão já estiver no estado normal, isso não é necessário
            if (Lista[i].Estado != Estados.Normal)
                return;

            // Altera o estado do botão
            Lista[i].Estado = Estados.Sobrepor;
            Áudio.Som.Reproduzir(Áudio.Sons.Sobrepor);
        }

        public static void Executar(string Nome)
        {
            // Executa o evento do botão
            switch (Nome)
            {
                case "Conectar": Conectar(); break;
                case "Registrar": Registrar(); break;
                case "Opções": Opções(); break;
                case "Opções_Retornar": Menu_Retornar(); break;
                case "Conectar_Pronto": Conectar_Pronto(); break;
                case "Registrar_Pronto": Registrar_Pronto(); break;
                case "CriarPersonagem": CriarPersonagem(); break;
                case "CriarPersonagem_TrocarDireita": CriarPersonagem_TrocarDireita(); break;
                case "CriarPersonagem_TrocarEsquerda": CriarPersonagem_TrocarEsquerda(); break;
                case "CriarPersonagem_Retornar": CriarPersonagem_Retornar(); break;
                case "Personagem_Usar": Personagem_Usar(); break;
                case "Personagem_Criar": Personagem_Criar(); break;
                case "Personagem_Deletar": Personagem_Deletar(); break;
                case "Personagem_TrocarDireita": Personagem_TrocarDireita(); break;
                case "Personagem_TrocarEsquerda": Personagem_TrocarEsquerda(); break;
                case "Chat_Subir": Chat_Subir(); break;
                case "Chat_Descer": Chat_Descer(); break;
                case "Menu_Personagem": Menu_Personagem(); break;
                case "Atributos_Força": Atributos_Força(); break;
                case "Atributos_Resistência": Atributos_Resistência(); break;
                case "Atributos_Inteligência": Atributos_Inteligência(); break;
                case "Atributos_Agilidade": Atributos_Agilidade(); break;
                case "Atributos_Vitalidade": Atributos_Vitalidade(); break;
                case "Menu_Inventário": Menu_Inventário(); break;
            }
        }

        public static void Mudar_Personagens_Botões()
        {
            bool Visibilidade = false;

            // Verifica apenas se o painel for visível
            if (!Paineis.Encontrar("SelecionarPersonagem").Geral.Visível)
                return;

            if (Listas.Personagens[Game.SelecionarPersonagem].Classe != 0)
                Visibilidade = true;

            // Altera os botões visíveis
            Encontrar("Personagem_Criar").Geral.Visível = !Visibilidade;
            Encontrar("Personagem_Deletar").Geral.Visível = Visibilidade;
            Encontrar("Personagem_Usar").Geral.Visível = Visibilidade;
        }

        public static void Conectar()
        {
            // Termina a conexão
            Rede.Desconectar();

            // Abre o painel
            Paineis.Menu_Fechar();
            Paineis.Encontrar("Conectar").Geral.Visível = true;
        }

        public static void Registrar()
        {
            // Termina a conexão
            Rede.Desconectar();

            // Abre o painel
            Paineis.Menu_Fechar();
            Paineis.Encontrar("Registrar").Geral.Visível = true;
        }

        public static void Opções()
        {
            // Termina a conexão
            Rede.Desconectar();

            // Abre o painel
            Paineis.Menu_Fechar();
            Paineis.Encontrar("Opções").Geral.Visível = true;
        }

        public static void Menu_Retornar()
        {
            // Termina a conexão
            Rede.Desconectar();

            // Abre o painel
            Paineis.Menu_Fechar();
            Paineis.Encontrar("Conectar").Geral.Visível = true;
        }

        public static void Conectar_Pronto()
        {
            // Salva o nome do usuário
            Listas.Opções.Usuário = Digitalizadores.Encontrar("Conectar_Usuário").Texto;
            Escrever.Opções();

            // Conecta-se ao Game
            Game.DefinirSituação(Game.Situações.Conectar);
        }

        public static void Registrar_Pronto()
        {
            // Regras de segurança
            if (Digitalizadores.Encontrar("Registrar_Senha").Texto != Digitalizadores.Encontrar("Registrar_RepetirSenha").Texto)
            {
                MessageBox.Show("As senhas digitadas não são iquais.");
                return;
            }

            // Registra o jogador, se estiver tudo certo
            Game.DefinirSituação(Game.Situações.Registrar);
        }

        public static void CriarPersonagem()
        {
            // Abre a criação de personagem
            Game.DefinirSituação(Game.Situações.CriarPersonagem);
        }

        public static void CriarPersonagem_TrocarDireita()
        {
            // Altera a classe selecionada pelo jogador
            if (Game.CriarPersonagem_Classe == Listas.Classe.GetUpperBound(0))
                Game.CriarPersonagem_Classe = 1;
            else
                Game.CriarPersonagem_Classe += 1;
        }

        public static void CriarPersonagem_TrocarEsquerda()
        {
            // Altera a classe selecionada pelo jogador
            if (Game.CriarPersonagem_Classe == 1)
                Game.CriarPersonagem_Classe = (byte)Listas.Classe.GetUpperBound(0);
            else
                Game.CriarPersonagem_Classe -= 1;
        }

        public static void CriarPersonagem_Retornar()
        {
            // Abre o painel de personagens
            Paineis.Menu_Fechar();
            Paineis.Encontrar("SelecionarPersonagem").Geral.Visível = true;
        }

        public static void Personagem_Usar()
        {
            // Usa o personagem selecionado
            Enviar.Personagem_Usar();
        }

        public static void Personagem_Deletar()
        {
            // Deleta o personagem selecionado
            Enviar.Personagem_Deletar();
        }

        public static void Personagem_Criar()
        {
            // Abre a criação de personagem
            Enviar.Personagem_Criar();
        }

        public static void Personagem_TrocarDireita()
        {
            // Altera o personagem selecionado pelo jogador
            if (Game.SelecionarPersonagem == Listas.Servidor_Dados.Máx_Personagens)
                Game.SelecionarPersonagem = 1;
            else
                Game.SelecionarPersonagem += 1;
        }

        public static void Personagem_TrocarEsquerda()
        {
            // Altera o personagem selecionado pelo jogador
            if (Game.SelecionarPersonagem == 1)
                Game.SelecionarPersonagem = Listas.Servidor_Dados.Máx_Personagens;
            else
                Game.SelecionarPersonagem -= 1;
        }

        public static void Chat_Subir()
        {
            // Sobe as linhas do chat
            if (Ferramentas.Linha > 0)
                Ferramentas.Linha -= 1;
        }

        public static void Chat_Descer()
        {
            // Sobe as linhas do chat
            if (Ferramentas.Chat.Count - 1 - Ferramentas.Linha - Ferramentas.Linhas_Visíveis > 0)
                Ferramentas.Linha += 1;
        }

        public static void Menu_Personagem()
        {
            // Altera a visibilidade do painel e fecha os outros
            Paineis.Encontrar("Menu_Personagem").Geral.Visível = !Paineis.Encontrar("Menu_Personagem").Geral.Visível;
            Paineis.Encontrar("Menu_Inventário").Geral.Visível = false;
        }

        public static void Atributos_Força()
        {
            Enviar.AdicionarPonto(Game.Atributos.Força);
        }

        public static void Atributos_Resistência()
        {
            Enviar.AdicionarPonto(Game.Atributos.Resistência);
        }

        public static void Atributos_Inteligência()
        {
            Enviar.AdicionarPonto(Game.Atributos.Inteligência);
        }

        public static void Atributos_Agilidade()
        {
            Enviar.AdicionarPonto(Game.Atributos.Agilidade);
        }

        public static void Atributos_Vitalidade()
        {
            Enviar.AdicionarPonto(Game.Atributos.Vitalidade);
        }

        public static void Menu_Inventário()
        {
            // Altera a visibilidade do painel e fecha os outros
            Paineis.Encontrar("Menu_Inventário").Geral.Visível = !Paineis.Encontrar("Menu_Inventário").Geral.Visível;
            Paineis.Encontrar("Menu_Personagem").Geral.Visível = false;
        }
    }
}