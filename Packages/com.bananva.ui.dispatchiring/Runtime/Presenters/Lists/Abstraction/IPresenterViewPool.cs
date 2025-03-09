using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Presenters.Wrappers;

namespace Bananva.UI.Dispatchiring.Presenters.Lists.Abstraction
{
    public interface IPresenterViewPool
    {
        PresenterTypelessWrapper SpawnAndSetupPresenter(object model, IViewFactory viewFactory);
        void Despawn(PresenterTypelessWrapper wrapper);
    }
}