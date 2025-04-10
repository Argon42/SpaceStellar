namespace Bananva.UI.Dispatching.WaitingWindow.Abstraction
{
    public interface IReadonlyWaitingRequest
    {
        bool IsRequestStarted { get; }
        object? Sender { get; }

        public IReadonlyWaitingRequest MemberwiseCopy();
    }

    public interface IWaitingRequest : IReadonlyWaitingRequest
    {
        void Start();
        void Clear();
    }
}