using System;
using System.Collections.Generic;

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

        public static void ShowUsage()
        {
            Console.WriteLine("To play Monopoly, enter a space delimited list of at least 2 player names followed by the number of rounds to play.");
        }

        public static void BeginGamePlay(string[] args)
        {
            RandomNumberGenerator generator = new RandomNumberGenerator();

            List<Player> players = new List<Player>();

            for (int i = 0; i < args.Length - 1; i++)
            {
                Player player = new Player(args[i], generator);

                players.Add(player);
            }

            int roundsToPlay = int.Parse(args[args.Length - 1]);

            MonopolyGame game = new MonopolyGame(players);

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();
            }

            Console.ReadLine();
        }
    }
}
