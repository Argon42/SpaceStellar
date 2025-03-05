using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public interface IMainScreenView : IScreenView
    {
        IListView TilesListView { get; }
    }
}