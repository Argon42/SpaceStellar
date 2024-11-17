using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters
{
    public interface IReadOnlyListPresenter<TModelItem> :
        IConfigurablePresenter<IReadOnlyList<TModelItem>, IListView> where TModelItem : class { }

    public class ReadOnlyListPresenter<TModel, TPresenterItem, TViewItem> :
        BaseListPresenter<IReadOnlyList<TModel>, TModel, TPresenterItem, TViewItem>,
        IReadOnlyListPresenter<TModel>
        where TPresenterItem : IConfigurablePresenter<TModel, TViewItem>
        where TModel : class
        where TViewItem : class, IView
    {
        public ReadOnlyListPresenter(IMemoryPool<TPresenterItem> pool) : base(pool) { }

        protected override int GetCountOfElements() => Model.Count;

        protected override TModel GetElementByIndex(int index) => Model[index];

        protected override void OnOpenInternal() { }

        protected override void OnCloseInternal() { }
    }
}