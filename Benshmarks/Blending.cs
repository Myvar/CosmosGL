using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Benshmarks
{
    public class Blending
    {
        public static void TimeTest()
        {
            new Blending().Test();
        }

        public void Test()
        {
            var pixels = 100_000;

            Program.TimeTest("Blending Method A", pixels, () => Blend(10, 10, 50));
            Program.TimeTest("Blending Method B", pixels, () => AlphaBlend(10, 10, 10, 10, 10, 10, 10, 10));
            Program.TimeTest("Blending Method C", pixels, () => blendPreMulAlpha(10, 10, 50));
            Program.TimeTest("Blending Method D", pixels, () => BlendColors(10, 10, 50));
        }

        public static Color AlphaBlend(byte a1, byte r1, byte b1, byte g1, byte a2, byte r2, byte b2, byte g2)
        {
            var a = (byte) ((a2 / 255F) * a2 + (1F - a2 / 255F) * a1);
            var r = (byte) ((a2 / 255F) * r2 + (1F - a2 / 255F) * r1);
            var g = (byte) ((a2 / 255F) * g2 + (1F - a2 / 255F) * g1);
            var b = (byte) ((a2 / 255F) * b2 + (1F - a2 / 255F) * b1);
            return Color.FromArgb(r, g, b, a);
        }

        int BlendColors(float alpha, int src, int dest)
        {
            int r1 = (src >> 16) & 0xFF;
            int g1 = (src >> 8) & 0xFF;
            int b1 = src & 0xFF;
            int r2 = (dest >> 16) & 0xFF;
            int g2 = (dest >> 8) & 0xFF;
            int b2 = dest & 0xFF;
            int ar, ag, ab;

            ar = (int) (alpha * (r1 - r2) + r2);
            ag = (int) (alpha * (g1 - g2) + g2);
            ab = (int) (alpha * (b1 - b2) + b2);

            return ab | (ag << 8) | (ar << 16);
        }

        int blendPreMulAlpha(int colora, int colorb, int alpha)
        {
            int rb = (colora & 0xFF00FF) + (alpha * (colorb & 0xFF00FF)) >> 8;
            int g = (colora & 0x00FF00) + (alpha * (colorb & 0x00FF00)) >> 8;
            return (rb & 0xFF00FF) + (g & 0x00FF00);
        }


        private uint Blend(uint color1, uint color2, byte alpha)
        {
            uint rb = color1 & 0xff00ff;
            uint g = color1 & 0x00ff00;
            rb += ((color2 & 0xff00ff) - rb) * alpha >> 8;
            g += ((color2 & 0x00ff00) - g) * alpha >> 8;
            return (rb & 0xff00ff) | (g & 0xff00);
        }
    }
}