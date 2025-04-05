namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IScreenPresenter : IPresentationLayerItem, IChangeableModel { }

    public interface IScreenPresenter<TModel> : IScreenPresenter, IChangeableModel<TModel> { }
}