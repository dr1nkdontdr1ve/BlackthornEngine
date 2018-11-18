using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

class NPC
{
    public static void Lógica()
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
            {
                // Dano
                if (Lists.Map.Temp_NPC[i].Sofrendo + 325 < Environment.TickCount) Lists.Map.Temp_NPC[i].Sofrendo = 0;

                // Movimento
                ProcessarMovimento(i);
            }
    }

    public static void ProcessarMovimento(byte Index)
    {
        byte Velocidade = 0;
        short x = Lists.Map.Temp_NPC[Index].X2, y = Lists.Map.Temp_NPC[Index].Y2;

        // Reseta a animação se necessário
        if (Lists.Map.Temp_NPC[Index].Animação == Game.Animação_Parada) Lists.Map.Temp_NPC[Index].Animação = Game.Animação_Direita;

        // Define a velocidade que o Player se move
        switch (Lists.Map.Temp_NPC[Index].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os Data
                Lists.Map.Temp_NPC[Index].X2 = 0;
                Lists.Map.Temp_NPC[Index].Y2 = 0;
                return;
        }

        // Define a Posição exata do Player
        switch (Lists.Map.Temp_NPC[Index].Direction)
        {
            case Game.Direções.Acima: Lists.Map.Temp_NPC[Index].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Lists.Map.Temp_NPC[Index].Y2 += Velocidade; break;
            case Game.Direções.Direita: Lists.Map.Temp_NPC[Index].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Lists.Map.Temp_NPC[Index].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Map.Temp_NPC[Index].X2 < 0) Lists.Map.Temp_NPC[Index].X2 = 0;
        if (x < 0 && Lists.Map.Temp_NPC[Index].X2 > 0) Lists.Map.Temp_NPC[Index].X2 = 0;
        if (y > 0 && Lists.Map.Temp_NPC[Index].Y2 < 0) Lists.Map.Temp_NPC[Index].Y2 = 0;
        if (y < 0 && Lists.Map.Temp_NPC[Index].Y2 > 0) Lists.Map.Temp_NPC[Index].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Map.Temp_NPC[Index].Direction == Game.Direções.Direita || Lists.Map.Temp_NPC[Index].Direction == Game.Direções.Abaixo)
        {
            if (Lists.Map.Temp_NPC[Index].X2 < 0 || Lists.Map.Temp_NPC[Index].Y2 < 0)
                return;
        }
        else if (Lists.Map.Temp_NPC[Index].X2 > 0 || Lists.Map.Temp_NPC[Index].Y2 > 0)
            return;

        // Define as animações
        Lists.Map.Temp_NPC[Index].Movimento = Game.Movimentos.Parado;
        if (Lists.Map.Temp_NPC[Index].Animação == Game.Animação_Esquerda)
            Lists.Map.Temp_NPC[Index].Animação = Game.Animação_Direita;
        else
            Lists.Map.Temp_NPC[Index].Animação = Game.Animação_Esquerda;
    }
}

