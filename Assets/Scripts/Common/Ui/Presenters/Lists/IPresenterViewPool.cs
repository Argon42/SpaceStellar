using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public interface IPresenterViewPool
    {
        PresenterTypelessWrapper SpawnAndSetupPresenter(object model, IViewFactory viewFactory);
        void Despawn(PresenterTypelessWrapper wrapper);
    }
}