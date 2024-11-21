using System;

namespace SpaceStellar.Common.Ui.WaitingWindow
{
    public class WaitingWindowRequest : IWaitingRequest
    {
        public bool IsRequestStarted { get; private set; }
        public object? Sender { get; private set; }

        public IReadonlyWaitingRequest MemberwiseCopy()
        {
            return (IReadonlyWaitingRequest)MemberwiseClone();
        }

        public void Start()
        {
            if (IsRequestStarted)
            {
                throw new InvalidOperationException("Request already started!");
            }

            if (Sender == null)
            {
                throw new InvalidOperationException("Unable to start without sender!");
            }

            IsRequestStarted = true;
        }

        public void Clear()
        {
            IsRequestStarted = false;
            Sender = null;
        }

        public void SetSender(object sender)
        {
            Sender = sender;
        }
    }
}