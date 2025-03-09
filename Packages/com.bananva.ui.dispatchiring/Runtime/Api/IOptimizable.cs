using System.Threading;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Api
{
    public interface IOptimizable
    {
        bool IsCanOptimize { get; }

        UniTask Optimize(CancellationToken token);
    }
}