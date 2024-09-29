namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IPresenterProvider
    {
        TPresenter GetPresenter<TPresenter, TModel>()
            where TPresenter : IPresentationLayerItem;
    }
}
