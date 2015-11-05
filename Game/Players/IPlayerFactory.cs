using System.Collections.Generic;

namespace Monopoly.Game.Players
{
    public interface IPlayerFactory
    {
        IEnumerable<IPlayer> CreateAll();
        void ValidateNumberOfPlayers(IEnumerable<string> players);
        IEnumerable<string> RandomizePlayerOrder(IEnumerable<string> players);
    }
}
