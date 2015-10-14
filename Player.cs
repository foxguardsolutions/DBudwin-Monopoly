using System;
using System.Linq;

namespace Monopoly
{
    public class Player : IPlayer
    {
        private int currentPosition;
        public int RoundsPlayed { get; private set; }
        public IRandomNumberGenerator Generator { get; }

        public int CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value % Board.NUMBER_OF_SPACES; }
        }

        public string Name { get; }

        public Player(string name, IRandomNumberGenerator generator)
        {
            Name = name;
            Generator = generator;
        }

        public void TakeTurn()
        {
            int rollValue = RollDice();

            CurrentPosition += rollValue;
            RoundsPlayed++;

            PrintTurnSummary(rollValue, CurrentPosition);
        }

        public int RollDie()
        {
            return Generator.Generate(1, MonopolyGame.NUMBER_OF_SIDES);
        }

        public int RollDice()
        {
            return RollDie() + RollDie();
        }

        public void PrintTurnSummary(int rollValue, int newSpace)
        {
            if (MonopolyGame.Board != null)
            {
                Console.WriteLine("On round {0}, \"{1}\" rolled a {2} moving to \"{3}\"", RoundsPlayed, Name, rollValue, MonopolyGame.Board.Spaces.ElementAt(CurrentPosition).Name);
            }
        }
    }
}
