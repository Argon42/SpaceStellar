using SpaceStellar.Bootstrap.Ui;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Bootstrap
{
    public class LoadingScreenService
    {
        private readonly IScreenLoading _loadingScreen;

        public LoadingScreenService(IViewProvider screenContainer)
        {
            _loadingScreen = screenContainer.LoadView<IScreenLoading>(default).GetAwaiter().GetResult();
        }

        public void UpdateProgress(float progress)
        {
            _loadingScreen.ShowProgress(progress);
        }

        public void EnableLoadingScreen()
        {
            if (!_loadingScreen.IsActive)
            {
                _loadingScreen.Activate();
            }
        }

        public void DisableLoadingScreen()
        {
            if (_loadingScreen.IsActive)
            {
                _loadingScreen.Deactivate();
            }
        }

        public void ShowProgressTitle(string message)
        {
            if (_loadingScreen.IsActive)
            {
                _loadingScreen.ShowProgressTitle(message);
            }
        }

        public void ShowProgress(string message, float progress)
        {
            _loadingScreen.ShowProgressTitle(message);
            _loadingScreen.ShowProgress(progress);
        }
    }
}