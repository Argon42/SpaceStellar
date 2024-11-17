using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters
{
    public abstract class BaseListPresenter<TCollection, TModel, TPresenterItem, TViewItem> :
        Presenter<TCollection, IListView>
        where TPresenterItem : IConfigurablePresenter<TModel, TViewItem>
        where TModel : class
        where TViewItem : class, IView
        where TCollection : class
    {
        private const bool KeepVelocityOnCountChange = true;

        private readonly IMemoryPool<TPresenterItem> _pool;
        private readonly Dictionary<int, TPresenterItem> _presenters = new();

        private bool _isWaitInitializing;

        protected BaseListPresenter(IMemoryPool<TPresenterItem> pool)
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
            {
                UnbindPresenter(item);
                DespawnPresenter(item);
            }

            _presenters.Clear();

            if (!TryPrepareAdapter())
            {
                View.ResetItems(GetCountOfElements(), false, KeepVelocityOnCountChange);
            }
        }

        protected override void OnOpen()
        {
            View.OnBind += OnViewBinding;
            View.OnUnbind += OnViewUnbind;

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
                View.ResetItems(0, false, KeepVelocityOnCountChange);
            }

            View.OnBind -= OnViewBinding;
            View.OnUnbind -= OnViewUnbind;
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
            NotifyListChangedExternally();
        }

        private void NotifyListChangedExternally(bool freezeEndEdge = false)
        {
            View.ResetItems(GetCountOfElements(), freezeEndEdge, KeepVelocityOnCountChange);
        }

        private void OnViewBinding(int index, IView view)
        {
            if (_presenters.ContainsKey(index))
            {
                return;
            }

            var model = GetElementByIndex(index);
            var presenter = CreatePresenter(model);
            _presenters.Add(index, presenter);
            BindPresenter(presenter, view);
        }

        private void OnViewUnbind(int index)
        {
            if (!_presenters.Remove(index, out var presenter))
            {
                return;
            }

            UnbindPresenter(presenter);
            DespawnPresenter(presenter);
        }

        private void BindPresenter(TPresenterItem presenter, IView view)
        {
            presenter.SetView((TViewItem)view);
            presenter.Open();
        }

        private void UnbindPresenter(TPresenterItem presenter)
        {
            presenter.Close();
            presenter.ResetView();
        }

        private void DespawnPresenter(TPresenterItem presenter)
        {
            presenter.ResetModel();
            _pool.Despawn(presenter);
        }

        private TPresenterItem CreatePresenter(TModel model)
        {
            TPresenterItem presenter = _pool.Spawn();
            presenter.SetModel(model);
            return presenter;
        }
    }
}