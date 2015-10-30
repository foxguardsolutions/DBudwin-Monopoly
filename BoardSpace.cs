namespace Monopoly
{
    public abstract class BoardSpace : IBoardSpace
    {
        public string Name { get; set; }
        public int Position { get; set; }
    }
}
