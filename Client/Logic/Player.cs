using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

public class Player
{
    // O maior índice dos jogadores conectados
    public static byte MaiorÍndice;

    // Inventário
    public static Listas.Estruturas.Inventário[] Inventário = new Listas.Estruturas.Inventário[Game.Máx_Inventário + 1];
    public static byte Inventário_Movendo;

    // Hotbar
    public static Listas.Estruturas.Hotbar[] Hotbar = new Listas.Estruturas.Hotbar[Game.Máx_Hotbar+1];
    public static byte Hotbar_Movendo;

    // O próprio jogador
    public static byte MeuÍndice;
    public static Listas.Estruturas.Jogador Eu
    {
        get
        {
            return Listas.Jogador[MeuÍndice];
        }
        set
        {
            Listas.Jogador[MeuÍndice] = value;
        }
    }

    public static bool EstáJogando(byte Índice)
    {
        // Verifica se o jogador está dentro do Game
        if (MeuÍndice > 0 && !string.IsNullOrEmpty(Listas.Jogador[Índice].Nome))
            return true;
        else
            return false;
    }

    public static short Personagem_Textura(byte Índice)
    {
        byte Classe = Listas.Jogador[Índice].Classe;

        // Retorna com o valor da textura
        if (Listas.Jogador[Índice].Gênero)
            return Listas.Classe[Classe].Textura_Masculina;
        else
            return Listas.Classe[Classe].Textura_Feminina;
    }

    public static void Lógica()
    {
        // Verificações
        VerificarMovimentação();
        VerificarAtaque();

        // Lógica dos jogadores
        for (byte i = 1; i <= Player.MaiorÍndice; i++)
        {
            // Dano
            if (Listas.Jogador[i].Sofrendo + 325 < Environment.TickCount) Listas.Jogador[i].Sofrendo = 0;

            // Movimentaçãp
            ProcessarMovimento(i);
        }
    }

    public static bool TentandoMover()
    {
        // Se estiver pressionando alguma tecla, está tentando se mover
        if (Game.Pressionado_Acima || Game.Pressionado_Abaixo || Game.Pressionado_Esquerda || Game.Pressionado_Direita)
            return true;
        else
            return false;
    }

    public static bool PodeMover()
    {
        // Não mover se já estiver tentando movimentar-se
        if (Listas.Jogador[MeuÍndice].Movimento != Game.Movimentos.Parado)
            return false;

        return true;
    }

    public static void VerificarMovimentação()
    {
        if (Eu.Movimento > 0) return;

        // Move o personagem
        if (Game.Pressionado_Acima) Mover(Game.Direções.Acima);
        else if (Game.Pressionado_Abaixo) Mover(Game.Direções.Abaixo);
        else if (Game.Pressionado_Esquerda) Mover(Game.Direções.Esquerda);
        else if (Game.Pressionado_Direita) Mover(Game.Direções.Direita);
    }

    public static void Mover(Game.Direções Direção)
    {
        // Verifica se o jogador pode se mover
        if (!PodeMover()) return;

        // Define a direção do jogador
        if (Listas.Jogador[MeuÍndice].Direção != Direção)
        {
            Listas.Jogador[MeuÍndice].Direção = Direção;
            Enviar.Jogador_Direção();
        }

        // Verifica se o azulejo seguinte está livre
        if (Map.Azulejo_Bloqueado(Listas.Jogador[MeuÍndice].Mapa, Listas.Jogador[MeuÍndice].X, Listas.Jogador[MeuÍndice].Y, Direção)) return;

        // Define a velocidade que o jogador se move
        if (Game.Pressionado_Shift)
            Listas.Jogador[MeuÍndice].Movimento = Game.Movimentos.Correndo;
        else
            Listas.Jogador[MeuÍndice].Movimento = Game.Movimentos.Andando;

        // Movimento o jogador
        Enviar.Jogador_Mover();

        // Define a Posição exata do jogador
        switch (Direção)
        {
            case Game.Direções.Acima: Listas.Jogador[MeuÍndice].Y2 = Game.Grade; Listas.Jogador[MeuÍndice].Y -= 1; break;
            case Game.Direções.Abaixo: Listas.Jogador[MeuÍndice].Y2 = Game.Grade * -1; Listas.Jogador[MeuÍndice].Y += 1; break;
            case Game.Direções.Direita: Listas.Jogador[MeuÍndice].X2 = Game.Grade * -1; Listas.Jogador[MeuÍndice].X += 1; break;
            case Game.Direções.Esquerda: Listas.Jogador[MeuÍndice].X2 = Game.Grade; Listas.Jogador[MeuÍndice].X -= 1; break;
        }
    }

