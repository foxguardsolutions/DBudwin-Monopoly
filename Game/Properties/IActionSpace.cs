using System;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public interface IActionSpace
    {
        Action<IPlayer> SpaceAction { get; set; }
    }
}
