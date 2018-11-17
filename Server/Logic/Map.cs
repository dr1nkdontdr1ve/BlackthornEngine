using System;
using System.IO;

class Map
{
    ////////////////
    // Numerações //
    ////////////////
    public enum Morais
    {
        Pacific,
        Dangerous,
        Amount
    }

    public enum Layers
    {
        Floor,
        Roof,
        Amount
    }

    public enum Attributes
    {
        Nenhum,
        Block,
        Teleportation,
        Item,
        Amount
    }

    public static void Logic()
    {
        for (byte i = 1; i <= Lists.Map.GetUpperBound(0); i++)
        {
            // Não é necessário fazer todos os cálculos se não houver nenhum jogador no Map
            if (!ThereIsPlayers(i)) continue;

            // Lógica dos NPCs
            NPC.Logic(i);

            // Faz reaparecer todos os itens do Map
            if (Environment.TickCount > Tie.Score_Map_Items + 300000)
            {
                Lists.Map[i].Temp_Item = new System.Collections.Generic.List<Lists.Structures.Map_Items>();
                Appearance_Items(i);
                Sending.Map_Items(i);
            }
        }

        // Reseta as contagens
        if (Environment.TickCount > Tie.Score_NPC_Reneration + 5000) Tie.Score_NPC_Reneration = Environment.TickCount;
        if (Environment.TickCount > Tie.Score_Map_Items + 300000) Tie.Score_Map_Items = Environment.TickCount;
    }

    public static byte ThereIsNPC(short Map_Num, short X, short Y)
    {
        // Verifica se há algum npc na cordenada
        for (byte i = 1; i <= Lists.Map[Map_Num].Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map[Map_Num].Temp_NPC[i].Index > 0)
                if (Lists.Map[Map_Num].Temp_NPC[i].X == X && Lists.Map[Map_Num].Temp_NPC[i].Y == Y)
                    return i;

        return 0;
    }

    public static byte ThereIsPlayer(short Map_Num, short X, short Y)
    {
        // Verifica se há algum Jogador na cordenada
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Player.Character(i).X == X && Player.Character(i).Y == Y && Player.Character(i).Map == Map_Num)
                    return i;

        return 0;
    }

    public static bool ThereIsPlayers(short Map_Num)
    {
        // Verifica se tem algum jogador no Map
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Player.IsPlaying(i))
                if (Player.Character(i).Map == Map_Num)
                    return true;

        return false;
    }

    public static byte ThereIsItem(short Map_Num, byte X, byte Y)
    {
        // Verifica se tem algum item nas coordenadas 
        for (byte i = (byte)(Lists.Map[Map_Num].Temp_Item.Count - 1); i >= 1; i--)
            if (Lists.Map[Map_Num].Temp_Item[i].X == X && Lists.Map[Map_Num].Temp_Item[i].Y == Y)
                return i;

        return 0;
    }

    public static bool ForaDoLimite(short Map_Num, short X, short Y)
    {
        // Verifica se as coordenas estão no limite do Map
        if (X > Lists.Map[Map_Num].Width || Y > Lists.Map[Map_Num].Height || X < 0 || Y < 0)
            return true;
        else
            return false;
    }

    public static void NextTile(Game.Location Direction, ref short X, ref short Y)
    {
        // Next Tile
        switch (Direction)
        {
            case Game.Location.Above: Y -= 1; break;
            case Game.Location.Below: Y += 1; break;
            case Game.Location.Right: X += 1; break;
            case Game.Location.Left: X -= 1; break;
        }
    }

    public static bool Tile_Blocked(short Map_Num, short X, short Y)
    {
        // Verifica se o Tile está bloqueado
        if (ForaDoLimite(Map_Num, X, Y))
            return true;
        else if (Lists.Map[Map_Num].Tile[X, Y].Attribute == (byte)Attributes.Block)
            return true;

        return false;
    }

    public static bool Tile_Blocked(short Map_Num, short X, short Y, Game.Location Direction, bool ContarPersonagens = true)
    {
        short Next_X = X, Next_Y = Y;

        // Next Tile
        NextTile(Direction, ref Next_X, ref Next_Y);

        // Verifica se o Tile está bloqueado
        if (Tile_Blocked(Map_Num, (byte)Next_X, (byte)Next_Y))
            return true;
        else if (Lists.Map[Map_Num].Tile[Next_X, Next_Y].Block[(byte)Game.InverseDirection(Direction)])
            return true;
        else if (Lists.Map[Map_Num].Tile[X, Y].Block[(byte)Direction])
            return true;
        else if (ContarPersonagens && (ThereIsPlayer(Map_Num, Next_X, Next_Y) > 0 || ThereIsNPC(Map_Num, Next_X, Next_Y) > 0))
            return true;

        return false;
    }

    public static void Appearance_Items(short Map_Num)
    {
        Lists.Structures.Maps Data = Lists.Map[Map_Num];
        Lists.Structures.Map_Items Item = new Lists.Structures.Map_Items();

        // Verifica se tem algum atributo de item no Map
        for (byte x = 0; x <= Data.Width; x++)
            for (byte y = 0; y <= Data.Height; y++)
                if (Data.Tile[x, y].Attribute == (byte)Attributes.Item)
                {
                    // Faz o item aparecer
                    Item.Index = Data.Tile[x, y].Dado_1;
                    Item.Amount = Data.Tile[x, y].Dado_2;
                    Item.X = x;
                    Item.Y = y;
                    Lists.Map[Map_Num].Temp_Item.Add(Item);
                }
    }
}

