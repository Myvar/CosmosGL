using System;
using System.Drawing;
using CosmosGL.System.Fonts;
using CosmosGL.System.TrueType;

namespace WinDebug
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (var propertyInfo in typeof(Color).GetProperties())
            {
                if (propertyInfo.Name == "R") break;
                //Console.WriteLine(propertyInfo.Name);

                var color = (Color)propertyInfo.GetValue(null);
                Console.WriteLine("public static Color " + propertyInfo.Name + " { get; set; } = new Color(" + color.R + "," + color.G + ", " + color.B + ");");
            }

            /*
             var font = new TrueTypeFont(Cairo.CairoRegular_ttf);

             //font.DrawLine(screen, 0, 0, 300, 300, 0);


             //  var width = font.XMax - font.XMin;
             //  var height = font.YMax - font.YMin;
             float scale = 1f;

             font.DrawGlyph(null, 37, scale, scale, 0, 0);
             */
            Console.ReadKey();
        }
    }
}
