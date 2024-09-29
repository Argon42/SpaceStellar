using SpaceStellar.Common.Data;
using SpaceStellar.Utility;
using Zenject;

namespace SpaceStellar.Common
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneSwitcher>().AsSingle();
            Container.Bind<ILogger>().To<UnityLogger>().AsSingle();
            Container.Bind<ICachedDataProvider>().To<PlayerPrefsCachedDataProvider>().AsSingle();
            Container.Bind<ClientConfiguration>().AsSingle();
            BindDataSources();
        }

        private void BindDataSources()
        {
            Container.BindInterfacesAndSelfTo<ClientProfileDataSource>().AsSingle();
        }
    }
}