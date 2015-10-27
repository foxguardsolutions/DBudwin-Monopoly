using System.Linq;
using Monopoly.Game;
using Monopoly.Game.Properties;
using Monopoly.Random;
using Moq;
using Ninject;

namespace MonopolyTests
{
    using NUnit.Framework;

    [TestFixture]
    public class MonopolyGameTest
    {
        private int roundsToPlay;
        private IMonopolyGame game;
        private RailroadSpace readingRailroadSpace;
        private RailroadSpace pennsylvaniaRailroadSpace;
        private RailroadSpace boRailroadSpace;
        private RailroadSpace shortlineRailroadSpace;
        private Mock<IRandomNumberGenerator> randMock;
        private Player player;

        [SetUp]
        public void SetUp()
        {
            randMock = new Mock<IRandomNumberGenerator>();
            player = new Player("Car", randMock.Object);
        }

        public void RollDiceMock(int die1, int die2)
        {
            randMock.SetupSequence(s => s.Generate(1, MonopolyGame.NUMBER_OF_SIDES)).Returns(die1).Returns(die2);
        }

        public void CreateGame(string args)
        {            
            string[] names = args.Split(',');

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();

                roundsToPlay = 20;
            }
        }

        public void CreateRailroads()
        {
            readingRailroadSpace = (RailroadSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.ReadingRR);
            pennsylvaniaRailroadSpace = (RailroadSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.PennsylvaniaRR);
            boRailroadSpace = (RailroadSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.BORR);
            shortlineRailroadSpace = (RailroadSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.ShortLine);
    }

        [TestCase("Car,Horse", Result = 2, Description = "User Story: Test lower bound with Horse and Car players")]
        [TestCase("Car,Horse,Thimble,Dog,Ship", Result = 5)]
        [TestCase("Car,Horse,Thimble,Dog,Ship,Top Hat,Iron,Wheelbarrow", Result = 8, Description = "User Story: Test upper bound with too many players")]
        public int TestCreateMonopolyGame(string args)
        {
            CreateGame(args);

            return game.Players.Count();
        }

        [TestCase("Car,Horse", Description = "User Story: Make sure the order of the players remains constant")]
        public void TestPlayerOrderIsConsistent(string args)
        {
            CreateGame(args);

            IPlayer player1 = game.Players.ElementAt(0);
            IPlayer player2 = game.Players.ElementAt(1);

            for (int i = 0; i < roundsToPlay; i++)
            {
                game.PlayRound();

                Assert.AreEqual(player1, game.Players.ElementAt(0));
                Assert.AreEqual(player2, game.Players.ElementAt(1));
            }
        }

        [TestCase("Car,Horse", 34, 8, Result = 600, Description = "Test passing Go")]
        [TestCase("Car,Horse", 38, 2, Result = 600, Description = "Test landing on Go")]
        [TestCase("Car,Horse", 10, 8, Result = 220, Description = "Test not passing Go")]
        public int TestCheckIfPassGo(string args, int position, int roll)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.CurrentPosition = position;

            player.TakeTurn(roll);

            game.EvaluateRollOutcome(player);

