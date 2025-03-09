using Bananva.UI.Dispatchiring.Api;

namespace SpaceStellar.Bootstrap.Ui
{
    public interface IScreenLoading : IScreenView
    {
        void ShowProgress(float progress);
        void ShowProgressTitle(string message);
    }
}