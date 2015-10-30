using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public class CardManager<T> : ICardManager<T>
        where T : ICard
    {
        public List<T> Cards { get; }

        public CardManager(List<T> cards)
        {
            Cards = cards;
        }

        public void PlayCard(IPlayer player)
        {
            T card = Cards.First();

            Console.WriteLine("    Drew a card that said \"{0}\"", card.CardText);

            card.CardAction.Invoke(player, card);

            PlaceOnBottomOfDeck(card);
        }

        private void PlaceOnBottomOfDeck(T card)
        {
            Cards.Remove(card);
            Cards.Add(card);
        }
    }
}
