namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IWindowDispatcher
    {
        void ShowWindow<TModel>(IWindowPresenter<TModel> presenter, TModel model);
    }
}
