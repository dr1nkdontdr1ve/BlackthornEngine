using System.IO;

class Read
{
    public static void Data()
    {
        // Carrega todos os Data
        Client_Data();
        Buttons_Data();
        Digitalizadores_Data();
        Panels_Data();
        Marcadores_Data();
        Options();
    }

    public static void Options()
    {
        // Cria o arquivo se ele não existir
        if (!Diretórios.Opções.Exists)
            Clean.Opções();
        else
        {
            // Cria um arquivo temporário
            BinaryReader Arquivo = new BinaryReader(File.OpenRead(Diretórios.Opções.FullName));

            // Carrega os Data
            Lists.Opções.Game_Name = Arquivo.ReadString();
            Lists.Opções.SalvarUsuário = Arquivo.ReadBoolean();
            Lists.Opções.Sons = Arquivo.ReadBoolean();
            Lists.Opções.Músicas = Arquivo.ReadBoolean();
            Lists.Opções.Usuário = Arquivo.ReadString();

            // Descarrega o arquivo
            Arquivo.Dispose();
        }

        // Adiciona os Data ao cache
        Markers.Encontrar("Sons").State = Lists.Opções.Sons;
        Markers.Encontrar("Músicas").State = Lists.Opções.Músicas;
        Markers.Encontrar("SalvarUsuário").State = Lists.Opções.SalvarUsuário;
        if (Lists.Opções.SalvarUsuário) Scanners.Encontrar("Conectar_Usuário").Texto = Lists.Opções.Usuário;
    }

