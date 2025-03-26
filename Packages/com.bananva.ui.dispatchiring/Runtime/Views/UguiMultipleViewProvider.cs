using System;
using System.Collections.Generic;
using System.Linq;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Ugui;
using UnityEngine;

namespace Bananva.UI.Dispatchiring.Views
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
            typedView.Activate();
            return typedView;
        }

        public override void ReturnToPool(UguiView view)
        {
            view.Deactivate();
            view.transform.SetAsLastSibling();
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
            view.Activate();
            return view as TView ?? throw new Exception($"Prefab of type {typeof(TView)} is not {typeof(TView)}");
        }
    }
}