namespace Monopoly.Game
{
    public interface IBoardSpace
    {
        string Name { get; set; }
        BoardSpace.SpaceKeys Position { get; set; }
    }
}
