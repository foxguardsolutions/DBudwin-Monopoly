using Monopoly.Game;
using Monopoly.Game.GamePlay;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game.GamePlay
{
    [TestFixture]
    public class DiceOutcomeHandlerTest
    {
        private IDiceOutcomeHandler diceOutcome;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                diceOutcome = kernel.Get<IDiceOutcomeHandler>();
            }
        }

        [TestCase(1, Result = 1)]
        [TestCase(2, Result = 2)]
        public int TestCheckForDoubles(int doublesRolled)
        {
            for (int i = 0; i < doublesRolled; i++)
            {
                diceOutcome.RollDice(1, 1);
            }

            return diceOutcome.ConsecutiveDoublesRolled;
        }
    }
}
