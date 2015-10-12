using System;

namespace Monopoly
{
    public class Player
    {
        private int currentPosition;
        public int RoundsPlayed { get; private set; }
        private IRandomNumberGenerator Generator { get; }

        public int CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value % Board.NUMBER_OF_SPACES; }
        }

        public string Name { get; private set; }

        public Player(string name, IRandomNumberGenerator generator)
        {
            Name = name;
            Generator = generator;
        }

        public void TakeTurn()
        {
            int rollValue = Generator.RollDice();

            CurrentPosition += rollValue;
            RoundsPlayed++;

            PrintTurnSummary(rollValue, CurrentPosition);
        }

        public void PrintTurnSummary(int rollValue, int newSpace)
        {
            if (MonopolyGame.Board != null)
            {
                Console.WriteLine("On round {0}, \"{1}\" rolled a {2} moving to \"{3}\"", RoundsPlayed, Name, rollValue, MonopolyGame.Board.Spaces[CurrentPosition].Name);
            }
        }
    }
}
