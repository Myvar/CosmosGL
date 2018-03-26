using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.Core;

namespace CosmosGL.Graphics
{
    public unsafe class Layer
    {
        public uint* Pixels { get; set; }

        public Layer(Surface surface)
        {
            Pixels = (uint*) Memory.MemAlloc(surface.Height * surface.Width * 4);
        }
    }
}
