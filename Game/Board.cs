using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Game
{
    public class Board : IBoard
    {
        public const int NUMBER_OF_SPACES = 40;

        public IEnumerable<IBoardSpace> Spaces { get; }

        public Board(IEnumerable<IBoardSpace> spaces)
        {
            Spaces = spaces;
        }

        public IBoardSpace SpaceAt(int position)
        {
            return Spaces.ElementAt(position);
        }

        public IBoardSpace SpaceAt(BoardSpace.SpaceKeys position)
        {
            return SpaceAt((int)position);
        }
    }
}
