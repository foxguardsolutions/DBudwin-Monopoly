using System.Collections.Generic;
using Monopoly.Game.Bank;
using Monopoly.Game.GamePlay;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public interface IMonopolyGame
    {
        IEnumerable<IPlayer> Players { get; }
        IPropertyManager Manager { get; }
        IBanker Banker { get; set; }
        IDice Dice { get; set; }
        int RoundsPlayed { get; set; }

        void PlayRound();
        void PlayerRollEvent(IPlayer player);
        void EvaluateRollOutcome(IPlayer player);
        void RollAgainIfDoublesRolled(IPlayer player);
    }
}