using System.Collections.Generic;
using System.Linq;
using Monopoly.Game;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests
{
    public class BoardSpacesFactoryTest
    {
        private IBoardSpacesFactory boardFactory;

        [SetUp]
        public void SetUp()
        {
            IKernel kernel = new StandardKernel(new MonopolyBindings(new string[] { "Car", "Horse" }));

            boardFactory = kernel.Get<IBoardSpacesFactory>();
        }

        [Test(Description = "Make sure spaces are created and sorted in the right order through the factory methods")]
        public void TestCreateBoard()
        {
            IEnumerable<IBoardSpace> spaces = boardFactory.CreateAll();

            for (int i = 0; i < Board.NUMBER_OF_SPACES; i++)
            {
                Assert.AreEqual(i, (int)spaces.ElementAt(i).Position);
            }
        }
    }
}