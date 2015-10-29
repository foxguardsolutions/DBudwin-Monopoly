using System.Linq;
using Monopoly.Game;
using Monopoly.Game.MonopolyBoard;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game.Board
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
            for (int i = 0; i < Monopoly.Game.MonopolyBoard.Board.NUMBER_OF_SPACES; i++)
            {
                Assert.AreEqual(i, (int)board.Spaces.ElementAt(i).Position);
            }
        }
    }
}
