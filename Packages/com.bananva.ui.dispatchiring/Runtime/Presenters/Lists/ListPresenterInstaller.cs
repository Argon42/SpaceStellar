using System;
using Bananva.UI.Dispatching.Api.Presenters;
using Bananva.UI.Dispatching.Presenters.Lists.Abstraction;
using Bananva.UI.Dispatching.Presenters.Lists.Common;
using Bananva.Utilities.Extensions;
using Zenject;

namespace Bananva.UI.Dispatching.Presenters.Lists
{
    public class ListPresenterInstaller<TListPresenter, TContractListPresenter, TCollection, TBaseModel> : Installer
        where TBaseModel : class
        where TListPresenter : TContractListPresenter, IListPresenter<TCollection>
    {
        private readonly Type[] _presenters;

        public ListPresenterInstaller(Type[] presenters) => _presenters = presenters;

        public override void InstallBindings()
        {
            _presenters.ForEach(type =>
            {
                typeof(ZenjectExtensions)
                    .GetMethod(nameof(ZenjectExtensions.BindClassWithPool))!
                    .MakeGenericMethod(type)
                    .Invoke(null, new object[] { Container });
            });
            Container.Bind<PresenterViewMatchers>()
                .AsSingle()
                .WithArguments(_presenters)
                .WhenInjectedInto<MultiplePresenterViewPool>();
            Container.Bind<IPresenterViewPool>()
                .To<MultiplePresenterViewPool>()
                .AsSingle()
                .WhenInjectedInto<TListPresenter>();
            Container.BindInterfacesAndSelfTo<TListPresenter>()
                .AsSingle();
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder InstallInContainer(
            DiContainer container,
            Type[] presenters)
        {
            var subContainer = container.CreateSubContainer();
            var args = new object[] { presenters };
            subContainer
                .Install<ListPresenterInstaller<TListPresenter, TContractListPresenter, TCollection, TBaseModel>>(args);
            return container.Bind<TContractListPresenter>()
                .FromSubContainerResolve()
                .ByInstance(subContainer)
                .AsCached();
        }
    }
}