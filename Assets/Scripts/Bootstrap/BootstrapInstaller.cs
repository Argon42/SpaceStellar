using Bananva.LoadingService;
using SpaceStellar.Bootstrap.LoadUnits;
using Zenject;

namespace SpaceStellar.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapFlow>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenService>().AsSingle();

            Container.BindInterfacesAndSelfTo<ApplicationConfigurationLoadUnit>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClientConfigurationLoadUnit>().AsSingle();
            Container.BindInterfacesAndSelfTo<CachedDataLoaderUnit>().AsSingle();
        }
    }
}