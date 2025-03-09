using System;
using System.Threading;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Presenters
{
    public abstract class ScreenPresenter<TModel, TView> : PresentationLayerItem,
        IScreenPresenter<TModel>,
        IPreparable,
        IOptimizable
        where TView : class, IScreenView
    {
        protected TView? ScreenView { get; private set; }
        public TModel? Model { get; private set; }

        private readonly IViewProvider _viewProvider;
        public override bool IsOpenAvailable => base.IsOpenAvailable && ScreenView != null && IsPrepared;
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

            ScreenView = await GetScreenView(token);
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

        protected abstract UniTask OnSetView(TView view, CancellationToken token);

        protected abstract UniTask OnResetView(CancellationToken token);

        protected virtual void OnSetModel() { }

        protected virtual void OnResetModel() { }

        private void ReleaseView()
        {
            if (ScreenView == null)
            {
                return;
            }

            _viewProvider.Release(ScreenView);
            IsPrepared = false;
            ScreenView = null;
        }
    }
}