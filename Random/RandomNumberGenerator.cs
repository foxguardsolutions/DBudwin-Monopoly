namespace Monopoly.Random
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly System.Random random = new System.Random();

        public int Generate(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
