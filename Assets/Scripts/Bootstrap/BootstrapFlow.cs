using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using SpaceStellar.Bootstrap.LoadUnits;
using SpaceStellar.Common;
using SpaceStellar.Utility;
using Zenject;
using ILogger = SpaceStellar.Utility.ILogger;

namespace SpaceStellar.Bootstrap
{
    public class BootstrapFlow : IInitializable, IDisposable
    {
        private readonly ILogger _logger;
        private readonly LoadingService _loadingService;
        private readonly ApplicationConfigurationLoadUnit _configurationLoadUnit;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly LoadingScreenService _loadingScreenService;
        private readonly CachedDataLoaderUnit _cachedDataLoaderUnit;
        private readonly ClientConfigurationLoadUnit _clientConfigurationLoadUnit;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public BootstrapFlow(
            ILogger logger,
            LoadingService loadingService,
            ApplicationConfigurationLoadUnit configurationLoadUnit,
            SceneSwitcher sceneSwitcher,
            LoadingScreenService loadingScreenService,
            CachedDataLoaderUnit cachedDataLoaderUnit,
            ClientConfigurationLoadUnit clientConfigurationLoadUnit)
        {
            _logger = logger;
            _loadingService = loadingService;
            _configurationLoadUnit = configurationLoadUnit;
            _sceneSwitcher = sceneSwitcher;
            _loadingScreenService = loadingScreenService;
            _cachedDataLoaderUnit = cachedDataLoaderUnit;
            _clientConfigurationLoadUnit = clientConfigurationLoadUnit;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public async void Initialize()
        {
            _logger.Debug("Starting application");
            _loadingScreenService.EnableLoadingScreen();
            _loadingScreenService.ShowProgress("Loading...", 0);
            await _loadingService.BeginLoading(_configurationLoadUnit);
            await _loadingService.BeginLoading(_clientConfigurationLoadUnit);
            _loadingScreenService.UpdateProgress(0.5f);
            await _loadingService.BeginLoading(_cachedDataLoaderUnit);

            var activateScene = await LoadGameScene();

            _logger.Debug("Game scene is loaded");
            _loadingScreenService.DisableLoadingScreen();

            _logger.Debug("Activating scene");
            activateScene?.Invoke();
        }

        private async UniTask<Action> LoadGameScene()
        {
            _loadingScreenService.ShowProgress("Loading scene...", 0);

            var loading = _sceneSwitcher.SwitchTo(GameScenes.Meta);
            loading.allowSceneActivation = false;
            using var cts = new CancellationTokenSource();
            _ = Observable
                .EveryValueChanged(loading, x => x.progress)
                .ForEachAsync(_ =>
                        _loadingScreenService.UpdateProgress(loading.progress),
                    cts.Token);
            await UniTask.WaitUntil(() => loading.progress >= 0.9f, cancellationToken: cts.Token);
            cts.Cancel();
            return () => { loading.allowSceneActivation = true; };
        }
    }
}