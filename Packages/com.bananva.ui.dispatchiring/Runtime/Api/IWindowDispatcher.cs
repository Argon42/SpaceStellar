using Bananva.UI.Dispatching.Api.Presenters;

namespace Bananva.UI.Dispatching.Api
{
    public interface IWindowDispatcher
    {
        void ShowWindow<TModel>(IWindowPresenter<TModel> presenter, TModel model);
    }
}