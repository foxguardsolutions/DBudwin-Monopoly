using System;
using Monopoly.Game.GamePlay;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public class MonopolyGame : IMonopolyGame
    {
        public IGamePlayers Players { get; }
        public IBoardManager Manager { get; }
        public IDice Dice { get; set; }
        public int RoundsPlayed { get; set; }

        public MonopolyGame(IBoardManager manager, IGamePlayers players)
        {
            Manager = manager;
            Players = players;
        }

        public void PlayRound()
        {
            foreach (IPlayer p in Players.AllPlayers)
            {
                PlayRoundForPlayer(p);
            }

            RoundsPlayed++;
        }

        private void PlayRoundForPlayer(IPlayer player)
        {
            if (player.Cash <= 0)
            {
                return;
            }

            Console.WriteLine("On round {0}, \"{1}\" started at \"{2}\" with ${3} and...", RoundsPlayed, player.Name, Manager.Board.GetSpaceAt(player.CurrentPosition).Name, player.Cash);

            PlayerRollEvent(player);

            Console.WriteLine("    Finished on \"{0}\" with ${1}", Manager.Board.GetSpaceAt(player.CurrentPosition).Name, player.Cash);
        }

        public void PlayerRollEvent(IPlayer player)
        {
            int die1 = Dice.RollDie();
            int die2 = Dice.RollDie();

            player.DiceOutcome.RollDice(die1, die2);

            if (player.Cash <= 0)
            {
                return;
            }

            bool playerInJailAtBeginningOfTurn = player.IsInJail();

            player.TakeTurn(player.DiceOutcome.RollValue);

            Console.WriteLine("    Rolled {0} moving to \"{1}\"", player.DiceOutcome.RollValue, Manager.Board.GetSpaceAt(player.CurrentPosition).Name);

            if (!playerInJailAtBeginningOfTurn)
            {
                Manager.EvaluateBoardSpaceOutcome(player);
                RollAgainIfDoublesRolled(player);
            }
            else
            {
                OutOfJailAdvance(player, player.DiceOutcome.RollValue);
            }
        }

        public void OutOfJailAdvance(IPlayer player, int roll)
        {
            player.TakeTurn(roll);

            Manager.EvaluateBoardSpaceOutcome(player);
        }

        public void RollAgainIfDoublesRolled(IPlayer player)
        {
            if (player.DiceOutcome.WereDoublesRolled && !player.IsInJail())
            {
                Console.WriteLine("    Rolled double {0}'s and is rolling again", player.DiceOutcome.RollValue / 2);

                PlayerRollEvent(player);
            }
        }
    }
}
