using System;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Presenters
{
    public abstract class Presenter<TModel, TView> : Presenter<TView>, IConfigurablePresenter<TModel, TView>
    {
        public override bool IsOpenAvailable => base.IsOpenAvailable && Model != null;

        public TModel Model { get; private set; } = default!;

        protected virtual void OnSetModel()
        {
        }

        protected virtual void OnResetModel()
        {
        }

        public void SetModel(TModel model)
        {
            if (Model != null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");

            Model = model;
            OnSetModel();
        }

        public void ResetModel()
        {
            if (Model == null)
                throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");

            if (IsOpened)
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");

            OnResetModel();
            Model = default!;
        }
    }
}