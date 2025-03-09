using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.Api
{
    public interface IWindowDispatcher
    {
        void ShowWindow<TModel>(IWindowPresenter<TModel> presenter, TModel model);
    }
}