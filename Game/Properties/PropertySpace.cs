namespace Monopoly.Game.Properties
{
    public class PropertySpace : RealEstateSpace
    {
        public PropertySpace(string name, SpaceKeys position, PropertyGroup.Groups group, int cost, int rent)
            : base(name, position, group, cost, rent)
        {
        }
    }
}
