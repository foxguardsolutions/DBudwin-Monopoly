using System.Collections.Generic;
using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public interface ICardManager<T>
        where T : ICard
    {
        List<T> Cards { get; }

        void PlayCard(IPlayer player);
    }
}
