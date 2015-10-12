using System.Collections.Generic;

namespace Monopoly
{
    public interface IBoardSpacesFactory
    {
        IEnumerable<IBoardSpace> CreateAll();
    }
}
