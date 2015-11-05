using System.Collections.Generic;
using System.Linq;
using Monopoly.Game;
using Monopoly.Game.Cards;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game.Cards
{
    [TestFixture]
    public class CardFactoryTest
    {
        private ICardFactory cardFactory;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                cardFactory = kernel.Get<ICardFactory>();
            }
        }

        [TestCase(Description = "Test creating a list of Community Chest cards")]
        public void TestCreateCommunityChestCards()
        {
            List<ICommunityChestCard> communityChestCards = cardFactory.CreateCommunityChestCards();

            Assert.AreEqual(16, communityChestCards.Count());
        }

        [TestCase(Description = "Test creating a list of Chance cards")]
        public void TestCreateChanceCards()
        {
            List<IChanceCard> chanceCards = cardFactory.CreateChanceCards();

            Assert.AreEqual(15, chanceCards.Count());
        }
    }
}
