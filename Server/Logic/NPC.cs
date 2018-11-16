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

    public static short[] MáxVital(byte Índice, short Mapa)
    {
        return Listas.NPC[Listas.Mapa[Mapa].NPC[Índice].Índice].Vital;
    }

    public static short Renegeração(short Mapa_Num, byte Índice, byte Vital)
    {
        Listas.Estruturas.NPCs Dados = Listas.NPC[Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Índice];

        // Cálcula o máximo de vital que o NPC possui
        switch ((Game.Vitais)Vital)
        {
            case Game.Vitais.Vida: return (short)(Dados.Vital[Vital] * 0.05 + Dados.Atributo[(byte)Game.Atributos.Vitalidade] * 0.3);
            case Game.Vitais.Mana: return (short)(Dados.Vital[Vital] * 0.05 + Dados.Atributo[(byte)Game.Atributos.Inteligência] * 0.1);
        }

        return 0;
    }

    public static void Lógica(short Mapa_Num)
    {
        // Lógica dos NPCs
        for (byte i = 1; i <= Listas.Mapa[Mapa_Num].Temp_NPC.GetUpperBound(0); i++)
        {
            Listas.Estruturas.Mapa_NPCs Dados = Listas.Mapa[Mapa_Num].Temp_NPC[i];
            Listas.Estruturas.NPCs NPC_Dados = Listas.NPC[Listas.Mapa[Mapa_Num].NPC[i].Índice];

            //////////////////
            // Aparecimento //
            //////////////////
            if (Dados.Índice == 0)
            {
                if (Environment.TickCount > Dados.Aparecimento_Tempo + (NPC_Dados.Aparecimento * 1000)) Aparecer(i, Mapa_Num);
            }
            else
            {
                byte AlvoX = 0, AlvoY = 0;
                bool[] PodeMover = new bool[(byte)Game.Direções.Quantidade];
                short DistânciaX, DistânciaY;
                bool SeMoveu = false;
                bool Movimentar = false;

                /////////////////
                // Regeneração //
                /////////////////
                if (Environment.TickCount > Tie.Score_NPC_Reneration + 5000)
                    for (byte v = 0; v <= (byte)Game.Vitais.Quantidade - 1; v++)
                        if (Dados.Vital[v] < NPC_Dados.Vital[v])
                        {
                            // Renera os vitais
                            Listas.Mapa[Mapa_Num].Temp_NPC[i].Vital[v] += Renegeração(Mapa_Num, i, v);

                            // Impede que o valor passe do limite
                            if (Listas.Mapa[Mapa_Num].Temp_NPC[i].Vital[v] > NPC_Dados.Vital[v])
                                Listas.Mapa[Mapa_Num].Temp_NPC[i].Vital[v] = NPC_Dados.Vital[v];

                            // Envia os dados aos jogadores do mapa
                            Enviar.Mapa_NPC_Vitais(Mapa_Num, i);
                        }

                ///////////////
                // Movimento //
                ///////////////
                // Atacar ao ver
                if (Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice == 0 && NPC_Dados.Agressividade == (byte)Aggressiveness.AtacarAoVer)
                    for (byte p = 1; p <= Game.MaiorÍndice; p++)
                    {
                        // Verifica se o jogador está jogando e no mesmo mapa que o NPC
                        if (!Player.EstáJogando(p)) continue;
                        if (Player.Personagem(p).Mapa != Mapa_Num) continue;

                        // Distância entre o NPC e o jogador
                        DistânciaX = (short)(Dados.X - Player.Personagem(p).X);
                        DistânciaY = (short)(Dados.Y - Player.Personagem(p).Y);
                        if (DistânciaX < 0) DistânciaX *= -1;
                        if (DistânciaY < 0) DistânciaY *= -1;

                        // Se estiver no alcance, ir atrás do jogador
                        if (DistânciaX <= NPC_Dados.Visão && DistânciaY <= NPC_Dados.Visão)
                        {
                            Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Tipo = (byte)Game.Alvo.Jogador;
                            Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice = p;
                            Dados = Listas.Mapa[Mapa_Num].Temp_NPC[i];
                        }
                    }

                if (Dados.Alvo_Tipo == (byte)Game.Alvo.Jogador)
                {
                    // Posição do alvo
                    AlvoX = Player.Personagem(Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice).X;
                    AlvoY = Player.Personagem(Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice).Y;

                    // Verifica se o jogador ainda está disponível
                    if (!Player.EstáJogando(Dados.Alvo_Índice) || Player.Personagem(Dados.Alvo_Índice).Mapa != Mapa_Num)
                    {
                        Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Tipo = 0;
                        Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice = 0;
                        Dados = Listas.Mapa[Mapa_Num].Temp_NPC[i];
                    }
                }

                // Distância entre o NPC e o alvo
                DistânciaX = (short)(Dados.X - AlvoX);
                DistânciaY = (short)(Dados.Y - AlvoY);
                if (DistânciaX < 0) DistânciaX *= -1;
                if (DistânciaY < 0) DistânciaY *= -1;

                // Verifica se o alvo saiu do alcance
                if (DistânciaX > NPC_Dados.Visão || DistânciaY > NPC_Dados.Visão)
                {
                    Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Tipo = 0;
                    Listas.Mapa[Mapa_Num].Temp_NPC[i].Alvo_Índice = 0;
                    Dados = Listas.Mapa[Mapa_Num].Temp_NPC[i];
                }

                // Evita que ele se movimente sem sentido
                if (Dados.Alvo_Índice > 0)
                    Movimentar = true;
                else
                {
                    // Define o alvo até a zona do NPC
                    if (Listas.Mapa[Mapa_Num].NPC[i].Zona > 0)
                        if (Listas.Mapa[Mapa_Num].Azulejo[Dados.X, Dados.Y].Zona != Listas.Mapa[Mapa_Num].NPC[i].Zona)
                            for (byte x2 = 0; x2 <= Listas.Mapa[Mapa_Num].Largura; x2++)
                                for (byte y2 = 0; y2 <= Listas.Mapa[Mapa_Num].Altura; y2++)
                                    if (Listas.Mapa[Mapa_Num].Azulejo[x2, y2].Zona == Listas.Mapa[Mapa_Num].NPC[i].Zona)
                                        if (!Map.Azulejo_Bloqueado(Mapa_Num, x2, y2))
                                        {
                                            AlvoX = x2;
                                            AlvoY = y2;
                                            Movimentar = true;
                                            break;
                                        }
                }

                // Movimenta o NPC até mais perto do alvo
                if (Movimentar)
                {
                    // Verifica como pode se mover até o alvo
                    if (Dados.Y > AlvoY) PodeMover[(byte)Game.Direções.Acima] = true;
                    if (Dados.Y < AlvoY) PodeMover[(byte)Game.Direções.Abaixo] = true;
                    if (Dados.X > AlvoX) PodeMover[(byte)Game.Direções.Esquerda] = true;
                    if (Dados.X < AlvoX) PodeMover[(byte)Game.Direções.Direita] = true;

                    // Aleatoriza a forma que ele vai se movimentar até o alvo
                    if (Game.Aleatório.Next(0, 2) == 0)
                    {
                        for (byte d = 0; d <= (byte)Game.Direções.Quantidade - 1; d++)
                            if (!SeMoveu && PodeMover[d] && Mover(Mapa_Num, i, (Game.Direções)d))
                                SeMoveu = true;
                    }
                    else
                        for (short d = (byte)Game.Direções.Quantidade - 1; d >= 0; d--)
                            if (!SeMoveu && PodeMover[d] && Mover(Mapa_Num, i, (Game.Direções)d))
                                SeMoveu = true;
                }

                // Move-se aleatoriamente
                if (NPC_Dados.Agressividade == (byte)Aggressiveness.Passive || Dados.Alvo_Índice == 0)
                    if (Game.Aleatório.Next(0, 3) == 0 && !SeMoveu)
                        Mover(Mapa_Num, i, (Game.Direções)Game.Aleatório.Next(0, 4), 1, true);
            }

            ////////////
            // Ataque //
            ////////////
            short Próx_X = Dados.X, Próx_Y = Dados.Y;
            Map.PróximoAzulejo(Dados.Direção, ref Próx_X, ref Próx_Y);
            if (Dados.Alvo_Tipo == (byte)Game.Alvo.Jogador)
            {
                // Verifica se o jogador está na frente do NPC
                if (Map.HáJogador(Mapa_Num, Próx_X, Próx_Y) == Dados.Alvo_Índice) Atacar_Jogador(Mapa_Num, i, Dados.Alvo_Índice);
            }
        }
    }

    public static void Aparecer(byte Índice, short Mapa)
    {
        byte x, y;

        // Antes verifica se tem algum local de aparecimento específico
        if (Listas.Mapa[Mapa].NPC[Índice].Aparecer)
        {
            Aparecer(Índice, Mapa, Listas.Mapa[Mapa].NPC[Índice].X, Listas.Mapa[Mapa].NPC[Índice].Y);
            return;
        }

        // Faz com que ele apareça em um local aleatório
        for (byte i = 0; i <= 50; i++) // tenta 50 vezes com que ele apareça em um local aleatório
        {
            x = (byte)Game.Aleatório.Next(0, Listas.Mapa[Mapa].Largura);
            y = (byte)Game.Aleatório.Next(0, Listas.Mapa[Mapa].Altura);

            // Verifica se está dentro da zona
            if (Listas.Mapa[Mapa].NPC[Índice].Zona > 0)
                if (Listas.Mapa[Mapa].Azulejo[x, y].Zona != Listas.Mapa[Mapa].NPC[Índice].Zona)
                    continue;

            // Define os dados
            if (!global::Map.Azulejo_Bloqueado(Mapa, x, y))
            {
                Aparecer(Índice, Mapa, x, y);
                return;
            }
        }

        // Em último caso, tentar no primeiro lugar possível
        for (byte x2 = 0; x2 <= Listas.Mapa[Mapa].Largura; x2++)
            for (byte y2 = 0; y2 <= Listas.Mapa[Mapa].Altura; y2++)
                if (!global::Map.Azulejo_Bloqueado(Mapa, x2, y2))
                {
                    // Verifica se está dentro da zona
                    if (Listas.Mapa[Mapa].NPC[Índice].Zona > 0)
                        if (Listas.Mapa[Mapa].Azulejo[x2, y2].Zona != Listas.Mapa[Mapa].NPC[Índice].Zona)
                            continue;

                    // Define os dados
                    Aparecer(Índice, Mapa, x2, y2);
                    return;
                }
    }

    public static void Aparecer(byte Índice, short Mapa, byte x, byte y, Game.Direções Direção = 0)
    {
        Listas.Estruturas.NPCs Dados = Listas.NPC[Listas.Mapa[Mapa].NPC[Índice].Índice];
        short[] s = Dados.Vital;

        // Define os dados
        Listas.Mapa[Mapa].Temp_NPC[Índice].Índice = Listas.Mapa[Mapa].NPC[Índice].Índice;
        Listas.Mapa[Mapa].Temp_NPC[Índice].X = x;
        Listas.Mapa[Mapa].Temp_NPC[Índice].Y = y;
        Listas.Mapa[Mapa].Temp_NPC[Índice].Direção = Direção;
        Listas.Mapa[Mapa].Temp_NPC[Índice].Vital = new short[(byte)Game.Vitais.Quantidade];
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++) Listas.Mapa[Mapa].Temp_NPC[Índice].Vital[i] = Dados.Vital[i];

        // Envia os dados aos jogadores
        if (Rede.Dispositivo != null) Enviar.Mapa_NPC(Mapa, Índice);
    }

    public static bool Mover(short Mapa_Num, byte Índice, Game.Direções Direção, byte Movimento = 1, bool ContarZona = false)
    {
        Listas.Estruturas.Mapa_NPCs Dados = Listas.Mapa[Mapa_Num].Temp_NPC[Índice];
        byte x = Dados.X, y = Dados.Y;
        short Próximo_X = x, Próximo_Y = y;

        // Define a direção do NPC
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Direção = Direção;
        Enviar.Mapa_NPC_Direção(Mapa_Num, Índice);

        // Próximo azulejo
        Map.PróximoAzulejo(Direção, ref Próximo_X, ref Próximo_Y);

        // Próximo azulejo bloqueado ou fora do limite
        if (Map.ForaDoLimite(Mapa_Num, Próximo_X, Próximo_Y)) return false;
        if (Map.Azulejo_Bloqueado(Mapa_Num, x, y, Direção)) return false;

        // Verifica se está dentro da zona
        if (ContarZona)
            if (Listas.Mapa[Mapa_Num].Azulejo[Próximo_X, Próximo_Y].Zona != Listas.Mapa[Mapa_Num].NPC[Índice].Zona)
                return false;

        // Movimenta o NPC
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].X = (byte)Próximo_X;
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Y = (byte)Próximo_Y;
        Enviar.Mapa_NPC_Movimento(Mapa_Num, Índice, Movimento);
        return true;
    }

    public static void Atacar_Jogador(short Mapa_Num, byte Índice, byte Vítima)
    {
        Listas.Estruturas.Mapa_NPCs Dados = Listas.Mapa[Mapa_Num].Temp_NPC[Índice];
        short x = Dados.X, y = Dados.Y;

        // Define o azujelo a frente do NPC
        Map.PróximoAzulejo(Dados.Direção, ref x, ref y);

        // Verifica se a vítima pode ser atacada
        if (Dados.Índice == 0) return;
        if (Environment.TickCount < Dados.Ataque_Tempo + 750) return;
        if (!Player.EstáJogando(Vítima)) return;
        if (Listas.TempJogador[Vítima].ObtendoMapa) return;
        if (Mapa_Num != Player.Personagem(Vítima).Mapa) return;
        if (Player.Personagem(Vítima).X != x || Player.Personagem(Vítima).Y != y) return;
        if (Map.Azulejo_Bloqueado(Mapa_Num, Dados.X, Dados.Y, Dados.Direção, false)) return;

        // Tempo de ataque 
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Ataque_Tempo = Environment.TickCount;

        // Demonstra o ataque aos outros jogadores
        Enviar.Mapa_NPC_Atacar(Mapa_Num, Índice, Vítima, (byte)Game.Alvo.Jogador);

        // Cálculo de dano
        short Dano = (short)(Listas.NPC[Dados.Índice].Atributo[(byte)Game.Atributos.Força] - Player.Personagem(Vítima).Jogador_Defesa);

        // Dano não fatal
        if (Dano < Player.Personagem(Vítima).Vital[(byte)Game.Vitais.Vida])
        {
            Player.Personagem(Vítima).Vital[(byte)Game.Vitais.Vida] -= Dano;
            Enviar.Jogador_Vitais(Vítima);
        }
        // FATALITY
        else
        {
            // Reseta o alvo do NPC
            Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Alvo_Tipo = 0;
            Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Alvo_Índice = 0;

            // Mata o jogador
            Player.Morreu(Vítima);
        }
    }

    public static void Morreu(short Mapa_Num, byte Índice)
    {
        Listas.Estruturas.NPCs NPC = Listas.NPC[Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Índice];

        // Solta os itens
        for (byte i = 0; i <= Game.Max_NPC_Queda - 1; i++)
            if (NPC.Queda[i].Item_Num > 0)
                if (Game.Aleatório.Next(NPC.Queda[i].Chance, 101) == 100)
                {
                    // Dados do item
                    Listas.Estruturas.Mapa_Itens Item = new Listas.Estruturas.Mapa_Itens();
                    Item.Índice = NPC.Queda[i].Item_Num;
                    Item.Quantidade = NPC.Queda[i].Quantidade;
                    Item.X = Listas.Mapa[Mapa_Num].Temp_NPC[Índice].X;
                    Item.Y = Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Y;

                    // Solta
                    Listas.Mapa[Mapa_Num].Temp_Item.Add(Item);
                }

        // Envia os dados dos itens no chão para o mapa
        Enviar.Mapa_Itens(Mapa_Num);

        // Reseta os dados do NPC 
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Vital[(byte)Game.Vitais.Vida] = 0;
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Aparecimento_Tempo = Environment.TickCount;
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Índice = 0;
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Alvo_Tipo = 0;
        Listas.Mapa[Mapa_Num].Temp_NPC[Índice].Alvo_Índice = 0;
        Enviar.Mapa_NPC_Morreu(Mapa_Num, Índice);
    }
}

