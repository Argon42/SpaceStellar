using System.Collections.Generic;

namespace SpaceStellar.Meta.Ui.MainScreen.Models
{
    public class MainScreenModel : IMainScreenModel
    {
        private readonly List<MainMenuTile> _tiles = new()
        {
            new MainMenuTileSimple("StartNewCampaign", "placeholder", "", false),
            new MainMenuTileSimple("Settings", "placeholder", "", false),
            new MainMenuTileSimple("Chat", "placeholder", "", false),
            new MainMenuTileBattle("BattlePVE", "placeholder", "", false, true),
            new MainMenuTileBattle("BattlePVP", "placeholder", "", false, false),
            new MainMenuTileSimple("Exit", "placeholder", "", false)
        };

        public IReadOnlyList<MainMenuTile> Tiles => _tiles;
    }
}