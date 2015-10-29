using System.Collections.Generic;

namespace Monopoly.Game.MonopolyBoard
{
    public interface IBoard
    {
        IEnumerable<IBoardSpace> Spaces { get; }
        IBoardSpace GetSpaceAt(int position);
        IBoardSpace GetSpaceAt(BoardSpace.SpaceKeys position);
    }
}
