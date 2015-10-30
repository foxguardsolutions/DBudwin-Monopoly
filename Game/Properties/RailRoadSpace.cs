namespace Monopoly.Game.Properties
{
    public class RailroadSpace : RealEstateSpace
    {
        public RailroadSpace(string name, SpaceKeys position, PropertyGroup.Groups group, int cost, int rent)
            : base(name, position, group, cost, rent)
        {
        }
    }
}
