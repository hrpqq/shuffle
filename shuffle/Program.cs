using System;
using System.Threading.Tasks;
using shuffle.models;

namespace shuffle
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var round = GetRound();
            var gameContext = new GameContext(round);
            ThrowIfNotValid(gameContext);

            Console.WriteLine("Press enter to start game.");
            Console.ReadLine();

            var notificationSvc = new NotificationService();
            while (true)
            {
                Console.WriteLine($"---------------Round {gameContext.Round}------------------");
                Console.WriteLine("Shuffling");
                var game = gameContext.GetGame();
                Console.WriteLine("Shuffle complete");
                // Console.WriteLine(game);

                Console.WriteLine("Sending notification");
                await notificationSvc.Notify(game);
                Console.WriteLine("Notification sent, please confirm with players.");

                do
                {
                    Console.WriteLine("Enter 'next' for next round.");
                } while (Console.ReadLine() != "next");
            }
        }

        private static void ThrowIfNotValid(GameContext gameContext)
        {
            if (gameContext.IsValid(out var msg)) return;

            var originalColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.WriteLine($"Validation failed: {msg}");
            Console.BackgroundColor = originalColor;
            Console.ReadLine();
            throw new ApplicationException(msg);
        }

        private static int GetRound()
        {
            Console.WriteLine("What's current game round?");
            return int.TryParse(Console.ReadLine(), out var round)
                ? round
                : GetRound();
        }
    }
}