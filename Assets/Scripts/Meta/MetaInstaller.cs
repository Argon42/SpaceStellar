using Bananva.UI.Dispatching.DispatcherImpl;
using SpaceStellar.Meta.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Meta
{
    public class MetaInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MetaFlow>().AsSingle();

            InstallUi();
        }

        private void InstallUi()
        {
            UiInstaller.Install(Container);
            MainScreenInstaller.Install(Container);
        }
    }
}