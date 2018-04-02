using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CosmosGL.TrueType.Tables
{
    public unsafe class Glyph
    {
        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct GlyphDescription
        {
            [FieldOffset(0)] public short numberOfContours;
            [FieldOffset(2)] public short xMin;
            [FieldOffset(4)] public short yMin;
            [FieldOffset(6)] public short xMax;
            [FieldOffset(8)] public short yMax;
        }

        private ushort GetBiggest(ushort* list, int total)
        {
            ushort max = 0;

            for (int i = 0; i < total; i++)
            {
                var u = EndianUtils.SwapEndian(list[i]);
                if (u > max)
                {
                    max = u;
                }
            }

            return max;
        }

        public short NumberOfContours { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public List<ushort> ContourEnds { get; set; } = new List<ushort>();
        public List<Point> Points { get; set; } = new List<Point>();


        public Glyph(void* ptr)
        {
            var onCurve = 1;
            var xIsByte = 2;
            var yIsByte = 4;
            var repeat = 8;
            var xDelta = 16;
            var yDelta = 32;

            var x = (GlyphDescription*) ptr;

            NumberOfContours = EndianUtils.SwapEndian(x->numberOfContours);
            XMin = EndianUtils.SwapEndian(x->xMin);
            YMin = EndianUtils.SwapEndian(x->yMin);
            XMax = EndianUtils.SwapEndian(x->xMax);
            YMax = EndianUtils.SwapEndian(x->yMax);

            var bace = (long) ptr + sizeof(GlyphDescription);

            var endPtsOfContours = (ushort*) (bace);
            var instructionLength = ((ushort*) (bace + NumberOfContours * 2));
            var instructions = (byte*) ((long) instructionLength + 2);
            var flagsdata = (byte*) ((long) instructions + instructionLength[0]);
            var numPoints = GetBiggest(endPtsOfContours, NumberOfContours) + 1;
            var z = numPoints;
            for (int i = 0; i < NumberOfContours; i++)
            {
                ContourEnds.Add(EndianUtils.SwapEndian(endPtsOfContours[i]));
            }


            var cp = new List<ContourPoint>();

            var flags = new List<byte>();
            for (var i = 0; i < numPoints; i++)
            {
                var flag = flagsdata[i];
                flags.Add(flag);
                var point = new ContourPoint((flag & onCurve) > 0);
                cp.Add(point);

                if ((flag & repeat) != 0)
                {
                    i++;
                    numPoints++;
                    var repeatCount = flagsdata[i];

                    i += repeatCount;
                    while (repeatCount-- != 0)
                    {
                        flags.Add(flag);
                        var pointa = new ContourPoint((flag & onCurve) > 0);
                        cp.Add(pointa);
                    }
                }
            }


            var j = ReadCoordString((long) flagsdata + flags.Count, z, flags.ToArray(), cp, "x", xIsByte,
                xDelta,
                XMin, XMax);
            ReadCoordString(j, z, flags.ToArray(), cp, "y", yIsByte, yDelta,
                YMin, YMax);

            foreach (var contourPoint in cp)
            {
                if (contourPoint.Values.Count != 0) Points.Add(new Point(contourPoint["x"], contourPoint["y"]));
            }
        }

        public long ReadCoordString(long off, int numPoints, byte[] flags, List<ContourPoint> points, string name,
            int byteFlag,
            int deltaFlag, int min, int max)
        {
            var value = 0;
            var offset = off;

            for (var i = 0; i < numPoints; i++)
            {
                var flag = flags[i];
                if ((flag & byteFlag) != 0)
                {
                    if ((flag & deltaFlag) != 0)
                    {
                        value += *(byte*) (offset);
                        offset++;
                    }
                    else
                    {
                        value -= *(byte*) (offset);
                        offset++;
                    }
                }
                else if ((~flag & deltaFlag) != 0)
                {
                    value += EndianUtils.SwapEndian(*(short*) (offset));
                    offset += 2;
                }
                else
                {
                    // value is unchanged.
                }

                points[i][name] = value;
            }

            return offset;
        }
    }
}