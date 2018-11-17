using System;

class Clean
{
    public static void Required()
    {
        // Clears all required data
        Players();
    }

    public static void Players()
    {
        // Resizes the list
        Lists.Player = new Lists.Structures.Player[Lists.Server_Data.Max_Players + 1];
        Lists.TempPlayer = new Lists.Structures.TempPlayer[Lists.Server_Data.Max_Players + 1];

        // Clears all Player data
        for (byte i = 1; i <= Lists.Server_Data.Max_Players; i++)
            Player(i);
    }

    public static void Player(byte Index)
    {
        // Clears Player data
        Lists.Player[Index] = new Lists.Structures.Player();
        Lists.TempPlayer[Index] = new Lists.Structures.TempPlayer();
        Lists.Player[Index].User = string.Empty;
        Lists.Player[Index].Password = string.Empty;
        Player_Characters(Index);
    }

    public static void Player_Characters(byte Index)
    {
        Lists.Player[Index].Character = new Player.Character_Structure[Lists.Server_Data.Max_Characters + 1];

        // Limpa os dados
        for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
            Player_Character(Index, i);
    }

    public static void Player_Character(byte Index, byte Character)
    {
        // Limpa os dados
        Lists.Player[Index].Character[Character] = new Player.Character_Structure();
        Lists.Player[Index].Character[Character].Index = Index;
        Lists.Player[Index].Character[Character].Inventory = new Lists.Structures.Inventory[Game.Max_Inventory + 1];
        Lists.Player[Index].Character[Character].Equipment = new short[(byte)Game.Equipment.Amount];
        Lists.Player[Index].Character[Character].Hotbar = new Lists.Structures.Hotbar[Game.Max_Hotbar + 1];
    }
}