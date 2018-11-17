using System;
using System.IO;

partial class Read
{
    public static void Required()
    {
        // Load all data
        Server_Data();
        Console.WriteLine("Data loaded.");
        Classes();
        Console.WriteLine("Classes loaded.");
        NPCs();
        Console.WriteLine("NPCs loaded.");
        Items();
        Console.WriteLine("Items loaded.");
        Maps();
        Console.WriteLine("Maps loaded.");
    }

    public static void Server_Data()
    {
        // Creates a Binary system for data manipulation
        BinaryReader Binary = new BinaryReader(Directories.Server_Data.OpenRead());

        // Lê os dados
        Lists.Server_Data.Game_Name = Binary.ReadString();
        Lists.Server_Data.Message = Binary.ReadString();
        Lists.Server_Data.Porta = Binary.ReadInt16();
        Lists.Server_Data.Max_Players = Binary.ReadByte();
        Lists.Server_Data.Max_Characters = Binary.ReadByte();
        Lists.Server_Data.Num_Classes = Binary.ReadByte();
        Lists.Server_Data.Num_Tiles = Binary.ReadByte();
        Lists.Server_Data.Num_Maps = Binary.ReadInt16();
        Lists.Server_Data.Num_NPCs = Binary.ReadInt16();
        Lists.Server_Data.Num_Items = Binary.ReadInt16();

        // Closes the system
        Binary.Dispose();
    }

    public static void Player(byte Index, string Nome, bool Characters = true)
    {
        string Directory = Directories.Accounts.FullName + Nome + Directories.Format;

        // Create a temporary file
        BinaryReader Archive = new BinaryReader(File.OpenRead(Directory));

        // Loads the data and adds it to the cache
        Lists.Player[Index].User = Archive.ReadString();
        Lists.Player[Index].Password = Archive.ReadString();
        Lists.Player[Index].Access = (Game.Rank)Archive.ReadByte();

        // Dados do Character
        if (Characters)
            for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
            {
                Lists.Player[Index].Character[i].Name = Archive.ReadString();
                Lists.Player[Index].Character[i].Classe = Archive.ReadByte();
                Lists.Player[Index].Character[i].Genre = Archive.ReadBoolean();
                Lists.Player[Index].Character[i].Level = Archive.ReadInt16();
                Lists.Player[Index].Character[i].Experience = Archive.ReadInt16();
                Lists.Player[Index].Character[i].Points = Archive.ReadByte();
                Lists.Player[Index].Character[i].Map = Archive.ReadInt16();
                Lists.Player[Index].Character[i].X = Archive.ReadByte();
                Lists.Player[Index].Character[i].Y = Archive.ReadByte();
                Lists.Player[Index].Character[i].Direction = (Game.Location)Archive.ReadByte();
                for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Lists.Player[Index].Character[i].Vital[n] = Archive.ReadInt16();
                for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Lists.Player[Index].Character[i].Attribute[n] = Archive.ReadInt16();
                for (byte n = 1; n <= Game.Max_Inventory; n++)
                {
                    Lists.Player[Index].Character[i].Inventory[n].Item_Num = Archive.ReadInt16();
                    Lists.Player[Index].Character[i].Inventory[n].Amount = Archive.ReadInt16();
                }
                for (byte n = 0; n <= (byte)Game.Equipment.Amount - 1; n++) Lists.Player[Index].Character[i].Equipment[n] = Archive.ReadInt16();
                for (byte n = 1; n <= Game.Max_Hotbar; n++)
                {
                    Lists.Player[Index].Character[i].Hotbar[n].Type = Archive.ReadByte();
                    Lists.Player[Index].Character[i].Hotbar[n].Slot = Archive.ReadByte();
                }
            }

        // Descarrega o Archive
        Archive.Dispose();
    }

    public static string Player_Password(string User)
    {
        // Create a temporary archive
        BinaryReader Archive = new BinaryReader(File.OpenRead(Directories.Accounts.FullName + User + Directories.Format));

        // Find the account password
        Archive.ReadString();
        string PasswordLoaded = Archive.ReadString();

        // Download Archive
        Archive.Dispose();

        // Returns the value of the function
        return PasswordLoaded;
    }

