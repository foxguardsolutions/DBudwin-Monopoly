using System;
using System.Collections.Generic;
using Monopoly.Game.Bank;
using Monopoly.Game.GamePlay;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public class MonopolyGame : IMonopolyGame
    {
        public const int PASS_GO_REWARD = 200;
        public const int INCOME_TAX_PENALTY = 200;
        public const int LUXURY_TAX_PENALTY = 75;

        public IEnumerable<IPlayer> Players { get; }
        public IPropertyManager Manager { get; }
        public IBanker Banker { get; set; }
        public IDice Dice { get; set; }
        public int RoundsPlayed { get; set; }

        public MonopolyGame(IPropertyManager manager, IEnumerable<IPlayer> players)
        {
            Manager = manager;
            Players = players;
        }

        public void PlayRound()
        {
            foreach (IPlayer p in Players)
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

            Console.WriteLine("On round {0} player \"{1}\"...", RoundsPlayed, player.Name);

            PlayerRollEvent(player);
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

            Console.WriteLine("    Rolled {0}", player.DiceOutcome.RollValue);

            bool playerInJailAtBeginningOfTurn = player.IsInJail();

            player.TakeTurn(player.DiceOutcome.RollValue);

            if (!playerInJailAtBeginningOfTurn)
            {
                EvaluateRollOutcome(player);
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

            EvaluateRollOutcome(player);
        }

        public void RollAgainIfDoublesRolled(IPlayer player)
        {
            if (player.DiceOutcome.WereDoublesRolled && !player.IsInJail())
            {
                Console.WriteLine("    Rolled double {0}'s and is rolling again", player.DiceOutcome.RollValue / 2);

                PlayerRollEvent(player);
            }
        }

        public void EvaluateRollOutcome(IPlayer player)
        {
            CheckIfPassGo(player);

            switch (player.CurrentPosition)
            {
                case (int)BoardSpace.SpaceKeys.GoToJail:
                    player.GoToJail();
                    break;
                case (int)BoardSpace.SpaceKeys.IncomeTax:
                    PayIncomeTax(player);
                    break;
                case (int)BoardSpace.SpaceKeys.LuxuryTax:
                    PayLuxuryTax(player);
                    break;
            }

            Manager.CheckPlayersCurrentSpaceAvailability(player);
        }

        public void CheckIfPassGo(IPlayer player)
        {
            if (player.CurrentPosition < player.PreviousPosition)
            {
                player.Cash += PASS_GO_REWARD;

                Console.WriteLine("    Reached \"Go\" and collected ${0}", PASS_GO_REWARD);
            }
        }

        public void PayIncomeTax(IPlayer player)
        {
            int penalty = Math.Min((int)(player.Cash * 0.2), INCOME_TAX_PENALTY);

            string message = "Landed on \"Income Tax\" and paid " + penalty;

            Banker.Pay(player, penalty, message);
        }

        public void PayLuxuryTax(IPlayer player)
        {
            string message = "Landed on \"Luxury Tax\" and paid " + LUXURY_TAX_PENALTY;

            Banker.Pay(player, LUXURY_TAX_PENALTY, message);
        }
    }
}
