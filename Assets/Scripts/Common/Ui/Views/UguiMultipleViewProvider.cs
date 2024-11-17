using System;
using System.Collections.Generic;
using System.Linq;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Ugui;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Views
{
    public class UguiMultipleViewProvider : UguiListViewProvider
    {
        [SerializeField] private List<UguiView> prefabs = new();
        [SerializeField] private RectTransform contentParent = default!;

        private List<UguiView> _viewPool = new();

        public override TView Spawn<TView>()
        {
            if (_viewPool.Count == 0)
            {
                return CreateNew<TView>();
            }

            var view = _viewPool.FirstOrDefault(uguiView => uguiView is TView);
            if (view == default || view is not TView typedView)
            {
                return CreateNew<TView>();
            }

            _viewPool.Remove(view);
            return typedView;
        }

        public override void ReturnToPool(UguiView view)
        {
            view.Deactivate();
            _viewPool.Add(view);
        }

        private TView CreateNew<TView>() where TView : class, IView
        {
            var prefab = prefabs.FirstOrDefault(uguiView => uguiView is TView);
            if (prefab == null)
            {
                throw new Exception($"Prefab of type {typeof(TView)} not found");
            }

            var view = Instantiate(prefab, contentParent);
            return view as TView ?? throw new Exception($"Prefab of type {typeof(TView)} is not {typeof(TView)}");
        }
    }
}