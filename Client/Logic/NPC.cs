using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

class NPC
{
    public static void Logic()
    {
        // Logic dos NPCs
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
            if (Lists.Map.Temp_NPC[i].Index > 0)
            {
                // Dano
                if (Lists.Map.Temp_NPC[i].Suffering + 325 < Environment.TickCount) Lists.Map.Temp_NPC[i].Suffering = 0;

                // Movement
                ProcessMovement(i);
            }
    }

    public static void ProcessMovement(byte Index)
    {
        byte Velocity = 0;
        short x = Lists.Map.Temp_NPC[Index].X2, y = Lists.Map.Temp_NPC[Index].Y2;

        // Reseta a Animation se necessário
        if (Lists.Map.Temp_NPC[Index].Animation == Game.Animation_Stop) Lists.Map.Temp_NPC[Index].Animation = Game.Animation_Right;

        // Define a Velocity que o Player se move
        switch (Lists.Map.Temp_NPC[Index].Movement)
        {
            case Game.Movements.Andando: Velocity = 2; break;
            case Game.Movements.Correndo: Velocity = 3; break;
            case Game.Movements.Parado:
                // Reseta os Data
                Lists.Map.Temp_NPC[Index].X2 = 0;
                Lists.Map.Temp_NPC[Index].Y2 = 0;
                return;
        }

        // Define a Position exata do Player
        switch (Lists.Map.Temp_NPC[Index].Direction)
        {
            case Game.Location.Above: Lists.Map.Temp_NPC[Index].Y2 -= Velocity; break;
            case Game.Location.Below: Lists.Map.Temp_NPC[Index].Y2 += Velocity; break;
            case Game.Location.Right: Lists.Map.Temp_NPC[Index].X2 += Velocity; break;
            case Game.Location.Left: Lists.Map.Temp_NPC[Index].X2 -= Velocity; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Map.Temp_NPC[Index].X2 < 0) Lists.Map.Temp_NPC[Index].X2 = 0;
        if (x < 0 && Lists.Map.Temp_NPC[Index].X2 > 0) Lists.Map.Temp_NPC[Index].X2 = 0;
        if (y > 0 && Lists.Map.Temp_NPC[Index].Y2 < 0) Lists.Map.Temp_NPC[Index].Y2 = 0;
        if (y < 0 && Lists.Map.Temp_NPC[Index].Y2 > 0) Lists.Map.Temp_NPC[Index].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Map.Temp_NPC[Index].Direction == Game.Location.Right || Lists.Map.Temp_NPC[Index].Direction == Game.Location.Below)
        {
            if (Lists.Map.Temp_NPC[Index].X2 < 0 || Lists.Map.Temp_NPC[Index].Y2 < 0)
                return;
        }
        else if (Lists.Map.Temp_NPC[Index].X2 > 0 || Lists.Map.Temp_NPC[Index].Y2 > 0)
            return;

        // Define as animações
        Lists.Map.Temp_NPC[Index].Movement = Game.Movements.Parado;
        if (Lists.Map.Temp_NPC[Index].Animation == Game.Animation_Left)
            Lists.Map.Temp_NPC[Index].Animation = Game.Animation_Right;
        else
            Lists.Map.Temp_NPC[Index].Animation = Game.Animation_Left;
    }
}

