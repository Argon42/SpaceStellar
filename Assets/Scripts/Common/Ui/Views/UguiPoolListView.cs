using System;
using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Ugui;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Views
{
    public class UguiPoolListView : UguiView, IListView
    {
        [SerializeField] private UguiListViewProvider provider = default!;

        private readonly List<UguiView> _binded = new();

        public Func<int, IView>? OnBind { get; set; }
        public Action<int>? OnUnbind { get; set; }

        public bool IsInitialized => true;
        public event Action Initialized = delegate { };
        public int ItemsCount => _binded.Count;

        public bool InsertAtIndexSupported => true;

        public void Init() { }

        public TView Spawn<TView>() where TView : class, IView => provider.Spawn<TView>();

        public void ResetItems(int newCount)
        {
            for (int i = _binded.Count - 1; i >= 0; i--)
            {
                OnUnbind?.Invoke(i);
                _binded.Remove(_binded[i]);
                provider.ReturnToPool(_binded[i]);
            }

            InsertViews(0, newCount);
        }

        public void InsertItems(int index, int itemsCount) =>
            InsertViews(index, itemsCount);


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
    }
}