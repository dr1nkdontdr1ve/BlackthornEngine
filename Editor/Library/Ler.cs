using System.IO;
using System.Drawing;

class Ler
{
    public static void Opções()
    {
        // Se o arquivo não existir, não é necessário carregá-lo
        if (!Diretórios.Opções.Exists)
            Limpar.Opções();
        else
        {
            // Cria um sistema binário para a manipulação dos dados
            BinaryReader Binário = new BinaryReader(Diretórios.Opções.OpenRead());

            // Lê os dados
            Listas.Opções.Diretório_Cliente = Binário.ReadString();
            Listas.Opções.Diretório_Servidor = Binário.ReadString();
            Listas.Opções.Pre_Mapa_Grades = Binário.ReadBoolean();
            Listas.Opções.Pre_Mapa_Visualização = Binário.ReadBoolean();
            Listas.Opções.Pre_Mapa_Áudio = Binário.ReadBoolean();

            // Fecha o sistema
            Binário.Dispose();
        }

        // Define e cria os diretórios
        Diretórios.Definir_Cliente();
        Diretórios.Definir_Servidor();

        // Atualiza os valores
        Editor_Mapas.Objetos.butGrades.Checked = Listas.Opções.Pre_Mapa_Grades;
        Editor_Mapas.Objetos.butÁudio.Checked = Listas.Opções.Pre_Mapa_Áudio;

        if (!Listas.Opções.Pre_Mapa_Visualização)
        {
            Editor_Mapas.Objetos.butVisualização.Checked = false;
            Editor_Mapas.Objetos.butEdição.Checked = true;
        }
    }

    public static void Cliente_Dados()
    {
        // Limpa os dados
        Limpar.Cliente_Dados();

        // Se o arquivo não existir, não é necessário carregá-lo
        if (!Diretórios.Cliente_Dados.Exists)
        {
            Escrever.Cliente_Dados();
            return;
        }

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

    public static void Servidor_Dados()
    {
        // Limpa os dados
        Limpar.Servidor_Dados();

        // Se o arquivo não existir, não é necessário carregá-lo
        if (!Diretórios.Servidor_Dados.Exists)
        {
            Escrever.Servidor_Dados();
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Diretórios.Servidor_Dados.OpenRead());

        // Lê os dados
        Listas.Servidor_Dados.Jogo_Nome = Binário.ReadString();
        Listas.Servidor_Dados.Mensagem = Binário.ReadString();
        Listas.Servidor_Dados.Porta = Binário.ReadInt16();
        Listas.Servidor_Dados.Máx_Jogadores = Binário.ReadByte();
        Listas.Servidor_Dados.Máx_Personagens = Binário.ReadByte();
        Listas.Servidor_Dados.Num_Classes = Binário.ReadByte();
        Listas.Servidor_Dados.Num_Azulejos = Binário.ReadByte();
        Listas.Servidor_Dados.Num_Mapas = Binário.ReadInt16();
        Listas.Servidor_Dados.Num_NPCs = Binário.ReadInt16();
        Listas.Servidor_Dados.Num_Itens = Binário.ReadInt16();

        // Fecha o sistema
        Binário.Dispose();
    }

    #region Servidor
    public static void Classes()
    {
        Listas.Classe = new Listas.Estruturas.Classes[Listas.Servidor_Dados.Num_Classes + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Classe.GetUpperBound(0); i++)
        {
            Limpar.Classe(i);
            Classe(i);
        }
    }

    public static void Classe(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Classes_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Classe(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Classe[Índice].Nome = Binário.ReadString();
        Listas.Classe[Índice].Textura_Masculina = Binário.ReadInt16();
        Listas.Classe[Índice].Textura_Feminina = Binário.ReadInt16();
        Listas.Classe[Índice].Aparecer_Mapa = Binário.ReadInt16();
        Listas.Classe[Índice].Aparecer_Direção = Binário.ReadByte();
        Listas.Classe[Índice].Aparecer_X = Binário.ReadByte();
        Listas.Classe[Índice].Aparecer_Y = Binário.ReadByte();

        // Vitais
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++)
            Listas.Classe[Índice].Vital[i] = Binário.ReadInt16();

        // Atributos
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++)
            Listas.Classe[Índice].Atributo[i] = Binário.ReadInt16();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Azulejos()
    {
        Listas.Servidor_Dados.Num_Azulejos = (byte)Gráficos.Tex_Azulejo.GetUpperBound(0);
        Listas.Azulejo = new Listas.Estruturas.Azulejos_Azulejo[Listas.Servidor_Dados.Num_Azulejos + 1];

        // Salva a quantidade dos azulejos
        Escrever.Servidor_Dados();

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Azulejo.GetUpperBound(0); i++)
        {
            Limpar.Azulejo(i);
            Azulejo(i);
        }
    }

