using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Presenters.Wrappers
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