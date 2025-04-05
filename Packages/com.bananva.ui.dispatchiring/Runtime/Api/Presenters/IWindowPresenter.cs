namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IWindowPresenter : IPresentationLayerItem { }

    public interface IWindowPresenter<TModel> : IWindowPresenter, IChangeableModel<TModel> { }
}