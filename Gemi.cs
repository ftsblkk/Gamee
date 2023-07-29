using System;
using System.Collections.Generic;
using SFMLResim;

namespace SAOyun2023
{
    class Gemi
    {
        static private List<int> resimler;
        private int tip;

        public float x { get; private set; }
        public float y { get; private set; }
        public float r { get; private set; }

        private float vx, vy, a;
        public int can { get; private set; }

        static Gemi()
        {
            resimler = new List<int>();
            for (int i=0; i<=9; i++)
                resimler.Add( Resim.Ekle("res\\Gemi\\" + i + ".png") );
        }

        static Random rnd = new Random();
        public Gemi()
        {
            tip = rnd.Next(0, 10);
            x = rnd.Next(100, 700);
            y = rnd.Next(-300, -100);
            a = 0;

            if (tip == 0)
            {
                r = 35;
                vx = rnd.Next(-100, 100);
                vy = rnd.Next(50, 100);
                can = 4;

            }
            else if (tip < 4)
            {
                r = 30;
                vx = rnd.Next(-200, 200);
                vy = rnd.Next(50, 200);
                can = 3;
            }
            else if (tip < 7)
            {
                r = 25;
                vx = 0;
                vy = rnd.Next(100, 150);
                can = 2;
            }
            else
            {
                r = 20;
                vx = 0;
                vy = rnd.Next(300, 400);
                can = 1;
            }
        }

        public void Hesapla(float t)
        {
            a += 90 * t;

            x += vx * t;

            if (x < r)
            {
                x = r;
                vx *= -1;
            }

            if (x > 800 - r)
            {
                x = 800 - r;
                vx *= -1;
            }

            y += vy * t;

            if (y > 600 + r)
                y = rnd.Next(-300, -100);
        }

        public void Ciz()
        {
            if (tip == 0)
                Resim.Ciz(resimler[tip], x - r, y - r, 2 * r, 2 * r, a);
            else
                Resim.Ciz(resimler[tip], x - r, y - r, 2 * r, 2 * r);
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

        // **********************************************************
        public void CanAzalt(int n=1)
        {   
            can -= n;
        }
        // **********************************************************

    }
}
