using System;
using System.IO;
using Lidgren.Network;

class NPC
{
    public enum Aggressiveness
    {
        Passive,
        AtacarAoVer,
        AtacarAoAtacado
    }

    public static short[] MaxVital(byte Index, short Map)
    {
        return Lists.NPC[Lists.Map[Map].NPC[Index].Index].Vital;
    }

    public static short Renegation(short Map_Num, byte Index, byte Vital)
    {
        Lists.Structures.NPCs Data = Lists.NPC[Lists.Map[Map_Num].Temp_NPC[Index].Index];

        // Calculates the most vital that the NPC has
        switch ((Game.Vital)Vital)
        {
            case Game.Vital.Life: return (short)(Data.Vital[Vital] * 0.05 + Data.Attribute[(byte)Game.Attributes.Vitality] * 0.3);
            case Game.Vital.Mana: return (short)(Data.Vital[Vital] * 0.05 + Data.Attribute[(byte)Game.Attributes.Intelligence] * 0.1);
        }

        return 0;
    }

    public static void Logic(short Map_Num)
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Lists.Map[Map_Num].Temp_NPC.GetUpperBound(0); i++)
        {
            Lists.Structures.Map_NPCs Data = Lists.Map[Map_Num].Temp_NPC[i];
            Lists.Structures.NPCs NPC_Data = Lists.NPC[Lists.Map[Map_Num].NPC[i].Index];

            //////////////////
            // Aparecimento //
            //////////////////
            if (Data.Index == 0)
            {
                if (Environment.TickCount > Data.Appearance_Time + (NPC_Data.Appearance * 1000)) Appearance(i, Map_Num);
            }
            else
            {
                byte TargetX = 0, TargetY = 0;
                bool[] PodeMover = new bool[(byte)Game.Location.Amount];
                short DistânciaX, DistânciaY;
                bool SeMoveu = false;
                bool Movimentar = false;

                /////////////////
                // Regeneração //
                /////////////////
                if (Environment.TickCount > Tie.Score_NPC_Reneration + 5000)
                    for (byte v = 0; v <= (byte)Game.Vital.Amount - 1; v++)
                        if (Data.Vital[v] < NPC_Data.Vital[v])
                        {
                            // Renera os Vital
                            Lists.Map[Map_Num].Temp_NPC[i].Vital[v] += Renegation(Map_Num, i, v);

                            // Impede que o valor passe do limite
                            if (Lists.Map[Map_Num].Temp_NPC[i].Vital[v] > NPC_Data.Vital[v])
                                Lists.Map[Map_Num].Temp_NPC[i].Vital[v] = NPC_Data.Vital[v];

                            // Envia os Data aos jogadores do mapa
                            Sending.Map_NPC_Vital(Map_Num, i);
                        }

                ///////////////
                // Movement //
                ///////////////
                // Atacar ao ver
                if (Lists.Map[Map_Num].Temp_NPC[i].Target_Index == 0 && NPC_Data.Aggressiveness == (byte)Aggressiveness.AtacarAoVer)
                    for (byte p = 1; p <= Game.BiggerIndex; p++)
                    {
                        // Verifica se o jogador está jogando e no mesmo mapa que o NPC
                        if (!Player.IsPlaying(p)) continue;
                        if (Player.Character(p).Map != Map_Num) continue;

                        // Distância entre o NPC e o jogador
                        DistânciaX = (short)(Data.X - Player.Character(p).X);
                        DistânciaY = (short)(Data.Y - Player.Character(p).Y);
                        if (DistânciaX < 0) DistânciaX *= -1;
                        if (DistânciaY < 0) DistânciaY *= -1;

                        // Se estiver no alcance, ir atrás do jogador
                        if (DistânciaX <= NPC_Data.View && DistânciaY <= NPC_Data.View)
                        {
                            Lists.Map[Map_Num].Temp_NPC[i].Target_Type = (byte)Game.Target.Player;
                            Lists.Map[Map_Num].Temp_NPC[i].Target_Index = p;
                            Data = Lists.Map[Map_Num].Temp_NPC[i];
                        }
                    }

                if (Data.Target_Type == (byte)Game.Target.Player)
                {
                    // Posição do Target
                    TargetX = Player.Character(Lists.Map[Map_Num].Temp_NPC[i].Target_Index).X;
                    TargetY = Player.Character(Lists.Map[Map_Num].Temp_NPC[i].Target_Index).Y;

                    // Verifica se o jogador ainda está disponível
                    if (!Player.IsPlaying(Data.Target_Index) || Player.Character(Data.Target_Index).Map != Map_Num)
                    {
                        Lists.Map[Map_Num].Temp_NPC[i].Target_Type = 0;
                        Lists.Map[Map_Num].Temp_NPC[i].Target_Index = 0;
                        Data = Lists.Map[Map_Num].Temp_NPC[i];
                    }
                }

                // Distância entre o NPC e o Target
                DistânciaX = (short)(Data.X - TargetX);
                DistânciaY = (short)(Data.Y - TargetY);
                if (DistânciaX < 0) DistânciaX *= -1;
                if (DistânciaY < 0) DistânciaY *= -1;

                // Verifica se o Target saiu do alcance
                if (DistânciaX > NPC_Data.View || DistânciaY > NPC_Data.View)
                {
                    Lists.Map[Map_Num].Temp_NPC[i].Target_Type = 0;
                    Lists.Map[Map_Num].Temp_NPC[i].Target_Index = 0;
                    Data = Lists.Map[Map_Num].Temp_NPC[i];
                }

                // Evita que ele se movimente sem sentido
                if (Data.Target_Index > 0)
                    Movimentar = true;
                else
                {
                    // Define o Target até a zona do NPC
                    if (Lists.Map[Map_Num].NPC[i].Zone > 0)
                        if (Lists.Map[Map_Num].Tile[Data.X, Data.Y].Zone != Lists.Map[Map_Num].NPC[i].Zone)
                            for (byte x2 = 0; x2 <= Lists.Map[Map_Num].Width; x2++)
                                for (byte y2 = 0; y2 <= Lists.Map[Map_Num].Height; y2++)
                                    if (Lists.Map[Map_Num].Tile[x2, y2].Zone == Lists.Map[Map_Num].NPC[i].Zone)
                                        if (!Map.Tile_Blocked(Map_Num, x2, y2))
                                        {
                                            TargetX = x2;
                                            TargetY = y2;
                                            Movimentar = true;
                                            break;
                                        }
                }

                // Movimenta o NPC até mais perto do Target
                if (Movimentar)
                {
                    // Verifica como pode se mover até o Target
                    if (Data.Y > TargetY) PodeMover[(byte)Game.Location.Above] = true;
                    if (Data.Y < TargetY) PodeMover[(byte)Game.Location.Below] = true;
                    if (Data.X > TargetX) PodeMover[(byte)Game.Location.Left] = true;
                    if (Data.X < TargetX) PodeMover[(byte)Game.Location.Right] = true;

                    // Aleatoriza a forma que ele vai se movimentar até o Target
                    if (Game.Aleatório.Next(0, 2) == 0)
                    {
                        for (byte d = 0; d <= (byte)Game.Location.Amount - 1; d++)
                            if (!SeMoveu && PodeMover[d] && Move(Map_Num, i, (Game.Location)d))
                                SeMoveu = true;
                    }
                    else
                        for (short d = (byte)Game.Location.Amount - 1; d >= 0; d--)
                            if (!SeMoveu && PodeMover[d] && Move(Map_Num, i, (Game.Location)d))
                                SeMoveu = true;
                }

                // Move-se aleatoriamente
                if (NPC_Data.Aggressiveness == (byte)Aggressiveness.Passive || Data.Target_Index == 0)
                    if (Game.Aleatório.Next(0, 3) == 0 && !SeMoveu)
                        Move(Map_Num, i, (Game.Location)Game.Aleatório.Next(0, 4), 1, true);
            }

            ////////////
            // Attack //
            ////////////
            short Next_X = Data.X, Next_Y = Data.Y;
            Map.NextTile(Data.Direction, ref Next_X, ref Next_Y);
            if (Data.Target_Type == (byte)Game.Target.Player)
            {
                // Checks if player is in front of NPC
                if (Map.ThereIsPlayer(Map_Num, Next_X, Next_Y) == Data.Target_Index) Attack_Player(Map_Num, i, Data.Target_Index);
            }
        }
    }

    public static void Appearance(byte Index, short Map)
    {
        byte x, y;

        // Antes verifica se tem algum local de aparecimento específico
        if (Lists.Map[Map].NPC[Index].Aparecer)
        {
            Appearance(Index, Map, Lists.Map[Map].NPC[Index].X, Lists.Map[Map].NPC[Index].Y);
            return;
        }

        // Causes it to appear in a random place
        for (byte i = 0; i <= 50; i++) //tries 50 times to appear in a random location
        {
            x = (byte)Game.Aleatório.Next(0, Lists.Map[Map].Width);
            y = (byte)Game.Aleatório.Next(0, Lists.Map[Map].Height);

            // Verifica se está dentro da zona
            if (Lists.Map[Map].NPC[Index].Zone > 0)
                if (Lists.Map[Map].Tile[x, y].Zone != Lists.Map[Map].NPC[Index].Zone)
                    continue;

            // Define os Data
            if (!global::Map.Tile_Blocked(Map, x, y))
            {
                Appearance(Index, Map, x, y);
                return;
            }
        }

        // Em último caso, tentar no primeiro lugar possível
        for (byte x2 = 0; x2 <= Lists.Map[Map].Width; x2++)
            for (byte y2 = 0; y2 <= Lists.Map[Map].Height; y2++)
                if (!global::Map.Tile_Blocked(Map, x2, y2))
                {
                    // Verifica se está dentro da zona
                    if (Lists.Map[Map].NPC[Index].Zone > 0)
                        if (Lists.Map[Map].Tile[x2, y2].Zone != Lists.Map[Map].NPC[Index].Zone)
                            continue;

                    // Define os Data
                    Appearance(Index, Map, x2, y2);
                    return;
                }
    }

    public static void Appearance(byte Index, short Map, byte x, byte y, Game.Location Direction = 0)
    {
        Lists.Structures.NPCs Data = Lists.NPC[Lists.Map[Map].NPC[Index].Index];
        short[] s = Data.Vital;

        // Define os Data
        Lists.Map[Map].Temp_NPC[Index].Index = Lists.Map[Map].NPC[Index].Index;
        Lists.Map[Map].Temp_NPC[Index].X = x;
        Lists.Map[Map].Temp_NPC[Index].Y = y;
        Lists.Map[Map].Temp_NPC[Index].Direction = Direction;
        Lists.Map[Map].Temp_NPC[Index].Vital = new short[(byte)Game.Vital.Amount];
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++) Lists.Map[Map].Temp_NPC[Index].Vital[i] = Data.Vital[i];

        // Envia os Data aos jogadores
        if (Network.Device != null) Sending.Map_NPC(Map, Index);
    }

    public static bool Move(short Map_Num, byte Index, Game.Location Direction, byte Movement = 1, bool ContarZona = false)
    {
        Lists.Structures.Map_NPCs Data = Lists.Map[Map_Num].Temp_NPC[Index];
        byte x = Data.X, y = Data.Y;
        short Next_X = x, Next_Y = y;

        // Define a direção do NPC
        Lists.Map[Map_Num].Temp_NPC[Index].Direction = Direction;
        Sending.Map_NPC_Direction(Map_Num, Index);

        // Nextimo azulejo
        Map.NextTile(Direction, ref Next_X, ref Next_Y);

        // Nextimo azulejo bloqueado ou fora do limite
        if (Map.ForaDoLimite(Map_Num, Next_X, Next_Y)) return false;
        if (Map.Tile_Blocked(Map_Num, x, y, Direction)) return false;

        // Verifica se está dentro da zona
        if (ContarZona)
            if (Lists.Map[Map_Num].Tile[Next_X, Next_Y].Zone != Lists.Map[Map_Num].NPC[Index].Zone)
                return false;

        // Movimenta o NPC
        Lists.Map[Map_Num].Temp_NPC[Index].X = (byte)Next_X;
        Lists.Map[Map_Num].Temp_NPC[Index].Y = (byte)Next_Y;
        Sending.Map_NPC_Movement(Map_Num, Index, Movement);
        return true;
    }

    public static void Attack_Player(short Map_Num, byte Index, byte Victim)
    {
        Lists.Structures.Map_NPCs Data = Lists.Map[Map_Num].Temp_NPC[Index];
        short x = Data.X, y = Data.Y;

        // Define o azujelo a frente do NPC
        Map.NextTile(Data.Direction, ref x, ref y);

        // Verifica se a Victim pode ser atacada
        if (Data.Index == 0) return;
        if (Environment.TickCount < Data.Attack_Time + 750) return;
        if (!Player.IsPlaying(Victim)) return;
        if (Lists.TempPlayer[Victim].GettingMap) return;
        if (Map_Num != Player.Character(Victim).Map) return;
        if (Player.Character(Victim).X != x || Player.Character(Victim).Y != y) return;
        if (Map.Tile_Blocked(Map_Num, Data.X, Data.Y, Data.Direction, false)) return;

        // Tempo de ataque 
        Lists.Map[Map_Num].Temp_NPC[Index].Attack_Time = Environment.TickCount;

        // Demonstra o ataque aos outros jogadores
        Sending.Map_NPC_Attack(Map_Num, Index, Victim, (byte)Game.Target.Player);

        // Calculation of damage
        short Damage = (short)(Lists.NPC[Data.Index].Attribute[(byte)Game.Attributes.Force] - Player.Character(Victim).Player_Defense);

        // Non-fatal injury
        if (Damage < Player.Character(Victim).Vital[(byte)Game.Vital.Life])
        {
            Player.Character(Victim).Vital[(byte)Game.Vital.Life] -= Damage;
            Sending.Player_Vital(Victim);
        }
        // FATALITY
        else
        {
            // Reset NPC Target
            Lists.Map[Map_Num].Temp_NPC[Index].Target_Type = 0;
            Lists.Map[Map_Num].Temp_NPC[Index].Target_Index = 0;

            // Kill the player
            Player.Died(Victim);
        }
    }

    public static void Died(short Map_Num, byte Index)
    {
        Lists.Structures.NPCs NPC = Lists.NPC[Lists.Map[Map_Num].Temp_NPC[Index].Index];

        // Solta os itens
        for (byte i = 0; i <= Game.Max_NPC_Queda - 1; i++)
            if (NPC.Queda[i].Item_Num > 0)
                if (Game.Aleatório.Next(NPC.Queda[i].Chance, 101) == 100)
                {
                    // Data do item
                    Lists.Structures.Map_Items Item = new Lists.Structures.Map_Items();
                    Item.Index = NPC.Queda[i].Item_Num;
                    Item.Amount = NPC.Queda[i].Amount;
                    Item.X = Lists.Map[Map_Num].Temp_NPC[Index].X;
                    Item.Y = Lists.Map[Map_Num].Temp_NPC[Index].Y;

                    // Solta
                    Lists.Map[Map_Num].Temp_Item.Add(Item);
                }

        // Envia os Data dos itens no chão para o mapa
        Sending.Map_Items(Map_Num);

        // Reseta os Data do NPC 
        Lists.Map[Map_Num].Temp_NPC[Index].Vital[(byte)Game.Vital.Life] = 0;
        Lists.Map[Map_Num].Temp_NPC[Index].Appearance_Time = Environment.TickCount;
        Lists.Map[Map_Num].Temp_NPC[Index].Index = 0;
        Lists.Map[Map_Num].Temp_NPC[Index].Target_Type = 0;
        Lists.Map[Map_Num].Temp_NPC[Index].Target_Index = 0;
        Sending.Map_NPC_Died(Map_Num, Index);
    }
}

