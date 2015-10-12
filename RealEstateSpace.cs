namespace Monopoly
{
    public abstract class RealEstateSpace : BoardSpace
    {
        public int Cost { get; set; }

        protected RealEstateSpace(string name, int position, int cost)
        {
            Name = name;
            Position = position;
            Cost = cost;
        }
    }
}
