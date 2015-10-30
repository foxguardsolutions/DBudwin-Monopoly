using System.Collections.Generic;
using Monopoly.Game.Bank;
using Monopoly.Game.Cards;
using Monopoly.Game.GamePlay;
using Monopoly.Game.MonopolyBoard;
using Monopoly.Game.Players;
using Monopoly.Game.Properties;
using Monopoly.Random;
using Ninject;
using Ninject.Modules;

namespace Monopoly.Game
{
    public class MonopolyBindings : NinjectModule
    {
        private readonly IEnumerable<string> playerNames;

        public MonopolyBindings(IEnumerable<string> playerNames)
        {
            this.playerNames = playerNames;
        }

        public override void Load()
        {
            Bind<IPlayerFactory>().To<PlayerFactory>().InSingletonScope().WithConstructorArgument(playerNames);
            Bind<IBoardSpacesFactory>().To<BoardSpacesFactory>().InSingletonScope();
            Bind<IBoard>().ToMethod(c => new Board(c.Kernel.Get<IBoardSpacesFactory>().CreateAll())).InSingletonScope();
            Bind<IRandomNumberGenerator>().To<RandomNumberGenerator>().InSingletonScope();
            Bind<IMonopolyGame>().To<MonopolyGame>().InSingletonScope();
            Bind<IGamePlayers>().To<GamePlayers>().InSingletonScope();
            Bind<IBoardManager>().To<BoardManager>().InSingletonScope();
            Bind<IBanker>().To<Banker>().InSingletonScope();
            Bind<IDice>().To<Dice>().InSingletonScope();
            Bind<IJail>().To<Jail>().InSingletonScope();
            Bind<IDiceOutcomeHandler>().To<DiceOutcomeHandler>().InSingletonScope();
            Bind<ICardManager<ICommunityChestCard>>().ToMethod(c => new CardManager<ICommunityChestCard>(c.Kernel.Get<ICardFactory>().CreateCommunityChestCards())).InSingletonScope();
            Bind<ICardManager<IChanceCard>>().ToMethod(c => new CardManager<IChanceCard>(c.Kernel.Get<ICardFactory>().CreateChanceCards())).InSingletonScope();
            Bind<IPropertySpaceActions>().To<PropertySpaceActions>().InSingletonScope();
            Bind<ICardSpaceActions>().To<CardSpaceActions>().InSingletonScope();
            Bind<ICardActions>().To<CardActions>().InSingletonScope();
            Bind<ICardFactory>().To<CardFactory>().InSingletonScope();
        }
    }
}