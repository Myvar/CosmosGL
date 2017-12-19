﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System
{
    public class CGLFGlyph
    {
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public List<Triangle> Triangles { get; set; } = new List<Triangle>();
    }
}
