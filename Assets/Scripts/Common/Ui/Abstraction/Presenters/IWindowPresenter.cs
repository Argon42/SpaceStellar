namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IWindowPresenter : IPresentationLayerItem { }

    public interface IWindowPresenter<TModel> : IWindowPresenter, IChangeableModel<TModel> { }
}