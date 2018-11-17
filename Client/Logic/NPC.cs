using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

class NPC
{
    public static void Lógica()
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Lists.Mapa.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Mapa.Temp_NPC[i].Índice > 0)
            {
                // Dano
                if (Lists.Mapa.Temp_NPC[i].Sofrendo + 325 < Environment.TickCount) Lists.Mapa.Temp_NPC[i].Sofrendo = 0;

                // Movimento
                ProcessarMovimento(i);
            }
    }

    public static void ProcessarMovimento(byte Índice)
    {
        byte Velocidade = 0;
        short x = Lists.Mapa.Temp_NPC[Índice].X2, y = Lists.Mapa.Temp_NPC[Índice].Y2;

        // Reseta a animação se necessário
        if (Lists.Mapa.Temp_NPC[Índice].Animação == Game.Animação_Parada) Lists.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Direita;

        // Define a velocidade que o jogador se move
        switch (Lists.Mapa.Temp_NPC[Índice].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os Data
                Lists.Mapa.Temp_NPC[Índice].X2 = 0;
                Lists.Mapa.Temp_NPC[Índice].Y2 = 0;
                return;
        }

        // Define a Posição exata do jogador
        switch (Lists.Mapa.Temp_NPC[Índice].Direção)
        {
            case Game.Direções.Acima: Lists.Mapa.Temp_NPC[Índice].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Lists.Mapa.Temp_NPC[Índice].Y2 += Velocidade; break;
            case Game.Direções.Direita: Lists.Mapa.Temp_NPC[Índice].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Lists.Mapa.Temp_NPC[Índice].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Mapa.Temp_NPC[Índice].X2 < 0) Lists.Mapa.Temp_NPC[Índice].X2 = 0;
        if (x < 0 && Lists.Mapa.Temp_NPC[Índice].X2 > 0) Lists.Mapa.Temp_NPC[Índice].X2 = 0;
        if (y > 0 && Lists.Mapa.Temp_NPC[Índice].Y2 < 0) Lists.Mapa.Temp_NPC[Índice].Y2 = 0;
        if (y < 0 && Lists.Mapa.Temp_NPC[Índice].Y2 > 0) Lists.Mapa.Temp_NPC[Índice].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Mapa.Temp_NPC[Índice].Direção == Game.Direções.Direita || Lists.Mapa.Temp_NPC[Índice].Direção == Game.Direções.Abaixo)
        {
            if (Lists.Mapa.Temp_NPC[Índice].X2 < 0 || Lists.Mapa.Temp_NPC[Índice].Y2 < 0)
                return;
        }
        else if (Lists.Mapa.Temp_NPC[Índice].X2 > 0 || Lists.Mapa.Temp_NPC[Índice].Y2 > 0)
            return;

        // Define as animações
        Lists.Mapa.Temp_NPC[Índice].Movimento = Game.Movimentos.Parado;
        if (Lists.Mapa.Temp_NPC[Índice].Animação == Game.Animação_Esquerda)
            Lists.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Direita;
        else
            Lists.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Esquerda;
    }
}

