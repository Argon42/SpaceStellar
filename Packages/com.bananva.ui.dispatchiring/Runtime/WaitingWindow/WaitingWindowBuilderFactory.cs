using Bananva.UI.Dispatchiring.WaitingWindow.Abstraction;
using Bananva.UI.Dispatchiring.WaitingWindow.Api;
using Zenject;

namespace Bananva.UI.Dispatchiring.WaitingWindow
{
    internal class WaitingWindowBuilderFactory :
        PlaceholderFactory<IWaitingWindowDispatcherInternal, object, WaitingWindowBuilder>,
        IWaitingWindowBuilderFactory
    {
        public new IWaitingWindowBuilder Create(IWaitingWindowDispatcherInternal dispatcher, object sender)
        {
            return base.Create(dispatcher, sender);
        }
    }
}