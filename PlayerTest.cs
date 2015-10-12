namespace Monopoly
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PlayerTest
    {
        [TestCase(2, Result = 2)]
        [TestCase(4, Result = 4)]
        [TestCase(12, Result = 12)]
        public int TestRollDie(int roll)
        {
            Mock<IPlayer> mock = new Mock<IPlayer>();
            mock.Setup(s => s.RollDie()).Returns(roll);

            return mock.Object.RollDie();
        }

        [TestCase(0, 3, 4, Result = 7, Description = "User Story: Test from starting spot")]
        [TestCase(39, 3, 3, Result = 5, Description = "User Story: Test pass Go")]
        [TestCase(14, 1, 6, Result = 21, Description = "Test from middle of board")]
        public int TestTakeTurn(int initialPosition, int die1, int die2)
        {
            Mock<IRandomNumberGenerator> randMock = new Mock<IRandomNumberGenerator>();
            randMock.SetupSequence(s => s.Generate(1, MonopolyGame.NUMBER_OF_SIDES)).Returns(die1).Returns(die2);

            Player player = new Player("Car", randMock.Object) { CurrentPosition = initialPosition };

            player.TakeTurn();

            return player.CurrentPosition;
        }

        [TestCase(1, Result = 1)]
        [TestCase(20, Result = 20)]
        public int TestRoundsPlayed(int roundsToPlay)
        {
            Player player = new Player("Car", new RandomNumberGenerator());

            for (int i = 0; i < roundsToPlay; i++)
            {
                player.TakeTurn();
            }

            return player.RoundsPlayed;
        }
    }
}
