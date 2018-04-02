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
        public VbeScreen Display { get; set; }

        int _frames = 0;
        int _fps = 0;
        int _deltaT = 0;


        private Surface _surface;


        protected override void BeforeRun()
        {
            Console.Clear();

            var f = new Font(Karla.Karla_Regular_ttf);
            var g = f.Glyphs[(byte) 'C'];

            _deltaT = RTC.Second;

            Display = new VbeScreen();
            Display.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);

            _surface = new Surface(800, 600);
            _surface.Clear(0xFFD3D3D3);
           // _surface.FillRectangle(10, 10, 100, 100, 0xFF00FFFF);


            _surface.PushLayer();
            _surface.Clear(0x00000000);

            float scale = 0.1f;


            var x = g.Points;
            // x.Add(g.Points[GetBiggest(g.ContourEnds) + 1]);
            var color = 0xFFFF0000;
            var last = 0;
            for (int i = 1; i < x.Count; i++)
            {
                for (var index = 0; index < g.ContourEnds.Count; index++)
                {
                    var end = g.ContourEnds[index];
                    if (end == i)
                    {
                        {
                            var aPoint = x[i - 1];
                            var bPoint = x[i];
                            _surface.DrawLine(100 + (int)(aPoint.X * scale), 100 + (int)(aPoint.Y * scale),
                                100 + (int)(bPoint.X * scale), 100 + (int)(bPoint.Y * scale), color);
                        }
                        {
                            var aPoint = x[i];
                            var bPoint = x[last];
                            _surface.DrawLine(100 + (int)(aPoint.X * scale), 100 + (int)(aPoint.Y * scale),
                                100 + (int)(bPoint.X * scale), 100 + (int)(bPoint.Y * scale), color);
                        }

                        if (i + 2 > x.Count)
                        {
                            goto Done;
                        }
                        i++;
                        last = i;
                        i++;
                       
                        Done:;
                        if (color == 0xFFFF0000) color = 0xFF00FF00;
                        if (color == 0xFF00FF00) color = 0xFF0000FF;
                        break;
                    }
                }

                {
                    var aPoint = x[i - 1];
                    var bPoint = x[i];
                    _surface.DrawLine(100 + (int) (aPoint.X * scale), 100 + (int) (aPoint.Y * scale),
                        100 + (int) (bPoint.X * scale), 100 + (int) (bPoint.Y * scale), color);
                }
                
            }
           /* {
                var aPoint = x[x.Count - 1];
                var bPoint = x[last];
                _surface.DrawLine(100 + (int)(aPoint.X * scale), 100 + (int)(aPoint.Y * scale),
                    100 + (int)(bPoint.X * scale), 100 + (int)(bPoint.Y * scale), color);
            }*/

            _surface.FlushToDisplay();
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