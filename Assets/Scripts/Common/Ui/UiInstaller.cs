using SpaceStellar.Common.Ui.Commands;
using SpaceStellar.Common.Ui.WaitingWindow;
using Zenject;

namespace SpaceStellar.Common.Ui
{
    public class UiInstaller : Installer<UiInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PresenterProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<UiDispatcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenSwitcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowDispatcher>().AsSingle();

            WaitingWindowInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<UiCommandDispatcher>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiCommandExecutor>().AsSingle();
            Container.BindInterfacesAndSelfTo<ZenjectUiCommandPool>().AsSingle();
        }
    }
}