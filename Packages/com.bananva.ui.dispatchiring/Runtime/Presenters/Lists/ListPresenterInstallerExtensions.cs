using System;
using System.Collections.Generic;
using ObservableCollections;
using Zenject;

namespace Bananva.UI.Dispatchiring.Presenters.Lists
{
    public static class ListPresenterInstallerExtensions
    {
        /// <summary>
        /// Данный Bind установит в контейнер объект <see cref="ReadOnlyListPresenter{TModel}"/> по интерфейсу <see cref="IReadOnlyListPresenter{TModel}"/>.
        /// </summary>
        /// <param name="container">Контейнер в рамках которого происходит bind класса</param>
        /// <param name="presenters">То какие презентеры будут создаваться для работы в списке. Для каждого будет создан <see cref="IMemoryPool"/></param>
        /// <typeparam name="TModelBase">Базовый класс модели хранимой в списке <see cref="IReadOnlyList{TModelBase}"/> который будет отображаться</typeparam>
        /// <returns></returns>
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindReadOnlyListPresenter<TModelBase>(
            this DiContainer container,
            params Type[] presenters)
            where TModelBase : class
        {
            return ListPresenterInstaller<ReadOnlyListPresenter<TModelBase>, IReadOnlyList<TModelBase>, TModelBase>
                .InstallInContainer(container, presenters);
        }

        /// <summary>
        /// Данный Bind установит в контейнер объект <see cref="ReactiveListPresenter{TModel}"/> по интерфейсу <see cref="IReadOnlyObservableList{TModel}"/>.
        /// </summary>
        /// <param name="container">Контейнер в рамках которого происходит bind класса</param>
        /// <param name="presenters">То какие презентеры будут создаваться для работы в списке. Для каждого будет создан <see cref="IMemoryPool"/></param>
        /// <typeparam name="TModelBase">Базовый класс модели хранимой в списке <see cref="IReadOnlyObservableList{TModelBase}"/> который будет отображаться</typeparam>
        /// <returns></returns>
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindReactiveListPresenter<TModelBase>(
            this DiContainer container,
            params Type[] presenters)
            where TModelBase : class
        {
            return ListPresenterInstaller<ReactiveListPresenter<TModelBase>, IReadOnlyObservableList<TModelBase>,
                    TModelBase>
                .InstallInContainer(container, presenters);
        }
    }
}