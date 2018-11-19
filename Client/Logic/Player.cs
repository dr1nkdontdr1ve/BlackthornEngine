using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

public class Player
{
    // O maior Index dos Playeres conectados
    public static byte BiggerIndex;

    // Inventory
    public static Lists.Structures.Inventory[] Inventory = new Lists.Structures.Inventory[Game.Max_Inventory + 1];
    public static byte Inventory_Moving;

    // Hotbar
    public static Lists.Structures.Hotbar[] Hotbar = new Lists.Structures.Hotbar[Game.Max_Hotbar+1];
    public static byte Hotbar_Moving;

    // The Player itself
    public static byte MyIndex;
    public static Lists.Structures.Player Eu
    {
        get
        {
            return Lists.Player[MyIndex];
        }
        set
        {
            Lists.Player[MyIndex] = value;
        }
    }

    public static bool IsPlaying(byte Index)
    {
        // Verifica se o Player está dentro do Game
        if (MyIndex > 0 && !string.IsNullOrEmpty(Lists.Player[Index].Name))
            return true;
        else
            return false;
    }

    public static short Character_Texture(byte Index)
    {
        byte Classe = Lists.Player[Index].Classe;

        // Retorna com o valor da Texture
        if (Lists.Player[Index].Genre)
            return Lists.Classe[Classe].Texture_Male;
        else
            return Lists.Classe[Classe].Texture_Female;
    }

    public static void Logic()
    {
        // Verificações
        CheckMovement();
        CheckAttack();

        // Logic dos Playeres
        for (byte i = 1; i <= Player.BiggerIndex; i++)
        {
            // Dano
            if (Lists.Player[i].Suffering + 325 < Environment.TickCount) Lists.Player[i].Suffering = 0;

            // Movimentaçãp
            ProcessMovement(i);
        }
    }

    public static bool TryMove()
    {
        // Se estiver pressionando alguma tecla, está tentando se Move
        if (Game.HoldKey_Above || Game.HoldKey_Below || Game.HoldKey_Left || Game.HoldKey_Right)
            return true;
        else
            return false;
    }

    public static bool CanMove()
    {
        // Não Move se já estiver tentando Moving-se
        if (Lists.Player[MyIndex].Movement != Game.Movements.Parado)
            return false;

        return true;
    }

    public static void CheckMovement()
    {
        if (Eu.Movement > 0) return;

        // Move o Character
        if (Game.HoldKey_Above) Move(Game.Location.Above);
        else if (Game.HoldKey_Below) Move(Game.Location.Below);
        else if (Game.HoldKey_Left) Move(Game.Location.Left);
        else if (Game.HoldKey_Right) Move(Game.Location.Right);
    }

    public static void Move(Game.Location Direction)
    {
        // Verifica se o Player pode se Move
        if (!CanMove()) return;

        // Define a Direction do Player
        if (Lists.Player[MyIndex].Direction != Direction)
        {
            Lists.Player[MyIndex].Direction = Direction;
            Sending.Player_Direction();
        }

        // Verifica se o Tile seguinte está livre
        if (Map.Tile_Blocked(Lists.Player[MyIndex].Map, Lists.Player[MyIndex].X, Lists.Player[MyIndex].Y, Direction)) return;

        // Define a Velocity que o Player se move
        if (Game.HoldKey_Shift)
            Lists.Player[MyIndex].Movement = Game.Movements.Correndo;
        else
            Lists.Player[MyIndex].Movement = Game.Movements.Andando;

        // Movement o Player
        Sending.Player_Move();

        // Define a Position exata do Player
        switch (Direction)
        {
            case Game.Location.Above: Lists.Player[MyIndex].Y2 = Game.Grade; Lists.Player[MyIndex].Y -= 1; break;
            case Game.Location.Below: Lists.Player[MyIndex].Y2 = Game.Grade * -1; Lists.Player[MyIndex].Y += 1; break;
            case Game.Location.Right: Lists.Player[MyIndex].X2 = Game.Grade * -1; Lists.Player[MyIndex].X += 1; break;
            case Game.Location.Left: Lists.Player[MyIndex].X2 = Game.Grade; Lists.Player[MyIndex].X -= 1; break;
        }
    }

