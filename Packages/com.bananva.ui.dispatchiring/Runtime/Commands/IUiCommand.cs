using System.Threading;
using Bananva.UI.Dispatching.Api;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Commands
{
    public interface IUiCommand
    {
        UniTask Execute(IUiDispatcher uiDispatcher, CancellationToken token);

        void Clear();
    }
}