using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Ugui;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Views
{
    public abstract class UguiListViewProvider : MonoBehaviour
    {
        public abstract TView Spawn<TView>() where TView : class, IView;
        public abstract void ReturnToPool(UguiView view);
    }
}