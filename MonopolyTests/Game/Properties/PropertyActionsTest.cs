using System.Linq;
using Ninject;
using Monopoly.Game;
using Monopoly.Game.Bank;
using Monopoly.Game.GamePlay;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;

namespace MonopolyTests.Game.Properties
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PropertyActionsTest
    {
        private IMonopolyGame game;
        private IPropertyManager manager;
        private IBoard board;
        private IPlayer player;
        private IPlayer ownerPlayer;
        private int startingCash = Monopoly.Game.Players.Player.STARTING_CASH;

        public void CreateGame(string args)
        {
            string[] names = args.Split(',');

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();

                game.Banker = kernel.Get<IBanker>();
                game.Dice = kernel.Get<IDice>();
            }
        }

        [SetUp]
        public void SetUp()
        {
            CreateGame("Car,Horse");

            manager = game.Manager;
            board = manager.Board;
            player = game.Players.First();
            ownerPlayer = game.Players.Last();
        }

        private RealEstateSpace PurchaseProperty(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = (RealEstateSpace)manager.Board.GetSpaceAt(space);

            manager.PurchaseCurrentSpace(player, realEstateSpace);

            return realEstateSpace;
        }

        [TestCase(BoardSpace.SpaceKeys.Mediterranean, Description = "User Story: Land on a Property that is not owned. After turn, property is owned and balance decreases by cost of property")]
        public void TestSuccessfulPurchaseCurrentSpaceBalanceDecreased(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = PurchaseProperty(space);

            int expectedCash = startingCash - realEstateSpace.Cost;

            Assert.AreEqual(expectedCash, player.Cash);
        }

        [TestCase(BoardSpace.SpaceKeys.Mediterranean, Description = "User Story: Land on a Property that is not owned. After turn, property is owned and balance decreases by cost of property")]
        public void TestSuccessfulPurchaseCurrentSpacePlayerOwnsDeed(BoardSpace.SpaceKeys space)
        {
            Assert.AreEqual(player, PurchaseProperty(space).Owner);
        }

        [TestCase(BoardSpace.SpaceKeys.Mediterranean, 50)]
        public void TestUnsuccessfulPurchaseCurrentSpace(BoardSpace.SpaceKeys space, int expectedCash)
        {
            player.Cash = 50;

            RealEstateSpace realEstateSpace = (RealEstateSpace)board.GetSpaceAt(space);

            manager.PurchaseCurrentSpace(player, realEstateSpace);

            Assert.AreEqual(expectedCash, player.Cash);
        }

        [TestCase(BoardSpace.SpaceKeys.Virginia, Description = "User Story: Land on a Property that I own, nothing happens")]
        public void TestLandingOnPropertyIOwn(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = (RealEstateSpace)board.GetSpaceAt(space);

            realEstateSpace.Owner = player;

            player.CurrentPosition = 8;

            Mock<IDice> mock = new Mock<IDice>();
            mock.SetupSequence(s => s.RollDie()).Returns(2).Returns(4);

            game.Dice = mock.Object;
            game.PlayerRollEvent(player);

            Assert.AreEqual(startingCash, player.Cash);
            Assert.AreEqual(player, realEstateSpace.Owner);
        }

        private RealEstateSpace PayRentForProperty(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = (RealEstateSpace)board.GetSpaceAt(space);
            realEstateSpace.Owner = ownerPlayer;

            manager.PayRent(player, realEstateSpace);

            return realEstateSpace;
        }

        [TestCase(BoardSpace.SpaceKeys.Mediterranean)]
        public void TestSuccessfulPayRentPlayerDebit(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = PayRentForProperty(space);

            Assert.AreEqual(startingCash - realEstateSpace.Rent, player.Cash);
        }

        [TestCase(BoardSpace.SpaceKeys.Mediterranean)]
        public void TestSuccessfulPayRentOwnerCredit(BoardSpace.SpaceKeys space)
        {
            RealEstateSpace realEstateSpace = PayRentForProperty(space);

            Assert.AreEqual(startingCash + realEstateSpace.Rent, ownerPlayer.Cash);
        }

        [TestCase(BoardSpace.SpaceKeys.Boardwalk, Description = "The rent is too damn high!  Can't afford to pay.")]
        public void TestUnableToPayRent(BoardSpace.SpaceKeys space)
        {
            player.Cash = 100;

            PayRentForProperty(space);

            Assert.AreEqual(0, player.Cash);
        }

        [TestCase(2, 8, Description = "User Story: If landing on Utility and only one Utility owned, rent is 4 times current value on Dice")]
        public void TestDetermineUtilityRentOneOwned(int rollValue, int expectedRent)
        {
            RealEstateSpace electricSpace = (RealEstateSpace)board.GetSpaceAt(BoardSpace.SpaceKeys.ElectricCompany);

            electricSpace.Owner = ownerPlayer;

            int rent = manager.DetermineUtilityRent(rollValue);

            Assert.AreEqual(rent, expectedRent);
        }

        [TestCase(2, 20, Description = "User Story: If landing on Utility and both owned (not necessarily by same Player), rent is 10 times current value on Dice")]
        public void TestDetermineUtilityRentBothOwned(int rollValue, int expectedRent)
        {
            RealEstateSpace electricSpace = (RealEstateSpace)board.GetSpaceAt(BoardSpace.SpaceKeys.ElectricCompany);
            RealEstateSpace waterSpace = (RealEstateSpace)board.GetSpaceAt(BoardSpace.SpaceKeys.WaterWorks);

            electricSpace.Owner = ownerPlayer;
            waterSpace.Owner = ownerPlayer;

            int rent = manager.DetermineUtilityRent(rollValue);

            Assert.AreEqual(rent, expectedRent);
        }

        [TestCase(25, 1, Description = "User Story: Rent is 25 when one railroad is owned by owner")]
        [TestCase(50, 2, Description = "User Story: Rent is 50 when two railroads are owned by owner")]
        [TestCase(100, 3, Description = "User Story: Rent is 100 when three railroads are owned by owner")]
        [TestCase(200, 4, Description = "User Story: Rent is 200 when four railroads are owned by owner")]
        public void TestDetermineRailroadOneOwnedRent(int expectedRent, int ownedRailroads)
        {
            RailroadSpace readingRailroadSpace = (RailroadSpace)manager.Board.GetSpaceAt(BoardSpace.SpaceKeys.ReadingRR);

            readingRailroadSpace.Owner = ownerPlayer;

            int rent = manager.DetermineRailroadRent(readingRailroadSpace, ownedRailroads);

            Assert.AreEqual(expectedRent, rent);
        }

        [TestCase(Description = "User Story: If landing on Real Estate and not all in the same Property Group are owned, rent is stated rent value")]
        public void TestDeterminePropertyRentNotAllOwned()
        {
            PropertySpace boardwalk = (PropertySpace)board.GetSpaceAt(BoardSpace.SpaceKeys.Boardwalk);

            boardwalk.Owner = ownerPlayer;

            int rent = manager.DeterminePropertyRent(boardwalk);

            Assert.AreEqual(boardwalk.Rent, rent);
        }

        [TestCase(Description = "User Story: If landing on Real Estate and Owner owns all in the same Property Group, rent is 2 times stated rent value")]
        public void TestDeterminePropertyRentAllOwned()
        {
            PropertySpace parkPlace = (PropertySpace)board.GetSpaceAt(BoardSpace.SpaceKeys.ParkPlace);
            PropertySpace boardwalk = (PropertySpace)board.GetSpaceAt(BoardSpace.SpaceKeys.Boardwalk);

            parkPlace.Owner = ownerPlayer;
            boardwalk.Owner = ownerPlayer;

            int rent = manager.DeterminePropertyRent(boardwalk);

            Assert.AreEqual(boardwalk.Rent * 2, rent);
        }
    }
}