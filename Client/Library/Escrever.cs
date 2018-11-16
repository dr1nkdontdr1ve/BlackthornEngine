using System.IO;

class Escrever
{
    public static void Dados()
    {
        // Salva todos os dados
        Opções();
    }

    public static void Opções()
    {
        // Cria um arquivo temporário
        BinaryWriter Arquivo = new BinaryWriter(File.OpenWrite(Diretórios.Opções.FullName));

        // Carrega todas as opções
        Arquivo.Write(Listas.Opções.Jogo_Nome);
        Arquivo.Write(Listas.Opções.SalvarUsuário);
        Arquivo.Write(Listas.Opções.Sons);
        Arquivo.Write(Listas.Opções.Músicas);
        Arquivo.Write(Listas.Opções.Usuário);

        // Fecha o arquivo
        Arquivo.Dispose();
    }

    public static void Mapa(short Índice)
    {
        // Cria um arquivo temporário
        FileInfo Arquivo = new FileInfo(Diretórios.Mapas_Dados.FullName + Índice + Diretórios.Formato);
        BinaryWriter Binário = new BinaryWriter(Arquivo.OpenWrite());

        // Escreve os dados
        Binário.Write(Listas.Mapa.Revisão);
        Binário.Write(Listas.Mapa.Nome);
        Binário.Write(Listas.Mapa.Largura);
        Binário.Write(Listas.Mapa.Altura);
        Binário.Write(Listas.Mapa.Moral);
        Binário.Write(Listas.Mapa.Panorama);
        Binário.Write(Listas.Mapa.Música);
        Binário.Write(Listas.Mapa.Coloração);
        Binário.Write(Listas.Mapa.Clima.Tipo);
        Binário.Write(Listas.Mapa.Clima.Intensidade);
        Binário.Write(Listas.Mapa.Fumaça.Textura);
        Binário.Write(Listas.Mapa.Fumaça.VelocidadeX);
        Binário.Write(Listas.Mapa.Fumaça.VelocidadeY);
        Binário.Write(Listas.Mapa.Fumaça.Transparência);

        // Ligação
        for (short i = 0; i <= (short)Jogo.Direções.Quantidade - 1; i++)
            Binário.Write(Listas.Mapa.Ligação[i]);

        // Azulejos
        Binário.Write((byte)Listas.Mapa.Azulejo[0, 0].Dados.GetUpperBound(1));
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
                for (byte c = 0; c <= (byte)global::Mapa.Camadas.Quantidade - 1; c++)
                    for (byte q = 0; q <= Listas.Mapa.Azulejo[x, y].Dados.GetUpperBound(1); q++)
                    {
                        Binário.Write(Listas.Mapa.Azulejo[x, y].Dados[c, q].x);
                        Binário.Write(Listas.Mapa.Azulejo[x, y].Dados[c, q].y);
                        Binário.Write(Listas.Mapa.Azulejo[x, y].Dados[c, q].Azulejo);
                        Binário.Write(Listas.Mapa.Azulejo[x, y].Dados[c, q].Automático);
                    }

        // Dados específicos dos azulejos
        for (byte x = 0; x <= Listas.Mapa.Largura; x++)
            for (byte y = 0; y <= Listas.Mapa.Altura; y++)
            {
                Binário.Write((byte)Listas.Mapa.Azulejo[x, y].Atributo);
                for (byte i = 0; i <= (byte)Jogo.Direções.Quantidade - 1; i++)
                    Binário.Write(Listas.Mapa.Azulejo[x, y].Bloqueio[i]);
            }

        // Luzes
        Binário.Write(Listas.Mapa.Luz.GetUpperBound(0));
        if (Listas.Mapa.Luz.GetUpperBound(0) > 0)
            for (byte i = 0; i <= Listas.Mapa.Luz.GetUpperBound(0); i++)
            {
                Binário.Write(Listas.Mapa.Luz[i].X);
                Binário.Write(Listas.Mapa.Luz[i].Y);
                Binário.Write(Listas.Mapa.Luz[i].Largura);
                Binário.Write(Listas.Mapa.Luz[i].Altura);
            }

        // NPCs
        Binário.Write(Listas.Mapa.NPC.GetUpperBound(0));
        if (Listas.Mapa.NPC.GetUpperBound(0) > 0)
            for (byte i = 1; i <= Listas.Mapa.NPC.GetUpperBound(0) ; i++)
                Binário.Write(Listas.Mapa.NPC[i]);

        // Fecha o sistema
        Binário.Dispose();
    }
}