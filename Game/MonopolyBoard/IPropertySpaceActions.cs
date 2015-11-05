using Monopoly.Game.Players;

namespace Monopoly.Game.MonopolyBoard
{
    public interface IPropertySpaceActions
    {
        void CheckIfPassGo(IPlayer player);
        void GoToJail(IPlayer player);
        void PayIncomeTax(IPlayer player);
        void PayLuxuryTax(IPlayer player);
        void EmptyAction(IPlayer player);
    }
}
