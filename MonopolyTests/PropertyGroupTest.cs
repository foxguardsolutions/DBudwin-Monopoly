using System.Collections.Generic;
using Monopoly.Game;
using Monopoly.Game.Properties;
using Ninject;

namespace MonopolyTests
{
    using NUnit.Framework;

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
                board.SpaceAt(BoardSpace.SpaceKeys.StJames),
                board.SpaceAt(BoardSpace.SpaceKeys.Tennessee),
                board.SpaceAt(BoardSpace.SpaceKeys.NewYork)
            };

            IEnumerable<IBoardSpace> propertiesInGroup = PropertyGroup.GetAllPropertiesInGroup(board, PropertyGroup.Groups.Orange);

            CollectionAssert.AreEqual(orangeProperties, propertiesInGroup);
        }
    }
}