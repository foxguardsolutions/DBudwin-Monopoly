using System.Collections.Generic;
using System.Linq;
using Monopoly.Random;

namespace Monopoly.Game.Cards
{
    public class CardFactory : ICardFactory
    {
        private ICardActions actions;
        private IRandomNumberGenerator generator;

        public CardFactory(ICardActions actions, IRandomNumberGenerator generator)
        {
            this.actions = actions;
            this.generator = generator;
        }

        public List<ICommunityChestCard> CreateCommunityChestCards()
        {
            List<ICommunityChestCard> communityChestCards = new List<ICommunityChestCard>();

            communityChestCards.Add(new Card("Advance to Go", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Go, 0));
            communityChestCards.Add(new Card("Bank error in your favor", actions.CollectMoneyFromBank, 0, 200));
            communityChestCards.Add(new Card("Doctor's fees", actions.PayBank, 0, 50));
            communityChestCards.Add(new Card("Sale of Stock", actions.CollectMoneyFromBank, 0, 50));
            communityChestCards.Add(new Card("Get Out of Jail Free", actions.GetOutOfJailForFree, 0, 0));
            communityChestCards.Add(new Card("Go to Jail", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Jail, 0));
            communityChestCards.Add(new Card("Grand Opera Night", actions.CollectMoneyFromPlayers, 0, 50));
            communityChestCards.Add(new Card("Holiday Fund matures", actions.CollectMoneyFromBank, 0, 100));
            communityChestCards.Add(new Card("Income tax refund", actions.CollectMoneyFromBank, 0, 100));
            communityChestCards.Add(new Card("It is your birthday", actions.CollectMoneyFromPlayers, 0, 10));
            communityChestCards.Add(new Card("Life insurance matures", actions.CollectMoneyFromBank, 0, 100));
            communityChestCards.Add(new Card("Pay hospital fees", actions.PayBank, 0, 100));
            communityChestCards.Add(new Card("Pay school fees", actions.PayBank, 0, 150));
            communityChestCards.Add(new Card("Receive $25 consultancy fee", actions.CollectMoneyFromBank, 0, 25));
            communityChestCards.Add(new Card("You have won second prize in a beauty contest", actions.CollectMoneyFromBank, 0, 10));
            communityChestCards.Add(new Card("You inherit $100", actions.CollectMoneyFromBank, 0, 100));

            return communityChestCards.OrderBy(c => generator.Generate(1, communityChestCards.Count())).ToList();
        }

        public List<IChanceCard> CreateChanceCards()
        {
            List<IChanceCard> chanceCards = new List<IChanceCard>();

            chanceCards.Add(new Card("Advance to Go", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Go, 0));
            chanceCards.Add(new Card("Advance to Illinois Ave.", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Illinois, 0));
            chanceCards.Add(new Card("Advance to St. Charles Place", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.StCharles, 0));
            chanceCards.Add(new Card("Advance token to nearest Utility", actions.MoveToNearestUtility, 0, 0));
            chanceCards.Add(new Card("Advance token to the nearest Railroad", actions.MoveToNearestRailroad, 0, 0));
            chanceCards.Add(new Card("Bank pays you dividend of $50", actions.CollectMoneyFromBank, 0, 50));
            chanceCards.Add(new Card("Get out of Jail Free", actions.GetOutOfJailForFree, 0, 0));
            chanceCards.Add(new Card("Go Back 3 Spaces", actions.MoveBackThreeSpaces, 0, 0));
            chanceCards.Add(new Card("Go to Jail – Go directly to Jail – Do not pass Go, do not collect $200", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Jail, 0));
            chanceCards.Add(new Card("Pay poor tax of $15", actions.PayBank, 0, 15));
            chanceCards.Add(new Card("Take a trip to Reading Railroad", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.ReadingRR, 0));
            chanceCards.Add(new Card("Take a walk on the Boardwalk – Advance token to Boardwalk", actions.MovePlayerTo, MonopolyBoard.BoardSpace.SpaceKeys.Boardwalk, 0));
            chanceCards.Add(new Card("You have been elected Chairman of the Board – Pay each player $50", actions.PayPlayers, 0, 50));
            chanceCards.Add(new Card("Your building and loan matures – Collect $150", actions.CollectMoneyFromBank, 0, 150));
            chanceCards.Add(new Card("You have won a crossword competition", actions.CollectMoneyFromBank, 0, 100));

            return chanceCards.OrderBy(c => generator.Generate(1, chanceCards.Count())).ToList();
        }
    }
}
