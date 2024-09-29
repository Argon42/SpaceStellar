using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using R3;
using SpaceStellar.Common;
using SpaceStellar.Utility;
using Zenject;

namespace SpaceStellar.Bootstrap
{
    public class BootstrapFlow : IInitializable, IDisposable
    {
        private readonly ILogger _logger;
        private readonly LoadingService _loadingService;
        private readonly ApplicationConfigurationLoadUnit _configurationLoadUnit;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly LoadingScreenService _loadingScreenService;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public BootstrapFlow(
            ILogger logger,
            LoadingService loadingService,
            ApplicationConfigurationLoadUnit configurationLoadUnit,
            SceneSwitcher sceneSwitcher,
            LoadingScreenService loadingScreenService)
        {
            _logger = logger;
            _loadingService = loadingService;
            _configurationLoadUnit = configurationLoadUnit;
            _sceneSwitcher = sceneSwitcher;
            _loadingScreenService = loadingScreenService;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public async void Initialize()
        {
            _loadingScreenService.EnableLoadingScreen();
            _loadingScreenService.ShowProgress("Loading...", 0);
            _logger.Debug("Starting application...");
            await _loadingService.BeginLoading(_configurationLoadUnit);
            _logger.Debug("Application is started");
            _loadingScreenService.ShowProgress("Loading scene...", 0);
            await LoadGameScene();
            _logger.Debug("Game scene is loaded");
            _loadingScreenService.DisableLoadingScreen();
        }

        private async UniTask LoadGameScene()
        {
            var loading = _sceneSwitcher.SwitchTo(GameScenes.Game);
            using var cts = new CancellationTokenSource();
            _ = Observable
                .EveryValueChanged(loading, x => x.progress)
                .ForEachAsync(unit =>
                        _loadingScreenService.UpdateProgress(loading.progress),
                    cancellationToken: cts.Token);
            await loading.ToUniTask(cancellationToken: cts.Token);
            cts.Cancel();
        }
    }
}