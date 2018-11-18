using System;
using SFML.Audio;

class Sound
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
        Amount
    }

    // Lists das músicas
    public enum Músicas
    {
        Menu = 1,
        Amount
    }

    public class Som
    {
        // Formato em o Device irá ler os sons
        public const string Formato = ".wav";

        // Device sonoro
        public static Sound[] Lista;

        public static void Ler()
        {
            // Redimensiona a lista
            Array.Resize(ref Lista, (byte)Sons.Amount);

            // Carrega todos os arquivos e os adiciona a lista
               for (int i = 1; i <= Lista.GetUpperBound(0); i++)
             Lista[i] = new Sound(new SoundBuffer(Diretórios.Sons.FullName + i + Formato));
        }

        public static void Reproduzir(Sons Index, bool Laço = false)
        {
            // Apenas se necessário
            if (!Lists.Opções.Sons) return;

            // Reproduz o áudio
            Lista[(byte)Index].Volume = 20;
            Lista[(byte)Index].Loop = Laço;
            Lista[(byte)Index].Play();
        }

        public static void Parar_Tudo()
        {
            // Apenas se necessário
            if (Lista == null) return;

            // Para todos os sons
            for (byte i = 1; i <= (byte)Sons.Amount - 1; i++)
                Lista[i].Stop();
        }
    }

    public class Música
    {
        // Formato em o Device irá ler as músicas
        public const string Formato = ".ogg";

        // Lista das músicas
        public static Music Reprodutor;

        // Index da música reproduzida atualmente
        public static byte Atual;

        public static void Reproduzir(Músicas Index, bool Laço = false)
        {
            string Diretório = Diretórios.Músicas.FullName + (byte)Index + Formato;

            // Apenas se necessário
            if (Reprodutor != null) return;
            if (!Lists.Opções.Músicas) return;

            // Carrega o áudio
            Reprodutor = new Music(Diretórios.Músicas.FullName + (byte)Index + Formato);
            Reprodutor.Loop = true;
            Reprodutor.Volume = 20;
            Reprodutor.Loop = Laço;

            // Reproduz
            Reprodutor.Play();
            Atual = (byte)Index;
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