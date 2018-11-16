using System;
using System.IO;

partial class Ler
{
    public static void Necessário()
    {
        // Carrega todos os dados
        Servidor_Dados();
        Console.WriteLine("Dados carregados.");
        Classes();
        Console.WriteLine("Classes carregadas.");
        NPCs();
        Console.WriteLine("NPCs carregados.");
        Itens();
        Console.WriteLine("Itens carregados.");
        Mapas();
        Console.WriteLine("Mapas carregados.");
    }

    public static void Servidor_Dados()
    {
        // Cria um sistema binário para a manipulação dos dados
        BinaryReader Binário = new BinaryReader(Diretórios.Servidor_Dados.OpenRead());

        // Lê os dados
        Listas.Servidor_Dados.Game_Nome = Binário.ReadString();
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

    public static void Jogador(byte Índice, string Nome, bool Personagens = true)
    {
        string Diretório = Diretórios.Contas.FullName + Nome + Diretórios.Formato;

        // Cria um arquivo temporário
        BinaryReader Arquivo = new BinaryReader(File.OpenRead(Diretório));

        // Carrega os dados e os adiciona ao cache
        Listas.Jogador[Índice].Usuário = Arquivo.ReadString();
        Listas.Jogador[Índice].Senha = Arquivo.ReadString();
        Listas.Jogador[Índice].Acesso = (Game.Acessos)Arquivo.ReadByte();

        // Dados do personagem
        if (Personagens)
            for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
            {
                Listas.Jogador[Índice].Personagem[i].Nome = Arquivo.ReadString();
                Listas.Jogador[Índice].Personagem[i].Classe = Arquivo.ReadByte();
                Listas.Jogador[Índice].Personagem[i].Gênero = Arquivo.ReadBoolean();
                Listas.Jogador[Índice].Personagem[i].Level = Arquivo.ReadInt16();
                Listas.Jogador[Índice].Personagem[i].Experiência = Arquivo.ReadInt16();
                Listas.Jogador[Índice].Personagem[i].Pontos = Arquivo.ReadByte();
                Listas.Jogador[Índice].Personagem[i].Mapa = Arquivo.ReadInt16();
                Listas.Jogador[Índice].Personagem[i].X = Arquivo.ReadByte();
                Listas.Jogador[Índice].Personagem[i].Y = Arquivo.ReadByte();
                Listas.Jogador[Índice].Personagem[i].Direção = (Game.Direções)Arquivo.ReadByte();
                for (byte n = 0; n <= (byte)Game.Vitais.Quantidade - 1; n++) Listas.Jogador[Índice].Personagem[i].Vital[n] = Arquivo.ReadInt16();
                for (byte n = 0; n <= (byte)Game.Atributos.Quantidade - 1; n++) Listas.Jogador[Índice].Personagem[i].Atributo[n] = Arquivo.ReadInt16();
                for (byte n = 1; n <= Game.Máx_Inventário; n++)
                {
                    Listas.Jogador[Índice].Personagem[i].Inventário[n].Item_Num = Arquivo.ReadInt16();
                    Listas.Jogador[Índice].Personagem[i].Inventário[n].Quantidade = Arquivo.ReadInt16();
                }
                for (byte n = 0; n <= (byte)Game.Equipamentos.Quantidade - 1; n++) Listas.Jogador[Índice].Personagem[i].Equipamento[n] = Arquivo.ReadInt16();
                for (byte n = 1; n <= Game.Máx_Hotbar; n++)
                {
                    Listas.Jogador[Índice].Personagem[i].Hotbar[n].Tipo = Arquivo.ReadByte();
                    Listas.Jogador[Índice].Personagem[i].Hotbar[n].Slot = Arquivo.ReadByte();
                }
            }

        // Descarrega o arquivo
        Arquivo.Dispose();
    }

    public static string Jogador_Senha(string Usuário)
    {
        // Cria um arquivo temporário
        BinaryReader Arquivo = new BinaryReader(File.OpenRead(Diretórios.Contas.FullName + Usuário + Diretórios.Formato));

        // Encontra a senha da conta
        Arquivo.ReadString();
        string SenhaCarregada = Arquivo.ReadString();

        // Descarrega o arquivo
        Arquivo.Dispose();

        // Retorna o valor da função
        return SenhaCarregada;
    }

    public static string Personagens_Nomes()
    {
        // Cria o arquivo caso ele não existir
        if (!Diretórios.Personagens.Exists)
        {
            Escrever.Personagens(string.Empty);
            return string.Empty;
        }

        // Cria um arquivo temporário
        StreamReader Arquivo = new StreamReader(Diretórios.Personagens.FullName);

        // Carrega todos os nomes dos personagens
        string Personagens = Arquivo.ReadToEnd();

        // Descarrega o arquivo
        Arquivo.Dispose();

        // Retorna o valor de acordo com o que foi carregado
        return Personagens;
    }

    public static void Classes()
    {
        Listas.Classe = new Listas.Estruturas.Classes[Listas.Servidor_Dados.Num_Classes + 1];

        // Lê os dados
        for (byte i = 1; i <= Listas.Classe.GetUpperBound(0); i++)
            Classe(i);
    }

    public static void Classe(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Classes.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Redimensiona os valores necessários 
        Listas.Classe[Índice].Vital = new short[(byte)Game.Vitais.Quantidade];
        Listas.Classe[Índice].Atributo = new short[(byte)Game.Atributos.Quantidade];

        // Lê os dados
        Listas.Classe[Índice].Nome = Binário.ReadString();
        Listas.Classe[Índice].Textura_Masculina = Binário.ReadInt16();
        Listas.Classe[Índice].Textura_Feminina = Binário.ReadInt16();
        Listas.Classe[Índice].Aparecer_Mapa = Binário.ReadInt16();
        Listas.Classe[Índice].Aparecer_Direção = Binário.ReadByte();
        Listas.Classe[Índice].Aparecer_X = Binário.ReadByte();
        Listas.Classe[Índice].Aparecer_Y = Binário.ReadByte();
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++) Listas.Classe[Índice].Vital[i] = Binário.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Atributos.Quantidade - 1; i++) Listas.Classe[Índice].Atributo[i] = Binário.ReadInt16();