partial class Receiving
{
    public static void NPCs(NetIncomingMessage Data)
    {
        // Amount
        Lists.NPC = new Lists.Structures.NPCs[Data.ReadByte() + 1];

        // Lê os Data de todos
        for (byte i = 1; i <= Lists.NPC.GetUpperBound(0); i++)
        {
            // General
            Lists.NPC[i].Name = Data.ReadString();
            Lists.NPC[i].Texture = Data.ReadInt16();
            Lists.NPC[i].Type = Data.ReadByte();

            // Vital
            Lists.NPC[i].Vital = new short[(byte)Game.Vital.Amount];
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Lists.NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Map_NPCs(NetIncomingMessage Data)
    {
        // Lê os Data
        Lists.Map.Temp_NPC = new Lists.Structures.Map_NPCs[Data.ReadInt16() + 1];
        for (byte i = 1; i <= Lists.Map.Temp_NPC.GetUpperBound(0); i++)
        {
            Lists.Map.Temp_NPC[i].X2 = 0;
            Lists.Map.Temp_NPC[i].Y2 = 0;
            Lists.Map.Temp_NPC[i].Index = Data.ReadInt16();
            Lists.Map.Temp_NPC[i].X = Data.ReadByte();
            Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
            Lists.Map.Temp_NPC[i].Direction = (Game.Location)Data.ReadByte();

            // Vital
            Lists.Map.Temp_NPC[i].Vital = new short[(byte)Game.Vital.Amount];
            for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
                Lists.Map.Temp_NPC[i].Vital[n] = Data.ReadInt16();
        }
    }

    public static void Map_NPC(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Index = Data.ReadInt16();
        Lists.Map.Temp_NPC[i].X = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Location)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++) Lists.Map.Temp_NPC[i].Vital[n] = Data.ReadInt16();
    }

    public static void Map_NPC_Movement(NetIncomingMessage Data)
    {
        // Lê os Data
        byte i = Data.ReadByte();
        byte x = Lists.Map.Temp_NPC[i].X, y = Lists.Map.Temp_NPC[i].Y;
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
        Lists.Map.Temp_NPC[i].X = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Y = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Location)Data.ReadByte();
        Lists.Map.Temp_NPC[i].Movement = (Game.Movements)Data.ReadByte();

        // Position exata do Player
        if (x != Lists.Map.Temp_NPC[i].X || y != Lists.Map.Temp_NPC[i].Y)
            switch (Lists.Map.Temp_NPC[i].Direction)
            {
                case Game.Location.Above: Lists.Map.Temp_NPC[i].Y2 = Game.Grade; break;
                case Game.Location.Below: Lists.Map.Temp_NPC[i].Y2 = Game.Grade * -1; break;
                case Game.Location.Right: Lists.Map.Temp_NPC[i].X2 = Game.Grade * -1; break;
                case Game.Location.Left: Lists.Map.Temp_NPC[i].X2 = Game.Grade; break;
            }
    }

    public static void Map_NPC_Attack(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Type = Data.ReadByte();

        // Inicia o Attack
        Lists.Map.Temp_NPC[Index].Atacando = true;
        Lists.Map.Temp_NPC[Index].Attack_Time = Environment.TickCount;

        // Suffering dano
        if (Vítima > 0)
            if (Vítima_Type == (byte)Game.Alvo.Player)
                Lists.Player[Vítima].Suffering = Environment.TickCount;
            else if (Vítima_Type == (byte)Game.Alvo.NPC)
                Lists.Map.Temp_NPC[Vítima].Suffering = Environment.TickCount;
    }

    public static void Map_NPC_Direction(NetIncomingMessage Data)
    {
        // Define a Direction de determinado NPC
        byte i = Data.ReadByte();
        Lists.Map.Temp_NPC[i].Direction = (Game.Location)Data.ReadByte();
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
    }

    public static void Map_NPC_Vital(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Define os Vital de determinado NPC
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
            Lists.Map.Temp_NPC[Index].Vital[n] = Data.ReadInt16();
    }

    public static void Map_NPC_Died(NetIncomingMessage Data)
    {
        byte i = Data.ReadByte();

        // Limpa os Data do NPC
        Lists.Map.Temp_NPC[i].X2 = 0;
        Lists.Map.Temp_NPC[i].Y2 = 0;
        Lists.Map.Temp_NPC[i].Index = 0;
        Lists.Map.Temp_NPC[i].X = 0;
        Lists.Map.Temp_NPC[i].Y = 0;
        Lists.Map.Temp_NPC[i].Vital = new short[(byte)Game.Vital.Amount];
    }
}

