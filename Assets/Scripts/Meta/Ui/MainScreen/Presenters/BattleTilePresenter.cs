using SpaceStellar.CardFightGame;
using SpaceStellar.CardFightGame.Core;
using SpaceStellar.CardFightGame.Core.Data;
using SpaceStellar.Common;
using SpaceStellar.Common.Ui.Presenters;
using SpaceStellar.Meta.Ui.MainScreen.Models;
using SpaceStellar.Meta.Ui.MainScreen.Views;
using UnityEngine;

namespace SpaceStellar.Meta.Ui.MainScreen.Presenters
{
    public class BattleTilePresenter : Presenter<MainMenuTileBattle, UguiBattleTileView>
    {
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly BattleConfigurationProvider _provider;

        public BattleTilePresenter(SceneSwitcher sceneSwitcher, BattleConfigurationProvider provider)
        {
            _sceneSwitcher = sceneSwitcher;
            _provider = provider;
        }

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
            Debug.Log("Battle Tile Clicked. Switch to Card Fight Game.");
            // TODO: вырезать это отсюда в отдельный сервис/модель
            _provider.OnlineMode = OnlineMode.Offline;
            _sceneSwitcher.SwitchTo(GameScenes.CardFightGame);
        }
    }
}