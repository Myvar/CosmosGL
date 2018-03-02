using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.Graphics
{
    public class Surface
    {
        public int LayerIndex { get; set; } = 0;
        public List<uint> Layers { get; set; } = new List<uint>();

        public uint CurrentLayer => Layers[LayerIndex];

        public int Width { get; set; }
        public int Height { get; set; }

        private uint NewLayer()
        {
            return GfxDriver.Malloc((uint) (Width * Height * 4));
        }

        public Surface(int w, int h)
        {
            Width = w;
            Height = h;
            Layers.Add(NewLayer());
        }

        public void PushLayer()
        {
            Layers.Add(NewLayer());
            LayerIndex++;
        }

        public void Blit(Surface s)
        {
            Blit(s.CurrentLayer, 0, 0, s.Width, s.Height);
        }

        public void Dispose()
        {
            foreach (var layer in Layers)
            {
                GfxDriver.Free(layer);
            }
        }

        public void Blit(uint target, int x, int y, int w, int h)
        {
            var tmpLayer = NewLayer();

            foreach (var layer in Layers)
            {
                GfxDriver.BlendLayers(tmpLayer, layer, w, h);
            }
            
            GfxDriver.Blit(target, tmpLayer, x, y, w, h);

            GfxDriver.Free(tmpLayer);
        }

        public void Clear(uint color)
        {
            GfxDriver.Clear(CurrentLayer, color, (uint) Width, (uint) Height);
        }

        public void SetPixel(int x, int y, uint color)
        {
        }

        public uint GetPixel(int x, int y)
        {
            return 0;
        }

        public void FillRectangle(int x, int y, int w, int h, uint color)
        {
            GfxDriver.Fillrect(CurrentLayer, x, y, w, h, color, Width);
        }
    }
}