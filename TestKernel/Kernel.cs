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
        public VBEScreen Screen = new VBEScreen();
        public Canvas Canvas = new Canvas(800, 600);

        protected override void BeforeRun()
        {
            Console.Clear();
            Screen.SetMode(VBEScreen.ScreenSize.Size800x600, VBEScreen.Bpp.Bpp32);
            Screen.Clear(Colors.Blue);


            deltaT = RTC.Second;

            var g = new Graphics(Canvas);
            g.Clear(Colors.White);


            Canvas.WriteToScreen();
        }

//        public void FontTest(Graphics gr)
//        {
//            var font = new TrueTypeFont(Cairo.CairoRegular_ttf);
//
//            float scale = 64f / font.UnitsPerEm;
//
//            var y = 200;
//            var x = 200;
//
//            int c = 0;
//
//            for (int i = 25; i < 26; i++)
//            {
//                var g = font.ReadGlyph(5);
//
//                font.DrawGlyph(gr, i, scale, scale, x, y);
//                x += g.XMax + 5;
//
//                if (c > 15)
//                {
//                    y += 200;
//                    c = 0;
//                }
//                c++;
//            }
//        }

        int Frames = 0;
        int FPS = 0;
        int deltaT = 0;

        protected override void Run()
        {
            if (deltaT != RTC.Second)
            {
                FPS = Frames;
                Frames = 0;
                deltaT = RTC.Second;
            }

            Frames++;

            var g = new Graphics(Canvas);
            g.Clear(Colors.DarkCyan);

            //test code

            //FontTest(g);

            g.FillRectangle(10, 20, 50, 50, Colors.Green);

            g.DrawLine(20, 30, 70, 60, Colors.Blue);
            g.DrawEllipse(40, 40, 20, 40, Colors.DarkKhaki);
            g.DrawRectangle(10, 10, 100, 100, Colors.BlanchedAlmond);

            g.FillEllipse(100, 100, 20, 40, Colors.DarkSeaGreen);

            g.DrawPath(new Point[]
            {
                new Point(200,220), 
                new Point(250,210), 
                new Point(300,260), 
            }, Colors.Aqua);



            /*   g.FillPolygon(new Point[]
             {
                 new Point(300,300),
                 new Point(350,300),
                 new Point(350,350),
             }, Colors.Beige);
             */
            g.DrawPolygon(new Point[]
             {
                 new Point(300,300),
                 new Point(350,300),
                 new Point(350,350),
             }, Colors.Red);

            g.FillRectangle(25, 35, 100, 100, new Color(Colors.Red, 128));

            Canvas.WriteToScreen();
        }
    }
}