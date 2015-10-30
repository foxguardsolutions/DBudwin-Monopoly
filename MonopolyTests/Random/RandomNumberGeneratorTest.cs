using Monopoly.Game.GamePlay;
using Monopoly.Random;
using Moq;
using NUnit.Framework;

namespace MonopolyTests.Random
{
    [TestFixture]
    public class RandomNumberGeneratorTest
    {
        [TestCase(1, Result = 1)]
        [TestCase(4, Result = 4)]
        public int TestGenerate(int expected)
        {
            Mock<IRandomNumberGenerator> mock = new Mock<IRandomNumberGenerator>();
            mock.Setup(s => s.Generate(1, Dice.NUMBER_OF_SIDES)).Returns(expected);

            return mock.Object.Generate(1, Dice.NUMBER_OF_SIDES);
        }
    }
}
