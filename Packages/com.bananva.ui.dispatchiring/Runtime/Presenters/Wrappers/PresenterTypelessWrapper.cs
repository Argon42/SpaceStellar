using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;

namespace Bananva.UI.Dispatchiring.Presenters.Wrappers
{
    public abstract class PresenterTypelessWrapper
    {
        public abstract bool IsOpened { get; }
        public abstract IView View { get; }
        public abstract IPresentationLayerItem LayerItem { get; }

        public abstract void Open(IView view, object model);
        public abstract void Close();
    }
}