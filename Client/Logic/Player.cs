using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

public class Player
{
    // O maior índice dos jogadores conectados
    public static byte MaiorÍndice;

    // Inventário
    public static Lists.Estruturas.Inventário[] Inventário = new Lists.Estruturas.Inventário[Game.Máx_Inventário + 1];
    public static byte Inventário_Movendo;

    // Hotbar
    public static Lists.Estruturas.Hotbar[] Hotbar = new Lists.Estruturas.Hotbar[Game.Máx_Hotbar+1];
    public static byte Hotbar_Movendo;

    // O próprio jogador
    public static byte MeuÍndice;
    public static Lists.Estruturas.Jogador Eu
    {
        get
        {
            return Lists.Jogador[MeuÍndice];
        }
        set
        {
            Lists.Jogador[MeuÍndice] = value;
        }
    }

    public static bool EstáJogando(byte Índice)
    {
        // Verifica se o jogador está dentro do Game
        if (MeuÍndice > 0 && !string.IsNullOrEmpty(Lists.Jogador[Índice].Name))
            return true;
        else
            return false;
    }

    public static short Personagem_Textura(byte Índice)
    {
        byte Classe = Lists.Jogador[Índice].Classe;

        // Retorna com o valor da textura
        if (Lists.Jogador[Índice].Gênero)
            return Lists.Classe[Classe].Textura_Masculina;
        else
            return Lists.Classe[Classe].Textura_Feminina;
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
            if (Lists.Jogador[i].Sofrendo + 325 < Environment.TickCount) Lists.Jogador[i].Sofrendo = 0;

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
        if (Lists.Jogador[MeuÍndice].Movimento != Game.Movimentos.Parado)
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
        if (Lists.Jogador[MeuÍndice].Direção != Direção)
        {
            Lists.Jogador[MeuÍndice].Direção = Direção;
            Enviar.Jogador_Direção();
        }

        // Verifica se o azulejo seguinte está livre
        if (Map.Azulejo_Bloqueado(Lists.Jogador[MeuÍndice].Mapa, Lists.Jogador[MeuÍndice].X, Lists.Jogador[MeuÍndice].Y, Direção)) return;

        // Define a velocidade que o jogador se move
        if (Game.Pressionado_Shift)
            Lists.Jogador[MeuÍndice].Movimento = Game.Movimentos.Correndo;
        else
            Lists.Jogador[MeuÍndice].Movimento = Game.Movimentos.Andando;

        // Movimento o jogador
        Enviar.Jogador_Mover();

        // Define a Posição exata do jogador
        switch (Direção)
        {
            case Game.Direções.Acima: Lists.Jogador[MeuÍndice].Y2 = Game.Grade; Lists.Jogador[MeuÍndice].Y -= 1; break;
            case Game.Direções.Abaixo: Lists.Jogador[MeuÍndice].Y2 = Game.Grade * -1; Lists.Jogador[MeuÍndice].Y += 1; break;
            case Game.Direções.Direita: Lists.Jogador[MeuÍndice].X2 = Game.Grade * -1; Lists.Jogador[MeuÍndice].X += 1; break;
            case Game.Direções.Esquerda: Lists.Jogador[MeuÍndice].X2 = Game.Grade; Lists.Jogador[MeuÍndice].X -= 1; break;
        }
    }

    public static void ProcessarMovimento(byte Índice)
    {
        byte Velocidade = 0;
        short x = Lists.Jogador[Índice].X2, y = Lists.Jogador[Índice].Y2;

        // Reseta a animação se necessário
        if (Lists.Jogador[Índice].Animação == Game.Animação_Parada) Lists.Jogador[Índice].Animação = Game.Animação_Direita;

        // Define a velocidade que o jogador se move
        switch (Lists.Jogador[Índice].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os Data
                Lists.Jogador[Índice].X2 = 0;
                Lists.Jogador[Índice].Y2 = 0;
                return;
        }

        // Define a Posição exata do jogador
        switch (Lists.Jogador[Índice].Direção)
        {
            case Game.Direções.Acima: Lists.Jogador[Índice].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Lists.Jogador[Índice].Y2 += Velocidade; break;
            case Game.Direções.Direita: Lists.Jogador[Índice].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Lists.Jogador[Índice].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Jogador[Índice].X2 < 0) Lists.Jogador[Índice].X2 = 0;
        if (x < 0 && Lists.Jogador[Índice].X2 > 0) Lists.Jogador[Índice].X2 = 0;
        if (y > 0 && Lists.Jogador[Índice].Y2 < 0) Lists.Jogador[Índice].Y2 = 0;
        if (y < 0 && Lists.Jogador[Índice].Y2 > 0) Lists.Jogador[Índice].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Jogador[Índice].Direção == Game.Direções.Direita || Lists.Jogador[Índice].Direção == Game.Direções.Abaixo)
        {
            if (Lists.Jogador[Índice].X2 < 0 || Lists.Jogador[Índice].Y2 < 0)
                return;
        }
        else if (Lists.Jogador[Índice].X2 > 0 || Lists.Jogador[Índice].Y2 > 0)
            return;

