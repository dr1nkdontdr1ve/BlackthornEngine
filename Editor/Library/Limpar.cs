using System;
using System.Drawing;
using System.Collections.Generic;

class Limpar
{
    public static void Opções()
    {
        // Defini os dados das opções
        Listas.Opções.Diretório_Cliente = string.Empty;
        Listas.Opções.Diretório_Servidor = string.Empty;

        // Salva o que foi modificado
        Escrever.Opções();
    }

    public static void Cliente_Dados()
    {
        // Defini os dados das opções
        Listas.Cliente_Dados.Num_Botões = 1;
        Listas.Cliente_Dados.Num_Digitalizadores = 1;
        Listas.Cliente_Dados.Num_Paineis = 1;
        Listas.Cliente_Dados.Num_Marcadores = 1;
    }

    public static void Servidor_Dados()
    {
        // Defini os dados das opções
        Listas.Servidor_Dados.Game_Nome = "CryBits";
        Listas.Servidor_Dados.Mensagem = "Bem vindos à CryBits.";
        Listas.Servidor_Dados.Porta = 7001;
        Listas.Servidor_Dados.Máx_Jogadores = 15;
        Listas.Servidor_Dados.Máx_Personagens = 3;
        Listas.Servidor_Dados.Num_Classes = 1;
        Listas.Servidor_Dados.Num_Mapas = 1;
        Listas.Servidor_Dados.Num_Itens = 1;
    }

    public static void Botão(byte Índice)
    {
        // Limpa a estrutura
        Listas.Botão[Índice] = new Listas.Estruturas.Botões();
        Listas.Botão[Índice].Geral = new Listas.Estruturas.Ferramentas();

        // Reseta os valores
        Listas.Botão[Índice].Geral.Nome = string.Empty;
    }

    public static void Digitalizador(byte Índice)
    {
        // Limpa a estrutura
        Listas.Digitalizador[Índice] = new Listas.Estruturas.Digitalizadores();
        Listas.Digitalizador[Índice].Geral = new Listas.Estruturas.Ferramentas();

        // Reseta os valores
        Listas.Digitalizador[Índice].Geral.Nome = string.Empty;
    }

    public static void Painel(byte Índice)
    {
        // Limpa a estrutura
        Listas.Painel[Índice] = new Listas.Estruturas.Paineis();
        Listas.Painel[Índice].Geral = new Listas.Estruturas.Ferramentas();

        // Reseta os valores
        Listas.Painel[Índice].Geral.Nome = string.Empty;
    }

    public static void Marcador(byte Índice)
    {
        // Limpa a estrutura
        Listas.Marcador[Índice] = new Listas.Estruturas.Marcadores();
        Listas.Marcador[Índice].Geral = new Listas.Estruturas.Ferramentas();

        // Reseta os valores
        Listas.Marcador[Índice].Geral.Nome = string.Empty;
        Listas.Marcador[Índice].Texto = string.Empty;
    }

    public static void Classe(byte Índice)
    {
        // Reseta os valores
        Listas.Classe[Índice] = new Listas.Estruturas.Classes();
        Listas.Classe[Índice].Nome = string.Empty;
        Listas.Classe[Índice].Vital = new short[(byte)Globais.Vitais.Quantidade];
        Listas.Classe[Índice].Atributo = new short[(byte)Globais.Atributos.Quantidade];
        Listas.Classe[Índice].Aparecer_Mapa = 1;
    }

    public static void NPC(byte Índice)
    {
        // Reseta os valores
        Listas.NPC[Índice] = new Listas.Estruturas.NPCs();
        Listas.NPC[Índice].Nome = string.Empty;
        Listas.NPC[Índice].Vital = new short[(byte)Globais.Vitais.Quantidade];
        Listas.NPC[Índice].Atributo = new short[(byte)Globais.Atributos.Quantidade];
        Listas.NPC[Índice].Queda = new Listas.Estruturas.NPC_Queda[Globais.Máx_NPC_Queda];
        for (byte i = 0; i <= Globais.Máx_NPC_Queda - 1; i++)
        {
            Listas.NPC[Índice].Queda[i].Chance = 100;
            Listas.NPC[Índice].Queda[i].Quantidade = 1;
        }
    }

    public static void Item(byte Índice)
    {
        // Reseta os valores
        Listas.Item[Índice] = new Listas.Estruturas.Itens();
        Listas.Item[Índice].Nome = string.Empty;
        Listas.Item[Índice].Descrição = string.Empty;
        Listas.Item[Índice].Poção_Vital = new short[(byte)Globais.Vitais.Quantidade];
        Listas.Item[Índice].Equip_Atributo = new short[(byte)Globais.Atributos.Quantidade];
    }

    public static void Mapa(short Índice)
    {
        // Reseta 
        Listas.Mapa[Índice] = new Listas.Estruturas.Mapas();
        Listas.Mapa[Índice].Nome = string.Empty;
        Listas.Mapa[Índice].Largura = Globais.Min_Mapa_Largura;
        Listas.Mapa[Índice].Altura = Globais.Min_Mapa_Altura;
        Listas.Mapa[Índice].Coloração = -1;
        Listas.Mapa[Índice].Fumaça.Transparência = 255;
        Listas.Mapa[Índice].Iluminação = 100;

        // Redimensiona 
        Listas.Mapa[Índice].Ligação = new short[(byte)Globais.Direções.Quantidade];
        Listas.Mapa[Índice].Luz = new List<Listas.Estruturas.Luz_Estrutura>();
        Listas.Mapa[Índice].Camada = new List<Listas.Estruturas.Camada>();
        Listas.Mapa[Índice].Camada.Add(new Listas.Estruturas.Camada());
        Listas.Mapa[Índice].Camada[0].Nome = "Chão";
        Mapa_Camadas(Índice);
        Listas.Mapa[Índice].Azulejo = new Listas.Estruturas.Mapas_Azulejo_Dados[Listas.Mapa[Índice].Largura + 1, Listas.Mapa[Índice].Altura + 1];
        Listas.Mapa[Índice].NPC = new List<Listas.Estruturas.Mapa_NPC>();

        // Redimensiona os bloqueios
        for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
                Listas.Mapa[Índice].Azulejo[x, y].Bloqueio = new bool[(byte)Globais.Direções.Quantidade];
    }

    public static void Mapa_Camadas(short Índice)
    {
        for (byte c = 0; c <= Listas.Mapa[Índice].Camada.Count - 1; c++)
        {
            // Redimensiona os azulejos
            Listas.Mapa[Índice].Camada[c].Azulejo = new Listas.Estruturas.Azulejo_Dados[Listas.Mapa[Índice].Largura + 1, Listas.Mapa[Índice].Altura + 1];
            for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
                for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
                    Listas.Mapa[Índice].Camada[c].Azulejo[x, y].Mini = new Point[4];
        }
    }

    public static void Azulejo(byte Índice)
    {
        Size Textura_Tamanho = Gráficos.TTamanho(Gráficos.Tex_Azulejo[Índice]);
        Size Tamanho = new Size(Textura_Tamanho.Width / Globais.Grade - 1, Textura_Tamanho.Height / Globais.Grade - 1);

        // Redimensiona os valores
        Listas.Azulejo[Índice].Azulejo = new Listas.Estruturas.Azulejos_Azulejo_Dados[Tamanho.Width + 1, Tamanho.Height + 1];

        for (byte x = 0; x <= Tamanho.Width; x++)
            for (byte y = 0; y <= Tamanho.Height; y++)
                Listas.Azulejo[Índice].Azulejo[x, y].Bloqueio = new bool[(byte)Globais.Direções.Quantidade];
    }
}