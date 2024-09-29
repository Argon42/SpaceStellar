using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Commands
{
    public interface IUiCommand
    {
        UniTask Execute(UiDispatcher uiDispatcher, CancellationToken token);

        void Clear();
    }
}
