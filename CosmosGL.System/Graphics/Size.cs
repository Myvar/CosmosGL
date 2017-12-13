using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System.Graphics
{
    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size(int w, int h)
        {
            Width = w;
            Height = h;
        }
    }
}
