using Bananva.UI.Dispatching.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Bananva.UI.Dispatching.Tests.PlayMode.Common
{
    internal class ImageView : UguiView
    {
        [SerializeField] private Image image;
        [SerializeField] private Text text;

        public void SetData(Color color, int value)
        {
            image.color = color;
            text.text = value.ToString();
        }
    }
}