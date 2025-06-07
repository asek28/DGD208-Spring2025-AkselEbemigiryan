using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using DinosaurSimulator.Enums;
using DinosaurSimulator.Models;
using DinosaurSimulator.Dinosaurs;

namespace DinosaurSimulator.Managers
{
    /// <summary>
    /// Manages the game state and user interactions
    /// </summary>
    public class GameManager
    {
        private readonly List<Dinosaur> dinosaurs = new List<Dinosaur>();
        private bool isGameRunning = true;
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private const string CREATOR_NAME = "Aksel Ebemigiryan";
        private const string STUDENT_NUMBER = "225040094";
        private const string GAME_NAME = "DinosaurSimulator";

        public async Task RunGame()
        {
            Console.WriteLine("Welcome to Dinosaur Caretaking Simulator!");
            Console.WriteLine("Let's see if you have what it takes to care for these prehistoric creatures!");

            // Start the stat decay background task
            _ = Task.Run(() => StatDecayLoop(cancellationTokenSource.Token));

            while (isGameRunning)
            {
                DisplayMainMenu();
                var choice = GetUserChoice();
                await ProcessMenuChoice(choice);
            }

            // Clean up
            cancellationTokenSource.Cancel();
        }

        private async Task StatDecayLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var dino in dinosaurs.Where(d => d.IsAlive))
                {
                    dino.UpdateStats();
                }

                // Sleep stat decreases every 3.5 seconds (was 5)
                await Task.Delay(3500, cancellationToken);
                foreach (var dino in dinosaurs.Where(d => d.IsAlive))
                {
                    dino.UpdateStat(-1, StatType.Sleep);
                }

                // Feed and Fun decrease every 2.25 seconds (was 2.5)
                await Task.Delay(2250, cancellationToken);
                foreach (var dino in dinosaurs.Where(d => d.IsAlive))
                {
                    dino.UpdateStat(-1, StatType.Feed);
                    dino.UpdateStat(-1, StatType.Fun);
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Adopt a Dinosaur");
            Console.WriteLine("2. View Dinosaur Stats");
            Console.WriteLine("3. Interact with a Dinosaur");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Tutorial");
            Console.WriteLine("0. Exit Game");
            Console.Write("\nChoose an option by numbers: ");
        }

