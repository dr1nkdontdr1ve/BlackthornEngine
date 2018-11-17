using System.IO;

class Write
{
    public static void Player(byte Index)
    {
        string Directory = Directories.Accounts.FullName + Lists.Player[Index].User + Directories.Format;

        // Evita erros
        if (Lists.Player[Index].User == string.Empty) return;

        // Cria um Archive temporário
        BinaryWriter Archive = new BinaryWriter(File.OpenWrite(Directory));

        // Salva os dados no Archive
        Archive.Write(Lists.Player[Index].User);
        Archive.Write(Lists.Player[Index].Password);
        Archive.Write((byte)Lists.Player[Index].Access);

        for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
        {
            Archive.Write(Lists.Player[Index].Character[i].Name);
            Archive.Write(Lists.Player[Index].Character[i].Classe);
            Archive.Write(Lists.Player[Index].Character[i].Genre);
            Archive.Write(Lists.Player[Index].Character[i].Level);
            Archive.Write(Lists.Player[Index].Character[i].Experience);
            Archive.Write(Lists.Player[Index].Character[i].Points);
            Archive.Write(Lists.Player[Index].Character[i].Map);
            Archive.Write(Lists.Player[Index].Character[i].X);
            Archive.Write(Lists.Player[Index].Character[i].Y);
            Archive.Write((byte)Lists.Player[Index].Character[i].Direction);
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Archive.Write(Lists.Player[Index].Character[i].Vital[n]);
            for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Archive.Write(Lists.Player[Index].Character[i].Attribute[n]);
            for (byte n = 1; n <= Game.Max_Inventory; n++)
            {
                Archive.Write(Lists.Player[Index].Character[i].Inventory[n].Item_Num);
                Archive.Write(Lists.Player[Index].Character[i].Inventory[n].Amount);
            }
            for (byte n = 0; n <= (byte)Game.Equipment.Amount - 1; n++) Archive.Write(Lists.Player[Index].Character[i].Equipment[n]);
            for (byte n = 1; n <= Game.Max_Hotbar; n++)
            {
                Archive.Write(Lists.Player[Index].Character[i].Hotbar[n].Type);
                Archive.Write(Lists.Player[Index].Character[i].Hotbar[n].Slot);
            }
        }

        // Download the file
        Archive.Dispose();
    }

    public static void Character(string Name)
    {
        // Create a temporary file
        StreamWriter Archive = new StreamWriter(Directories.Characters.FullName, true);

        // Saves the character's name to the file
        Archive.Write(";" + Name + ":");

        // Download the file
        Archive.Dispose();
    }

    public static void Characters(string Characters)
    {
        // Create a temporary file
        StreamWriter Archive = new StreamWriter(Directories.Characters.FullName);

        // Saves the character's name to the file
        Archive.Write(Characters);

        // Download the file
        Archive.Dispose();
    }
}