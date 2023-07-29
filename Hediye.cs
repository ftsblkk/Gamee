using System;
using System.Collections.Generic;
using SFMLResim;

namespace SAOyun2023
{
    class Hediye
    {
        static private List<int> resimler;

        public int tip { get; private set; }
        private float x, r, vy;

        public float y { get; private set; }

        static Hediye()
        {
            resimler = new List<int>();
            for (int i = 0; i <= 4; i++)
                resimler.Add(Resim.Ekle("res\\Hediye\\" + i + ".png"));
        }

        public Hediye(int tip, float x, float y)
        {
            this.tip = tip;
            this.x = x;
            this.y = y;

            r = 25;
            vy = 100;
        }

        public void Hesapla(float t)
        {
            y += vy * t;
        }

        public void Ciz()
        {
            Resim.Ciz(resimler[tip], x - r, y - r, 2 * r, 2 * r);
        }

        public bool Carpti(Oyuncu o)
        {
            float dx = Math.Abs(x - o.x);
            float dy = Math.Abs(y - o.y);
            float toplamR = (r + o.r);

            if (dx < toplamR && dy < toplamR)
                return true;

            return false;
        }
    }
}
