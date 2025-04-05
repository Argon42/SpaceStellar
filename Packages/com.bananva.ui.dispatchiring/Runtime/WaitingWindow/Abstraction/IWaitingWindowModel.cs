using System;

namespace Bananva.UI.Dispatching.WaitingWindow.Abstraction
{
    internal interface IWaitingWindowModel
    {
        bool ContainSender(object waitingRequestSender);
        void Update(IWaitingRequest waitingRequest);
        void Add(IWaitingRequest waitingRequest);
        void Remove(object sender);
        IReadonlyWaitingRequest? ActiveWaitingRequest { get; }
        void RemoveAll();
        event Action<IReadonlyWaitingRequest?> OnActiveWaitingInfoChangedEvent;
    }
}