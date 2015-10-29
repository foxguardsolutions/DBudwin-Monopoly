namespace Monopoly.Game.GamePlay
{
    public interface IDiceOutcomeHandler
    {
        bool WereDoublesRolled { get; set; }
        int RollValue { get; set; }
        int ConsecutiveDoublesRolled { get; set; }
        void HandleDoubles(int die1, int die2);
        int RollDice(int die1, int die2);
    }
}
