using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Game.Properties
{
    public class PropertyGroup : IPropertyGroup
    {
        public enum Groups
        {
            Utilities,
            Railroads,
            Brown,
            LightBlue,
            Pink,
            Orange,
            Red,
            Yellow,
            Green,
            DarkBlue
        }

        public static IEnumerable<RealEstateSpace> GetAllPropertiesInGroup(IBoard board, Groups group)
        {
            return board.Spaces.Select(space => space as RealEstateSpace).Where(realEstateSpace => realEstateSpace?.Group == group);
        }
    }
}