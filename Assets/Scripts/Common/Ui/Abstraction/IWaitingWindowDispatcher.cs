using SpaceStellar.Common.Ui.WaitingWindow;

namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IWaitingWindowDispatcher
    {
        IWaitingWindowBuilder CreateWaiting(object sender);
        void Hide(object sender);
        void Show(IWaitingRequest request);
    }
}