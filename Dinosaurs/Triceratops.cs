using System;
using System.Collections.Generic;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;

namespace DinosaurSimulator.Dinosaurs
{
    /// <summary>
    /// Represents a Triceratops dinosaur
    /// </summary>
    public class Triceratops : Dinosaur
    {
        public Triceratops(string name) : base(name, DinoType.Triceratops) { }

        public override List<Food> GetPreferredFoods()
        {
            return new List<Food>
            {
                new Food("Plants", FoodType.Plants, 25, 5, -5),
                new Food("Special Herbs", FoodType.Special, 35, 10, -5),
                new Food("Berry Cluster", FoodType.Special, 30, 20, -8)
            };
        }

        public override List<Toy> GetPreferredToys()
        {
            return new List<Toy>
            {
                new Toy("Horn Ring", ToyType.Rope, -5, 30, -10),
                new Toy("Plant Puzzle", ToyType.Puzzle, -3, 25, -5),
                new Toy("Horn Rattle", ToyType.Special, -4, 35, -8)
            };
        }

        public override bool UpdateStats()
        {
            // Triceratops stats decay moderately due to its herbivorous nature
            if (UpdateStat(-1, StatType.Feed) ||
                UpdateStat(-1, StatType.Fun) ||
                UpdateStat(-1, StatType.Sleep))
            {
                return true;
            }
            return false;
        }
    }
} 