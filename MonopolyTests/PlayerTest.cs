using Monopoly.Game;
using Monopoly.Random;

namespace MonopolyTests
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PlayerTest
    {
        private Mock<IRandomNumberGenerator> randMock;
        private Player player;

        [SetUp]
        public void SetUp()
        {
            randMock = new Mock<IRandomNumberGenerator>();
            player = new Player("Car", randMock.Object);
        }

        [TestCase(2, Result = 2)]
        [TestCase(4, Result = 4)]
        [TestCase(12, Result = 12)]
        public int TestRollDie(int roll)
        {
            Mock<IPlayer> mock = new Mock<IPlayer>();
            mock.Setup(s => s.RollDie()).Returns(roll);

            return mock.Object.RollDie();
        }

        [TestCase(0, 7, Result = 7, Description = "User Story: Test from starting spot")]
        [TestCase(39, 6, Result = 5, Description = "User Story: Test pass Go")]
        [TestCase(14, 7, Result = 21, Description = "Test from middle of board")]
        public int TestTakeTurn(int initialPosition, int roll)
        {
            player.CurrentPosition = initialPosition;
            player.TakeTurn(roll);

            return player.CurrentPosition;
        }

        [TestCase(7, Result = 10, Description = "Player in jail, unsuccessful roll to leave jail")]
        public int TestTakeTurnPlayerIncarcerated(int roll)
        {
            player.CurrentPosition = (int)BoardSpace.SpaceKeys.Jail;
            player.IsIncarcerated = true;
            player.TakeTurn(roll);

            return player.CurrentPosition;
        }

        [TestCase(1, Result = 1)]
        [TestCase(20, Result = 20)]
        public int TestRoundsPlayed(int roundsToPlay)
        {
            for (int i = 0; i < roundsToPlay; i++)
            {
                player.TakeTurn(1);
            }

            return player.RoundsPlayed;
        }

        [TestCase(3, 3, Result = 1)]
        [TestCase(2, 3, Result = 0)]
        public int TestCheckForDoubles(int die1, int die2)
        {
            player.CheckForDoubles(die1, die2);

            return player.DoublesCounter;
        }
    }
}
