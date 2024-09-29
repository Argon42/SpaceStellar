using ObservableCollections;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters
{
    public interface IReactiveListPresenter<TModelItem, TPresenterItem, TViewList, TViewItem> : 
        IConfigurablePresenter<IReadOnlyObservableList<TModelItem>, TViewList>
        where TViewList : class, IListView
        where TPresenterItem : Presenter<TModelItem, TViewItem>
        where TModelItem : class
        where TViewItem : class, IView
    { }
}