namespace Monopoly.Game.Properties
{
    public abstract class RealEstateSpace : BoardSpace
    {
        private IPlayer owner;
        public PropertyGroup.Groups Group { get; }
        public int Cost { get; private set; }
        public int Rent { get; set; }

        public IPlayer Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
                IsOwned = true;
            }
        }

        public bool IsOwned { get; set; }

        protected RealEstateSpace(string name, SpaceKeys position, PropertyGroup.Groups group, int cost, int rent)
        {
            Name = name;
            Position = position;
            Group = group;
            Cost = cost;
            Rent = rent;
        }
    }
}
