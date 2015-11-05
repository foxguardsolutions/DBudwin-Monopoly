using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public class CardActions : ICardActions
    {
        public IGamePlayers Players { get; set; }

        public CardActions(IGamePlayers players)
        {
            Players = players;
        }

        public void CollectMoneyFromBank(IPlayer player, ICard card)
        {
            player.Cash += card.CardValue;

            Console.WriteLine("    Collected ${0}", card.CardValue);
        }

        public void CollectMoneyFromPlayers(IPlayer player, ICard card)
        {
            foreach (IPlayer playerToCollectFrom in Players.AllPlayers)
            {
                if (!playerToCollectFrom.Equals(player))
                {
                    playerToCollectFrom.Cash -= card.CardValue;
                    player.Cash += card.CardValue;

                    Console.WriteLine("    \"{0}\" paid ${1} and now has ${2}", playerToCollectFrom.Name, card.CardValue, playerToCollectFrom.Cash);
                }
            }
        }

        public void PayBank(IPlayer player, ICard card)
        {
            player.Cash -= card.CardValue;

            Console.WriteLine("    Paid bank ${0}", card.CardValue);
        }

        public void PayPlayers(IPlayer player, ICard card)
        {
            foreach (IPlayer playerToPay in Players.AllPlayers)
            {
                if (!playerToPay.Equals(player))
                {
                    playerToPay.Cash += card.CardValue;
                    player.Cash -= card.CardValue;

                    Console.WriteLine("    Paid ${0} to \"{1}\" and now has ${2}", card.CardValue, playerToPay.Name, playerToPay.Cash);
                }
            }
        }

        public void MovePlayerTo(IPlayer player, ICard card)
        {
            player.CurrentPosition = (int)card.PropertyToMoveTo;

            if (card.PropertyToMoveTo == BoardSpace.SpaceKeys.Jail)
            {
                player.GoToJail();
            }
        }

        public void MoveToNearestUtility(IPlayer player, ICard card)
        {
            if (player.CurrentPosition >= (int)BoardSpace.SpaceKeys.WaterWorks || player.CurrentPosition < (int)BoardSpace.SpaceKeys.ElectricCompany)
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.ElectricCompany;
            }
            else
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.WaterWorks;
            }
        }

        public void MoveToNearestRailroad(IPlayer player, ICard card)
        {
            if (player.CurrentPosition >= (int)BoardSpace.SpaceKeys.ShortLine || player.CurrentPosition < (int)BoardSpace.SpaceKeys.ReadingRR)
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.ReadingRR;
            }
            else if (player.CurrentPosition >= (int)BoardSpace.SpaceKeys.BORR)
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.ShortLine;
            }
            else if (player.CurrentPosition >= (int)BoardSpace.SpaceKeys.PennsylvaniaRR)
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.BORR;
            }
            else
            {
                player.CurrentPosition = (int)BoardSpace.SpaceKeys.PennsylvaniaRR;
            }
        }

        public void MoveBackThreeSpaces(IPlayer player, ICard card)
        {
            player.CurrentPosition -= 3;
        }

        public void GetOutOfJailForFree(IPlayer player, ICard card)
        {
            player.LeaveJail();
        }
    }
}
