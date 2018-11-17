using System.IO;
using Lidgren.Network;

class Receiving
{
    // Customer Packages
    public enum Packages
    {
        Latency,
        Connect,
        Register,
        CreateCharacter,
        Character_Use,
        Character_Create,
        Character_Delete,
        Player_Direction,
        Player_Move,
        Request_Map,
        Message,
        Player_Attack,
        AddPoints,
        CollectItem,
        SoltarItem,
        Inventory_Change,
        Inventory_Use,
        Remove_Equipment,
        Hotbar_Add,
        Hotbar_Change,
        Hotbar_Use
    }

    public static void ForwardData(byte Index, NetIncomingMessage Data)
    {
        // Manuseia os Data recebidos
        switch ((Packages)Data.ReadByte())
        {
            case Packages.Latency: Latency(Index, Data); break;
            case Packages.Connect: Connect(Index, Data); break;
            case Packages.Register: Register(Index, Data); break;
            case Packages.CreateCharacter: CreateCharacter(Index, Data); break;
            case Packages.Character_Use: Character_Use(Index, Data); break;
            case Packages.Character_Create: Character_Create(Index, Data); break;
            case Packages.Character_Delete: Character_Delete(Index, Data); break;
            case Packages.Player_Direction: Player_Direction(Index, Data); break;
            case Packages.Player_Move: Player_Move(Index, Data); break;
            case Packages.Request_Map: Request_Map(Index, Data); break;
            case Packages.Message: Message(Index, Data); break;
            case Packages.Player_Attack: Player_Attack(Index, Data); break;
            case Packages.AddPoints: AddPoints(Index, Data); break;
            case Packages.CollectItem: CollectItem(Index, Data); break;
            case Packages.SoltarItem: SoltarItem(Index, Data); break;
            case Packages.Inventory_Change: Inventory_Change(Index, Data); break;
            case Packages.Inventory_Use: Inventory_Use(Index, Data); break;
            case Packages.Remove_Equipment: Remove_Equipment(Index, Data); break;
            case Packages.Hotbar_Add: Hotbar_Add(Index, Data); break;
            case Packages.Hotbar_Change: Hotbar_Change(Index, Data); break;
            case Packages.Hotbar_Use: Hotbar_Use(Index, Data); break;
        }
    }

    private static void Latency(byte Index, NetIncomingMessage Data)
    {
        // Sends the packet to the latency count
        Sending.Latency(Index);
    }

    private static void Connect(byte Index, NetIncomingMessage Data)
    {
        // Lê os Data
        string User = Data.ReadString().Trim();
        string Password = Data.ReadString();

        // Check if everything is okay
        if (User.Length < Game.Min_Character || User.Length > Game.Max_Character || Password.Length < Game.Min_Character || Password.Length > Game.Max_Character)
        {
            Sending.Alert(Index, "The user name and password must contain " + Game.Min_Character + " and" + Game.Max_Character + " characters.");
            return;
        }
        if (!File.Exists(Directories.Accounts.FullName + User + Directories.Format))
        {
            Sending.Alert(Index, "This username is not registered.");
            return;
        }
        if (Player.MultipleAccounts(User))
        {
            Sending.Alert(Index, "There's already someone connected to that account.");
            return;
        }
        else if (Password != Read.Player_Password(User))
        {
            Sending.Alert(Index, "The password is incorrect.");
            return;
        }

        // Carrega os Data do Player
        Read.Player(Index, User);

        // Envia os Data das classes
        Sending.Classes(Index);

        // Se o Player não tiver nenhum Character então abrir o painel de criação de Character
        if (!Player.PossuiCharacters(Index))
        {
            Sending.CreateCharacter(Index);
            return;
        }

        // Abre a janela de seleção de personagens
        Sending.Characters(Index);
        Sending.Conectar(Index);
    }

    private static void Register(byte Index, NetIncomingMessage Data)
    {
        // Lê os Data
        string User = Data.ReadString().Trim();
        string Password = Data.ReadString();

        // Check if everything is okay
        if (User.Length < Game.Min_Character || User.Length > Game.Max_Character || Password.Length < Game.Min_Character || Password.Length > Game.Max_Character)
        {
            Sending.Alert(Index, "The User name and password must contain " + Game.Min_Character + " and " + Game.Max_Character + " characters.");
            return;
        }
        else if (File.Exists(Directories.Accounts.FullName + User + Directories.Format))
        {
            Sending.Alert(Index, "Already registered with this name.");
            return;
        }

        // Cria a conta
        Lists.Player[Index].User = User;
        Lists.Player[Index].Password = Password;

        // Salva a conta
        Write.Player(Index);

        // Abre a janela de seleção de personagens
        Sending.Classes(Index);
        Sending.CreateCharacter(Index);
    }

