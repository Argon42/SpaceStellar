using SpaceStellar.Common.Ui.Presenters.Lists;
using SpaceStellar.Game.Ui.MainScreen.Presenters;
using SpaceStellar.Game.Ui.MainScreen.Views;
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

            Container
                .BindReadOnlyListPresenter<MainMenuTile>(
                    typeof(BattleTilePresenter),
                    typeof(SimpleTilePresenter))
                .AsSingle();

            Container.Bind<OpenMainScreenCommand>().AsSingle();
        }
    }
}