using System;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;
using Zenject;

namespace SpaceStellar.Common.Ui.Presenters.Lists
{
    public class SingleIPresenterViewPool<TModel, TPresenter, TView> : IPresenterViewPool
        where TView : class, IView
        where TPresenter : IConfigurablePresenter<TModel, TView>
        where TModel : class
    {
        private readonly IMemoryPool<TPresenter> _pool;

        public SingleIPresenterViewPool(IMemoryPool<TPresenter> pool)
        {
            _pool = pool;
        }

        public PresenterTypelessWrapper SpawnAndSetupPresenter<TModelItem>(TModelItem item, IViewFactory viewList)
        {
            if (item is not TModel model)
            {
                throw new InvalidOperationException(
                    $"{GetType().Name}:{typeof(TModelItem).Name} is not {typeof(TModel).Name}");
            }

            var presenter = _pool.Spawn();
            var view = viewList.Spawn<TView>();
            var wrapper = new PresenterWrapper<TPresenter, TModel, TView>(presenter);
            wrapper.Open(view, model);
            return wrapper;
        }

        public void Despawn(PresenterTypelessWrapper wrapper)
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