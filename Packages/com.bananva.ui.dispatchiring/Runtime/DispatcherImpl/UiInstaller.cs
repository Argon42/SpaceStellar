﻿using Bananva.UI.Dispatching.Commands;
using Bananva.UI.Dispatching.WaitingWindow;
using Zenject;

namespace Bananva.UI.Dispatching.DispatcherImpl
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