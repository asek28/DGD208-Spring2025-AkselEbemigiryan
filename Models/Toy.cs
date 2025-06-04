using DinosaurSimulator.Enums;

namespace DinosaurSimulator.Models
{
    /// <summary>
    /// Represents a toy that can be used to play with dinosaurs
    /// </summary>
    public class Toy
    {
        public string Name { get; set; }
        public ToyType Type { get; set; }
        public int FeedEffect { get; set; }
        public int FunEffect { get; set; }
        public int SleepEffect { get; set; }

        public Toy(string name, ToyType type, int feedEffect, int funEffect, int sleepEffect)
        {
            Name = name;
            Type = type;
            FunEffect = funEffect;
            FeedEffect = feedEffect;
            SleepEffect = sleepEffect;
        }
    }
} 