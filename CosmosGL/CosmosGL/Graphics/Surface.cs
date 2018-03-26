using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.Core;

namespace CosmosGL.Graphics
{
    public unsafe class Surface
    {
        public List<Layer> Layers { get; set; } = new List<Layer>();
        public int SelectedLayer { get; set; } = -1;

        private Layer _tmpLayer;

        public Layer ActiveLayer
        {
            get => Layers[SelectedLayer];
            set => Layers[SelectedLayer] = value;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public Surface(int width, int height)
        {
            Width = width;
            Height = height;

            PushLayer();
            _tmpLayer = new Layer(this);
        }

        public void PushLayer()
        {
            Layers.Add(new Layer(this));
            SelectedLayer++;
        }

        public void Clear(uint color)
        {
            Memory.Memset(ActiveLayer.Pixels, color, (uint) (Width * Height));
        }

        public void FillRectangle(int x, int y, int w, int h, uint color)
        {
            for (int i = 0; i < h; i++)
            {
                Memory.Memset((uint*) ((uint) ActiveLayer.Pixels + (x * 4) + ((i + y) * Width) * 4), color, (uint) w);
            }
        }

        public void FlushToDisplay()
        {
            Memory.Memset(_tmpLayer.Pixels, 0, (uint)(Width * Height));

            for (var index = 0; index < Layers.Count; index++)
            {
                Blend.BlendSurfaceInplace(_tmpLayer.Pixels, Layers[index].Pixels, Width , Height);
            }
            
            Memory.Memcpy((uint*)0xE0000000, _tmpLayer.Pixels, Width * Height);
        }
    }
}