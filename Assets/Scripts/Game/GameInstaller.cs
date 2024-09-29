using SpaceStellar.Common.Ui;
using SpaceStellar.Game.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Game
{
    public class GameInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameFlow>().AsSingle();

            
            InstallUi();
        }

        private void InstallUi()
        {
            UiInstaller.Install(Container);
            MainScreenInstaller.Install(Container);
        }
    }
}