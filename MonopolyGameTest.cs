using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    using NUnit.Framework;

    [TestFixture]
    public class MonopolyGameTest
    {
        private IEnumerable<IPlayer> players;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse", "Thimble", "Dog", "Ship", "Top Hat", "Iron", "Wheelbarrow", "Shoe" };

            PlayerFactory factory = new PlayerFactory(names);

            players = factory.CreateAll(new RandomNumberGenerator());
        }

        [TestCase(2, Result = 2, Description = "User Story: Test lower bound with Horse and Car players")]
        [TestCase(5, Result = 5)]
        [TestCase(8, Result = 8, Description = "User Story: Test upper bound with too many players")]
        public int TestCreateMonopolyGame(int numberOfPlayers)
        {
            MonopolyGame game = new MonopolyGame(players.ToList().GetRange(0, numberOfPlayers), 1);

            return game.Players.Count;
        }

        [TestCase(1, Description = "User Story: Try creating a game with less than 2 players")]
        [TestCase(9, Description = "User Story: Try creating a game with more than 8 players")]
        [ExpectedException(typeof(System.Exception))]
        public void TestCreateMonopolyGameIllegalNumberOfPlayersException(int numberOfPlayers)
        {
            MonopolyGame game = new MonopolyGame(players.ToList().GetRange(0, numberOfPlayers), 1);
        }

        [TestCase(2, 20, Description = "User Story: Verify that each player played 20 rounds")]
        public void TestGameRoundsPlayed(int numberOfPlayers, int roundsToPlay)
        {
            MonopolyGame game = new MonopolyGame(players.ToList().GetRange(0, numberOfPlayers), 1);

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();
            }

            game.Players.ForEach(p => Assert.AreEqual(p.RoundsPlayed, roundsToPlay));
        }

        [TestCase(20, Description = "User Story: Make sure the order of the players remains constant")]
        public void TestPlayerOrderIsConsistent(int roundsToPlayer)
        {
            MonopolyGame game = new MonopolyGame(players.ToList().GetRange(0, 2), 1);

            IPlayer player1 = game.Players[0];
            IPlayer player2 = game.Players[1];

            for (int i = 0; i < roundsToPlayer; i++)
            {
                game.PlayRound();

                Assert.AreEqual(player1, game.Players[0]);
                Assert.AreEqual(player2, game.Players[1]);
            }
        }

        [Test]
        public void TestGeneratePlayerListFromArgs()
        {
            MonopolyGame game = new MonopolyGame(new string[] { "Car", "Horse", "20" });

            Assert.AreEqual(game.Players.Count, 2);
        }
    }
}
