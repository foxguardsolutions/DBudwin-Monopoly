using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Random;

namespace Monopoly.Game
{
    public class PlayerFactory : IPlayerFactory
    {
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 8;

        private readonly IRandomNumberGenerator generator;
        private IEnumerable<string> playerNames;

        public PlayerFactory(IEnumerable<string> playerNames, IRandomNumberGenerator generator)
        {
            this.playerNames = playerNames;
            this.generator = generator;
        }

        public IEnumerable<IPlayer> CreateAll()
        {
            ValidateNumberOfPlayers(playerNames);

            playerNames = RandomizePlayerOrder(playerNames);

            IEnumerable<IPlayer> players = playerNames.Select(playerName => new Player(playerName, generator)).ToList();

            return players;
        }

        public void ValidateNumberOfPlayers(IEnumerable<string> players)
        {
            if (!(players.Count() >= MIN_PLAYERS && players.Count() <= MAX_PLAYERS))
            {
                throw new Exception("Illegal number of players: " + players.Count());
            }
        }

        public IEnumerable<string> RandomizePlayerOrder(IEnumerable<string> players)
        {
            IEnumerable<string> randomOrderOfPlayers = players.OrderBy(c => generator.Generate(1, players.Count())).ToList();

            if (players.SequenceEqual(randomOrderOfPlayers))
            {
                randomOrderOfPlayers = RandomizePlayerOrder(randomOrderOfPlayers);
            }

            return randomOrderOfPlayers;
        }
    }
}
