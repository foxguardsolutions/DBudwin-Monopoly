using System;

namespace Monopoly
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random random = new Random();

        public int Generate(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
