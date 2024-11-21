using SpaceStellar.Common.Ui.Abstraction.Presenters;
using Zenject;

namespace SpaceStellar.Common.Ui
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