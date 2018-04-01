using System;
using System.IO;
using CosmosGL.TrueType;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new Font(File.ReadAllBytes("Karla-Regular.ttf"));
        }
    }
}
