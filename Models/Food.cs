using DinosaurSimulator.Enums;

namespace DinosaurSimulator.Models
{
    /// <summary>
    /// Represents a food item that can be given to dinosaurs
    /// </summary>
    public class Food
    {
        public string Name { get; set; }
        public FoodType Type { get; set; }
        public int FeedEffect { get; set; }
        public int FunEffect { get; set; }
        public int SleepEffect { get; set; }

        public Food(string name, FoodType type, int feedEffect, int funEffect, int sleepEffect)
        {
            Name = name;
            Type = type;
            FeedEffect = feedEffect;
            FunEffect = funEffect;
            SleepEffect = sleepEffect;
        }
    }
} 