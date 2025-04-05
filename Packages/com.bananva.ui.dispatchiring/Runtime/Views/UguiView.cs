using Bananva.UI.Dispatching.Api;
using UnityEngine;

namespace Bananva.UI.Dispatching.Views
{
    public abstract class UguiView : MonoBehaviour, IView
    {
        public bool IsActive => this != null && gameObject.activeSelf;
        public bool IsShow => this != null && gameObject.activeInHierarchy;

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
    }
}