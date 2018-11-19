using System.IO;

class Read
{
    public static void Data()
    {
        // Carrega todos os Data
        Client_Data();
        Buttons_Data();
        Scanners_Data();
        Panels_Data();
        Markers_Data();
        Options();
    }

    public static void Options()
    {
        // Cria o arquivo se ele não existir
        if (!Directories.Options.Exists)
            Clean.Options();
        else
        {
            // Create a Timer file
            BinaryReader Arquivo = new BinaryReader(File.OpenRead(Directories.Options.FullName));

            // Load the Data
            Lists.Options.Game_Name = Arquivo.ReadString();
            Lists.Options.SaveUser = Arquivo.ReadBoolean();
            Lists.Options.Sons = Arquivo.ReadBoolean();
            Lists.Options.Músicas = Arquivo.ReadBoolean();
            Lists.Options.User = Arquivo.ReadString();

            // Download the file
            Arquivo.Dispose();
        }

        // Adds the Data to the cache
        Markers.Locate("Sons").State = Lists.Options.Sons;
        Markers.Locate("Músicas").State = Lists.Options.Músicas;
        Markers.Locate("SaveUser").State = Lists.Options.SaveUser;
        if (Lists.Options.SaveUser) Scanners.Locate("Connect_User").Text = Lists.Options.User;
    }

