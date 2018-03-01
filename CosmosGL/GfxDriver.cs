using System;
using System.Collections.Generic;
using System.Text;
using CosmosELFCore;

namespace CosmosGL
{
    public static unsafe class GfxDriver
    {
        private static UnmanagedExecutible _exe;

        public static void Init()
        {
            fixed (byte* ptr = BlockReader.ReadBytes("0:\\gfx.so"))
            {
                _exe = new UnmanagedExecutible(ptr);
                _exe.Load();
                _exe.Link();
            }
        }

        public static void ClearScreen(uint color, int w, int h)
        {
            new ArgumentWriter()
                .Push(color)
                .Push((uint) w)
                .Push((uint) h);
            _exe.Invoke("clear_screen");
        }
    }
}