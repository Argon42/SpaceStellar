namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IWindowPresenter : IPresentationLayerItem { }

    public interface IWindowPresenter<TModel> : IWindowPresenter, IPresentationLayerItem { }
}
