using System;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui
{
    public abstract class Presenter<TModel> : Presenter, IPresenter<TModel>
    {
        public override bool IsOpenAvailable => base.IsOpenAvailable && Model != null;

        public TModel? Model { get; private set; }

        protected virtual void OnSetModel() { }

        protected virtual void OnResetModel() { }

        public void SetModel(TModel model)
        {
            if (Model != null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");

            Model = model;
            OnSetModel();
        }

        public sealed override void ResetModel()
        {
            if (Model == null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");

            if (IsOpened)
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");

            OnResetModel();
            Model = default;
        }
    }

    public abstract class Presenter : IPresenter
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

        public virtual void ResetModel()
        {
        }
    }
}