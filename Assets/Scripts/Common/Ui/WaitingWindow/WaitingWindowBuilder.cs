using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowBuilder : IWaitingWindowBuilder
    {
        private readonly IWaitingWindowDispatcher _dispatcher;
        private readonly object _sender;
        private readonly WaitingWindowRequest _request;

        public WaitingWindowBuilder(IWaitingWindowDispatcher dispatcher, object sender)
        {
            _dispatcher = dispatcher;
            _sender = sender;
            _request = new WaitingWindowRequest();

            _request.SetSender(_sender);
        }

        public void Show()
        {
            _dispatcher.Show(_request);
        }
    }
}
