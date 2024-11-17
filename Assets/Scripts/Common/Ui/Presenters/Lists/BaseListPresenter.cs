using System;
using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters.Lists
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

        protected BaseListPresenter(IPresenterViewPool pool)
        {
            _pool = pool;
        }

        protected abstract int GetCountOfElements();

        protected abstract TModel GetElementByIndex(int index);

        protected abstract void OnOpenInternal();

        protected abstract void OnCloseInternal();

        protected void UpdateList()
        {
            foreach (var item in _presenters.Values)
                _pool.Despawn(item);

            _presenters.Clear();

            if (!TryPrepareAdapter())
            {
                View.ResetItems(GetCountOfElements());
            }
        }

        protected override void OnOpen()
        {
            View.OnBind = OnViewBinding;
            View.OnUnbind = OnViewUnbind;

            OnOpenInternal();
            UpdateList();
        }

        protected override void OnClose()
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

            View.OnBind = null;
            View.OnUnbind = null;
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
                return;
            }

            _pool.Despawn(presenter);
        }
    }
}