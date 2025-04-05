using System;
using System.Threading;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Presenters
{
    // TODO: разобраться с дублированием логики с SreenPresenter
    public abstract class WindowPresenter<TModel, TView> : PresentationLayerItem,
        IWindowPresenter<TModel>,
        IPreparable,
        IOptimizable
        where TView : class, IWindowView
    {
        private TView? _windowView;
        private TModel? _model;
        private readonly IViewProvider _viewProvider;

        protected TView WindowView =>
            _windowView ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");

        public TModel Model =>
            _model ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");

        public bool HasModel => !Equals(_model, default(TModel));
        
        public override bool IsOpenAvailable => base.IsOpenAvailable && HasModel && IsPrepared;
        public bool IsPrepared { get; private set; }
        public virtual bool IsCanOptimize => IsPrepared && !IsOpened;

        protected WindowPresenter(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public async UniTask Prepare(CancellationToken token)
        {
            if (IsPrepared)
            {
                return;
            }

            _windowView = await GetScreenView(token);
            try
            {
                await OnSetView(WindowView, token);
                IsPrepared = true;
            }
            catch
            {
                ReleaseView();
                throw;
            }
        }

        async UniTask IOptimizable.Optimize(CancellationToken token)
        {
            if (WindowView == null)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");
            }

            if (!IsCanOptimize)
            {
                throw new InvalidOperationException($"Presenter {GetType().Name} can't be optimized");
            }

            await OnResetView(token);
            ReleaseView();
        }

        private UniTask<TView> GetScreenView(CancellationToken token)
        {
            return _viewProvider.TryGetOrLoadView<TView>(token);
        }

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
            _model = default!;
        }

        protected abstract UniTask OnSetView(TView view, CancellationToken token);
        protected abstract UniTask OnResetView(CancellationToken token);

        protected virtual void OnSetModel() { }
        protected virtual void OnResetModel() { }

        private void ReleaseView()
        {
            if (_windowView == null)
            {
                return;
            }

            _viewProvider.Release(_windowView);
            IsPrepared = false;
            _windowView = null;
        }
    }
}