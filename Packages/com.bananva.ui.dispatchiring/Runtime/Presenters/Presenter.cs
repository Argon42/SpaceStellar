using System;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.Presenters
{
    public abstract class Presenter<TView> : PresentationLayerItem, IChangeableViewPresenter<TView>
    {
        private TView? _view;

        public TView View =>
            _view ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");

        public bool HasView => !Equals(_view, default(TView));

        public override bool IsOpenAvailable => base.IsOpenAvailable && HasView;

        public void SetView(TView view)
        {
            if (HasView)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");
            }

            _view = view;
            OnSetView();
        }

        public void ResetView()
        {
            if (!HasView)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");
            }

            if (IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");
            }

            OnResetView();
            _view = default;
        }

        protected virtual void OnResetView() { }

        protected virtual void OnSetView() { }
    }

    public abstract class Presenter<TModel, TView> : Presenter<TView>, IConfigurablePresenter<TModel, TView>
        where TView : IView
    {
        private TModel? _model;
        public override bool IsOpenAvailable => base.IsOpenAvailable && HasModel;

        public TModel Model =>
            _model ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");

        public bool HasModel => !Equals(_model, default(TModel));

        protected virtual void OnSetModel() { }

        protected virtual void OnResetModel() { }


        public void SetModel(TModel model)
        {
            if (HasModel)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has model already set");
            }

            _model = model;
            OnSetModel();
        }

        public void ResetModel()
        {
            if (!HasModel)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");
            }

            if (IsOpened)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} is opened");
            }

            OnResetModel();
            _model = default;
        }
    }
}