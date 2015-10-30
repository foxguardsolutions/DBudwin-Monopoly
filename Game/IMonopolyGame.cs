using System.Collections.Generic;

namespace Monopoly.Game
{
    public interface IMonopolyGame
    {
        IEnumerable<IPlayer> Players { get; }
        IBoard Board { get; }

        void PlayRound();
        void TakeTurn(IPlayer player, int roll);
        void EvaluateTurnOutcome(IPlayer player);
        void PrintTurnSummary(IPlayer player);
    }
}