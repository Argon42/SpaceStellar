using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public interface IMainScreenView : IScreenView
    {
        IListView TilesListView { get; }
    }
}