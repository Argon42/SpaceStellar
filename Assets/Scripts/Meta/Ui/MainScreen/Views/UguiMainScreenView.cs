using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Ugui;
using Bananva.UI.Dispatchiring.Views;
using UnityEngine;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public class UguiMainScreenView : UguiScreenView, IMainScreenView
    {
        [SerializeField] private UguiPoolListView tilesListView = default!;

        public IListView TilesListView => tilesListView;
    }
}