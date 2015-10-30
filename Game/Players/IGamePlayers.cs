using System.Collections.Generic;

namespace Monopoly.Game.Players
{
    public interface IGamePlayers
    {
        IEnumerable<IPlayer> AllPlayers { get; set; }
    }
}
