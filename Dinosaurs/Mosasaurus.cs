using System;
using System.Collections.Generic;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;

namespace DinosaurSimulator.Dinosaurs
{
    /// <summary>
    /// Represents a Mosasaurus dinosaur
    /// </summary>
    public class Mosasaurus : Dinosaur
    {
        public Mosasaurus(string name) : base(name, DinoType.Mosasaurus) { }

        public override List<Food> GetPreferredFoods()
        {
            return new List<Food>
            {
                new Food("Large Fish", FoodType.Fish, 35, 5, -5),
                new Food("Sea Creatures", FoodType.Special, 45, 10, -10),
                new Food("Crab Leg", FoodType.Special, 40, 15, -8)
            };
        }

        public override List<Toy> GetPreferredToys()
        {
            return new List<Toy>
            {
                new Toy("Water Ball", ToyType.WaterToy, -5, 30, -10),
                new Toy("Floating Ring", ToyType.Special, -3, 25, -5),
                new Toy("Wave Rider", ToyType.Special, -6, 35, -12)
            };
        }

        public override bool UpdateStats()
        {
            // Mosasaurus stats decay slowly due to its aquatic lifestyle
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