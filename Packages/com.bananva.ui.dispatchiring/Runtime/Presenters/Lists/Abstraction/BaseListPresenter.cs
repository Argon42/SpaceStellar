using System;
using System.Collections.Generic;
using System.Linq;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;
using Bananva.UI.Dispatching.Presenters.Wrappers;
using UnityEngine;

namespace Bananva.UI.Dispatching.Presenters.Lists.Abstraction
{
    public abstract class BaseListPresenter<TCollection, TModel> :
        Presenter<TCollection, IListView>,
        IListPresenter<TCollection>
        where TModel : class
        where TCollection : class
    {
        private readonly IPresenterViewPool _pool;
        private readonly Dictionary<int, PresenterTypelessWrapper> _presenters = new();
        private bool _isWaitInitializing;
        private readonly List<int> _keysToShift = new();

        protected BaseListPresenter(IPresenterViewPool pool) => _pool = pool;

        protected abstract int GetCountOfElements();

        protected abstract TModel GetElementByIndex(int index);

        protected abstract void OnOpenInternal();

        protected abstract void OnCloseInternal();

        protected void UpdateList()
        {
            if (!TryPrepareAdapter())
            {
                View.ResetItems(GetCountOfElements());
            }
        }

        protected void InsertElements(int from, int count)
        {
            if (TryPrepareAdapter())
            {
                return;
            }

            if (!View.WorkWithIndexSupported)
            {
                View.ResetItems(GetCountOfElements());
                return;
            }

            ShiftElements(from, count);
            View.InsertItems(from, count);
        }

        protected void RemoveElements(int from, int count)
        {
            if (TryPrepareAdapter())
            {
                return;
            }

            if (!View.WorkWithIndexSupported)
            {
                View.ResetItems(GetCountOfElements());
                return;
            }

            View.RemoveItems(from, count);
            ShiftElements(from, -count);
        }

        protected void ReplaceElement(int index)
        {
            if (!_presenters.ContainsKey(index) || TryPrepareAdapter())
            {
                return;
            }
            
            View.ReplaceItem(index);
        }

        protected sealed override void OnOpen()
        {
            View.StartWork(OnViewBinding, OnViewUnbind);
            OnOpenInternal();
            UpdateList();
        }

        protected sealed override void OnClose()
        {
            OnCloseInternal();

            if (_isWaitInitializing)
            {
                View.Initialized -= OnInitialized;
                _isWaitInitializing = false;
            }

            if (View.IsInitialized)
            {
                View.ResetItems(0);
            }

            View.StopWork();
        }

        private bool TryPrepareAdapter()
        {
            if (_isWaitInitializing)
            {
                return true;
            }

            if (!View.IsInitialized)
            {
                WaitInitialize();
                return true;
            }

            return false;
        }

        private void WaitInitialize()
        {
            if (_isWaitInitializing)
            {
                return;
            }

            _isWaitInitializing = true;
            View.Initialized += OnInitialized;
        }

        private void OnInitialized()
        {
            _isWaitInitializing = false;
            View.Initialized -= OnInitialized;
            View.ResetItems(GetCountOfElements());
        }

        private IView OnViewBinding(int index)
        {
            if (_presenters.ContainsKey(index))
            {
                throw new InvalidOperationException($"Presenter already has element with index {index}");
            }

            var model = GetElementByIndex(index);
            var presenter = _pool.SpawnAndSetupPresenter(model, View);
            _presenters.Add(index, presenter);
            return presenter.View;
        }

        private void OnViewUnbind(int index)
        {
            if (!_presenters.Remove(index, out var presenter))
            {
                Debug.LogError($"Presenter not found, but view was unbinded {index}");
                return;
            }

            _pool.CloseAndDespawnPresenter(presenter);
        }

        private void ShiftElements(int from, int offset)
        {
            _keysToShift.AddRange(_presenters.Keys.Where(key => key >= from));

            var orderedEnumerable = offset > 0
                ? _keysToShift.OrderByDescending(i => i)
                : _keysToShift.OrderBy(i => i);

            foreach (var key in orderedEnumerable)
            {
                var value = _presenters[key];
                _presenters.Remove(key);
                _presenters[key + offset] = value;
            }

            _keysToShift.Clear();
        }
    }
}