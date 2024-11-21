using System;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Presenters
{
    public abstract class Presenter<TView> : PresentationLayerItem, IChangeableViewPresenter<TView>
    {
        public TView View { get; private set; } = default!;

        public void SetView(TView view)
        {
            if (View != null)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");
            }

            View = view;
            OnSetView();
        }

        public void ResetView()
        {
            if (View == null)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");
            }

            if (IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");
            }

            OnResetView();
            View = default!;
        }

        protected virtual void OnResetView() { }

        protected virtual void OnSetView() { }
    }

    public abstract class Presenter<TModel, TView> : Presenter<TView>, IConfigurablePresenter<TModel, TView>
        where TView : IView
    {
        public override bool IsOpenAvailable => base.IsOpenAvailable && Model != null;

        public TModel Model { get; private set; } = default!;

        protected virtual void OnSetModel() { }

        protected virtual void OnResetModel() { }

        public void SetModel(TModel model)
        {
            if (Model != null)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");
            }

            Model = model;
            OnSetModel();
        }

        public void ResetModel()
        {
            if (Model == null)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");
            }

            if (IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");
            }

            OnResetModel();
            Model = default!;
        }
    }
}