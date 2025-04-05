using System.Threading;
using Bananva.UI.Dispatching.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Api
{
    public interface IUiDispatcher
    {
        UniTask Open<TPresenter, TModel>(TModel model, CancellationToken token)
            where TPresenter : IPresentationLayerItem;
    }
}