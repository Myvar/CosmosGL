﻿using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.System.Graphics;

namespace CosmosGL.System.TrueType
{
    public class TrueTypeFont
    {
        private BinaryReader _file;

        public List<string> TableList { get; set; } = new List<string>();
        public List<Table> Tables { get; set; } = new List<Table>();

        public int GetOffset(string s)
        {
            for (var index = 0; index < TableList.Count; index++)
            {
                var t = TableList[index];
                if (t == s)
                {
                    return index;
                }
            }

            return -1;
        }

        public Table GetTable(string s)
        {
            return Tables[GetOffset(s)];
        }

        public uint ScalarType { get; set; }
        public uint SearchRange { get; set; }
        public uint EntrySelector { get; set; }
        public uint RangeShift { get; set; }
        public int Version { get; set; }
        public int FontRevision { get; set; }
        public uint ChecksumAdjustment { get; set; }
        public uint MagicNumber { get; set; }
        public ushort Flags { get; set; }
        public ushort UnitsPerEm { get; set; }
        public short XMin { get; set; }
        public short YMin { get; set; }
        public short XMax { get; set; }
        public short YMax { get; set; }
        public ushort MacStyle { get; set; }
        public ushort LowestRecPPEM { get; set; }
        public short FontDirectionHint { get; set; }
        public short IndexToLocFormat { get; set; }
        public short GlyphDataFormat { get; set; }
        public int Lenght { get; set; }

        public TrueTypeFont(byte[] file)
        {
            _file = new BinaryReader(file);

            ReadOffsetTables();
            ReadHeadTable();
            Lenght = GlyphCount();
        }


        public void ReadOffsetTables()
        {
            ScalarType = _file.GetUint32();

            var numTables = _file.GetUint16();


            SearchRange = _file.GetUint16();
            EntrySelector = _file.GetUint16();
            RangeShift = _file.GetUint16();


            for (var i = 0; i < numTables; i++)
            {
                var tag = _file.GetString(4);

                var table = new Table()
                {
                    Checksum = _file.GetUint32(),
                    Offset = _file.GetUint32(),
                    Length = _file.GetUint32()
                };


                TableList.Add(tag);
                Tables.Add(table);
            }
        }


        public void ReadHeadTable()
        {
            _file.Seek((int)GetTable("head").Offset);

            Version = _file.GetFixed();
            FontRevision = _file.GetFixed();
            ChecksumAdjustment = _file.GetUint32();
            MagicNumber = _file.GetUint32();
            Flags = _file.GetUint16();
            UnitsPerEm = _file.GetUint16();
            _file.GetDate();
            _file.GetDate();
            XMin = _file.GetFword();
            YMin = _file.GetFword();
            XMax = _file.GetFword();
            YMax = _file.GetFword();
            MacStyle = _file.GetUint16();
            LowestRecPPEM = _file.GetUint16();
            FontDirectionHint = _file.GetInt16();
            IndexToLocFormat = _file.GetInt16();
            GlyphDataFormat = _file.GetInt16();
        }

        public int GlyphCount()
        {
            var old = _file.Seek((int)(GetTable("maxp").Offset + 4));
            var count = _file.GetUint16();
            _file.Seek(old);
            return count;
        }

        public int GetGlyphOffset(int index)
        {
            var table = GetTable("loca");
            var offset = 0;
            var old = 0;

            if (IndexToLocFormat == 1)
            {
                old = _file.Seek((int)(table.Offset + index * 4));
                offset = (int)_file.GetUint32();
            }
            else
            {
                old = _file.Seek((int)(table.Offset + index * 2));
                offset = _file.GetUint16() * 2;
            }

            _file.Seek(old);

            return (int)(offset + GetTable("glyf").Offset);
        }

        public List<Glyph> ReadGlyphs(int start, int count)
        {
            var glyphs = new List<Glyph>();

            for (int i = start; i < start + count; i++)
            {
                glyphs.Add(ReadGlyph(i));
            }


            return glyphs;
        }