    public static void ProcessarMovimento(byte Índice)
    {
        byte Velocidade = 0;
        short x = Listas.Jogador[Índice].X2, y = Listas.Jogador[Índice].Y2;

        // Reseta a animação se necessário
        if (Listas.Jogador[Índice].Animação == Game.Animação_Parada) Listas.Jogador[Índice].Animação = Game.Animação_Direita;

        // Define a velocidade que o jogador se move
        switch (Listas.Jogador[Índice].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os dados
                Listas.Jogador[Índice].X2 = 0;
                Listas.Jogador[Índice].Y2 = 0;
                return;
        }

        // Define a Posição exata do jogador
        switch (Listas.Jogador[Índice].Direção)
        {
            case Game.Direções.Acima: Listas.Jogador[Índice].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Listas.Jogador[Índice].Y2 += Velocidade; break;
            case Game.Direções.Direita: Listas.Jogador[Índice].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Listas.Jogador[Índice].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Listas.Jogador[Índice].X2 < 0) Listas.Jogador[Índice].X2 = 0;
        if (x < 0 && Listas.Jogador[Índice].X2 > 0) Listas.Jogador[Índice].X2 = 0;
        if (y > 0 && Listas.Jogador[Índice].Y2 < 0) Listas.Jogador[Índice].Y2 = 0;
        if (y < 0 && Listas.Jogador[Índice].Y2 > 0) Listas.Jogador[Índice].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Listas.Jogador[Índice].Direção == Game.Direções.Direita || Listas.Jogador[Índice].Direção == Game.Direções.Abaixo)
        {
            if (Listas.Jogador[Índice].X2 < 0 || Listas.Jogador[Índice].Y2 < 0)
                return;
        }
        else if (Listas.Jogador[Índice].X2 > 0 || Listas.Jogador[Índice].Y2 > 0)
            return;

        // Define as animações
        Listas.Jogador[Índice].Movimento = Game.Movimentos.Parado;
        if (Listas.Jogador[Índice].Animação == Game.Animação_Esquerda)
            Listas.Jogador[Índice].Animação = Game.Animação_Direita;
        else
            Listas.Jogador[Índice].Animação = Game.Animação_Esquerda;
    }

    public static void VerificarAtaque()
    {
        // Reseta o ataque
        if (Eu.Ataque_Tempo + Game.Ataque_Velocidade < Environment.TickCount)
        {
            Eu.Ataque_Tempo = 0;
            Eu.Atacando = false;
        }

        // Only if you are pressing the attack key and are not attacking
        if (!Game.Pressionado_Control) return;
        if (Eu.Ataque_Tempo > 0) return;

        //Sends the data to the server
        Eu.Ataque_Tempo = Environment.TickCount;
        Enviar.Jogador_Atacar();
    }

    public static void ColetarItem()
    {
        bool TemItem = false, TemEspaço = false;

        // Previni erros
        if (Tools.JanelaAtual != Tools.Janelas.Game) return;

        // Check if you have any items in the coordinates
        for (byte i = 1; i <= Listas.Mapa.Temp_Item.GetUpperBound(0); i++)
            if (Listas.Mapa.Temp_Item[i].X == Eu.X && Listas.Mapa.Temp_Item[i].Y == Eu.Y)
                TemItem = true;

        // Verifica se tem algum espaço vazio no inventário
        for (byte i = 1; i <= Game.Máx_Inventário; i++)
            if (Inventário[i].Item_Num == 0)
                TemEspaço = true;

        // Somente se necessário
        if (!TemItem) return;
        if (!TemEspaço) return;
        if (Environment.TickCount <= Eu.Coletar_Tempo + 250) return;
        if (Panels.Encontrar("Chat").General.Visível) return;

        // Coleta o item
        Enviar.ColetarItem();
        Eu.Coletar_Tempo = Environment.TickCount;
    }
}

