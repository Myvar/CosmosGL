using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL;
using CosmosGL.Graphics;
using CosmosGL.Vbe;
using Sys = Cosmos.System;

namespace TestKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            //init fs
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("Loading Gfx Driver");
            GfxDriver.Init();

            Console.WriteLine("Init VBE");
            //init screen & gfx
            var vbe = new VbeScreen();
            vbe.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);

            surf = new Surface(800, 600);
            surf.PushLayer();
            surf.PushLayer();

            surf.LayerIndex = 0;
            surf.Clear(0x2D4659FF);
            surf.FillRectangle(10, 10, 100, 100, 0xFF530DFF);
            surf.FillRectangle(10, 10, 50, 50, 0xD9D5D1FF);

            surf.LayerIndex = 1;
            surf.Clear(0x000000FF);
            surf.FillRectangle(10, 10, 50, 50, 0xD7885780);

            surf.LayerIndex = 2;
            surf.Clear(0x000000FF);
            surf.FillRectangle(x, 50, 100, 100, 0xA4615380);

            //blit the surf to the screen
            surf.Blit(0xE0000000, 0, 0, 800, 600);
        }

        private int x = 0;
        private Surface surf;
        protected override void Run()
        {
        }
    }
}