using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmosGL.TrueType.Tables
{
    public unsafe class FontDirectory
    {
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct OffsetSubtable
        {
            [FieldOffset(0)] public uint scalerType;
            [FieldOffset(4)] public ushort numTables;
            [FieldOffset(6)] public ushort searchRange;
            [FieldOffset(8)] public ushort entrySelector;
            [FieldOffset(10)] public ushort rangeShift;
        }


        public uint ScalerType { get; set; }
        public ushort NumTables { get; set; }
        public ushort SearchRange { get; set; }
        public ushort EntrySelector { get; set; }
        public ushort RangeShift { get; set; }

        public FontDirectory(void* ptr)
        {
            var d = (OffsetSubtable*) ptr;
            ScalerType = EndianUtils.SwapEndian(d->scalerType);
            NumTables = EndianUtils.SwapEndian(d->numTables);
            SearchRange = EndianUtils.SwapEndian(d->searchRange);
            EntrySelector = EndianUtils.SwapEndian(d->entrySelector);
            RangeShift = EndianUtils.SwapEndian(d->rangeShift);
        }
    }
}