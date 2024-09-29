using System.Threading;
using SpaceStellar.Common.Ui.Abstraction;
using Zenject;

namespace SpaceStellar.Common.Ui.WaitingWindow
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
                .AsSingle();

            Container
                .BindFactory<IWaitingWindowDispatcher, object, WaitingWindowBuilder, WaitingWindowBuilderFactory>()
                .AsSingle();
            Container
                .Bind<IWaitingWindowBuilderFactory>()
                .To<WaitingWindowBuilderFactory>()
                .FromResolve()
                .AsCached();
        }
    }
}
