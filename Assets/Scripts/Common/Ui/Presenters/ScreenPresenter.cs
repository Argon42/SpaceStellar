using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Presenters
{
    public abstract class ScreenPresenter<TModel, TView> : PresentationLayerItem,
        IScreenPresenter<TModel>,
        IPreparable
        where TView : class, IScreenView
    {
        protected TView? ScreenView { get; private set; }

        private readonly IViewProvider _viewProvider;
        public override bool IsOpenAvailable => base.IsOpenAvailable && ScreenView != null && IsPrepared;
        public bool IsPrepared { get; private set; }

        protected ScreenPresenter(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public async UniTask Prepare(CancellationToken token)
        {
            if (IsPrepared)
                return;

            ScreenView = await GetScreenView(token);
            IsPrepared = true;
        }

        private UniTask<TView> GetScreenView(CancellationToken token) =>
            _viewProvider.TryGetOrLoadView<TView>(token);

        public TModel Model { get; private set; }

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

        protected virtual void OnSetModel() { }
        protected virtual void OnResetModel() { }
    }
}