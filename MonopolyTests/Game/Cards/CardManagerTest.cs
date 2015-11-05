using System.Collections.Generic;
using System.Linq;
using Monopoly.Game;
using Monopoly.Game.Cards;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;
using Ninject;
using NUnit.Framework;

namespace MonopolyTests.Game.Cards
{
    [TestFixture]
    public class CardManagerTest
    {
        private IEnumerable<IChanceCard> chanceCards;
        private IMonopolyGame game;
        private IPlayer player;
        private ICardManager<IChanceCard> chanceManager;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse", "Thimble" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                chanceManager = kernel.Get<ICardSpaceActions>().ChanceManager;
                chanceCards = chanceManager.Cards;
                game = kernel.Get<IMonopolyGame>();
                player = game.Players.AllPlayers.First();
            }
        }

        [TestCase(Description = "Test to make sure the top card is placed on the bottom of the deck")]
        public void TestTopCardIsPlacedOnBottomAfterPlaying()
        {
            List<IChanceCard> cards = new List<IChanceCard>(chanceManager.Cards);

            chanceManager.PlayCard(player);

            Assert.AreEqual(cards.First(), chanceManager.Cards.Last());
        }
    }
}
