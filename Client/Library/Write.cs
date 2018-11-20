using System.IO;

class Write
{
    public static void Data()
    {
        // Save All Data
        Options();
    }

    public static void Options()
    {
        // Create a Timer file
        BinaryWriter Arquivo = new BinaryWriter(File.OpenWrite(Directories.Options.FullName));

        // Load all Options
        Arquivo.Write(Lists.Options.Jogo_Name);
        Arquivo.Write(Lists.Options.SalvarUsuário);
        Arquivo.Write(Lists.Options.Sons);
        Arquivo.Write(Lists.Options.Músicas);
        Arquivo.Write(Lists.Options.User);

        // Closes the file
        Arquivo.Dispose();
    }

    public static void Map(short Index)
    {
        // Cria um arquivo Timerário
        FileInfo Arquivo = new FileInfo(Directories.Maps_Data.FullName + Index + Directories.Format);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os Data
        Binário.Write(Lists.Map.Review);
        Binário.Write(Lists.Map.Name);
        Binário.Write(Lists.Map.Width);
        Binário.Write(Lists.Map.Height);
        Binário.Write(Lists.Map.Moral);
        Binário.Write(Lists.Map.Panorama);
        Binário.Write(Lists.Map.Music);
        Binário.Write(Lists.Map.Coloração);
        Binário.Write(Lists.Map.Climate.Type);
        Binário.Write(Lists.Map.Climate.Intensity);
        Binário.Write(Lists.Map.Smoke.Texture);
        Binário.Write(Lists.Map.Smoke.VelocityX);
        Binário.Write(Lists.Map.Smoke.VelocityY);
        Binário.Write(Lists.Map.Smoke.Transparency);

        // Ligação
        for (short i = 0; i <= (short)Jogo.Location.Amount - 1; i++)
            Binário.Write(Lists.Map.Ligação[i]);

        // Tiles
        Binário.Write((byte)Lists.Map.Tile[0, 0].Data.GetUpperBound(1));
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
                for (byte c = 0; c <= (byte)global::Map.Layers.Amount - 1; c++)
                    for (byte q = 0; q <= Lists.Map.Tile[x, y].Data.GetUpperBound(1); q++)
                    {
                        Binário.Write(Lists.Map.Tile[x, y].Data[c, q].x);
                        Binário.Write(Lists.Map.Tile[x, y].Data[c, q].y);
                        Binário.Write(Lists.Map.Tile[x, y].Data[c, q].Tile);
                        Binário.Write(Lists.Map.Tile[x, y].Data[c, q].Automático);
                    }

        // Data específicos dos Tiles
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
            {
                Binário.Write((byte)Lists.Map.Tile[x, y].Attribute);
                for (byte i = 0; i <= (byte)Jogo.Location.Amount - 1; i++)
                    Binário.Write(Lists.Map.Tile[x, y].Block[i]);
            }

        // Lightes
        Binário.Write(Lists.Map.Light.GetUpperBound(0));
        if (Lists.Map.Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Light.GetUpperBound(0); i++)
            {
                Binário.Write(Lists.Map.Light[i].X);
                Binário.Write(Lists.Map.Light[i].Y);
                Binário.Write(Lists.Map.Light[i].Width);
                Binário.Write(Lists.Map.Light[i].Height);
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