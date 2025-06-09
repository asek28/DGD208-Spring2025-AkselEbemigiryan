using System;
using System.Collections.Generic;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;

namespace DinosaurSimulator.Dinosaurs
{
    /// <summary>
    /// Represents a Tyrannosaurus Rex dinosaur
    /// </summary>
    public class TyrannosaurusRex : Dinosaur
    {
        public TyrannosaurusRex(string name) : base(name, DinoType.TyrannosaurusRex) { }

        public override List<Food> GetPreferredFoods()
        {
            return new List<Food>
            {
                new Food("Raw Meat", FoodType.Meat, 30, -5, -5),
                new Food("Special Dino Steak", FoodType.Special, 50, 10, -10),
                new Food("Steamed Raptor Ribs", FoodType.Special, 45, 15, -15)
            };
        }

        public override List<Toy> GetPreferredToys()
        {
            return new List<Toy>
            {
                new Toy("Bone Rope", ToyType.Rope, -10, 30, -15),
                new Toy("Dino Ball", ToyType.Ball, -5, 25, -10),
                new Toy("Rolling Barrel", ToyType.Special, -8, 35, -12)
            };
        }

        public override bool UpdateStats()
        {
            // Tyrannosaurus Rex stats decay faster due to its size and energy needs
            if (UpdateStat(-2, StatType.Feed) ||
                UpdateStat(-2, StatType.Fun) ||
                UpdateStat(-1, StatType.Sleep))
            {
                return true;
            }
            return false;
        }
    }
} 