partial class Graphics
{
    public static void NPC(byte Index)
    {
        int x2 = Lists.Map.Temp_NPC[Index].X2, y2 = Lists.Map.Temp_NPC[Index].Y2;
        byte Coluna = 0;
        bool Suffering = false;
        short Texture = Lists.NPC[Lists.Map.Temp_NPC[Index].Index].Texture;

        // Previni sobrecargas
        if (Texture <= 0 || Texture > Tex_Character.GetUpperBound(0)) return;

        // Define a Animation
        if (Lists.Map.Temp_NPC[Index].Atacando && Lists.Map.Temp_NPC[Index].Attack_Time + Game.Attack_Velocity / 2 > Environment.TickCount)
            Coluna = Game.Animation_Attack;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Map.Temp_NPC[Index].Animation;
            else if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Map.Temp_NPC[Index].Animation;
            else if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Map.Temp_NPC[Index].Animation;
            else if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Map.Temp_NPC[Index].Animation;
        }

        // Demonstra que o Character está Suffering dano
        if (Lists.Map.Temp_NPC[Index].Suffering > 0) Suffering = true;

        // Desenha o Player
        int x = Lists.Map.Temp_NPC[Index].X * Game.Grade + x2;
        int y = Lists.Map.Temp_NPC[Index].Y * Game.Grade + y2;
        Character(Texture, new Point(Game.ConvertX(x), Game.ConvertY(y)), Lists.Map.Temp_NPC[Index].Direction, Coluna, Suffering);
        NPC_Name(Index, x, y);
        NPC_Bars(Index, x, y);
    }

    public static void NPC_Name(byte Index, int x, int y)
    {
        Point Position = new Point(); SFML.Graphics.Color Cor;
        short NPC_Num = Lists.Map.Temp_NPC[Index].Index;
        int Name_Size = Tools.MeasureText_Width(Lists.NPC[NPC_Num].Name);
        Texture Texture = Tex_Character[Lists.NPC[NPC_Num].Texture];

        // Position do Text
        Position.X = x + MySize(Texture).Width / Game.Animation_Amount / 2 - Name_Size / 2;
        Position.Y = y - MySize(Texture).Height / Game.Animation_Amount / 2;

        // Cor do Text
        switch ((Game.NPCs)Lists.NPC[NPC_Num].Type)
        {
            case Game.NPCs.Passivo: Cor = SFML.Graphics.Color.White; break;
            case Game.NPCs.Atacado: Cor = SFML.Graphics.Color.Red; break;
            case Game.NPCs.AoVer: Cor = new SFML.Graphics.Color(228, 120, 51); break;
            default: Cor = SFML.Graphics.Color.White; break;
        }

        // Desenha o Text
        Desenhar(Lists.NPC[NPC_Num].Name, Game.ConvertX(Position.X), Game.ConvertY(Position.Y), Cor);
    }

    public static void NPC_Bars(byte Index, int x, int y)
    {
        Lists.Structures.Map_NPCs NPC = Lists.Map.Temp_NPC[Index];
        int Name_Size = Tools.MeasureText_Width(Lists.NPC[NPC.Index].Name);
        Texture Texture = Tex_Character[Lists.NPC[NPC.Index].Texture];
        short Valor = NPC.Vital[(byte)Game.Vital.Life];

        // Apenas se necessário
        if (Valor <= 0 || Valor >= Lists.NPC[NPC.Index].Vital[(byte)Game.Vital.Life]) return;

        // Position
        Point Position = new Point(Game.ConvertX(x), Game.ConvertY(y) + MySize(Texture).Height / Game.Animation_Amount + 4);
        int Width_Complete = MySize(Texture).Width / Game.Animation_Amount;
        int Width = (Valor * Width_Complete) / Lists.NPC[NPC.Index].Vital[(byte)Game.Vital.Life];

        // Desenha a barra 
        Desenhar(Tex_Bars, Position.X, Position.Y, 0, 4, Width_Complete, 4);
        Desenhar(Tex_Bars, Position.X, Position.Y, 0, 0, Width, 4);
    }
}