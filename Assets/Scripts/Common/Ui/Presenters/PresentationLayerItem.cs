using System;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui
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
                throw new InvalidOperationException($"Presenter {GetType().Name} is not open available");

            if (IsOpened)
                throw new InvalidOperationException($"Presenter {GetType().Name} is already opened");

            OnOpen();
            IsOpened = true;
        }

        public void Close()
        {
            if (!IsOpened)
                throw new InvalidOperationException($"Presenter {GetType().Name} is not opened");

            OnClose();
            IsOpened = false;
        }
    }

    public abstract class Presenter<TView> : PresentationLayerItem, IChangeableViewPresenter<TView>
    {
        public TView View { get; private set; } = default!;

        public void SetView(TView view)
        {
            if (View != null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");

            View = view;
            OnSetView();
        }

        public void ResetView()
        {
            if (View == null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");

            if (IsOpened)
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");

            OnResetView();
            View = default!;
        }

        protected virtual void OnResetView()
        {
        }

        protected virtual void OnSetView()
        {
        }
    }
}