    public static void ProcessMovement(byte Index)
    {
        byte Velocity = 0;
        short x = Lists.Player[Index].X2, y = Lists.Player[Index].Y2;

        // Reseta a Animation se necessário
        if (Lists.Player[Index].Animation == Game.Animation_Stop) Lists.Player[Index].Animation = Game.Animation_Right;

        // Define a Velocity que o Player se move
        switch (Lists.Player[Index].Movement)
        {
            case Game.Movements.Andando: Velocity = 2; break;
            case Game.Movements.Correndo: Velocity = 3; break;
            case Game.Movements.Parado:
                // Reseta os Data
                Lists.Player[Index].X2 = 0;
                Lists.Player[Index].Y2 = 0;
                return;
        }

        // Define a Position exata do Player
        switch (Lists.Player[Index].Direction)
        {
            case Game.Location.Above: Lists.Player[Index].Y2 -= Velocity; break;
            case Game.Location.Below: Lists.Player[Index].Y2 += Velocity; break;
            case Game.Location.Right: Lists.Player[Index].X2 += Velocity; break;
            case Game.Location.Left: Lists.Player[Index].X2 -= Velocity; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Player[Index].X2 < 0) Lists.Player[Index].X2 = 0;
        if (x < 0 && Lists.Player[Index].X2 > 0) Lists.Player[Index].X2 = 0;
        if (y > 0 && Lists.Player[Index].Y2 < 0) Lists.Player[Index].Y2 = 0;
        if (y < 0 && Lists.Player[Index].Y2 > 0) Lists.Player[Index].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Player[Index].Direction == Game.Location.Right || Lists.Player[Index].Direction == Game.Location.Below)
        {
            if (Lists.Player[Index].X2 < 0 || Lists.Player[Index].Y2 < 0)
                return;
        }
        else if (Lists.Player[Index].X2 > 0 || Lists.Player[Index].Y2 > 0)
            return;

        // Define as animações
        Lists.Player[Index].Movement = Game.Movements.Parado;
        if (Lists.Player[Index].Animation == Game.Animation_Left)
            Lists.Player[Index].Animation = Game.Animation_Right;
        else
            Lists.Player[Index].Animation = Game.Animation_Left;
    }

    public static void CheckAttack()
    {
        // Reseta o Attack
        if (Eu.Attack_Time + Game.Attack_Velocity < Environment.TickCount)
        {
            Eu.Attack_Time = 0;
            Eu.Atacando = false;
        }

        // Only if you are pressing the attack key and are not attacking
        if (!Game.HoldKey_Control) return;
        if (Eu.Attack_Time > 0) return;

        //Sends the data to the server
        Eu.Attack_Time = Environment.TickCount;
        Sending.Player_Attack();
    }

    public static void CollectItem()
    {
        bool TemItem = false, TemEspaço = false;

        // Previni erros
        if (Tools.CurrentWindow != Tools.Windows.Game) return;

        // Check if you have any items in the coordinates
        for (byte i = 1; i <= Lists.Map.Temp_Item.GetUpperBound(0); i++)
            if (Lists.Map.Temp_Item[i].X == Eu.X && Lists.Map.Temp_Item[i].Y == Eu.Y)
                TemItem = true;

        // Verifica se tem algum espaço vazio no Inventory
        for (byte i = 1; i <= Game.Max_Inventory; i++)
            if (Inventory[i].Item_Num == 0)
                TemEspaço = true;

        // Somente se necessário
        if (!TemItem) return;
        if (!TemEspaço) return;
        if (Environment.TickCount <= Eu.Coletar_Time + 250) return;
        if (Panels.Locate("Chat").General.Visible) return;

        // Coleta o item
        Sending.CollectItem();
        Eu.Coletar_Time = Environment.TickCount;
    }
}

partial class Sending
{
    public static void Player_Direction()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Direction);
        Data.Write((byte)Player.Eu.Direction);
        Package(Data);
    }

    public static void Player_Move()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Move);
        Data.Write(Player.Eu.X);
        Data.Write(Player.Eu.Y);
        Data.Write((byte)Player.Eu.Movement);
        Package(Data);
    }

    public static void Player_Attack()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Packages.Player_Attack);
        Package(Data);
    }
}

