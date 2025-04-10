﻿using System;
using Bananva.UI.Dispatching.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceStellar.Meta.Ui.MainScreen.Views
{
    public class UguiBattleTileView : UguiView
    {
        [SerializeField] private TMP_Text title = default!;
        [SerializeField] private Image background = default!;
        [SerializeField] private Button button = default!;

        public event Action OnClick = delegate { };

        private void Awake()
        {
            button.onClick.AddListener(() => OnClick());
        }

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