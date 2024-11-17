using System;
using SpaceStellar.Game.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public class ReadOnlyListPresenterInstaller<TBaseModel> : Installer
        where TBaseModel : class
    {
        private readonly Type[] _presenters;

        public ReadOnlyListPresenterInstaller(Type[] presenters)
        {
            _presenters = presenters;
        }

        public override void InstallBindings()
        {
            Container.Bind<PresenterViewMatchers>()
                .AsSingle()
                .WithArguments(_presenters)
                .WhenInjectedInto<MultiplePresenterViewPool>();
            Container.Bind<IPresenterViewPool>()
                .To<MultiplePresenterViewPool>()
                .AsSingle()
                .WhenInjectedInto<ReadOnlyListPresenter<TBaseModel>>();
            Container.Bind<IReadOnlyListPresenter<TBaseModel>>()
                .To<ReadOnlyListPresenter<TBaseModel>>()
                .AsSingle();
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder InstallInContainer(
            DiContainer container,
            Type[] presenters)
        {
            var subContainer = container.CreateSubContainer();
            var args = new object[] { presenters };
            subContainer.Install<ReadOnlyListPresenterInstaller<MainMenuTile>>(args);
            return container.Bind<IReadOnlyListPresenter<MainMenuTile>>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsSingle();
        }
    }
}