using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmosGL.TrueType.Tables
{
    public unsafe class HeadTable
    {
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct HeadTableStruct
        {
            [FieldOffset(0)] public uint version;
            [FieldOffset(4)] public uint fontRevision;
            [FieldOffset(8)] public uint checkSumAdjustment;
            [FieldOffset(12)] public uint magicNumber;
            [FieldOffset(16)] public ushort flags;
            [FieldOffset(18)] public ushort unitsPerEm;
            [FieldOffset(20)] public long created;
            [FieldOffset(28)] public long modified;
            [FieldOffset(36)] public short xMin;
            [FieldOffset(38)] public short yMin;
            [FieldOffset(40)] public short xMax;
            [FieldOffset(42)] public short yMax;
            [FieldOffset(44)] public ushort macStyle;
            [FieldOffset(46)] public ushort lowestRecPPEM;
            [FieldOffset(48)] public short fontDirectionHint;
            [FieldOffset(30)] public short indexToLocFormat;
            [FieldOffset(32)] public short glyphDataFormat;
        }

        public uint Version { get; set; }
        public uint FontRevision { get; set; }
        public uint CheckSumAdjustment { get; set; }
        public uint MagicNumber { get; set; }
        public ushort Flags { get; set; }
        public ushort UnitsPerEm { get; set; }
        public long Created { get; set; }
        public long Modified { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public ushort MacStyle { get; set; }
        public ushort LowestRecPpem { get; set; }
        public short FontDirectionHint { get; set; }
        public short IndexToLocFormat { get; set; }
        public short GlyphDataFormat { get; set; }

        public HeadTable(void* ptr)
        {
            var x = (HeadTableStruct*) ptr;

            Version = EndianUtils.SwapEndian(x->version);
            FontRevision = EndianUtils.SwapEndian(x->fontRevision);
            CheckSumAdjustment = EndianUtils.SwapEndian(x->checkSumAdjustment);
            MagicNumber = EndianUtils.SwapEndian(x->magicNumber);
            Flags = EndianUtils.SwapEndian(x->flags);
            UnitsPerEm = EndianUtils.SwapEndian(x->unitsPerEm);
         //   Created = EndianUtils.SwapEndian(x->created);
         //   Modified = EndianUtils.SwapEndian(x->modified);
            XMin = EndianUtils.SwapEndian(x->xMin);
            YMin = EndianUtils.SwapEndian(x->yMin);
            XMax = EndianUtils.SwapEndian(x->xMax);
            YMax = EndianUtils.SwapEndian(x->yMax);
            MacStyle = EndianUtils.SwapEndian(x->macStyle);
            LowestRecPpem = EndianUtils.SwapEndian(x->lowestRecPPEM);
            FontDirectionHint = EndianUtils.SwapEndian(x->fontDirectionHint);
            IndexToLocFormat = EndianUtils.SwapEndian(x->indexToLocFormat);
            GlyphDataFormat = EndianUtils.SwapEndian(x->glyphDataFormat);
        }
    }
}