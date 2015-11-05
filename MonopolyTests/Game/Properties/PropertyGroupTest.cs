using System.Collections.Generic;
using Monopoly.Game;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Properties;
using Ninject;

namespace MonopolyTests.Game.Properties
{
    using NUnit.Framework;

    [TestFixture]
    public class PropertyGroupTest
    {
        private IBoard board;

        [SetUp]
        public void SetUp()
        {
            IKernel kernel = new StandardKernel(new MonopolyBindings(new string[] { "Car", "Horse" }));

            board = kernel.Get<IBoard>();
        }

        [Test]
        public void TestGetAllPropertiesInGroup()
        {
            IEnumerable<IBoardSpace> orangeProperties = new List<IBoardSpace>
            {
                board.GetSpaceAt(BoardSpace.SpaceKeys.StJames),
                board.GetSpaceAt(BoardSpace.SpaceKeys.Tennessee),
                board.GetSpaceAt(BoardSpace.SpaceKeys.NewYork)
            };

            IEnumerable<IBoardSpace> propertiesInGroup = PropertyColorGroup.GetAllPropertiesInGroup(board, PropertyColorGroup.Groups.Orange);

            CollectionAssert.AreEqual(orangeProperties, propertiesInGroup);
        }
    }
}