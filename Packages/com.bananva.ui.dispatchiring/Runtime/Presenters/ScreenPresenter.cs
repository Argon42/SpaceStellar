using System;
using System.Threading;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Presenters
{
    public abstract class ScreenPresenter<TModel, TView> : PresentationLayerItem,
        IScreenPresenter<TModel>,
        IPreparable,
        IOptimizable
        where TView : class, IScreenView
    {
        private TView? _screenView;
        private TModel? _model;
        private readonly IViewProvider _viewProvider;

        protected TView ScreenView =>
            _screenView ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no view set");

        public TModel Model =>
            _model ?? throw new InvalidOperationException($"Presenter {GetType().Name} has no model set");

        public bool HasModel => !Equals(_model, default(TModel));

        public override bool IsOpenAvailable => base.IsOpenAvailable && _screenView != null && IsPrepared;

        public bool IsPrepared { get; private set; }
        public virtual bool IsCanOptimize => IsPrepared && !IsOpened;

        protected ScreenPresenter(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public async UniTask Prepare(CancellationToken token)
        {
            if (IsPrepared)
            {
                return;
            }

            _screenView = await GetScreenView(token);
            try
            {
                await OnSetView(ScreenView, token);
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
            if (ScreenView == null)
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
            _model = default;
        }

        protected abstract UniTask OnSetView(TView view, CancellationToken token);

        protected abstract UniTask OnResetView(CancellationToken token);

        protected virtual void OnSetModel() { }

        protected virtual void OnResetModel() { }

        private void ReleaseView()
        {
            if (_screenView == null)
            {
                return;
            }

            _viewProvider.Release(_screenView);
            IsPrepared = false;
            _screenView = null;
        }
    }
}