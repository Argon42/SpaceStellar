using Bananva.UI.Dispatching.Api;
using UnityEngine;

namespace Bananva.UI.Dispatching.Views
{
    public abstract class UguiListViewProvider : MonoBehaviour
    {
        public abstract TView Spawn<TView>() where TView : class, IView;
        public abstract void ReturnToPool(UguiView view);
    }
}