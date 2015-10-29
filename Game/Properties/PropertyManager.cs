using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.Bank;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public class PropertyManager : IPropertyManager
    {
        public IBoard Board { get; }
        private IBanker banker;

        public PropertyManager(IBoard board, IBanker banker)
        {
            Board = board;
            this.banker = banker;
        }

        public void CheckPlayersCurrentSpaceAvailability(IPlayer player)
        {
            IBoardSpace currentSpace = Board.GetSpaceAt(player.CurrentPosition);

            var realEstateSpace = currentSpace as RealEstateSpace;

            if (realEstateSpace != null)
            {
                BuyOrRent(player, realEstateSpace);
            }
        }

        public void BuyOrRent(IPlayer player, RealEstateSpace space)
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

                Console.WriteLine("    Purchased \"{0}\" for ${1}.", space.Name, space.Cost);
            }
        }

        public void PayRent(IPlayer player, RealEstateSpace space)
        {
            int totalRent = DetermineRent(player, space);

            if (space.Owner != player)
            {
                banker.MoveCashFromBuyerToOwner(player, space.Owner, totalRent, space.Name);
            }
        }

        public int DetermineRent(IPlayer player, RealEstateSpace space)
        {
            if (space is UtilitySpace)
            {
                return DetermineUtilityRent(player.DiceOutcome.RollValue);
            }

            var railroadSpace = space as RailroadSpace;

            if (railroadSpace != null)
            {
                int ownedRailroads = NumberOfRailroadsOwnedByPlayer(player);

                return DetermineRailroadRent(railroadSpace, ownedRailroads);
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
            IEnumerable<UtilitySpace> utilities = PropertyColorGroup.GetAllPropertiesInGroup(Board, PropertyColorGroup.Groups.Utilities).Cast<UtilitySpace>();

            return utilities.First().IsOwned && utilities.Last().IsOwned ? 10 * rollValue : 4 * rollValue;
        }

        public int DetermineRailroadRent(RailroadSpace space, int numberOfOwnedRailroads)
        {
            int currentRent = space.Rent;

            for (int i = 1; i < numberOfOwnedRailroads; i++)
            {
                currentRent *= 2;
            }

            return currentRent;
        }

        public int NumberOfRailroadsOwnedByPlayer(IPlayer player)
        {
            IEnumerable<RailroadSpace> railroads = PropertyColorGroup.GetAllPropertiesInGroup(Board, PropertyColorGroup.Groups.Railroads).Cast<RailroadSpace>();

            return railroads.Count(railroad => player == railroad.Owner);
        }

        public int DeterminePropertyRent(PropertySpace space)
        {
            IEnumerable<PropertySpace> properties = PropertyColorGroup.GetAllPropertiesInGroup(Board, space.Group).Cast<PropertySpace>();

            int currentRent = space.Rent;

            return properties.All(property => property.Owner == space.Owner) ? currentRent * 2 : currentRent;
        }
    }
}