    public static string Characters_Names()
    {
        // Create Archive if it does not exist
        if (!Directories.Characters.Exists)
        {
            Write.Characters(string.Empty);
            return string.Empty;
        }

        // Cria um Archive temporário
        StreamReader Archive = new StreamReader(Directories.Characters.FullName);

        // Carrega todos os nomes dos personagens
        string Characters = Archive.ReadToEnd();

        // Descarrega o Archive
        Archive.Dispose();

        // Retorna o valor de acordo com o que foi carregado
        return Characters;
    }

    public static void Classes()
    {
        Lists.Classe = new Lists.Structures.Classes[Lists.Server_Data.Num_Classes + 1];

        // Lê os dados
        for (byte i = 1; i <= Lists.Classe.GetUpperBound(0); i++)
            Classe(i);
    }

    public static void Classe(byte Index)
    {
        // Cria um sistema Binary para a manipulação dos dados
        FileInfo Archive = new FileInfo(Directories.Classes.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Archive.OpenRead());

        // Redimensiona os valores necessários 
        Lists.Classe[Index].Vital = new short[(byte)Game.Vital.Amount];
        Lists.Classe[Index].Attribute = new short[(byte)Game.Attributes.Amount];

        // Lê os dados
        Lists.Classe[Index].Name = Binary.ReadString();
        Lists.Classe[Index].Texture_Male = Binary.ReadInt16();
        Lists.Classe[Index].Texture_Female = Binary.ReadInt16();
        Lists.Classe[Index].Appearance_Map = Binary.ReadInt16();
        Lists.Classe[Index].Appearance_Direction = Binary.ReadByte();
        Lists.Classe[Index].Appearance_X = Binary.ReadByte();
        Lists.Classe[Index].Appearance_Y = Binary.ReadByte();
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++) Lists.Classe[Index].Vital[i] = Binary.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Attributes.Amount - 1; i++) Lists.Classe[Index].Attribute[i] = Binary.ReadInt16();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Items()
    {
        Lists.Item = new Lists.Structures.Items[Lists.Server_Data.Num_Items + 1];

        // Lê os dados
        for (byte i = 1; i <= Lists.Item.GetUpperBound(0); i++)
            Item(i);
    }

    public static void Item(byte Index)
    {
        // Cria um sistema Binary para a manipulação dos dados
        FileInfo Archive = new FileInfo(Directories.Items.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Archive.OpenRead());

        // Redimensiona os valores necessários 
        Lists.Item[Index].Potion_Vital = new short[(byte)Game.Vital.Amount];
        Lists.Item[Index].Equip_Attribute = new short[(byte)Game.Attributes.Amount];

        // Lê os dados
        Lists.Item[Index].Name = Binary.ReadString();
        Lists.Item[Index].Description = Binary.ReadString();
        Lists.Item[Index].Texture = Binary.ReadInt16();
        Lists.Item[Index].Type = Binary.ReadByte();
        Lists.Item[Index].Price = Binary.ReadInt16();
        Lists.Item[Index].Empilhável = Binary.ReadBoolean();
        Lists.Item[Index].NãoDropável = Binary.ReadBoolean();
        Lists.Item[Index].Req_Level = Binary.ReadInt16();
        Lists.Item[Index].Req_Classe = Binary.ReadByte();
        Lists.Item[Index].Potion_Experience = Binary.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++) Lists.Item[Index].Potion_Vital[i] = Binary.ReadInt16();
        Lists.Item[Index].Equip_Type = Binary.ReadByte();
        for (byte i = 0; i <= (byte)Game.Attributes.Amount - 1; i++) Lists.Item[Index].Equip_Attribute[i] = Binary.ReadInt16();
        Lists.Item[Index].Weapon_Damage = Binary.ReadInt16();

        // Fecha o sistema
        Binary.Dispose();
    }
}