partial class Read
{
    public static void NPCs()
    {
        Lists.NPC = new Lists.Structures.NPCs[Lists.Server_Data.Num_NPCs + 1];

        // Lê os Data
        for (byte i = 1; i <= Lists.NPC.GetUpperBound(0); i++)
            NPC(i);
    }

    public static void NPC(byte Index)
    {
        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Archive = new FileInfo(Directories.NPCs.FullName + Index + Directories.Format);
        BinaryReader Binary = new BinaryReader(Archive.OpenRead());

        // Redimensiona os valores necessários 
        Lists.NPC[Index].Vital = new short[(byte)Game.Vital.Amount];
        Lists.NPC[Index].Attribute = new short[(byte)Game.Attributes.Amount];
        Lists.NPC[Index].Queda = new Lists.Structures.NPC_Queda[Game.Max_NPC_Queda];

        // Lê os Data
        Lists.NPC[Index].Name = Binary.ReadString();
        Lists.NPC[Index].Texture = Binary.ReadInt16();
        Lists.NPC[Index].Aggressiveness= Binary.ReadByte();
        Lists.NPC[Index].Appearance = Binary.ReadByte();
        Lists.NPC[Index].View = Binary.ReadByte();
        Lists.NPC[Index].Experience = Binary.ReadByte();
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++) Lists.NPC[Index].Vital[i] = Binary.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Attributes.Amount - 1; i++) Lists.NPC[Index].Attribute[i] = Binary.ReadInt16();
        for (byte i = 0; i <= Game.Max_NPC_Queda - 1; i++)
        {
            Lists.NPC[Index].Queda[i].Item_Num = Binary.ReadInt16();
            Lists.NPC[Index].Queda[i].Amount = Binary.ReadInt16();
            Lists.NPC[Index].Queda[i].Chance = Binary.ReadByte();
        }

        // Fecha o sistema
        Binary.Dispose();
    }
}

