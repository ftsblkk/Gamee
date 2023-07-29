using System;
using System.Collections.Generic;
using SFMLSes;

namespace SAOyun2023
{
    static class Hediyeler 
    {
        static private List<Hediye> hediyeler;
        static int hediyeAlSesi; 



        static Random rnd = new Random();

        static Hediyeler()
        {
            hediyeler = new List<Hediye>();

            hediyeAlSesi = Ses.SesEkle("res\\Ses\\HediyeAl.wav");
        }

        static public void Ekle(int tip, float x, float y)
        {
            hediyeler.Add(new Hediye(tip, x, y));
        }

        static public void Hesapla(float t, Oyuncu o)
        {
            foreach (Hediye h in hediyeler)
                h.Hesapla(t);

            for (int i = hediyeler.Count - 1; i >= 0; i--)
                if (hediyeler[i].y > 700)
                    hediyeler.RemoveAt(i);

            for (int i = hediyeler.Count - 1; i >= 0; i--)
                if (hediyeler[i].Carpti(o) == true)
                {
                    Ses.SesCal(hediyeAlSesi);

                    int tip = hediyeler[i].tip;

                    if (tip == 0)
                        o.MermiEkle(10);
                    else if (tip == 1)
                        o.BombaEkle(1);
                    else if (tip == 2)
                        Oyun.SkorEkle(3);
                    else if (tip == 3)
                        o.CanEkle();
                    else if (tip == 4)
                    {
                        // **********************************************************
                        Gemiler.HepsiniPatlat();

                        //for (int j = 0; j < 100; j++)
                        //    Patlamalar.Ekle(rnd.Next(800), rnd.Next(600), rnd.Next(20, 100));
                        // **********************************************************
                    }

                    hediyeler.RemoveAt(i);
                }
        }

        static public void Ciz()
        {
            foreach (Hediye h in hediyeler)
                h.Ciz();
        }

        static public int Say()
        {
            return hediyeler.Count;
        }

        static public void Sil()
        {
            hediyeler.Clear();
        }
    }
}
