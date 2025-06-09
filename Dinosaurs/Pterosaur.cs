using System;
using System.Collections.Generic;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;

namespace DinosaurSimulator.Dinosaurs
{
    /// <summary>
    /// Represents a Pterosaur dinosaur
    /// </summary>
    public class Pterosaur : Dinosaur
    {
        public Pterosaur(string name) : base(name, DinoType.Pterosaur) { }

        public override List<Food> GetPreferredFoods()
        {
            return new List<Food>
            {
                new Food("Fish", FoodType.Fish, 25, 5, -5),
                new Food("Insects", FoodType.Insects, 20, 10, -5),
                new Food("Sweet Nectar", FoodType.Special, 30, 20, -8)
            };
        }

        public override List<Toy> GetPreferredToys()
        {
            return new List<Toy>
            {
                new Toy("Flying Disc", ToyType.Ball, -5, 35, -15),
                new Toy("Wind Chimes", ToyType.Special, -2, 20, -5),
                new Toy("Cloud Hoop", ToyType.Special, -4, 40, -10)
            };
        }

        public override bool UpdateStats()
        {
            // Pterosaur stats decay moderately due to its active flying lifestyle
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