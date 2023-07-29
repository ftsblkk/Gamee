using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAOyun2023
{
    static class Mermiler
    {
        static List<Mermi> mermiler;

        static Mermiler()
        {
            mermiler = new List<Mermi>();
        }

        static public void Ekle(float x, float y, int tip = 0)
        {
            mermiler.Add( new Mermi(x, y, tip) );
        }

        static public void Hesapla(float t)
        {
            foreach (Mermi m in mermiler)
                m.Hesapla(t);

            for (int i = mermiler.Count - 1; i >= 0; i--)
                if (mermiler[i].y < 0)
                    mermiler.RemoveAt(i);
        }

        static public void Ciz()
        {
            foreach (Mermi m in mermiler)
                m.Ciz();
        }

        static public bool Vurdu(Gemi g)
        {
            foreach (Mermi m in mermiler)
                if (m.Vurdu(g) == true)
                    return true;
            return false;
        }

        static public bool Vurdu(Asteroid a)
        {
            foreach (Mermi m in mermiler)
                if (m.Vurdu(a) == true)
                    return true;
            return false;
        }
    }
}
