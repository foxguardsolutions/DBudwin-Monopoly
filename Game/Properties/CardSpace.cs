using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public class CardSpace : BoardSpace, IActionSpace
    {
        public Action<IPlayer> SpaceAction { get; set; }

        public CardSpace(string name, Action<IPlayer> spaceAction, SpaceKeys position)
        {
            Name = name;
            SpaceAction = spaceAction;
            Position = position;
        }
    }
}
