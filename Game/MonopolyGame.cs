using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly.Game
{
    public class MonopolyGame : IMonopolyGame
    {
        public const int NUMBER_OF_SIDES = 6;

        public IEnumerable<IPlayer> Players { get; }
        public IBoard Board { get; }

        public MonopolyGame(IBoard board, IEnumerable<IPlayer> players)
        {
            Board = board;
            Players = players;
        }

        public void PlayRound()
        {
            foreach (IPlayer p in Players)
            {
                TakeTurn(p, p.RollDice());
            }
        }

        public void TakeTurn(IPlayer player, int roll)
        {
            player.TakeTurn(roll);
            EvaluateTurnOutcome(player);
            PrintTurnSummary(player);
        }

        public void EvaluateTurnOutcome(IPlayer player)
        {
            CheckIfPassGo(player);

            switch (player.CurrentPosition)
            {
                case (int)BoardSpace.SpaceKeys.GoToJail:
                    GoToJail(player);
                    break;
                case (int)BoardSpace.SpaceKeys.IncomeTax:
                    PayIncomeTax(player);
                    break;
                case (int)BoardSpace.SpaceKeys.LuxuryTax:
                    PayLuxuryTax(player);
                    break;
            }
        }

        public void CheckIfPassGo(IPlayer player)
        {
            if (player.CurrentPosition < player.PreviousPosition)
            {
                player.Cash += 200;
            }
        }

        public void GoToJail(IPlayer player)
        {
            player.CurrentPosition = (int)BoardSpace.SpaceKeys.Jail;
        }

        public void PayIncomeTax(IPlayer player)
        {
            player.Cash -= Math.Min((int)(player.Cash * 0.2), 200);
        }

        public void PayLuxuryTax(IPlayer player)
        {
            player.Cash -= 75;
        }

        public void PrintTurnSummary(IPlayer player)
        {
            Console.WriteLine("On round {0}, \"{1}\" rolled a {2} moving to \"{3}\"", player.RoundsPlayed, player.Name, Math.Abs(player.CurrentPosition - player.PreviousPosition), Board.Spaces.ElementAt(player.CurrentPosition).Name);
        }
    }
}
