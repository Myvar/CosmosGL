using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.TrueType.Tables;

namespace CosmosGL.TrueType
{
    public unsafe class Font
    {
        public HeadTable Head { get; set; }
        private CharacterMap CharacterMap { get; set; }
        public Glyph[] Glyphs { get; set; } = new Glyph[255];

        private int GetGlyphOffset(TableDirectory td, long off, int index)
        {
            var table = td;
            var offset = 0;

            if (Head.IndexToLocFormat == 1)
                offset = (int) EndianUtils.SwapEndian(*((uint*) (table.Offset + off + index * 4)));
            else
                offset = (int) EndianUtils.SwapEndian(*((ushort*) (off + table.Offset + index * 2))) * 2;

            return offset;
        }

        public Font(byte[] file)
        {
            fixed (void* data = file)
            {
                var fd = new FontDirectory(data);

                var tds = new List<TableDirectory>();

                var offsetbase = (long) data + sizeof(FontDirectory.OffsetSubtable);
                var offset = offsetbase;

                for (var i = 0; i < fd.NumTables; i++)
                {
                    var td = new TableDirectory((void*) offset);
                    tds.Add(td);
                    offset += sizeof(TableDirectory.TableDirectoryStruct);
                }

                var glyfOffsets = new int[255];
                uint glyfOffset = 0;

                foreach (var td in tds)
                {
                    /*
                        * head - font header
                        * cmap - character to glyph mapping
                        * glyf - glyph data                     
                        * hhea - horizontal header
                        * hmtx - horizontal metrics                     
                        */
                    switch (td.Id)
                    {
                        case "head":
                            Head = new HeadTable((void*) ((long) data + td.Offset));
                            break;
                        case "cmap":
                            CharacterMap = new CharacterMap((void*) ((long) data + td.Offset));
                            break;
                        case "loca":
                            for (var i = 0; i < 255; i++) glyfOffsets[i] = GetGlyphOffset(td, (long) data, i);

                            break;
                        case "glyf":
                            glyfOffset = td.Offset;
                            break;
                    }
                }

                for (var i = 0; i < 150; i++)
                {
                    var maped = CharacterMap.Index[i];
                    Glyphs[i] = new Glyph((void*) ((long) data + glyfOffset + glyfOffsets[maped]));
                }
            }
        }
    }
}