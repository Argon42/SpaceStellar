using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Abstraction
{
    internal interface IPreparable
    {
        bool IsPrepared { get; }
        UniTask Prepare(CancellationToken token);
    }
}