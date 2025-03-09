using Bananva.UI.Dispatchiring.Presenters;
using Bananva.UI.Dispatchiring.WaitingWindow.Abstraction;

namespace Bananva.UI.Dispatchiring.WaitingWindow
{
    internal class WaitingWindowPresenter : Presenter<IWaitingWindowModel>, IWaitingWindowPresenter
    {
        private readonly IWaitingWindowView _view;

        public WaitingWindowPresenter(IWaitingWindowView view)
        {
            _view = view;
        }

        protected override void OnOpen()
        {
            _view.Activate();
        }

        protected override void OnClose()
        {
            _view.Deactivate();
        }
    }
}