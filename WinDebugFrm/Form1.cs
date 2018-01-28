#define WINDOWS

using System;
using System.IO;
using System.Windows.Forms;
using CosmosGL.System;
using CosmosGL.System.Graphics;
using CosmosGL.System.Imaging;

namespace WinDebugFrm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var canvas = new FormCanvas(Width, Height);

            var g = new Graphics(canvas);
            g.Clear(Colors.White);


           // var img = Image.FromBytes(File.ReadAllBytes("Myvar Logo Solid plain.ppm"), "ppm").ResizeImage(128, 128);
           // g.DrawImage(10, 10, img);


           var terminus = new SdfFont(File.ReadAllText("Terminus.fnt"),
                 Image.FromBytes(File.ReadAllBytes("Terminus.ppm"), "ppm"));

            // g.DrawString(10, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 14f, terminus, Colors.Black);
             // g.DrawString(10, 25, "abcdefghijklmnopqrstuvwxyz", 14f, terminus, Colors.Black);
            //  g.DrawString(10, 44, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 30f, terminus, Colors.Black);
           //   g.DrawString(10, 74, "abcdefghijklmnopqrstuvwxyz", 30f, terminus, Colors.Black);
            int x = 10;
            int y = 10;

            var s = "Hi";
            e.Graphics.Clear(System.Drawing.Color.White);
            for (int i = 1; i < 158; i++)
            {
                g.DrawString(x, y, s, i, terminus, Colors.Black);

                var size = g.MeasureString(s, i, terminus);

                y += size.Height + 2;

                if (y + 50 > Height)
                {
                    y = 10;
                    x += size.Width;
                }

               
                e.Graphics.DrawImageUnscaled(canvas.Bitmap, 0, 0);
                e.Graphics.Flush();
                Update();
            }
            

            //g.DrawString(10, 100, "Hello World", 10f, terminus, Colors.Black);

            e.Graphics.Clear(System.Drawing.Color.White);
            e.Graphics.DrawImageUnscaled(canvas.Bitmap, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            //  WindowState = FormWindowState.Maximized;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Update();
        }
    }
}