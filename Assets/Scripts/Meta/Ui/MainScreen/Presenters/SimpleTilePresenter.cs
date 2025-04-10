﻿using Bananva.UI.Dispatching.Presenters;
using SpaceStellar.Meta.Ui.MainScreen.Models;
using SpaceStellar.Meta.Ui.MainScreen.Views;

namespace SpaceStellar.Meta.Ui.MainScreen.Presenters
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