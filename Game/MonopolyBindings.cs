using System.Collections.Generic;
using Monopoly.Game.Bank;
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
            Bind<IBoard>().To<Board>().WithConstructorArgument(typeof(IEnumerable<IBoardSpace>), context => context.Kernel.Get<IBoardSpacesFactory>().CreateAll());
            Bind<IPlayerFactory>().To<PlayerFactory>().WithConstructorArgument(playerNames);
            Bind<IBoardSpacesFactory>().To<BoardSpacesFactory>();
            Bind<IRandomNumberGenerator>().To<RandomNumberGenerator>();
            Bind<IEnumerable<IPlayer>>().ToMethod(context => context.Kernel.Get<IPlayerFactory>().CreateAll()).InSingletonScope();
            Bind<IMonopolyGame>().To<MonopolyGame>().WithConstructorArgument(typeof(IEnumerable<IPlayer>), context => context.Kernel.Get<IPlayerFactory>().CreateAll());
            Bind<IPropertyManager>().To<PropertyManager>().WithConstructorArgument(typeof(IBoard), context => context.Kernel.Get<IBoard>()).WithConstructorArgument(typeof(IBanker), context => context.Kernel.Get<IBanker>());
            Bind<IBanker>().To<Banker>();
            Bind<IDice>().To<Dice>();
            Bind<IJail>().To<Jail>();
            Bind<IDiceOutcomeHandler>().To<DiceOutcomeHandler>();
        }
    }
}