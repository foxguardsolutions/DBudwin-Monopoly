using Monopoly.Game.Cards;
using Monopoly.Game.Players;

namespace Monopoly.Game.MonopolyBoard
{
    public class CardSpaceActions : ICardSpaceActions
    {
        public ICardManager<IChanceCard> ChanceManager { get; set; }
        public ICardManager<ICommunityChestCard> CommunityChestManager { get; set; }

        public CardSpaceActions(ICardManager<IChanceCard> chanceManager, ICardManager<ICommunityChestCard> communityChestManager)
        {
            ChanceManager = chanceManager;
            CommunityChestManager = communityChestManager;
        }

        public void DrawCommunityChestCard(IPlayer player)
        {
            CommunityChestManager.PlayCard(player);
        }

        public void DrawChanceCard(IPlayer player)
        {
            ChanceManager.PlayCard(player);
        }
    }
}
