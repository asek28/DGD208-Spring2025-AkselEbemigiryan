using System;

namespace DinosaurSimulator.Models
{
    public static class GameOverAnimation
    {
        public static void ShowGameOver(string dinoName)
        {
            try
            {
                Console.Clear();
                
                // Center the gravestone
                Console.WriteLine("\n\n");
                Console.WriteLine("           .-\"\"\"\"\"-.");
                Console.WriteLine("         .'  RIP  '.");
                Console.WriteLine($"        /    {dinoName.PadRight(9)}\\");
                Console.WriteLine("       |             |");
                Console.WriteLine("       |    _____    |");
                Console.WriteLine("       |   /     \\   |");
                Console.WriteLine("       |   \\_____/   |");
                Console.WriteLine("       |             |");
                Console.WriteLine("        \\           /");
                Console.WriteLine("         '.       .'");
                Console.WriteLine("           '-...-'\n");
                Console.WriteLine("      Press any key to exit...");
                
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GameOverAnimation: {ex.Message}");
            }
        }
    }
} 