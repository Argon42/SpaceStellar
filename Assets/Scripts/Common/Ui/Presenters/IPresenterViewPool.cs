using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters
{
    public interface IPresenterViewPool
    {
        PresenterTypelessWrapper SpawnAndSetupPresenter<TModel>(TModel model, IViewFactory viewList);
        void Despawn(PresenterTypelessWrapper wrapper);
    }
}