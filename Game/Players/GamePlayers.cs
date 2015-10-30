using System.Collections.Generic;

namespace Monopoly.Game.Players
{
    public class GamePlayers : IGamePlayers
    {
        public IEnumerable<IPlayer> AllPlayers { get; set; }

        private IPlayerFactory factory;

        public GamePlayers(IPlayerFactory factory)
        {
            this.factory = factory;

            AllPlayers = factory.CreateAll();
        }
    }
}
