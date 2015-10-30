namespace Monopoly.Game.Properties
{
    public class UtilitySpace : RealEstateSpace
    {
        public UtilitySpace(string name, SpaceKeys position, PropertyGroup.Groups group, int cost, int rent)
            : base(name, position, group, cost, rent)
        {
        }
    }
}
