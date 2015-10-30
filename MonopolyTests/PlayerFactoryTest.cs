using System.Collections.Generic;
using System.Linq;
using Monopoly.Game;
using Ninject;

namespace MonopolyTests
{
    using NUnit.Framework;

    public class PlayerFactoryTest
    {
        private IPlayerFactory factory;

        public void CreateFactoryWithNames(IEnumerable<string> names)
        {
            IKernel kernel = new StandardKernel(new MonopolyBindings(names));

            factory = kernel.Get<IPlayerFactory>();
        }

        [TestCase("Car", Description = "User Story: Try creating a factory with less than 2 players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship,Top Hat,Iron,Wheelbarrow,Shoe", Description = "User Story: Try creating a factory with more than 8 players")]
        [ExpectedException(typeof(System.Exception))]
        public void TestCreateMonopolyGameIllegalNumberOfPlayersException(string names)
        {
            string[] namesArray = names.Split(',');

            CreateFactoryWithNames(namesArray);

            factory.CreateAll();
        }

        [TestCase("Car,Horse", Result = 2, Description = "Create a factory with 2 players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship", Result = 5, Description = "Create a factory with 5 players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship,Top Hat,Iron,Wheelbarrow", Result = 8, Description = "Create a factory with 8 players")]
        public int TestCreateMonopolyGameValidNumberOfPlayers(string names)
        {
            string[] namesArray = names.Split(',');

            CreateFactoryWithNames(namesArray);

            IEnumerable<IPlayer> players = factory.CreateAll();

            return players.Count();
        }

        [TestCase("Car,Horse,Thimble,Dog,Ship", Result = true)]
        public bool TestPlayerOrderIsRandom(string names)
        {
            string[] namesArray = names.Split(',');

            CreateFactoryWithNames(namesArray);

            IEnumerable<IPlayer> players = factory.CreateAll();

            string[] randomPlayerNames = players.Select(player => player.Name).ToArray();

            bool isRandom = !namesArray.SequenceEqual(randomPlayerNames);

            return isRandom;
        }
    }
}