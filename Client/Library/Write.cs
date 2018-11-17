using System.IO;

class Write
{
    public static void Data()
    {
        // Salva todos os Data
        Opções();
    }

    public static void Opções()
    {
        // Cria um arquivo temporário
        BinaryWriter Arquivo = new BinaryWriter(File.OpenWrite(Diretórios.Opções.FullName));

        // Carrega todas as opções
        Arquivo.Write(Lists.Opções.Game_Name);
        Arquivo.Write(Lists.Opções.SalvarUsuário);
        Arquivo.Write(Lists.Opções.Sons);
        Arquivo.Write(Lists.Opções.Músicas);
        Arquivo.Write(Lists.Opções.Usuário);

        // Fecha o arquivo
        Arquivo.Dispose();
    }

    public static void Mapa(short Índice)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas_Data.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os Data
        Binário.Write(Lists.Mapa.Revisão);
        Binário.Write(Lists.Mapa.Name);
        Binário.Write(Lists.Mapa.Largura);
        Binário.Write(Lists.Mapa.Altura);
        Binário.Write(Lists.Mapa.Moral);
        Binário.Write(Lists.Mapa.Panorama);
        Binário.Write(Lists.Mapa.Música);
        Binário.Write(Lists.Mapa.Coloração);
        Binário.Write(Lists.Mapa.Clima.Tipo);
        Binário.Write(Lists.Mapa.Clima.Intensidade);
        Binário.Write(Lists.Mapa.Fumaça.Textura);
        Binário.Write(Lists.Mapa.Fumaça.VelocidadeX);
        Binário.Write(Lists.Mapa.Fumaça.VelocidadeY);
        Binário.Write(Lists.Mapa.Fumaça.Transparência);

        // Ligação
        for (short i = 0; i <= (short)Game.Direções.Quantidade - 1; i++)
            Binário.Write(Lists.Mapa.Ligação[i]);

        // Azulejos
        Binário.Write((byte)Lists.Mapa.Azulejo[0, 0].Data.GetUpperBound(1));
        for (byte x = 0; x <= Lists.Mapa.Largura; x++)
            for (byte y = 0; y <= Lists.Mapa.Altura; y++)
                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= Lists.Mapa.Azulejo[x, y].Data.GetUpperBound(1); q++)
                    {
                        Binário.Write(Lists.Mapa.Azulejo[x, y].Data[c, q].x);
                        Binário.Write(Lists.Mapa.Azulejo[x, y].Data[c, q].y);
                        Binário.Write(Lists.Mapa.Azulejo[x, y].Data[c, q].Azulejo);
                        Binário.Write(Lists.Mapa.Azulejo[x, y].Data[c, q].Automático);
                    }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Mapa.Largura; x++)
            for (byte y = 0; y <= Lists.Mapa.Altura; y++)
            {
                Binário.Write((byte)Lists.Mapa.Azulejo[x, y].Atributo);
                for (byte i = 0; i <= (byte)Game.Direções.Quantidade - 1; i++)
                    Binário.Write(Lists.Mapa.Azulejo[x, y].Bloqueio[i]);
            }

        // Luzes
        Binário.Write(Lists.Mapa.Luz.GetUpperBound(0));
        if (Lists.Mapa.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Mapa.Luz.GetUpperBound(0); i++)
            {
                Binário.Write(Lists.Mapa.Luz[i].X);
                Binário.Write(Lists.Mapa.Luz[i].Y);
                Binário.Write(Lists.Mapa.Luz[i].Largura);
                Binário.Write(Lists.Mapa.Luz[i].Altura);
            }

        // NPCs
        Binário.Write(Lists.Mapa.NPC.GetUpperBound(0));
        if (Lists.Mapa.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Mapa.NPC.GetUpperBound(0) ; i++)
                Binário.Write(Lists.Mapa.NPC[i]);

        // Fecha o sistema
        Binário.Dispose();
    }
}