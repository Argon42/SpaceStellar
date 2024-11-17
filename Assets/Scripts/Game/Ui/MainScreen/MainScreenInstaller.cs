using SpaceStellar.Common.Ui.Presenters.Lists;
using SpaceStellar.Game.Ui.MainScreen.Presenters;
using SpaceStellar.Utility;
using Zenject;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class MainScreenInstaller : Installer<MainScreenInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MainScreenModel>().AsSingle();
            Container.BindInterfacesTo<MainScreenPresenter>().AsSingle();

            Container.BindClassWithPool<BattleTilePresenter>();
            Container.BindClassWithPool<SimpleTilePresenter>();

            ReadOnlyListPresenterInstaller<MainMenuTile>.InstallInContainer(Container, new[]
            {
                typeof(BattleTilePresenter),
                typeof(SimpleTilePresenter),
            });

            Container.Bind<OpenMainScreenCommand>().AsSingle();
        }
    }
}