partial class Receiving
{
    private static void Player_Data(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Defini os Data do Player
        Lists.Player[Index].Name = Data.ReadString();
        Lists.Player[Index].Classe = Data.ReadByte();
        Lists.Player[Index].Genre = Data.ReadBoolean();
        Lists.Player[Index].Level = Data.ReadInt16();
        Lists.Player[Index].Map = Data.ReadInt16();
        Lists.Player[Index].X = Data.ReadByte();
        Lists.Player[Index].Y = Data.ReadByte();
        Lists.Player[Index].Direction = (Game.Location)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
        {
            Lists.Player[Index].Vital[n] = Data.ReadInt16();
            Lists.Player[Index].Max_Vital[n] = Data.ReadInt16();
        }
        for (byte n = 0; n <= (byte)Game.Attributes.Amount - 1; n++) Lists.Player[Index].Attribute[n] = Data.ReadInt16();
        for (byte n = 0; n <= (byte)Game.Equipments.Amount - 1; n++) Lists.Player[Index].Equipment[n] = Data.ReadInt16();
    }

    private static void Player_Position(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Defini os Data do Player
        Lists.Player[Index].X = Data.ReadByte();
        Lists.Player[Index].Y = Data.ReadByte();
        Lists.Player[Index].Direction = (Game.Location)Data.ReadByte();

        // Para a Movement
        Lists.Player[Index].X2 = 0;
        Lists.Player[Index].Y2 = 0;
        Lists.Player[Index].Movement = Game.Movements.Parado;
    }

    private static void Player_Vital(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Define os Data
        for (byte i = 0; i <= (byte)Game.Vital.Amount - 1; i++)
        {
            Lists.Player[Index].Vital[i] = Data.ReadInt16();
            Lists.Player[Index].Max_Vital[i] = Data.ReadInt16();
        }
    }

    private static void Player_Equipments(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Define os Data
        for (byte i = 0; i <= (byte)Game.Equipments.Amount - 1; i++)  Lists.Player[Index].Equipment[i] = Data.ReadInt16();
    }

    private static void Player_Exited(NetIncomingMessage Data)
    {
        // Limpa os Data do Player
        Clean.Player(Data.ReadByte());
    }

    public static void Player_Move(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Move o Player
        Lists.Player[Index].X = Data.ReadByte();
        Lists.Player[Index].Y = Data.ReadByte();
        Lists.Player[Index].Direction = (Game.Location)Data.ReadByte();
        Lists.Player[Index].Movement = (Game.Movements)Data.ReadByte();
        Lists.Player[Index].X2 = 0;
        Lists.Player[Index].Y2 = 0;

        // Position exata do Player
        switch (Lists.Player[Index].Direction)
        {
            case Game.Location.Above: Lists.Player[Index].Y2 = Game.Grade; break;
            case Game.Location.Below: Lists.Player[Index].Y2 = Game.Grade * -1; break;
            case Game.Location.Right: Lists.Player[Index].X2 = Game.Grade * -1; break;
            case Game.Location.Left: Lists.Player[Index].X2 = Game.Grade; break;
        }
    }

    public static void Player_Direction(NetIncomingMessage Data)
    {
        // Define a Direction de determinado Player
        Lists.Player[Data.ReadByte()].Direction = (Game.Location)Data.ReadByte();
    }

    public static void Player_Attack(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Type = Data.ReadByte();

        // Inicia o Attack
        Lists.Player[Index].Atacando = true;
        Lists.Player[Index].Attack_Time = Environment.TickCount;

        // Suffering dano
        if (Vítima > 0)
            if (Vítima_Type == (byte)Game.Alvo.Player)
                Lists.Player[Vítima].Suffering = Environment.TickCount;
            else if (Vítima_Type == (byte)Game.Alvo.NPC)
                Lists.Map.Temp_NPC[Vítima].Suffering = Environment.TickCount;
    }

