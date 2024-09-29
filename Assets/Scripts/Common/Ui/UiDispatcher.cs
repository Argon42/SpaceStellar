using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui
{
    public class UiDispatcher
    {
        private readonly IPresenterProvider _presenterProvider;
        private readonly IScreenSwitcher _screenSwitcher;
        private readonly IWaitingWindowDispatcher _waitingWindowDispatcher;
        private readonly IWindowDispatcher _windowDispatcher;


        public UiDispatcher(
            IPresenterProvider presenterProvider,
            IWaitingWindowDispatcher waitingWindowDispatcher,
            IScreenSwitcher screenSwitcher,
            IWindowDispatcher windowDispatcher)
        {
            _presenterProvider = presenterProvider;
            _waitingWindowDispatcher = waitingWindowDispatcher;
            _screenSwitcher = screenSwitcher;
            _windowDispatcher = windowDispatcher;
        }

        public UniTask Open<TPresenter, TModel>(TModel model, CancellationToken token)
            where TPresenter : IPresenter<TModel>
        {
            TPresenter presenter = _presenterProvider.GetPresenter<TPresenter, TModel>();
            return SafeOpenAsync(presenter, model, token);
        }

        private async UniTask SafeOpenAsync<TPresenter, TModel>(
            TPresenter presenter,
            TModel model,
            CancellationToken token)
            where TPresenter : IPresenter<TModel>
        {
            try
            {
                await PreparePresenter<TPresenter, TModel>(presenter, token);

                if (presenter is IScreenPresenter<TModel> screenPresenter)
                {
                    await _screenSwitcher.SwitchToScreen(screenPresenter, model);
                    return;
                }

                if (presenter is IWindowPresenter<TModel> windowPresenter)
                    _windowDispatcher.ShowWindow(windowPresenter, model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task PreparePresenter<TPresenter, TModel>(TPresenter presenter, CancellationToken token)
            where TPresenter : IPresenter<TModel>
        {
            if (presenter is not IPreparable preparable)
                return;

            if (preparable.IsPrepared)
                return;

            _waitingWindowDispatcher.CreateWaiting(this).Show();
            await preparable.Prepare(token);
            _waitingWindowDispatcher.Hide(this);
        }
    }
}
