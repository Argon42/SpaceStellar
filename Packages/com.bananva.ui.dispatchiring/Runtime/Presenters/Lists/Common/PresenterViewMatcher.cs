using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Bananva.UI.Dispatchiring.Presenters.Wrappers;
using Zenject;

namespace Bananva.UI.Dispatchiring.Presenters.Lists.Common
{
    public abstract class PresenterViewMatcher
    {
        public abstract bool HasMatch(object item);

        public abstract PresenterTypelessWrapper CreatePresenter(IViewFactory viewFactory, out IView view);
        public abstract void ReleasePresenter(PresenterTypelessWrapper presenter);
    }

    public class PresenterViewMatcher<TPresenter, TModel, TView>
        : PresenterViewMatcher
        where TPresenter : class, IConfigurablePresenter<TModel, TView>
        where TView : class, IView
    {
        private readonly IMemoryPool<TPresenter> _pool;

        public PresenterViewMatcher(IMemoryPool<TPresenter> pool) => _pool = pool;

        public override bool HasMatch(object item) => item.GetType() == typeof(TModel);

        public override PresenterTypelessWrapper CreatePresenter(IViewFactory viewFactory, out IView view)
        {
            view = viewFactory.Spawn<TView>();
            var presenter = _pool.Spawn();
            return new PresenterWrapper<TPresenter, TModel, TView>(presenter);
        }

        public override void ReleasePresenter(PresenterTypelessWrapper presenter)
        {
            _pool.Despawn(presenter.LayerItem);
        }
    }
}