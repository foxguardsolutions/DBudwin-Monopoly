using System.Collections.Generic;

namespace Monopoly
{
    using NUnit.Framework;

    [TestFixture]
    public class MonopolyGameTest
    {
        private readonly List<Player> players = new List<Player>()
        {
            new Player("Car", new RandomNumberGenerator()),
            new Player("Horse", new RandomNumberGenerator()),
            new Player("Thimble", new RandomNumberGenerator()),
            new Player("Dog", new RandomNumberGenerator()),
            new Player("Ship", new RandomNumberGenerator()),
            new Player("Top Hat", new RandomNumberGenerator()),
            new Player("Iron", new RandomNumberGenerator()),
            new Player("Wheelbarrow", new RandomNumberGenerator()),
            new Player("Shoe", new RandomNumberGenerator())
        };

        [TestCase(2, Result = 2, Description = "User Story: Test lower bound with Horse and Car players")]
        [TestCase(5, Result = 5)]
        [TestCase(8, Result = 8, Description = "Upper bound")]
        public int TestCreateMonopolyGame(int numberOfPlayers)
        {
            MonopolyGame game = new MonopolyGame(players.GetRange(0, numberOfPlayers));

            return game.Players.Count;
        }

        [TestCase(1, Description = "User Story: Try creating a game with less than 2 players")]
        [TestCase(9, Description = "User Story: Try creating a game with more than 8 players")]
        [ExpectedException(typeof(System.Exception))]
        public void TestCreateMonopolyGameIllegalNumberOfPlayersException(int numberOfPlayers)
        {
            MonopolyGame game = new MonopolyGame(players.GetRange(0, numberOfPlayers));
        }

        [TestCase(2, 20, Description = "User Story: Verify that each player played 20 rounds")]
        public void TestGameRoundsPlayed(int numberOfPlayers, int roundsToPlay)
        {
            MonopolyGame game = new MonopolyGame(players.GetRange(0, numberOfPlayers));

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();
            }

            game.Players.ForEach(p => Assert.AreEqual(p.RoundsPlayed, roundsToPlay));
        }

        [TestCase(20, Description = "User Story: Make sure the order of the players remains constant")]
        public void TestPlayerOrderIsConsistent(int roundsToPlayer)
        {
            MonopolyGame game = new MonopolyGame(players.GetRange(0, 2));

            Player player1 = game.Players[0];
            Player player2 = game.Players[1];

            for (int i = 0; i < roundsToPlayer; i++)
            {
                game.PlayRound();

                Assert.AreEqual(player1, game.Players[0]);
                Assert.AreEqual(player2, game.Players[1]);
            }
        }
    }
}