        // Fecha o sistema
        Binário.Dispose();
    }

    public static void Itens()
    {
        Listas.Item = new Listas.Estruturas.Itens[Listas.Servidor_Dados.Num_Itens + 1];

        // Lê os dados
        for (byte i = 1; i <= Listas.Item.GetUpperBound(0); i++)
            Item(i);
    }

    public static void Item(byte Índice)
    {
        // Cria um sistema binário para a manipulação dos dados
        FileInfo Arquivo = new FileInfo(Diretórios.Itens.FullName + Índice + Diretórios.Formato);
        BinaryReader Binário = new BinaryReader(Arquivo.OpenRead());

        // Redimensiona os valores necessários 
        Listas.Item[Índice].Poção_Vital = new short[(byte)Game.Vitais.Quantidade];
        Listas.Item[Índice].Equip_Atributo = new short[(byte)Game.Atributos.Quantidade];

        // Lê os dados
        Listas.Item[Índice].Nome = Binário.ReadString();
        Listas.Item[Índice].Descrição = Binário.ReadString();
        Listas.Item[Índice].Textura = Binário.ReadInt16();
        Listas.Item[Índice].Tipo = Binário.ReadByte();
        Listas.Item[Índice].Preço = Binário.ReadInt16();
        Listas.Item[Índice].Empilhável = Binário.ReadBoolean();
        Listas.Item[Índice].NãoDropável = Binário.ReadBoolean();
        Listas.Item[Índice].Req_Level = Binário.ReadInt16();
        Listas.Item[Índice].Req_Classe = Binário.ReadByte();
        Listas.Item[Índice].Poção_Experiência = Binário.ReadInt16();
        for (byte i = 0; i <= (byte)Game.Vitais.Quantidade - 1; i++) Listas.Item[Índice].Poção_Vital[i] = Binário.ReadInt16();
        Listas.Item[Índice].Equip_Tipo = Binário.ReadByte();
        for (byte i = 0; i <= (byte)Game.Atributos.Quantidade - 1; i++) Listas.Item[Índice].Equip_Atributo[i] = Binário.ReadInt16();
        Listas.Item[Índice].Arma_Dano = Binário.ReadInt16();

        // Fecha o sistema
        Binário.Dispose();
    }
}