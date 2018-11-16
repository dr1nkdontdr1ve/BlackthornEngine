﻿using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

class NPC
{
    public static void Lógica()
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Listas.Mapa.Temp_NPC.GetUpperBound(0); i++)
            if (Listas.Mapa.Temp_NPC[i].Índice > 0)
            {
                // Dano
                if (Listas.Mapa.Temp_NPC[i].Sofrendo + 325 < Environment.TickCount) Listas.Mapa.Temp_NPC[i].Sofrendo = 0;

                // Movimento
                ProcessarMovimento(i);
            }
    }

    public static void ProcessarMovimento(byte Índice)
    {
        byte Velocidade = 0;
        short x = Listas.Mapa.Temp_NPC[Índice].X2, y = Listas.Mapa.Temp_NPC[Índice].Y2;

        // Reseta a animação se necessário
        if (Listas.Mapa.Temp_NPC[Índice].Animação == Game.Animação_Parada) Listas.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Direita;

        // Define a velocidade que o jogador se move
        switch (Listas.Mapa.Temp_NPC[Índice].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os dados
                Listas.Mapa.Temp_NPC[Índice].X2 = 0;
                Listas.Mapa.Temp_NPC[Índice].Y2 = 0;
                return;
        }

        // Define a Posição exata do jogador
        switch (Listas.Mapa.Temp_NPC[Índice].Direção)
        {
            case Game.Direções.Acima: Listas.Mapa.Temp_NPC[Índice].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Listas.Mapa.Temp_NPC[Índice].Y2 += Velocidade; break;
            case Game.Direções.Direita: Listas.Mapa.Temp_NPC[Índice].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Listas.Mapa.Temp_NPC[Índice].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Listas.Mapa.Temp_NPC[Índice].X2 < 0) Listas.Mapa.Temp_NPC[Índice].X2 = 0;
        if (x < 0 && Listas.Mapa.Temp_NPC[Índice].X2 > 0) Listas.Mapa.Temp_NPC[Índice].X2 = 0;
        if (y > 0 && Listas.Mapa.Temp_NPC[Índice].Y2 < 0) Listas.Mapa.Temp_NPC[Índice].Y2 = 0;
        if (y < 0 && Listas.Mapa.Temp_NPC[Índice].Y2 > 0) Listas.Mapa.Temp_NPC[Índice].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Listas.Mapa.Temp_NPC[Índice].Direção == Game.Direções.Direita || Listas.Mapa.Temp_NPC[Índice].Direção == Game.Direções.Abaixo)
        {
            if (Listas.Mapa.Temp_NPC[Índice].X2 < 0 || Listas.Mapa.Temp_NPC[Índice].Y2 < 0)
                return;
        }
        else if (Listas.Mapa.Temp_NPC[Índice].X2 > 0 || Listas.Mapa.Temp_NPC[Índice].Y2 > 0)
            return;

        // Define as animações
        Listas.Mapa.Temp_NPC[Índice].Movimento = Game.Movimentos.Parado;
        if (Listas.Mapa.Temp_NPC[Índice].Animação == Game.Animação_Esquerda)
            Listas.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Direita;
        else
            Listas.Mapa.Temp_NPC[Índice].Animação = Game.Animação_Esquerda;
    }
}

