using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System.TrueType
{
    public class Table
    {
        public uint Checksum { get; set; }
        public uint Offset { get; set; }
        public uint Length { get; set; }
    }
}
