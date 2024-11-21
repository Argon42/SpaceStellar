using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IWindowDispatcher
    {
        void ShowWindow<TModel>(IWindowPresenter<TModel> presenter, TModel model);
    }
}