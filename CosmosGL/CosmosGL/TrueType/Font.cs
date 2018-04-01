using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.TrueType.Tables;

namespace CosmosGL.TrueType
{
    public unsafe class Font
    {
        public HeadTable Head { get; set; }

        public Font(byte[] file)
        {
            fixed (void* data = file)
            {
                var fd = new FontDirectory(data);

                var offsetbase = (uint) data + sizeof(FontDirectory.OffsetSubtable);
                var offset = offsetbase;

                for (int i = 0; i < fd.NumTables; i++)
                {
                    var td = new TableDirectory((void*) (offset));
                    offset += sizeof(TableDirectory.OffsetSubtable);

                    /*
                     * head - font header
                     * cmap - character to glyph mapping
                     * loca - index to location
                     * glyf - glyph data                     
                     * hhea - horizontal header
                     * hmtx - horizontal metrics                     
                     */
                    switch (td.Id)
                    {
                        case "head":
                            Head = new HeadTable((void*)(offsetbase + td.Offset));
                            Console.WriteLine(Head.Flags);
                            break;
                    }
                }
            }
        }
    }
}