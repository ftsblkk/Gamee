using System;
using System.Collections.Generic;

namespace SAOyun2023
{
    static class Patlamalar
    {
        static List<Patlama> patlamalar;

        static Patlamalar()
        {
            patlamalar = new List<Patlama>();
        }

        public static void Ekle(float x, float y, float r)
        {
            patlamalar.Add( new Patlama(x, y, r) );
        }

        public static void Hesapla(float t)
        {
            foreach (Patlama p in patlamalar)
                p.Hesapla(t);

            for (int i = patlamalar.Count - 1; i >= 0; i--)
                if (patlamalar[i].Bitti() == true)
                    patlamalar.RemoveAt(i);
        }

        public static void Ciz()
        {
            foreach (Patlama p in patlamalar)
                p.Ciz();
        }

        public static int Say()
        {
            return patlamalar.Count;
        }

    }
}