partial class Sending
{
    public static void NPCs(byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.NPCs);
        Data.Write((byte)Lists.NPC.GetUpperBound(0));
        for (byte i = 1; i <= Lists.NPC.GetUpperBound(0); i++)
        {
            // General
            Data.Write(Lists.NPC[i].Name);
            Data.Write(Lists.NPC[i].Texture);
            Data.Write(Lists.NPC[i].Aggressiveness);
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Data.Write(Lists.NPC[i].Vital[n]);
        }
        Para(Index, Data);
    }

    public static void Map_NPCs(byte Index, short Map)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPCs);
        Data.Write((short)Lists.Map[Map].Temp_NPC.GetUpperBound(0));
        for (byte i = 1; i <= Lists.Map[Map].Temp_NPC.GetUpperBound(0); i++)
        {
            Data.Write(Lists.Map[Map].Temp_NPC[i].Index);
            Data.Write(Lists.Map[Map].Temp_NPC[i].X);
            Data.Write(Lists.Map[Map].Temp_NPC[i].Y);
            Data.Write((byte)Lists.Map[Map].Temp_NPC[i].Direction);
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Data.Write(Lists.Map[Map].Temp_NPC[i].Vital[n]);
        }
        Para(Index, Data);
    }

    public static void Map_NPC(short Map, byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC);
        Data.Write(Index);
        Data.Write(Lists.Map[Map].Temp_NPC[Index].Index);
        Data.Write(Lists.Map[Map].Temp_NPC[Index].X);
        Data.Write(Lists.Map[Map].Temp_NPC[Index].Y);
        Data.Write((byte)Lists.Map[Map].Temp_NPC[Index].Direction);
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Data.Write(Lists.Map[Map].Temp_NPC[Index].Vital[n]);
        ParaMap(Map, Data);
    }

    public static void Map_NPC_Movement(short Map, byte Index, byte Movement)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC_Movement);
        Data.Write(Index);
        Data.Write(Lists.Map[Map].Temp_NPC[Index].X);
        Data.Write(Lists.Map[Map].Temp_NPC[Index].Y);
        Data.Write((byte)Lists.Map[Map].Temp_NPC[Index].Direction);
        Data.Write(Movement);
        ParaMap(Map, Data);
    }

    public static void Map_NPC_Direction(short Map, byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC_Direction);
        Data.Write(Index);
        Data.Write((byte)Lists.Map[Map].Temp_NPC[Index].Direction);
        ParaMap(Map, Data);
    }

    public static void Map_NPC_Vital(short Map, byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC_Vital);
        Data.Write(Index);
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Data.Write(Lists.Map[Map].Temp_NPC[Index].Vital[n]);
        ParaMap(Map, Data);
    }

    public static void Map_NPC_Attack(short Map, byte Index, byte Victim, byte Victim_Type)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC_Attack);
        Data.Write(Index);
        Data.Write(Victim);
        Data.Write(Victim_Type);
        ParaMap(Map, Data);
    }

    public static void Map_NPC_Died(short Map, byte Index)
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Map_NPC_Died);
        Data.Write(Index);
        ParaMap(Map, Data);
    }
}