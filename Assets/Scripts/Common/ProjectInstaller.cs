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
        }
    }
}