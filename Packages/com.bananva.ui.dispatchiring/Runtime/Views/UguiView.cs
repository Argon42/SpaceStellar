using Bananva.UI.Dispatchiring.Api;
using UnityEngine;

namespace Bananva.UI.Dispatchiring.Ugui
{
    public class UguiView : MonoBehaviour, IView
    {
        public bool IsActive => this != null && gameObject.activeSelf;
        public bool IsShow => this != null && gameObject.activeInHierarchy;

        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
    }
}