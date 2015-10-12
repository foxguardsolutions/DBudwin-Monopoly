using System;

namespace Monopoly
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private const int NUMBER_OF_SIDES = 6;

        private readonly Random random = new Random();

        public int Generate(int min, int max)
        {
            return random.Next(min, max + 1);
        }

        public int RollDice()
        {
            return Generate(1, NUMBER_OF_SIDES) + Generate(1, NUMBER_OF_SIDES);
        }
    }
}
