using System;
using System.Drawing;
using SFML.Graphics;
using Lidgren.Network;

public class Player
{
    // O maior Index dos Playeres conectados
    public static byte MaiorIndex;

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
        if (Lists.Player[Index].Gênero)
            return Lists.Classe[Classe].Texture_Masculina;
        else
            return Lists.Classe[Classe].Texture_Feminina;
    }

    public static void Logic()
    {
        // Verificações
        VerificarMovimentação();
        VerificarAtaque();

        // Lógica dos Playeres
        for (byte i = 1; i <= Player.MaiorIndex; i++)
        {
            // Dano
            if (Lists.Player[i].Sofrendo + 325 < Environment.TickCount) Lists.Player[i].Sofrendo = 0;

            // Movimentaçãp
            ProcessarMovimento(i);
        }
    }

    public static bool TentandoMover()
    {
        // Se estiver pressionando alguma tecla, está tentando se mover
        if (Game.Pressionado_Acima || Game.Pressionado_Abaixo || Game.Pressionado_Esquerda || Game.Pressionado_Direita)
            return true;
        else
            return false;
    }

    public static bool PodeMover()
    {
        // Não mover se já estiver tentando movimentar-se
        if (Lists.Player[MyIndex].Movimento != Game.Movimentos.Parado)
            return false;

        return true;
    }

    public static void VerificarMovimentação()
    {
        if (Eu.Movimento > 0) return;

        // Move o Character
        if (Game.Pressionado_Acima) Mover(Game.Direções.Acima);
        else if (Game.Pressionado_Abaixo) Mover(Game.Direções.Abaixo);
        else if (Game.Pressionado_Esquerda) Mover(Game.Direções.Esquerda);
        else if (Game.Pressionado_Direita) Mover(Game.Direções.Direita);
    }

    public static void Mover(Game.Direções Direction)
    {
        // Verifica se o Player pode se mover
        if (!PodeMover()) return;

        // Define a Direction do Player
        if (Lists.Player[MyIndex].Direction != Direction)
        {
            Lists.Player[MyIndex].Direction = Direction;
            Sending.Player_Direction();
        }

        // Verifica se o azulejo seguinte está livre
        if (Map.Azulejo_Bloqueado(Lists.Player[MyIndex].Map, Lists.Player[MyIndex].X, Lists.Player[MyIndex].Y, Direction)) return;

        // Define a velocidade que o Player se move
        if (Game.Pressionado_Shift)
            Lists.Player[MyIndex].Movimento = Game.Movimentos.Correndo;
        else
            Lists.Player[MyIndex].Movimento = Game.Movimentos.Andando;

        // Movimento o Player
        Sending.Player_Mover();

        // Define a Posição exata do Player
        switch (Direction)
        {
            case Game.Direções.Acima: Lists.Player[MyIndex].Y2 = Game.Grade; Lists.Player[MyIndex].Y -= 1; break;
            case Game.Direções.Abaixo: Lists.Player[MyIndex].Y2 = Game.Grade * -1; Lists.Player[MyIndex].Y += 1; break;
            case Game.Direções.Direita: Lists.Player[MyIndex].X2 = Game.Grade * -1; Lists.Player[MyIndex].X += 1; break;
            case Game.Direções.Esquerda: Lists.Player[MyIndex].X2 = Game.Grade; Lists.Player[MyIndex].X -= 1; break;
        }
    }

    public static void ProcessarMovimento(byte Index)
    {
        byte Velocidade = 0;
        short x = Lists.Player[Index].X2, y = Lists.Player[Index].Y2;

        // Reseta a animação se necessário
        if (Lists.Player[Index].Animação == Game.Animação_Parada) Lists.Player[Index].Animação = Game.Animação_Direita;

        // Define a velocidade que o Player se move
        switch (Lists.Player[Index].Movimento)
        {
            case Game.Movimentos.Andando: Velocidade = 2; break;
            case Game.Movimentos.Correndo: Velocidade = 3; break;
            case Game.Movimentos.Parado:
                // Reseta os Data
                Lists.Player[Index].X2 = 0;
                Lists.Player[Index].Y2 = 0;
                return;
        }

        // Define a Posição exata do Player
        switch (Lists.Player[Index].Direction)
        {
            case Game.Direções.Acima: Lists.Player[Index].Y2 -= Velocidade; break;
            case Game.Direções.Abaixo: Lists.Player[Index].Y2 += Velocidade; break;
            case Game.Direções.Direita: Lists.Player[Index].X2 += Velocidade; break;
            case Game.Direções.Esquerda: Lists.Player[Index].X2 -= Velocidade; break;
        }

        // Verifica se não passou do limite
        if (x > 0 && Lists.Player[Index].X2 < 0) Lists.Player[Index].X2 = 0;
        if (x < 0 && Lists.Player[Index].X2 > 0) Lists.Player[Index].X2 = 0;
        if (y > 0 && Lists.Player[Index].Y2 < 0) Lists.Player[Index].Y2 = 0;
        if (y < 0 && Lists.Player[Index].Y2 > 0) Lists.Player[Index].Y2 = 0;

        // Alterar as animações somente quando necessário
        if (Lists.Player[Index].Direction == Game.Direções.Direita || Lists.Player[Index].Direction == Game.Direções.Abaixo)
        {
            if (Lists.Player[Index].X2 < 0 || Lists.Player[Index].Y2 < 0)
                return;
        }
        else if (Lists.Player[Index].X2 > 0 || Lists.Player[Index].Y2 > 0)
            return;

        // Define as animações
        Lists.Player[Index].Movimento = Game.Movimentos.Parado;
        if (Lists.Player[Index].Animação == Game.Animação_Esquerda)
            Lists.Player[Index].Animação = Game.Animação_Direita;
        else
            Lists.Player[Index].Animação = Game.Animação_Esquerda;
    }

    public static void VerificarAtaque()
    {
        // Reseta o ataque
        if (Eu.Ataque_Tempo + Game.Ataque_Velocidade < Environment.TickCount)
        {
            Eu.Ataque_Tempo = 0;
            Eu.Atacando = false;
        }

        // Only if you are pressing the attack key and are not attacking
        if (!Game.Pressionado_Control) return;
        if (Eu.Ataque_Tempo > 0) return;

        //Sends the data to the server
        Eu.Ataque_Tempo = Environment.TickCount;
        Sending.Player_Atacar();
    }

    public static void ColetarItem()
    {
        bool TemItem = false, TemEspaço = false;

        // Previni erros
        if (Tools.JanelaAtual != Tools.Janelas.Game) return;

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
        if (Environment.TickCount <= Eu.Coletar_Tempo + 250) return;
        if (Panels.Encontrar("Chat").General.Visível) return;

        // Coleta o item
        Sending.ColetarItem();
        Eu.Coletar_Tempo = Environment.TickCount;
    }
}

