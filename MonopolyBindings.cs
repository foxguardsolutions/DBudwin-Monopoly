using System.Collections.Generic;
using Ninject;
using Ninject.Modules;

namespace Monopoly
{
    public class MonopolyBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlayerFactory>().To<PlayerFactory>();
            Bind<IBoardSpacesFactory>().To<BoardSpacesFactory>();
            Bind<IRandomNumberGenerator>().To<RandomNumberGenerator>();
            Bind<IEnumerable<IPlayer>>().ToMethod(context => context.Kernel.Get<IPlayerFactory>().CreateAll(context.Kernel.Get<IRandomNumberGenerator>())).InSingletonScope();
            Bind<IEnumerable<IBoardSpace>>().ToMethod(context => context.Kernel.Get<IBoardSpacesFactory>().CreateAll()).InSingletonScope();
        }
    }
}