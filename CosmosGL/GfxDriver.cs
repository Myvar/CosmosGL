using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.Core.Memory.Old;
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

                //give it some memmory
                uint memSize = 200 * 1024 * 1024;
                InitMemmory(Heap.MemAlloc(memSize), memSize);
            }
        }

        private static void InitMemmory(uint ptr, uint size)
        {
            new ArgumentWriter()
                .Push(ptr)
                .Push(size);
            _exe.Invoke("heap_init");
        }

        public static uint Malloc(uint size)
        {
            new ArgumentWriter()
                .Push(size);
            return _exe.Invoke("malloc");
        }

        public static void Free(uint ptr)
        {
            new ArgumentWriter()
                .Push(ptr);
            _exe.Invoke("free");
        }

        public static void Blit(uint target, uint src, int x0, int y, int w, int h)
        {
            new ArgumentWriter()
                .Push(target)
                .Push(src)
                .Push(x0)
                .Push(y)
                .Push(w)
                .Push(h);
            _exe.Invoke("blit");
        }

        public static void BlendLayers(uint background, uint foreground, int w, int h)
        {
            new ArgumentWriter()
                .Push(background)
                .Push(foreground)
                .Push(w)
                .Push(w);
            _exe.Invoke("blend_image_inplace");
        }


        public static void Fillrect(uint target, int x, int y, int w, int h, uint color, int cw)
        {
            new ArgumentWriter()
                .Push(target)
                .Push(x)
                .Push(y)
                .Push(w)
                .Push(h)
                .Push(color)
                .Push(cw);
            _exe.Invoke("fillrect");
        }

        public static void Clear(uint target, uint color, uint w, uint h)
        {
            new ArgumentWriter()
                .Push(target)
                .Push(color)
                .Push(w)
                .Push(h);
            _exe.Invoke("clear");
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