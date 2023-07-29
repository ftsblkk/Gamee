using System;
using System.Collections.Generic;
using SFMLResim;

namespace SAOyun2023
{
    class Asteroid
    {
        static List<int> resimler;

        public float x { get; private set; }
        public float y { get; private set; }
        public float r { get; private set; }
        public int can { get; private set; }

        float vx, vy, animNo;

        static Random rnd = new Random();

        static Asteroid()
        {
            resimler = new List<int>();

            for (int i=0; i<=29; i++)
                resimler.Add(Resim.Ekle("res\\Asteroid\\" + i + ".png") );
        }

        public Asteroid(int can, float x, float y, float vx, float vy, float r)
        {
            this.can = can;
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.r = r;
            animNo = rnd.Next(30);
        }

        public Asteroid()
        {
            can = 5;
            vx = 0;
            vy = rnd.Next(20, 60);
            r = 40;
            animNo = rnd.Next(30);

            x = rnd.Next(100, 700);
            y = rnd.Next(-300, -100);
        }

        public void Hesapla(float t)
        {
            animNo += 15*t;
            if ((int)animNo > 29)
                animNo = 0;

            x += vx * t;
            y += vy * t;
        }

        public void Ciz()
        {
            if ((int)animNo < resimler.Count)
                Resim.Ciz(resimler[(int)animNo], x - r, y - r, 2 * r, 2 * r); 
        }

        public bool Carpti(Oyuncu o)
        {
            float dx = Math.Abs(x - o.x);
            float dy = Math.Abs(y - o.y);
            float toplamR = (r + o.r) * 0.7f;

            if (dx < toplamR && dy < toplamR)
                return true;

            return false;
        }

        public void CanAzalt()
        {
            can--;
        }



    }
}
