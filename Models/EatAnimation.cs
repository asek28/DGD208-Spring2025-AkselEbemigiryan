using System;
using System.Threading;

namespace DinosaurSimulator.Models
{
    public static class EatAnimation
    {
        public static void ShowEatingDino(string dinoName, string foodEmoji, string foodName)
        {
            Console.Clear();
            Console.WriteLine($"\n{dinoName} is getting hungry... 🍽️");
            Thread.Sleep(1000);

            string[] nomFrames = new string[]
            {
                @"
                nom nom",
                @"
              nom   nom",
                @"
            nom       nom",
                @"
          nom           nom",
                @"
        nom   🦖   nom",
                @"
      nom     🦖     nom",
                @"
    nom       🦖       nom",
                @"
  nom         🦖         nom"
            };

            for (int i = 0; i < 12; i++)
            {
                Console.Clear();
                Console.WriteLine($"\n{foodEmoji}");
                Console.WriteLine(nomFrames[i % nomFrames.Length]);
                Thread.Sleep(400);
            }

            Console.Clear();
            Console.WriteLine($@"

😋 {dinoName} enjoyed the {foodName}! {foodEmoji}");
            Thread.Sleep(2000);
        }
    }
} 