using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using CosmosGL;
using CosmosGL.Graphics;
using CosmosGL.TrueType;
using Sys = Cosmos.System;

namespace TestKernel
{
    public class Kernel : Sys.Kernel
    {
       /* public VbeScreen Display { get; set; }

        int _frames = 0;
        int _fps = 0;
        int _deltaT = 0;*/


        private Surface _surface;

        protected override void BeforeRun()
        {
            Console.Clear();

            var f = new Font(Karla.Karla_Regular_ttf);
            

            /*  _deltaT = RTC.Second;
  
              Display = new VbeScreen();
              Display.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);
  
              _surface = new Surface(800, 600);
              _surface.Clear(0xFFD3D3D3);
              _surface.FillRectangle(10, 10, 100, 100, 0xFF00FF00);
  
  
              _surface.PushLayer();
              _surface.Clear(0x00000000);
              _surface.FillRectangle(60, 60, 100, 100, 0x80FF0000);
  
  
              _surface.FlushToDisplay();*/
        }


        protected override void Run()
        {
          /*  if (_deltaT != RTC.Second)
            {
                _fps = _frames;
                _frames = 0;
                _deltaT = RTC.Second;
            }

            _frames++;

            _surface.SelectedLayer = 1;
            _surface.Clear(0x00000000);
            _surface.FillRectangle(60, 60, 100, 100, 0x80FF0000);

            _surface.FlushToDisplay();*/
        }
    }
}