            return player.Cash;
        }

        [TestCase("Car,Horse", 0, 100, 4, Result = 80, Description = "Test Income Tax Penalty for 20%")]
        [TestCase("Car,Horse", 0, 1000, 4, Result = 800, Description = "Test Income Tax Penalty for 200 dollars")]
        public int TestPayIncomeTax(string args, int position, int cash, int roll)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.CurrentPosition = position;
            player.Cash = cash;
            player.TakeTurn(roll);

            game.EvaluateRollOutcome(player);

            return player.Cash;
        }

        [TestCase("Car,Horse", BoardSpace.SpaceKeys.Mediterranean, 200, 140, Description = "User Story: Land on a Property that is not owned. After turn, property is owned and balance decreases by cost of property")]
        public void TestSuccessfulPurchaseCurrentSpace(string args, BoardSpace.SpaceKeys space, int defaultCash, int expectedCash)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.Cash = defaultCash;

            RealEstateSpace realEstateSpace = (RealEstateSpace)game.Board.SpaceAt(space);

            game.PurchaseCurrentSpace(player, realEstateSpace);

            Assert.AreEqual(expectedCash, player.Cash);
            Assert.AreEqual(player, realEstateSpace.Owner);
        }

        [TestCase("Car,Horse", BoardSpace.SpaceKeys.Mediterranean, 0, 0)]
        public void TestUnsuccessfulPurchaseCurrentSpace(string args, BoardSpace.SpaceKeys space, int defaultCash, int expectedCash)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.Cash = defaultCash;

            RealEstateSpace realEstateSpace = (RealEstateSpace)game.Board.SpaceAt(space);

            game.PurchaseCurrentSpace(player, realEstateSpace);

            Assert.AreEqual(expectedCash, player.Cash);
        }

        [TestCase("Car,Horse", BoardSpace.SpaceKeys.Mediterranean, 200, Description = "User Story: Land on a Property that I own, nothing happens")]
        public void TestLandingOnPropertyIOwn(string args, BoardSpace.SpaceKeys space, int defaultCash)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.Cash = defaultCash;

            RealEstateSpace realEstateSpace = (RealEstateSpace)game.Board.SpaceAt(space);

            realEstateSpace.Owner = player;

            game.PurchaseCurrentSpace(player, realEstateSpace);

            Assert.AreEqual(defaultCash, player.Cash);
            Assert.AreEqual(player, realEstateSpace.Owner);
        }

        [TestCase("Car,Horse", BoardSpace.SpaceKeys.Mediterranean, 200, 195)]
        public void TestSuccessfulPayRent(string args, BoardSpace.SpaceKeys space, int defaultCash, int expectedCash)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();
            IPlayer ownerPlayer = game.Players.Last();

            player.Cash = defaultCash;
            ownerPlayer.Cash = defaultCash;

            RealEstateSpace realEstateSpace = (RealEstateSpace)game.Board.SpaceAt(space);
            realEstateSpace.Owner = ownerPlayer;

            game.PayRent(player, realEstateSpace);

            Assert.AreEqual(expectedCash, player.Cash);
            Assert.AreEqual(defaultCash + realEstateSpace.Rent, ownerPlayer.Cash);
        }

        [TestCase("Car,Horse", BoardSpace.SpaceKeys.Boardwalk, 20, Description = "The rent is too damn high!  Can't afford to pay.")]
        public void TestUnableToPayRent(string args, BoardSpace.SpaceKeys space, int defaultCash)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();
            IPlayer ownerPlayer = game.Players.Last();

            player.Cash = defaultCash;
            ownerPlayer.Cash = defaultCash;

            RealEstateSpace realEstateSpace = (RealEstateSpace)game.Board.SpaceAt(space);
            realEstateSpace.Owner = ownerPlayer;

            game.PayRent(player, realEstateSpace);

            Assert.AreEqual(0, player.Cash);
        }

        [TestCase("Car,Horse", 2, 8, Description = "User Story: If landing on Utility and only one Utility owned, rent is 4 times current value on Dice")]
        public void TestDetermineUtilityRentOneOwned(string args, int rollValue, int expectedRent)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            RealEstateSpace electricSpace = (RealEstateSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.ElectricCompany);

            electricSpace.Owner = ownerPlayer;

            int rent = game.DetermineUtilityRent(rollValue);

            Assert.AreEqual(rent, expectedRent);
        }

        [TestCase("Car,Horse", 2, 20, Description = "User Story: If landing on Utility and both owned (not necessarily by same Player), rent is 10 times current value on Dice")]
        public void TestDetermineUtilityRentBothOwned(string args, int rollValue, int expectedRent)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            RealEstateSpace electricSpace = (RealEstateSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.ElectricCompany);
            RealEstateSpace waterSpace = (RealEstateSpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.WaterWorks);

            electricSpace.Owner = ownerPlayer;
            waterSpace.Owner = ownerPlayer;

            int rent = game.DetermineUtilityRent(rollValue);

            Assert.AreEqual(rent, expectedRent);
        }

        [TestCase("Car,Horse", Description = "User Story: Rent is 25 when one railroad is owned by owner")]
        public void TestDetermineRailroadOneOwnedRent(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            CreateRailroads();

            readingRailroadSpace.Owner = ownerPlayer;

            int rent = game.DetermineRailroadRent(readingRailroadSpace);

            Assert.AreEqual(25, rent);
        }

        [TestCase("Car,Horse", Description = "User Story: Rent is 50 when two railroads are owned by owner")]
        public void TestDetermineRailroadTwoOwnedRent(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            CreateRailroads();

            readingRailroadSpace.Owner = ownerPlayer;
            pennsylvaniaRailroadSpace.Owner = ownerPlayer;

            int rent = game.DetermineRailroadRent(readingRailroadSpace);

            Assert.AreEqual(50, rent);
        }

        [TestCase("Car,Horse", Description = "User Story: Rent is 100 when three railroads are owned by owner")]
        public void TestDetermineRailroadThreeOwnedRent(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            CreateRailroads();

            readingRailroadSpace.Owner = ownerPlayer;
            pennsylvaniaRailroadSpace.Owner = ownerPlayer;
            boRailroadSpace.Owner = ownerPlayer;

            int rent = game.DetermineRailroadRent(readingRailroadSpace);

            Assert.AreEqual(100, rent);
        }

        [TestCase("Car,Horse", Description = "User Story: Rent is 200 when four railroads are owned by owner")]
        public void TestDetermineRailroadFourOwnedRent(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            CreateRailroads();

            readingRailroadSpace.Owner = ownerPlayer;
            pennsylvaniaRailroadSpace.Owner = ownerPlayer;
            boRailroadSpace.Owner = ownerPlayer;
            shortlineRailroadSpace.Owner = ownerPlayer;

            int rent = game.DetermineRailroadRent(readingRailroadSpace);

            Assert.AreEqual(200, rent);
        }

        [TestCase("Car,Horse", Description = "User Story: If landing on Real Estate and not all in the same Property Group are owned, rent is stated rent value")]
        public void TestDeterminePropertyRentNotAllOwned(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            PropertySpace boardwalk = (PropertySpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.Boardwalk);

            boardwalk.Owner = ownerPlayer;

            int rent = game.DeterminePropertyRent(boardwalk);

            Assert.AreEqual(200, rent);
        }

        [TestCase("Car,Horse", Description = "User Story: If landing on Real Estate and Owner owns all in the same Property Group, rent is 2 times stated rent value")]
        public void TestDeterminePropertyRentAllOwned(string args)
        {
            CreateGame(args);

            IPlayer ownerPlayer = game.Players.Last();

            PropertySpace parkPlace = (PropertySpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.ParkPlace);
            PropertySpace boardwalk = (PropertySpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.Boardwalk);

            parkPlace.Owner = ownerPlayer;
            boardwalk.Owner = ownerPlayer;

            int rent = game.DeterminePropertyRent(boardwalk);

            Assert.AreEqual(400, rent);
        }

        [TestCase("Car,Horse", 100, Description = "Make sure cash is transferred between buyer and owner correctly")]
        public void TestMoveCashFromBuyerToOwner(string args, int rent)
        {
            CreateGame(args);

            IPlayer buyerPlayer = game.Players.First();
            IPlayer ownerPlayer = game.Players.Last();

            game.MoveCashFromBuyerToOwner(buyerPlayer, ownerPlayer, rent, "Some Space");

            Assert.AreEqual(Player.STARTING_CASH - rent, buyerPlayer.Cash);
            Assert.AreEqual(Player.STARTING_CASH + rent, ownerPlayer.Cash);
        }

        [TestCase("Car,Horse", 1000, Description = "Make sure cash is transferred between buyer and owner correctly when buyer doesn't have enough cash")]
        public void TestMoveCashFromBuyerToOwnerCantPay(string args, int rent)
        {
            CreateGame(args);

            IPlayer buyerPlayer = game.Players.First();
            IPlayer ownerPlayer = game.Players.Last();

            game.MoveCashFromBuyerToOwner(buyerPlayer, ownerPlayer, rent, "Some Space");

            Assert.AreEqual(0, buyerPlayer.Cash);
            Assert.AreEqual(Player.STARTING_CASH * 2, ownerPlayer.Cash);
        }

        [TestCase("Car,Horse")]
        public void TestPlayerLoses(string args)
        {
            CreateGame(args);

            IPlayer losingPlayer = game.Players.First();
            IPlayer otherPlayer = game.Players.Last();

            PropertySpace boardwalk = (PropertySpace)game.Board.SpaceAt(BoardSpace.SpaceKeys.Boardwalk);

            boardwalk.Owner = otherPlayer;

            losingPlayer.Cash = 1;

            game.PayRent(losingPlayer, boardwalk);

            Assert.AreEqual(0, losingPlayer.Cash);
            Assert.AreEqual(Player.STARTING_CASH + 1, otherPlayer.Cash);
        }

        [TestCase("Car,Horse", 1)]
        [TestCase("Car,Horse", 2)]
        public void TestRollAgainIfDoubles(string args, int consecutiveDoublesRolled)
        {
            CreateGame(args);

            IPlayer player = game.Players.First();

            player.DoublesCounter = consecutiveDoublesRolled;

            game.RollAgainIfDoublesRolled(player);

            Assert.Greater(player.CurrentPosition, 0);
            Assert.AreEqual(1, player.RoundsPlayed);
        }

        [TestCase("Car,Horse", Description = "User Story: Roll doubles 3 times in a row, never pass or land on go. Balance is unchanged. Player is in Jail.")]
        public void TestGoToJailForTripleDoubles(string args)
        {
            CreateGame(args);

            Mock<IPlayer> mock = new Mock<IPlayer>();
            mock.Setup(s => s.RollDie()).Returns(2);

            game.PlayerRollEvent(player, player.RollDice(mock.Object.RollDie(), mock.Object.RollDie()));

            Assert.IsTrue(player.IsIncarcerated);
        }

        [TestCase("Car,Horse")]
        public void TestLeaveJailForRollingDoubles(string args)
        {
            CreateGame(args);

            IPlayer jailedPlayer = game.Players.Last();

            game.GoToJail(jailedPlayer);

            Mock<IPlayer> mock = new Mock<IPlayer>();
            mock.Setup(s => s.RollDie()).Returns(2);

            game.PlayerRollEvent(jailedPlayer, jailedPlayer.RollDice(mock.Object.RollDie(), mock.Object.RollDie()));

            Assert.IsFalse(jailedPlayer.IsIncarcerated);
        }

        [TestCase("Car,Horse", 20, 10, true, Description = "User Story: Roll doubles, land on Go To Jail, player is in Jail, turn is over, balance is unchanged")]
        [TestCase("Car,Horse", 25, 5, false, Description = "User Story: Roll doubles, land on Go To Jail, player is in Jail, turn is over, balance is unchanged")]
        public void TestGoToJail(string args, int initialPosition, int roll, bool isRollDoubles)
        {
            CreateGame(args);

            IPlayer jailedPlayer = game.Players.First();

            int playerBalance = jailedPlayer.Cash;

            jailedPlayer.CurrentPosition = initialPosition;

            game.PlayerRollEvent(jailedPlayer, roll);

            Assert.IsTrue(jailedPlayer.IsIncarcerated);
            Assert.AreEqual(jailedPlayer.CurrentPosition, (int)BoardSpace.SpaceKeys.Jail);
            Assert.AreEqual(Player.STARTING_CASH, playerBalance);
        }

        [TestCase("Car,Horse")]
        public void TestPayToGetOutOfJail(string args)
        {
            CreateGame(args);

            IPlayer jailedPlayer = game.Players.First();

            int balance = jailedPlayer.Cash;

            game.GoToJail(jailedPlayer);

            jailedPlayer.RoundsInJail = 3;

            game.PlayerRollEvent(jailedPlayer, 10);

            Assert.AreEqual(balance - MonopolyGame.JAIL_BAIL, jailedPlayer.Cash);
            Assert.IsFalse(jailedPlayer.IsIncarcerated);
        }
    }
}