        public Glyph ReadGlyph(int index)
        {
            var offset = GetGlyphOffset(index);
            var _file = this._file;

            if (offset >= GetTable("glyf").Offset + GetTable("glyf").Length)
            {
                return null;
            }

            _file.Seek(offset);

            var glyph = new Glyph()
            {
                NumberOfContours = _file.GetInt16(),
                XMin = _file.GetFword(),
                YMin = _file.GetFword(),
                XMax = _file.GetFword(),
                YMax = _file.GetFword()
            };


            if (glyph.NumberOfContours == -1)
            {
                ReadCompoundGlyph(ref glyph);
            }
            else
            {
                this.ReadSimpleGlyph(ref glyph);
            }

            return glyph;
        }

        public void ReadCoordString(int numPoints, byte[] flags, List<Point> Points, string name, int byteFlag,
            int deltaFlag, int min, int max)
        {
            var value = 0;

            for (var i = 0; i < numPoints; i++)
            {
                var flag = flags[i];
                if ((flag & byteFlag) != 0)
                {
                    if ((flag & deltaFlag) != 0)
                    {
                        value += _file.GetUint8();
                    }
                    else
                    {
                        value -= _file.GetUint8();
                    }
                }
                else if ((~flag & deltaFlag) != 0)
                {
                    value += _file.GetInt16();
                }
                else
                {
                    // value is unchanged.
                }
                Points[i][name] = value;
            }
        }

        private ushort GetBiggest(List<ushort> list)
        {
            ushort max = 0;
            foreach (ushort u in list)
            {
                if (u > max)
                {
                    max = u;
                }
            }

            return max;
        }

        public void ReadSimpleGlyph(ref Glyph glyph)
        {
            var ON_CURVE = 1;
            var X_IS_BYTE = 2;
            var Y_IS_BYTE = 4;
            var REPEAT = 8;
            var X_DELTA = 16;
            var Y_DELTA = 32;

            glyph.Type = "simple";
            glyph.ContourEnds = new List<ushort>();
            glyph.Points = new List<Point>();

            for (var i = 0; i < glyph.NumberOfContours; i++)
            {
                glyph.ContourEnds.Add(_file.GetUint16());
            }

            // skip over intructions
            _file.Seek(_file.GetUint16() + _file.Tell());

            if (glyph.NumberOfContours == 0)
            {
                return;
            }

            var numPoints = GetBiggest(glyph.ContourEnds) + 1;

            var flags = new List<byte>();

            for (var i = 0; i < numPoints; i++)
            {
                var flag = _file.GetUint8();
                flags.Add(flag);
                var point = new Point((flag & ON_CURVE) > 0);
                glyph.Points.Add(point);

                if ((flag & REPEAT) != 0)
                {
                    var repeatCount = _file.GetUint8();
                    i += repeatCount;
                    while (repeatCount-- != 0)
                    {
                        flags.Add(flag);
                        var pointa = new Point((flag & ON_CURVE) > 0);
                        glyph.Points.Add(pointa);
                    }
                }
            }

            ReadCoordString(numPoints, flags.ToArray(), glyph.Points, "x", X_IS_BYTE, X_DELTA, glyph.XMin, glyph.XMax);
            ReadCoordString(numPoints, flags.ToArray(), glyph.Points, "y", Y_IS_BYTE, Y_DELTA, glyph.YMin, glyph.YMax);
        }

