namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IPresenterProvider
    {
        TPresenter GetPresenter<TPresenter, TModel>()
            where TPresenter : IPresentationLayerItem;
    }
}