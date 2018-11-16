using System.IO;

class Ler
{
    public static void Dados()
    {
        // Carrega todos os dados
        Cliente_Dados();
        Botões_Dados();
        Digitalizadores_Dados();
        Paineis_Dados();
        Marcadores_Dados();
        Opções();
    }

    public static void Opções()
    {
        // Cria o arquivo se ele não existir
        if (!Diretórios.Opções.Exists)
            Limpar.Opções();
        else
        {
            // Cria um arquivo temporário
            BinaryReader Arquivo = new BinaryReader(File.OpenRead(Diretórios.Opções.FullName));

            // Carrega os dados
            Listas.Opções.Jogo_Nome = Arquivo.ReadString();
            Listas.Opções.SalvarUsuário = Arquivo.ReadBoolean();
            Listas.Opções.Sons = Arquivo.ReadBoolean();
            Listas.Opções.Músicas = Arquivo.ReadBoolean();
            Listas.Opções.Usuário = Arquivo.ReadString();

            // Descarrega o arquivo
            Arquivo.Dispose();
        }

        // Adiciona os dados ao cache
        Marcadores.Encontrar("Sons").Estado = Listas.Opções.Sons;
        Marcadores.Encontrar("Músicas").Estado = Listas.Opções.Músicas;
        Marcadores.Encontrar("SalvarUsuário").Estado = Listas.Opções.SalvarUsuário;
        if (Listas.Opções.SalvarUsuário) Digitalizadores.Encontrar("Conectar_Usuário").Texto = Listas.Opções.Usuário;
    }

    public static void Cliente_Dados()
    {
        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Diretórios.Cliente_Dados.OpenRead());

        // Lê os dados
        Listas.Cliente_Dados.Num_Botões = Binário.ReadByte();
        Listas.Cliente_Dados.Num_Digitalizadores = Binário.ReadByte();
        Listas.Cliente_Dados.Num_Paineis = Binário.ReadByte();
        Listas.Cliente_Dados.Num_Marcadores = Binário.ReadByte();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Botões_Dados()
    {
        Botões.Lista = new Botões.Estrutura[Listas.Cliente_Dados.Num_Botões + 1];

        // Lê os dados
        for (byte i = 1; i <= Botões.Lista.GetUpperBound(0); i++)
            Botão_Dados(i);
    }

