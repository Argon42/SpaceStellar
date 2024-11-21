using System;
using System.Collections.Generic;
using SpaceStellar.Common.Ui.Ugui;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Views
{
    public class UguiSingleViewPool : UguiListViewProvider
    {
        [SerializeField] private UguiView itemPrefab = default!;
        [SerializeField] private RectTransform contentParent = default!;
        [SerializeField] private List<UguiView> pool = new();

        private void Awake()
        {
            pool.ForEach(view => view.Deactivate());
        }

        public override TView Spawn<TView>()
        {
            if (itemPrefab is TView)
            {
                return GetFromPool() as TView
                       ?? throw new InvalidOperationException();
            }

            throw new InvalidOperationException($"{nameof(UguiPoolListView)} item prefab is not {typeof(TView).Name}");
        }

        private UguiView GetFromPool()
        {
            if (pool.Count == 0)
            {
                var view = Instantiate(itemPrefab, contentParent);
                return view;
            }

            var fromPool = pool[^1];
            pool.RemoveAt(pool.Count - 1);
            fromPool.Activate();
            return fromPool;
        }

        public override void ReturnToPool(UguiView view)
        {
            pool.Add(view);
            view.Deactivate();
            view.transform.SetAsLastSibling();
        }
    }
}