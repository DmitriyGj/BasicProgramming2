using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dictionaries_and_Lists_Action
{
    class Program
    {
        //75431
        static void Main()
        {
            DefaultSequnce();
            MySequnce();
            Console.ReadKey();
        }
        public static void DefaultSequnce()
        {
            Dictionary<int, int> SequnceCollector = new Dictionary<int, int>();
            var StopWatch = new Stopwatch();
            StopWatch.Start();
            int i = 0;
            while (i != 50000)
            {
                SequnceCollector.Add(i, 0);
                i++;
            }
            StopWatch.Stop();
            Console.WriteLine($"Complited {StopWatch.ElapsedMilliseconds} - ms");
        }
        public static void MySequnce()
        {
            Dictionary<int, int> SequnceCollector = new Dictionary<int, int>();
            var StopWatch = new Stopwatch();
            StopWatch.Start();
            int value = 1;
            int bucketResizeCount = 1;
            while(SequnceCollector.Keys.Count != 50000)
            {
                SequnceCollector[bucketResizeCount % 75431 ] = value;
                value++;
                if (value > bucketResizeCount) 
                { 
                    bucketResizeCount++; 
                    value = 0; 
                }
            }
            StopWatch.Stop();
            Console.WriteLine($"Complited {StopWatch.ElapsedMilliseconds} - ms");
        }
    }
}