partial class Enviar
{
    public static void Jogador_Direção()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Direção);
        Dados.Write((byte)Player.Eu.Direção);
        Pacote(Dados);
    }

    public static void Jogador_Mover()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Mover);
        Dados.Write(Player.Eu.X);
        Dados.Write(Player.Eu.Y);
        Dados.Write((byte)Player.Eu.Movimento);
        Pacote(Dados);
    }

    public static void Jogador_Atacar()
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Jogador_Atacar);
        Pacote(Dados);
    }
}

partial class Receber
{
    private static void Jogador_Dados(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Defini os dados do jogador
        Listas.Jogador[Índice].Nome = Dados.ReadString();
        Listas.Jogador[Índice].Classe = Dados.ReadByte();
        Listas.Jogador[Índice].Gênero = Dados.ReadBoolean();
        Listas.Jogador[Índice].Level = Dados.ReadInt16();
        Listas.Jogador[Índice].Mapa = Dados.ReadInt16();
        Listas.Jogador[Índice].X = Dados.ReadByte();
        Listas.Jogador[Índice].Y = Dados.ReadByte();
        Listas.Jogador[Índice].Direção = (Game.Direções)Dados.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
        {
            Listas.Jogador[Índice].Vital[n] = Dados.ReadInt16();
            Listas.Jogador[Índice].Máx_Vital[n] = Dados.ReadInt16();
        }
        for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Listas.Jogador[Índice].Atributo[n] = Dados.ReadInt16();
        for (byte n = 0; n <= (byte)Game.Equipamentos.Quantidade - 1; n++) Listas.Jogador[Índice].Equipamento[n] = Dados.ReadInt16();
    }

    private static void Jogador_Posição(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Defini os dados do jogador
        Listas.Jogador[Índice].X = Dados.ReadByte();
        Listas.Jogador[Índice].Y = Dados.ReadByte();
        Listas.Jogador[Índice].Direção = (Game.Direções)Dados.ReadByte();

        // Para a movimentação
        Listas.Jogador[Índice].X2 = 0;
        Listas.Jogador[Índice].Y2 = 0;
        Listas.Jogador[Índice].Movimento = Game.Movimentos.Parado;
    }

    private static void Jogador_Vitais(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Define os dados
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++)
        {
            Listas.Jogador[Índice].Vital[i] = Dados.ReadInt16();
            Listas.Jogador[Índice].Máx_Vital[i] = Dados.ReadInt16();
        }
    }

    private static void Jogador_Equipamentos(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Define os dados
        for (byte i = 0; i <= (byte)Game.Equipamentos.Quantidade - 1; i++)  Listas.Jogador[Índice].Equipamento[i] = Dados.ReadInt16();
    }

    private static void Jogador_Saiu(NetIncomingMessage Dados)
    {
        // Limpa os dados do jogador
        Limpar.Jogador(Dados.ReadByte());
    }

    public static void Jogador_Mover(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Move o jogador
        Listas.Jogador[Índice].X = Dados.ReadByte();
        Listas.Jogador[Índice].Y = Dados.ReadByte();
        Listas.Jogador[Índice].Direção = (Game.Direções)Dados.ReadByte();
        Listas.Jogador[Índice].Movimento = (Game.Movimentos)Dados.ReadByte();
        Listas.Jogador[Índice].X2 = 0;
        Listas.Jogador[Índice].Y2 = 0;

        // Posição exata do jogador
        switch (Listas.Jogador[Índice].Direção)
        {
            case Game.Direções.Acima: Listas.Jogador[Índice].Y2 = Game.Grade; break;
            case Game.Direções.Abaixo: Listas.Jogador[Índice].Y2 = Game.Grade * -1; break;
            case Game.Direções.Direita: Listas.Jogador[Índice].X2 = Game.Grade * -1; break;
            case Game.Direções.Esquerda: Listas.Jogador[Índice].X2 = Game.Grade; break;
        }
    }

    public static void Jogador_Direção(NetIncomingMessage Dados)
    {
        // Define a direção de determinado jogador
        Listas.Jogador[Dados.ReadByte()].Direção = (Game.Direções)Dados.ReadByte();
    }

    public static void Jogador_Atacar(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte(), Vítima = Dados.ReadByte(), Vítima_Tipo = Dados.ReadByte();

        // Inicia o ataque
        Listas.Jogador[Índice].Atacando = true;
        Listas.Jogador[Índice].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Tipo == (byte)Game.Alvo.Jogador)
                Listas.Jogador[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Tipo == (byte)Game.Alvo.NPC)
                Listas.Mapa.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Jogador_Experiência(NetIncomingMessage Dados)
    {
        // Define os dados
        Player.Eu.Experiência = Dados.ReadInt16();
        Player.Eu.ExpNecessária = Dados.ReadInt16();
        Player.Eu.Pontos = Dados.ReadByte();
    }

    private static void Jogador_Inventário(NetIncomingMessage Dados)
    {
        // Define os dados
        for (byte i = 1; i <= Game.Máx_Inventário; i++)
        {
            Player.Inventário[i].Item_Num = Dados.ReadInt16();
            Player.Inventário[i].Quantidade = Dados.ReadInt16();
        }
    }

    private static void Jogador_Hotbar(NetIncomingMessage Dados)
    {
        // Define os dados
        for (byte i = 1; i <= Game.Máx_Hotbar; i++)
        {
            Player.Hotbar[i].Tipo = Dados.ReadByte();
            Player.Hotbar[i].Slot = Dados.ReadByte();
        }
    }
}

