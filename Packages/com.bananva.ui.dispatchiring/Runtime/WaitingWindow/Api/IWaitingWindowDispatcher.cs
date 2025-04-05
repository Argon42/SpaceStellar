namespace Bananva.UI.Dispatching.WaitingWindow.Api
{
    public interface IWaitingWindowDispatcher
    {
        IWaitingWindowBuilder CreateWaiting(object sender);
        void Hide(object sender);
    }
}