using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Presenters.Wrappers;

namespace Bananva.UI.Dispatching.Presenters.Lists.Abstraction
{
    public interface IPresenterViewPool
    {
        PresenterTypelessWrapper SpawnAndSetupPresenter(object model, IViewFactory viewFactory);
        void CloseAndDespawnPresenter(PresenterTypelessWrapper wrapper);
    }
}