using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Presenters;

namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    // TODO: разобраться с дублированием логики с SreenPresenter
    public abstract class WindowPresenter<TModel, TView> : PresentationLayerItem,
        IWindowPresenter<TModel>,
        IPreparable,
        IOptimizable
        where TView : class, IWindowView
    {
        protected TView? WindowView { get; private set; }
        public TModel? Model { get; private set; }
        private readonly IViewProvider _viewProvider;
        public override bool IsOpenAvailable => base.IsOpenAvailable && WindowView != null && IsPrepared;
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

            WindowView = await GetScreenView(token);
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
            if (WindowView == null)
            {
                return;
            }

            _viewProvider.Release(WindowView);
            IsPrepared = false;
            WindowView = null;
        }
    }
}