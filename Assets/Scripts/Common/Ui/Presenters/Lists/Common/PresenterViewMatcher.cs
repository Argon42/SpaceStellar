using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters.Lists.Common
{
    public abstract class PresenterViewMatcher
    {
        public abstract bool HasMatch(object item);

        public abstract PresenterTypelessWrapper CreatePresenter(IViewFactory viewFactory, out IView view);
    }

    public class PresenterViewMatcher<TPresenter, TModel, TView>
        : PresenterViewMatcher
        where TPresenter : class, IConfigurablePresenter<TModel, TView>
        where TView : class, IView
    {
        private readonly IMemoryPool<TPresenter> _pool;

        public PresenterViewMatcher(IMemoryPool<TPresenter> pool)
        {
            _pool = pool;
        }

        public override bool HasMatch(object item)
        {
            return item.GetType() == typeof(TModel);
        }

        public override PresenterTypelessWrapper CreatePresenter(IViewFactory viewFactory, out IView view)
        {
            view = viewFactory.Spawn<TView>();
            var presenter = _pool.Spawn();
            return new PresenterWrapper<TPresenter, TModel, TView>(presenter);
        }
    }
}