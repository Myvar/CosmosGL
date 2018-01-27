using CosmosGL.System;
using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using CosmosGL.System.Fonts;
using CosmosGL.System.Graphics;
using CosmosGL.System.Images;
using CosmosGL.System.Imaging;
using Sys = Cosmos.System;

namespace TestKernel
{
    public class Kernel : Sys.Kernel
    {
        public VbeScreen Screen = new VbeScreen();
        public Canvas Canvas = new Canvas(800, 600);
        public SdfFont terminus;

        protected override void BeforeRun()
        {
            Console.Clear();
            Screen.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);
            Screen.Clear(Colors.Blue);


            _deltaT = RTC.Second;

            var g = new Graphics(Canvas);
            g.Clear(Colors.White);


            Canvas.WriteToScreen();

            terminus = new SdfFont(Terminus.Terminus_fnt,
                Image.FromBytes(Terminus.Terminus_ppm, "ppm"));

            
            g.DrawString(10, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 14f, terminus, Colors.Black);
            Canvas.WriteToScreen();
            g.DrawString(10, 25, "abcdefghijklmnopqrstuvwxyz", 14f, terminus, Colors.Black);
            Canvas.WriteToScreen();
            g.DrawString(10, 44, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 30f, terminus, Colors.Black);
            Canvas.WriteToScreen();
            g.DrawString(10, 74, "abcdefghijklmnopqrstuvwxyz", 30f, terminus, Colors.Black);
            Canvas.WriteToScreen();
        }


        int _frames = 0;
        int _fps = 0;
        int _deltaT = 0;

        private bool flag = false;

        protected override void Run()
        {
            if (_deltaT != RTC.Second)
            {
                _fps = _frames;
                _frames = 0;
                _deltaT = RTC.Second;
            }

            _frames++;

            //  var g = new Graphics(Canvas);

            //g.Clear(Colors.White);


            /*
            //var img = Image.FromBytes(MyvarLogoPng.Myvar_LogoPng, "png");
            var img = Image.FromBytes(MyvarLogoPPM.Myvar_LogoPPM, "ppm");
            g.DrawImage(10, 10, img);*/

            // Canvas.WriteToScreen();
        }
    }
}