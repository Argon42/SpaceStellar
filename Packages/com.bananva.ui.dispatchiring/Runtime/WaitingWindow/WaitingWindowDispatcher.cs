using Bananva.UI.Dispatching.WaitingWindow.Abstraction;
using Bananva.UI.Dispatching.WaitingWindow.Api;
using Bananva.Utilities.Logger;

namespace Bananva.UI.Dispatching.WaitingWindow
{
    internal class WaitingWindowDispatcher : IWaitingWindowDispatcherInternal
    {
        private readonly IWaitingWindowModel _model;
        private readonly IWaitingWindowPresenter _presenter;
        private readonly IWaitingWindowBuilderFactory _waitingWindowBuilderFactory;
        private readonly ILogger _logger;

        internal WaitingWindowDispatcher(
            IWaitingWindowModel model,
            IWaitingWindowPresenter presenter,
            IWaitingWindowBuilderFactory waitingWindowBuilderFactory,
            ILogger logger)
        {
            _model = model;
            _presenter = presenter;
            _waitingWindowBuilderFactory = waitingWindowBuilderFactory;
            _logger = logger;
        }

        public IWaitingWindowBuilder CreateWaiting(object sender)
        {
            return _waitingWindowBuilderFactory.Create(this, sender);
        }

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

        public void Show(IWaitingRequest request)
        {
            if (request.Sender == null)
            {
                _logger.Error("Invalid waiting request, no sender. Sender: {0}", request);
                return;
            }

            if (request.IsRequestStarted)
            {
                _logger.Error("Invalid waiting request, already started. Sender: {0}", request);
                return;
            }

            request.Start();
            if (_model.ContainSender(request.Sender))
            {
                _model.Update(request);
            }
            else
            {
                _model.Add(request);
            }

            request.Clear();
            if (!_presenter.IsOpened)
            {
                _presenter.Open();
            }
        }
    }
}