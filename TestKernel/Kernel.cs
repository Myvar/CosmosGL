using CosmosGL.System;
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using CosmosGL.System.Fonts;
using CosmosGL.System.Graphics;
using Sys = Cosmos.System;

namespace TestKernel
{
    public class Kernel : Sys.Kernel
    {
        public VbeScreen Screen = new VbeScreen();
        public Canvas Canvas = new Canvas(800, 600);

        protected override void BeforeRun()
        {
            Console.Clear();
            Screen.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);
            Screen.Clear(Colors.Blue);


            _deltaT = RTC.Second;

            var g = new Graphics(Canvas);
            g.Clear(Colors.White);


            Canvas.WriteToScreen();
        }


        int _frames = 0;
        int _fps = 0;
        int _deltaT = 0;

        protected override void Run()
        {
            if (_deltaT != RTC.Second)
            {
                _fps = _frames;
                _frames = 0;
                _deltaT = RTC.Second;
            }

            _frames++;

            var g = new Graphics(Canvas);
            g.Clear(Colors.DarkCyan);

            /*   g.FillRectangle(10, 20, 50, 50, Colors.Green);

              g.FillRectangle(10, 10, 100, 100, Colors.BlanchedAlmond);

              g.DrawLine(20, 30, 70, 60, Colors.Blue);
                g.DrawEllipse(40, 40, 20, 40, Colors.DarkKhaki);


                g.FillEllipse(100, 100, 20, 40, Colors.DarkSeaGreen);

                g.DrawPath(new Point[]
                {
                    new Point(200, 220),
                    new Point(250, 210),
                    new Point(300, 260),
                }, Colors.Aqua);


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

              g.DrawPolygon(new Point[]
              {
                  new Point(300, 300),
                  new Point(350, 300),
                  new Point(350, 350),
              }, Colors.Red);

              Canvas.WriteToScreen();

              */

            g.FillRectangle(10, 20, 50, 50, Colors.Green);

            FontTest.Draw(ref g);

            Canvas.WriteToScreen();
        }
    }
}