using System;
using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Ugui;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Views
{
    public class PoolListView : UguiView, IListView
    {
        [SerializeField] private UguiView itemPrefab = default!;
        [SerializeField] private RectTransform contentParent = default!;
        [SerializeField] private List<UguiView> pool = new();

        private readonly List<UguiView> _binded = new();

        public Func<int, IView>? OnBind { get; set; }
        public Action<int>? OnUnbind { get; set; }

        public bool IsInitialized => true;
        public event Action Initialized = delegate { };
        public int ItemsCount => _binded.Count;

        public bool InsertAtIndexSupported => true;

        private void Awake() => pool.ForEach(view => view.Deactivate());

        public void Init()
        {
            // not need init
        }

        public TView Spawn<TView>() where TView : class, IView
        {
            if (itemPrefab is TView)
                return GetFromPool() as TView
                       ?? throw new InvalidOperationException();
            throw new InvalidOperationException($"{nameof(PoolListView)} item prefab is not {typeof(TView).Name}");
        }

        public void ResetItems(int newCount)
        {
            for (int i = _binded.Count - 1; i >= 0; i--)
            {
                OnUnbind?.Invoke(i);
                ReturnToPool(_binded[i]);
            }

            InsertViews(0, newCount);
        }

        public void InsertItems(int index, int itemsCount) =>
            InsertViews(index, itemsCount);

        private UguiView GetFromPool()
        {
            if (pool.Count == 0)
            {
                var view = Instantiate(itemPrefab, contentParent);
                _binded.Add(view);
                return view;
            }

            var fromPool = pool[^1];
            pool.RemoveAt(pool.Count - 1);
            fromPool.gameObject.SetActive(true);
            _binded.Add(fromPool);
            return fromPool;
        }

        private void InsertViews(int fromIndex, int count)
        {
            for (var i = fromIndex; i < fromIndex + count; i++)
            {
                var view = OnBind?.Invoke(i);

                if (view == null)
                {
                    throw new InvalidOperationException("PoolListView view from OnBind is null");
                }

                if (view is not UguiView uguiView)
                {
                    throw new InvalidOperationException($"PoolListView view from OnBind is not {nameof(UguiView)}");
                }

                uguiView.transform.SetSiblingIndex(i);
                _binded.Add(uguiView);
            }
        }

        private void ReturnToPool(UguiView view)
        {
            _binded.Remove(view);
            pool.Add(view);
            view.Deactivate();
            view.transform.SetAsLastSibling();
        }
    }
}