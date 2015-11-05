using Monopoly.Game.Cards;
using Monopoly.Game.Players;

namespace Monopoly.Game.MonopolyBoard
{
    public interface ICardSpaceActions
    {
        ICardManager<IChanceCard> ChanceManager { get; set; }
        ICardManager<ICommunityChestCard> CommunityChestManager { get; set; }
        void DrawCommunityChestCard(IPlayer player);
        void DrawChanceCard(IPlayer player);
    }
}
