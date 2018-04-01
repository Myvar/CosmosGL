using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmosGL.TrueType.Tables
{
    public unsafe class CharacterMap
    {
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct CmapIndex
        {
            [FieldOffset(0)] public ushort version;
            [FieldOffset(2)] public ushort numberSubtables;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct CmapEncoding
        {
            [FieldOffset(0)] public ushort platformID;
            [FieldOffset(2)] public ushort platformSpecificID;
            [FieldOffset(4)] public uint offset;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct cmap
        {
            [FieldOffset(0)] public ushort format;
            [FieldOffset(2)] public ushort length;
            [FieldOffset(4)] public ushort language;
            [FieldOffset(6)] public ushort segCountX2;
            [FieldOffset(8)] public ushort searchRange;
            [FieldOffset(10)] public ushort entrySelector;
            [FieldOffset(12)] public ushort rangeShift;
        }


        public List<ushort> Index { get; set; } = new List<ushort>();

        public CharacterMap(void* ptr)
        {
            var x = (CmapIndex*) ptr;
            var num = EndianUtils.SwapEndian(x->numberSubtables);

            var offset = (long) ptr + sizeof(CmapIndex);

            for (int i = 0; i < num; i++)
            {
                var z = (CmapEncoding*) offset;
                if (EndianUtils.SwapEndian(z->platformID) == 0)
                {
                    var psid = EndianUtils.SwapEndian(z->platformSpecificID);
                    var off = EndianUtils.SwapEndian(z->offset);

                    var map = (cmap*) ((long) ptr + off);
                    var format = EndianUtils.SwapEndian(map->format);
                    if (format == 4)
                    {
                        var range = EndianUtils.SwapEndian(map->searchRange);
                        var segcount = EndianUtils.SwapEndian(map->segCountX2) / 2;

                        var endCode = (ushort*) ((long) ptr + off + sizeof(cmap));
                        var startCode = (ushort*) ((long) endCode + 2 + (segcount * 2));
                        var idDelta = (ushort*) ((long) startCode + (segcount * 2));
                        var idRangeOffset = (ushort*) ((long) idDelta + (segcount * 2));

                        for (int charcode = 0; charcode < 255; charcode++)
                        {
                            var found = false;
                            for (int j = 0; j < segcount - 1; j++)
                            {

                                if ((EndianUtils.SwapEndian(endCode[j]) >= charcode)
                                    && (EndianUtils.SwapEndian(startCode[j]) <= charcode))
                                {
                                    if (EndianUtils.SwapEndian(idRangeOffset[j]) != 0)
                                    {
                                        Index.Add(
                                            *(&idRangeOffset[j] + EndianUtils.SwapEndian(idRangeOffset[j])
                                              / 2 + (charcode - EndianUtils.SwapEndian(startCode[j]))));
                                    }
                                    else
                                    {
                                        Index.Add((ushort) (EndianUtils.SwapEndian(idDelta[j]) + charcode));
                                    }
                                    found = true;
                                }
                            }

                            if (!found)
                            {
                                Index.Add(0);
                            }
                        }
                    }
                }

                offset += sizeof(CmapEncoding);
            }
        }
    }
}