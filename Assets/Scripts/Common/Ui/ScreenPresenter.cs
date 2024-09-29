using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui
{
    public abstract class ScreenPresenter<TModel, TView> : Presenter<TModel>,
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
    }
}