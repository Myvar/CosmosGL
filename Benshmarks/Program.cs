using System;
using System.Diagnostics;

namespace Benshmarks
{
    class Program
    {
        public static void TimeTest(string name, int times, Action s)
        {
            //warmup
            for (int i = 0; i < times; i++)
            {
                s();
            }

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < times; i++)
            {
                s();
            }

            sw.Stop();

            Console.WriteLine($"{name}[{times}]: Sec:{sw.Elapsed.Seconds}, Ms: {sw.ElapsedMilliseconds}, Ticks: {sw.Elapsed.Ticks}");

            

        }

        static void Main(string[] args)
        {

            Blending.TimeTest();

            Console.ReadKey();
        }
    }
}
