using Bananva.UI.Dispatchiring.WaitingWindow.Api;

namespace Bananva.UI.Dispatchiring.WaitingWindow.Abstraction
{
    internal interface IWaitingWindowBuilderFactory
    {
        IWaitingWindowBuilder Create(IWaitingWindowDispatcherInternal dispatcher, object sender);
    }
}