namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IScreenPresenter : IPresentationLayerItem, IChangeableModel { }

    public interface IScreenPresenter<TModel> : IScreenPresenter, IChangeableModel<TModel> { }
}
