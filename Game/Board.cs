using System.Collections.Generic;

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
    }
}