partial class Receber
{
    public static void NPCs(NetIncomingMessage Data)
    {
        // Quantidade
        Lists.NPC = new Lists.Estruturas.NPCs[Data.ReadByte() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.NPC.GetUpperBound(0); i++)
        {
            // Geral
            Lists.NPC[i].Name = Data.ReadString();
            Lists.NPC[i].Textura = Data.ReadInt16();
            Lists.NPC[i].Tipo = Data.ReadByte();

            // Vitais
            Lists.NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
                Lists.NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Mapa_NPCs(NetIncomingMessage Data)
    {
        // Lê os Data
        Lists.Mapa.Temp_NPC = new Lists.Estruturas.Mapa_NPCs[Data.ReadInt16() + 1];
        for (byte i = 1; i <= Lists.Mapa.Temp_NPC.GetUpperBound(0); i++)
        {
            Lists.Mapa.Temp_NPC[i].X2 = 0;
            Lists.Mapa.Temp_NPC[i].Y2 = 0;
            Lists.Mapa.Temp_NPC[i].Índice = Data.ReadInt16();
            Lists.Mapa.Temp_NPC[i].X = Data.ReadByte();
            Lists.Mapa.Temp_NPC[i].Y = Data.ReadByte();
            Lists.Mapa.Temp_NPC[i].Direção = (Game.Direções)Data.ReadByte();

            // Vitais
            Lists.Mapa.Temp_NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
                Lists.Mapa.Temp_NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Mapa_NPC(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Índice = Data.ReadInt16();
        Lists.Mapa.Temp_NPC[i].X = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Direção = (Game.Direções)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Lists.Mapa.Temp_NPC[i].Vital[n] = Data.ReadInt16();
    }

    public static void Mapa_NPC_Movimento(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        byte x = Lists.Mapa.Temp_NPC[i].X, y = Lists.Mapa.Temp_NPC[i].Y;
        Lists.Mapa.Temp_NPC[i].X2 = 0;
        Lists.Mapa.Temp_NPC[i].Y2 = 0;
        Lists.Mapa.Temp_NPC[i].X = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Direção = (Game.Direções)Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Movimento = (Game.Movimentos)Data.ReadByte();

        // Posição exata do jogador
        if (x != Lists.Mapa.Temp_NPC[i].X || y != Lists.Mapa.Temp_NPC[i].Y)
            switch (Lists.Mapa.Temp_NPC[i].Direção)
            {
                case Game.Direções.Acima: Lists.Mapa.Temp_NPC[i].Y2 = Game.Grade; break;
                case Game.Direções.Abaixo: Lists.Mapa.Temp_NPC[i].Y2 = Game.Grade * -1; break;
                case Game.Direções.Direita: Lists.Mapa.Temp_NPC[i].X2 = Game.Grade * -1; break;
                case Game.Direções.Esquerda: Lists.Mapa.Temp_NPC[i].X2 = Game.Grade; break;
            }
    }

    public static void Mapa_NPC_Atacar(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Tipo = Data.ReadByte();

        // Inicia o ataque
        Lists.Mapa.Temp_NPC[Índice].Atacando = true;
        Lists.Mapa.Temp_NPC[Índice].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Tipo == (byte)Game.Alvo.Jogador)
                Lists.Jogador[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Tipo == (byte)Game.Alvo.NPC)
                Lists.Mapa.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Mapa_NPC_Direção(NetIncomingMessage Data)
    {
        // Define a direção de determinado NPC
        byte i = Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].Direção = (Game.Direções)Data.ReadByte();
        Lists.Mapa.Temp_NPC[i].X2 = 0;
        Lists.Mapa.Temp_NPC[i].Y2 = 0;
    }

    public static void Mapa_NPC_Vitais(NetIncomingMessage Data)
    {
        byte Índice = Data.ReadByte();

        // Define os vitais de determinado NPC
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
            Lists.Mapa.Temp_NPC[Índice].Vital[n] = Data.ReadInt16();
    }

    public static void Mapa_NPC_Morreu(NetIncomingMessage Data)
    {
        byte i = Data.ReadByte();

        // Limpa os Data do NPC
        Lists.Mapa.Temp_NPC[i].X2 = 0;
        Lists.Mapa.Temp_NPC[i].Y2 = 0;
        Lists.Mapa.Temp_NPC[i].Índice = 0;
        Lists.Mapa.Temp_NPC[i].X = 0;
        Lists.Mapa.Temp_NPC[i].Y = 0;
        Lists.Mapa.Temp_NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
    }
}

partial class Gráficos
{
    public static void NPC(byte Índice)
    {
        int x2 = Lists.Mapa.Temp_NPC[Índice].X2, y2 = Lists.Mapa.Temp_NPC[Índice].Y2;
        byte Coluna = 0;
        bool Sofrendo = false;
        short Textura = Lists.NPC[Lists.Mapa.Temp_NPC[Índice].Índice].Textura;

        // Previni sobrecargas
        if (Textura <= 0 || Textura > Tex_Personagem.GetUpperBound(0)) return;

        // Define a animação
        if (Lists.Mapa.Temp_NPC[Índice].Atacando && Lists.Mapa.Temp_NPC[Índice].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Mapa.Temp_NPC[Índice].Animação;
            else if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Mapa.Temp_NPC[Índice].Animação;
            else if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Mapa.Temp_NPC[Índice].Animação;
            else if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Mapa.Temp_NPC[Índice].Animação;
        }

        // Demonstra que o personagem está sofrendo dano
        if (Lists.Mapa.Temp_NPC[Índice].Sofrendo > 0) Sofrendo = true;

        // Desenha o jogador
        int x = Lists.Mapa.Temp_NPC[Índice].X * Game.Grade + x2;
        int y = Lists.Mapa.Temp_NPC[Índice].Y * Game.Grade + y2;
        Personagem(Textura, new Point(Game.ConverterX(x), Game.ConverterY(y)), Lists.Mapa.Temp_NPC[Índice].Direção, Coluna, Sofrendo);
        NPC_Name(Índice, x, y);
        NPC_Barras(Índice, x, y);
    }

    public static void NPC_Name(byte Índice, int x, int y)
    {
        Point Posição = new Point(); SFML.Graphics.Color Cor;
        short NPC_Num = Lists.Mapa.Temp_NPC[Índice].Índice;
        int Name_Tamanho = Ferramentas.MedirTexto_Largura(Lists.NPC[NPC_Num].Name);
        Texture Textura = Tex_Personagem[Lists.NPC[NPC_Num].Textura];

        // Posição do texto
        Posição.X = x + TTamanho(Textura).Width / Game.Animação_Quantidade / 2 - Name_Tamanho / 2;
        Posição.Y = y - TTamanho(Textura).Height / Game.Animação_Quantidade / 2;

        // Cor do texto
        switch ((Game.NPCs)Lists.NPC[NPC_Num].Tipo)
        {
            case Game.NPCs.Passivo: Cor = SFML.Graphics.Color.White; break;
            case Game.NPCs.Atacado: Cor = SFML.Graphics.Color.Red; break;
            case Game.NPCs.AoVer: Cor = new SFML.Graphics.Color(228, 120, 51); break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o texto
        Desenhar(Lists.NPC[NPC_Num].Name, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }

    public static void NPC_Barras(byte Índice, int x, int y)
    {
        Lists.Estruturas.Mapa_NPCs NPC = Lists.Mapa.Temp_NPC[Índice];
        int Name_Tamanho = Ferramentas.MedirTexto_Largura(Lists.NPC[NPC.Índice].Name);
        Texture Textura = Tex_Personagem[Lists.NPC[NPC.Índice].Textura];
        short Valor = NPC.Vital[(byte)Game.Vitais.Vida];

        // Apenas se necessário
        if (Valor <= 0 || Valor >= Lists.NPC[NPC.Índice].Vital[(byte)Game.Vitais.Vida]) return;

        // Posição
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + TTamanho(Textura).Height / Game.Animação_Quantidade + 4);
        int Largura_Completa = TTamanho(Textura).Width / Game.Animação_Quantidade;
        int Largura = (Valor * Largura_Completa) / Lists.NPC[NPC.Índice].Vital[(byte)Game.Vitais.Vida];

        // Desenha a barra 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }
}