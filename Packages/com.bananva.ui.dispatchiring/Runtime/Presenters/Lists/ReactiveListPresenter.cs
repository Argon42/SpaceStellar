using System;
using System.Collections.Specialized;
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

        protected override int GetCountOfElements() => Model.Count;

        protected override TModel GetElementByIndex(int index) => Model[index];

        protected override void OnOpenInternal() => Model.CollectionChanged += OnCollectionChanged;

        protected override void OnCloseInternal() => Model.CollectionChanged -= OnCollectionChanged;

        private void OnCollectionChanged(in NotifyCollectionChangedEventArgs<TModel> e)
        {
            switch (e.Action, e.IsSingleItem)
            {
                case (NotifyCollectionChangedAction.Add, true):
                    InsertElements(e.NewStartingIndex, 1);
                    break;
                case (NotifyCollectionChangedAction.Add, false):
                    InsertElements(e.NewStartingIndex, e.NewItems.Length);
                    break;
                case (NotifyCollectionChangedAction.Remove, true):
                    RemoveElements(e.OldStartingIndex, 1);
                    break;
                case (NotifyCollectionChangedAction.Remove, false):
                    RemoveElements(e.OldStartingIndex, e.OldItems.Length);
                    break;
                default:
                    // TODO: добавить оставшиеся методы
                    UpdateList();
                    break;
            }
        }
    }
}