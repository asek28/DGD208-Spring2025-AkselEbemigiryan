using System;
using System.Threading.Tasks; // For async operations


namespace TamagotchiSimulator
{
    public enum MenuOption
    {
        Exit = 0,
        CheckStatus = 1,
        Feed = 2,
        Play = 3,
        Sleep = 4,
        Credits = 5
    }

    public class Program
    {
        private static bool isGameRunning = true;
        private const string CREATOR_NAME = "Aksel Ebemigiryan";
        private const string STUDENT_NUMBER = "225040094";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Tamagotchi Simulator");
            Console.WriteLine("Let's see if you have the responsibility to adopt a pet");

            while (isGameRunning)
            {
                DisplayMenu();
                var choice = GetUserChoice();
                await ProcessMenuChoice(choice);
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Check Your Pets Status");
            Console.WriteLine("2. Feed Your Pet");
            Console.WriteLine("3. Play with Your Pet");
            Console.WriteLine("4. Put Your Pet to Sleep");
            Console.WriteLine("5. Credits");
            Console.WriteLine("0. Exit Game");
            Console.Write("\nChoose an option by numbers: ");
        }

        private static MenuOption GetUserChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return (MenuOption)choice;
            }
            return MenuOption.Exit;
        }

        private static async Task ProcessMenuChoice(MenuOption choice)
        {
            switch (choice)
            {
                case MenuOption.Exit:
                    isGameRunning = false;
                    Console.WriteLine("Remember to respect and take care of animals!!");
                    break;

                case MenuOption.Credits:
                    Credits();
                    break;

                case MenuOption.Feed:
                    await FeedPet();
                    break;

                case MenuOption.Play:
                    await PlayWithPet();
                    break;

                case MenuOption.Sleep:
                    await PutPetToSleep();
                    break;

                case MenuOption.CheckStatus:
                    await CheckPetStatus();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again:");
                    break;
            }
        }

        private static void Credits()
        {
            Console.WriteLine($"\nCreator: {CREATOR_NAME}");
            Console.WriteLine($"Student Number: {STUDENT_NUMBER}");
        }

        private static async Task FeedPet()
        {
            Console.WriteLine("\nFeeding your pet...");
            await Task.Delay(2000); // Simulating feeding action
            Console.WriteLine("Your pet has been fed!");
        }

        private static async Task PlayWithPet()
        {
            Console.WriteLine("\nPlaying with your pet...");
            await Task.Delay(2000); // Simulating play action
            Console.WriteLine("Your pet had fun playing!");
        }

        private static async Task PutPetToSleep()
        {
            Console.WriteLine("\nPutting your pet to sleep...");
            await Task.Delay(2000); // Simulating sleep action
            Console.WriteLine("Your pet is now sleeping!");
        }

        private static async Task CheckPetStatus()
        {
            Console.WriteLine("\nChecking your pets status...");
            await Task.Delay(1000); // Simulating status check
            Console.WriteLine("Your pet is doing well!");
        }
    }
}
