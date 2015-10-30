using System.Linq;
using Moq;
using Ninject;
using NUnit.Framework;
using Monopoly.Game.Bank;
using Monopoly.Game;
using Monopoly.Game.GamePlay;
using Monopoly.Game.Players;

namespace MonopolyTests.Game.GamePlay
{
    [TestFixture]
    public class JailTest
    {
        private IMonopolyGame game;
        private IPlayer player;

        [SetUp]
        public void SetUp()
        {
            string[] names = { "Car", "Horse" };

            using (var kernel = new StandardKernel(new MonopolyBindings(names)))
            {
                game = kernel.Get<IMonopolyGame>();

                game.Banker = kernel.Get<IBanker>();
                game.Dice = kernel.Get<IDice>();
            }

            player = game.Players.First();
        }

        [TestCase(Description = "User Story: Roll doubles 3 times in a row, never pass or land on go. Balance is unchanged. Player is in Jail.")]
        public void TestGoToJailForTripleDoubles()
        {
            Mock<IDice> mock = new Mock<IDice>();
            mock.Setup(s => s.RollDie()).Returns(2);

            player.DiceOutcome.ConsecutiveDoublesRolled = 3;
            player.JailCell.GoToJailForThreeDoubles(player);

            Assert.IsTrue(player.IsInJail());
        }

        [Test]
        public void TestLeaveJailForRollingDoubles()
        {
            player.JailCell.GoToJail(player);

            Mock<IDice> mock = new Mock<IDice>();
            mock.Setup(s => s.RollDie()).Returns(2);

            game.Dice = mock.Object;
            game.PlayerRollEvent(player);

            Assert.IsFalse(player.IsInJail());
            Assert.AreEqual(14, player.CurrentPosition);
        }

        [TestCase(20, 5, 5, true, Description = "User Story: Roll doubles, land on Go To Jail, player is in Jail, turn is over, balance is unchanged")]
        [TestCase(25, 2, 3, false)]
        public void TestGoToJail(int initialPosition, int die1, int die2, bool isRollDoubles)
        {
            int playerBalance = player.Cash;

            player.CurrentPosition = initialPosition;

            Mock<IDice> mock = new Mock<IDice>();
            mock.SetupSequence(s => s.RollDie()).Returns(die1).Returns(die2);

            game.Dice = mock.Object;
            game.PlayerRollEvent(player);

            Assert.IsTrue(player.IsInJail());
            Assert.AreEqual(player.CurrentPosition, (int)Monopoly.Game.MonopolyBoard.BoardSpace.SpaceKeys.Jail);
            Assert.AreEqual(Monopoly.Game.Players.Player.STARTING_CASH, playerBalance);
        }

        [Test]
        public void TestPayToGetOutOfJail()
        {
            int balance = player.Cash;

            player.JailCell.GoToJail(player);
            player.JailCell.JailedPlayersSentence[player] = 3;

            Mock<IDice> mock = new Mock<IDice>();
            mock.SetupSequence(s => s.RollDie()).Returns(4).Returns(6);

            game.Dice = mock.Object;
            game.PlayerRollEvent(player);

            Assert.AreEqual(20, player.CurrentPosition);
            Assert.AreEqual(balance - Jail.JAIL_BAIL, player.Cash);
            Assert.IsFalse(player.IsInJail());
        }
    }
}