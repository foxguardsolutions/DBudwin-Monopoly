using System.Collections.Generic;

namespace Monopoly.Game.MonopolyBoard
{
    public interface IBoardSpacesFactory
    {
        IEnumerable<IBoardSpace> CreateAll();
    }
}
