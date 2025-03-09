using Bananva.UI.Dispatchiring.WaitingWindow.Api;

namespace Bananva.UI.Dispatchiring.WaitingWindow.Abstraction
{
    internal interface IWaitingWindowDispatcherInternal : IWaitingWindowDispatcher
    {
        void Show(IWaitingRequest request);
    }
}