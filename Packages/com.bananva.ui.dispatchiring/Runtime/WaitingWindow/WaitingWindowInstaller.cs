using System.Threading;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.WaitingWindow.Abstraction;
using Zenject;

namespace Bananva.UI.Dispatching.WaitingWindow
{
    public class WaitingWindowInstaller : Installer<WaitingWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WaitingWindowDispatcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaitingWindowModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaitingWindowPresenter>().AsSingle();
            Container.Bind<IWaitingWindowView>()
                .FromMethod(
                    context => context.Container
                        .Resolve<IViewProvider>()
                        .TryGetOrLoadView<IWaitingWindowView>(CancellationToken.None)
                        .GetAwaiter()
                        .GetResult())
                .AsSingle(); // TODO: подумать о том чтобы вынести это в общую часть диспетчера ибо это зависит от реализации

            Container
                .BindFactory<IWaitingWindowDispatcherInternal, object, WaitingWindowBuilder, WaitingWindowBuilderFactory>()
                .AsSingle();
            Container
                .Bind<IWaitingWindowBuilderFactory>()
                .To<WaitingWindowBuilderFactory>()
                .FromResolve()
                .AsCached();
        }
    }
}