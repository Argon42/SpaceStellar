using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public interface IWaitingWindowBuilderFactory
    {
        IWaitingWindowBuilder Create(IWaitingWindowDispatcher dispatcher, object sender);
    }
}
