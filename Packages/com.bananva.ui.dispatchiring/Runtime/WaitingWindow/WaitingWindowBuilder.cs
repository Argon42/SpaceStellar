using Bananva.UI.Dispatching.WaitingWindow.Abstraction;
using Bananva.UI.Dispatching.WaitingWindow.Api;

namespace Bananva.UI.Dispatching.WaitingWindow
{
    internal class WaitingWindowBuilder : IWaitingWindowBuilder
    {
        private readonly IWaitingWindowDispatcherInternal _dispatcher;
        private readonly WaitingWindowRequest _request;

        internal WaitingWindowBuilder(IWaitingWindowDispatcherInternal dispatcher, object sender)
        {
            _dispatcher = dispatcher;
            _request = new WaitingWindowRequest();

            _request.SetSender(sender);
        }

        public void Show()
        {
            _dispatcher.Show(_request);
        }
    }
}