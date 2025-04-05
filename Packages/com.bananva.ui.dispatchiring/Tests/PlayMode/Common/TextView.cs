using Bananva.UI.Dispatching.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Bananva.UI.Dispatching.Tests.PlayMode.Common
{
    internal class TextView : UguiView
    {
        [SerializeField] private Text text;

        public void SetText(string value) => text.text = value;
    }
}