        public void ReadCompoundGlyph(ref Glyph glyph)
        {
            var ARG_1_AND_2_ARE_WORDS = 1;
            var ARGS_ARE_XY_VALUES = 2;
            var ROUND_XY_TO_GRID = 4;
            var WE_HAVE_A_SCALE = 8;
            // RESERVED              = 16
            var MORE_COMPONENTS = 32;
            var WE_HAVE_AN_X_AND_Y_SCALE = 64;
            var WE_HAVE_A_TWO_BY_TWO = 128;
            var WE_HAVE_INSTRUCTIONS = 256;
            var USE_MY_METRICS = 512;
            var OVERLAP_COMPONENT = 1024;

            glyph.Type = "compound";
            glyph.Components = new List<Component>();

            var flags = MORE_COMPONENTS;
            while ((flags & MORE_COMPONENTS) != 0)
            {
                short arg1, arg2;

                flags = _file.GetUint16();
                var component = new Component()
                {
                    GlyphIndex = _file.GetUint16(),
                    Matrix = new Matrix()
                    {
                        A = 1,
                        B = 0,
                        C = 0,
                        D = 1,
                        E = 0,
                        F = 0
                    }
                };

                if ((flags & ARG_1_AND_2_ARE_WORDS) != 0)
                {
                    arg1 = _file.GetInt16();
                    arg2 = _file.GetInt16();
                }
                else
                {
                    arg1 = _file.GetUint8();
                    arg2 = _file.GetUint8();
                }

                if ((flags & ARGS_ARE_XY_VALUES) != 0)
                {
                    component.Matrix.E = arg1;
                    component.Matrix.F = arg2;
                }
                else
                {
                    component.DestPointIndex = arg1;
                    component.SrcPointIndex = arg2;
                }

                if ((flags & WE_HAVE_A_SCALE) != 0)
                {
                    component.Matrix.A = _file.Get2Dot14();
                    component.Matrix.D = component.Matrix.A;
                }
                else if ((flags & WE_HAVE_AN_X_AND_Y_SCALE) != 0)
                {
                    component.Matrix.A = _file.Get2Dot14();
                    component.Matrix.D = _file.Get2Dot14();
                }
                else if ((flags & WE_HAVE_A_TWO_BY_TWO) != 0)
                {
                    component.Matrix.A = _file.Get2Dot14();
                    component.Matrix.B = _file.Get2Dot14();
                    component.Matrix.C = _file.Get2Dot14();
                    component.Matrix.D = _file.Get2Dot14();
                }

                glyph.Components.Add(component);
            }

            if ((flags & WE_HAVE_INSTRUCTIONS) != 0)
            {
                _file.Seek(_file.GetUint16() + _file.Tell());
            }
        }

     
        public bool DrawGlyph(Graphics.Graphics gr, int index, float scalex, float scaley, int translatex, int translatey)
        {

          

            var glyph = ReadGlyph(index);

            if (glyph == null || glyph.Type != "simple")
            {
                return false;
            }


            var c = 0;

            var firstx = 0;
            var firsty = 0;

            bool flag = false;

            for (var i = 0; i < glyph.Points.Count - 1; i++)
            {
                if (i == glyph.ContourEnds[c])
                {
                    c += 1;
                    i++;
                    flag = false;
                }
                else
                {

                    var glyphPointA = glyph.Points[i];
                    var glyphPointB = glyph.Points[i + 1];

                    if (!false)
                    {
                        firstx = glyphPointA["x"];
                        firsty = glyphPointA["y"];
                        flag = true;
                    }
                

                    var x1 = scalex * (glyphPointA["x"] + translatex);
                    var y1 = scaley * (glyphPointA["y"] + translatey);

                    var x2 = scalex * (glyphPointB["x"] + translatex);
                    var y2 = scaley * (glyphPointB["y"] + translatey);

                    Console.WriteLine(
                        $"(scalex: {scalex}, trasx: {translatex}) x1: {x1}, y1: {y1} | x2: {x2}, y2: {y2}");

                    gr.DrawLine((int) x1, (int) y1, (int) x2, (int) y2, Colors.Black);

                }


            }


            /* 
                var p = 0;
            var c = 0;
            var first = 1;

            var startX = 0;
            var startY = 0;
              while (p < glyph.Points.Count)
              {
                  var point = glyph.Points[p];
                  if (first == 1)
                  {
                      startX = point["x"];
                      startY = point["y"];
                      first = 0;
                  }
                  else
                  {
  
                      var x1 = scalex * (startX + translatex);
                      var y1 = scaley * (startY + translatey);
  
                      var x2 = scalex * (point["x"] + translatex);
                      var y2 = scaley * (point["y"] + translatey);
  
                      Console.WriteLine($"(scalex: {scalex}, trasx: {translatex}) x1: {x1}, y1: {y1} | x2: {x2}, y2: {y2}");
  
                      DrawLine(screen, (int) x1, (int) y1, (int) x2 , (int) y2, (uint) (p * 20));
  
                      startX = point["x"];
                      startY = point["y"];
  
                  }
  
                  if (p == glyph.ContourEnds[c])
                  {
                      c += 1;
                      first = 1;
                  }
  
                  p += 1;
              }*/

            return true;
        }
    }
}