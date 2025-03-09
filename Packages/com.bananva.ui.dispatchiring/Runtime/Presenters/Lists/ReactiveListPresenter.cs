using Bananva.UI.Dispatchiring.Api.Presenters;
using Bananva.UI.Dispatchiring.Presenters.Lists.Abstraction;
using ObservableCollections;

namespace Bananva.UI.Dispatchiring.Presenters.Lists
{
    public interface IReactiveListPresenter<TModelItem> :
        IListPresenter<IReadOnlyObservableList<TModelItem>> where TModelItem : class { }

    public class ReactiveListPresenter<TModel> :
        BaseListPresenter<IReadOnlyObservableList<TModel>, TModel>,
        IReactiveListPresenter<TModel>
        where TModel : class
    {
        public ReactiveListPresenter(IPresenterViewPool pool) : base(pool) { }

        protected override int GetCountOfElements()
        {
            return Model.Count;
        }

        protected override TModel GetElementByIndex(int index)
        {
            return Model[index];
        }

        protected override void OnOpenInternal()
        {
            Model.CollectionChanged += OnCollectionChanged;
        }

        protected override void OnCloseInternal()
        {
            Model.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<TModel> e)
        {
            UpdateList();
        }
    }
}