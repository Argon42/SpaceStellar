using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Presenters
{
    public interface IOptimizable
    {
        bool IsCanOptimize { get; }

        UniTask Optimize(CancellationToken token);
    }
}