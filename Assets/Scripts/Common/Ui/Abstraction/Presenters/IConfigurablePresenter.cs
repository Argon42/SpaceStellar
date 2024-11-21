namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IConfigurablePresenter<TModel, TView> : IChangeableViewPresenter<TView>, IChangeableModel<TModel>
        where TView : IView { }
}