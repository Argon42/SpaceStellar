using Bananva.UI.Dispatching.WaitingWindow.Api;

namespace Bananva.UI.Dispatching.WaitingWindow.Abstraction
{
    internal interface IWaitingWindowDispatcherInternal : IWaitingWindowDispatcher
    {
        void Show(IWaitingRequest request);
    }
}