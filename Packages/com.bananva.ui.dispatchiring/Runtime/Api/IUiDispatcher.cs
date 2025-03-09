using System.Threading;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Api
{
    public interface IUiDispatcher
    {
        UniTask Open<TPresenter, TModel>(TModel model, CancellationToken token)
            where TPresenter : IPresentationLayerItem;
    }
}