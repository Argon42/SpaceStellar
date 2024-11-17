using ObservableCollections;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters
{
    public interface IReactiveListPresenter<TModelItem> :
        IConfigurablePresenter<IReadOnlyObservableList<TModelItem>, IListView> where TModelItem : class { }

    public class ReactiveListPresenter<TModel, TPresenterItem, TViewItem> :
        BaseListPresenter<IReadOnlyObservableList<TModel>, TModel, TPresenterItem, TViewItem>,
        IReactiveListPresenter<TModel>
        where TPresenterItem : IConfigurablePresenter<TModel, TViewItem>
        where TModel : class
        where TViewItem : class, IView
    {
        public ReactiveListPresenter(IMemoryPool<TPresenterItem> pool) : base(pool) { }

        protected override int GetCountOfElements() => Model.Count;

        protected override TModel GetElementByIndex(int index) => Model[index];

        protected override void OnOpenInternal()
        {
            UpdateList();
            Model.CollectionChanged += OnCollectionChanged;
        }

        protected override void OnCloseInternal()
        {
            Model.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<TModel> e) => UpdateList();
    }
}