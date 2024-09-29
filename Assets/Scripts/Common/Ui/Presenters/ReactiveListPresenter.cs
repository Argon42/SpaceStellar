using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using R3;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters
{

	public class ReactiveListPresenter<TModel, TPresenterItem, TViewItem> :
		Presenter<IReadOnlyObservableList<TModel>, IListView>,
		IReactiveListPresenter<TModel, TPresenterItem, IListView, TViewItem>
		where TPresenterItem : Presenter<TModel, TViewItem>
		where TModel : class
		where TViewItem : class, IView
	{
		private readonly IMemoryPool<TPresenterItem> _pool;

		private readonly Dictionary<int, TPresenterItem> _presenters = new();
		private readonly bool _keepVelocityOnCountChange;

		private bool _updateRequested;

		private bool _isWaitInitializing;
		private bool _isWaitOpen;

		public ReactiveListPresenter(IMemoryPool<TPresenterItem> pool)
		{
			_pool = pool;
		}

		protected override void OnSetModel()
		{
			base.OnSetModel();

			UpdateList();
			Model.CollectionChanged += OnCollectionChanged;
		}

		protected override void OnResetModel()
		{
			base.OnResetModel();
			Model.CollectionChanged -= OnCollectionChanged;
		}

		private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<TModel> e) => TryStartDelayedStart();

		private void TryStartDelayedStart()
		{
			if (_updateRequested)
			{
				return;
			}
			_updateRequested = true;
			DelayedUpdateList().Forget();
		}

		private async UniTaskVoid DelayedUpdateList()
		{
			await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);
			_updateRequested = false;

			UpdateList();
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

		private void UpdateList()
		{
			foreach (var (_, presenter) in _presenters)
			{
				UnbindPresenter(presenter);
				DespawnPresenter(presenter);
			}
			_presenters.Clear();

			if (!TryPrepareAdapter())
			{
				View.ResetItems(Model.Count, false, _keepVelocityOnCountChange);
			}
		}

		protected override void OnOpen()
		{
			View.OnBind += OnViewBinding;
			View.OnUnbind += OnViewUnbind;

			if (_isWaitOpen)
			{
				_isWaitOpen = false;

				if (!TryPrepareAdapter(false))
				{
					NotifyListChangedExternally();
				}
			}
		}

		protected override void OnClose()
		{
			if (_isWaitInitializing)
			{
				View.Initialized -= OnInitialized;
				_isWaitInitializing = false;
				_isWaitOpen = true;
			}

			if (View.IsInitialized)
			{
				View.ResetItems(0, false, _keepVelocityOnCountChange);
			}

			View.OnBind -= OnViewBinding;
			View.OnUnbind -= OnViewUnbind;
		}

		private bool TryPrepareAdapter(bool validateIsOpened = true)
		{
			if (_isWaitInitializing || _isWaitOpen)
			{
				return true;
			}

			if (!IsOpened && validateIsOpened)
			{
				_isWaitOpen = true;
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
			View.ResetItems(Model.Count, freezeEndEdge, _keepVelocityOnCountChange);
		}

		private void OnViewBinding(int index, IView view)
		{
			if (_presenters.ContainsKey(index))
			{
				return;
			}

			TModel model = Model[index];
			TPresenterItem presenter = CreatePresenter(model);
			_presenters.Add(index, presenter);
			BindPresenter(presenter, view);
		}

		private void OnViewUnbind(int index)
		{
			if (!_presenters.Remove(index, out TPresenterItem? presenter))
			{
				return;
			}

			UnbindPresenter(presenter);
			DespawnPresenter(presenter);
		}
	}
}