﻿using System;
using System.Collections.Generic;
using Bananva.UI.Dispatching.Api;
using UnityEngine;

namespace Bananva.UI.Dispatching.Views
{
    public class UguiPoolListView : UguiView, IListView
    {
        [SerializeField] private UguiListViewProvider provider = default!;

        private readonly List<UguiView> _binded = new();

        private Func<int, IView>? OnBind { get; set; }
        private Action<int>? OnUnbind { get; set; }

        public bool IsInitialized => true;
        public event Action Initialized = delegate { };
        public int ItemsCount => _binded.Count;

        public bool WorkWithIndexSupported => true;

        public void Init() { }

        public void StartWork(Func<int, IView> onBind, Action<int> onUnbind)
        {
            OnBind = onBind ?? throw new ArgumentNullException(nameof(onBind));
            OnUnbind = onUnbind ?? throw new ArgumentNullException(nameof(onUnbind));
        }

        public void StopWork()
        {
            ResetItems(0);
            OnBind = null;
            OnUnbind = null;
        }

        public TView Spawn<TView>() where TView : class, IView => provider.Spawn<TView>();

        public void ResetItems(int newCount)
        {
            RemoveItems(0, _binded.Count);
            InsertViews(0, newCount);
        }

        public void InsertItems(int index, int itemsCount) => InsertViews(index, itemsCount);

        public void RemoveItems(int index, int itemsCount)
        {
            for (int i = index + itemsCount - 1; i >= index; i--)
            {
                RemoveView(i);
            }
        }

        public void ReplaceItem(int index)
        {
            RemoveView(index);
            CreateView(index);
        }

        private void InsertViews(int fromIndex, int count)
        {
            for (var i = fromIndex; i < fromIndex + count; i++)
            {
                CreateView(i);
            }
        }

        private void RemoveView(int index)
        {
            OnUnbind?.Invoke(index);
            var view = _binded[index];
            _binded.Remove(view);
            provider.ReturnToPool(view);
        }

        private void CreateView(int i)
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