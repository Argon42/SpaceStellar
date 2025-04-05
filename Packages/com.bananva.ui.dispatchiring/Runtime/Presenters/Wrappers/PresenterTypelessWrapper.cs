using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;

namespace Bananva.UI.Dispatching.Presenters.Wrappers
{
    public abstract class PresenterTypelessWrapper
    {
        /// <summary>
        /// Метод при вызове выдаст ошибку при отсутствии View, необходимо проверять через <see cref="IsOpened"/>
        /// </summary>
        public abstract IView View { get; }
        public abstract bool IsOpened { get; }
        public abstract IPresentationLayerItem LayerItem { get; }
        public abstract object Model { get; }

        public abstract void Open(IView view, object model);
        public abstract void Close();
    }
}