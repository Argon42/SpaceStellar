using System;
using SpaceStellar.Common.Ui.Presenters.Lists.Abstraction;
using SpaceStellar.Common.Ui.Presenters.Wrappers;
using SpaceStellar.Common.Ui.Views;

namespace SpaceStellar.Common.Ui.Presenters.Lists.Common
{
    public class MultiplePresenterViewPool : IPresenterViewPool
    {
        private readonly PresenterViewMatchers _matchers;

        public MultiplePresenterViewPool(PresenterViewMatchers matchers)
        {
            _matchers = matchers;
        }

        public PresenterTypelessWrapper SpawnAndSetupPresenter(object item, IViewFactory viewFactory)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var matcher = _matchers.GetMatcher(item);

            if (matcher == null)
            {
                throw new InvalidOperationException($"No matchers found for {item.GetType().Name}");
            }

            var wrapper = matcher.CreatePresenter(viewFactory, out var view);
            wrapper.Open(view, item);
            return wrapper;
        }

        public void Despawn(PresenterTypelessWrapper wrapper) { }
    }
}