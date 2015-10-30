using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public interface IPropertyManager
    {
        IBoard Board { get; }

        void CheckPlayersCurrentSpaceAvailability(IPlayer player);
        void BuyOrRent(IPlayer player, RealEstateSpace space);
        void PurchaseCurrentSpace(IPlayer player, RealEstateSpace space);
        void PayRent(IPlayer player, RealEstateSpace space);
        int DetermineRent(IPlayer player, RealEstateSpace space);
        int DetermineUtilityRent(int rollValue);
        int DetermineRailroadRent(RailroadSpace space, int numberOfOwnedRailroads);
        int DeterminePropertyRent(PropertySpace space);
    }
}