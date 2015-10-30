using System.Collections.Generic;
using Monopoly.Game.Players;

namespace Monopoly.Game.GamePlay
{
    public interface IJail
    {
        Dictionary<IPlayer, int> JailedPlayersSentence { get; set; }
        void TakeTurn(IPlayer player);
        void GoToJailForThreeDoubles(IPlayer player);
        void LeaveJailForRollingDoubles(IPlayer player);
        void GoToJail(IPlayer player);
        void LeaveJail(IPlayer player);
        void PayBailIfInJailForThreeRounds(IPlayer player);
        bool IsPlayerInJail(IPlayer player);
    }
}