        // Define as animações
        Lists.Jogador[Índice].Movimento = Game.Movimentos.Parado;
        if (Lists.Jogador[Índice].Animação == Game.Animação_Esquerda)
            Lists.Jogador[Índice].Animação = Game.Animação_Direita;
        else
            Lists.Jogador[Índice].Animação = Game.Animação_Esquerda;
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
        for (byte i = 1; i <= Lists.Mapa.Temp_Item.GetUpperBound(0); i++)
            if (Lists.Mapa.Temp_Item[i].X == Eu.X && Lists.Mapa.Temp_Item[i].Y == Eu.Y)
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
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Jogador_Direção);
        Data.Write((byte)Player.Eu.Direção);
        Pacote(Data);
    }

    public static void Jogador_Mover()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Jogador_Mover);
        Data.Write(Player.Eu.X);
        Data.Write(Player.Eu.Y);
        Data.Write((byte)Player.Eu.Movimento);
        Pacote(Data);
    }

    public static void Jogador_Atacar()
    {
        NetOutgoingMessage Data = Rede.Dispositivo.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Jogador_Atacar);
        Pacote(Data);
    }
}

partial class Receber
{
    private static void Jogador_Data(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Defini os Data do jogador
        Lists.Jogador[Índice].Name = Data.ReadString();
        Lists.Jogador[Índice].Classe = Data.ReadByte();
        Lists.Jogador[Índice].Gênero = Data.ReadBoolean();
        Lists.Jogador[Índice].Level = Data.ReadInt16();
        Lists.Jogador[Índice].Mapa = Data.ReadInt16();
        Lists.Jogador[Índice].X = Data.ReadByte();
        Lists.Jogador[Índice].Y = Data.ReadByte();
        Lists.Jogador[Índice].Direção = (Game.Direções)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
        {
            Lists.Jogador[Índice].Vital[n] = Data.ReadInt16();
            Lists.Jogador[Índice].Máx_Vital[n] = Data.ReadInt16();
        }
        for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Lists.Jogador[Índice].Atributo[n] = Data.ReadInt16();
        for (byte n = 0; n <= (byte)Game.Equipamentos.Quantidade - 1; n++) Lists.Jogador[Índice].Equipamento[n] = Data.ReadInt16();
    }

    private static void Jogador_Posição(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Defini os Data do jogador
        Lists.Jogador[Índice].X = Data.ReadByte();
        Lists.Jogador[Índice].Y = Data.ReadByte();
        Lists.Jogador[Índice].Direção = (Game.Direções)Data.ReadByte();

        // Para a movimentação
        Lists.Jogador[Índice].X2 = 0;
        Lists.Jogador[Índice].Y2 = 0;
        Lists.Jogador[Índice].Movimento = Game.Movimentos.Parado;
    }

    private static void Jogador_Vitais(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Define os Data
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++)
        {
            Lists.Jogador[Índice].Vital[i] = Data.ReadInt16();
            Lists.Jogador[Índice].Máx_Vital[i] = Data.ReadInt16();
        }
    }

    private static void Jogador_Equipamentos(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Define os Data
        for (byte i = 0; i <= (byte)Game.Equipamentos.Quantidade - 1; i++)  Lists.Jogador[Índice].Equipamento[i] = Data.ReadInt16();
    }

    private static void Jogador_Saiu(NetIncomingMessage Data)
    {
        // Limpa os Data do jogador
        Limpar.Jogador(Data.ReadByte());
    }

    public static void Jogador_Mover(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Move o jogador
        Lists.Jogador[Índice].X = Data.ReadByte();
        Lists.Jogador[Índice].Y = Data.ReadByte();
        Lists.Jogador[Índice].Direção = (Game.Direções)Data.ReadByte();
        Lists.Jogador[Índice].Movimento = (Game.Movimentos)Data.ReadByte();
        Lists.Jogador[Índice].X2 = 0;
        Lists.Jogador[Índice].Y2 = 0;

        // Posição exata do jogador
        switch (Lists.Jogador[Índice].Direção)
        {
            case Game.Direções.Acima: Lists.Jogador[Índice].Y2 = Game.Grade; break;
            case Game.Direções.Abaixo: Lists.Jogador[Índice].Y2 = Game.Grade * -1; break;
            case Game.Direções.Direita: Lists.Jogador[Índice].X2 = Game.Grade * -1; break;
            case Game.Direções.Esquerda: Lists.Jogador[Índice].X2 = Game.Grade; break;
        }
    }

    public static void Jogador_Direção(NetIncomingMessage Data)
    {
        // Define a direção de determinado jogador
        Lists.Jogador[Data.ReadByte()].Direção = (Game.Direções)Data.ReadByte();
    }

    public static void Jogador_Atacar(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Tipo = Data.ReadByte();

        // Inicia o ataque
        Lists.Jogador[Índice].Atacando = true;
        Lists.Jogador[Índice].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Tipo == (byte)Game.Alvo.Jogador)
                Lists.Jogador[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Tipo == (byte)Game.Alvo.NPC)
                Lists.Mapa.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Jogador_Experiência(NetIncomingMessage Data)
    {
        // Define os Data
        Player.Eu.Experiência = Data.ReadInt16();
        Player.Eu.ExpNecessária = Data.ReadInt16();
        Player.Eu.Pontos = Data.ReadByte();
    }

    private static void Jogador_Inventário(NetIncomingMessage Data)
    {
        // Define os Data
        for (byte i = 1; i <= Game.Máx_Inventário; i++)
        {
            Player.Inventário[i].Item_Num = Data.ReadInt16();
            Player.Inventário[i].Quantidade = Data.ReadInt16();
        }
    }

    private static void Jogador_Hotbar(NetIncomingMessage Data)
    {
        // Define os Data
        for (byte i = 1; i <= Game.Máx_Hotbar; i++)
        {
            Player.Hotbar[i].Tipo = Data.ReadByte();
            Player.Hotbar[i].Slot = Data.ReadByte();
        }
    }
}

