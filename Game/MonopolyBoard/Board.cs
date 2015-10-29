using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Game.MonopolyBoard
{
    public class Board : IBoard
    {
        public const int NUMBER_OF_SPACES = 40;

        public IEnumerable<IBoardSpace> Spaces { get; }

        public Board(IEnumerable<IBoardSpace> spaces)
        {
            Spaces = spaces;
        }

        public IBoardSpace GetSpaceAt(int position)
        {
            return Spaces.ElementAt(position);
        }

        public IBoardSpace GetSpaceAt(BoardSpace.SpaceKeys position)
        {
            return GetSpaceAt((int)position);
        }
    }
}
