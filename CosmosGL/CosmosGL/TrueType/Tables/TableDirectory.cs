using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmosGL.TrueType.Tables
{
    public unsafe class TableDirectory
    {
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct TableDirectoryStruct
        {
            [FieldOffset(0)] public byte Id0;
            [FieldOffset(1)] public byte Id1;
            [FieldOffset(2)] public byte Id2;
            [FieldOffset(3)] public byte Id3;
            [FieldOffset(4)] public uint checkSum;
            [FieldOffset(8)] public uint offset;
            [FieldOffset(12)] public uint length;
        }

        public string Id { get; set; }
        public uint CheckSum { get; set; }
        public uint Offset { get; set; }
        public uint Length { get; set; }

        public TableDirectory(void* ptr)
        {
            var x = (TableDirectoryStruct*) ptr;
            Id = $"{(char) x->Id0}{(char) x->Id1}{(char) x->Id2}{(char) x->Id3}";
            CheckSum = EndianUtils.SwapEndian(x->checkSum);
            Offset = EndianUtils.SwapEndian(x->offset);
            Length = EndianUtils.SwapEndian(x->length);
        }
    }
}