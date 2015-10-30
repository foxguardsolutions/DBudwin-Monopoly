using System;
using Monopoly.Game.Players;

namespace Monopoly.Game.Bank
{
    public class Banker : IBanker
    {
        public void MoveCashFromBuyerToOwner(IPlayer buyer, IPlayer owner, int rent, string spaceName)
        {
            if (buyer.Cash >= rent)
            {
                buyer.Cash -= rent;
                owner.Cash += rent;

                Console.WriteLine("    Paid rent on \"{0}\" to {1} for ${2}.", spaceName, owner.Name, rent);
            }
            else
            {
                owner.Cash += buyer.Cash;
                buyer.Cash = 0;

                Console.WriteLine("    Loses since they couldn't pay all the rent to {0}", owner.Name);
            }
        }

        public void Pay(IPlayer player, int amount, string message)
        {
            player.Cash -= amount;

            if (player.Cash >= amount)
            {
                Console.WriteLine("    {0}", message);
            }
            else
            {
                Console.WriteLine("    {0} loses since they couldn't pay {1}.  {2}", player.Name, amount, message);
            }
        }
    }
}