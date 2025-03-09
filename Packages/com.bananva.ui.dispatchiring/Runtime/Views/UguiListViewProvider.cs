using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Ugui;
using UnityEngine;

namespace Bananva.UI.Dispatchiring.Views
{
    public abstract class UguiListViewProvider : MonoBehaviour
    {
        public abstract TView Spawn<TView>() where TView : class, IView;
        public abstract void ReturnToPool(UguiView view);
    }
}