partial class Sending
{
    public static void Player_Direction()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Player_Direction);
        Data.Write((byte)Player.Eu.Direction);
        Pacote(Data);
    }

    public static void Player_Move()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Player_Mover);
        Data.Write(Player.Eu.X);
        Data.Write(Player.Eu.Y);
        Data.Write((byte)Player.Eu.Movimento);
        Pacote(Data);
    }

    public static void Player_Attack()
    {
        NetOutgoingMessage Data = Network.Device.CreateMessage();

        // Envia os Data
        Data.Write((byte)Pacotes.Player_Atacar);
        Pacote(Data);
    }
}

partial class Receber
{
    private static void Player_Data(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Defini os Data do Player
        Lists.Player[Index].Name = Data.ReadString();
        Lists.Player[Index].Classe = Data.ReadByte();
        Lists.Player[Index].Gênero = Data.ReadBoolean();
        Lists.Player[Index].Level = Data.ReadInt16();
        Lists.Player[Index].Map = Data.ReadInt16();
        Lists.Player[Index].X = Data.ReadByte();
        Lists.Player[Index].Y = Data.ReadByte();
        Lists.Player[Index].Direction = (Game.Direções)Data.ReadByte();
        for (byte n = 0; n <= (byte)Game.Vital.Amount - 1; n++)
        {
            Lists.Player[Index].Vital[n] = Data.ReadInt16();
            Lists.Player[Index].Max_Vital[n] = Data.ReadInt16();
        }
        for (byte n = 0; n <= (byte)Game.Atributos.Amount - 1; n++) Lists.Player[Index].Atributo[n] = Data.ReadInt16();
        for (byte n = 0; n <= (byte)Game.Equipamentos.Amount - 1; n++) Lists.Player[Index].Equipamento[n] = Data.ReadInt16();
    }

