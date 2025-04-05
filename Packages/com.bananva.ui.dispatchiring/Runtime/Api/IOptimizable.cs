using System.Threading;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Api
{
    public interface IOptimizable
    {
        bool IsCanOptimize { get; }

        UniTask Optimize(CancellationToken token);
    }
}