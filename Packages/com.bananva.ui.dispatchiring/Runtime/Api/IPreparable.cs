using System.Threading;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Api
{
    public interface IPreparable
    {
        bool IsPrepared { get; }
        UniTask Prepare(CancellationToken token);
    }
}