    public static void Azulejo(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Azulejos_Dados.FullName + Índice + Diretórios.Formato);
        Size Textura_Tamanho = Gráficos.TTamanho(Gráficos.Tex_Azulejo[Índice]);
        Size Tamanho = new Size(Textura_Tamanho.Width / Globais.Grade - 1, Textura_Tamanho.Height / Globais.Grade - 1);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Azulejo(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Dados básicos
        Listas.Azulejo[Índice].Largura = Binário.ReadByte();
        Listas.Azulejo[Índice].Altura = Binário.ReadByte();

        // Previni erros (Foi trocado de azulejo)
        if (Tamanho != new Size(Listas.Azulejo[Índice].Largura, Listas.Azulejo[Índice].Altura))
        {
            Binário.Dispose();
            Limpar.Azulejo(Índice);
            Escrever.Azulejo(Índice);
            return;
        }

        for (byte x = 0; x <= Tamanho.Width; x++)
            for (byte y = 0; y <= Tamanho.Height; y++)
            {
                // Atributos
                Listas.Azulejo[Índice].Azulejo[x, y].Atributo = Binário.ReadByte();

                // Bloqueio direcional
                for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                    Listas.Azulejo[Índice].Azulejo[x, y].Bloqueio[i] = Binário.ReadBoolean();
            }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Mapas()
    {
        Listas.Mapa = new Listas.Estruturas.Mapas[Listas.Servidor_Dados.Num_Mapas + 1];

        // Limpa e lê os dados
        for (short i = 1; i <= Listas.Mapa.GetUpperBound(0); i++)
        {
            Limpar.Mapa(i);
            Mapa(i);
        }
    }

    public static void Mapa(short Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Mapa(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Mapa[Índice].Revisão = Binário.ReadInt16();
        Listas.Mapa[Índice].Nome = Binário.ReadString();
        Listas.Mapa[Índice].Largura = Binário.ReadByte();
        Listas.Mapa[Índice].Altura = Binário.ReadByte();
        Listas.Mapa[Índice].Moral = Binário.ReadByte();
        Listas.Mapa[Índice].Panorama = Binário.ReadByte();
        Listas.Mapa[Índice].Música = Binário.ReadByte();
        Listas.Mapa[Índice].Coloração = Binário.ReadInt32();
        Listas.Mapa[Índice].Clima.Tipo = Binário.ReadByte();
        Listas.Mapa[Índice].Clima.Intensidade = Binário.ReadByte();
        Listas.Mapa[Índice].Fumaça.Textura = Binário.ReadByte();
        Listas.Mapa[Índice].Fumaça.VelocidadeX = Binário.ReadSByte();
        Listas.Mapa[Índice].Fumaça.VelocidadeY = Binário.ReadSByte();
        Listas.Mapa[Índice].Fumaça.Transparência = Binário.ReadByte();
        Listas.Mapa[Índice].LuzGlobal = Binário.ReadByte();
        Listas.Mapa[Índice].Iluminação = Binário.ReadByte();

        // Ligações
        for (short i = 0; i <= (short)Globais.Direções.Quantidade - 1; i++)
            Listas.Mapa[Índice].Ligação[i] = Binário.ReadInt16();

        // Quantidade de camadas
        byte NumCamadas = Binário.ReadByte();
        Listas.Mapa[Índice].Camada = new System.Collections.Generic.List<Listas.Estruturas.Camada>();

        // Camadas
        for (byte i = 0; i <= NumCamadas; i++)
        {
            // Dados básicos
            Listas.Mapa[Índice].Camada.Add(new Listas.Estruturas.Camada());
            Listas.Mapa[Índice].Camada[i].Nome = Binário.ReadString();
            Listas.Mapa[Índice].Camada[i].Tipo = Binário.ReadByte();

            // Redimensiona os azulejos
            Listas.Mapa[Índice].Camada[i].Azulejo = new Listas.Estruturas.Azulejo_Dados[Listas.Mapa[Índice].Largura + 1, Listas.Mapa[Índice].Altura + 1];

            // Azulejos
            for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
                for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
                {
                    Listas.Mapa[Índice].Camada[i].Azulejo[x, y].x = Binário.ReadByte();
                    Listas.Mapa[Índice].Camada[i].Azulejo[x, y].y = Binário.ReadByte();
                    Listas.Mapa[Índice].Camada[i].Azulejo[x, y].Azulejo = Binário.ReadByte();
                    Listas.Mapa[Índice].Camada[i].Azulejo[x, y].Automático = Binário.ReadBoolean();
                    Listas.Mapa[Índice].Camada[i].Azulejo[x, y].Mini = new Point[4];
                }
        }

        // Dados específicos dos azulejos
        Listas.Mapa[Índice].Azulejo = new Listas.Estruturas.Mapas_Azulejo_Dados[Listas.Mapa[Índice].Largura + 1, Listas.Mapa[Índice].Altura + 1];
        for (byte x = 0; x <= Listas.Mapa[Índice].Largura; x++)
            for (byte y = 0; y <= Listas.Mapa[Índice].Altura; y++)
            {
                Listas.Mapa[Índice].Azulejo[x, y].Atributo = Binário.ReadByte();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_1 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_2 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_3 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Dado_4 = Binário.ReadInt16();
                Listas.Mapa[Índice].Azulejo[x, y].Zona = Binário.ReadByte();
                Listas.Mapa[Índice].Azulejo[x, y].Bloqueio = new bool[(byte)Globais.Direções.Quantidade];

                for (byte i = 0; i <= (byte)Globais.Direções.Quantidade - 1; i++)
                    Listas.Mapa[Índice].Azulejo[x, y].Bloqueio[i] = Binário.ReadBoolean();
            }

        // Luzes
        byte NumLuzes = Binário.ReadByte();
        Listas.Mapa[Índice].Luz = new System.Collections.Generic.List<Listas.Estruturas.Luz_Estrutura>();
        if (NumLuzes > 0)
            for (byte i = 0; i <= NumLuzes - 1; i++)
                Listas.Mapa[Índice].Luz.Add(new Listas.Estruturas.Luz_Estrutura(new Rectangle(Binário.ReadByte(), Binário.ReadByte(), Binário.ReadByte(), Binário.ReadByte())));

        // NPCs
        byte NumNPCs = Binário.ReadByte();
        Listas.Mapa[Índice].NPC = new System.Collections.Generic.List<Listas.Estruturas.Mapa_NPC>();
        Listas.Estruturas.Mapa_NPC NPC = new Listas.Estruturas.Mapa_NPC();
        if (NumNPCs > 0)
            for (byte i = 0; i <= NumNPCs - 1; i++)
            {
                NPC.Índice = Binário.ReadInt16();
                NPC.Zona = Binário.ReadByte();
                NPC.Aparecer = Binário.ReadBoolean();
                NPC.X = Binário.ReadByte();
                NPC.Y = Binário.ReadByte();
                Listas.Mapa[Índice].NPC.Add(NPC);
            }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void NPCs()
    {
        Listas.NPC = new Listas.Estruturas.NPCs[Listas.Servidor_Dados.Num_NPCs + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.NPC.GetUpperBound(0); i++)
        {
            Limpar.NPC(i);
            NPC(i);
        }
    }

    public static void NPC(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.NPCs_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.NPC(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.NPC[Índice].Nome = Binário.ReadString();
        Listas.NPC[Índice].Textura = Binário.ReadInt16();
        Listas.NPC[Índice].Agressividade = Binário.ReadByte();
        Listas.NPC[Índice].Aparecimento = Binário.ReadByte();
        Listas.NPC[Índice].Visão = Binário.ReadByte();
        Listas.NPC[Índice].Experiência = Binário.ReadByte();
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++) Listas.NPC[Índice].Vital[i] = Binário.ReadInt16();
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++) Listas.NPC[Índice].Atributo[i] = Binário.ReadInt16();
        for (byte i = 0; i <= Globais.Máx_NPC_Queda - 1; i++)
        {
            Listas.NPC[Índice].Queda[i].Item_Num = Binário.ReadInt16();
            Listas.NPC[Índice].Queda[i].Quantidade = Binário.ReadInt16();
            Listas.NPC[Índice].Queda[i].Chance = Binário.ReadByte();
        }

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Itens()
    {
        Listas.Item = new Listas.Estruturas.Itens[Listas.Servidor_Dados.Num_Itens + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++)
        {
            Limpar.Item(i);
            Item(i);
        }
    }

    public static void Item(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Itens_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Item(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Item[Índice].Nome = Binário.ReadString();
        Listas.Item[Índice].Descrição = Binário.ReadString();
        Listas.Item[Índice].Textura = Binário.ReadInt16();
        Listas.Item[Índice].Tipo = Binário.ReadByte();
        Listas.Item[Índice].Preço= Binário.ReadInt16();
        Listas.Item[Índice].Empilhável = Binário.ReadBoolean();
        Listas.Item[Índice].NãoDropável = Binário.ReadBoolean();
        Listas.Item[Índice].Req_Level = Binário.ReadInt16();
        Listas.Item[Índice].Req_Classe = Binário.ReadByte();
        Listas.Item[Índice].Poção_Experiência = Binário.ReadInt16();
        for (byte i = 0; i <= (byte)Globais.Vitais.Quantidade - 1; i++) Listas.Item[Índice].Poção_Vital[i] = Binário.ReadInt16();
        Listas.Item[Índice].Equip_Tipo = Binário.ReadByte();
        for (byte i = 0; i <= (byte)Globais.Atributos.Quantidade - 1; i++) Listas.Item[Índice].Equip_Atributo[i] = Binário.ReadInt16();
        Listas.Item[Índice].Arma_Dano = Binário.ReadInt16();

        // Fecha o sistema
        Binário.Dispose();
    }
    #endregion

    #region Cliente
    public static void Ferramentas()
    {
        // Lê todas as ferramentas
        Botões_Dados();
        Digitalizadores_Dados();
        Paineis_Dados();
        Marcadores_Dados();
    }

    public static void Botões_Dados()
    {
        Listas.Botão = new Listas.Estruturas.Botões[Listas.Cliente_Dados.Num_Botões + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Botão.GetUpperBound(0); i++)
        {
            Limpar.Botão(i);
            Botão_Dados(i);
        }
    }

    public static void Botão_Dados(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Botões_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Botão_Dados(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Botão[Índice].Geral.Nome = Binário.ReadString();
        Listas.Botão[Índice].Geral.Posição.X = Binário.ReadInt32();
        Listas.Botão[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Listas.Botão[Índice].Geral.Visível = Binário.ReadBoolean();
        Listas.Botão[Índice].Textura = Binário.ReadByte();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Digitalizadores_Dados()
    {
        Listas.Digitalizador = new Listas.Estruturas.Digitalizadores[Listas.Cliente_Dados.Num_Digitalizadores + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Digitalizador.GetUpperBound(0); i++)
        {
            Limpar.Digitalizador(i);
            Digitalizador_Dados(i);
        }
    }

    public static void Digitalizador_Dados(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Digitalizadores_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Digitalizador_Dados(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Lê os dados
        Listas.Digitalizador[Índice].Geral.Nome = Binário.ReadString();
        Listas.Digitalizador[Índice].Geral.Posição.X = Binário.ReadInt32();
        Listas.Digitalizador[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Listas.Digitalizador[Índice].Geral.Visível = Binário.ReadBoolean();
        Listas.Digitalizador[Índice].Máx_Carácteres = Binário.ReadInt16();
        Listas.Digitalizador[Índice].Largura = Binário.ReadInt16();
        Listas.Digitalizador[Índice].Senha = Binário.ReadBoolean();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Paineis_Dados()
    {
        Listas.Painel = new Listas.Estruturas.Paineis[Listas.Cliente_Dados.Num_Paineis + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Painel.GetUpperBound(0); i++)
        {
            Limpar.Painel(i);
            Painel_Dados(i);
        }
    }

    public static void Painel_Dados(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Paineis_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Painel_Dados(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Carrega os dados
        Listas.Painel[Índice].Geral.Nome = Binário.ReadString();
        Listas.Painel[Índice].Geral.Posição.X = Binário.ReadInt32();
        Listas.Painel[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Listas.Painel[Índice].Geral.Visível = Binário.ReadBoolean();
        Listas.Painel[Índice].Textura = Binário.ReadByte();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Marcadores_Dados()
    {
        Listas.Marcador = new Listas.Estruturas.Marcadores[Listas.Cliente_Dados.Num_Marcadores + 1];

        // Limpa e lê os dados
        for (byte i = 1; i <= Listas.Marcador.GetUpperBound(0); i++)
        {
            Limpar.Marcador(i);
            Marcador_Dados(i);
        }
    }

    public static void Marcador_Dados(byte Índice)
    {
        FileInfo Arquivo = new FileInfo(Diretórios.Marcadores_Dados.FullName + Índice + Diretórios.Formato);

        // Cria o arquivo caso ele não existir
        if (!Arquivo.Exists)
        {
            Escrever.Marcador_Dados(Índice);
            return;
        }

        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Carrega os dados
        Listas.Marcador[Índice].Geral.Nome = Binário.ReadString();
        Listas.Marcador[Índice].Geral.Posição.X = Binário.ReadInt32();
        Listas.Marcador[Índice].Geral.Posição.Y = Binário.ReadInt32();
        Listas.Marcador[Índice].Geral.Visível = Binário.ReadBoolean();
        Listas.Marcador[Índice].Texto = Binário.ReadString();
        Listas.Marcador[Índice].Estado = Binário.ReadBoolean();

        // Fecha o sistema
        Binário.Dispose();
    }
    #endregion
}