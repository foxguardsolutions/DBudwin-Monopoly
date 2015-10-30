using System;

namespace Monopoly
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                ShowUsage();
            }
            else
            {
                BeginGamePlay(args);
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("To play Monopoly, enter a space delimited list of at least 2 player names followed by the number of rounds to play.");
        }

        private static void BeginGamePlay(string[] args)
        {
            MonopolyGame game = new MonopolyGame(args);

            RunGame(game);

            Console.ReadLine();
        }

        private static void RunGame(MonopolyGame game)
        {
            for (int i = 0; i < game.RoundsToPlay; i++)
            {
                game.PlayRound();
            }
        }
    }
}
