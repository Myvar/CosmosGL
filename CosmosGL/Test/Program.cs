using System;
using System.Collections.Generic;
using System.IO;
using CosmosGL;
using CosmosGL.TrueType;

namespace Test
{

    
    class Program
    {

      

        static void Main(string[] args)
        {
            var f = new Font(File.ReadAllBytes("Karla-Regular.ttf"));
            var g = f.Glyphs[(byte)'#'];

            foreach (var gContourEnd in g.ContourEnds)
            {
                Console.WriteLine(gContourEnd);
            }

            Console.WriteLine(g.Points.Count);
            ;
            Console.ReadKey();
        }
    }
}
