using System;
using Monopoly.Game;
using Monopoly.Game.Bank;
using Monopoly.Game.GamePlay;
using Ninject;

namespace Monopoly
{
    public static class Program
    {
        public const int ROUNDS_TO_PLAY = 20;

        public static void Main(string[] args)
        {
            using (var kernel = new StandardKernel(new MonopolyBindings(args)))
            {
                MonopolyGame game = kernel.Get<MonopolyGame>();

                game.Banker = kernel.Get<Banker>();
                game.Dice = kernel.Get<Dice>();

                BeginGamePlay(game);
            }
        }

        private static void BeginGamePlay(MonopolyGame game)
        {
            RunGame(game);

            Console.ReadLine();
        }

        private static void RunGame(MonopolyGame game)
        {
            for (int i = 0; i < ROUNDS_TO_PLAY; i++)
            {
                game.PlayRound();
            }
        }
    }
}