    private static void CreateCharacter(byte Index, NetIncomingMessage Data)
    {
        byte Character = Player.EncontrarCharacter(Index, string.Empty);

        // Lê os Data
        string Name = Data.ReadString().Trim();

        // Verifica se está tudo certo
        if (Name.Length < Game.Min_Character || Name.Length > Game.Max_Character)
        {
            Sending.Alert(Index, "The character name must contain " + Game.Min_Character + " and " + Game.Max_Character + " characters.", false);
            return;
        }
        if (Name.Contains(";") || Name.Contains(":"))
        {
            Sending.Alert(Index, "Cannot contain ';' and ':' in the Character name.", false);
            return;
        }
        if (Read.Characters_Names().Contains(";" + Name + ":"))
        {
            Sending.Alert(Index, "A character with this name already exists.", false);
            return;
        }

        // Define o Character que será usado
        Lists.TempPlayer[Index].Used = Character;

        // Define os valores iniciais do Character
        Player.Character(Index).Name = Name;
        Player.Character(Index).Level = 1;
        Player.Character(Index).Classe = Data.ReadByte();
        Player.Character(Index).Genre = Data.ReadBoolean();
        Player.Character(Index).Attribute = Lists.Classe[Player.Character(Index).Classe].Attribute;
        Player.Character(Index).Map = Lists.Classe[Player.Character(Index).Classe].Appearance_Map;
        Player.Character(Index).Direction = (Game.Location)Lists.Classe[Player.Character(Index).Classe].Appearance_Direction;
        Player.Character(Index).X = Lists.Classe[Player.Character(Index).Classe].Appearance_X;
        Player.Character(Index).Y = Lists.Classe[Player.Character(Index).Classe].Appearance_Y;
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++) Player.Character(Index).Vital[i] = Player.Character(Index).MaxVital(i);

        // Salva a conta
        Write.Character(Name);
        Write.Player(Index);

