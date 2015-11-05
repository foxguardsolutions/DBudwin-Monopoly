using Monopoly.Game;
using Monopoly.Game.GamePlay;
using Moq;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game.GamePlay
{
    [TestFixture]
    public class DiceTest
    {
        private IDice dice;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                dice = kernel.Get<IDice>();
            }
        }

        [TestCase(2, Result = 2)]
        [TestCase(4, Result = 4)]
        [TestCase(12, Result = 12)]
        public int TestRollDie(int roll)
        {
            Mock<IDice> mock = new Mock<IDice>();
            mock.Setup(s => s.RollDie()).Returns(roll);

            return mock.Object.RollDie();
        }
    }
}