partial class Gráficos
{
    public static void Jogador_Personagem(byte Índice)
    {
        byte Coluna = Game.Animação_Parada;
        int x, y;
        short x2 = Listas.Jogador[Índice].X2, y2 = Listas.Jogador[Índice].Y2;
        bool Sofrendo = false;
        short Textura = Player.Personagem_Textura(Índice);

        // Previni sobrecargas
        if (Textura <= 0 || Textura > Tex_Personagem.GetUpperBound(0)) return;

        // Define a animação
        if (Listas.Jogador[Índice].Atacando && Listas.Jogador[Índice].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Listas.Jogador[Índice].Animação;
            if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Listas.Jogador[Índice].Animação;
            if (y2 > 8 && y2 < Game.Grade) Coluna = Listas.Jogador[Índice].Animação;
            if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Listas.Jogador[Índice].Animação;
        }

        // Demonstra que o personagem está sofrendo dano
        if (Listas.Jogador[Índice].Sofrendo > 0) Sofrendo = true;

        // Desenha o jogador
        x = Listas.Jogador[Índice].X * Game.Grade + Listas.Jogador[Índice].X2;
        y = Listas.Jogador[Índice].Y * Game.Grade + Listas.Jogador[Índice].Y2;
        Personagem(Textura, new Point(Game.ConverterX(x), Game.ConverterY(y)), Listas.Jogador[Índice].Direção, Coluna, Sofrendo);
        Jogador_Nome(Índice, x, y);
        Jogador_Barras(Índice, x, y);
    }

    public static void Jogador_Barras(byte Índice, int x, int y)
    {
        Size Personagem_Tamanho = TTamanho(Tex_Personagem[Player.Personagem_Textura(Índice)]);
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + Personagem_Tamanho.Height / Game.Animação_Quantidade + 4);
        int Largura_Completa = Personagem_Tamanho.Width / Game.Animação_Quantidade;
        short Contagem = Listas.Jogador[Índice].Vital[(byte)Game.Vitais.Vida];

        // Apenas se necessário
        if (Contagem <= 0 || Contagem >= Listas.Jogador[Índice].Máx_Vital[(byte)Game.Vitais.Vida]) return;

        // Cálcula a largura da barra
        int Largura = (Contagem * Largura_Completa) / Listas.Jogador[Índice].Máx_Vital[(byte)Game.Vitais.Vida];

        // Desenha as barras 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }

    public static void Jogador_Nome(byte Índice, int x, int y)
    {
        Texture Textura = Tex_Personagem[Player.Personagem_Textura(Índice)];
        int Nome_Tamanho = Tools.MedirTexto_Largura(Listas.Jogador[Índice].Nome);

        // Posição do texto
        Point Posição = new Point();
        Posição.X = x + TTamanho(Textura).Width / Game.Animação_Quantidade / 2 - Nome_Tamanho / 2;
        Posição.Y = y - TTamanho(Textura).Height / Game.Animação_Quantidade / 2;

        // Cor do texto
        SFML.Graphics.Color Cor;
        if (Índice == Player.MeuÍndice)
            Cor = SFML.Graphics.Color.Yellow;
        else
            Cor = SFML.Graphics.Color.White;

        // Desenha o texto
        Desenhar(Listas.Jogador[Índice].Nome, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }
}