partial class Receber
{
    public static void NPCs(NetIncomingMessage Dados)
    {
        // Quantidade
        Listas.NPC = new Listas.Estruturas.NPCs[Dados.ReadByte() + 1];

        // Lê os dados de todos
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)
        {
            // Geral
            Listas.NPC[i].Nome = Dados.ReadString();
            Listas.NPC[i].Textura = Dados.ReadInt16();
            Listas.NPC[i].Tipo = Dados.ReadByte();

            // Vitais
            Listas.NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
                Listas.NPC[i].Vital[n] = Dados.ReadInt16();
        }
    }

    public static void Mapa_NPCs(NetIncomingMessage Dados)
    {
        // Lê os dados
        Listas.Mapa.Temp_NPC = new Listas.Estruturas.Mapa_NPCs[Dados.ReadInt16() + 1];
        for (byte i = 1; i <= Listas.Mapa.Temp_NPC.GetUpperBound(0); i++)
        {
            Listas.Mapa.Temp_NPC[i].X2 = 0;
            Listas.Mapa.Temp_NPC[i].Y2 = 0;
            Listas.Mapa.Temp_NPC[i].Índice = Dados.ReadInt16();
            Listas.Mapa.Temp_NPC[i].X = Dados.ReadByte();
            Listas.Mapa.Temp_NPC[i].Y = Dados.ReadByte();
            Listas.Mapa.Temp_NPC[i].Direção = (Game.Direções)Dados.ReadByte();

            // Vitais
            Listas.Mapa.Temp_NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
                Listas.Mapa.Temp_NPC[i].Vital[n] = Dados.ReadInt16();
        }
    }

    public static void Mapa_NPC(NetIncomingMessage Dados)
    {
        // Lê os dados
        byte i = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Índice = Dados.ReadInt16();
        Listas.Mapa.Temp_NPC[i].X = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Y = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Direção = (Game.Direções)Dados.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Listas.Mapa.Temp_NPC[i].Vital[n] = Dados.ReadInt16();
    }

    public static void Mapa_NPC_Movimento(NetIncomingMessage Dados)
    {
        // Lê os dados
        byte i = Dados.ReadByte();
        byte x = Listas.Mapa.Temp_NPC[i].X, y = Listas.Mapa.Temp_NPC[i].Y;
        Listas.Mapa.Temp_NPC[i].X2 = 0;
        Listas.Mapa.Temp_NPC[i].Y2 = 0;
        Listas.Mapa.Temp_NPC[i].X = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Y = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Direção = (Game.Direções)Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Movimento = (Game.Movimentos)Dados.ReadByte();

        // Posição exata do jogador
        if (x != Listas.Mapa.Temp_NPC[i].X || y != Listas.Mapa.Temp_NPC[i].Y)
            switch (Listas.Mapa.Temp_NPC[i].Direção)
            {
                case Game.Direções.Acima: Listas.Mapa.Temp_NPC[i].Y2 = Game.Grade; break;
                case Game.Direções.Abaixo: Listas.Mapa.Temp_NPC[i].Y2 = Game.Grade * -1; break;
                case Game.Direções.Direita: Listas.Mapa.Temp_NPC[i].X2 = Game.Grade * -1; break;
                case Game.Direções.Esquerda: Listas.Mapa.Temp_NPC[i].X2 = Game.Grade; break;
            }
    }

    public static void Mapa_NPC_Atacar(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte(), Vítima = Dados.ReadByte(), Vítima_Tipo = Dados.ReadByte();

        // Inicia o ataque
        Listas.Mapa.Temp_NPC[Índice].Atacando = true;
        Listas.Mapa.Temp_NPC[Índice].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Tipo == (byte)Game.Alvo.Jogador)
                Listas.Jogador[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Tipo == (byte)Game.Alvo.NPC)
                Listas.Mapa.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Mapa_NPC_Direção(NetIncomingMessage Dados)
    {
        // Define a direção de determinado NPC
        byte i = Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].Direção = (Game.Direções)Dados.ReadByte();
        Listas.Mapa.Temp_NPC[i].X2 = 0;
        Listas.Mapa.Temp_NPC[i].Y2 = 0;
    }

    public static void Mapa_NPC_Vitais(NetIncomingMessage Dados)
    {
        byte Índice = Dados.ReadByte();

        // Define os vitais de determinado NPC
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++)
            Listas.Mapa.Temp_NPC[Índice].Vital[n] = Dados.ReadInt16();
    }

    public static void Mapa_NPC_Morreu(NetIncomingMessage Dados)
    {
        byte i = Dados.ReadByte();

        // Limpa os dados do NPC
        Listas.Mapa.Temp_NPC[i].X2 = 0;
        Listas.Mapa.Temp_NPC[i].Y2 = 0;
        Listas.Mapa.Temp_NPC[i].Índice = 0;
        Listas.Mapa.Temp_NPC[i].X = 0;
        Listas.Mapa.Temp_NPC[i].Y = 0;
        Listas.Mapa.Temp_NPC[i].Vital = new short[(byte)Game.Vitais.Quantidade];
    }
}

