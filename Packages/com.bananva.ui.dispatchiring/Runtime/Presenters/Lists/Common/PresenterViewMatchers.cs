using System;
using System.Linq;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Zenject;

namespace Bananva.UI.Dispatchiring.Presenters.Lists.Common
{
    public class PresenterViewMatchers
    {
        private readonly PresenterViewMatcher[] _matchers;

        public PresenterViewMatchers(DiContainer container, Type[] presenterTypes)
        {
            ValidateTypes(presenterTypes);
            _matchers = CreateMatchers(container, presenterTypes);
        }

        public PresenterViewMatcher GetMatcher(object item) =>
            _matchers.FirstOrDefault(matcher => matcher.HasMatch(item))
            ?? throw new InvalidOperationException("No matchers found");

        private static void ValidateTypes(Type[] presenterTypes)
        {
            foreach (var type in presenterTypes)
            {
                var interfaceType = GetPresenterInterfaceType(type);
                var genericArguments = interfaceType.GetGenericArguments();
                if (genericArguments.Length != 2)
                {
                    throw new ArgumentException($"{type.Name} has invalid generic arguments");
                }

                var modelType = genericArguments[0];
                var viewType = genericArguments[1];
                if (modelType == null || viewType == null)
                {
                    throw new ArgumentException($"{type.Name} has invalid generic arguments");
                }
            }
        }

        private static Type GetPresenterInterfaceType(Type type) =>
            type.GetInterfaces().FirstOrDefault(IsConfigurablePresenter)
            ?? throw new InvalidOperationException($"{type.Name} is not {typeof(IConfigurablePresenter<,>).Name}");

        private static bool IsConfigurablePresenter(Type type) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IConfigurablePresenter<,>);

        private static PresenterViewMatcher[] CreateMatchers(DiContainer container, Type[] presenterTypes) =>
            presenterTypes.Select(presenterType =>
            {
                var presenterInterface = GetPresenterInterfaceType(presenterType);
                var genericArguments = presenterInterface.GetGenericArguments();
                var modelType = genericArguments[0];
                var viewType = genericArguments[1];

                return (PresenterViewMatcher)container.Instantiate(
                    typeof(PresenterViewMatcher<,,>)
                        .MakeGenericType(presenterType, modelType, viewType));
            }).ToArray();
    }
}