namespace SpaceStellar.Meta.Ui.MainScreen
{
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