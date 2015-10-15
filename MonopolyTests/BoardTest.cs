using NUnit.Framework;
using Monopoly.Game;
using System.Linq;
using Ninject;

namespace MonopolyTests
{
    [TestFixture]
    public class BoardTest
    {
        private IBoard board;

        [SetUp]
        public void SetUp()
        {
            IKernel kernel = new StandardKernel(new MonopolyBindings(new string[] {"Car", "Horse"}));
            board = kernel.Get<IBoard>();
        }

        [Test(Description = "Make sure spaces are created and sorted in the right order via the Ninject bindings")]
        public void TestCreateBoard()
        {
            for (int i = 0; i < Board.NUMBER_OF_SPACES; i++)
            {
                Assert.AreEqual(i, (int)board.Spaces.ElementAt(i).Position);
            }
        }
    }
}
