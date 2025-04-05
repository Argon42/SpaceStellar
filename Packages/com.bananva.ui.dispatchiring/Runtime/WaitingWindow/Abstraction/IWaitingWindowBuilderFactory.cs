using Bananva.UI.Dispatching.WaitingWindow.Api;

namespace Bananva.UI.Dispatching.WaitingWindow.Abstraction
{
    internal interface IWaitingWindowBuilderFactory
    {
        IWaitingWindowBuilder Create(IWaitingWindowDispatcherInternal dispatcher, object sender);
    }
}