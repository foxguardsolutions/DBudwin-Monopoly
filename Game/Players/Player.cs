using Monopoly.Game.GamePlay;
using Monopoly.Game.MonopolyBoard;

namespace Monopoly.Game.Players
{
    public class Player : IPlayer
    {
        public const int STARTING_CASH = 400;

        private int currentPosition;
        public int PreviousPosition { get; set; }
        public int Cash { get; set; }
        public IJail JailCell { get; }
        public IDiceOutcomeHandler DiceOutcome { get; set; }

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

        public Player(string name, IJail jailCell, IDiceOutcomeHandler diceOutcome)
        {
            Name = name;
            JailCell = jailCell;
            DiceOutcome = diceOutcome;
            Cash = STARTING_CASH;
        }

        public void TakeTurn(int rollValue)
        {
            if (DiceOutcome.ConsecutiveDoublesRolled < 3)
            {
                CurrentPosition += rollValue;

                JailCell.TakeTurn(this);
            }
            else
            {
                GoToJail();
            }
        }

        private void UpdatePlayerPosition(int newPosition)
        {
            if (!IsInJail())
            {
                PreviousPosition = currentPosition;
                currentPosition = newPosition % Board.NUMBER_OF_SPACES;
            }
        }

        public void GoToJail()
        {
            JailCell.GoToJail(this);
        }

        public bool IsInJail()
        {
            return JailCell.IsPlayerInJail(this);
        }
    }
}
