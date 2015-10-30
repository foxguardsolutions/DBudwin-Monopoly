using System;
using System.Collections.Generic;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;

namespace Monopoly.Game.GamePlay
{
    public class Jail : IJail
    {
        public const int JAIL_BAIL = 50;

        public Dictionary<IPlayer, int> JailedPlayersSentence { get; set; }

        public Jail()
        {
            JailedPlayersSentence = new Dictionary<IPlayer, int>();
        }

        public void TakeTurn(IPlayer player)
        {
            GoToJailForThreeDoubles(player);
            LeaveJailForRollingDoubles(player);
            PayBailIfInJailForThreeRounds(player);

            List<IPlayer> jailedPlayersList = new List<IPlayer>(JailedPlayersSentence.Keys);

            foreach (IPlayer jailedPlayer in jailedPlayersList)
            {
                JailedPlayersSentence[jailedPlayer]++;
            }
        }

        public void GoToJailForThreeDoubles(IPlayer player)
        {
            if (player.DiceOutcome.ConsecutiveDoublesRolled == 3)
            {
                Console.WriteLine("    Rolled triples three times in a row and is going to jail");

                GoToJail(player);
            }
        }

        public void LeaveJailForRollingDoubles(IPlayer player)
        {
            if (player.DiceOutcome.WereDoublesRolled && player.IsInJail())
            {
                Console.WriteLine("    While in jail, rolled double {0}'s and is leaving jail", player.DiceOutcome.RollValue / 2);

                LeaveJail(player);
            }
        }

        public void GoToJail(IPlayer player)
        {
            player.CurrentPosition = (int)BoardSpace.SpaceKeys.Jail;

            JailedPlayersSentence.Add(player, 0);

            Console.Write("    Is in jail");
        }

        public void LeaveJail(IPlayer player)
        {
            player.DiceOutcome.ConsecutiveDoublesRolled = 0;

            JailedPlayersSentence.Remove(player);
        }

        public void PayBailIfInJailForThreeRounds(IPlayer player)
        {
            if (JailSentenceForPlayer(player) == 3 && player.Cash >= JAIL_BAIL)
            {
                Console.WriteLine("    Paid ${0} to leave jail", JAIL_BAIL);

                LeaveJail(player);

                player.Cash -= JAIL_BAIL;
            }
        }

        private int JailSentenceForPlayer(IPlayer player)
        {
            return JailedPlayersSentence.ContainsKey(player) ? JailedPlayersSentence[player] : 0;
        }

        public bool IsPlayerInJail(IPlayer player)
        {
            return JailedPlayersSentence.ContainsKey(player);
        }
    }
}