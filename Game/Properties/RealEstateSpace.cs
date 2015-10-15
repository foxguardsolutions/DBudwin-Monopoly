namespace Monopoly.Game.Properties
{
    public abstract class RealEstateSpace : BoardSpace
    {
        public int Cost { get; private set; }

        protected RealEstateSpace(string name, SpaceKeys position, int cost)
        {
            Name = name;
            Position = position;
            Cost = cost;
        }
    }
}
