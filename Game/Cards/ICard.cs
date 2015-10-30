using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Cards
{
    public interface ICard
    {
        string CardText { get; }
        Action<IPlayer, ICard> CardAction { get; }
        int CardValue { get; set; }
        BoardSpace.SpaceKeys PropertyToMoveTo { get; set; }
    }
}
