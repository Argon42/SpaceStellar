using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SpaceStellar.Common.Ui.Ugui
{
    public class UguiScreenContainer : IViewProvider
    {
        private readonly Canvas _canvas;
        private readonly UguiScreenPrefabStorage _uguiScreenPrefabStorage;

        private readonly Dictionary<Type, IView> _screenViews = new();

        public UguiScreenContainer(Canvas canvas, UguiScreenPrefabStorage uguiScreenPrefabStorage)
        {
            _canvas = canvas;
            _uguiScreenPrefabStorage = uguiScreenPrefabStorage;
        }


        public bool TryGetView<TView>([NotNullWhen(true)] out TView? view) where TView : class, IView
        {
            if (_screenViews.TryGetValue(typeof(TView), out var screenView))
            {
                view = (TView)screenView;
                return true;
            }

            view = default!;
            return false;
        }

        public UniTask<TView> LoadView<TView>(CancellationToken token) where TView : class, IView
        {
            var prefab = _uguiScreenPrefabStorage.GetScreenPrefab<TView>();
            if (prefab == null)
            {
                throw new NotSupportedException($"View {typeof(TView).Name} not found in config");
            }

            var viewMonoBehaviour = Object.Instantiate(prefab as MonoBehaviour, _canvas.transform);
            if (viewMonoBehaviour == null)
            {
                throw new InvalidOperationException("Failed to instantiate screen prefab");
            }

            var view = viewMonoBehaviour.GetComponent<TView>();
            _screenViews.Add(typeof(TView), view);
            return UniTask.FromResult(view);
        }
    }
}