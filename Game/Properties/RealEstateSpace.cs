using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.Properties
{
    public abstract class RealEstateSpace : BoardSpace
    {
        private IPlayer owner;
        public PropertyColorGroup.Groups Group { get; }
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

        protected RealEstateSpace(string name, SpaceKeys position, PropertyColorGroup.Groups group, int cost, int rent)
        {
            Name = name;
            Position = position;
            Group = group;
            Cost = cost;
            Rent = rent;
        }
    }
}
