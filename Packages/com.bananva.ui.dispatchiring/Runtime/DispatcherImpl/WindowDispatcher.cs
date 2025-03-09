using System;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.DispatcherImpl
{
    public class WindowDispatcher : IWindowDispatcher
    {
        public void ShowWindow<TModel>(IWindowPresenter<TModel> windowPresenter, TModel model)
        {
            throw new NotImplementedException();
        }
    }
}