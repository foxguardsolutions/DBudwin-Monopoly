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
        public const int JAIL_BAIL = 50;

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
                PlayRoundForPlayer(p);
            }
        }

        private void PlayRoundForPlayer(IPlayer player)
        {
            if (player.Cash > 0)
            {
                Console.WriteLine("On round {0} player \"{1}\"...", player.RoundsPlayed, player.Name);

                PlayerRollEvent(player, player.RollDice(player.RollDie(), player.RollDie()));
            }
        }

        public void PlayerRollEvent(IPlayer player, int roll)
        {
            if (player.Cash <= 0)
            {
                return;
            }

            Console.WriteLine("    Rolled {0}", roll);

            SendPlayerToJailForThreeDoubles(player);
            LeaveJailForRollingDoubles(player);

            player.TakeTurn(roll);

            EvaluateRollOutcome(player);
            RollAgainIfDoublesRolled(player);
            PayJailBondIfInJailFor3Rounds(player, roll);
        }

        public void PayJailBondIfInJailFor3Rounds(IPlayer player, int roll)
        {
            if (player.RoundsInJail == 3 && player.Cash >= JAIL_BAIL)
            {
                Console.WriteLine("    Paid {0} to leave jail", JAIL_BAIL);

                player.LeaveJail();
                player.Cash -= 50;

                PlayerRollEvent(player, roll);
            }
        }

        public void RollAgainIfDoublesRolled(IPlayer player)
        {
            if (player.DoublesCounter > 0)
            {
                Console.WriteLine("    Rolled double {0}'s and is rolling again", player.MostRecentRoll / 2);

                PlayerRollEvent(player, player.RollDice(player.RollDie(), player.RollDie()));
            }
        }

        public void LeaveJailForRollingDoubles(IPlayer player)
        {
            if (player.DoublesCounter > 0 && player.IsIncarcerated)
            {
                Console.WriteLine("    While in jail, rolled double {0}'s and is leaving jail", player.MostRecentRoll / 2);

                player.LeaveJail();
            }
        }

        public void SendPlayerToJailForThreeDoubles(IPlayer player)
        {
            if (player.DoublesCounter == 3)
            {
                Console.WriteLine("    Rolled triples three times in a row and is going to jail");

                GoToJail(player);
            }
        }

        public void EvaluateRollOutcome(IPlayer player)
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

                Console.WriteLine("    Purchased \"{0}\" for ${1}.", space.Name, space.Cost);
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

                Console.WriteLine("    Paid rent on \"{0}\" to {1} for ${2}.", spaceName, owner.Name, rent);
            }
            else
            {
                owner.Cash += buyer.Cash;
                buyer.Cash = 0;

                Console.WriteLine("    Loses since they couldn't pay all the rent to {0}", owner.Name);
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

                Console.WriteLine("    Reached \"Go\" and collected ${0}", PASS_GO_REWARD);
            }
        }

        public void GoToJail(IPlayer player)
        {
            player.CurrentPosition = (int)BoardSpace.SpaceKeys.Jail;
            player.IsIncarcerated = true;

            Console.Write("    Is in jail");
        }

        public void PayIncomeTax(IPlayer player)
        {
            int penalty = Math.Min((int)(player.Cash * 0.2), INCOME_TAX_PENALTY);

            if (penalty <= player.Cash)
            {
                player.Cash -= penalty;

                Console.Write("    Landed on \"Income Tax\" and paid ${0}", penalty);
            }
            else
            {
                player.Cash = 0;

                Console.WriteLine("    Loses since they couldn't pay their income tax");
            }
        }

        public void PayLuxuryTax(IPlayer player)
        {
            if (player.Cash <= LUXURY_TAX_PENALTY)
            {
                player.Cash -= LUXURY_TAX_PENALTY;

                Console.Write("    Landed on \"Luxury Tax\" and paid ${1}", LUXURY_TAX_PENALTY);
            }
            else
            {
                player.Cash = 0;

                Console.WriteLine("    Loses since they couldn't pay their luxury tax");
            }
        }
    }
}
