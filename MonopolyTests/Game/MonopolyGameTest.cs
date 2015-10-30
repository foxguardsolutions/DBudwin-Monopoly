using System.Linq;
using Monopoly.Game;
using Monopoly.Game.GamePlay;
using Monopoly.Game.Players;
using Monopoly.Random;
using Moq;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game
{
    [TestFixture]
    public class MonopolyGameTest
    {
        private IMonopolyGame game;
        private Mock<IRandomNumberGenerator> randMock;
        private IPlayer player;

        [SetUp]
        public void SetUp()
        {
            randMock = new Mock<IRandomNumberGenerator>();

            CreateGame("Car,Horse");

            player = game.Players.AllPlayers.First();
        }

        public void RollDiceMock(int die1, int die2)
        {
            randMock.SetupSequence(s => s.Generate(1, Dice.NUMBER_OF_SIDES)).Returns(die1).Returns(die2);
        }

        public void CreateGame(string args)
        {
            string[] names = args.Split(',');

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();
                game.Dice = kernel.Get<IDice>();
            }
        }

        [TestCase("Car,Horse", Result = 2, Description = "User Story: Test lower bound with Horse and Car players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship", Result = 5)]
        [TestCase("Car,Horse,Thimble,Dog,Ship,Top Hat,Iron,Wheelbarrow", Result = 8, Description = "User Story: Test upper bound with too many players")]
        public int TestCreateMonopolyGame(string args)
        {
            CreateGame(args);

            return game.Players.AllPlayers.Count();
        }

        [TestCase(Description = "User Story: Make sure the order of the players remains constant")]
        public void TestPlayerOrderIsConsistent()
        {
            IPlayer player1 = game.Players.AllPlayers.ElementAt(0);
            IPlayer player2 = game.Players.AllPlayers.ElementAt(1);

            for (int i = 0; i < 20; i++)
            {
                game.PlayRound();

                Assert.AreEqual(player1.Name, game.Players.AllPlayers.ElementAt(0).Name);
                Assert.AreEqual(player2.Name, game.Players.AllPlayers.ElementAt(1).Name);
            }
        }

        [TestCase(34, 10, Result = 480, Description = "Test passing Go and landing on income tax")]
        [TestCase(38, 2, Result = 600, Description = "Test landing on Go")]
        [TestCase(10, 8, Result = 220, Description = "Test not passing Go")]
        public int TestCheckIfPassGo(int position, int roll)
        {
            player.CurrentPosition = position;

            player.TakeTurn(roll);

            game.Manager.EvaluateBoardSpaceOutcome(player);

            return player.Cash;
        }

        [TestCase(35, 100, 3, Result = 25, Description = "Test landing on Luxury Tax")]
        public int TestPayLuxuryTax(int position, int cash, int roll)
        {
            player.CurrentPosition = position;
            player.Cash = cash;
            player.TakeTurn(roll);

            game.Manager.EvaluateBoardSpaceOutcome(player);

            return player.Cash;
        }

        [TestCase(0, 100, 4, Result = 80, Description = "Test Income Tax Penalty for 20%")]
        [TestCase(0, 1000, 4, Result = 800, Description = "Test Income Tax Penalty for 200 dollars")]
        public int TestPayIncomeTax(int position, int cash, int roll)
        {
            player.CurrentPosition = position;
            player.Cash = cash;
            player.TakeTurn(roll);

            game.Manager.EvaluateBoardSpaceOutcome(player);

            return player.Cash;
        }

        [Test]
        public void TestRollAgainIfDoubles()
        {
            Mock<IDice> mock = new Mock<IDice>();
            mock.SetupSequence(s => s.RollDie()).Returns(2).Returns(2);

            game.Dice = mock.Object;
            game.PlayerRollEvent(player);

            Assert.AreEqual(3, player.DiceOutcome.ConsecutiveDoublesRolled);
        }
    }
}