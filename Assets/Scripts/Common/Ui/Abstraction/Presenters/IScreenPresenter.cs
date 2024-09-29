namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IScreenPresenter : IPresentationLayerItem { }

    public interface IScreenPresenter<TModel> : IScreenPresenter, IPresentationLayerItem { }
}
