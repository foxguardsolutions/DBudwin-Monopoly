using Monopoly.Random;

namespace Monopoly.Game.GamePlay
{
    public interface IDice
    {
        IRandomNumberGenerator Generator { get; }
        int RollDie();
    }
}