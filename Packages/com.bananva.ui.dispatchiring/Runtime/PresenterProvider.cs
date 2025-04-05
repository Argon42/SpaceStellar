using Bananva.UI.Dispatching.Api.Presenters;
using Zenject;

namespace Bananva.UI.Dispatching
{
    public class PresenterProvider : IPresenterProvider
    {
        private readonly DiContainer _container;

        public PresenterProvider(DiContainer container)
        {
            _container = container;
        }

        public TPresenter GetPresenter<TPresenter, TModel>() where TPresenter : IPresentationLayerItem
        {
            var presenter = _container.Resolve<TPresenter>();
            return presenter;
        }
    }
}