using SpaceStellar.Common.Ui.Presenters;
using SpaceStellar.Meta.Ui.MainScreen.Views;
using UnityEngine;

namespace SpaceStellar.Meta.Ui.MainScreen.Presenters
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