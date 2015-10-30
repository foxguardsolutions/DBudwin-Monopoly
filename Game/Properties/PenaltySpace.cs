namespace Monopoly.Game.Properties
{
    public class PenaltySpace : BoardSpace
    {
        public int Cost { get; set; }

        public PenaltySpace(string name, SpaceKeys position, int cost)
        {
            Name = name;
            Position = position;
            Cost = cost;
        }
    }
}
