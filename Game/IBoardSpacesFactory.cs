using System.Collections.Generic;

namespace Monopoly.Game
{
    public interface IBoardSpacesFactory
    {
        IEnumerable<IBoardSpace> CreateAll();
    }
}
