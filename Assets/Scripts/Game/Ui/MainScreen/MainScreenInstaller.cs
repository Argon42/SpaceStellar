using Zenject;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class MainScreenInstaller : Installer<MainScreenInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainScreenModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainScreenPresenter>().AsSingle();

            Container.Bind<OpenMainScreenCommand>().AsSingle();
        }
    }
}