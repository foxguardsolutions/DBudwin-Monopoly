using System.Linq;
using Monopoly.Game;
using Monopoly.Game.Bank;
using Ninject;
using NUnit.Framework;
using Monopoly.Game.Players;

namespace MonopolyTests.Game.Bank
{
    [TestFixture]
    public class BankerTest
    {
        private IMonopolyGame game;
        private IBanker banker;
        private const int playerStartingCash = Monopoly.Game.Players.Player.STARTING_CASH;
        private IPlayer buyerPlayer;
        private IPlayer ownerPlayer;

        public void CreateGame(string args)
        {
            string[] names = args.Split(',');

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();
                banker = kernel.Get<IBanker>();
            }
        }

        [SetUp]
        public void SetUp()
        {
            CreateGame("Car,Horse");

            buyerPlayer = game.Players.First();
            ownerPlayer = game.Players.Last();
        }

        [TestCase(100, Description = "Make sure cash is transferred between buyer and owner correctly")]
        public void TestMoveCashFromBuyerToOwner(int rent)
        {
            banker.MoveCashFromBuyerToOwner(buyerPlayer, ownerPlayer, rent, "Some Space");

            Assert.AreEqual(playerStartingCash - rent, buyerPlayer.Cash);
            Assert.AreEqual(playerStartingCash + rent, ownerPlayer.Cash);
        }

        [TestCase(1000, Description = "Make sure cash is transferred between buyer and owner correctly when buyer doesn't have enough cash")]
        public void TestMoveCashFromBuyerToOwnerCantPay(int rent)
        {
            banker.MoveCashFromBuyerToOwner(buyerPlayer, ownerPlayer, rent, "Some Space");

            Assert.AreEqual(0, buyerPlayer.Cash);
            Assert.AreEqual(playerStartingCash * 2, ownerPlayer.Cash);
        }

        [TestCase(100, Result = playerStartingCash - 100, Description = "Have enough to pay")]
        [TestCase(1000, Result = playerStartingCash - 1000, Description = "Don't have enough to pay, go into debt.  This causes player to lose")]
        public int TestPay(int amount)
        {
            banker.Pay(buyerPlayer, amount, "Test Pay");

            return buyerPlayer.Cash;
        }
    }
}