    public static void Botão_Dados(byte Índice)
    {
        // Limpa os valores
        Limpar.Botão(Índice);

        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Botões_Dados.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Botões.Lista[Índice].Geral.Nome = Binário.ReadString();
        Botões.Lista[Índice].Geral.Posição.X = Binário.ReadInt32();
        Botões.Lista[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Botões.Lista[Índice].Geral.Visível = Binário.ReadBoolean();
        Botões.Lista[Índice].Textura = Binário.ReadByte();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Digitalizadores_Dados()
    {
        Digitalizadores.Lista = new Digitalizadores.Estrutura[Listas.Cliente_Dados.Num_Digitalizadores + 1];

        // Lê os dados
        for (byte i = 1; i <= Digitalizadores.Lista.GetUpperBound(0); i++)
            Digitalizador_Dados(i);
    }

    public static void Digitalizador_Dados(byte Índice)
    {
        // Limpa os valores
        Limpar.Digitalizador(Índice);

        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Digitalizadores_Dados.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Digitalizadores.Lista[Índice].Geral.Nome = Binário.ReadString();
        Digitalizadores.Lista[Índice].Geral.Posição.X = Binário.ReadInt32();
        Digitalizadores.Lista[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Digitalizadores.Lista[Índice].Geral.Visível = Binário.ReadBoolean();
        Digitalizadores.Lista[Índice].Máx_Carácteres = Binário.ReadInt16();
        Digitalizadores.Lista[Índice].Largura = Binário.ReadInt16();
        Digitalizadores.Lista[Índice].Senha = Binário.ReadBoolean();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Paineis_Dados()
    {
        Paineis.Lista = new Paineis.Estrutura[Listas.Cliente_Dados.Num_Paineis + 1];

        // Lê os dados
        for (byte i = 1; i <= Paineis.Lista.GetUpperBound(0); i++)
            Painel_Dados(i);
    }

    public static void Painel_Dados(byte Índice)
    {
        // Limpa os valores
        Limpar.Painel(Índice);

        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Paineis_Dados.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Carrega os dados
        Paineis.Lista[Índice].Geral.Nome = Binário.ReadString();
        Paineis.Lista[Índice].Geral.Posição.X = Binário.ReadInt32();
        Paineis.Lista[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Paineis.Lista[Índice].Geral.Visível = Binário.ReadBoolean();
        Paineis.Lista[Índice].Textura = Binário.ReadByte();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Marcadores_Dados()
    {
        Marcadores.Lista = new Marcadores.Estrutura[Listas.Cliente_Dados.Num_Marcadores + 1];

        // Lê os dados
        for (byte i = 1; i <= Marcadores.Lista.GetUpperBound(0); i++)
            Marcador_Dados(i);
    }

    public static void Marcador_Dados(byte Índice)
    {
        // Limpa os valores
        Limpar.Marcador(Índice);

        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Marcadores_Dados.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Carrega os dados
        Marcadores.Lista[Índice].Geral.Nome = Binário.ReadString();
        Marcadores.Lista[Índice].Geral.Posição.X = Binário.ReadInt32();
        Marcadores.Lista[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Marcadores.Lista[Índice].Geral.Visível = Binário.ReadBoolean();
        Marcadores.Lista[Índice].Texto = Binário.ReadString();
        Marcadores.Lista[Índice].Estado = Binário.ReadBoolean();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Mapa(int Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas_Dados.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Mapa.Revisão = Binário.ReadInt16();
        Listas.Mapa.Nome = Binário.ReadString();
        Listas.Mapa.Largura = Binário.ReadByte();
        Listas.Mapa.Altura = Binário.ReadByte();
        Listas.Mapa.Moral = Binário.ReadByte();
        Listas.Mapa.Panorama = Binário.ReadByte();
        Listas.Mapa.Música = Binário.ReadByte();
        Listas.Mapa.Coloração = Binário.ReadInt32();
        Listas.Mapa.Clima.Tipo = Binário.ReadByte();
        Listas.Mapa.Clima.Intensidade = Binário.ReadByte();
        Listas.Mapa.Fumaça.Textura = Binário.ReadByte();
        Listas.Mapa.Fumaça.VelocidadeX = Binário.ReadSByte();
        Listas.Mapa.Fumaça.VelocidadeY = Binário.ReadSByte();
        Listas.Mapa.Fumaça.Transparência = Binário.ReadByte();

        // Redimensiona as ligações
        Listas.Mapa.Ligação = new short[(byte)Jogo.Direções.Quantidade];
        for (short i = 0; i <= (short)Jogo.Direções.Quantidade - 1; i++)
            Listas.Mapa.Ligação[i] = Binário.ReadInt16();

        // Redimensiona os azulejos 
        Listas.Mapa.Azulejo = new Listas.Estruturas.Azulejo[Listas.Mapa.Largura + 1, Listas.Mapa.Altura + 1];

        // Lê os dados
        byte NumCamadas = Binário.ReadByte();
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
            {
                // Redimensiona os dados dos azulejos
                Listas.Mapa.Azulejo[x, y].Dados = new Listas.Estruturas.Azulejo_Dados[(byte)global::Mapa.Camadas.Quantidade, NumCamadas + 1];

                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= NumCamadas; q++)
                    {
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].x = Binário.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].y = Binário.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Azulejo = Binário.ReadByte();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Automático = Binário.ReadBoolean();
                        Listas.Mapa.Azulejo[x, y].Dados[c, q].Mini = new System.Drawing.Point[4];
                    }
            }

        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
            {
                Listas.Mapa.Azulejo[x, y].Atributo = Binário.ReadByte();
                Listas.Mapa.Azulejo[x, y].Bloqueio = new bool[(byte)Jogo.Direções.Quantidade];
                for (byte i = 0; i <= (byte)Jogo.Direções.Quantidade - 1; i++)
                    Listas.Mapa.Azulejo[x, y].Bloqueio[i] = Binário.ReadBoolean();
            }

        // Luzes
        Listas.Mapa.Luz = new Listas.Estruturas.Luz[Binário.ReadInt32() + 1];
        if (Listas.Mapa.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Listas.Mapa.Luz.GetUpperBound(0); i++)
            {
                Listas.Mapa.Luz[i].X = Binário.ReadByte();
                Listas.Mapa.Luz[i].Y = Binário.ReadByte();
                Listas.Mapa.Luz[i].Largura = Binário.ReadByte();
                Listas.Mapa.Luz[i].Altura = Binário.ReadByte();
            }

        // NPCs
        Listas.Mapa.NPC = new short[Binário.ReadInt32() + 1];
        Listas.Mapa.Temp_NPC = new Listas.Estruturas.Mapa_NPCs  [Listas.Mapa.NPC.GetUpperBound(0) + 1];
        if (Listas.Mapa.NPC.GetUpperBound(0) > 0)
            for (byte i =1; i <= Listas.Mapa.NPC.GetUpperBound(0); i++)
            {
                Listas.Mapa.NPC[i] = Binário.ReadInt16();
                Listas.Mapa.Temp_NPC[i].Índice = Listas.Mapa.NPC[i];
            }

        // Fecha o sistema
        Binário.Dispose();

        // Redimensiona as partículas do clima
        global::Mapa.Clima_Ajustar();
        global::Mapa.AutoCriação.Atualizar();
    }
}