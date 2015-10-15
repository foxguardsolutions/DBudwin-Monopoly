using Monopoly.Random;

namespace Monopoly.Game
{
    public class Player : IPlayer
    {
        private int currentPosition;
        public int PreviousPosition { get; set; }
        public int Cash { get; set; }
        public int RoundsPlayed { get; private set; }
        public IRandomNumberGenerator Generator { get; }

        public int CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                PreviousPosition = currentPosition;
                currentPosition = value % Board.NUMBER_OF_SPACES;
            }
        }

        public string Name { get; }

        public Player(string name, IRandomNumberGenerator generator)
        {
            Name = name;
            Generator = generator;
        }

        public void TakeTurn(int rollValue)
        {
            CurrentPosition += rollValue;
            RoundsPlayed++;
        }

        public int RollDie()
        {
            return Generator.Generate(1, MonopolyGame.NUMBER_OF_SIDES);
        }

        public int RollDice()
        {
            return RollDie() + RollDie();
        }
    }
}
