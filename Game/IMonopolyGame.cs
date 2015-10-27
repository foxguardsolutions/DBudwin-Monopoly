using System.Collections.Generic;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public interface IMonopolyGame
    {
        IEnumerable<IPlayer> Players { get; }
        IBoard Board { get; }

        void PlayRound();
        void PlayerRollEvent(IPlayer player, int roll);
        void EvaluateRollOutcome(IPlayer player);
        void CheckPlayersCurrentSpaceAvailability(IPlayer player);
        void PurchaseCurrentSpace(IPlayer player, RealEstateSpace space);
        void PayRent(IPlayer player, RealEstateSpace space);
        int DetermineRent(IPlayer player, RealEstateSpace space);
        int DetermineUtilityRent(int rollValue);
        int DetermineRailroadRent(RailroadSpace space);
        int DeterminePropertyRent(PropertySpace space);
        void MoveCashFromBuyerToOwner(IPlayer buyer, IPlayer owner, int rent, string spaceName);
        void RollAgainIfDoublesRolled(IPlayer player);
        void SendPlayerToJailForThreeDoubles(IPlayer player);
        void GoToJail(IPlayer player);
    }
}