using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IOptimizable
    {
        bool IsCanOptimize { get; }

        UniTask Optimize(CancellationToken token);
    }
}