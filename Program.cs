using System;
using SFMLResim;
using SFMLSes;

namespace SAOyun2023
{
    static class Program
    {
        static void Main()
        {
            //System.Windows.Forms.Cursor.Hide();

            Resim.Yeni(800, 600, "SAOyun2023");
            //Resim.SetFPSLimit(60);

            int bgMusik = Ses.MusikEkle("res\\Ses\\bg.ogg");
            Ses.MusikCal(bgMusik);

            int seviye = 50;
            Ses.MusikSeviye(bgMusik, seviye);

            //Ses.Sus();
            
            Oyun.YeniOyun();

            while (Resim.Acik())
            {
                Resim.Baslat();

                Oyun.FareKontrol();
                Oyun.Hesapla(Resim.FrameTime);
                Oyun.Ciz();



                if (Resim.KlavyeAsagi(Resim.Klavye.Escape))
                    Resim.Kapat();

                if (Resim.KlavyeAsagi(Resim.Klavye.Num1))
                    Resim.YeniRes(800, 600, "SAOyun 800x600");

                if (Resim.KlavyeAsagi(Resim.Klavye.Num2))
                    Resim.YeniRes(1200, 900, "SAOyun 1200x900");

                if (Resim.KlavyeAsagi(Resim.Klavye.Num3))
                    Resim.YeniRes(1024, 768, "SAOyun 1024x768 FS", true);

                if (Resim.KlavyeAsagi(Resim.Klavye.P))
                {
                    seviye += 10;
                    if (seviye > 100)
                        seviye = 100;
                    Ses.MusikSeviye(bgMusik, seviye);
                }

                if (Resim.KlavyeAsagi(Resim.Klavye.O))
                {
                    seviye -= 10;
                    if (seviye < 0)
                        seviye = 0;
                    Ses.MusikSeviye(bgMusik, seviye);
                }

                if (Resim.KlavyeAsagi(Resim.Klavye.Num0))
                    Ses.Sus(true);
                if (Resim.KlavyeAsagi(Resim.Klavye.Num9))
                    Ses.Sus(false);


                Resim.Bitir();
            }
        }
    }
}
