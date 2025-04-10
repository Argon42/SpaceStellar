﻿using System;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Presenters.Lists.Abstraction;
using Bananva.UI.Dispatching.Presenters.Wrappers;

namespace Bananva.UI.Dispatching.Presenters.Lists.Common
{
    public class MultiplePresenterViewPool : IPresenterViewPool
    {
        private readonly PresenterViewMatchers _matchers;

        public MultiplePresenterViewPool(PresenterViewMatchers matchers) => _matchers = matchers;

        public PresenterTypelessWrapper SpawnAndSetupPresenter(object model, IViewFactory viewFactory)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var matcher = _matchers.GetMatcher(model);

            if (matcher == null)
            {
                throw new InvalidOperationException($"No matchers found for {model.GetType().Name}");
            }

            var wrapper = matcher.CreatePresenter(viewFactory, out var view);
            wrapper.Open(view, model);
            return wrapper;
        }

        public void CloseAndDespawnPresenter(PresenterTypelessWrapper wrapper)
        {
            var matcher = _matchers.GetMatcher(wrapper.Model);
            wrapper.Close();
            matcher.ReleasePresenter(wrapper);
        }
    }
}