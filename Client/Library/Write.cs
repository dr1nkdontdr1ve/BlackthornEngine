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

    public static void Map(short Index)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Maps_Data.FullName + Index + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os Data
        Binário.Write(Lists.Map.Revisão);
        Binário.Write(Lists.Map.Name);
        Binário.Write(Lists.Map.Largura);
        Binário.Write(Lists.Map.Altura);
        Binário.Write(Lists.Map.Moral);
        Binário.Write(Lists.Map.Panorama);
        Binário.Write(Lists.Map.Música);
        Binário.Write(Lists.Map.Coloração);
        Binário.Write(Lists.Map.Clima.Type);
        Binário.Write(Lists.Map.Clima.Intensidade);
        Binário.Write(Lists.Map.Fumaça.Texture);
        Binário.Write(Lists.Map.Fumaça.VelocidadeX);
        Binário.Write(Lists.Map.Fumaça.VelocidadeY);
        Binário.Write(Lists.Map.Fumaça.Transparência);

        // Ligação
        for (short i = 0; i <= (short)Game.Direções.Amount - 1; i++)
            Binário.Write(Lists.Map.Ligação[i]);

        // Azulejos
        Binário.Write((byte)Lists.Map.Azulejo[0, 0].Data.GetUpperBound(1));
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
                for (byte c = 0; c <= (byte)global::Map.Camadas.Amount - 1; c++)
                    for (byte q = 0; q <= Lists.Map.Azulejo[x, y].Data.GetUpperBound(1); q++)
                    {
                        Binário.Write(Lists.Map.Azulejo[x, y].Data[c, q].x);
                        Binário.Write(Lists.Map.Azulejo[x, y].Data[c, q].y);
                        Binário.Write(Lists.Map.Azulejo[x, y].Data[c, q].Azulejo);
                        Binário.Write(Lists.Map.Azulejo[x, y].Data[c, q].Automático);
                    }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
            {
                Binário.Write((byte)Lists.Map.Azulejo[x, y].Atributo);
                for (byte i = 0; i <= (byte)Game.Direções.Amount - 1; i++)
                    Binário.Write(Lists.Map.Azulejo[x, y].Bloqueio[i]);
            }

        // Luzes
        Binário.Write(Lists.Map.Luz.GetUpperBound(0));
        if (Lists.Map.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Luz.GetUpperBound(0); i++)
            {
                Binário.Write(Lists.Map.Luz[i].X);
                Binário.Write(Lists.Map.Luz[i].Y);
                Binário.Write(Lists.Map.Luz[i].Largura);
                Binário.Write(Lists.Map.Luz[i].Altura);
            }

        // NPCs
        Binário.Write(Lists.Map.NPC.GetUpperBound(0));
        if (Lists.Map.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Map.NPC.GetUpperBound(0) ; i++)
                Binário.Write(Lists.Map.NPC[i]);

        // Fecha o sistema
        Binário.Dispose();
    }
}