using System.Threading;
using Bananva.UI.Dispatchiring.Api;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Commands
{
    public interface IUiCommand
    {
        UniTask Execute(IUiDispatcher uiDispatcher, CancellationToken token);

        void Clear();
    }
}