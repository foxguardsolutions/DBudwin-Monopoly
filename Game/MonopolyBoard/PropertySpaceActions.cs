using System;
using Monopoly.Game.Bank;
using Monopoly.Game.Players;

namespace Monopoly.Game.MonopolyBoard
{
    public class PropertySpaceActions : IPropertySpaceActions
    {
        public const int PASS_GO_REWARD = 200;
        public const int INCOME_TAX_PENALTY = 200;
        public const int LUXURY_TAX_PENALTY = 75;

        public IBanker Banker { get; set; }

        public PropertySpaceActions(IBanker banker)
        {
            Banker = banker;
        }

        public void CheckIfPassGo(IPlayer player)
        {
            if (player.CurrentPosition < player.PreviousPosition)
            {
                player.Cash += PASS_GO_REWARD;

                Console.WriteLine("    Reached \"Go\" and collected ${0}", PASS_GO_REWARD);
            }
        }

        public void GoToJail(IPlayer player)
        {
            player.GoToJail();
        }

        public void PayIncomeTax(IPlayer player)
        {
            CheckIfPassGo(player);

            int penalty = Math.Min((int)(player.Cash * 0.2), INCOME_TAX_PENALTY);

            string message = "Landed on \"Income Tax\" and paid $" + penalty;

            Banker.Pay(player, penalty, message);
        }

        public void PayLuxuryTax(IPlayer player)
        {
            CheckIfPassGo(player);

            string message = "Landed on \"Luxury Tax\" and paid $" + LUXURY_TAX_PENALTY;

            Banker.Pay(player, LUXURY_TAX_PENALTY, message);
        }

        public void EmptyAction(IPlayer player)
        {
        }
    }
}
