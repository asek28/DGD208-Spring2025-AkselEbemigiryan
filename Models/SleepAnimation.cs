using System;
using System.Threading;

namespace DinosaurSimulator.Models
{
    public static class SleepAnimation
    {
        public static void ShowSleepingDino(string dinoName)
        {
            Console.Clear();
            Console.WriteLine($"\n{dinoName} is getting sleepy...");
            Thread.Sleep(1000);

            string[] zzzFrames = new string[]
            {
                @"
                Z Z Z",
                @"
              Z Z Z",
                @"
            Z Z Z",
                @"
          Z Z Z"
            };

            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                Console.WriteLine(zzzFrames[i % zzzFrames.Length]);
                Thread.Sleep(500);
            }

            Console.Clear();
            Console.WriteLine($@"
            💤 Z Z Z 💤

{dinoName} is now sleeping peacefully... 💤");
            Thread.Sleep(2000);
        }
    }
} 