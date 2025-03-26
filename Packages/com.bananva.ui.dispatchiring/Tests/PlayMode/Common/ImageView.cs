using Bananva.UI.Dispatchiring.Ugui;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.PlayMode.Common
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