partial class Gráficos
{
    public static void NPC(byte Índice)
    {
        int x2 = Listas.Mapa.Temp_NPC[Índice].X2, y2 = Listas.Mapa.Temp_NPC[Índice].Y2;
        byte Coluna = 0;
        bool Sofrendo = false;
        short Textura = Listas.NPC[Listas.Mapa.Temp_NPC[Índice].Índice].Textura;

        // Previni sobrecargas
        if (Textura <= 0 || Textura > Tex_Personagem.GetUpperBound(0)) return;

        // Define a animação
        if (Listas.Mapa.Temp_NPC[Índice].Atacando && Listas.Mapa.Temp_NPC[Índice].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Listas.Mapa.Temp_NPC[Índice].Animação;
            else if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Listas.Mapa.Temp_NPC[Índice].Animação;
            else if (y2 > 8 && y2 < Game.Grade) Coluna = Listas.Mapa.Temp_NPC[Índice].Animação;
            else if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Listas.Mapa.Temp_NPC[Índice].Animação;
        }

        // Demonstra que o personagem está sofrendo dano
        if (Listas.Mapa.Temp_NPC[Índice].Sofrendo > 0) Sofrendo = true;

        // Desenha o jogador
        int x = Listas.Mapa.Temp_NPC[Índice].X * Game.Grade + x2;
        int y = Listas.Mapa.Temp_NPC[Índice].Y * Game.Grade + y2;
        Personagem(Textura, new Point(Game.ConverterX(x), Game.ConverterY(y)), Listas.Mapa.Temp_NPC[Índice].Direção, Coluna, Sofrendo);
        NPC_Nome(Índice, x, y);
        NPC_Barras(Índice, x, y);
    }

    public static void NPC_Nome(byte Índice, int x, int y)
    {
        Point Posição = new Point(); SFML.Graphics.Color Cor;
        short NPC_Num = Listas.Mapa.Temp_NPC[Índice].Índice;
        int Nome_Tamanho = Ferramentas.MedirTexto_Largura(Listas.NPC[NPC_Num].Nome);
        Texture Textura = Tex_Personagem[Listas.NPC[NPC_Num].Textura];

        // Posição do texto
        Posição.X = x + TTamanho(Textura).Width / Game.Animação_Quantidade / 2 - Nome_Tamanho / 2;
        Posição.Y = y - TTamanho(Textura).Height / Game.Animação_Quantidade / 2;

        // Cor do texto
        switch ((Game.NPCs)Listas.NPC[NPC_Num].Tipo)
        {
            case Game.NPCs.Passivo: Cor = SFML.Graphics.Color.White; break;
            case Game.NPCs.Atacado: Cor = SFML.Graphics.Color.Red; break;
            case Game.NPCs.AoVer: Cor = new SFML.Graphics.Color(228, 120, 51); break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o texto
        Desenhar(Listas.NPC[NPC_Num].Nome, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }

    public static void NPC_Barras(byte Índice, int x, int y)
    {
        Listas.Estruturas.Mapa_NPCs NPC = Listas.Mapa.Temp_NPC[Índice];
        int Nome_Tamanho = Ferramentas.MedirTexto_Largura(Listas.NPC[NPC.Índice].Nome);
        Texture Textura = Tex_Personagem[Listas.NPC[NPC.Índice].Textura];
        short Valor = NPC.Vital[(byte)Game.Vitais.Vida];

        // Apenas se necessário
        if (Valor <= 0 || Valor >= Listas.NPC[NPC.Índice].Vital[(byte)Game.Vitais.Vida]) return;

        // Posição
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + TTamanho(Textura).Height / Game.Animação_Quantidade + 4);
        int Largura_Completa = TTamanho(Textura).Width / Game.Animação_Quantidade;
        int Largura = (Valor * Largura_Completa) / Listas.NPC[NPC.Índice].Vital[(byte)Game.Vitais.Vida];

        // Desenha a barra 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }
}