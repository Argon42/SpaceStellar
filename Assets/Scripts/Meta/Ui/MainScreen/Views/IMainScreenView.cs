using Bananva.UI.Dispatchiring.Api;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public interface IMainScreenView : IScreenView
    {
        IListView TilesListView { get; }
    }
}