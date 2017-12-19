#define WINDOWS

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmosGL.System.Graphics;
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
            g.Clear(Colors.DarkCyan);
            /*
                        //test code

                 */
            //g.IncludeClip(50, 50, 100, 100);

             
              g.DrawLine(20, 30, 70, 60, Colors.Blue);
              g.DrawEllipse(40, 40, 20, 40, Colors.DarkKhaki);
              g.DrawRectangle(10, 10, 100, 100, Colors.BlanchedAlmond);

              g.FillEllipse(100, 100, 20, 40, Colors.DarkSeaGreen);

              g.DrawPath(new Point[]
              {
                  new Point(200, 220),
                  new Point(250, 210),
                  new Point(300, 260),
              }, Colors.Aqua);

              g.DrawPolygon(new Point[]
              {
                  new Point(300, 300),
                  new Point(350, 300),
                  new Point(350, 350),
              }, Colors.Red);

            canvas.Lock();

            g.FillPolygon(new Point[]
            {
                new Point(300, 300),
                new Point(350, 300),
                new Point(350, 350),
            }, Colors.Beige);


            g.FillPolygon(new Point[]
              {
                  new Point(400, 300),
                  new Point(450, 300),
                  new Point(450, 450),
                  new Point(500, 450),
              }, Colors.Beige);

  
         
            g.FillRectangle(10, 20, 50, 50, Colors.Green);

           
            FontTest.Draw(g);
            canvas.UnLock();

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