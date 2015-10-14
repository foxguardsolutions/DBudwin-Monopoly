using System.Collections.Generic;

namespace Monopoly
{
    public class Board
    {
        public const int NUMBER_OF_SPACES = 40;

        public IEnumerable<IBoardSpace> Spaces { get; }

        public Board()
        {
            BoardSpacesFactory factory = new BoardSpacesFactory();

            Spaces = factory.CreateAll();
        }
    }
}
