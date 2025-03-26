using Bananva.UI.Dispatchiring.Ugui;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.PlayMode.Common
{
    internal class TextView : UguiView
    {
        [SerializeField] private Text text;

        public void SetText(string value) => text.text = value;
    }
}