partial class Read
{
    public static void Maps()
    {
        Lists.Map = new Lists.Structures.Maps[Lists.Server_Data.Num_Maps + 1];

        // Lê os Data
        for (short i = 1; i <= Lists.Map.GetUpperBound(0); i++)
            Map(i);
    }

    public static void Map(short Index)
    {
        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Archive = new FileInfo(Directories.Maps.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Archive.OpenRead());

        // Lê os Data
        Lists.Map[Index].Revisão = Binary.ReadInt16();
        Lists.Map[Index].Name = Binary.ReadString();
        Lists.Map[Index].Width = Binary.ReadByte();
        Lists.Map[Index].Height = Binary.ReadByte();
        Lists.Map[Index].Moral = Binary.ReadByte();
        Lists.Map[Index].Panorama = Binary.ReadByte();
        Lists.Map[Index].Music = Binary.ReadByte();
        Lists.Map[Index].Coloração = Binary.ReadInt32();
        Lists.Map[Index].Climate.Type = Binary.ReadByte();
        Lists.Map[Index].Climate.Intensity = Binary.ReadByte();
        Lists.Map[Index].Smoke.Texture = Binary.ReadByte();
        Lists.Map[Index].Smoke.VelocityX = Binary.ReadSByte();
        Lists.Map[Index].Smoke.VelocityY = Binary.ReadSByte();
        Lists.Map[Index].Smoke.Transparency = Binary.ReadByte();
        Lists.Map[Index].LightGlobal = Binary.ReadByte();
        Lists.Map[Index].Iluminação = Binary.ReadByte();

        // Ligações
        Lists.Map[Index].Ligação = new short[(byte)Game.Location.Amount];
        for (short i = 0; i <= (short)Game.Location.Amount - 1; i++)
            Lists.Map[Index].Ligação[i] = Binary.ReadInt16();

        // Tiles
        Map_Tiles(Index, Binary);

        // Tiles specific data
        for (byte x = 0; x <= Lists.Map[Index].Width; x++)
            for (byte y = 0; y <= Lists.Map[Index].Height; y++)
            {
                Lists.Map[Index].Tile[x, y].Attribute = Binary.ReadByte();
                Lists.Map[Index].Tile[x, y].Dado_1 = Binary.ReadInt16();
                Lists.Map[Index].Tile[x, y].Dado_2 = Binary.ReadInt16();
                Lists.Map[Index].Tile[x, y].Dado_3 = Binary.ReadInt16();
                Lists.Map[Index].Tile[x, y].Dado_4 = Binary.ReadInt16();
                Lists.Map[Index].Tile[x, y].Zone = Binary.ReadByte();

                // Directional lock
                Lists.Map[Index].Tile[x, y].Block = new bool[(byte)Game.Location.Amount];
                for (byte i = 0; i <= (byte)Game.Location.Amount - 1; i++)
                    Lists.Map[Index].Tile[x, y].Block[i] = Binary.ReadBoolean();
            }

        // Lights
        Lists.Map[Index].Light = new Lists.Structures.Light[Binary.ReadByte()];
        if (Lists.Map[Index].Light.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map[Index].Light.GetUpperBound(0); i++)
            {
                Lists.Map[Index].Light[i].X = Binary.ReadByte();
                Lists.Map[Index].Light[i].Y = Binary.ReadByte();
                Lists.Map[Index].Light[i].Width = Binary.ReadByte();
                Lists.Map[Index].Light[i].Height = Binary.ReadByte();
            }

        // NPCs
        Lists.Map[Index].NPC = new Lists.Structures.Map_NPC[Binary.ReadByte() + 1];
        Lists.Map[Index].Temp_NPC = new Lists.Structures.Map_NPCs[Lists.Map[Index].NPC.GetUpperBound(0) + 1];
        if (Lists.Map[Index].NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Lists.Map[Index].NPC.GetUpperBound(0); i++)
            {
                Lists.Map[Index].NPC[i].Index = Binary.ReadInt16();
                Lists.Map[Index].NPC[i].Zone = Binary.ReadByte();
                Lists.Map[Index].NPC[i].Aparecer = Binary.ReadBoolean();
                Lists.Map[Index].NPC[i].X = Binary.ReadByte();
                Lists.Map[Index].NPC[i].Y = Binary.ReadByte();
                global::NPC.Appearance(i, Index);
            }

        // Items
        Lists.Map[Index].Temp_Item = new System.Collections.Generic.List<Lists.Structures.Map_Items>();
        Lists.Map[Index].Temp_Item.Add(new Lists.Structures.Map_Items()); // Nulo
        global::Map.Appearance_Items(Index);

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Map_Tiles(short Index, BinaryReader Binary)
    {
        byte Num_Layers = Binary.ReadByte();

        // Redimensiona os Data
        Lists.Map[Index].Tile = new Lists.Structures.Tile[Lists.Map[Index].Width + 1, Lists.Map[Index].Height + 1];
        for (byte x = 0; x <= Lists.Map[Index].Width; x++)
            for (byte y = 0; y <= Lists.Map[Index].Height; y++)
                Lists.Map[Index].Tile[x, y].Data = new Lists.Structures.Tile_Data[(byte)global::Map.Layers.Amount, Num_Layers + 1];

        // Read the Tiles
        for (byte i = 0; i <= Num_Layers; i++)
        {
            // Basic data
            Binary.ReadString(); // Nome
            byte t = Binary.ReadByte(); // Tipo

            // Tiles
            for (byte x = 0; x <= Lists.Map[Index].Width; x++)
                for (byte y = 0; y <= Lists.Map[Index].Height; y++)
                {
                    Lists.Map[Index].Tile[x, y].Data[t, i].x = Binary.ReadByte();
                    Lists.Map[Index].Tile[x, y].Data[t, i].y = Binary.ReadByte();
                    Lists.Map[Index].Tile[x, y].Data[t, i].Tile = Binary.ReadByte();
                    Lists.Map[Index].Tile[x, y].Data[t, i].Automatic = Binary.ReadBoolean();
                }
        }
    }
}