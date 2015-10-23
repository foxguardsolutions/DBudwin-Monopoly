using Monopoly.Random;

namespace Monopoly.Game
{
    public class Player : IPlayer
    {
        public const int STARTING_CASH = 400;

        private int currentPosition;
        public int PreviousPosition { get; set; }
        public int MostRecentRoll { get; set; }
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
            Cash = STARTING_CASH;
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
            MostRecentRoll = RollDie() + RollDie();

            return MostRecentRoll;
        }
    }
}
