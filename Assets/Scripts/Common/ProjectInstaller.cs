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
            Container.Bind<ILogger>().FromMethod(CreateLoggerByType).AsTransient();

            Container.Bind<IAssetProvider>().To<ResourcesAssetProvider>().AsSingle();
            Container.Bind<ICachedDataProvider>().To<PlayerPrefsCachedDataProvider>().AsSingle();
            Container.Bind<ILocalDataProvider>().To<LocalLocalDataProvider>().AsSingle();
            Container.Bind<ClientConfiguration>().AsSingle();

            BindDataSources();
        }

        private void BindDataSources()
        {
            Container.BindInterfacesAndSelfTo<ClientProfileDataSource>().AsSingle();
        }

        private static ILogger CreateLoggerByType(InjectContext context)
        {
            var genericLogger = typeof(UnityLogger<>);
            var typedLogger = genericLogger.MakeGenericType(context.ObjectType);
            return (ILogger)context.Container.Instantiate(typedLogger);
        }
    }
}