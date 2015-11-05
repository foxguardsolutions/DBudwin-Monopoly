using System;
using System.Collections.Generic;
using System.Linq;
using Monopoly.Game.GamePlay;
using Monopoly.Random;

namespace Monopoly.Game.Players
{
    public class PlayerFactory : IPlayerFactory
    {
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 8;

        private readonly IDiceOutcomeHandler diceOutcome;
        private IEnumerable<string> playerNames;
        private IJail jailCell;
        private IRandomNumberGenerator generator;

        public PlayerFactory(IEnumerable<string> playerNames, IDiceOutcomeHandler diceOutcome, IJail jailCell, IRandomNumberGenerator generator)
        {
            this.playerNames = playerNames;
            this.diceOutcome = diceOutcome;
            this.jailCell = jailCell;
            this.generator = generator;
        }

        public IEnumerable<IPlayer> CreateAll()
        {
            ValidateNumberOfPlayers(playerNames);

            playerNames = RandomizePlayerOrder(playerNames);

            IEnumerable<IPlayer> players = playerNames.Select(playerName => new Player(playerName, jailCell, diceOutcome)).ToList();

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
