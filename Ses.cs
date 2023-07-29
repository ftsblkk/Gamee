using System;
using System.Collections.Generic;

using SFML.Audio;

namespace SFMLSes
{
    public static class Ses
    {
        private static List<Sound> sesler = new List<Sound>();
        private static List<Music> musikler = new List<Music>();
        private static bool sessiz = false;


        /// <summary>Tüm ses ve müzikleri durdurur veya yeniden başlatır</summary>
        /// <param name="durum">true ise tüm sesler durur, false ise sesler oynatılır</param>
        public static void Sus(bool durum = true)
        {
            sessiz = durum;

            if (sessiz)
            {
                foreach (Music m in musikler)
                    m.Pause();
            }
            else
            {
                foreach (Music m in musikler)
                    m.Play();
            }
        }

        /// <summary>Yeni bir ses yükler. Yüklenen sesi belirten bir sayı döndürür</summary>
        /// <param name="dosyaAdi">Yüklenecek ses dosyası adı</param>
        public static int SesEkle(string dosyaAdi)
        {
            SoundBuffer sbuffer = new SoundBuffer(dosyaAdi);
            Sound snd = new Sound(sbuffer);

            sesler.Add(snd);

            return sesler.Count - 1;
        }

        /// <summary>Belirtilen sesi çalar</summary>
        /// <param name="n">Ses no</param>
        public static void SesCal(int n)
        {
            if (sessiz)
                return;

            if (n < sesler.Count)
                sesler[n].Play();
        }

        /// <summary>Yeni bir müzik yükler. Yüklenen müziği belirten bir sayı döndürür</summary>
        /// <param name="dosyaAdi">Yüklenecek müzik dosyası adı</param>
        public static int MusikEkle(string dosyaAdi)
        {
            Music m = new Music(dosyaAdi);
            m.Loop = true;

            musikler.Add(m);

            return musikler.Count - 1;
        }

        /// <summary>Belirtilen müziği çalar</summary>
        /// <param name="n">Müzik no</param>
        public static void MusikCal(int n)
        {
            if (sessiz)
                return;

            if (n < musikler.Count)
                musikler[n].Play();
        }

        /// <summary>Belirtilen müziği geçici olarak durdurur</summary>
        /// <param name="n">Müzik no</param>
        public static void MusikDurdur(int n)
        {
            if (n < musikler.Count)
                musikler[n].Pause();
        }

        /// <summary>Belirtilen müziği durdurur</summary>
        /// <param name="n">Müzik no</param>
        public static void MusikBitir(int n)
        {
            if (n < musikler.Count)
                musikler[n].Stop();
        }

        /// <summary>Belirtilen müziğin seviyesini ayarlar</summary>
        /// <param name="n">Müzik no</param>
        /// <param name="seviye">Müzik seviyesi ([0...100] aralığında)</param>
        public static void MusikSeviye(int n, int seviye)
        {
            if (seviye < 0)
                seviye = 0;
            if (seviye > 100)
                seviye = 100;

            if (n < musikler.Count)
                musikler[n].Volume = seviye;
        }
    }
}
