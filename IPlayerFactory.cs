using System.Collections.Generic;

namespace Monopoly
{
    public interface IPlayerFactory
    {
        IEnumerable<IPlayer> CreateAll(IRandomNumberGenerator generator);
    }
}
