using System;
using SFMLResim;

namespace SAOyun2023
{
    static class Oyun
    {
        static int bgResim0, bgResim1;
        static float bgY;
        static int skor;
        
        static Oyuncu o;

        static Random rnd = new Random();


        static Oyun()
        {
            bgResim0 = Resim.Ekle("res\\bg0.png");
            bgResim1 = Resim.Ekle("res\\bg1.png");

            bgY = 0;
        }

        static public void YeniOyun()
        {
            o = new Oyuncu(30);
            skor = 0;
        }

        static public void Hesapla(float t)
        {
            bgY += 10 * t;
            if (bgY > 600)
                bgY = 0;

            if (Gemiler.Say() < 5)
                Gemiler.Ekle(1);

            if (Asteroidler.Say() < 3)
                Asteroidler.Ekle();

            //if (Hediyeler.Say() < 10)
            //    Hediyeler.Ekle(rnd.Next(5), rnd.Next(100, 700), rnd.Next(-200, -100));
            
            Gemiler.Hesapla(t, o);
            Asteroidler.Hesapla(t, o);
            o.Hesapla(t);
            Patlamalar.Hesapla(t);
            Mermiler.Hesapla(t);
            Hediyeler.Hesapla(t, o);
        }

        static public void Ciz()
        {
            // PARALLAX SCROLLING
            Resim.Ciz(bgResim0, 0, 0, 800, 600);
            Resim.Ciz(bgResim1, 0, bgY, 800, 600);
            Resim.Ciz(bgResim1, 0, bgY - 600, 800, 600);
            
            Asteroidler.Ciz();
            Gemiler.Ciz();
            Patlamalar.Ciz();
            Hediyeler.Ciz();
            Mermiler.Ciz();
            o.Ciz();

            Resim.YaziYaz(skor.ToString(), 0, 50, 50);
        }

        static public void FareKontrol()
        {
            o.FareKontrol();
        }

        static public void SkorEkle(int n)
        {
            skor += n;
            if (skor < 0)
                skor = 0; 
        }
    }
}
