using System;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui
{
    public class WindowDispatcher : IWindowDispatcher
    {
        public void ShowWindow<TModel>(IWindowPresenter<TModel> windowPresenter, TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
