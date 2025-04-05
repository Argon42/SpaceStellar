using System;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;

namespace Bananva.UI.Dispatching.DispatcherImpl
{
    public class WindowDispatcher : IWindowDispatcher
    {
        public void ShowWindow<TModel>(IWindowPresenter<TModel> windowPresenter, TModel model)
        {
            throw new NotImplementedException();
        }
    }
}