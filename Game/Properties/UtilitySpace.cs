namespace Monopoly.Game.Properties
{
    public class UtilitySpace : RealEstateSpace
    {
        public UtilitySpace(string name, SpaceKeys position, PropertyColorGroup.Groups group, int cost, int rent)
            : base(name, position, group, cost, rent)
        {
        }
    }
}
