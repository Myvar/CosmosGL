using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL;
using CosmosGL.Vbe;
using Sys = Cosmos.System;

namespace TestKernel
{
    public class Kernel: Sys.Kernel
    {
        protected override void BeforeRun()
        {
            //init fs
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            //init screen & gfx
            var vbe = new VbeScreen();
            vbe.SetMode(VbeScreen.ScreenSize.Size800X600, VbeScreen.Bpp.Bpp32);
            GfxDriver.Init();

            //clear screen
            GfxDriver.ClearScreen(0x3F5765, 800, 600);

        }
        
        protected override void Run()
        {
            
        }
    }
}
