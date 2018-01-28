using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosGL.System;
using CosmosGL.System.Graphics;

namespace WinDebugFrm
{
    public static class FontTest
    {
        public static void Draw(Graphics gr)
        {
          /*  var st = new Stopwatch();
            st.Start();

            var f = new CGLF(Karla.Karla_cglf);

            st.Stop();

            Debug.WriteLine(st.Elapsed.ToString());
            st.Reset();

            st.Start();
            int x = 0;
            int y = 0;
            int c = 0;
            for (var index = 0; index <  1; index++)
            {
                var g = f.Glyphs[index];
                var scale = 22f / f.UnitsPerEm;


                gr.TranslateTransform(x, y + (int) (((f.YMax - f.YMin) * scale)));
                gr.ScaleTransform(scale, -scale);

                x += 22;

                if (c > 10)
                {
                    y += 50;
                    x = 0;
                    c = 0;
                }

                foreach (var gTriangle in g.Triangles)
                {
                    gr.FillTriangle(0, 0, gTriangle.A, gTriangle.B, gTriangle.C, Colors.Black);
                }
            }
            st.Stop();

            Debug.WriteLine(st.Elapsed.ToString());
            st.Reset();*/
        }
    }
}