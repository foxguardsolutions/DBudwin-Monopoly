namespace Monopoly.Game.GamePlay
{
    public class DiceOutcomeHandler : IDiceOutcomeHandler
    {
        public bool WereDoublesRolled { get; set; }
        public int RollValue { get; set; }
        public int ConsecutiveDoublesRolled { get; set; }

        public int RollDice(int die1, int die2)
        {
            RollValue = die1 + die2;

            HandleDoubles(die1, die2);

            return RollValue;
        }

        public void HandleDoubles(int die1, int die2)
        {
            WereDoublesRolled = die1 == die2;

            if (WereDoublesRolled)
            {
                ConsecutiveDoublesRolled++;
            }
            else
            {
                ConsecutiveDoublesRolled = 0;
            }
        }
    }
}
