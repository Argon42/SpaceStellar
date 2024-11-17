using SpaceStellar.Common.Ui.Presenters;
using SpaceStellar.Game.Ui.MainScreen.Views;
using UnityEngine;

namespace SpaceStellar.Game.Ui.MainScreen.Presenters
{
    public class BattleTilePresenter : Presenter<MainMenuTileBattle, UguiBattleTileView>
    {
        protected override void OnOpen()
        {
            View.SetText(Model.Name);
            View.OnClick += OnClick;
        }

        protected override void OnClose()
        {
            View.OnClick -= OnClick;
        }

        private void OnClick()
        {
            Debug.Log("Battle Tile Clicked");
        }
    }
}