using System.Collections.Generic;
using Monopoly.Game.GamePlay;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;

namespace Monopoly.Game
{
    public interface IMonopolyGame
    {
        IGamePlayers Players { get; }
        IBoardManager Manager { get; }
        IDice Dice { get; set; }
        int RoundsPlayed { get; set; }

        void PlayRound();
        void PlayerRollEvent(IPlayer player);
        void RollAgainIfDoublesRolled(IPlayer player);
    }
}