        private MenuOption GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return (MenuOption)choice;
            }
            return MenuOption.Exit;
        }

        private async Task ProcessMenuChoice(MenuOption choice)
        {
            switch (choice)
            {
                case MenuOption.Exit:
                    isGameRunning = false;
                    Console.WriteLine("Remember to respect and take care of animals!");
                    break;

                case MenuOption.AdoptDinosaur:
                    await AdoptDinosaur();
                    break;

                case MenuOption.ViewStats:
                    DisplayDinosaurStats();
                    break;

                case MenuOption.InteractWithDinosaur:
                    await InteractWithDinosaur();
                    break;

                case MenuOption.Credits:
                    DisplayCredits();
                    break;

                case MenuOption.Tutorial:
                    DisplayTutorial();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again:");
                    break;
            }
        }

        private async Task AdoptDinosaur()
        {
            Console.WriteLine("\nChoose a dinosaur to adopt:");
            Console.WriteLine("1. Tyrannosaurus Rex");
            Console.WriteLine("2. Pterosaur");
            Console.WriteLine("3. Mosasaurus");
            Console.WriteLine("4. Triceratops");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
            {
                Console.Write("Enter a name for your dinosaur: ");
                string name = Console.ReadLine();

                Dinosaur dinosaur = choice switch
                {
                    1 => new TyrannosaurusRex(name),
                    2 => new Pterosaur(name),
                    3 => new Mosasaurus(name),
                    4 => new Triceratops(name),
                    _ => null
                };

                if (dinosaur != null)
                {
                    dinosaurs.Add(dinosaur);
                    Console.WriteLine($"\nCongratulations! You've adopted {dinosaur.Name} the {dinosaur.Type}!");
                    
                    // Show ASCII art based on dinosaur type
                    switch (dinosaur.Type)
                    {
                        case DinoType.TyrannosaurusRex:
                            Console.WriteLine(AsciiArt.GetTRex());
                            break;
                        case DinoType.Pterosaur:
                            Console.WriteLine(AsciiArt.GetPterosaur());
                            break;
                        case DinoType.Triceratops:
                            Console.WriteLine(AsciiArt.GetTriceratops());
                            break;
                        case DinoType.Mosasaurus:
                            Console.WriteLine(AsciiArt.GetMosasaurus());
                            break;
                    }
                    
                    dinosaur.DisplayStats();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private void DisplayDinosaurStats()
        {
            if (!dinosaurs.Any())
            {
                Console.WriteLine("\nYou haven't adopted any dinosaurs yet!");
                return;
            }

            foreach (var dino in dinosaurs)
            {
                dino.DisplayStats();
            }
        }

        private async Task InteractWithDinosaur()
        {
            var aliveDinosaurs = dinosaurs.Where(d => d.IsAlive).ToList();
            if (!aliveDinosaurs.Any())
            {
                Console.WriteLine("\nYou don't have any living dinosaurs to interact with!");
                return;
            }

            Console.WriteLine("\nChoose a dinosaur to interact with:");
            for (int i = 0; i < aliveDinosaurs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {aliveDinosaurs[i].Name} the {aliveDinosaurs[i].Type}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= aliveDinosaurs.Count)
            {
                var selectedDino = aliveDinosaurs[choice - 1];
                await InteractWithSelectedDinosaur(selectedDino);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private async Task InteractWithSelectedDinosaur(Dinosaur dino)
        {
            if (!dino.IsAlive)
            {
                Console.WriteLine($"\n{dino.Name} is no longer with us...");
                return;
            }

            Console.WriteLine($"\nWhat would you like to do with {dino.Name}?");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Feed");
            Console.WriteLine("3. Put to Sleep");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await PlayWithDinosaur(dino);
                        break;
                    case 2:
                        await FeedDinosaur(dino);
                        break;
                    case 3:
                        await PutDinosaurToSleep(dino);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (dino.IsAlive)
                {
                    dino.DisplayStats();
                }
            }
        }

        private async Task PlayWithDinosaur(Dinosaur dino)
        {
            if (!dino.IsAlive) return;

            var toys = dino.GetPreferredToys();
            Console.WriteLine("\nChoose a toy:");
            for (int i = 0; i < toys.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {toys[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= toys.Count)
            {
                var selectedToy = toys[choice - 1];
                Console.WriteLine($"\nPlaying with {dino.Name} using {selectedToy.Name}...");
                await Task.Delay(2000);

                dino.UpdateStat(selectedToy.FeedEffect, StatType.Feed);
                dino.UpdateStat(selectedToy.FunEffect, StatType.Fun);
                dino.UpdateStat(selectedToy.SleepEffect, StatType.Sleep);

                if (dino.IsAlive)
                {
                    Console.WriteLine($"{dino.Name} had fun playing!");
                }
            }
        }

        private async Task FeedDinosaur(Dinosaur dino)
        {
            if (!dino.IsAlive) return;

            var foods = dino.GetPreferredFoods();
            Console.WriteLine("\nChoose food:");
            for (int i = 0; i < foods.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {foods[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= foods.Count)
            {
                var selectedFood = foods[choice - 1];
                string foodEmoji = selectedFood.Type switch
                {
                    FoodType.Meat => "🥩",
                    FoodType.Fish => "🐟",
                    FoodType.Plants => "🌿",
                    FoodType.Insects => "🪲",
                    FoodType.Special => "⭐",
                    _ => "🍖"
                };

                EatAnimation.ShowEatingDino(dino.Name, foodEmoji, selectedFood.Name);

                dino.UpdateStat(selectedFood.FeedEffect, StatType.Feed);
                dino.UpdateStat(selectedFood.FunEffect, StatType.Fun);
                dino.UpdateStat(selectedFood.SleepEffect, StatType.Sleep);

                if (!dino.IsAlive)
                {
                    Console.WriteLine($"{dino.Name} couldn't handle the food...");
                }
            }
        }

        private async Task PutDinosaurToSleep(Dinosaur dino)
        {
            if (!dino.IsAlive) return;

            SleepAnimation.ShowSleepingDino(dino.Name);
            dino.UpdateStat(100 - dino.Sleep, StatType.Sleep);
            
            // Dinosaur wakes up hungry
            Random random = new Random();
            int hungerIncrease = random.Next(10, 16); // Random value between 10-15
            dino.UpdateStat(-hungerIncrease, StatType.Feed);
            Console.WriteLine($"{dino.Name} wakes up feeling a bit hungry...");
            Thread.Sleep(2000);
        }

        private void DisplayCredits()
        {
            Console.WriteLine($"\nGame: {GAME_NAME}");
            Console.WriteLine($"Creator: {CREATOR_NAME}");
            Console.WriteLine($"Student Number: {STUDENT_NUMBER}");
        }

        private void DisplayTutorial()
        {
            Console.WriteLine("\n=== Dinosaur Care Tutorial ===");
            Console.WriteLine("Welcome to the Dinosaur Caretaking Simulator! Here's what you need to know:");
            Console.WriteLine("\n1. Your main goal is to take care of your adopted dinosaurs by managing their Feed, Fun, and Sleep stats.");
            Console.WriteLine("2. Each dinosaur has unique foods and toys that affect their stats differently - some items give bigger boosts!");
            Console.WriteLine("3. Be careful when playing with your dinosaur - activities will reduce their Sleep stat.");
            Console.WriteLine("4. Keep an eye on all stats - if any of them drops to 0, your dinosaur will not survive!");
            Console.WriteLine("5. It is recommended to play the game in Full Screen mode for the best experience!");
            Console.WriteLine("6. Remember that dinosaurs might get hungry after waking up from sleep!");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
} 