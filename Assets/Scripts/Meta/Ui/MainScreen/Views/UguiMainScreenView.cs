using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Views;
using UnityEngine;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public class UguiMainScreenView : UguiScreenView, IMainScreenView
    {
        [SerializeField] private UguiPoolListView tilesListView = default!;

        public IListView TilesListView => tilesListView;
    }
}