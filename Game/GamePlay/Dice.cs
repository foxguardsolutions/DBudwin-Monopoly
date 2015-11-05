using Monopoly.Random;

namespace Monopoly.Game.GamePlay
{
    public class Dice : IDice
    {
        public const int NUMBER_OF_SIDES = 6;
        public IRandomNumberGenerator Generator { get; }

        public Dice(IRandomNumberGenerator generator)
        {
            Generator = generator;
        }

        public int RollDie()
        {
            return Generator.Generate(1, NUMBER_OF_SIDES);
        }
    }
}