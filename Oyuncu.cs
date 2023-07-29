using System;
using SFMLResim;
using SFMLSes;

namespace SAOyun2023
{
    class Oyuncu
    {
        static private int resim;
        static private int canResmi;
        static private int mermiResmi0;
        static private int mermiResmi1;

        static int atesSesi;

        public float x { get; private set; }
        public float y { get; private set; }
        public float r { get; private set; }

        private float mermiZamani;
        public int can { get; private set; }

        int mermiSayisi0, mermiSayisi1;

        static Oyuncu()
        {
            resim = Resim.Ekle("res\\Oyuncu\\1.png");
            canResmi = Resim.Ekle("res\\Oyuncu\\can.png");
            mermiResmi0 = Resim.Ekle("res\\Oyuncu\\mermi0.png");
            mermiResmi1 = Resim.Ekle("res\\Oyuncu\\mermi1.png");

            atesSesi = Ses.SesEkle("res\\Ses\\ates.ogg");
        }

        public Oyuncu(float r)
        {
            can = 5;

            this.r = r;
            x = 400;
            y = 300;

            mermiZamani = 0;
            mermiSayisi0 = 100;
            mermiSayisi1 = 5;
        }

        public void Hesapla(float t)
        {
            mermiZamani += t;
        }

        public void Ciz()
        {
            Resim.Ciz(resim, x - r, y - r, 2 * r, 2 * r);

            for (int i = 0; i < can; i++)
                Resim.Ciz(canResmi, 20*i, 0, 50, 50);

            for (int i=0; i<mermiSayisi1; i++)
                Resim.Ciz(mermiResmi1, 780 - i*20, 0, 20, 50);

            for (int i = 0; i < mermiSayisi0 / 5; i++)
                Resim.Ciz(mermiResmi0, 750, 70 + 15*i, 50, 10);

            int w = mermiSayisi0 % 5;
            Resim.Ciz(mermiResmi0, 750, 70 + 15 * (mermiSayisi0 / 5), 10 * w, 10);

        }

        private void Git(float x, float y)
        {
            if (x < r)
                x = r;
            else if (x > 800 - r)
                x = 800 - r;

            this.x = x;

            if (y < r)
                y = r;
            else if (y > 600 - r)
                y = 600 - r;

            this.y = y;
        }

        private void AtesEt(int tip=0)
        {
            if (mermiZamani > 0.2)
            {

                if (tip == 0 && mermiSayisi0 > 0)
                {
                    Mermiler.Ekle(x, y - r, tip);
                    mermiSayisi0--;

                    Ses.SesCal(atesSesi);
                }

                if (tip == 1 && mermiSayisi1 > 0)
                {
                    Mermiler.Ekle(x, y - r, tip);
                    mermiSayisi1--;

                    Ses.SesCal(atesSesi);
                }

                mermiZamani = 0;
            }
        }

        public void FareKontrol()
        {
            Git(Resim.FarePos.X, Resim.FarePos.Y);

            if (Resim.FareSolBasili == true)
                AtesEt(0);
            else if (Resim.FareSagAsagi == true)
                AtesEt(1);
        }

        public void CanAzalt()
        {
            can--;
            if (can < 0)
                can = 0;
        }

        public void CanEkle()
        {
            can++;
            if (can > 10)
                can = 10;
        }

        public void MermiEkle(int n)
        {
            mermiSayisi0 += n;
            if (mermiSayisi0 > 150)
                mermiSayisi0 = 150;
        }

        public void BombaEkle(int n)
        {
            mermiSayisi1 += n;
            if (mermiSayisi1 > 5)
                mermiSayisi1 = 5;
        }



    }
}
