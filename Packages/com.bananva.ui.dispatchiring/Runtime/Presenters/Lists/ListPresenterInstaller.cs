using System;
using System.Collections.Generic;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Bananva.UI.Dispatchiring.Presenters.Lists.Abstraction;
using Bananva.UI.Dispatchiring.Presenters.Lists.Common;
using Zenject;

namespace Bananva.UI.Dispatchiring.Presenters.Lists
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
                .Install<ListPresenterInstaller<ReadOnlyListPresenter<TBaseModel>, IReadOnlyList<TBaseModel>,
                    TBaseModel>>(args);
            return container.Bind<IReadOnlyListPresenter<TBaseModel>>()
                .FromSubContainerResolve()
                .ByInstance(subContainer);
        }
    }
}