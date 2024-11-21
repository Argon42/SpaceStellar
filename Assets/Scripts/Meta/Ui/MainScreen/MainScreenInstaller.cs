using SpaceStellar.Common.Ui.Presenters.Lists;
using SpaceStellar.Meta.Ui.MainScreen.Presenters;
using SpaceStellar.Utility;
using Zenject;

namespace SpaceStellar.Meta.Ui.MainScreen
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