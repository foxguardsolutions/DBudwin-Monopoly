using System;

namespace Monopoly
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
<<<<<<< HEAD
        private const int NUMBER_OF_SIDES = 6;

=======
>>>>>>> 3268421... Monopoly release 1 commit
        private readonly Random random = new Random();

        public int Generate(int min, int max)
        {
            return random.Next(min, max + 1);
        }
<<<<<<< HEAD

        public int RollDice()
        {
            return Generate(1, NUMBER_OF_SIDES) + Generate(1, NUMBER_OF_SIDES);
        }
=======
>>>>>>> 3268421... Monopoly release 1 commit
    }
}
