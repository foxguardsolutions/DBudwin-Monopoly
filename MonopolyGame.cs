using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class MonopolyGame
    {
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 8;
        public const int NUMBER_OF_SIDES = 6;

        public int RoundsToPlay { get; private set; }
        public List<IPlayer> Players { get; private set; }
        public static Board Board { get; private set; }

        public MonopolyGame(string[] args)
        {
            Setup(GeneratePlayerListFromArgs(args), ParseRoundsToPlay(args));
        }

        public MonopolyGame(IEnumerable<IPlayer> players, int roundsToPlay)
        {
            Setup(players, roundsToPlay);
        }

        private void Setup(IEnumerable<IPlayer> players, int roundsToPlay)
        {
            RoundsToPlay = roundsToPlay;

            Board = new Board();

            AddPlayers(players.ToList());
        }

        private void AddPlayers(List<IPlayer> players)
        {
            if (players.Count >= MIN_PLAYERS && players.Count <= MAX_PLAYERS)
            {
                Players = players;

                GeneratePlayerOrder();
            }
            else
            {
                throw new Exception("Illegal number of players: " + players.Count);
            }
        }

        private void GeneratePlayerOrder()
        {
            Random rand = new Random();

            Players = Players.OrderBy(c => rand.Next()).ToList();
        }

        public void PlayRound()
        {
            Players.ForEach(p => p.TakeTurn());
        }

        public IEnumerable<IPlayer> GeneratePlayerListFromArgs(string[] args)
        {
            IEnumerable<string> playerNames = args.Reverse().Skip(1);

            return new PlayerFactory(playerNames).CreateAll(new RandomNumberGenerator());
        }

        private int ParseRoundsToPlay(string[] args)
        {
            return int.Parse(args.Last());
        }
    }
}
