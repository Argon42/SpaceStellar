using Bananva.UI.Dispatching.Views;
using TMPro;
using UnityEngine;

namespace SpaceStellar.Bootstrap.Ui.Ugui
{
    public class UguiScreenLoading : UguiScreenView, IScreenLoading
    {
        [SerializeField] private TextMeshProUGUI titleText = default!;
        [SerializeField] private TextMeshProUGUI progressText = default!;

        public void ShowProgress(float progress)
        {
            progressText.text = $"{progress * 100:F0}%";
        }

        public void ShowProgressTitle(string message)
        {
            titleText.text = message;
        }
    }
}