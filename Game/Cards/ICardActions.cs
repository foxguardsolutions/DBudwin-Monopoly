using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public interface ICardActions
    {
        void CollectMoneyFromBank(IPlayer player, ICard card);
        void CollectMoneyFromPlayers(IPlayer player, ICard card);
        void PayBank(IPlayer player, ICard card);
        void PayPlayers(IPlayer player, ICard card);
        void MovePlayerTo(IPlayer player, ICard card);
        void MoveToNearestUtility(IPlayer player, ICard card);
        void MoveToNearestRailroad(IPlayer player, ICard card);
        void MoveBackThreeSpaces(IPlayer player, ICard card);
        void GetOutOfJailForFree(IPlayer player, ICard card);
    }
}
