using System.Collections.Generic;

namespace SpaceStellar.Meta.Ui.MainScreen.Models
{
    public interface IMainScreenModel
    {
        IReadOnlyList<MainMenuTile> Tiles { get; }
    }
}