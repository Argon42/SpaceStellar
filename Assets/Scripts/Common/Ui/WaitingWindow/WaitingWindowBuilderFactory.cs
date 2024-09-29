using SpaceStellar.Common.Ui.Abstraction;
using Zenject;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowBuilderFactory :
        PlaceholderFactory<IWaitingWindowDispatcher, object, WaitingWindowBuilder>,
        IWaitingWindowBuilderFactory
    {
        public new IWaitingWindowBuilder Create(IWaitingWindowDispatcher dispatcher, object sender) =>
            base.Create(dispatcher, sender);
    }
}
