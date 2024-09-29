using SpaceStellar.Common.Ui.Abstraction;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Ugui
{
    public class UguiView : MonoBehaviour, IView
    {
        public bool IsActive => this != null && gameObject.activeSelf;
        
        public void Activate() => gameObject.SetActive(true);

        public void Deactivate() => gameObject.SetActive(false);
    }
}