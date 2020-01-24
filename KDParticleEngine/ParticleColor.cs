using System;

namespace KDParticleEngine
{
    public class ParticleColor
    {
        #region Constructors
        public ParticleColor(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
        #endregion


        #region Public Methods
        public static ParticleColor FromArgb(byte a, byte r, byte g, byte b) => new ParticleColor(a, r, g, b);
        #endregion


        #region Props
        public byte R { get; set; }

        public byte G { get; set; }

        public byte B { get; set; }

        public byte A { get; set; }

        public static ParticleColor AliceBlue => new ParticleColor(255, 240, 248, 255);

        public static ParticleColor AntiqueWhite => new ParticleColor(255, 250, 235, 215);

        public static ParticleColor Aqua => new ParticleColor(255, 0, 255, 255);

        public static ParticleColor Aquamarine => new ParticleColor(255, 127, 255, 212);

        public static ParticleColor Azure => new ParticleColor(255, 240, 255, 255);

        public static ParticleColor Beige => new ParticleColor(255, 245, 245, 220);

        public static ParticleColor Bisque => new ParticleColor(255, 255, 228, 196);

        public static ParticleColor Black => new ParticleColor(255, 0, 0, 0);

        public static ParticleColor BlanchedAlmond => new ParticleColor(255, 255, 235, 205);

        public static ParticleColor Blue => new ParticleColor(255, 0, 0, 255);

        public static ParticleColor BlueViolet => new ParticleColor(255, 138, 43, 226);

        public static ParticleColor Brown => new ParticleColor(255, 165, 42, 42);

        public static ParticleColor BurlyWood => new ParticleColor(255, 222, 184, 135);

        public static ParticleColor CadetBlue => new ParticleColor(255, 95, 158, 160);

        public static ParticleColor Chartreuse => new ParticleColor(255, 127, 255, 0);

        public static ParticleColor Chocolate => new ParticleColor(255, 210, 105, 30);

        public static ParticleColor Coral => new ParticleColor(255, 255, 127, 80);

        public static ParticleColor CornflowerBlue => new ParticleColor(255, 100, 149, 237);

        public static ParticleColor Cornsilk => new ParticleColor(255, 255, 248, 220);

        public static ParticleColor Crimson => new ParticleColor(255, 220, 20, 60);

        public static ParticleColor Cyan => new ParticleColor(255, 0, 255, 255);

        public static ParticleColor DarkBlue => new ParticleColor(255, 0, 0, 139);

        public static ParticleColor DarkCyan => new ParticleColor(255, 0, 139, 139);

        public static ParticleColor DarkGoldenrod => new ParticleColor(255, 184, 134, 11);

        public static ParticleColor DarkGray => new ParticleColor(255, 169, 169, 169);

        public static ParticleColor DarkGreen => new ParticleColor(255, 0, 100, 0);

        public static ParticleColor DarkKhaki => new ParticleColor(255, 189, 183, 107);

        public static ParticleColor DarkMagenta => new ParticleColor(255, 139, 0, 139);

        public static ParticleColor DarkOliveGreen => new ParticleColor(255, 85, 107, 47);

        public static ParticleColor DarkOrange => new ParticleColor(255, 255, 140, 0);

        public static ParticleColor DarkOrchid => new ParticleColor(255, 153, 50, 204);

        public static ParticleColor DarkRed => new ParticleColor(255, 139, 0, 0);

        public static ParticleColor DarkSalmon => new ParticleColor(255, 233, 150, 122);

        public static ParticleColor DarkSeaGreen => new ParticleColor(255, 143, 188, 139);

        public static ParticleColor DarkSlateBlue => new ParticleColor(255, 72, 61, 139);

        public static ParticleColor DarkSlateGray => new ParticleColor(255, 47, 79, 79);

        public static ParticleColor DarkTurquoise => new ParticleColor(255, 0, 206, 209);

        public static ParticleColor DarkViolet => new ParticleColor(255, 148, 0, 211);

        public static ParticleColor DeepPink => new ParticleColor(255, 255, 20, 147);

        public static ParticleColor DeepSkyBlue => new ParticleColor(255, 0, 191, 255);

        public static ParticleColor DimGray => new ParticleColor(255, 105, 105, 105);

        public static ParticleColor DodgerBlue => new ParticleColor(255, 30, 144, 255);

        public static ParticleColor Firebrick => new ParticleColor(255, 178, 34, 34);

        public static ParticleColor FloralWhite => new ParticleColor(255, 255, 250, 240);

        public static ParticleColor ForestGreen => new ParticleColor(255, 34, 139, 34);

        public static ParticleColor Fuchsia => new ParticleColor(255, 255, 0, 255);

        public static ParticleColor Gainsboro => new ParticleColor(255, 220, 220, 220);

        public static ParticleColor GhostWhite => new ParticleColor(255, 248, 248, 255);

        public static ParticleColor Gold => new ParticleColor(255, 255, 215, 0);

        public static ParticleColor Goldenrod => new ParticleColor(255, 218, 165, 32);

        public static ParticleColor Gray => new ParticleColor(255, 128, 128, 128);

        public static ParticleColor Green => new ParticleColor(255, 0, 128, 0);

        public static ParticleColor GreenYellow => new ParticleColor(255, 173, 255, 47);

        public static ParticleColor Honeydew => new ParticleColor(255, 240, 255, 240);

        public static ParticleColor HotPink => new ParticleColor(255, 255, 105, 180);

        public static ParticleColor IndianRed => new ParticleColor(255, 205, 92, 92);

        public static ParticleColor Indigo => new ParticleColor(255, 75, 0, 130);

        public static ParticleColor Ivory => new ParticleColor(255, 255, 255, 240);

        public static ParticleColor Khaki => new ParticleColor(255, 240, 230, 140);

        public static ParticleColor Lavender => new ParticleColor(255, 230, 230, 250);

        public static ParticleColor LavenderBlush => new ParticleColor(255, 255, 240, 245);

        public static ParticleColor LawnGreen => new ParticleColor(255, 124, 252, 0);

        public static ParticleColor LemonChiffon => new ParticleColor(255, 255, 250, 205);

        public static ParticleColor LightBlue => new ParticleColor(255, 173, 216, 230);

        public static ParticleColor LightCoral => new ParticleColor(255, 240, 128, 128);

        public static ParticleColor LightCyan => new ParticleColor(255, 224, 255, 255);

        public static ParticleColor LightGoldenrodYellow => new ParticleColor(255, 250, 250, 210);

        public static ParticleColor LightGray => new ParticleColor(255, 211, 211, 211);

        public static ParticleColor LightGreen => new ParticleColor(255, 144, 238, 144);

        public static ParticleColor LightPink => new ParticleColor(255, 255, 182, 193);

        public static ParticleColor LightSalmon => new ParticleColor(255, 255, 160, 122);

        public static ParticleColor LightSeaGreen => new ParticleColor(255, 32, 178, 170);

        public static ParticleColor LightSkyBlue => new ParticleColor(255, 135, 206, 250);

        public static ParticleColor LightSlateGray => new ParticleColor(255, 119, 136, 153);

        public static ParticleColor LightSteelBlue => new ParticleColor(255, 176, 196, 222);

        public static ParticleColor LightYellow => new ParticleColor(255, 255, 255, 224);

        public static ParticleColor Lime => new ParticleColor(255, 0, 255, 0);

        public static ParticleColor LimeGreen => new ParticleColor(255, 50, 205, 50);

        public static ParticleColor Linen => new ParticleColor(255, 250, 240, 230);

        public static ParticleColor Magenta => new ParticleColor(255, 255, 0, 255);

        public static ParticleColor Maroon => new ParticleColor(255, 128, 0, 0);

        public static ParticleColor MediumAquamarine => new ParticleColor(255, 102, 205, 170);

        public static ParticleColor MediumBlue => new ParticleColor(255, 0, 0, 205);

        public static ParticleColor MediumOrchid => new ParticleColor(255, 186, 85, 211);

        public static ParticleColor MediumPurple => new ParticleColor(255, 147, 112, 219);

        public static ParticleColor MediumSeaGreen => new ParticleColor(255, 60, 179, 113);

        public static ParticleColor MediumSlateBlue => new ParticleColor(255, 123, 104, 238);

        public static ParticleColor MediumSpringGreen => new ParticleColor(255, 0, 250, 154);

        public static ParticleColor MediumTurquoise => new ParticleColor(255, 72, 209, 204);

        public static ParticleColor MediumVioletRed => new ParticleColor(255, 199, 21, 133);

        public static ParticleColor MidnightBlue => new ParticleColor(255, 25, 25, 112);

        public static ParticleColor MintCream => new ParticleColor(255, 245, 255, 250);

        public static ParticleColor MistyRose => new ParticleColor(255, 255, 228, 225);

        public static ParticleColor Moccasin => new ParticleColor(255, 255, 228, 181);

        public static ParticleColor NavajoWhite => new ParticleColor(255, 255, 222, 173);

        public static ParticleColor Navy => new ParticleColor(255, 0, 0, 128);

        public static ParticleColor OldLace => new ParticleColor(255, 253, 245, 230);

        public static ParticleColor Olive => new ParticleColor(255, 128, 128, 0);

        public static ParticleColor OliveDrab => new ParticleColor(255, 107, 142, 35);

        public static ParticleColor Orange => new ParticleColor(255, 255, 165, 0);

        public static ParticleColor OrangeRed => new ParticleColor(255, 255, 69, 0);

        public static ParticleColor Orchid => new ParticleColor(255, 218, 112, 214);

        public static ParticleColor PaleGoldenrod => new ParticleColor(255, 238, 232, 170);

        public static ParticleColor PaleGreen => new ParticleColor(255, 152, 251, 152);

        public static ParticleColor PaleTurquoise => new ParticleColor(255, 175, 238, 238);

        public static ParticleColor PaleVioletRed => new ParticleColor(255, 219, 112, 147);

        public static ParticleColor PapayaWhip => new ParticleColor(255, 255, 239, 213);

        public static ParticleColor PeachPuff => new ParticleColor(255, 255, 218, 185);

        public static ParticleColor Peru => new ParticleColor(255, 205, 133, 63);

        public static ParticleColor Pink => new ParticleColor(255, 255, 192, 203);

        public static ParticleColor Plum => new ParticleColor(255, 221, 160, 221);

        public static ParticleColor PowderBlue => new ParticleColor(255, 176, 224, 230);

        public static ParticleColor Purple => new ParticleColor(255, 128, 0, 128);

        public static ParticleColor Red => new ParticleColor(255, 255, 0, 0);

        public static ParticleColor RosyBrown => new ParticleColor(255, 188, 143, 143);

        public static ParticleColor RoyalBlue => new ParticleColor(255, 65, 105, 225);

        public static ParticleColor SaddleBrown => new ParticleColor(255, 139, 69, 19);

        public static ParticleColor Salmon => new ParticleColor(255, 250, 128, 114);

        public static ParticleColor SandyBrown => new ParticleColor(255, 244, 164, 96);

        public static ParticleColor SeaGreen => new ParticleColor(255, 46, 139, 87);

        public static ParticleColor SeaShell => new ParticleColor(255, 255, 245, 238);

        public static ParticleColor Sienna => new ParticleColor(255, 160, 82, 45);

        public static ParticleColor Silver => new ParticleColor(255, 192, 192, 192);

        public static ParticleColor SkyBlue => new ParticleColor(255, 135, 206, 235);

        public static ParticleColor SlateBlue => new ParticleColor(255, 106, 90, 205);

        public static ParticleColor SlateGray => new ParticleColor(255, 112, 128, 144);

        public static ParticleColor Snow => new ParticleColor(255, 255, 250, 250);

        public static ParticleColor SpringGreen => new ParticleColor(255, 0, 255, 127);

        public static ParticleColor SteelBlue => new ParticleColor(255, 70, 130, 180);

        public static ParticleColor Tan => new ParticleColor(255, 210, 180, 140);

        public static ParticleColor Teal => new ParticleColor(255, 0, 128, 128);

        public static ParticleColor Thistle => new ParticleColor(255, 216, 191, 216);

        public static ParticleColor Tomato => new ParticleColor(255, 255, 99, 71);

        public static ParticleColor Turquoise => new ParticleColor(255, 64, 224, 208);

        public static ParticleColor Violet => new ParticleColor(255, 238, 130, 238);

        public static ParticleColor Wheat => new ParticleColor(255, 245, 222, 179);

        public static ParticleColor White => new ParticleColor(255, 255, 255, 255);

        public static ParticleColor WhiteSmoke => new ParticleColor(255, 245, 245, 245);

        public static ParticleColor Yellow => new ParticleColor(255, 255, 255, 0);

        public static ParticleColor YellowGreen => new ParticleColor(255, 154, 205, 50);
        #endregion


        #region Public Methods
        public float GetBrightness()
        {
            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

            return (max + min) / (byte.MaxValue * 2f);
        }


        public float GetHue()
        {
            if (R == G && G == B)
                return 0f;

            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

            float delta = max - min;
            float hue;

            if (R == max)
                hue = (G - B) / delta;
            else if (G == max)
                hue = (B - R) / delta + 2f;
            else
                hue = (R - G) / delta + 4f;

            hue *= 60f;
            if (hue < 0f)
                hue += 360f;

            return hue;
        }


        public float GetSaturation()
        {
            if (R == G && G == B)
                return 0f;

            int min = Math.Min(Math.Min(R, G), B);
            int max = Math.Max(Math.Max(R, G), B);

            int div = max + min;
            if (div > byte.MaxValue)
                div = byte.MaxValue * 2 - max - min;

            return (max - min) / (float)div;
        }
        #endregion
    }
}
