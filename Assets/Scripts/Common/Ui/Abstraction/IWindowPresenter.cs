namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IWindowPresenter : IPresenter { }

    public interface IWindowPresenter<TModel> : IWindowPresenter, IPresenter<TModel> { }
}
