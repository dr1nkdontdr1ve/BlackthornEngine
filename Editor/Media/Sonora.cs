using System;
using SFML.Audio;

class Áudio
{
    // Lista dos sons
    public enum Sons
    {
        Clique = 1,
        Sobrepor,
        Chuva,
        Trovão_1,
        Trovão_2,
        Trovão_3,
        Trovão_4,
        Quantidade
    }

    // Listas das músicas
    public enum Músicas
    {
        Menu = 1,
        Quantidade
    }

    public class Som
    {
        // Formato em o dispositivo irá ler os sons
        public const string Formato = ".wav";

        // Dispositivo sonoro
        public static Sound[] Lista;

        public static void Ler()
        {
            // Redimensiona a lista
            Array.Resize(ref Lista, (byte)Sons.Quantidade);

            // Carrega todos os arquivos e os adiciona a lista
            for (int i = 1; i <= Lista.GetUpperBound(0); i++)
                Lista[i] = new Sound(new SoundBuffer(Diretórios.Sons.FullName + i + Formato));
        }

        public static void Reproduzir(Sons Índice, bool Laço = false)
        {
            // Somente se necessário
            if (Editor_Mapas.Objetos.Visible && !Editor_Mapas.Objetos.butÁudio.Checked) return;

            // Reproduz o áudio
            Lista[(byte)Índice].Volume = 20;
            Lista[(byte)Índice].Loop = Laço;
            Lista[(byte)Índice].Play();
        }

        public static void Parar_Tudo()
        {
            // Apenas se necessário
            if (Lista == null) return;

            // Para todos os sons
            for (byte i = 1; i <= (byte)Sons.Quantidade - 1; i++)
                Lista[i].Stop();
        }
    }

    public class Música
    {
        // Formato em o dispositivo irá ler as músicas
        public const string Formato = ".ogg";

        // Lista das músicas
        public static Music Reprodutor;

        // Índice da música reproduzida atualmente
        public static byte Atual;

        public static void Reproduzir(Músicas Índice, bool Laço = false)
        {
            System.IO.FileInfo Arquivo = new System.IO.FileInfo(Diretórios.Músicas.FullName + (byte)Índice + Formato);

            // Apenas se necessário
            if (Reprodutor != null) return;
            if (Editor_Mapas.Objetos.Visible && !Editor_Mapas.Objetos.butÁudio.Checked) return;
            if (!Arquivo.Exists) return;

            // Carrega o áudio
            Reprodutor = new Music(Diretórios.Músicas.FullName + (byte)Índice + Formato);
            Reprodutor.Loop = true;
            Reprodutor.Volume = 20;
            Reprodutor.Loop = Laço;

            // Reproduz
            Reprodutor.Play();
            Atual = (byte)Índice;
        }

        public static void Parar()
        {
            // Para a música que está tocando
            if (Reprodutor != null && Atual != 0)
            {
                Reprodutor.Stop();
                Reprodutor.Dispose();
                Reprodutor = null;
            }
        }
    }
}