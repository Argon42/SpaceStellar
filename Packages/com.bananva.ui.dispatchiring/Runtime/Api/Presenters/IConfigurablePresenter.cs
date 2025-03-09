namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IConfigurablePresenter<TModel, TView> : IChangeableViewPresenter<TView>, IChangeableModel<TModel>
        where TView : IView { }
}