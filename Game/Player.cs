using Monopoly.Random;

namespace Monopoly.Game
{
    public class Player : IPlayer
    {
        public const int STARTING_CASH = 400;

        private int currentPosition;
        private bool isIncarcerated;
        public int PreviousPosition { get; set; }
        public int MostRecentRoll { get; set; }
        public int Cash { get; set; }
        public int RoundsPlayed { get; set; }
        public int RoundsInJail { get; set; }
        public IRandomNumberGenerator Generator { get; }

        public bool IsIncarcerated
        {
            get
            {
                return isIncarcerated;
            }
            set
            {
                isIncarcerated = value;

                if (isIncarcerated)
                {
                    DoublesCounter = 0;
                }
            }
        }

        public int DoublesCounter { get; set; }

        public int CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                UpdatePlayerPosition(value);
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

            if (DoublesCounter == 0)
            {
                RoundsPlayed++;
            }
        }

        public int RollDie()
        {
            return Generator.Generate(1, MonopolyGame.NUMBER_OF_SIDES);
        }

        public int RollDice(int die1, int die2)
        {
            CheckForDoubles(die1, die2);

            MostRecentRoll = die1 + die2;

            return MostRecentRoll;
        }

        public void CheckForDoubles(int die1, int die2)
        {
            if (die1 == die2)
            {
                DoublesCounter++;
            }
            else
            {
                DoublesCounter = 0;
            }
        }

        private void UpdatePlayerPosition(int newPosition)
        {
            if (!IsIncarcerated)
            {
                PreviousPosition = currentPosition;
                currentPosition = newPosition % Board.NUMBER_OF_SPACES;
            }
            else
            {
                RoundsPlayed++;
            }
        }

        public void LeaveJail()
        {
            IsIncarcerated = false;
            DoublesCounter = 0;
            RoundsInJail = 0;
        }
    }
}
