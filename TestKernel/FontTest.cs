using CosmosGL.System;
using CosmosGL.System.Fonts;
using CosmosGL.System.Graphics;

namespace TestKernel
{
    public static class FontTest
    {
        public static void Draw(ref Graphics gr)
        {
            var f = new CGLF(Karla.Karla_cglf);

            var g = f.Glyphs[0];
            
            var scale = 22f / f.UnitsPerEm;
            gr.TranslateTransform(10, 10 + (int)(((f.YMax - f.YMin) * scale)));
            gr.ScaleTransform(scale, -scale);

            foreach (var gTriangle in g.Triangles)
            {
                gr.FillTriangle(0, 0, gTriangle.A, gTriangle.B, gTriangle.C, Colors.Black);
            }
        }
    }
}