partial class Ler
{
    public static void NPCs()
    {
        Listas.NPC = new Listas.Estruturas.NPCs[Listas.Servidor_Dados.Num_NPCs + 1];

        // Lê os dados
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)
            NPC(i);
    }

    public static void NPC(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.NPCs.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Redimensiona os valores necessários 
        Listas.NPC[Índice].Vital = new short[(byte)Game.Vitais.Quantidade];
        Listas.NPC[Índice].Atributo = new short[(byte)Game.Atributos.Quantidade];
        Listas.NPC[Índice].Queda = new Listas.Estruturas.NPC_Queda[Game.Max_NPC_Queda];

        // Lê os dados
        Listas.NPC[Índice].Nome = Binário.ReadString();
        Listas.NPC[Índice].Textura = Binário.ReadInt16();
        Listas.NPC[Índice].Agressividade = Binário.ReadByte();
        Listas.NPC[Índice].Aparecimento = Binário.ReadByte();
        Listas.NPC[Índice].Visão = Binário.ReadByte();
        Listas.NPC[Índice].Experiência = Binário.ReadByte();
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++) Listas.NPC[Índice].Vital[i] = Binário.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Atributos.Quantidade - 1; i++) Listas.NPC[Índice].Atributo[i] = Binário.ReadInt16();
        for (byte i = 0; i <= Game.Max_NPC_Queda - 1; i++)
        {
            Listas.NPC[Índice].Queda[i].Item_Num = Binário.ReadInt16();
            Listas.NPC[Índice].Queda[i].Quantidade = Binário.ReadInt16();
            Listas.NPC[Índice].Queda[i].Chance = Binário.ReadByte();
        }

        // Fecha o sistema
        Binário.Dispose();
    }
}

