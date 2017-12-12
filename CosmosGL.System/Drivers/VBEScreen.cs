﻿using CosmosGL.Hal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.IL2CPU.API.Attribs;
using CosmosGL.System.Drivers;
using CosmosGL.System.Graphics;
using XSharp.Assembler;

namespace CosmosGL.System
{
    public unsafe class VBEScreen
    {
        /// <summary>
        /// Driver for Setting vbe modes and ploting/getting pixels
        /// </summary>
        private VBEDriver _vbe = new VBEDriver();

        /// <summary>
        /// The current Width of the screen in pixels
        /// </summary>
        public int ScreenWidth { get; set; }
        /// <summary>
        /// The current Height of the screen in pixels
        /// </summary>
        public int ScreenHeight { get; set; }
        /// <summary>
        /// The current Bytes per pixel
        /// </summary>
        public int ScreenBpp { get; set; }

        #region Display

        /// <summary>
        /// All the avalible screen resolutions
        /// </summary>
        public enum ScreenSize
        {
            Size320x200,
            Size640x400,
            Size640x480,
            Size800x600,
            Size1024x768,
            Size1280x1024

        }
        /// <summary>
        /// All the suported Bytes per pixel
        /// </summary>
        public enum Bpp
        {
            Bpp15,
            Bpp16,
            Bpp24,
            Bpp32
        }

        /// <summary>
        /// Use this to setup the screen, this will disable the console.
        /// </summary>
        /// <param name="aSize">The dezired screen resolution</param>
        /// <param name="aBpp">The dezired Bytes per pixel</param>
        public void SetMode(ScreenSize aSize, Bpp aBpp)
        {
            //Get screen size
            switch (aSize)
            {
                case ScreenSize.Size320x200:
                    ScreenWidth = 320;
                    ScreenHeight = 200;
                    break;
                case ScreenSize.Size640x400:
                    ScreenWidth = 640;
                    ScreenHeight = 400;
                    break;
                case ScreenSize.Size640x480:
                    ScreenWidth = 640;
                    ScreenHeight = 480;
                    break;
                case ScreenSize.Size800x600:
                    ScreenWidth = 800;
                    ScreenHeight = 600;
                    break;
                case ScreenSize.Size1024x768:
                    ScreenWidth = 1024;
                    ScreenHeight = 768;
                    break;
                case ScreenSize.Size1280x1024:
                    ScreenWidth = 1280;
                    ScreenHeight = 1024;
                    break;
            }
            //Get bpp
            switch (aBpp)
            {
                case Bpp.Bpp15:
                    ScreenBpp = 15;
                    break;
                case Bpp.Bpp16:
                    ScreenBpp = 16;
                    break;
                case Bpp.Bpp24:
                    ScreenBpp = 24;
                    break;
                case Bpp.Bpp32:
                    ScreenBpp = 32;
                    break;
            }
            //set the screen
            _vbe.vbe_set((ushort)ScreenWidth, (ushort)ScreenHeight, (ushort)ScreenBpp);
        }

        #endregion

        #region Drawing
        //used to convert from rgb to hex color
        private uint GetIntFromRBG(byte red, byte green, byte blue)
        {
            uint x;
            x = (blue);
            x += (uint)(green << 8);
            x += (uint)(red << 16);
            return x;
        }

        public void Disable()
        {
            _vbe.vbe_disable();
        }

        public void Clear(Color color)
        {
            Clear((uint)color.ToHex());
        }

        /// <summary>
        /// Clear the screen with a given color
        /// </summary>
        /// <param name="color">The color in hex</param>
        public void Clear(uint color)
        {
            Color c = new Color((int)color);
           Memmory.memset((uint*) 0xE0000000, (uint)color, (uint)(ScreenWidth * ScreenHeight));
        }
        /// <summary>
        /// Clear the screen with a given color
        /// </summary>
        /// <param name="red">Red value max is 255</param>
        /// <param name="green">Green value max is 255</param>
        /// <param name="blue">Blue value max is 255</param>
        public void Clear(byte red, byte green, byte blue)
        {
            Clear(GetIntFromRBG(red, green, blue));
        }

        public void SetBuffer(byte[] Buffer)
        {
            _vbe.set_vram(Buffer);
        }

        /// <summary>
        /// Set a pixel at a given point to the give color
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="color">Color in hex</param>
        public void SetPixel(uint x, uint y, uint color)
        {
            byte* where = (byte*)(0xE0000000 + (x * ((uint)ScreenBpp / 8) + y * (uint)(ScreenWidth * ((uint)ScreenBpp / 8))));
            where[0] = (byte)(color & 255);              // BLUE
            where[0 + 1] = (byte)((color >> 8) & 255);   // GREEN
            where[0 + 2] = (byte)((color >> 16) & 255);  // RED
        }

        /// <summary>
        /// Set a pixel at a given point to the give color
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="red">Red value max is 255</param>
        /// <param name="green">Green value max is 255</param>
        /// <param name="blue">Blue value max is 255</param>
        public void SetPixel(uint x, uint y, byte red, byte green, byte blue)
        {
            SetPixel(x, y, GetIntFromRBG(red, green, blue));
        }
        #endregion

        #region Reading

        /// <summary>
        /// Get a pixel's color at the given point
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y"></param>
        /// <returns>Returns the color in hex</returns>
        public uint GetPixel(uint x, uint y)
        {
            uint where = x * ((uint)ScreenBpp / 8) + y * (uint)(ScreenWidth * ((uint)ScreenBpp / 8));
            return GetIntFromRBG(_vbe.get_vram(where + 2), _vbe.get_vram(where + 1), _vbe.get_vram(where));
        }
        #endregion

    }
}
