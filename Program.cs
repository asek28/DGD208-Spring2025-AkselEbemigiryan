using System.Threading.Tasks;
using DinosaurSimulator.Managers;

namespace DinosaurSimulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var gameManager = new GameManager();
            await gameManager.RunGame();
        }
    }
}
