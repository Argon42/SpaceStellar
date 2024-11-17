using SpaceStellar.Common.Ui.Presenters;
using SpaceStellar.Game.Ui.MainScreen.Views;

namespace SpaceStellar.Game.Ui.MainScreen.Presenters
{
    public class SimpleTilePresenter : Presenter<MainMenuTileSimple, UguiSimpleTileView>
    {
        protected override void OnOpen()
        {
            View.SetText(Model.Name);
        }

        protected override void OnClose() { }
    }
}