        // Entra no Game
        Player.Entrar(Index);
    }

    private static void Character_Use(byte Index, NetIncomingMessage Data)
    {
        // Sets the Character that will be used
        Lists.TempPlayer[Index].Used = Data.ReadByte();

        // Enter the Game
        Player.Entrar(Index);
    }

    private static void Character_Create(byte Index, NetIncomingMessage Data)
    {
        // Verifica se o Player já criou o máximo de personagens possíveis
        if (Player.EncontrarCharacter(Index, string.Empty) == 0)
        {
            Sending.Alert(Index, "You can only have " + Lists.Server_Data.Max_Characters + " characters.", false);
            return;
        }

        // Abre a janela de seleção de personagens
        Sending.Classes(Index);
        Sending.CreateCharacter(Index);
    }

    private static void Character_Delete(byte Index, NetIncomingMessage Data)
    {
        byte Character = Data.ReadByte();
        string Name = Lists.Player[Index].Character[Character].Name;

        // Verifica se o Character existe
        if (string.IsNullOrEmpty(Name))
            return;

        // Deleta o Character
        Sending.Alert(Index, "The character '" + Name + "' has been deleted.", false);
        Write.Characters(Read.Characters_Names().Replace(":;" + Name + ":", ":"));
        Clean.Player_Character(Index, Character);

        // Salva o Character
        Sending.Characters(Index);
        Write.Player(Index);
    }

    private static void Player_Direction(byte Index, NetIncomingMessage Data)
    {
        Game.Location Direction = (Game.Location)Data.ReadByte();

        // Previni erros
        if (Direction < Game.Location.Above || Direction > Game.Location.Right) return;
        if (Lists.TempPlayer[Index].GettingMap) return;

        // Defini a direção do Player
        Player.Character(Index).Direction = Direction;
        Sending.Player_Direction(Index);
    }

    private static void Player_Move(byte Index, NetIncomingMessage Data)
    {
        byte X = Data.ReadByte(), Y = Data.ReadByte();

        // Move o Player se necessário
        if (Player.Character(Index).X != X || Player.Character(Index).Y != Y)
            Sending.Player_Position(Index);
        else
            Player.Move(Index, Data.ReadByte());
    }

    private static void Request_Map(byte Index, NetIncomingMessage Data)
    {
        // Se necessário Sending as informações do mapa ao Player
        if (Data.ReadBoolean()) Sending.Map(Index, Player.Character(Index).Map);

        // Envia a informação aos outros Playeres
        Sending.Players_Data_Map(Index);

        // Entra no mapa
        Lists.TempPlayer[Index].GettingMap = false;
        Sending.Enter_Map(Index);
    }

    private static void Message(byte Index, NetIncomingMessage Data)
    {
        string Message = Data.ReadString();

        // Evita Characters inválidos
        for (byte i = 0; i >= Message.Length; i++)
            if ((Message[i] < 32 && Message[i] > 126))
                return;

        // Envia a Message para os outros Playeres
        switch ((Game.Mensagens)Data.ReadByte())
        {
            case Game.Mensagens.Map: Sending.Message_Map(Index, Message); break;
            case Game.Mensagens.Global: Sending.Global_Message(Index, Message); break;
            case Game.Mensagens.Private: Sending.Private_Message(Index, Data.ReadString(), Message); break;
        }
    }

    private static void Player_Attack(byte Index, NetIncomingMessage Data)
    {
        // Ataca
        Player.Attack(Index);
    }

    private static void AddPoints(byte Index, NetIncomingMessage Data)
    {
        byte Attribute = Data.ReadByte();

        // Adiciona um ponto a determinado atributo
        if (Player.Character(Index).Points > 0)
        {
            Player.Character(Index).Attribute[Attribute] += 1;
            Player.Character(Index).Points -= 1;
            Sending.Player_Experience(Index);
            Sending.Players_Data_Map(Index);
        }
    }

    private static void CollectItem(byte Index, NetIncomingMessage Data)
    {
        short Map_Num = Player.Character(Index).Map;
        byte Map_Item = Map.ThereIsItem(Map_Num, Player.Character(Index).X, Player.Character(Index).Y);
        short Map_Item_Num = Lists.Map[Map_Num].Temp_Item[Map_Item].Index;

        // Somente se necessário
        if (Map_Item == 0) return;

        // Dá o item ao Player
        if (Player.GiveItem(Index, Map_Item_Num, Lists.Map[Map_Num].Temp_Item[Map_Item].Amount))
        {
            // Retira o item do mapa
            Lists.Map[Map_Num].Temp_Item.RemoveAt(Map_Item);
            Sending.Map_Items(Map_Num);
        }
    }

    private static void SoltarItem(byte Index, NetIncomingMessage Data)
    {
        Player.SoltarItem(Index, Data.ReadByte());
    }

    private static void Inventory_Change(byte Index, NetIncomingMessage Data)
    {
        byte Slot_Antigo = Data.ReadByte(), Slot_Novo = Data.ReadByte();
        byte Hotbar_Antigo = Player.EncontrarHotbar(Index, (byte)Game.Hotbar.Item, Slot_Antigo), Hotbar_Novo = Player.EncontrarHotbar(Index, (byte)Game.Hotbar.Item, Slot_Novo);
        Lists.Structures.Inventory Antigo = Player.Character(Index).Inventory[Slot_Antigo];

        // Somente se necessário
        if (Player.Character(Index).Inventory[Slot_Antigo].Item_Num == 0) return;
        if (Slot_Antigo == Slot_Novo) return;

        // Caso houver um item no novo slot, trocar ele para o velho
        if (Player.Character(Index).Inventory[Slot_Novo].Item_Num > 0)
        {
            // Inventory
            Player.Character(Index).Inventory[Slot_Antigo].Item_Num = Player.Character(Index).Inventory[Slot_Novo].Item_Num;
            Player.Character(Index).Inventory[Slot_Antigo].Amount = Player.Character(Index).Inventory[Slot_Novo].Amount;
            Player.Character(Index).Hotbar[Hotbar_Novo].Slot = Slot_Antigo;
        }
        else
        {
            Player.Character(Index).Inventory[Slot_Antigo].Item_Num = 0;
            Player.Character(Index).Inventory[Slot_Antigo].Amount = 0;
        }

        // Muda o item de slot
        Player.Character(Index).Inventory[Slot_Novo].Item_Num = Antigo.Item_Num;
        Player.Character(Index).Inventory[Slot_Novo].Amount = Antigo.Amount;
        Player.Character(Index).Hotbar[Hotbar_Antigo].Slot = Slot_Novo;
        Sending.Player_Inventory(Index);
        Sending.Player_Hotbar(Index);
    }

    private static void Inventory_Use(byte Index, NetIncomingMessage Data)
    {
        Player.UseItem(Index, Data.ReadByte());
    }

    private static void Remove_Equipment(byte Index, NetIncomingMessage Data)
    {
        byte Slot = Data.ReadByte();
        byte Slot_Vazio = Player.EncontrarInventory(Index, 0);
        short Map_Num = Player.Character(Index).Map;
        Lists.Structures.Map_Items Map_Item = new Lists.Structures.Map_Items();

        // Apenas se necessário
        if (Player.Character(Index).Equipment[Slot] == 0) return;

        // Adiciona o equipamento ao Inventory
        if (!Player.GiveItem(Index, Player.Character(Index).Equipment[Slot], 1))
        {
            // Somente se necessário
            if (Lists.Map[Map_Num].Temp_Item.Count == Game.Max_Map_Items) return;

            // Solta o item no chão
            Map_Item.Index = Player.Character(Index).Equipment[Slot];
            Map_Item.Amount = 1;
            Map_Item.X = Player.Character(Index).X;
            Map_Item.Y = Player.Character(Index).Y;
            Lists.Map[Map_Num].Temp_Item.Add(Map_Item);

            // Envia os Data
            Sending.Map_Items(Map_Num);
            Sending.Player_Inventory(Index);
        }

        // Remove o equipamento
        for (byte i = 0; i <= (byte)Game.Attributes.Amount - 1; i++) Player.Character(Index).Attribute[i] -= Lists.Item[Player.Character(Index).Equipment[Slot]].Equip_Attribute[i];
        Player.Character(Index).Equipment[Slot] = 0;

        // Envia os Data
        Sending.Player_Equipment(Index);
    }

    private static void Hotbar_Add(byte Index, NetIncomingMessage Data)
    {
        byte Hotbar_Slot = Data.ReadByte();
        byte Type = Data.ReadByte();
        byte Slot = Data.ReadByte();

        // Somente se necessário
        if (Slot != 0 && Player.EncontrarHotbar(Index, Type, Slot) > 0) return;

        // Define os Data
        Player.Character(Index).Hotbar[Hotbar_Slot].Slot = Slot;
        Player.Character(Index).Hotbar[Hotbar_Slot].Type = Type;

        // Envia os Data
        Sending.Player_Hotbar(Index);
    }

    private static void Hotbar_Change(byte Index, NetIncomingMessage Data)
    {
        byte Slot_Antigo = Data.ReadByte(), Slot_Novo = Data.ReadByte();
        Lists.Structures.Hotbar Antigo = Player.Character(Index).Hotbar[Slot_Antigo];

        // Somente se necessário
        if (Player.Character(Index).Hotbar[Slot_Antigo].Slot == 0) return;
        if (Slot_Antigo == Slot_Novo) return;

        // Caso houver um item no novo slot, trocar ele para o velho
        if (Player.Character(Index).Hotbar[Slot_Novo].Slot > 0)
        {
            Player.Character(Index).Hotbar[Slot_Antigo].Slot = Player.Character(Index).Hotbar[Slot_Novo].Slot;
            Player.Character(Index).Hotbar[Slot_Antigo].Type = Player.Character(Index).Hotbar[Slot_Novo].Type;
        }
        else
        {
            Player.Character(Index).Hotbar[Slot_Antigo].Slot = 0;
            Player.Character(Index).Hotbar[Slot_Antigo].Type = 0;
        }

        // Muda o item de slot
        Player.Character(Index).Hotbar[Slot_Novo].Slot = Antigo.Slot;
        Player.Character(Index).Hotbar[Slot_Novo].Type = Antigo.Type;
        Sending.Player_Hotbar(Index);
    }

    private static void Hotbar_Use(byte Index, NetIncomingMessage Data)
    {
        byte Hotbar_Slot = Data.ReadByte();

        // Usa o item
        if (Player.Character(Index).Hotbar[Hotbar_Slot].Type == (byte)Game.Hotbar.Item)
            Player.UseItem(Index, Player.Character(Index).Hotbar[Hotbar_Slot].Slot);
    }
}