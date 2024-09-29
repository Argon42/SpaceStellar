namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IPresenterProvider
    {
        TPresenter GetPresenter<TPresenter, TModel>()
            where TPresenter : IPresentationLayerItem;
    }
}
