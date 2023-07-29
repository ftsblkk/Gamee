using System;
using System.Collections.Generic;
//using System.Diagnostics;

using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SFMLResim
{

    public static class Resim
    {
        //***************************************************************
        private static float kodEn, kodBoy, winEn, winBoy;
        private static bool scaled;
        //***************************************************************

        private static RenderWindow window=null;
        private static List<Sprite> sprites;
        private static Font fnt;
        private static Text text;
        private static Clock clk;

        private static bool klavyeAsaği;

        static Resim()
        {
            scaled = false;
        }

        /// <summary>Bir çizim için geçen süre (saniye cinsinden)</summary>
        public static float FrameTime {get; private set; }

        /// <summary>Farenin pozisyonu</summary>
        public static Vector2i FarePos { get; private set; }

        /// <summary>Farenin sol düğmesi aşağı indiğinde true döndürür</summary>
        public static bool FareSolAsagi { get; private set; }

        /// <summary>Farenin sağ düğmesi aşağı indiğinde true döndürür</summary>
        public static bool FareSagAsagi { get; private set; }

        /// <summary>Farenin sol düğmesi bırakıldığında true döndürür</summary>
        public static bool FareSolYukari { get; private set; }

        /// <summary>Farenin sağ düğmesi bırakıldığında true döndürür</summary>
        public static bool FareSagYukari { get; private set; }


        private static void Kapat(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        /// <summary>Pencereyi kapatır</summary>
        public static void Kapat()
        {
            window.Close();
        }

        /// <summary>Yeni bir pencere oluşturur</summary>
        /// <param name="en">Pencerenin genişliği</param>
        /// <param name="boy">Pencerenin yüksekliği</param>
        /// <param name="baslık">Pencerenin başlığı</param>
        /// <param name="tamEkran">true ise tam ekran çalışır</param>
        public static void Yeni(uint en, uint boy, string baslık, bool tamEkran=false)
        {
            kodEn = en;
            kodBoy = boy;
            scaled = false;

            if (tamEkran)
                window = new RenderWindow(new VideoMode(en, boy), baslık, Styles.Fullscreen);
            else
                window = new RenderWindow(new VideoMode(en, boy), baslık, Styles.Close);

            clk = new Clock();
            FrameTime = 0;

            window.Closed += new EventHandler(Kapat);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseDown);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseUp);
            window.KeyPressed +=new EventHandler<KeyEventArgs>(OnKeyDown);

            fnt = new Font("font.ttf");
            text = new Text("", fnt, 10);
            text.Style = Text.Styles.Bold;

            sprites = new List<Sprite>();

            FarePos = new Vector2i();
            FareSolAsagi = false;
            FareSolYukari = false;
            FareSagAsagi = false;
            FareSagYukari = false;
            klavyeAsaği = false;

            window.SetKeyRepeatEnabled(false);
        }


        //***************************************************************
        /// <summary>Pencere boyutunu değiştirir</summary>
        /// <param name="en">Pencerenin genişliği</param>
        /// <param name="boy">Pencerenin yüksekliği</param>
        /// <param name="baslık">Pencerenin başlığı</param>
        /// <param name="tamEkran">true ise tam ekran çalışır</param>
        public static void YeniRes(uint en, uint boy, string baslık, bool tamEkran = false)
        {
            if (window == null)
            {
                Yeni(en, boy, baslık, tamEkran);
                return;
            }

            winEn = en;
            winBoy = boy;
            scaled = true;

            window.Close();
            if (tamEkran)
                window = new RenderWindow(new VideoMode(en, boy), baslık, Styles.Fullscreen);
            else
                window = new RenderWindow(new VideoMode(en, boy), baslık, Styles.Close);

            clk = new Clock();
            FrameTime = 0;

            window.Closed += new EventHandler(Kapat);
            window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(OnMouseDown);
            window.MouseButtonReleased += new EventHandler<MouseButtonEventArgs>(OnMouseUp);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyDown);
            window.SetKeyRepeatEnabled(false);

            FarePos = new Vector2i();
            FareSolAsagi = false;
            FareSolYukari = false;
            FareSagAsagi = false;
            FareSagYukari = false;
            klavyeAsaği = false;

        }

        private static float ToKodX(float n)
        {
            return n * kodEn / winEn;
        }

        private static float ToKodY(float n)
        {
            return n * kodBoy / winBoy;
        }

        private static float ToWinX(float n)
        {
            return n * winEn / kodEn;
        }

        private static float ToWinY(float n)
        {
            return n * winBoy / kodBoy;
        }


        //***************************************************************


        /// <summary>Yüklenen tüm resimleri siler</summary>
        public static void Temizle()
        {
            sprites.Clear();
        }

        /// <summary>Pencere açıksa true döndürür</summary>
        public static bool Acik()
        {
            return window.IsOpen;
        }

        /// <summary>Çizime başlar</summary>
        public static void Baslat()
        {
            window.DispatchEvents();

            FarePos = Mouse.GetPosition(window);

            if (scaled)
                FarePos = new Vector2i((int)ToKodX(FarePos.X), (int)ToKodY(FarePos.Y));

            FrameTime = clk.Restart().AsSeconds();
        }

        /// <summary>Çizimi bitirir</summary>
        public static void Bitir()
        {
            FareSolAsagi = false;
            FareSolYukari = false;
            FareSagAsagi = false;
            FareSagYukari = false;
            klavyeAsaği = false;

            window.Display();
        }

        /// <summary>Belirtilen klavye tuşuna basıldığında true döndürür</summary>
        /// <param name="tus">Kontrol edilmek istenen klavye tuşu</param>
        public static bool KlavyeAsagi(Klavye tus)
        {
            if (klavyeAsaği)
                return Keyboard.IsKeyPressed((Keyboard.Key)tus);
            return false;
        }

        /// <summary>Belirtilen klavye tuşu basılı ise true döndürür</summary>
        /// <param name="tus">Kontrol edilmek istenen klavye tuşu</param>
        public static bool KlavyeBasili(Klavye tus)
        {
            return Keyboard.IsKeyPressed((Keyboard.Key)tus);
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            klavyeAsaği = true;
            
        }

        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                FareSolAsagi = true;
            else if (e.Button == Mouse.Button.Right)
                FareSagAsagi = true;
        }

        private static void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                FareSolYukari = true;
            else if (e.Button == Mouse.Button.Right)
                FareSagYukari = true;
        }

        /// <summary>Farenin sol tuşu basılı ise true döndürür</summary>
        public static bool FareSolBasili
        {
            get
            {
                return Mouse.IsButtonPressed(Mouse.Button.Left);
            }
        }

        /// <summary>Farenin sağ tuşu basılı ise true döndürür</summary>
        public static bool FareSagBasili
        {
            get
            {
                return Mouse.IsButtonPressed(Mouse.Button.Right);
            }
        }

        /// <summary>Bir saniyede yapılacak çizim sayısını sınırlar</summary>
        /// <param name="n">Bir saniyede en fazla kaç çizim yapılacağını belirtir</param>
        public static void SetFPSLimit(uint n)
        {
            window.SetFramerateLimit(n);
        }

        /// <summary>Yeni bir resim yükler. Yüklenen resmi belirten bir sayı döndürür</summary>
        /// <param name="dosyaAdi">Yüklenecek resim adı</param>
        public static int Ekle(string dosyaAdi)
        {
            Texture texture = new Texture(dosyaAdi);
            texture.Smooth = true;

            Sprite sprite = new Sprite(texture);
            sprites.Add(sprite);

            return sprites.Count - 1;
        }

        /// <summary>Belirtilen resmi çizer</summary>
        /// <param name="n">Resim no</param>
        /// <param name="x">Resmin soldan uzaklığı</param>
        /// <param name="y">Resmin yukarıdan uzaklığı</param>
        /// <param name="en">Resmin genişliği</param>
        /// <param name="boy">Resmin yüksekliği</param>
        public static void Ciz(int n, float x, float y, float en, float boy)
        {
            if (n < sprites.Count)
            {

                if (scaled)
                {
                    x = ToWinX(x);
                    y = ToWinY(y);
                    en = ToWinX(en);
                    boy = ToWinY(boy);
                }


                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                sprites[n].Origin = new Vector2f(0, 0);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x, y);
                sprites[n].Rotation = 0;
                sprites[n].Color = new Color(255, 255, 255, 255);

                window.Draw(sprites[n]);
            }
        }

        /// <summary>Belirtilen resmi döndürerek çizer</summary>
        /// <param name="n">Resim no</param>
        /// <param name="x">Resmin soldan uzaklığı</param>
        /// <param name="y">Resmin yukarıdan uzaklığı</param>
        /// <param name="en">Resmin genişliği</param>
        /// <param name="boy">Resmin yüksekliği</param>
        /// <param name="a">Resmin açısı</param>
        public static void Ciz(int n, float x, float y, float en, float boy, float a)
        {
            if (n < sprites.Count)
            {

                if (scaled)
                {
                    x = ToWinX(x);
                    y = ToWinY(y);
                    en = ToWinX(en);
                    boy = ToWinY(boy);
                }


                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                sprites[n].Origin = new Vector2f(tx / 2, ty / 2);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x + en / 2, y + boy / 2);
                sprites[n].Rotation = a;
                sprites[n].Color = new Color(255, 255, 255, 255);

                window.Draw(sprites[n]);
            }
        }

        /// <summary>Belirtilen resmi belirtilen renkte döndürerek çizer</summary>
        /// <param name="n">Resim no</param>
        /// <param name="x">Resmin soldan uzaklığı</param>
        /// <param name="y">Resmin yukarıdan uzaklığı</param>
        /// <param name="en">Resmin genişliği</param>
        /// <param name="boy">Resmin yüksekliği</param>
        /// <param name="a">Resmin açısı</param>
        /// <param name="alpha">Resmin şeffaflığı. 0:Şeffaf, 255:Opak</param>
        /// <param name="r">Kırmızı [0...255]</param>
        /// <param name="g">Yeşil [0...255]</param>
        /// <param name="b">Mavi [0...255]</param>
        public static void Ciz(int n, float x, float y, float en, float boy, float a, int alpha, int r = 255, int g = 255, int b = 255)
        {
            if (n < sprites.Count)
            {

                if (scaled)
                {
                    x = ToWinX(x);
                    y = ToWinY(y);
                    en = ToWinX(en);
                    boy = ToWinY(boy);
                }


                FloatRect rect = sprites[n].GetLocalBounds();
                float tx = rect.Width;
                float ty = rect.Height;

                sprites[n].Origin = new Vector2f(tx / 2, ty / 2);
                sprites[n].Scale = new Vector2f(en / tx, boy / ty);
                sprites[n].Position = new Vector2f(x + en / 2, y + boy / 2);
                sprites[n].Rotation = a;
                sprites[n].Color = new Color((byte)r, (byte)g, (byte)b, (byte)alpha);
                window.Draw(sprites[n]);
            }
        }

        /// <summary>Yazı yazar</summary>
        /// <param name="s">Yazılacak yazı</param>
        /// <param name="x">Yazının soldan uzaklığı</param>
        /// <param name="y">Yazının yukarıdan uzaklığı</param>
        /// <param name="boy">Yazının boyu</param>
        public static void YaziYaz(string s, float x, float y, float boy)
        {

            if (scaled)
            {
                x = ToWinX(x);
                y = ToWinY(y);
                boy = ToWinY(boy);
            }


            text.DisplayedString = s;
            text.CharacterSize = (uint)boy;

            text.Color = new Color(0, 0, 0, 255);
            text.Position = new Vector2f(x + boy / 15, y + boy / 15);
            window.Draw(text);

            text.Color = new Color(255, 255, 255, 255);
            text.Position = new Vector2f(x, y);
            window.Draw(text);
        }

        /// <summary>Belirtilen renkte yazı yazar</summary>
        /// <param name="s">Yazılacak yazı</param>
        /// <param name="x">Yazının soldan uzaklığı</param>
        /// <param name="y">Yazının yukarıdan uzaklığı</param>
        /// <param name="boy">Yazının boyu</param>
        /// <param name="r">Kırmızı [0...255]</param>
        /// <param name="g">Yeşil [0...255]</param>
        /// <param name="b">Mavi [0...255]</param>
        /// <param name="alpha">Yazının şeffaflığı. 0:Şeffaf, 255:Opak</param>
        public static void YaziYaz(string s, float x, float y, float boy, int r, int g, int b, int alpha = 255)
        {

            if (scaled)
            {
                x = ToWinX(x);
                y = ToWinY(y);
                boy = ToWinY(boy);
            }


            text.Position = new Vector2f(x, y);
            text.Color = new Color((byte)r, (byte)g, (byte)b, (byte)alpha);
            text.DisplayedString = s;
            text.CharacterSize = (uint)boy;

            window.Draw(text);
        }

        /// <summary>Doğru çizer</summary>
        /// <param name="n">Doğru çiziminde kullanılacak resim</param>
        /// <param name="x1">Başlangıç noktasının soldan uzaklığı</param>
        /// <param name="y1">Başlangıç noktasının yukarıdan uzaklığı</param>
        /// <param name="x2">Bitiş noktasının soldan uzaklığı</param>
        /// <param name="y2">Bitiş noktasının yukarıdan uzaklığı</param>
        /// <param name="k">Doğrunun kalınlığı</param>
        public static void DogruCiz(int n, float x1, float y1, float x2, float y2, float k)
        {
            if (n < sprites.Count)
            {

                if (scaled)
                {
                    x1 = ToWinX(x1);
                    y1 = ToWinY(y1);
                    x2 = ToWinX(x2);
                    y2 = ToWinY(y2);

//*******************************************************************************
                    k = ToWinX(k);
//*******************************************************************************
                }


                FloatRect r = sprites[n].GetLocalBounds();
                float tx = r.Width;
                float ty = r.Height;

                float a = (float)Math.Atan2(y2 - y1, x2 - x1);
                a = (180 * a) / (float)Math.PI;

                float b = (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
                sprites[n].Origin = new Vector2f(0, ty / 2);
                sprites[n].Scale = new Vector2f(b / tx, k / ty);
                sprites[n].Position = new Vector2f(x1, y1);
                sprites[n].Rotation = a;
                
                window.Draw(sprites[n]);
            }
        }

        // *******************************************************************************

        /// <summary>Yazının enini bulur</summary>
        /// <param name="s">Eni bulunacak yazı</param>
        /// <param name="h">Eni bulunacak yazının yüksekliği</param>
        public static float YaziEni(string s, float h)
        {
            text.DisplayedString = s;
            text.CharacterSize = (uint)h;

            FloatRect r = text.GetGlobalBounds();

            return r.Width;
        }

        /// <summary>Belirtilen nokta yazı içinde ise true döndürür</summary>
        /// <param name="s">Yazı</param>
        /// <param name="x">Yazının soldan uzaklığı</param>
        /// <param name="y">Yazının yukarıdan uzaklığı</param>
        /// <param name="h">Yazının yüksekliği</param>
        /// <param name="px">Noktanın soldan uzaklığı</param>
        /// <param name="py">Noktanın yukarıdan uzaklığı</param>
        public static bool YaziIcinde(string s, float x, float y, float h, float px, float py)
        {
            text.DisplayedString = s;
            text.CharacterSize = (uint)h;
            text.Position = new Vector2f(x, y);

            FloatRect r = text.GetGlobalBounds();
            return r.Contains(px, py);
        }

        // *******************************************************************************
        public enum Klavye
        {
            A = Keyboard.Key.A,
            B = Keyboard.Key.B,
            C = Keyboard.Key.C,
            D = Keyboard.Key.D,
            E = Keyboard.Key.E,
            F = Keyboard.Key.F,
            G = Keyboard.Key.G,
            H = Keyboard.Key.H,
            I = Keyboard.Key.I,
            J = Keyboard.Key.J,
            K = Keyboard.Key.K,
            L = Keyboard.Key.L,
            M = Keyboard.Key.M,
            N = Keyboard.Key.N,
            O = Keyboard.Key.O,
            P = Keyboard.Key.P,
            Q = Keyboard.Key.Q,
            R = Keyboard.Key.R,
            S = Keyboard.Key.S,
            T = Keyboard.Key.T,
            U = Keyboard.Key.U,
            V = Keyboard.Key.V,
            W = Keyboard.Key.W,
            X = Keyboard.Key.X,
            Y = Keyboard.Key.Y,
            Z = Keyboard.Key.Z,
            Num0 = Keyboard.Key.Num0,
            Num1 = Keyboard.Key.Num1,
            Num2 = Keyboard.Key.Num2,
            Num3 = Keyboard.Key.Num3,
            Num4 = Keyboard.Key.Num4,
            Num5 = Keyboard.Key.Num5,
            Num6 = Keyboard.Key.Num6,
            Num7 = Keyboard.Key.Num7,
            Num8 = Keyboard.Key.Num8,
            Num9 = Keyboard.Key.Num9,
            Escape = Keyboard.Key.Escape,
            LControl = Keyboard.Key.LControl,
            LShift = Keyboard.Key.LShift,
            LAlt = Keyboard.Key.LAlt,
            LSystem = Keyboard.Key.LSystem,
            RControl = Keyboard.Key.RControl,
            RShift = Keyboard.Key.RShift,
            RAlt = Keyboard.Key.RAlt,
            RSystem = Keyboard.Key.RSystem,
            Menu = Keyboard.Key.Menu,
            LBracket = Keyboard.Key.LBracket,
            RBracket = Keyboard.Key.RBracket,
            SemiColon = Keyboard.Key.SemiColon,
            Comma = Keyboard.Key.Comma,
            Period = Keyboard.Key.Period,
            Quote = Keyboard.Key.Quote,
            Slash = Keyboard.Key.Slash,
            BackSlash = Keyboard.Key.BackSlash,
            Tilde = Keyboard.Key.Tilde,
            Equal = Keyboard.Key.Equal,
            Dash = Keyboard.Key.Dash,
            Space = Keyboard.Key.Space,
            Return = Keyboard.Key.Return,
            BackSpace = Keyboard.Key.BackSpace,
            Tab = Keyboard.Key.Tab,
            PageUp = Keyboard.Key.PageUp,
            PageDown = Keyboard.Key.PageDown,
            End = Keyboard.Key.End,
            Home = Keyboard.Key.Home,
            Insert = Keyboard.Key.Insert,
            Delete = Keyboard.Key.Delete,
            Add = Keyboard.Key.Add,
            Subtract = Keyboard.Key.Subtract,
            Multiply = Keyboard.Key.Multiply,
            Divide = Keyboard.Key.Divide,
            Left = Keyboard.Key.Left,
            Right = Keyboard.Key.Right,
            Up = Keyboard.Key.Up,
            Down = Keyboard.Key.Down,
            Numpad0 = Keyboard.Key.Numpad0,
            Numpad1 = Keyboard.Key.Numpad1,
            Numpad2 = Keyboard.Key.Numpad2,
            Numpad3 = Keyboard.Key.Numpad3,
            Numpad4 = Keyboard.Key.Numpad4,
            Numpad5 = Keyboard.Key.Numpad5,
            Numpad6 = Keyboard.Key.Numpad6,
            Numpad7 = Keyboard.Key.Numpad7,
            Numpad8 = Keyboard.Key.Numpad8,
            Numpad9 = Keyboard.Key.Numpad9,
            F1 = Keyboard.Key.F1,
            F2 = Keyboard.Key.F2,
            F3 = Keyboard.Key.F3,
            F4 = Keyboard.Key.F4,
            F5 = Keyboard.Key.F5,
            F6 = Keyboard.Key.F6,
            F7 = Keyboard.Key.F7,
            F8 = Keyboard.Key.F8,
            F9 = Keyboard.Key.F9,
            F10 = Keyboard.Key.F10,
            F11 = Keyboard.Key.F11,
            F12 = Keyboard.Key.F12,
            F13 = Keyboard.Key.F13,
            F14 = Keyboard.Key.F14,
            F15 = Keyboard.Key.F15,
            Pause = Keyboard.Key.Pause
        };

    }

}
