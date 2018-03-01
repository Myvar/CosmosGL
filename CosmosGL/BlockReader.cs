using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CosmosGL
{
    public static class BlockReader
    {
        public static byte[] ReadBytes(string filePath)
        {
            var re = new List<byte>();

            var chunckCount = File.ReadAllBytes(filePath)[0];

            var basePath = filePath.Replace(".", "");

            for (int i = 0; i < chunckCount; i++)
            {
                var path = basePath + "\\" + i;
                var x = File.ReadAllBytes(path);

                foreach (var b in x)
                {
                    re.Add(b);
                }
            }

            return re.ToArray();
        }
    }
}
