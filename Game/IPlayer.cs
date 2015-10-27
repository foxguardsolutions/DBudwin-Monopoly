using Monopoly.Random;

namespace Monopoly.Game
{
    public interface IPlayer
    {
        int RoundsPlayed { get; set; }
        IRandomNumberGenerator Generator { get; }
        string Name { get; }
        int CurrentPosition { get; set; }
        int PreviousPosition { get; set; }
        int MostRecentRoll { get; set; }
        int Cash { get; set; }
        bool IsIncarcerated { get; set; }
        int DoublesCounter { get; set; }
        int RoundsInJail { get; set; }

        void TakeTurn(int rollValue);
        int RollDie();
        int RollDice(int die1, int die2);
        void LeaveJail();
    }
}