    public static void Player_Experience(NetIncomingMessage Data)
    {
        // Define os Data
        Player.Eu.Experience = Data.ReadInt16();
        Player.Eu.ExpNecessária = Data.ReadInt16();
        Player.Eu.Pontos = Data.ReadByte();
    }

    private static void Player_Inventory(NetIncomingMessage Data)
    {
        // Define os Data
        for (byte i = 1; i <= Game.Max_Inventory; i++)
        {
            Player.Inventory[i].Item_Num = Data.ReadInt16();
            Player.Inventory[i].Amount = Data.ReadInt16();
        }
    }

    private static void Player_Hotbar(NetIncomingMessage Data)
    {
        // Define os Data
        for (byte i = 1; i <= Game.Max_Hotbar; i++)
        {
            Player.Hotbar[i].Type = Data.ReadByte();
            Player.Hotbar[i].Slot = Data.ReadByte();
        }
    }
}

partial class Graphics
{
    public static void Player_Character(byte Index)
    {
        byte Coluna = Game.Animation_Stop;
        int x, y;
        short x2 = Lists.Player[Index].X2, y2 = Lists.Player[Index].Y2;
        bool Suffering = false;
        short Texture = Player.Character_Texture(Index);

        // Previni sobrecargas
        if (Texture <= 0 || Texture > Tex_Character.GetUpperBound(0)) return;

        // Define a Animation
        if (Lists.Player[Index].Atacando && Lists.Player[Index].Attack_Time + Game.Attack_Velocity / 2 > Environment.TickCount)
            Coluna = Game.Animation_Attack;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Player[Index].Animation;
            if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Player[Index].Animation;
            if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Player[Index].Animation;
            if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Player[Index].Animation;
        }

        // Demonstra que o Character está Suffering dano
        if (Lists.Player[Index].Suffering > 0) Suffering = true;

        // Desenha o Player
        x = Lists.Player[Index].X * Game.Grade + Lists.Player[Index].X2;
        y = Lists.Player[Index].Y * Game.Grade + Lists.Player[Index].Y2;
        Character(Texture, new Point(Game.ConvertX(x), Game.ConvertY(y)), Lists.Player[Index].Direction, Coluna, Suffering);
        Player_Name(Index, x, y);
        Player_Bars(Index, x, y);
    }

    public static void Player_Bars(byte Index, int x, int y)
    {
        Size Character_Size = MySize(Tex_Character[Player.Character_Texture(Index)]);
        Point Position = new Point(Game.ConvertX(x), Game.ConvertY(y) + Character_Size.Height / Game.Animation_Amount + 4);
        int Width_Complete = Character_Size.Width / Game.Animation_Amount;
        short Contagem = Lists.Player[Index].Vital[(byte)Game.Vital.Life];

        // Apenas se necessário
        if (Contagem <= 0 || Contagem >= Lists.Player[Index].Max_Vital[(byte)Game.Vital.Life]) return;

        // Cálcula a Width da barra
        int Width = (Contagem * Width_Complete) / Lists.Player[Index].Max_Vital[(byte)Game.Vital.Life];

        // Desenha as Bars 
        Desenhar(Tex_Bars, Position.X, Position.Y, 0, 4, Width_Complete, 4);
        Desenhar(Tex_Bars, Position.X, Position.Y, 0, 0, Width, 4);
    }

    public static void Player_Name(byte Index, int x, int y)
    {
        Texture Texture = Tex_Character[Player.Character_Texture(Index)];
        int Name_Size = Tools.MeasureText_Width(Lists.Player[Index].Name);

        // Position do Text
        Point Position = new Point();
        Position.X = x + MySize(Texture).Width / Game.Animation_Amount / 2 - Name_Size / 2;
        Position.Y = y - MySize(Texture).Height / Game.Animation_Amount / 2;

        // Cor do Text
        SFML.Graphics.Color Cor;
        if (Index == Player.MyIndex)
            Cor = SFML.Graphics.Color.Yellow;
        else
            Cor = SFML.Graphics.Color.White;

        // Desenha o Text
        Desenhar(Lists.Player[Index].Name, Game.ConvertX(Position.X), Game.ConvertY(Position.Y), Cor);
    }
}