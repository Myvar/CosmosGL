using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.TrueType
{
    public static class EndianUtils
    {

        public static ushort SwapEndian(ushort val)
        {
            return (ushort) ((val << 8) | (val >> 8));
        }

        public static short SwapEndian(short val)
        {
            return (short)((val << 8) | (val >> 8));
        }

        public static uint SwapEndian(uint val)
        {
           return (val << 24) | ((val << 8) & 0x00ff0000) |
                  ((val >> 8) & 0x0000ff00) | (val >> 24);
        }
    }
}
