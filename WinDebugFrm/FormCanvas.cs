using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinDebugFrm
{
    public unsafe class FormCanvas : CosmosGL.System.Graphics.ICanvas
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public System.Drawing.Bitmap Bitmap;

        public FormCanvas(int width, int height)
        {
            Width = width;
            Height = height;

            Bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public void Clear(uint c)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetPixel(x, y, c);
                }
            }
        }

        public CosmosGL.System.Graphics.Color GetPixel(int x, int y)
        {
            if(x < 0 || x >= Width || y < 0 || y >= Height) return new CosmosGL.System.Graphics.Color(0,0,0,0);
            var z = Bitmap.GetPixel(x, y);
           
            return new CosmosGL.System.Graphics.Color(z.R, z.G, z.B, z.A);
        }

        public void SetPixel(int x, int y, CosmosGL.System.Graphics.Color c)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Height)
            {
                Bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B));
            }
        }

        public void SetPixel(int x, int y, uint c)
        {
            SetPixel(x, y, new CosmosGL.System.Graphics.Color((int) c));
        }

        public BitmapData bmpData;

        public void WriteToScreen()
        {
        }


        public void Lock()
        {
            bmpData = Bitmap.LockBits(new Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format32bppRgb);
        }

        public void UnLock()
        {
            Bitmap.UnlockBits(bmpData);
        }

        public void SetScanLine(int offset, int length, uint color)
        {
            if (offset >= 0)
            {
                var data = (uint*) bmpData.Scan0.ToPointer();

                for (int i = offset; i < offset + length; i++)
                {
                    data[i] = color;
                }
            }
        }
    }
}