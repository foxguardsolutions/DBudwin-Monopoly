using System.Linq;
using Monopoly.Game;
using Ninject;

namespace MonopolyTests
{
    using NUnit.Framework;

    [TestFixture]
    public class MonopolyGameTest
    {
        private int roundsToPlay;
        private IMonopolyGame game;

        public void CreateGame(string args)
        {            
            string[] names = args.Split(',');

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();

                roundsToPlay = 20;
            }
        }

        [TestCase("Car,Horse", Result = 2, Description = "User Story: Test lower bound with Horse and Car players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship", Result = 5)]
        [TestCase("Car,Horse,Thimble,Dog,Ship,Top Hat,Iron,Wheelbarrow", Result = 8, Description = "User Story: Test upper bound with too many players")]
        public int TestCreateMonopolyGame(string args)
        {
            CreateGame(args);

            return game.Players.Count();
        }

        [TestCase("Car,Horse", Description = "User Story: Verify that each player played 20 rounds")]
        public void TestGameRoundsPlayed(string args)
        {
            CreateGame(args);

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();
            }

            game.Players.ToList().ForEach(p => Assert.AreEqual(p.RoundsPlayed, roundsToPlay));
        }

        [TestCase("Car,Horse", Description = "User Story: Make sure the order of the players remains constant")]
        public void TestPlayerOrderIsConsistent(string args)
        {
            CreateGame(args);

            IPlayer player1 = game.Players.ElementAt(0);
            IPlayer player2 = game.Players.ElementAt(1);

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();

                Assert.AreEqual(player1, game.Players.ElementAt(0));
                Assert.AreEqual(player2, game.Players.ElementAt(1));
            }
        }

        [TestCase("Car,Horse", 34, 8, Result = 200, Description = "Test passing Go")]
        [TestCase("Car,Horse", 38, 2, Result = 200, Description = "Test landing on Go")]
        [TestCase("Car,Horse", 10, 8, Result = 0, Description = "Test not passing Go")]
        public int TestCheckIfPassGo(string args, int position, int roll)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.CurrentPosition = position;

            player.TakeTurn(roll);

            game.EvaluateTurnOutcome(player);

            return player.Cash;
        }

        [TestCase("Car,Horse", 0, 100, 4, Result = 80, Description = "Test Income Tax Penalty for 20%")]
        [TestCase("Car,Horse", 0, 1000, 4, Result = 800, Description = "Test Income Tax Penalty for 200 dollars")]
        public int TestPayIncomeTax(string args, int position, int cash, int roll)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.CurrentPosition = position;
            player.Cash = cash;
            player.TakeTurn(roll);

            game.EvaluateTurnOutcome(player);

            return player.Cash;
        }
    }
}