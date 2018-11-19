using System;
using SFML.Audio;

class Audio
{
    // List of sounds
    public enum Sons
    {
        Click = 1,
        Overlap,
        Chuva,
        Trovão_1,
        Trovão_2,
        Trovão_3,
        Trovão_4,
        Amount
    }
// Lists of Musics
    public enum Músicas
    {
        Menu = 1,
        Amount
    }

    public class Som
    {
        // Format in the Device will read the sounds
        public const string Format = ".wav";

        // Sound device
        public static Sound[] List;

        public static void Read()
        {
            // Resize the List
            Array.Resize(ref List, (byte)Sons.Amount);

            // Loads all files and adds them to List
               for (int i = 1; i <= List.GetUpperBound(0); i++)
             List[i] = new Sound(new SoundBuffer(Directories.Sons.FullName + i + Format));
        }

        public static void Reproduce(Sons Index, bool Laço = false)
        {
            // Apenas se necessário
            if (!Lists.Options.Sons) return;

            // Reproduz o Sound
            List[(byte)Index].Volume = 20;
            List[(byte)Index].Loop = Laço;
            List[(byte)Index].Play();
        }

        public static void Stop_All()
        {
            // Apenas se necessário
            if (List == null) return;

            // Para todos os sons
            for (byte i = 1; i <= (byte)Sons.Amount - 1; i++)
                List[i].Stop();
        }
    }

    public class Música
    {
        // Format in the Device will read the Musics
        public const string Format = ".ogg";

        // List of Musics
        public static Music Reprodutor;

        //Index of Music currently played
        public static byte Atual;

        public static void Reproduce(Músicas Index, bool Laço = false)
        {
            string Diretório = Directories.Músicas.FullName + (byte)Index + Format;

            // Only if necessary
            if (Reprodutor != null) return;
            if (!Lists.Options.Músicas) return;

            // Load the Sound
            Reprodutor = new Music(Directories.Músicas.FullName + (byte)Index + Format);
            Reprodutor.Loop = true;
            Reprodutor.Volume = 20;
            Reprodutor.Loop = Laço;

            // Reproduz
            Reprodutor.Play();
            Atual = (byte)Index;
        }

        public static void Stop()
        {
            // For Music that is playing
            if (Reprodutor != null && Atual != 0)
            {
                Reprodutor.Stop();
                Reprodutor.Dispose();
                Reprodutor = null;
            }
        }
    }
}