using Monopoly.Game.Players;

namespace Monopoly.Game.Bank
{
    public interface IBanker
    {
        void MoveCashFromBuyerToOwner(IPlayer buyer, IPlayer owner, int rent, string spaceName);
        void Pay(IPlayer player, int amount, string message);
    }
}