using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Utility;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowDispatcher : IWaitingWindowDispatcher
    {
        private readonly IWaitingWindowModel _model;
        private readonly IWaitingWindowPresenter _presenter;
        private readonly IWaitingWindowBuilderFactory _waitingWindowBuilderFactory;
        private readonly ILogger<WaitingWindowDispatcher> _logger;

        public WaitingWindowDispatcher(
            IWaitingWindowModel model,
            IWaitingWindowPresenter presenter,
            IWaitingWindowBuilderFactory waitingWindowBuilderFactory,
            ILogger<WaitingWindowDispatcher> logger)
        {
            _model = model;
            _presenter = presenter;
            _waitingWindowBuilderFactory = waitingWindowBuilderFactory;
            _logger = logger;
        }

        public IWaitingWindowBuilder CreateWaiting(object sender) => _waitingWindowBuilderFactory.Create(this, sender);

        public void Hide(object sender)
        {
            if (!_model.ContainSender(sender))
            {
                _logger.Error("Waiting request not found. Sender: {0}", sender);
                return;
            }

            _model.Remove(sender);
            if (_model.ActiveWaitingRequest == null && _presenter.IsOpened)
            {
                _presenter.Close();
            }
        }

        public void Show(IWaitingRequest waitingRequest)
        {
            if (waitingRequest.Sender == null)
            {
                _logger.Error("Invalid waiting request, no sender. Sender: {0}", waitingRequest);
                return;
            }

            if (waitingRequest.IsRequestStarted)
            {
                _logger.Error("Invalid waiting request, already started. Sender: {0}", waitingRequest);
                return;
            }

            waitingRequest.Start();
            if (_model.ContainSender(waitingRequest.Sender))
            {
                _model.Update(waitingRequest);
            }
            else
            {
                _model.Add(waitingRequest);
            }

            waitingRequest.Clear();
            if (!_presenter.IsOpened)
            {
                _presenter.Open();
            }
        }
    }
}
