using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public class Card : ICommunityChestCard, IChanceCard
    {
        public string CardText { get; }
        public Action<IPlayer, ICard> CardAction { get; }
        public int CardValue { get; set; }
        public BoardSpace.SpaceKeys PropertyToMoveTo { get; set; }

        public Card(string cardText, Action<IPlayer, ICard> cardAction, BoardSpace.SpaceKeys propertyToMoveTo, int cardValue)
        {
            CardText = cardText;
            CardAction = cardAction;
            PropertyToMoveTo = propertyToMoveTo;
            CardValue = cardValue;
        }
    }
}
