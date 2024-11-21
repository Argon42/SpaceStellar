using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowModel : IWaitingWindowModel
    {
        public event Action<IReadonlyWaitingRequest?> OnActiveWaitingInfoChangedEvent = delegate { };
        private readonly List<IReadonlyWaitingRequest> _requests = new();

        public IReadonlyWaitingRequest? ActiveWaitingRequest => _requests.FirstOrDefault();

        public bool ContainSender(object waitingRequestSender)
        {
            return _requests.Any(request => request.Sender == waitingRequestSender);
        }

        public void Update(IWaitingRequest waitingRequest)
        {
            if (waitingRequest.Sender == null)
            {
                throw new InvalidOperationException("Invalid waiting request, no sender.");
            }

            ThrowIfRequestNotStarted(waitingRequest);
            ThrowIfHasNotRequestWithSender(waitingRequest.Sender);

            var index = GetInfoIndex(waitingRequest.Sender);
            var waitingRequestCopy = waitingRequest.MemberwiseCopy();
            _requests[index] = waitingRequestCopy;
            SendEventIfRequestActive(waitingRequest);
        }

        public void Add(IWaitingRequest waitingRequest)
        {
            if (waitingRequest.Sender == null)
            {
                throw new InvalidOperationException("Invalid waiting request, no sender.");
            }

            ThrowIfRequestNotStarted(waitingRequest);
            ThrowIfContainRequestForSender(waitingRequest.Sender);

            var waitingRequestCopy = waitingRequest.MemberwiseCopy();
            _requests.Add(waitingRequestCopy);
            SendEventIfRequestActive(waitingRequest);
        }

        public void Remove(object sender)
        {
            ThrowIfHasNotRequestWithSender(sender);

            var index = GetInfoIndex(sender);
            var waitingRequestForRemove = _requests[index];
            var isWaitingRequestWillChange = ActiveWaitingRequest == waitingRequestForRemove;
            _requests.Remove(waitingRequestForRemove);

            if (isWaitingRequestWillChange)
            {
                OnActiveWaitingInfoChangedEvent(ActiveWaitingRequest);
            }
        }

        public void RemoveAll()
        {
            _requests.Clear();
            OnActiveWaitingInfoChangedEvent(null);
        }

        private void ThrowIfHasNotRequestWithSender(object sender)
        {
            if (!ContainSender(sender))
            {
                throw new InvalidOperationException($"Waiting request not found. Sender: {sender}");
            }
        }

        private static void ThrowIfRequestNotStarted(IWaitingRequest waitingRequest)
        {
            if (!waitingRequest.IsRequestStarted)
            {
                throw new InvalidOperationException("Invalid waiting request, not started.");
            }
        }

        private void ThrowIfContainRequestForSender(object sender)
        {
            if (ContainSender(sender))
            {
                throw new InvalidOperationException($"Waiting request already exists. Sender: {sender}");
            }
        }

        private void SendEventIfRequestActive(IWaitingRequest waitingRequest)
        {
            if (ActiveWaitingRequest == waitingRequest)
            {
                OnActiveWaitingInfoChangedEvent(waitingRequest);
            }
        }

        private int GetInfoIndex(object sender)
        {
            return _requests.FindIndex(request => request.Sender == sender);
        }
    }
}