namespace Monopoly
{
    public class PenaltySpace : BoardSpace
    {
        public int Cost { get; set; }

        public PenaltySpace(string name, int position, int cost)
        {
            Name = name;
            Position = position;
            Cost = cost;
        }
    }
}
