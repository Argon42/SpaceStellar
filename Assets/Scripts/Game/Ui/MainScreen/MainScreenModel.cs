using System.Collections.Generic;

namespace SpaceStellar.Game.Ui.MainScreen
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
            new MainMenuTileSimple("Exit", "placeholder", "", false),
        };

        public IReadOnlyList<MainMenuTile> Tiles => _tiles;
    }

    public abstract class MainMenuTile
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public bool TileLocked { get; set; }

        protected MainMenuTile(string name, string description, string background, bool tileLocked)
        {
            Name = name;
            Description = description;
            Background = background;
            TileLocked = tileLocked;
        }
    }

    public class MainMenuTileSimple : MainMenuTile
    {
        public MainMenuTileSimple(string name,
            string description,
            string background,
            bool tileLocked) : base(name, description, background, tileLocked) { }
    }

    public class MainMenuTileBattle : MainMenuTile
    {
        public bool ButtonEnabled { get; set; }

        public MainMenuTileBattle(string name,
            string description,
            string background,
            bool tileLocked,
            bool buttonEnabled) : base(name, description, background, tileLocked)
        {
            ButtonEnabled = buttonEnabled;
        }
    }
}