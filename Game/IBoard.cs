using System.Collections.Generic;

namespace Monopoly.Game
{
    public interface IBoard
    {
        IEnumerable<IBoardSpace> Spaces { get; }
        IBoardSpace SpaceAt(int position);
        IBoardSpace SpaceAt(BoardSpace.SpaceKeys position);
    }
}
