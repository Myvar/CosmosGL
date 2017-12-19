using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLFontConverter
{
    public class CGLF
    {
        public List<CGLFGlyph> Glyphs { get; set; } = new List<CGLFGlyph>();

        public ushort UnitsPerEm { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }

        public short XMax { get; set; }
        public short YMax { get; set; }

        public void AddGlyph(CGLFGlyph g)
        {
            Glyphs.Add(g);
        }

        public void WriteToFile(string file)
        {
            var bytes = new List<byte>();


            bytes.AddRange(BitConverter.GetBytes(UnitsPerEm).Reverse());
            bytes.AddRange(BitConverter.GetBytes(XMin).Reverse());
            bytes.AddRange(BitConverter.GetBytes(YMin).Reverse());
            bytes.AddRange(BitConverter.GetBytes(XMax).Reverse());
            bytes.AddRange(BitConverter.GetBytes(YMax).Reverse());


            bytes.AddRange(BitConverter.GetBytes(Glyphs.Count).Reverse());

            foreach (var g in Glyphs)
            {
                bytes.AddRange(BitConverter.GetBytes(g.YMin).Reverse());
                bytes.AddRange(BitConverter.GetBytes(g.YMax).Reverse());
                bytes.AddRange(BitConverter.GetBytes(g.XMax).Reverse());
                bytes.AddRange(BitConverter.GetBytes(g.XMin).Reverse());

                bytes.AddRange(BitConverter.GetBytes(g.Triangles.Count).Reverse());

                foreach (var gTriangle in g.Triangles)
                {
                    bytes.AddRange(BitConverter.GetBytes(gTriangle.A.X).Reverse());
                    bytes.AddRange(BitConverter.GetBytes(gTriangle.A.Y).Reverse());

                    bytes.AddRange(BitConverter.GetBytes(gTriangle.B.X).Reverse());
                    bytes.AddRange(BitConverter.GetBytes(gTriangle.B.Y).Reverse());

                    bytes.AddRange(BitConverter.GetBytes(gTriangle.C.X).Reverse());
                    bytes.AddRange(BitConverter.GetBytes(gTriangle.C.Y).Reverse());
                }
            }
            File.WriteAllBytes(file, bytes.ToArray());
            
        }
    }

    public class CGLFGlyph
    {
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public List<Triangle> Triangles { get; set; } = new List<Triangle>();
    }
}