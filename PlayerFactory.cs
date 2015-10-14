using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IEnumerable<string> playerNames;

        public PlayerFactory(IEnumerable<string> playerNames)
        {
            this.playerNames = playerNames;
        }

        public IEnumerable<IPlayer> CreateAll(IRandomNumberGenerator generator)
        {
            return playerNames.Select(playerName => new Player(playerName, generator)).ToList();
        }
    }
}
