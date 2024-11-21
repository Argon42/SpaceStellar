using System.Collections.Generic;

namespace SpaceStellar.Meta.Ui.MainScreen
{
    public interface IMainScreenModel
    {
        IReadOnlyList<MainMenuTile> Tiles { get; }
    }
}