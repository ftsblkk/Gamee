using System;
using SFMLResim;

namespace SAOyun2023
{
    class Mermi
    {
        static int mermiResmi0, mermiResmi1;

        public float y { get; private set; }
        float x, vy, r;
        private int tip;

        static Mermi()
        {
            mermiResmi0 = Resim.Ekle("res\\mermi0.png");
            mermiResmi1 = Resim.Ekle("res\\mermi1.png");
        }

        public Mermi(float x, float y, int tip = 0)
        {
            this.tip = tip;

            this.x = x;
            this.y = y;

            r = 10;

            if (tip == 0)
                vy = 500;
            else
                vy = 200;
        }

        public void Hesapla(float t)
        {
            y -= vy * t;
        }

        public void Ciz()
        {
            if (tip == 0)
                Resim.Ciz(mermiResmi0, x - r, y - r, 2 * r, 2 * r);
            else
                Resim.Ciz(mermiResmi1, x - r, y - 2*r, 2 * r, 4 * r);
        }

        public bool Vurdu(Gemi g)
        {
            if (x > g.x - g.r && x < g.x + g.r &&
                y > g.y - g.r && y < g.y + g.r)
            {
                Patlamalar.Ekle(x, y, 2 * r);

                if (tip == 0)
                    y = -999;

                return true;
            }

            return false;
        }

        public bool Vurdu(Asteroid a)
        {
            if (x > a.x - a.r && x < a.x + a.r &&
                y > a.y - a.r && y < a.y + a.r)
            {
                Patlamalar.Ekle(x, y, 2 * r);

                if (tip == 0)
                    y = -999;

                return true;
            }

            return false;
        }

    }
}