    public static void Client_Data()
    {
        // Cria um sistema Binary para a manipulação dos Data
        BinaryReader Binary = new BinaryReader(Directories.Client_Data.OpenRead());

        // Lê os Data
        Lists.Client_Data.Num_Buttons = Binary.ReadByte();
        Lists.Client_Data.Num_Scanners = Binary.ReadByte();
        Lists.Client_Data.Num_Panels = Binary.ReadByte();
        Lists.Client_Data.Num_Markers = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Buttons_Data()
    {
        Buttons.List = new Buttons.Structure[Lists.Client_Data.Num_Buttons + 1];

        // Lê os Data
        for (byte i = 1; i <= Buttons.List.GetUpperBound(0); i++)
            Button_Data(i);
    }

    public static void Button_Data(byte Index)
    {
        // Limpa os valores
        Clean.Button(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Directories.Buttons_Data.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Buttons.List[Index].General.Name = Binary.ReadString();
        Buttons.List[Index].General.Position.X = Binary.ReadInt32();
        Buttons.List[Index].General.Position.Y = Binary.ReadInt32();
        Buttons.List[Index].General.Visible = Binary.ReadBoolean();
        Buttons.List[Index].Texture = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Scanners_Data()
    {
        Scanners.List = new Scanners.Structure[Lists.Client_Data.Num_Scanners + 1];

        // Lê os Data
        for (byte i = 1; i <= Scanners.List.GetUpperBound(0); i++)
            Scanner_Data(i);
    }

    public static void Scanner_Data(byte Index)
    {
        // Limpa os valores
        Clean.Scanner(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Directories.Scanners_Data.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Scanners.List[Index].General.Name = Binary.ReadString();
        Scanners.List[Index].General.Position.X = Binary.ReadInt32();
        Scanners.List[Index].General.Position.Y = Binary.ReadInt32();
        Scanners.List[Index].General.Visible = Binary.ReadBoolean();
        Scanners.List[Index].Max_Carácteres = Binary.ReadInt16();
        Scanners.List[Index].Width = Binary.ReadInt16();
        Scanners.List[Index].Senha = Binary.ReadBoolean();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Panels_Data()
    {
        Panels.List = new Panels.Structure[Lists.Client_Data.Num_Panels + 1];

        // Lê os Data
        for (byte i = 1; i <= Panels.List.GetUpperBound(0); i++)
            Panel_Data(i);
    }

    public static void Panel_Data(byte Index)
    {
        // Limpa os valores
        Clean.Panel(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Directories.Panels_Data.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Carrega os Data
        Panels.List[Index].General.Name = Binary.ReadString();
        Panels.List[Index].General.Position.X = Binary.ReadInt32();
        Panels.List[Index].General.Position.Y = Binary.ReadInt32();
        Panels.List[Index].General.Visible = Binary.ReadBoolean();
        Panels.List[Index].Texture = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Markers_Data()
    {
        Markers.List = new Markers.Structure[Lists.Client_Data.Num_Markers + 1];

        // Lê os Data
        for (byte i = 1; i <= Markers.List.GetUpperBound(0); i++)
            Marker_Data(i);
    }

    public static void Marker_Data(byte Index)
    {
        // Limpa os valores
        Clean.Marker(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Directories.Markers_Data.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Carrega os Data
        Markers.List[Index].General.Name = Binary.ReadString();
        Markers.List[Index].General.Position.X = Binary.ReadInt32();
        Markers.List[Index].General.Position.Y = Binary.ReadInt32();
        Markers.List[Index].General.Visible = Binary.ReadBoolean();
        Markers.List[Index].Text = Binary.ReadString();
        Markers.List[Index].State = Binary.ReadBoolean();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Map(int Index)
    {
        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Directories.Maps_Data.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Lists.Map.Review = Binary.ReadInt16();
        Lists.Map.Name = Binary.ReadString();
        Lists.Map.Width = Binary.ReadByte();
        Lists.Map.Height = Binary.ReadByte();
        Lists.Map.Moral = Binary.ReadByte();
        Lists.Map.Panorama = Binary.ReadByte();
        Lists.Map.Music = Binary.ReadByte();
        Lists.Map.Coloração = Binary.ReadInt32();
        Lists.Map.Climate.Type = Binary.ReadByte();
        Lists.Map.Climate.Intensity = Binary.ReadByte();
        Lists.Map.Smoke.Texture = Binary.ReadByte();
        Lists.Map.Smoke.VelocityX = Binary.ReadSByte();
        Lists.Map.Smoke.VelocityY = Binary.ReadSByte();
        Lists.Map.Smoke.Transparency = Binary.ReadByte();

        // Redimensiona as ligações
        Lists.Map.Ligação = new short[(byte)Game.Location.Amount];
        for (short i = 0; i <= (short)Game.Location.Amount - 1; i++)
            Lists.Map.Ligação[i] = Binary.ReadInt16();

        // Redimensiona os Tiles 
        Lists.Map.Tile = new Lists.Structures.Tile[Lists.Map.Width + 1, Lists.Map.Height + 1];

        // Lê os Data
        byte NumLayers = Binary.ReadByte();
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
            {
                // Redimensiona os Data dos Tiles
                Lists.Map.Tile[x, y].Data = new Lists.Structures.Tile_Data[(byte)global::Map.Layers.Amount, NumLayers + 1];

                for (byte c = 0; c <= (byte)global::Map.Layers.Amount - 1; c++)
                    for (byte q = 0; q <= NumLayers; q++)
                    {
                        Lists.Map.Tile[x, y].Data[c, q].x = Binary.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].y = Binary.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].Tile = Binary.ReadByte();
                        Lists.Map.Tile[x, y].Data[c, q].Automático = Binary.ReadBoolean();
                        Lists.Map.Tile[x, y].Data[c, q].Mini = new System.Drawing.Point[4];
                    }
            }

        // Data específicos dos Tiles
        for (byte x = 0; x <= Lists.Map.Width; x++)
            for (byte y = 0; y <= Lists.Map.Height; y++)
            {
                Lists.Map.Tile[x, y].Attribute = Binary.ReadByte();
                Lists.Map.Tile[x, y].Block = new bool[(byte)Game.Location.Amount];
                for (byte i = 0; i <= (byte)Game.Location.Amount - 1; i++)
                    Lists.Map.Tile[x, y].Block[i] = Binary.ReadBoolean();
            }

        // Lightes
        Lists.Map.Light = new Lists.Structures.Light[Binary.ReadInt32() + 1];
        if (Lists.Map.Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Light.GetUpperBound(0); i++)
            {
                Lists.Map.Light[i].X = Binary.ReadByte();
                Lists.Map.Light[i].Y = Binary.ReadByte();
                Lists.Map.Light[i].Width = Binary.ReadByte();
                Lists.Map.Light[i].Height = Binary.ReadByte();
            }

        // NPCs
        Lists.Map.NPC = new short[Binary.ReadInt32() + 1];
        Lists.Map.Temp_NPC = new Lists.Structures.Map_NPCs  [Lists.Map.NPC.GetUpperBound(0) + 1];
        if (Lists.Map.NPC.GetUpperBound(0) > 0)
            for (byte i =1; i <= Lists.Map.NPC.GetUpperBound(0); i++)
            {
                Lists.Map.NPC[i] = Binary.ReadInt16();
                Lists.Map.Temp_NPC[i].Index = Lists.Map.NPC[i];
            }

        // Fecha o sistema
        Binary.Dispose();

        // Redimensiona as Particles do Climate
        global::Map.Climate_Ajustar();
        global::Map.AutoCriação.Update();
    }
}