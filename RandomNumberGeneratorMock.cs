namespace Monopoly
{
    public class RandomNumberGeneratorMock
    {
        private readonly IRandomNumberGenerator generator;

        private RandomNumberGeneratorMock()
        {
            generator = new RandomNumberGenerator();
        }

        public RandomNumberGeneratorMock(IRandomNumberGenerator generator)
        {
            this.generator = generator;
        }

        public int Generate(int min, int max)
        {
            return generator.Generate(min, max);
        }

        public int RollDice()
        {
            return generator.RollDice();
        }
    }
}
