using Monopoly.Random;

namespace Monopoly.Game
{
    public interface IPlayer
    {
        int RoundsPlayed { get; }
        IRandomNumberGenerator Generator { get; }
        string Name { get; }
        int CurrentPosition { get; set; }
        int PreviousPosition { get; set; }
        int MostRecentRoll { get; set; }
        int Cash { get; set; }

        void TakeTurn(int rollValue);
        int RollDie();
        int RollDice();
    }
}
