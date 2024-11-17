using System.Collections.Generic;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public interface IMainScreenModel
    {
        IReadOnlyList<MainMenuTile> Tiles { get; }
    }
}