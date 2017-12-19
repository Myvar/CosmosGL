using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System.TrueType
{
    public class Glyph
    {
        public short NumberOfContours { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public string Type { get; set; }
        public List<ushort> ContourEnds { get; set; } = new List<ushort>();
        public List<Point> Points { get; set; } = new List<Point>();
        public List<Component> Components { get; set; } = new List<Component>();
    }
}
