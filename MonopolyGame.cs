using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    public class MonopolyGame
    {
        public List<Player> Players { get; private set; }
        public static Board Board { get; private set; }

        public MonopolyGame(List<Player> players)
        {
            Board = new Board();

            AddPlayers(players);
        }

        private void AddPlayers(List<Player> players)
        {
            if (players.Count >= 2 && players.Count <= 8)
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
    }
}
