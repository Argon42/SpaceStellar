namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IPresenterProvider
    {
        TPresenter GetPresenter<TPresenter, TModel>()
            where TPresenter : IPresentationLayerItem;
    }
}