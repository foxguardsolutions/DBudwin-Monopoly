using Monopoly.Game.GamePlay;

namespace Monopoly.Game.Players
{
    public interface IPlayer
    {
        IJail JailCell { get; }
        IDiceOutcomeHandler DiceOutcome { get; set; }
        string Name { get; }
        int CurrentPosition { get; set; }
        int PreviousPosition { get; set; }
        int Cash { get; set; }
        void TakeTurn(int rollValue);
        void GoToJail();
        void LeaveJail();
        bool IsInJail();
    }
}
