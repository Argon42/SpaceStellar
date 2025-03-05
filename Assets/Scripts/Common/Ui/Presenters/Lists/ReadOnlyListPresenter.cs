using System.Collections.Generic;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Presenters.Lists.Abstraction;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public interface IReadOnlyListPresenter<TModelItem> :
        IListPresenter<IReadOnlyList<TModelItem>> where TModelItem : class { }

    public class ReadOnlyListPresenter<TModel> :
        BaseListPresenter<IReadOnlyList<TModel>, TModel>,
        IReadOnlyListPresenter<TModel> where TModel : class
    {
        public ReadOnlyListPresenter(IPresenterViewPool pool) : base(pool) { }

        protected override int GetCountOfElements()
        {
            return Model.Count;
        }

        protected override TModel GetElementByIndex(int index)
        {
            return Model[index];
        }

        protected override void OnOpenInternal() { }

        protected override void OnCloseInternal() { }
    }
}