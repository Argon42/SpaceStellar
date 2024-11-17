namespace SpaceStellar.Game.Ui.MainScreen
{
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
}