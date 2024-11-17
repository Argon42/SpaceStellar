using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public interface IPresenterViewPool
    {
        PresenterTypelessWrapper SpawnAndSetupPresenter<TModel>(TModel model, IViewFactory viewList);
        void Despawn(PresenterTypelessWrapper wrapper);
    }
}