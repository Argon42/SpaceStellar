using SpaceStellar.Common.Ui.Ugui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public class UguiSimpleTileView : UguiView
    {
        [SerializeField] private TMP_Text title = default!;
        [SerializeField] private Image background = default!;

        public void SetText(string titleText)
        {
            title.text = titleText;
        }

        public void SetColor(Color color)
        {
            background.color = color;
        }
    }
}