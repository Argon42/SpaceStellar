using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Bootstrap.Ui
{
    public interface IScreenLoading : IScreenView
    {
        void ShowProgress(float progress);
        void ShowProgressTitle(string message);
    }
}