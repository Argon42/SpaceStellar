namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IScreenPresenter : IPresenter { }

    public interface IScreenPresenter<TModel> : IScreenPresenter, IPresenter<TModel> { }
}
