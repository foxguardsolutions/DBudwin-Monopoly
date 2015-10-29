namespace Monopoly.Game.MonopolyBoard
{
    public interface IBoardSpace
    {
        string Name { get; set; }
        BoardSpace.SpaceKeys Position { get; set; }
    }
}
