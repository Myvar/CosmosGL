using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System.TrueType
{
    public class Component
    {
        public ushort GlyphIndex { get; set; }
        public Matrix Matrix { get; set; }
        public short DestPointIndex { get; set; }
        public short SrcPointIndex { get; set; }
    }
}
