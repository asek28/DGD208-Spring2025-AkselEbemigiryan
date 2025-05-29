using System;
using System.Collections.Generic;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;

namespace DinosaurSimulator.Models
{
    /// <summary>
    /// Base abstract class for all dinosaur types
    /// </summary>
    public abstract class Dinosaur
    {
        private int feed = 50;
        private int fun = 50;
        private int sleep = 50;

        public string Name { get; set; }
        public DinoType Type { get; set; }
        public bool IsAlive { get; private set; } = true;

        public int Feed 
        { 
            get => feed;
            set => feed = Math.Max(0, Math.Min(100, value));
        }

        public int Fun 
        { 
            get => fun;
            set => fun = Math.Max(0, Math.Min(100, value));
        }

        public int Sleep 
        { 
            get => sleep;
            set => sleep = Math.Max(0, Math.Min(100, value));
        }

        protected Dinosaur(string name, DinoType type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets the list of preferred foods for this dinosaur type
        /// </summary>
        public abstract List<Food> GetPreferredFoods();

        /// <summary>
        /// Gets the list of preferred toys for this dinosaur type
        /// </summary>
        public abstract List<Toy> GetPreferredToys();

        /// <summary>
        /// Updates the dinosaur's stats over time
        /// </summary>
        public abstract void UpdateStats();

        /// <summary>
        /// Updates a stat value and checks for death
        /// </summary>
        public void UpdateStat(int statChange, StatType statType)
        {
            switch (statType)
            {
                case StatType.Feed:
                    Feed += statChange;
                    break;
                case StatType.Fun:
                    Fun += statChange;
                    break;
                case StatType.Sleep:
                    Sleep += statChange;
                    break;
            }
            CheckForDeath();
        }

        /// <summary>
        /// Checks if the dinosaur has died due to low stats
        /// </summary>
        private void CheckForDeath()
        {
            if (Feed <= 0 || Fun <= 0 || Sleep <= 0)
            {
                IsAlive = false;
                Console.WriteLine($"\n{Name} the {Type} has died due to neglect!");
                Console.WriteLine($"Final Stats - Feed: {Feed}, Fun: {Fun}, Sleep: {Sleep}");
            }
        }

        /// <summary>
        /// Displays the dinosaur's stats as visual bars
        /// </summary>
        public void DisplayStats()
        {
            Console.WriteLine($"\n{Name} the {Type}:");
            Console.WriteLine($"Feed:  {GetStatBar(Feed)} {Feed}/100");
            Console.WriteLine($"Fun:   {GetStatBar(Fun)} {Fun}/100");
            Console.WriteLine($"Sleep: {GetStatBar(Sleep)} {Sleep}/100");
        }

        /// <summary>
        /// Creates a visual bar representation of a stat value
        /// </summary>
        private string GetStatBar(int value)
        {
            int barLength = 10;
            int filledBlocks = (int)Math.Ceiling((double)value / 100 * barLength);
            return $"[{new string('█', filledBlocks)}{new string(' ', barLength - filledBlocks)}]";
        }
    }

    /// <summary>
    /// Represents the different types of stats that can be updated
    /// </summary>
    public enum StatType
    {
        Feed,
        Fun,
        Sleep
    }
} 