partial class Gráficos
{
    public static void Jogador_Personagem(byte Índice)
    {
        byte Coluna = Game.Animação_Parada;
        int x, y;
        short x2 = Lists.Jogador[Índice].X2, y2 = Lists.Jogador[Índice].Y2;
        bool Sofrendo = false;
        short Textura = Player.Personagem_Textura(Índice);

        // Previni sobrecargas
        if (Textura <= 0 || Textura > Tex_Personagem.GetUpperBound(0)) return;

        // Define a animação
        if (Lists.Jogador[Índice].Atacando && Lists.Jogador[Índice].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Jogador[Índice].Animação;
            if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Jogador[Índice].Animação;
            if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Jogador[Índice].Animação;
            if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Jogador[Índice].Animação;
        }

        // Demonstra que o personagem está sofrendo dano
        if (Lists.Jogador[Índice].Sofrendo > 0) Sofrendo = true;

        // Desenha o jogador
        x = Lists.Jogador[Índice].X * Game.Grade + Lists.Jogador[Índice].X2;
        y = Lists.Jogador[Índice].Y * Game.Grade + Lists.Jogador[Índice].Y2;
        Personagem(Textura, new Point(Game.ConverterX(x), Game.ConverterY(y)), Lists.Jogador[Índice].Direção, Coluna, Sofrendo);
        Jogador_Name(Índice, x, y);
        Jogador_Barras(Índice, x, y);
    }

    public static void Jogador_Barras(byte Índice, int x, int y)
    {
        Size Personagem_Tamanho = TTamanho(Tex_Personagem[Player.Personagem_Textura(Índice)]);
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + Personagem_Tamanho.Height / Game.Animação_Quantidade + 4);
        int Largura_Completa = Personagem_Tamanho.Width / Game.Animação_Quantidade;
        short Contagem = Lists.Jogador[Índice].Vital[(byte)Game.Vitais.Vida];

        // Apenas se necessário
        if (Contagem <= 0 || Contagem >= Lists.Jogador[Índice].Máx_Vital[(byte)Game.Vitais.Vida]) return;

        // Cálcula a largura da barra
        int Largura = (Contagem * Largura_Completa) / Lists.Jogador[Índice].Máx_Vital[(byte)Game.Vitais.Vida];

        // Desenha as barras 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }

    public static void Jogador_Name(byte Índice, int x, int y)
    {
        Texture Textura = Tex_Personagem[Player.Personagem_Textura(Índice)];
        int Name_Tamanho = Tools.MedirTexto_Largura(Lists.Jogador[Índice].Name);

        // Posição do texto
        Point Posição = new Point();
        Posição.X = x + TTamanho(Textura).Width / Game.Animação_Quantidade / 2 - Name_Tamanho / 2;
        Posição.Y = y - TTamanho(Textura).Height / Game.Animação_Quantidade / 2;

        // Cor do texto
        SFML.Graphics.Color Cor;
        if (Índice == Player.MeuÍndice)
            Cor = SFML.Graphics.Color.Yellow;
        else
            Cor = SFML.Graphics.Color.White;

        // Desenha o texto
        Desenhar(Lists.Jogador[Índice].Name, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }
}