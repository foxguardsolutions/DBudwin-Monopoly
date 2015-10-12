namespace Monopoly
{
    public interface IPlayer
    {
        int RoundsPlayed { get; }
        IRandomNumberGenerator Generator { get; }
        string Name { get; }
        int CurrentPosition { get; set; }

        void TakeTurn();
        int RollDie();
        int RollDice();
        void PrintTurnSummary(int rollValue, int newSpace);
    }
}
