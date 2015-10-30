using System.Collections.Generic;

namespace Monopoly.Game.Cards
{
    public interface ICardFactory
    {
        List<ICommunityChestCard> CreateCommunityChestCards();
        List<IChanceCard> CreateChanceCards();
    }
}
