using System;
using System.Collections.Generic;
using SFMLSes;

namespace SAOyun2023
{
    static class Gemiler
    {
        static private List<Gemi> gemiler;
        static int vurSesi;
        static int patlamaSesi;

        static Random rnd = new Random();

        static Gemiler()
        {
            gemiler = new List<Gemi>();

            vurSesi = Ses.SesEkle("res\\Ses\\vur.wav");
            patlamaSesi = Ses.SesEkle("res\\Ses\\patla.wav");
        }

        static public void Ekle(int n = 1)
        {
            for (int i = 0; i < n; i++)
                gemiler.Add( new Gemi() );
        }

        static public void Hesapla(float t, Oyuncu o)
        {
            foreach (Gemi g in gemiler)
                g.Hesapla(t);

            for (int i = gemiler.Count - 1; i >= 0; i--)
            {
                if (gemiler[i].Carpti(o) == true)
                {
                    o.CanAzalt();

                    Patlamalar.Ekle(gemiler[i].x, gemiler[i].y, 3*gemiler[i].r);
                    gemiler.RemoveAt(i);

                    Ses.SesCal(patlamaSesi);
                }
            }

            for (int i = gemiler.Count - 1; i >= 0; i--)
            {
                if (Mermiler.Vurdu(gemiler[i]) == true)
                {
                    gemiler[i].CanAzalt();

                    Ses.SesCal(vurSesi);

                    if (gemiler[i].can < 1)
                    {
                        Oyun.SkorEkle(2);
                        Patlamalar.Ekle(gemiler[i].x, gemiler[i].y, 3 * gemiler[i].r);

                        Ses.SesCal(patlamaSesi);

                        int ht = rnd.Next(100);

                        //0..9 - 10..29 - 30..59 - 60..99
                        if (ht < 10)
                            Hediyeler.Ekle(4, gemiler[i].x, gemiler[i].y);
                        else if (ht < 30)
                            Hediyeler.Ekle(3, gemiler[i].x, gemiler[i].y);
                        else if (ht < 60)
                            Hediyeler.Ekle(1, gemiler[i].x, gemiler[i].y);
                        else // if (t < 100)
                            Hediyeler.Ekle(0, gemiler[i].x, gemiler[i].y);

                        gemiler.RemoveAt(i);
                    }
                }
            }

            // **********************************************************
            for (int i = gemiler.Count - 1; i >= 0; i--)
                if (gemiler[i].can < 1)
                {
                    for (int j = 0; j < 10; j++)
                        Patlamalar.Ekle(gemiler[i].x + rnd.Next(-100, 100),
                                        gemiler[i].y + rnd.Next(-100, 100),
                                        3 * gemiler[i].r);

                    gemiler.RemoveAt(i);

                    Ses.SesCal(patlamaSesi);
                }
            // **********************************************************

        }

        static public void Ciz()
        {
            foreach (Gemi g in gemiler)
                g.Ciz();
        }

        static public int Say()
        {
            return gemiler.Count;
        }

        // **********************************************************
        static public void HepsiniPatlat()
        {
            foreach (Gemi g in gemiler)
                g.CanAzalt(999);
        }
        // **********************************************************


    }
}
