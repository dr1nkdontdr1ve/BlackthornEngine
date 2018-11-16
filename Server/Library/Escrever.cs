using System.IO;

class Escrever
{
    public static void Jogador(byte Índice)
    {
        string Diretório = Diretórios.Contas.FullName + Listas.Jogador[Índice].Usuário + Diretórios.Formato;

        // Evita erros
        if (Listas.Jogador[Índice].Usuário == string.Empty) return;

        // Cria um arquivo temporário
        BinaryWriter Arquivo = new BinaryWriter(File.OpenWrite(Diretório));

        // Salva os dados no arquivo
        Arquivo.Write(Listas.Jogador[Índice].Usuário);
        Arquivo.Write(Listas.Jogador[Índice].Senha);
        Arquivo.Write((byte)Listas.Jogador[Índice].Acesso);

        for (byte i = 1; i <= Listas.Servidor_Dados.Máx_Personagens; i++)
        {
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Nome);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Classe);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Gênero);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Level);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Experiência);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Pontos);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Mapa);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].X);
            Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Y);
            Arquivo.Write((byte)Listas.Jogador[Índice].Personagem[i].Direção);
            for (byte n = 0; n <= (byte)Jogo.Vitais.Quantidade - 1; n++) Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Vital[n]);
            for (byte n = 0; n <= (byte)Jogo.Atributos.Quantidade - 1; n++) Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Atributo[n]);
            for (byte n = 1; n <= Jogo.Máx_Inventário; n++)
            {
                Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Inventário[n].Item_Num);
                Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Inventário[n].Quantidade);
            }
            for (byte n = 0; n <= (byte)Jogo.Equipamentos.Quantidade - 1; n++) Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Equipamento[n]);
            for (byte n = 1; n <= Jogo.Máx_Hotbar; n++)
            {
                Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Hotbar[n].Tipo);
                Arquivo.Write(Listas.Jogador[Índice].Personagem[i].Hotbar[n].Slot);
            }
        }

        // Descarrega o arquivo
        Arquivo.Dispose();
    }

    public static void Personagem(string Nome)
    {
        // Cria um arquivo temporário
        StreamWriter Arquivo = new StreamWriter(Diretórios.Personagens.FullName, true);

        // Salva o nome do personagem no arquivo
        Arquivo.Write(";" + Nome + ":");

        // Descarrega o arquivo
        Arquivo.Dispose();
    }

    public static void Personagens(string Personagens)
    {
        // Cria um arquivo temporário
        StreamWriter Arquivo = new StreamWriter(Diretórios.Personagens.FullName);

        // Salva o nome do personagem no arquivo
        Arquivo.Write(Personagens);

        // Descarrega o arquivo
        Arquivo.Dispose();
    }
}