    private static void Player_Posição(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Defini os Data do Player
        Lists.Player[Index].X = Data.ReadByte();
        Lists.Player[Index].Y = Data.ReadByte();
        Lists.Player[Index].Direction = (Game.Direções)Data.ReadByte();

        // Para a movimentação
        Lists.Player[Index].X2 = 0;
        Lists.Player[Index].Y2 = 0;
        Lists.Player[Index].Movimento = Game.Movimentos.Parado;
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

    private static void Player_Equipment(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte();

        // Define os Data
        for (byte i = 0; i <= (byte)Game.Equipamentos.Amount - 1; i++)  Lists.Player[Index].Equipamento[i] = Data.ReadInt16();
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
        Lists.Player[Index].Direction = (Game.Direções)Data.ReadByte();
        Lists.Player[Index].Movimento = (Game.Movimentos)Data.ReadByte();
        Lists.Player[Index].X2 = 0;
        Lists.Player[Index].Y2 = 0;

        // Posição exata do Player
        switch (Lists.Player[Index].Direction)
        {
            case Game.Direções.Acima: Lists.Player[Index].Y2 = Game.Grade; break;
            case Game.Direções.Abaixo: Lists.Player[Index].Y2 = Game.Grade * -1; break;
            case Game.Direções.Direita: Lists.Player[Index].X2 = Game.Grade * -1; break;
            case Game.Direções.Esquerda: Lists.Player[Index].X2 = Game.Grade; break;
        }
    }

    public static void Player_Direction(NetIncomingMessage Data)
    {
        // Define a Direction de determinado Player
        Lists.Player[Data.ReadByte()].Direction = (Game.Direções)Data.ReadByte();
    }

    public static void Player_Attack(NetIncomingMessage Data)
    {
        byte Index = Data.ReadByte(), Vítima = Data.ReadByte(), Vítima_Type = Data.ReadByte();

        // Inicia o ataque
        Lists.Player[Index].Atacando = true;
        Lists.Player[Index].Ataque_Tempo = Environment.TickCount;

        // Sofrendo dano
        if (Vítima > 0)
            if (Vítima_Type == (byte)Game.Alvo.Player)
                Lists.Player[Vítima].Sofrendo = Environment.TickCount;
            else if (Vítima_Type == (byte)Game.Alvo.NPC)
                Lists.Map.Temp_NPC[Vítima].Sofrendo = Environment.TickCount;
    }

    public static void Player_Experience(NetIncomingMessage Data)
    {
        // Define os Data
        Player.Eu.Experiência = Data.ReadInt16();
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
        byte Coluna = Game.Animação_Parada;
        int x, y;
        short x2 = Lists.Player[Index].X2, y2 = Lists.Player[Index].Y2;
        bool Sofrendo = false;
        short Texture = Player.Character_Texture(Index);

        // Previni sobrecargas
        if (Texture <= 0 || Texture > Tex_Character.GetUpperBound(0)) return;

        // Define a animação
        if (Lists.Player[Index].Atacando && Lists.Player[Index].Ataque_Tempo + Game.Ataque_Velocidade / 2 > Environment.TickCount)
            Coluna = Game.Animação_Ataque;
        else
        {
            if (x2 > 8 && x2 < Game.Grade) Coluna = Lists.Player[Index].Animação;
            if (x2 < -8 && x2 > Game.Grade * -1) Coluna = Lists.Player[Index].Animação;
            if (y2 > 8 && y2 < Game.Grade) Coluna = Lists.Player[Index].Animação;
            if (y2 < -8 && y2 > Game.Grade * -1) Coluna = Lists.Player[Index].Animação;
        }

        // Demonstra que o Character está sofrendo dano
        if (Lists.Player[Index].Sofrendo > 0) Sofrendo = true;

        // Desenha o Player
        x = Lists.Player[Index].X * Game.Grade + Lists.Player[Index].X2;
        y = Lists.Player[Index].Y * Game.Grade + Lists.Player[Index].Y2;
        Character(Texture, new Point(Game.ConverterX(x), Game.ConverterY(y)), Lists.Player[Index].Direction, Coluna, Sofrendo);
        Player_Name(Index, x, y);
        Player_Barras(Index, x, y);
    }

    public static void Player_Bars(byte Index, int x, int y)
    {
        Size Character_Tamanho = TTamanho(Tex_Character[Player.Character_Texture(Index)]);
        Point Posição = new Point(Game.ConverterX(x), Game.ConverterY(y) + Character_Tamanho.Height / Game.Animação_Amount + 4);
        int Largura_Completa = Character_Tamanho.Width / Game.Animação_Amount;
        short Contagem = Lists.Player[Index].Vital[(byte)Game.Vital.Vida];

        // Apenas se necessário
        if (Contagem <= 0 || Contagem >= Lists.Player[Index].Max_Vital[(byte)Game.Vital.Vida]) return;

        // Cálcula a largura da barra
        int Largura = (Contagem * Largura_Completa) / Lists.Player[Index].Max_Vital[(byte)Game.Vital.Vida];

        // Desenha as barras 
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 4, Largura_Completa, 4);
        Desenhar(Tex_Barras, Posição.X, Posição.Y, 0, 0, Largura, 4);
    }

    public static void Player_Name(byte Index, int x, int y)
    {
        Texture Texture = Tex_Character[Player.Character_Texture(Index)];
        int Name_Tamanho = Tools.MedirTexto_Largura(Lists.Player[Index].Name);

        // Posição do texto
        Point Posição = new Point();
        Posição.X = x + TTamanho(Texture).Width / Game.Animação_Amount / 2 - Name_Tamanho / 2;
        Posição.Y = y - TTamanho(Texture).Height / Game.Animação_Amount / 2;

        // Cor do texto
        SFML.Graphics.Color Cor;
        if (Index == Player.MyIndex)
            Cor = SFML.Graphics.Color.Yellow;
        else
            Cor = SFML.Graphics.Color.White;

        // Desenha o texto
        Desenhar(Lists.Player[Index].Name, Game.ConverterX(Posição.X), Game.ConverterY(Posição.Y), Cor);
    }
}