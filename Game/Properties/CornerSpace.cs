using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public class CornerSpace : BoardSpace, IActionSpace
    {
        public Action<IPlayer> SpaceAction { get; set; }

        public CornerSpace(string name, Action<IPlayer> spaceAction, SpaceKeys position)
        {
            Name = name;
            SpaceAction = spaceAction;
            Position = position;
        }
    }
}
