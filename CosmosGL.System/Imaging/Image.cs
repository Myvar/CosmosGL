using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.System.Graphics;
using CosmosGL.System.Imaging.Formats;

namespace CosmosGL.System.Imaging
{
    public class Image
    {

        public static Image FromBytes(byte[] data, string type)
        {
            switch (type)
            {
                case "ppm":
                    return new PPM().Read(data);
                case "png":
                    return new Png().Read(data);
            }

            return null;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        private uint[] _buffer;

        public Image(int width, int height)
        {
            Width = width;
            Height = height;

            _buffer = new uint[Width * Height * 4];
        }

        public void SetPixel(int x, int y, Color c)
        {
            SetPixel(x, y, (uint)c.ToHex());
        }

        public void SetPixel(int x, int y, uint c)
        {
            _buffer[x + (y * Width)] = c;
        }

        public Color GetPixel(int x, int y)
        {
            return new Color((int)_buffer[x + (y * Width)]);
        }

        public void Clear(Color c)
        {
          Clear((uint)c.ToHex());
        }

        public void Clear(uint c)
        {
            for (int i = 0; i < Width * Height * 4; i++)
            {
                _buffer[i] = c;
            }
        }
    }
}