    public static void Client_Data()
    {
        // Cria um sistema Binary para a manipulação dos Data
        BinaryReader Binary = new BinaryReader(Diretórios.Client_Data.OpenRead());

        // Lê os Data
        Lists.Client_Data.Num_Botões = Binary.ReadByte();
        Lists.Client_Data.Num_Digitalizadores = Binary.ReadByte();
        Lists.Client_Data.Num_Paineis = Binary.ReadByte();
        Lists.Client_Data.Num_Marcadores = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Buttons_Data()
    {
        Buttons.List = new Buttons.Structure[Lists.Client_Data.Num_Botões + 1];

        // Lê os Data
        for (byte i = 1; i <= Buttons.List.GetUpperBound(0); i++)
            Botão_Data(i);
    }

    public static void Button_Data(byte Index)
    {
        // Limpa os valores
        Clean.Button(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Diretórios.Botões_Data.FullName + Index + Diretórios.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Buttons.List[Index].Geral.Name = Binary.ReadString();
        Buttons.List[Index].Geral.Posição.X = Binary.ReadInt32();
        Buttons.List[Index].Geral.Posição.Y = Binary.ReadInt32();
        Buttons.List[Index].Geral.Visível = Binary.ReadBoolean();
        Buttons.List[Index].Texture = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Digitalizadores_Data()
    {
        Scanners.List = new Scanners.Structure[Lists.Cliente_Data.Num_Digitalizadores + 1];

        // Lê os Data
        for (byte i = 1; i <= Scanners.List.GetUpperBound(0); i++)
            Digitalizador_Data(i);
    }

    public static void Digitalizador_Data(byte Index)
    {
        // Limpa os valores
        Clean.Digitalizador(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Diretórios.Digitalizadores_Data.FullName + Index + Diretórios.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Scanners.List[Index].General.Name = Binary.ReadString();
        Scanners.List[Index].General.Posição.X = Binary.ReadInt32();
        Scanners.List[Index].General.Posição.Y = Binary.ReadInt32();
        Scanners.List[Index].General.Visível = Binary.ReadBoolean();
        Scanners.List[Index].Max_Carácteres = Binary.ReadInt16();
        Scanners.List[Index].Largura = Binary.ReadInt16();
        Scanners.List[Index].Senha = Binary.ReadBoolean();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Panels_Data()
    {
        Panels.List = new Panels.Structure[Lists.Cliente_Data.Num_Paineis + 1];

        // Lê os Data
        for (byte i = 1; i <= Panels.List.GetUpperBound(0); i++)
            Painel_Data(i);
    }

    public static void Panel_Data(byte Index)
    {
        // Limpa os valores
        Clean.Painel(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Diretórios.Paineis_Data.FullName + Index + Diretórios.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Carrega os Data
        Panels.List[Index].General.Name = Binary.ReadString();
        Panels.List[Index].General.Posição.X = Binary.ReadInt32();
        Panels.List[Index].General.Posição.Y = Binary.ReadInt32();
        Panels.List[Index].General.Visível = Binary.ReadBoolean();
        Panels.List[Index].Texture = Binary.ReadByte();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Marcadores_Data()
    {
        Markers.List = new Markers.Structure[Lists.Cliente_Data.Num_Marcadores + 1];

        // Lê os Data
        for (byte i = 1; i <= Markers.List.GetUpperBound(0); i++)
            Marcador_Data(i);
    }

    public static void Marcador_Data(byte Index)
    {
        // Limpa os valores
        Clean.Marcador(Index);

        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Diretórios.Marcadores_Data.FullName + Index + Diretórios.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Carrega os Data
        Markers.List[Index].General.Name = Binary.ReadString();
        Markers.List[Index].General.Posição.X = Binary.ReadInt32();
        Markers.List[Index].General.Posição.Y = Binary.ReadInt32();
        Markers.List[Index].General.Visível = Binary.ReadBoolean();
        Markers.List[Index].Text = Binary.ReadString();
        Markers.List[Index].State = Binary.ReadBoolean();

        // Fecha o sistema
        Binary.Dispose();
    }

    public static void Map(int Index)
    {
        // Cria um sistema Binary para a manipulação dos Data
        FileInfo Arquivo = new FileInfo(Diretórios.Maps_Data.FullName + Index + Diretórios.Format);
        BinaryReader Binary = new BinaryReader(Arquivo.OpenRead());

        // Lê os Data
        Lists.Map.Revisão = Binary.ReadInt16();
        Lists.Map.Name = Binary.ReadString();
        Lists.Map.Width = Binary.ReadByte();
        Lists.Map.Height = Binary.ReadByte();
        Lists.Map.Moral = Binary.ReadByte();
        Lists.Map.Panorama = Binary.ReadByte();
        Lists.Map.Música = Binary.ReadByte();
        Lists.Map.Coloração = Binary.ReadInt32();
        Lists.Map.Clima.Type = Binary.ReadByte();
        Lists.Map.Clima.Intensidade = Binary.ReadByte();
        Lists.Map.Fumaça.Texture = Binary.ReadByte();
        Lists.Map.Fumaça.VelocidadeX = Binary.ReadSByte();
        Lists.Map.Fumaça.VelocidadeY = Binary.ReadSByte();
        Lists.Map.Fumaça.Transparência = Binary.ReadByte();

        // Redimensiona as ligações
        Lists.Map.Ligação = new short[(byte)Game.Direções.Amount];
        for (short i = 0; i <= (short)Game.Direções.Amount - 1; i++)
            Lists.Map.Ligação[i] = Binary.ReadInt16();

        // Redimensiona os azulejos 
        Lists.Map.Tile = new Lists.Structures.Tile[Lists.Map.Width + 1, Lists.Map.Height + 1];

        // Lê os Data
        byte NumCamadas = Binary.ReadByte();
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
            {
                // Redimensiona os Data dos azulejos
                Lists.Map.Azulejo[x, y].Data = new Lists.Structures.Azulejo_Data[(byte)global::Map.Camadas.Amount, NumCamadas + 1];

                for (byte c = 0; c <= (byte)global::Map.Camadas.Amount - 1; c++)
                    for (byte q = 0; q <= NumCamadas; q++)
                    {
                        Lists.Map.Azulejo[x, y].Data[c, q].x = Binary.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].y = Binary.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].Azulejo = Binary.ReadByte();
                        Lists.Map.Azulejo[x, y].Data[c, q].Automático = Binary.ReadBoolean();
                        Lists.Map.Azulejo[x, y].Data[c, q].Mini = new System.Drawing.Point[4];
                    }
            }

        // Data específicos dos azulejos
        for (byte x = 0; x <= Lists.Map.Largura; x++)
            for (byte y = 0; y <= Lists.Map.Altura; y++)
            {
                Lists.Map.Azulejo[x, y].Atributo = Binary.ReadByte();
                Lists.Map.Azulejo[x, y].Bloqueio = new bool[(byte)Game.Direções.Amount];
                for (byte i = 0; i <= (byte)Game.Direções.Amount - 1; i++)
                    Lists.Map.Azulejo[x, y].Bloqueio[i] = Binary.ReadBoolean();
            }

        // Luzes
        Lists.Map.Luz = new Lists.Structures.Luz[Binary.ReadInt32() + 1];
        if (Lists.Map.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Lists.Map.Luz.GetUpperBound(0); i++)
            {
                Lists.Map.Luz[i].X = Binary.ReadByte();
                Lists.Map.Luz[i].Y = Binary.ReadByte();
                Lists.Map.Luz[i].Largura = Binary.ReadByte();
                Lists.Map.Luz[i].Altura = Binary.ReadByte();
            }

        // NPCs
        Lists.Map.NPC = new short[Binary.ReadInt32() + 1];
        Lists.Map.Temp_NPC = new Lists.Structures.Map_NPCs  [Lists.Map.NPC.GetUpperBound(0) + 1];
        if (Lists.Map.NPC.GetUpperBound(0) > 0)
            for (byte i =1; i <= Lists.Map.NPC.GetUpperBound(0); i++)
            {
                Lists.Map.NPC[i] = Binary.ReadInt16();
                Lists.Map.Temp_NPC[i].Index = Lists.Map.NPC[i];
            }

        // Fecha o sistema
        Binary.Dispose();

        // Redimensiona as partículas do clima
        global::Map.Clima_Ajustar();
        global::Map.AutoCriação.Atualizar();
    }
}