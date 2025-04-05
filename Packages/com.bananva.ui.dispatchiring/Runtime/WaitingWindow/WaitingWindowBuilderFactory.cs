using Bananva.UI.Dispatching.WaitingWindow.Abstraction;
using Bananva.UI.Dispatching.WaitingWindow.Api;
using Zenject;

namespace Bananva.UI.Dispatching.WaitingWindow
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