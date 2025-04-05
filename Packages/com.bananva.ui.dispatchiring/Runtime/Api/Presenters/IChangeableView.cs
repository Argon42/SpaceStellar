namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IChangeableView<TView> : IChangeableView
    {
        /// <summary>
        /// Поскольку view может отсутствовать, обращение к методу приведет к InvalidOperationException.
        /// Для проверки значения используйте <see cref="IChangeableView.HasView"/>
        /// </summary>
        TView View { get; }

        /// <summary>
        /// Установка view допустима только в случае отсутствия view в представлении и закрытого презентера.
        /// Если view уже присутствует, то вызов приведет к InvalidOperationException
        /// Если презентер не закрыт, то вызов приведет к InvalidOperationException.
        /// Для проверки значения используйте <see cref="IChangeableView.HasView"/>
        /// и <see cref="IReadonlyPresentationLayerItem.IsOpened"/>
        /// </summary>
        /// <param name="view"></param>
        void SetView(TView view);
    }

    public interface IChangeableView
    {
        bool HasView { get; }

        /// <summary>
        /// Сброс view допустим только в случае наличия view в представлении и закрытого презентера.
        /// Если view уже присутствует, то вызов приведет к InvalidOperationException
        /// Если презентер не закрыт, то вызов приведет к InvalidOperationException.
        /// Для проверки значения используйте <see cref="IChangeableView.HasView"/>
        /// и <see cref="IReadonlyPresentationLayerItem.IsOpened"/>
        /// </summary>
        void ResetView();
    }
}