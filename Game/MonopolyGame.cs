using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public class MonopolyGame : IMonopolyGame
    {
        public const int NUMBER_OF_SIDES = 6;
        public const int PASS_GO_REWARD = 200;
        public const int INCOME_TAX_PENALTY = 200;
        public const int LUXURY_TAX_PENALTY = 75;

        public IEnumerable<IPlayer> Players { get; }
        public IBoard Board { get; }

        public MonopolyGame(IBoard board, IEnumerable<IPlayer> players)
        {
            Board = board;
            Players = players;
        }

        public void PlayRound()
        {
            foreach (IPlayer p in Players)
            {
                TakeTurn(p, p.RollDice());
            }
        }

        public void TakeTurn(IPlayer player, int roll)
        {
            if (player.Cash > 0)
            {
                player.TakeTurn(roll);
                EvaluateTurnOutcome(player);
                PrintTurnSummary(player);
            }
        }

        public void EvaluateTurnOutcome(IPlayer player)
        {
            CheckIfPassGo(player);

            switch (player.CurrentPosition)
            {
                case (int)BoardSpace.SpaceKeys.GoToJail:
                    GoToJail(player);
                    break;
                case (int)BoardSpace.SpaceKeys.IncomeTax:
                    PayIncomeTax(player);
                    break;
                case (int)BoardSpace.SpaceKeys.LuxuryTax:
                    PayLuxuryTax(player);
                    break;
            }

            CheckPlayersCurrentSpaceAvailability(player);
        }

        public void CheckPlayersCurrentSpaceAvailability(IPlayer player)
        {
            IBoardSpace currentSpace = Board.SpaceAt(player.CurrentPosition);

            var realEstateSpace = currentSpace as RealEstateSpace;

            if (realEstateSpace != null)
            {
                BuyOrRent(player, realEstateSpace);
            }
        }

        private void BuyOrRent(IPlayer player, RealEstateSpace space)
        {
            if (space.IsOwned)
            {
                PayRent(player, space);
            }
            else
            {
                PurchaseCurrentSpace(player, space);
            }
        }

        public void PurchaseCurrentSpace(IPlayer player, RealEstateSpace space)
        {
            if (player.Cash >= space.Cost && !space.IsOwned)
            {
                player.Cash -= space.Cost;
                space.Owner = player;

                Console.WriteLine("{0} purchased \"{1}\" for ${2}.", player.Name, space.Name, space.Cost);
            }
        }

        public void PayRent(IPlayer player, RealEstateSpace space)
        {
            int totalRent = DetermineRent(player, space);

            if (space.Owner != player)
            {
                MoveCashFromBuyerToOwner(player, space.Owner, totalRent, space.Name);
            }
        }

        public void MoveCashFromBuyerToOwner(IPlayer buyer, IPlayer owner, int rent, string spaceName)
        {
            if (buyer.Cash >= rent)
            {
                buyer.Cash -= rent;
                owner.Cash += rent;

                Console.WriteLine("{0} paid rent on \"{1}\" to {2} for ${3}.", buyer.Name, spaceName, owner.Name, rent);
            }
            else
            {
                owner.Cash += buyer.Cash;
                buyer.Cash = 0;

                Console.WriteLine("{0} loses since they couldn't pay all the rent to {1}", buyer.Name, owner.Name);
            }
        }

        public int DetermineRent(IPlayer player, RealEstateSpace space)
        {
            if (space is UtilitySpace)
            {
                return DetermineUtilityRent(player.MostRecentRoll);
            }

            var railroadSpace = space as RailroadSpace;

            if (railroadSpace != null)
            {
                return DetermineRailroadRent(railroadSpace);
            }

            var propertySpace = space as PropertySpace;

            if (propertySpace != null)
            {
                return DeterminePropertyRent(propertySpace);
            }

            return 0;
        }

        public int DetermineUtilityRent(int rollValue)
        {
            IEnumerable<UtilitySpace> utilities = PropertyGroup.GetAllPropertiesInGroup(Board, PropertyGroup.Groups.Utilities).Cast<UtilitySpace>();

            return utilities.First().IsOwned && utilities.Last().IsOwned ? 10 * rollValue : 4 * rollValue;
        }

        public int DetermineRailroadRent(RailroadSpace space)
        {
            IEnumerable<RailroadSpace> railroads = PropertyGroup.GetAllPropertiesInGroup(Board, PropertyGroup.Groups.Railroads).Cast<RailroadSpace>();

            int ownedRailroads = railroads.Count(railroad => space.Owner == railroad.Owner);
            int currentRent = space.Rent;

            for (int i = 1; i < ownedRailroads; i++)
            {
                currentRent *= 2;
            }

            return currentRent;
        }

        public int DeterminePropertyRent(PropertySpace space)
        {
            IEnumerable<PropertySpace> properties = PropertyGroup.GetAllPropertiesInGroup(Board, space.Group).Cast<PropertySpace>();

            int currentRent = space.Rent;

            return properties.All(property => property.Owner == space.Owner) ? currentRent * 2 : currentRent;
        }

        public void CheckIfPassGo(IPlayer player)
        {
            if (player.CurrentPosition < player.PreviousPosition && player.RoundsPlayed > 0)
            {
                player.Cash += PASS_GO_REWARD;

                Console.WriteLine("{0} reached \"Go\" and collected ${1}", player.Name, PASS_GO_REWARD);
            }
        }

        public void GoToJail(IPlayer player)
        {
            player.CurrentPosition = (int)BoardSpace.SpaceKeys.Jail;

            Console.Write("{0} landed on \"Go To Jail\"", player.Name);
        }

        public void PayIncomeTax(IPlayer player)
        {
            int penalty = Math.Min((int)(player.Cash * 0.2), INCOME_TAX_PENALTY);

            if (penalty <= player.Cash)
            {
                player.Cash -= penalty;

                Console.Write("{0} landed on \"Income Tax\" and paid ${1}", player.Name, penalty);
            }
            else
            {
                player.Cash = 0;

                Console.WriteLine("{0} loses since they couldn't pay their income tax", player.Name);
            }
        }

        public void PayLuxuryTax(IPlayer player)
        {
            if (player.Cash <= LUXURY_TAX_PENALTY)
            {
                player.Cash -= LUXURY_TAX_PENALTY;

                Console.Write("{0} landed on \"Luxury Tax\" and paid ${1}", player.Name, LUXURY_TAX_PENALTY);
            }
            else
            {
                player.Cash = 0;

                Console.WriteLine("{0} loses since they couldn't pay their luxury tax", player.Name);
            }
        }

        public void PrintTurnSummary(IPlayer player)
        {
            Console.WriteLine("At end of round {0}, \"{1}\" rolled a {2} moving to \"{3}\" and has ${4}.", player.RoundsPlayed, player.Name, player.MostRecentRoll, Board.Spaces.ElementAt(player.CurrentPosition).Name, player.Cash);
        }
    }
}
