using System;
using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.Presenters
{
    public abstract class PresentationLayerItem : IPresentationLayerItem
    {
        public virtual bool IsOpenAvailable => !IsOpened;
        public bool IsOpened { get; private set; }

        protected abstract void OnOpen();
        protected abstract void OnClose();

        public void Open()
        {
            if (!IsOpenAvailable)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is not open available");
            }

            if (IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is already opened");
            }

            OnOpen();
            IsOpened = true;
        }

        public void Close()
        {
            if (!IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is not opened");
            }

            OnClose();
            IsOpened = false;
        }
    }
}