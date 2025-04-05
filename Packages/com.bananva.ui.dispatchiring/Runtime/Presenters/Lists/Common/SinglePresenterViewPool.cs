using System;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Api.Presenters;
using Bananva.UI.Dispatching.Presenters.Lists.Abstraction;
using Bananva.UI.Dispatching.Presenters.Wrappers;
using Zenject;

namespace Bananva.UI.Dispatching.Presenters.Lists.Common
{
    public class SinglePresenterViewPool<TModel, TPresenter, TView> : IPresenterViewPool
        where TView : class, IView
        where TPresenter : IConfigurablePresenter<TModel, TView>
        where TModel : class
    {
        private readonly IMemoryPool<TPresenter> _pool;

        public SinglePresenterViewPool(IMemoryPool<TPresenter> pool)
        {
            _pool = pool;
        }

        public PresenterTypelessWrapper SpawnAndSetupPresenter(object item, IViewFactory viewFactory)
        {
            if (item is not TModel model)
            {
                throw new InvalidOperationException(
                    $"{GetType().Name}:{item.GetType().Name} is not {typeof(TModel).Name}");
            }

            var presenter = _pool.Spawn();
            var view = viewFactory.Spawn<TView>();
            var wrapper = new PresenterWrapper<TPresenter, TModel, TView>(presenter);
            wrapper.Open(view, model);
            return wrapper;
        }

        public void CloseAndDespawnPresenter(PresenterTypelessWrapper wrapper)
        {
            if (wrapper.LayerItem is not TPresenter presenter)
            {
                throw new InvalidOperationException(
                    $"{GetType().Name}:{typeof(TPresenter).Name} is not {typeof(TPresenter).Name}");
            }

            wrapper.Close();
            _pool.Despawn(presenter);
        }
    }
}