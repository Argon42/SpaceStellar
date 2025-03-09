using System;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.Presenters.Wrappers
{
    public sealed class PresenterWrapper<TPresenter, TModel, TView> : PresenterTypelessWrapper
        where TPresenter : IConfigurablePresenter<TModel, TView>
        where TView : class, IView
    {
        private TPresenter Presenter { get; }
        private TView? _view;
        private TModel? _model;

        public override bool IsOpened => _view != null;
        public override IPresentationLayerItem LayerItem => Presenter;

        public override IView View =>
            _view ?? throw new InvalidOperationException("PresenterWrapper has no view set");

        public override object Model =>
            _model ?? throw new InvalidOperationException("PresenterWrapper has no model set");

        public PresenterWrapper(TPresenter presenter)
        {
            Presenter = presenter;
        }

        public override void Open(IView view, object model)
        {
            if (view is not TView typedView)
            {
                throw new ArgumentException($"View is not {typeof(TView).Name}");
            }

            if (model is not TModel typedModel)
            {
                throw new ArgumentException($"Model is not {typeof(TModel).Name}");
            }

            _view = typedView;
            _model = typedModel;
            Presenter.SetView(typedView);
            Presenter.SetModel(typedModel);
            Presenter.Open();
        }

        public override void Close()
        {
            Presenter.Close();
            Presenter.ResetView();
            Presenter.ResetModel();
            _view = default;
        }
    }
}