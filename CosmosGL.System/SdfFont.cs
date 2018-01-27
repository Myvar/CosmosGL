using System;
using System.Collections.Generic;
using System.Text;
using CosmosGL.System.Imaging;

namespace CosmosGL.System
{
    public class SdfChar
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Xoffset { get; set; }
        public int Yoffset { get; set; }
        public int Xadvance { get; set; }

        public SdfChar(string s)
        {
            var segs = s.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            Id = int.Parse(segs[1].Split('=')[1].Trim());
            X = int.Parse(segs[2].Split('=')[1].Trim());
            Y = int.Parse(segs[3].Split('=')[1].Trim());
            Width = int.Parse(segs[4].Split('=')[1].Trim());
            Height = int.Parse(segs[5].Split('=')[1].Trim());
            Xoffset = int.Parse(segs[6].Split('=')[1].Trim());
            Yoffset = int.Parse(segs[7].Split('=')[1].Trim());
            Xadvance = int.Parse(segs[8].Split('=')[1].Trim());
        }
    }

    public class SdfFont
    {
        public Image AtlasImage { get; set; }
        public List<SdfChar> Chars { get; set; } = new List<SdfChar>();

        public SdfFont(string fnt, Image atlasImage)
        {
            AtlasImage = atlasImage;
            foreach (var line in fnt.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!line.StartsWith("chars") && line.StartsWith("char"))
                {
                    Chars.Add(new SdfChar(line));
                }
            }
        }

        public SdfChar GetChar(char c)
        {

            foreach (var chr in Chars)
            {
                if (chr.Id == (byte) c)
                {
                    return chr;
                }
            }

            return null;
        }
    }
}