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
        public int TestRoll(int roll)
        {
            Mock<IRandomNumberGenerator> mock = new Mock<IRandomNumberGenerator>();
            RandomNumberGeneratorMock generator = new RandomNumberGeneratorMock(mock.Object);
            mock.Setup(s => s.RollDice()).Returns(roll).Verifiable();

            int result = generator.RollDice();

            mock.Verify();

            return result;
        }

        [TestCase(0, 7, Result = 7, Description = "User Story: Test from starting spot")]
        [TestCase(39, 6, Result = 5, Description = "User Story: Test pass Go")]
        [TestCase(14, 7, Result = 21, Description = "Test from middle of board")]
        public int TestTakeTurn(int initialPosition, int roll)
        {
            Mock<IRandomNumberGenerator> mock = new Mock<IRandomNumberGenerator>();
            RandomNumberGeneratorMock generator = new RandomNumberGeneratorMock(mock.Object);
            mock.Setup(s => s.RollDice()).Returns(roll).Verifiable();

            Player player = new Player("Car", mock.Object) { CurrentPosition = initialPosition };

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
