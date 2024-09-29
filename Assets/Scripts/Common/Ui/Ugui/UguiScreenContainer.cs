using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceStellar.Common.Ui.Ugui
{
    public class UguiScreenContainer : IScreenContainer
    {
        private readonly Canvas _canvas;
        private readonly UguiScreenPrefabStorage _uguiScreenPrefabStorage;

        private readonly Dictionary<Type, IScreenView> _screenViews = new(); 
        
        public UguiScreenContainer(Canvas canvas, UguiScreenPrefabStorage uguiScreenPrefabStorage)
        {
            _canvas = canvas;
            _uguiScreenPrefabStorage = uguiScreenPrefabStorage;
        }

        public T GetScreen<T>() where T : IScreenView
        {
            if (_screenViews.TryGetValue(typeof(T), out var screenView))
            {
                return (T)screenView;
            }

            var prefab = _uguiScreenPrefabStorage.GetScreenPrefab<T>();
            var viewMonoBehaviour = Object.Instantiate(prefab as MonoBehaviour, _canvas.transform);
            if (viewMonoBehaviour == null)
            {
                throw new InvalidOperationException("Failed to instantiate screen prefab");
            }

            var view = viewMonoBehaviour.GetComponent<T>();
            _screenViews.Add(typeof(T), view);
            return view;
        }
    }
}