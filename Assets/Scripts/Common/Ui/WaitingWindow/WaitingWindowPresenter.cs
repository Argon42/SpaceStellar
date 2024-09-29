using SpaceStellar.Common.Ui.Presenters;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowPresenter : Presenter<IWaitingWindowModel>, IWaitingWindowPresenter
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
