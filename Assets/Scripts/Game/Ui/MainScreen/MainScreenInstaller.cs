using Zenject;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class MainScreenInstaller : Installer<MainScreenInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainScreenModel>().AsSingle();
            Container.BindInterfacesTo<MainScreenPresenter>().AsSingle();

            Container.Bind<OpenMainScreenCommand>().AsSingle();
        }
    }
}