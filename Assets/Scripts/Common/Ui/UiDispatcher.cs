using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Utility;

namespace SpaceStellar.Common.Ui
{
    public class UiDispatcher
    {
        private readonly IPresenterProvider _presenterProvider;
        private readonly IScreenSwitcher _screenSwitcher;
        private readonly IWaitingWindowDispatcher _waitingWindowDispatcher;
        private readonly IWindowDispatcher _windowDispatcher;
        private readonly ILogger _logger;

        public UiDispatcher(
            IPresenterProvider presenterProvider,
            IWaitingWindowDispatcher waitingWindowDispatcher,
            IScreenSwitcher screenSwitcher,
            IWindowDispatcher windowDispatcher,
            ILogger logger)
        {
            _presenterProvider = presenterProvider;
            _waitingWindowDispatcher = waitingWindowDispatcher;
            _screenSwitcher = screenSwitcher;
            _windowDispatcher = windowDispatcher;
            _logger = logger;
        }

        public UniTask Open<TPresenter, TModel>(TModel model, CancellationToken token)
            where TPresenter : IPresentationLayerItem
        {
            var presenter = _presenterProvider.GetPresenter<TPresenter, TModel>();
            return SafeOpenAsync(presenter, model, token);
        }

        private async UniTask SafeOpenAsync<TPresenter, TModel>(
            TPresenter presenter,
            TModel model,
            CancellationToken token)
            where TPresenter : IPresentationLayerItem
        {
            try
            {
                await PreparePresenter(presenter, token);

                if (presenter is IScreenPresenter<TModel> screenPresenter)
                {
                    await _screenSwitcher.SwitchToScreen(screenPresenter, model);
                    return;
                }

                if (presenter is IWindowPresenter<TModel> windowPresenter)
                {
                    _windowDispatcher.ShowWindow(windowPresenter, model);
                }
            }
            catch (Exception e)
            {
                _logger.Exception(e);
                throw;
            }
        }

        private async Task PreparePresenter<TPresenter>(TPresenter presenter, CancellationToken token)
            where TPresenter : IPresentationLayerItem
        {
            if (presenter is not IPreparable preparable)
            {
                return;
            }

            if (preparable.IsPrepared)
            {
                return;
            }

            _waitingWindowDispatcher.CreateWaiting(this).Show();
            await preparable.Prepare(token);
            _waitingWindowDispatcher.Hide(this);
        }
    }
}