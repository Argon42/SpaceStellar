using Bananva.UI.Dispatchiring.Api.Presenters;
using Zenject;

namespace Bananva.UI.Dispatchiring
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