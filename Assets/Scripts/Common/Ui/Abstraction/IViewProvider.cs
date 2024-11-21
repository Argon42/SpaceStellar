using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IViewProvider
    {
        bool TryGetView<TView>([NotNullWhen(true)] out TView? view) where TView : class, IView;
        UniTask<TView> LoadView<TView>(CancellationToken token) where TView : class, IView;

        UniTask<TView> TryGetOrLoadView<TView>(CancellationToken token) where TView : class, IView
        {
            return !TryGetView(out TView? view) ? LoadView<TView>(token) : UniTask.FromResult(view);
        }
    }
}