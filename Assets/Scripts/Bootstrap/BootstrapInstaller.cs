using SpaceStellar.Utility;
using Zenject;

namespace SpaceStellar.Bootstrap
{
    public class BootstrapInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapFlow>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ApplicationConfigurationLoadUnit>().AsSingle();

            Container.BindInterfacesAndSelfTo<LoadingScreenService>().AsSingle();
        }
    }
}