using Monopoly.Game;
using Monopoly.Game.GamePlay;
using Ninject;

namespace MonopolyTests.Game.Player
{
    using Monopoly.Game.Players;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PlayerTest
    {
        private Mock<IDice> diceMock;
        private IPlayer player;

        [SetUp]
        public void SetUp()
        {
            string[] names = {"Car"};

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                IJail jail = kernel.Get<IJail>();
                IDiceOutcomeHandler diceOutcome = kernel.Get<IDiceOutcomeHandler>();

                player = new Player("Car", jail, diceOutcome);
            }
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
            player.GoToJail();
            player.TakeTurn(roll);

            return player.CurrentPosition;
        }
    }
}