partial class Receber
{
    public static void NPCs(NetIncomingMessage Data)
    {
        // Amount
        Lists.NPC = new Lists.Structures.NPCs[Data.ReadByte() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.NPC.GetUpperBound(0); i++)
        {
            // Geral
            Lists.NPC[i].Name = Data.ReadString();
            Lists.NPC[i].Texture = Data.ReadInt16();
            Lists.NPC[i].Type = Data.ReadByte();

            // Vital
            Lists.NPC[i].Vital = new short[(byte)Game.Vital.Amount];
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Lists.NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Map_NPCs(NetIncomingMessage Data)
    {
        // Lê os Data
        Lists.Map.Temp_NPC = new Lists.Structures.Map_NPCs[Data.ReadInt16() + 1];
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
        {
            Lists.Map.Temp_NPC[i].X2 = 0;
            Lists.Map.Temp_NPC[i].Y2 = 0;
            Lists.Map.Temp_NPC[i].Index = Data.ReadInt16();
            Lists.Map.Temp_NPC[i].X = Data.ReadByte();
            Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
            Lists.Map.Temp_NPC[i].Direction = (Game.Direções)Data.ReadByte();

            // Vital
            Lists.Map.Temp_NPC[i].Vital = new short[(byte)Game.Vital.Amount];
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Lists.Map.Temp_NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Map_NPC(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Index = Data.ReadInt16();
        Lists.Map.Temp_NPC[i].X = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Direções)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Lists.Map.Temp_NPC[i].Vital[n] = Data.ReadInt16();
    }

    public static void Map_NPC_Movimento(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        byte x = Lists.Map.Temp_NPC[i].X, y = Lists.Map.Temp_NPC[i].Y;
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
        Lists.Map.Temp_NPC[i].X = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Direções)Data.ReadByte();
        Lists.Map.Temp_NPC[i].Movimento = (Game.Movimentos)Data.ReadByte();

        // Posição exata do Player
        if (x != Lists.Map.Temp_NPC[i].X || y != Lists.Map.Temp_NPC[i].Y)
            switch (Lists.Map.Temp_NPC[i].Direction)
            {
                case Game.Direções.Acima: Lists.Map.Temp_NPC[i].Y2 = Game.Grade; break;
                case Game.Direções.Abaixo: Lists.Map.Temp_NPC[i].Y2 = Game.Grade * -1; break;
                case Game.Direções.Direita: Lists.Map.Temp_NPC[i].X2 = Game.Grade * -1; break;
                case Game.Direções.Esquerda: Lists.Map.Temp_NPC[i].X2 = Game.Grade; break;
            }
    }

    public static void Map_NPC_Atacar(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Type = Data.ReadByte();

        // Inicia o ataque
        Lists.Map.Temp_NPC[Index].Atacando = true;
        Lists.Map.Temp_NPC[Index].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Type == (byte)Game.Alvo.Player)
                Lists.Player[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Type == (byte)Game.Alvo.NPC)
                Lists.Map.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Map_NPC_Direction(NetIncomingMessage Data)
    {
        // Define a Direction de determinado NPC
        byte i = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Direções)Data.ReadByte();
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
    }

    public static void Map_NPC_Vital(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Define os Vital de determinado NPC
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
            Lists.Map.Temp_NPC[Index].Vital[n] = Data.ReadInt16();
    }

    public static void Map_NPC_Morreu(NetIncomingMessage Data)
    {
        byte i = Data.ReadByte();

        // Limpa os Data do NPC
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
        Lists.Map.Temp_NPC[i].Index = 0;
        Lists.Map.Temp_NPC[i].X = 0;
        Lists.Map.Temp_NPC[i].Y = 0;
        Lists.Map.Temp_NPC[i].Vital = new short[(byte)Game.Vital.Amount];
    }
}

partial class Gráficos
{
    public static void NPC(byte Index)
    {
        int x2 = Lists.Map.Temp_NPC[Index].X2, y2 = Lists.Map.Temp_NPC[Index].Y2;
        byte Coluna = 0;
        bool Sofrendo = false;
        short Texture = Lists.NPC[Lists.Map.Temp_NPC[Index].Index].Texture;

        // Previni sobrecargas
        if (Texture <= 0 || Texture > Tex_Character.GetUpperBound(0)) return;

        // Define a animação
        if (Lists.Map.Temp_NPC[Index].Atacando && Lists.Map.Temp_NPC[Index].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Map.Temp_NPC[Index].Animação;
            else if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Map.Temp_NPC[Index].Animação;
            else if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Map.Temp_NPC[Index].Animação;
            else if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Map.Temp_NPC[Index].Animação;
        }

        // Demonstra que o Character está sofrendo dano
        if (Lists.Map.Temp_NPC[Index].Sofrendo > 0) Sofrendo = true;

        // Desenha o Player
        int x = Lists.Map.Temp_NPC[Index].X * Game.Grade + x2;
        int y = Lists.Map.Temp_NPC[Index].Y * Game.Grade + y2;
        Character(Texture, new Point(Game.ConverterX(x), Game.ConverterY(y)), Lists.Map.Temp_NPC[Index].Direction, Coluna, Sofrendo);
        NPC_Name(Index, x, y);
        NPC_Barras(Index, x, y);
    }

    public static void NPC_Name(byte Index, int x, int y)
    {
        Point Posição = new Point(); SFML.Graphics.Color Cor;
        short NPC_Num = Lists.Map.Temp_NPC[Index].Index;
        int Name_Tamanho = Ferramentas.MedirTexto_Largura(Lists.NPC[NPC_Num].Name);
        Texture Texture = Tex_Character[Lists.NPC[NPC_Num].Texture];

        // Posição do texto
        Posição.X = x + TTamanho(Texture).Width / Game.Animação_Amount / 2 - Name_Tamanho / 2;
        Posição.Y = y - TTamanho(Texture).Height / Game.Animação_Amount / 2;

        // Cor do texto
        switch ((Game.NPCs)Lists.NPC[NPC_Num].Type)
        {
            case Game.NPCs.Passivo: Cor = SFML.Graphics.Color.White; break;
            case Game.NPCs.Atacado: Cor = SFML.Graphics.Color.Red; break;
            case Game.NPCs.AoVer: Cor = new SFML.Graphics.Color(228, 120, 51); break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o texto
        Desenhar(Lists.NPC[NPC_Num].Name, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }

    public static void NPC_Barras(byte Index, int x, int y)
    {
        Lists.Structures.Map_NPCs NPC = Lists.Map.Temp_NPC[Index];
        int Name_Tamanho = Ferramentas.MedirTexto_Largura(Lists.NPC[NPC.Index].Name);
        Texture Texture = Tex_Character[Lists.NPC[NPC.Index].Texture];
        short Valor = NPC.Vital[(byte)Game.Vital.Vida];

        // Apenas se necessário
        if (Valor <= 0 || Valor >= Lists.NPC[NPC.Index].Vital[(byte)Game.Vital.Vida]) return;

        // Posição
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + TTamanho(Texture).Height / Game.Animação_Amount + 4);
        int Largura_Completa = TTamanho(Texture).Width / Game.Animação_Amount;
        int Largura = (Valor * Largura_Completa) / Lists.NPC[NPC.Index].Vital[(byte)Game.Vital.Vida];

        // Desenha a barra 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }
}