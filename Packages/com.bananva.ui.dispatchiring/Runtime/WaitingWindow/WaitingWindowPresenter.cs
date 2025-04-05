using System;
using Bananva.UI.Dispatching.Presenters;
using Bananva.UI.Dispatching.WaitingWindow.Abstraction;

namespace Bananva.UI.Dispatching.WaitingWindow
{
    internal class WaitingWindowPresenter : Presenter<IWaitingWindowView>, IWaitingWindowPresenter, IDisposable
    {
        public WaitingWindowPresenter(IWaitingWindowView view) => SetView(view);

        public void Dispose() => ResetView();

        protected override void OnOpen()
        {
            View.Activate();
        }

        protected override void OnClose()
        {
            View.Deactivate();
        }
    }
}