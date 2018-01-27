#define WINDOWS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGL.System;
using CosmosGL.System.Fonts;
using CosmosGL.System.Graphics;
using CosmosGL.System.Imaging;
using Point = CosmosGL.System.Graphics.Point;

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


            // var img = Image.FromBytes(File.ReadAllBytes("Terminus.ppm"), "ppm");
            //  g.DrawImage(10, 10, img);


            var terminus = new SdfFont(File.ReadAllText("Terminus.fnt"),
                Image.FromBytes(File.ReadAllBytes("Terminus.ppm"), "ppm"));

            g.DrawString(10, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 14f, terminus, Colors.Black);
            g.DrawString(10, 25, "abcdefghijklmnopqrstuvwxyz", 14f, terminus, Colors.Black);
            g.DrawString(10, 44, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 30f, terminus, Colors.Black);
            g.DrawString(10, 74, "abcdefghijklmnopqrstuvwxyz", 30f, terminus, Colors.Black);
            //g.DrawString(10, 100, "Hello World", 10f, terminus, Colors.Black);

            e.Graphics.Clear(System.Drawing.Color.White);
            e.Graphics.DrawImageUnscaled(canvas.Bitmap, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}