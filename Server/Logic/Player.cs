using System;
using System.Drawing;

class Player
{
    public static Character_Structure Character(byte Index)
    {
        // Retorna com os valores do Character atual
        return Lists.Player[Index].Character[Lists.TempPlayer[Index].Used];
    }

    public class Character_Structure
    {
        // Basic data
        public byte Index;
        public string Name = string.Empty;
        public byte Classe;
        public bool Genre;
        public short Level;
        private short experience;
        public byte Points;
        public short[] Vital = new short[(byte)Game.Vital.Amount];
        public short[] Attribute = new short[(byte)Game.Attributes.Amount];
        public short Map;
        public byte X;
        public byte Y;
        public Game.Location Direction;
        public int Attack_Time;
        public Lists.Structures.Inventory[] Inventory;
        public short[] Equipment;
        public Lists.Structures.Hotbar[] Hotbar;

        public short Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;
                CheckLevel(Index);
            }
        }

        // Calculates the player's damage
        public short Damage
        {
            get
            {
                return (short)(Attribute[(byte)Game.Attributes.Force] + Lists.Item[Equipment[(byte)Game.Equipment.Weapon]].Weapon_Damage);
            }
        }

        // Calculates the player's defense
        public short Player_Defense
        {
            get
            {
                return Attribute[(byte)Game.Attributes.Resistance];
            }
        }

        public short MaxVital(byte Vital)
        {
            short[] Base = Lists.Classe[Classe].Vital;

            // Calculate the most vital amount a player has
            switch ((Game.Vital)Vital)
            {
                case Game.Vital.Life:
                    return (short)(Base[Vital] + (Attribute[(byte)Game.Attributes.Vitality] * 1.50 * (Level * 0.75)));
                case Game.Vital.Mana:
                    return (short)(Base[Vital] + (Attribute[(byte)Game.Attributes.Intelligence] * 1.25 * (Level * 0.5)));
            }

            return 0;
        }

        public short Regeneration(byte Vital)
        {
            // Calculate the most vital amount a player has
            switch ((Game.Vital)Vital)
            {
                case Game.Vital.Life:
                    return (short)(MaxVital(Vital) * 0.05 + Attribute[(byte)Game.Attributes.Vitality] * 0.3);
                case Game.Vital.Mana:
                    return (short)(MaxVital(Vital) * 0.05 + Attribute[(byte)Game.Attributes.Intelligence] * 0.1);
            }

            return 0;
        }

        public short ExpRequired
        {
            get
            {
                short Sum = 0;
                // Amount of experience to move to the next level
                for (byte i = 0; i <= (byte)(Game.Attributes.Amount - 1); i++) Sum += Attribute[i];
                return (short)((Level + 1) * 2.5 + (Sum + Points) / 2);
            }
        }
    }

    public static void Entrar(byte Index)
    {
        // Previni que alguém que já está online de logar
        if (IsPlaying(Index))
            return;

        // Defines that the player is inside the Game
        Lists.TempPlayer[Index].Playing = true;

        // Envia todos os dados necessários
        Sending.Entrada(Index);
        Sending.Players_Data_Map(Index);
        Sending.Player_Experience(Index);
        Sending.Player_Inventory(Index);
        Sending.Player_Hotbar(Index);
        Sending.Items(Index);
        Sending.NPCs(Index);
        Sending.Map_Items(Index, Character(Index).Map);

        // Transports the player to his determined position
        Transportar(Index, Character(Index).Map, Character(Index).X, Character(Index).Y);

        // Enter the Game
        Sending.Entrar(Index);
        Sending.Message(Index, Lists.Server_Data.Message, Color.Blue);
    }

    public static void Leave(byte Index)
    {
        // Salva os dados do jogador
        Write.Player(Index);
        Clean.Player(Index);

        // Sends everyone the player disconnect
        Sending.Player_Exited(Index);
    }

    public static bool IsPlaying(byte Index)
    {
        // Verifica se o jogador está dentro do Game
        if (Network.IsConnected(Index))
            if (Lists.TempPlayer[Index].Playing)
                return true;

        return false;
    }

    public static byte Encontrar(string Name)
    {
        // Encontra o usuário
        for (byte i = 1; i <= Lists.Player.GetUpperBound(0); i++)
            if (Character(i).Name == Name)
                return i;

        return 0;
    }

    public static byte EncontrarCharacter(byte Index, string Name)
    {
        // Encontra o Character
        for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
            if (Lists.Player[Index].Character[i].Name == Name)
                return i;

        return 0;
    }

    public static bool PossuiCharacters(byte Index)
    {
        // Verifica se o jogador tem algum Character
        for (byte i = 1; i <= Lists.Server_Data.Max_Characters; i++)
            if (Lists.Player[Index].Character[i].Name != string.Empty)
                return true;

        return false;
    }

    public static bool MultipleAccounts(string User)
    {
        // Verifica se já há alguém conectado com essa conta
        for (byte i = 1; i <= Game.BiggerIndex; i++)
            if (Network.IsConnected(i))
                if (Lists.Player[i].User == User)
                    return true;

        return false;
    }

    public static void Move(byte Index, byte Movement)
    {
        byte x = Character(Index).X, y = Character(Index).Y;
        short Map_Num = Character(Index).Map;
        short Next_X = x, Next_Y = y;
        short Ligação = Lists.Map[Map_Num].Ligação[(byte)Character(Index).Direction];
        bool OutroMovimento = false;

        // Previni erros
        if (Movement < 1 || Movement > 2) return;
        if (Lists.TempPlayer[Index].GettingMap) return;

        // Próximo Tile
        Map.NextTile(Character(Index).Direction, ref Next_X, ref Next_Y);

        // Ponto de ligação
        if (Map.ForaDoLimite(Map_Num, Next_X, Next_Y))
        {
            if (Ligação > 0)
                switch (Character(Index).Direction)
                {
                    case Game.Location.Above: Transportar(Index, Ligação, x, Lists.Map[Map_Num].Height); break;
                    case Game.Location.Below: Transportar(Index, Ligação, x, 0); break;
                    case Game.Location.Right: Transportar(Index, Ligação, 0, y); break;
                    case Game.Location.Left: Transportar(Index, Ligação, Lists.Map[Map_Num].Width, y); break;
                }
            else
            {
                Sending.Player_Position(Index);
                return;
            }
        }
        // Bloqueio
        else if (!Map.Tile_Blocked(Map_Num, x, y, Character(Index).Direction))
        {
            Character(Index).X = (byte)Next_X;
            Character(Index).Y = (byte)Next_Y;
        }

        // Attributes
        Lists.Structures.Tile Tile = Lists.Map[Map_Num].Tile[Next_X, Next_Y];

        switch ((Map.Attributes)Tile.Attribute)
        {
            // Teleportation
            case Map.Attributes.Teleportation:
                if (Tile.Dado_4 > 0) Character(Index).Direction = (Game.Location)Tile.Dado_4 - 1;
                Transportar(Index, Tile.Dado_1, (byte)Tile.Dado_2, (byte)Tile.Dado_3);
                OutroMovimento = true;
                break;
        }

        // Envia os dados
        if (!OutroMovimento && (x != Character(Index).X || y != Character(Index).Y))
            Sending.Player_Move(Index, Movement);
        else
            Sending.Player_Position(Index);
    }

    public static void Transportar(byte Index, short Map, byte x, byte y)
    {
        short Map_Antigo = Character(Index).Map;

        // Prevents the player from being transported out of bounds
        if (Map == 0) return;
        if (x > Lists.Map[Map].Width) x = Lists.Map[Map].Width;
        if (y > Lists.Map[Map].Height) y = Lists.Map[Map].Height;
        if (x < 0) x = 0;
        if (y < 0) y = 0;

        // Defines the position of the player
        Character(Index).Map = Map;
        Character(Index).X = x;
        Character(Index).Y = y;

        // Sends the NPC data
        Sending.Map_NPCs(Index, Map);

        // Envia os dados para os outros jogadores
        if (Map_Antigo != Map)
            Sending.Leave_Map(Index, Map_Antigo);

        Sending.Player_Position(Index);

        // Atualiza os valores
        Lists.TempPlayer[Index].GettingMap = true;

        // Checks if it will be necessary Sending map data to the player
        Sending.Map_Revisão(Index, Map);
    }

    public static void Attack(byte Index)
    {
        short Next_X = Character(Index).X, Next_Y = Character(Index).Y;
        byte Victim_Index;

        // Next Tile
        Map.NextTile(Character(Index).Direction, ref Next_X, ref Next_Y);

        // Apenas se necessário
        if (Environment.TickCount < Character(Index).Attack_Time + 750) return;
        if (Map.Tile_Blocked(Character(Index).Map, Character(Index).X, Character(Index).Y, Character(Index).Direction, false)) goto continuar;

        // Ataca um jogador
        Victim_Index = Map.ThereIsPlayer(Character(Index).Map, Next_X, Next_Y);
        if (Victim_Index > 0)
        {
            Attack_Player(Index, Victim_Index);
            return;
        }

        // Ataca um NPC
        Victim_Index = Map.ThereIsNPC(Character(Index).Map, Next_X, Next_Y);
        if (Victim_Index > 0)
        {
            Attack_NPC(Index, Victim_Index);
            return;
        }

        continuar:
        // Demonstra que aos outros jogadores o ataque
        Sending.Player_Attack(Index, 0, 0);
        Character(Index).Attack_Time = Environment.TickCount;
    }

    public static void Attack_Player(byte Index, byte Victim)
    {
        short Dano;
        short x = Character(Index).X, y = Character(Index).Y;

        // Define o azujelo a frente do jogador
        Map.NextTile(Character(Index).Direction, ref x, ref y);

        // Verifica se a Victim pode ser atacada
        if (!IsPlaying(Victim)) return;
        if (Lists.TempPlayer[Victim].GettingMap) return;
        if (Character(Index).Map != Character(Victim).Map) return;
        if (Character(Victim).X != x || Character(Victim).Y != y) return;
        if (Lists.Map[Character(Index).Map].Moral == (byte)Map.Morais.Pacific)
        {
            Sending.Message(Index, "This is a peaceful area.", Color.White);
            return;
        }

        // Demonstra o ataque aos outros jogadores
        Sending.Player_Attack(Index, Victim, (byte)Game.Target.Player);

        // Tempo de ataque 
        Character(Index).Attack_Time = Environment.TickCount;

        // Cálculo de dano
        Dano = (short)(Character(Index).Damage - Character(Victim).Player_Defense);

        // Dano não fatal
        if (Dano <= Character(Victim).MaxVital((byte)Game.Vital.Life))
        {
            Character(Victim).Vital[(byte)Game.Vital.Life] -= Dano;
            Sending.Player_Vital(Victim);
        }
        // FATALITY
        else
        {
            // Dá 10% da experiência da Victim ao atacante
            Character(Index).Experience+= (short)(Character(Victim).Experience / 10);

            // Mata a Victim
            Died(Victim);
        }
    }

    public static void Attack_NPC(byte Index, byte Victim)
    {
        short Damage;
        short x = Character(Index).X, y = Character(Index).Y;
        Lists.Structures.Map_NPCs NPC = Lists.Map[Character(Index).Map].Temp_NPC[Victim];

        // Define o azujelo a frente do jogador
        Map.NextTile(Character(Index).Direction, ref x, ref y);

        // Verifica se a Victim pode ser atacada
        if (NPC.X != x || NPC.Y != y) return;
        if (Lists.NPC[NPC.Index].Aggressiveness == (byte)global::NPC.Aggressiveness.Passive) return;

        // Define o alvo do NPC
        Lists.Map[Character(Index).Map].Temp_NPC[Victim].Target_Index = Index;
        Lists.Map[Character(Index).Map].Temp_NPC[Victim].Target_Type = (byte)Game.Target.Player;

        // Demonstra o ataque aos outros jogadores
        Sending.Player_Attack(Index, Victim, (byte)Game.Target.NPC);

        // Tempo de ataque 
        Character(Index).Attack_Time = Environment.TickCount;

        // Cálculo de dano
        Damage = (short)(Character(Index).Damage - Lists.NPC[NPC.Index].Attribute[(byte)Game.Attributes.Resistance]);

        // Dano não fatal
        if (Damage < Lists.Map[Character(Index).Map].Temp_NPC[Victim].Vital[(byte)Game.Vital.Life])
        {
            Lists.Map[Character(Index).Map].Temp_NPC[Victim].Vital[(byte)Game.Vital.Life] -= Damage;
            Sending.Map_NPC_Vital(Character(Index).Map, Victim);
        }
        // FATALITY
        else
        {
            // Experience gained
            Character(Index).Experience += Lists.NPC[NPC.Index].Experience;

            // Reset the NPC data 
            global::NPC.Died(Character(Index).Map, Victim);
        }
    }

    public static void Died(byte Index)
    {
        Lists.Structures.Classes Data = Lists.Classe[Character(Index).Classe];

        // Get back the vital
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
            Character(Index).Vital[n] = Character(Index).MaxVital(n);

        //You lose 10% of the experience
        Character(Index).Experience /= 10;
        Sending.Player_Experience(Index);

        //Back to top
        Character(Index).Direction = (Game.Location)Data.Appearance_Direction;
        Transportar(Index, Data.Appearance_Map, Data.Appearance_X, Data.Appearance_Y);
    }

    public static void Logic()
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Game.BiggerIndex; i++)
        {
            // Não é necessário
            if (!IsPlaying(i)) continue;

            ///////////////
            // Reneração // 
            ///////////////
            if (Environment.TickCount > Tie.Score_Player_Reneration + 5000)
                for (byte v = 0; v <= (byte)Game.Vital.Amount - 1; v++)
                    if (Character(i).Vital[v] < Character(i).MaxVital(v))
                    {
                        // Renera a vida do jogador
                        Character(i).Vital[v] += Character(i).Regeneration(v);
                        if (Character(i).Vital[v] > Character(i).MaxVital(v)) Character(i).Vital[v] = Character(i).MaxVital(v);

                        // Envia os dados aos jogadores
                        Sending.Player_Vital(i);
                    }
        }

        // Reseta as contagens
        if (Environment.TickCount > Tie.Score_Player_Reneration + 5000) Tie.Score_Player_Reneration = Environment.TickCount;
    }

    public static void CheckLevel(byte Index)
    {
        byte NumLevel = 0; short ExpSobrando;

        // Previni erros
        if (!IsPlaying(Index)) return;

        while (Character(Index).Experience >= Character(Index).ExpRequired)
        {
            NumLevel += 1;
            ExpSobrando = (short)(Character(Index).Experience - Character(Index).ExpRequired);

            // Define os dados
            Character(Index).Level += 1;
            Character(Index).Points += 3;
            Character(Index).Experience = ExpSobrando;
        }

        // Envia os dados
        Sending.Player_Experience(Index);
        if (NumLevel > 0) Sending.Players_Data_Map(Index);
    }

    public static bool GiveItem(byte Index, short Item_Num, short Amount)
    {
        byte Slot_Item = EncontrarInventory(Index, Item_Num);
        byte Slot_Vazio = EncontrarInventory(Index, 0);

        // Somente se necessário
        if (Item_Num == 0) return false;
        if (Slot_Vazio == 0) return false;
        if (Amount == 0) Amount = 1;

        // Stackable
        if (Slot_Item > 0 && Lists.Item[Item_Num].Empilhável)
            Character(Index).Inventory[Slot_Item].Amount += Amount;
        //Non-stackable
        else
        {
            Character(Index).Inventory[Slot_Vazio].Item_Num = Item_Num;
            Character(Index).Inventory[Slot_Vazio].Amount = Amount;
        }

        //Sends the data to the player
        Sending.Player_Inventory(Index);
        return true;
    }

    public static void SoltarItem(byte Index, byte Slot)
    {
        short Map_Num = Character(Index).Map;
        Lists.Structures.Map_Items Map_Item = new Lists.Structures.Map_Items();

        // Somente se necessário
        if (Lists.Map[Map_Num].Temp_Item.Count == Game.Max_Map_Items) return;
        if (Character(Index).Inventory[Slot].Item_Num == 0) return;
        if (Lists.Item[Character(Index).Inventory[Slot].Item_Num].NãoDropável) return;

        // Solta o item no chão
        Map_Item.Index = Character(Index).Inventory[Slot].Item_Num;
        Map_Item.Amount = Character(Index).Inventory[Slot].Amount;
        Map_Item.X = Character(Index).X;
        Map_Item.Y = Character(Index).Y;
        Lists.Map[Map_Num].Temp_Item.Add(Map_Item);
        Sending.Map_Items(Map_Num);

        // Retira o item do Inventory do jogador 
        Character(Index).Inventory[Slot].Item_Num = 0;
        Character(Index).Inventory[Slot].Amount = 0;
        Sending.Player_Inventory(Index);
    }

    public static void UseItem(byte Index, byte Slot)
    {
        short Item_Num = Character(Index).Inventory[Slot].Item_Num;

        // Somente se necessário
        if (Item_Num == 0) return;

        // Requerimentos
        if (Character(Index).Level < Lists.Item[Item_Num].Req_Level)
        {
            Sending.Message(Index, "You do not have the level required to use this item.", Color.White);
            return;
        }
        if (Lists.Item[Item_Num].Req_Classe > 0)
            if (Character(Index).Classe != Lists.Item[Item_Num].Req_Classe)
            {
                Sending.Message(Index, "You can not use this item.", Color.White);
                return;
            }

        if (Lists.Item[Item_Num].Type == (byte)Game.Items.Equipment)
        {
            // Retira o item da hotbar
            byte HotbarSlot = EncontrarHotbar(Index, (byte)Game.Hotbar.Item, Slot);
            Character(Index).Hotbar[HotbarSlot].Type = 0;
            Character(Index).Hotbar[HotbarSlot].Slot = 0;

            // Retira o item do Inventory
            Character(Index).Inventory[Slot].Item_Num = 0;
            Character(Index).Inventory[Slot].Amount = 0;

            // Caso já estiver com algum equipamento, desequipa ele
            if (Character(Index).Equipment[Lists.Item[Item_Num].Equip_Type] > 0) GiveItem(Index, Item_Num, 1);

            // Equipa o item
            Character(Index).Equipment[Lists.Item[Item_Num].Equip_Type] = Item_Num;
            for (byte i = 0; i <= (byte)Game.Attributes.Amount - 1; i++) Character(Index).Attribute[i] += Lists.Item[Item_Num].Equip_Attribute[i];

            // Envia os dados
            Sending.Player_Inventory(Index);
            Sending.Player_Equipment(Index);
            Sending.Player_Hotbar(Index);
        }
        else if (Lists.Item[Item_Num].Type == (byte)Game.Items.Potion)
        {
            // Efeitos
            bool TeveEfeito = false;
            Character(Index).Experience += Lists.Item[Item_Num].Potion_Experience;
            if (Character(Index).Experience < 0) Character(Index).Experience = 0;
            for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++)
            {
                // Verifica se o item causou algum efeito 
                if (Character(Index).Vital[i] < Character(Index).MaxVital(i) && Lists.Item[Item_Num].Potion_Vital[i] != 0) TeveEfeito = true;

                // Efeito
                Character(Index).Vital[i] += Lists.Item[Item_Num].Potion_Vital[i];

                // Impede que passe dos limites
                if (Character(Index).Vital[i] < 0) Character(Index).Vital[i] = 0;
                if (Character(Index).Vital[i] > Character(Index).MaxVital(i)) Character(Index).Vital[i] = Character(Index).MaxVital(i);
            }

            // Foi fatal
            if (Character(Index).Vital[(byte)Game.Vital.Life] == 0) Died(Index);

            // Remove o item caso tenha tido algum efeito
            if (Lists.Item[Item_Num].Potion_Experience > 0 || TeveEfeito)
            {
                Character(Index).Inventory[Slot].Item_Num = 0;
                Character(Index).Inventory[Slot].Amount = 0;
                Sending.Player_Inventory(Index);
                Sending.Player_Vital(Index);
            }
        }
    }

    public static byte EncontrarHotbar(byte Index, byte Type, byte Slot)
    {
        // Encontra algo especifico na hotbar
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
            if (Character(Index).Hotbar[i].Type == Type && Character(Index).Hotbar[i].Slot == Slot)
                return i;

        return 0;
    }

    public static byte EncontrarInventory(byte Index, short Item_Num)
    {
        // Find something specific in hotbar
        for (byte i = 1; i <= Game.Max_Inventory; i++)
            if (Character(Index).Inventory[i].Item_Num == Item_Num)
                return i;

        return 0;
    }
}