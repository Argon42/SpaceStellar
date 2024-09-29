using SpaceStellar.Common.Ui.Abstraction;
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

        public TPresenter GetPresenter<TPresenter, TModel>() where TPresenter : IPresenter<TModel>
        {
            var presenter = _container.Resolve<TPresenter>();
            return presenter;
        }
    }
}