partial class Enviar
{
    public static void NPCs(byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.NPCs);
        Dados.Write((byte)Listas.NPC.GetUpperBound(0));
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)
        {
            // Geral
            Dados.Write(Listas.NPC[i].Nome);
            Dados.Write(Listas.NPC[i].Textura);
            Dados.Write(Listas.NPC[i].Agressividade);
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Dados.Write(Listas.NPC[i].Vital[n]);
        }
        Para(Índice, Dados);
    }

    public static void Mapa_NPCs(byte Índice, short Mapa)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPCs);
        Dados.Write((short)Listas.Mapa[Mapa].Temp_NPC.GetUpperBound(0));
        for (byte i = 1; i <= Listas.Mapa[Mapa].Temp_NPC.GetUpperBound(0); i++)
        {
            Dados.Write(Listas.Mapa[Mapa].Temp_NPC[i].Índice);
            Dados.Write(Listas.Mapa[Mapa].Temp_NPC[i].X);
            Dados.Write(Listas.Mapa[Mapa].Temp_NPC[i].Y);
            Dados.Write((byte)Listas.Mapa[Mapa].Temp_NPC[i].Direção);
            for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Dados.Write(Listas.Mapa[Mapa].Temp_NPC[i].Vital[n]);
        }
        Para(Índice, Dados);
    }

    public static void Mapa_NPC(short Mapa, byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC);
        Dados.Write(Índice);
        Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].Índice);
        Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].X);
        Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].Y);
        Dados.Write((byte)Listas.Mapa[Mapa].Temp_NPC[Índice].Direção);
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].Vital[n]);
        ParaMapa(Mapa, Dados);
    }

    public static void Mapa_NPC_Movimento(short Mapa, byte Índice, byte Movimento)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC_Movimento);
        Dados.Write(Índice);
        Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].X);
        Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].Y);
        Dados.Write((byte)Listas.Mapa[Mapa].Temp_NPC[Índice].Direção);
        Dados.Write(Movimento);
        ParaMapa(Mapa, Dados);
    }

    public static void Mapa_NPC_Direção(short Mapa, byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC_Direção);
        Dados.Write(Índice);
        Dados.Write((byte)Listas.Mapa[Mapa].Temp_NPC[Índice].Direção);
        ParaMapa(Mapa, Dados);
    }

    public static void Mapa_NPC_Vitais(short Mapa, byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC_Vitais);
        Dados.Write(Índice);
        for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Dados.Write(Listas.Mapa[Mapa].Temp_NPC[Índice].Vital[n]);
        ParaMapa(Mapa, Dados);
    }

    public static void Mapa_NPC_Atacar(short Mapa, byte Índice, byte Vítima, byte Vítima_Tipo)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC_Atacar);
        Dados.Write(Índice);
        Dados.Write(Vítima);
        Dados.Write(Vítima_Tipo);
        ParaMapa(Mapa, Dados);
    }

    public static void Mapa_NPC_Morreu(short Mapa, byte Índice)
    {
        NetOutgoingMessage Dados = Rede.Dispositivo.CreateMessage();

        // Envia os dados
        Dados.Write((byte)Pacotes.Mapa_NPC_Morreu);
        Dados.Write(Índice);
        ParaMapa(Mapa, Dados);
    }
}