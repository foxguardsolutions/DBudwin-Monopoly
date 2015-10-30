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
    public class CardActionsTest
    {
        private ICardActions cardActions;
        private IEnumerable<IChanceCard> chanceCards;
        private IEnumerable<ICommunityChestCard> communityChestCards;
        private IMonopolyGame game;
        private IGamePlayers players;
        private IPlayer player;
        private int numberOfPlayers;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse", "Thimble" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                cardActions = kernel.Get<ICardActions>();
                chanceCards = kernel.Get<ICardSpaceActions>().ChanceManager.Cards;
                communityChestCards = kernel.Get<ICardSpaceActions>().CommunityChestManager.Cards;
                game = kernel.Get<IMonopolyGame>();
                players = game.Players;
                player = players.AllPlayers.First();
                numberOfPlayers = players.AllPlayers.Count();
            }
        }

        private ICard FindCardWithText(string text, IEnumerable<ICard> cards)
        {
            foreach (ICard card in cards)
            {
                if (card.CardText == text)
                {
                    return card;
                }
            }

            return default(ICard);
        }

        [TestCase("Bank error in your favor", Description = "Test collecting money from the bank")]
        public void TestCollectMoneyFromBank(string cardText)
        {
            ICard card = FindCardWithText(cardText, communityChestCards);

            int cash = player.Cash;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(player.Cash, cash + card.CardValue);
        }

        [TestCase("Grand Opera Night", Description = "Test collecting money from the other players")]
        public void TestCollectMoneyFromPlayers(string cardText)
        {
            ICard card = FindCardWithText(cardText, communityChestCards);

            int cash = player.Cash;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(cash + ((numberOfPlayers - 1) * card.CardValue), player.Cash);
        }

        [TestCase("Grand Opera Night", Description = "Test paying another player")]
        public void TestCollectMoneyFromPlayersCheckPayerPaid(string cardText)
        {
            ICard card = FindCardWithText(cardText, communityChestCards);

            IPlayer otherPlayer = game.Players.AllPlayers.Last();

            int cash = otherPlayer.Cash;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(cash - card.CardValue, otherPlayer.Cash);
        }

        [TestCase("Doctor's fees", Description = "Test paying the bank")]
        public void TestPayBank(string cardText)
        {
            ICard card = FindCardWithText(cardText, communityChestCards);

            int cash = player.Cash;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(cash - card.CardValue, player.Cash);
        }

        [TestCase("You have been elected Chairman of the Board – Pay each player $50", Description = "Test paying the bank")]
        public void TestPayPlayers(string cardText)
        {
            ICard card = FindCardWithText(cardText, chanceCards);

            int cash = player.Cash;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(cash - ((numberOfPlayers - 1) * card.CardValue), player.Cash);
        }

        [TestCase("Advance to Go", Description = "Test moving a player to Go")]
        [TestCase("Advance to Illinois Ave.", Description = "Test moving a player to Illinois Ave.")]
        [TestCase("Go to Jail – Go directly to Jail – Do not pass Go, do not collect $200", Description = "Test moving a player to jail")]
        public void TestMovePlayerTo(string cardText)
        {
            ICard card = FindCardWithText(cardText, chanceCards);

            card.CardAction.Invoke(player, card);

            Assert.AreEqual((int)card.PropertyToMoveTo, player.CurrentPosition);
        }

        [TestCase("Go to Jail – Go directly to Jail – Do not pass Go, do not collect $200", Description = "Test moving a player to jail, and that they're not visiting")]
        public void TestMovePlayerToJail(string cardText)
        {
            ICard card = FindCardWithText(cardText, chanceCards);

            card.CardAction.Invoke(player, card);

            Assert.IsTrue(player.IsInJail());
        }

        [TestCase(BoardSpace.SpaceKeys.Go, BoardSpace.SpaceKeys.ElectricCompany, Description = "Test moving the player to the nearest utility, starting at Go")]
        [TestCase(BoardSpace.SpaceKeys.ElectricCompany, BoardSpace.SpaceKeys.WaterWorks, Description = "Test moving the player to the nearest utility, starting at Electric Company")]
        [TestCase(BoardSpace.SpaceKeys.WaterWorks, BoardSpace.SpaceKeys.ElectricCompany, Description = "Test moving the player to the nearest utility, starting at Water Works")]
        [TestCase(BoardSpace.SpaceKeys.Boardwalk, BoardSpace.SpaceKeys.ElectricCompany, Description = "Test moving the player to the nearest utility, starting at Boardwalk")]
        public void TestMoveToNearestUtility(BoardSpace.SpaceKeys playerStartingPosition, BoardSpace.SpaceKeys expectedUtility)
        {
            ICard card = FindCardWithText("Advance token to nearest Utility", chanceCards);

            player.CurrentPosition = (int)playerStartingPosition;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual((int)expectedUtility, player.CurrentPosition);
        }

        [TestCase(BoardSpace.SpaceKeys.Go, BoardSpace.SpaceKeys.ReadingRR, Description = "Test moving the player to the nearest railroad, starting at Go")]
        [TestCase(BoardSpace.SpaceKeys.ReadingRR, BoardSpace.SpaceKeys.PennsylvaniaRR, Description = "Test moving the player to the nearest railroad, starting at Reading Railroad")]
        [TestCase(BoardSpace.SpaceKeys.PennsylvaniaRR, BoardSpace.SpaceKeys.BORR, Description = "Test moving the player to the nearest railroad, starting at Pennsylvania Railroad")]
        [TestCase(BoardSpace.SpaceKeys.BORR, BoardSpace.SpaceKeys.ShortLine, Description = "Test moving the player to the nearest railroad, starting at B. & O. Railroad")]
        [TestCase(BoardSpace.SpaceKeys.ShortLine, BoardSpace.SpaceKeys.ReadingRR, Description = "Test moving the player to the nearest railroad, starting at the Short Line Railroad")]
        public void TestMoveToNearestRailroad(BoardSpace.SpaceKeys playerStartingPosition, BoardSpace.SpaceKeys expectedRailroad)
        {
            ICard card = FindCardWithText("Advance token to the nearest Railroad", chanceCards);

            player.CurrentPosition = (int)playerStartingPosition;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual((int)expectedRailroad, player.CurrentPosition);
        }

        [TestCase(Description = "Test move back three spaces")]
        public void TestMoveBackThreeSpaces()
        {
            ICard card = FindCardWithText("Go Back 3 Spaces", chanceCards);

            int startingPosition = player.CurrentPosition;

            card.CardAction.Invoke(player, card);

            Assert.AreEqual(startingPosition - 3, player.CurrentPosition);
        }

        [TestCase(Description = "Test player uses Get out of Jail Free card")]
        public void TestGetOutOfJailForFree()
        {
            player.GoToJail();

            ICard card = FindCardWithText("Get out of Jail Free", chanceCards);

            card.CardAction.Invoke(player, card);

            Assert.IsFalse(player.IsInJail());
        }
    }
}
