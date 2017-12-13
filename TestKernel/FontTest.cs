using CosmosGL.System.Fonts;
using CosmosGL.System.Graphics;
using CosmosGL.System.TrueType;

namespace TestKernel
{
    public static class FontTest
    {
        public static void Draw(Graphics gr)
        {
            var font = new TrueTypeFont(Karla.KarlaRegularTtf);

            float scale = 32f / font.UnitsPerEm;

            var y = 5;
            var x = 5;

            int c = 0;

            for (int i = 6; i < font.GlyphCount(); i++)
            {
                var g = font.ReadGlyph(i);

                font.DrawGlyph(gr, i, scale, -scale, x,  (int) (y + ((font.YMax - font.YMin) * scale)) );
                x += (int)(scale * g.XMax) + 5;
                gr.Flush();
                if (c > 30)
                {
                    y += (int)(scale * g.YMax) + 40;
                    c = 0;
                    x = 5;
                }
                c++;
            }
        }
    }
}