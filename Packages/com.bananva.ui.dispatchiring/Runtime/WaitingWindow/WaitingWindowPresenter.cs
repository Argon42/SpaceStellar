using System;
using Bananva.UI.Dispatchiring.Presenters;
using Bananva.UI.Dispatchiring.WaitingWindow.Abstraction;

namespace Bananva.UI.Dispatchiring.WaitingWindow
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