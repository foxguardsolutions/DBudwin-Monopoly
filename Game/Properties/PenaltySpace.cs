using System;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public class PenaltySpace : BoardSpace, IActionSpace
    {
        public Action<IPlayer> SpaceAction { get; set; }
        public int Cost { get; set; }

        public PenaltySpace(string name, Action<IPlayer> spaceAction, SpaceKeys position, int cost)
        {
            Name = name;
            SpaceAction = spaceAction;
            Position = position;
            Cost = cost;
        }
    }
}
