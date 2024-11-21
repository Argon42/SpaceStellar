using System;
using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Meta.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public class ListPresenterInstaller<TListPresenter, TCollection, TBaseModel> : Installer
        where TBaseModel : class
        where TListPresenter : IListPresenter<TCollection>
    {
        private readonly Type[] _presenters;

        public ListPresenterInstaller(Type[] presenters)
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
            Container.BindInterfacesAndSelfTo<TListPresenter>()
                .AsSingle();
        }

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder InstallInContainer(
            DiContainer container,
            Type[] presenters)
        {
            var subContainer = container.CreateSubContainer();
            var args = new object[] { presenters };
            subContainer
                .Install<ListPresenterInstaller<ReadOnlyListPresenter<MainMenuTile>, IReadOnlyList<MainMenuTile>,
                    MainMenuTile>>(args);
            return container.Bind<IReadOnlyListPresenter<MainMenuTile>>()
                .FromSubContainerResolve()
                .ByInstance(subContainer);
        }
    }
}