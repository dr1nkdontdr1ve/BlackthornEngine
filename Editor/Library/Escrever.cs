using System.IO;
using System.Drawing;

class Escrever
{
    public static void Opções()
    {
        // Cria um sistema binário para a manipulação dos dados
        BinaryWriter Binário = new BinaryWriter(Diretórios.Opções.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Opções.Diretório_Cliente);
        Binário.Write(Listas.Opções.Diretório_Servidor);
        Binário.Write(Listas.Opções.Pre_Mapa_Grades);
        Binário.Write(Listas.Opções.Pre_Mapa_Visualização);
        Binário.Write(Listas.Opções.Pre_Mapa_Áudio);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Cliente_Dados()
    {
        // Cria um sistema binário para a manipulação dos dados
        BinaryWriter Binário = new BinaryWriter(Diretórios.Cliente_Dados.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Cliente_Dados.Num_Botões);
        Binário.Write(Listas.Cliente_Dados.Num_Digitalizadores);
        Binário.Write(Listas.Cliente_Dados.Num_Paineis);
        Binário.Write(Listas.Cliente_Dados.Num_Marcadores);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Servidor_Dados()
    {
        // Cria um sistema binário para a manipulação dos dados
        BinaryWriter Binário = new BinaryWriter(Diretórios.Servidor_Dados.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Servidor_Dados.Game_Nome);
        Binário.Write(Listas.Servidor_Dados.Mensagem);
        Binário.Write(Listas.Servidor_Dados.Porta);
        Binário.Write(Listas.Servidor_Dados.Máx_Jogadores);
        Binário.Write(Listas.Servidor_Dados.Máx_Personagens);
        Binário.Write(Listas.Servidor_Dados.Num_Classes);
        Binário.Write(Listas.Servidor_Dados.Num_Azulejos);
        Binário.Write(Listas.Servidor_Dados.Num_Mapas);
        Binário.Write(Listas.Servidor_Dados.Num_NPCs);
        Binário.Write(Listas.Servidor_Dados.Num_Itens);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Classes()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Classe.GetUpperBound(0); Índice++)
            Classe(Índice);
    }

    public static void Classe(byte Índice)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Classes_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Classe[Índice].Nome);
        Binário.Write(Listas.Classe[Índice].Textura_Masculina);
        Binário.Write(Listas.Classe[Índice].Textura_Feminina);
        Binário.Write(Listas.Classe[Índice].Aparecer_Mapa);
        Binário.Write(Listas.Classe[Índice].Aparecer_Direção);
        Binário.Write(Listas.Classe[Índice].Aparecer_X);
        Binário.Write(Listas.Classe[Índice].Aparecer_Y);
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++) Binário.Write(Listas.Classe[Índice].Vital[i]);
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++) Binário.Write(Listas.Classe[Índice].Atributo[i]);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void NPCs()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.NPC.GetUpperBound(0); Índice++)
            NPC(Índice);
    }

    public static void NPC(byte Índice)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.NPCs_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.NPC[Índice].Nome);
        Binário.Write(Listas.NPC[Índice].Textura);
        Binário.Write(Listas.NPC[Índice].Agressividade);
        Binário.Write(Listas.NPC[Índice].Aparecimento);
        Binário.Write(Listas.NPC[Índice].Visão);
        Binário.Write(Listas.NPC[Índice].Experiência);
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++) Binário.Write(Listas.NPC[Índice].Vital[i]);
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++) Binário.Write(Listas.NPC[Índice].Atributo[i]);
        for (byte i = 0; i <= Globais.Máx_NPC_Queda - 1; i++)
        {
            Binário.Write(Listas.NPC[Índice].Queda[i].Item_Num);
            Binário.Write(Listas.NPC[Índice].Queda[i].Quantidade);
            Binário.Write(Listas.NPC[Índice].Queda[i].Chance);
        }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Itens()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Item.GetUpperBound(0); Índice++)
            Item(Índice);
    }

    public static void Item(byte Índice)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Itens_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Item[Índice].Nome);
        Binário.Write(Listas.Item[Índice].Descrição);
        Binário.Write(Listas.Item[Índice].Textura);
        Binário.Write(Listas.Item[Índice].Tipo);
        Binário.Write(Listas.Item[Índice].Preço);
        Binário.Write(Listas.Item[Índice].Empilhável);
        Binário.Write(Listas.Item[Índice].NãoDropável);
        Binário.Write(Listas.Item[Índice].Req_Level);
        Binário.Write(Listas.Item[Índice].Req_Classe);
        Binário.Write(Listas.Item[Índice].Poção_Experiência);
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++) Binário.Write(Listas.Item[Índice].Poção_Vital[i]);
        Binário.Write(Listas.Item[Índice].Equip_Tipo);
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++) Binário.Write(Listas.Item[Índice].Equip_Atributo[i]);
        Binário.Write(Listas.Item[Índice].Arma_Dano);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Azulejos()
    {
        // Escreve os dados
        for (byte i = 1; i <= Listas.Azulejo.GetUpperBound(0); i++)
            Azulejo(i);
    }

    public static void Azulejo(byte Índice)
    {
        Size Tamanho = new Size(Listas.Azulejo[Índice].Azulejo.GetUpperBound(0), Listas.Azulejo[Índice].Azulejo.GetUpperBound(1));

        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Azulejos_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Dados básicos
        Binário.Write((byte)Tamanho.Width);
        Binário.Write((byte)Tamanho.Height);

        // Gerais
        for (byte x = 0; x <= Tamanho.Width; x++)
            for (byte y = 0; y <= Tamanho.Height; y++)
            {
                Binário.Write(Listas.Azulejo[Índice].Azulejo[x, y].Atributo);

                // Bloqueio direcional
                for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                    Binário.Write(Listas.Azulejo[Índice].Azulejo[x, y].Bloqueio[i]);
            }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Mapas()
    {
        // Escreve os dados
        for (short Índice = 1; Índice <= Listas.Mapa.GetUpperBound(0); Índice++)
            Mapa(Índice);
    }

    public static void Mapa(short Índice)
    {
        // Cria um arquivo temporário
        Listas.Estruturas.Mapas Dados = Listas.Mapa[Índice];
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write((short)(Dados.Revisão + 1));
        Binário.Write(Dados.Nome);
        Binário.Write(Dados.Largura);
        Binário.Write(Dados.Altura);
        Binário.Write(Dados.Moral);
        Binário.Write(Dados.Panorama);
        Binário.Write(Dados.Música);
        Binário.Write(Dados.Coloração);
        Binário.Write(Dados.Clima.Tipo);
        Binário.Write(Dados.Clima.Intensidade);
        Binário.Write(Dados.Fumaça.Textura);
        Binário.Write(Dados.Fumaça.VelocidadeX);
        Binário.Write(Dados.Fumaça.VelocidadeY);
        Binário.Write(Dados.Fumaça.Transparência);
        Binário.Write(Dados.LuzGlobal);
        Binário.Write(Dados.Iluminação);

        // Ligações
        for (short i = 0; i <= (short)Globais.Direções.Quantidade - 1; i++)
            Binário.Write(Dados.Ligação[i]);

        // Camadas
        Binário.Write((byte)(Dados.Camada.Count - 1));
        for (byte i = 0; i <= Dados.Camada.Count - 1; i++)
        {
            Binário.Write(Dados.Camada[i].Nome);
            Binário.Write(Dados.Camada[i].Tipo);

            // Azulejos
            for (byte x = 0; x <= Dados.Largura; x++)
                for (byte y = 0; y <= Dados.Altura; y++)
                {
                    Binário.Write(Dados.Camada[i].Azulejo[x, y].x);
                    Binário.Write(Dados.Camada[i].Azulejo[x, y].y);
                    Binário.Write(Dados.Camada[i].Azulejo[x, y].Azulejo);
                    Binário.Write(Dados.Camada[i].Azulejo[x, y].Automático);
                }
        }


        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
            {
                Binário.Write(Dados.Azulejo[x, y].Atributo);
                Binário.Write(Dados.Azulejo[x, y].Dado_1);
                Binário.Write(Dados.Azulejo[x, y].Dado_2);
                Binário.Write(Dados.Azulejo[x, y].Dado_3);
                Binário.Write(Dados.Azulejo[x, y].Dado_4);
                Binário.Write(Dados.Azulejo[x, y].Zona);

                // Bloqueio direcional
                for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                    Binário.Write(Dados.Azulejo[x, y].Bloqueio[i]);
            }

        // Luzes
        Binário.Write((byte)Dados.Luz.Count);
        if (Dados.Luz.Count > 0)
            for (byte i = 0; i <= Dados.Luz.Count - 1; i++)
            {
                Binário.Write(Dados.Luz[i].X);
                Binário.Write(Dados.Luz[i].Y);
                Binário.Write(Dados.Luz[i].Largura);
                Binário.Write(Dados.Luz[i].Altura);
            }

        // Itens
        Binário.Write((byte)Dados.NPC.Count);
        if (Dados.NPC.Count > 0)
            for (byte i = 0; i <= Dados.NPC.Count - 1; i++)
            {
                Binário.Write(Dados.NPC[i].Índice);
                Binário.Write(Dados.NPC[i].Zona);
                Binário.Write(Dados.NPC[i].Aparecer);
                Binário.Write(Dados.NPC[i].X);
                Binário.Write(Dados.NPC[i].Y);
            }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Ferramentas()
    {
        // Escreve os dados de todas as ferramentas
        Botões_Dados();
        Digitalizadores_Dados();
        Paineis_Dados();
        Marcadores_Dados();
    }

    public static void Botões_Dados()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Botão.GetUpperBound(0); Índice++)
            Botão_Dados(Índice);
    }

    public static void Botão_Dados(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Botões_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Botão[Índice].Geral.Nome);
        Binário.Write(Listas.Botão[Índice].Geral.Posição.X);
        Binário.Write(Listas.Botão[Índice].Geral.Posição.Y);
        Binário.Write(Listas.Botão[Índice].Geral.Visível);
        Binário.Write(Listas.Botão[Índice].Textura);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Digitalizadores_Dados()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Digitalizador.GetUpperBound(0); Índice++)
            Digitalizador_Dados(Índice);
    }

    public static void Digitalizador_Dados(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Digitalizadores_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Digitalizador[Índice].Geral.Nome);
        Binário.Write(Listas.Digitalizador[Índice].Geral.Posição.X);
        Binário.Write(Listas.Digitalizador[Índice].Geral.Posição.Y);
        Binário.Write(Listas.Digitalizador[Índice].Geral.Visível);
        Binário.Write(Listas.Digitalizador[Índice].Máx_Carácteres);
        Binário.Write(Listas.Digitalizador[Índice].Largura);
        Binário.Write(Listas.Digitalizador[Índice].Senha);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Paineis_Dados()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Painel.GetUpperBound(0); Índice++)
            Painel_Dados(Índice);
    }

    public static void Painel_Dados(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Paineis_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Painel[Índice].Geral.Nome);
        Binário.Write(Listas.Painel[Índice].Geral.Posição.X);
        Binário.Write(Listas.Painel[Índice].Geral.Posição.Y);
        Binário.Write(Listas.Painel[Índice].Geral.Visível);
        Binário.Write(Listas.Painel[Índice].Textura);

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Marcadores_Dados()
    {
        // Escreve os dados
        for (byte Índice = 1; Índice <= Listas.Marcador.GetUpperBound(0); Índice++)
            Marcador_Dados(Índice);
    }

    public static void Marcador_Dados(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Marcadores_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Marcador[Índice].Geral.Nome);
        Binário.Write(Listas.Marcador[Índice].Geral.Posição.X);
        Binário.Write(Listas.Marcador[Índice].Geral.Posição.Y);
        Binário.Write(Listas.Marcador[Índice].Geral.Visível);
        Binário.Write(Listas.Marcador[Índice].Texto);
        Binário.Write(Listas.Marcador[Índice].Estado);

        // Fecha o sistema
        Binário.Dispose();
    }
}