using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SpaceStellar.Common.Ui.Commands
{
    public class UiCommandExecutor : IDisposable
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly Queue<IUiCommand> _screenCommands = new();
        private readonly UiDispatcher _uiDispatcher;

        public UiCommandExecutor(UiDispatcher uiDispatcher)
        {
            _uiDispatcher = uiDispatcher;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public bool IsDisposed { get; private set; }

        private bool IsExecuting => _screenCommands.Count > 1 || ActiveExecutionItem != null;
        private IUiCommand? ActiveExecutionItem { get; set; }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            IsDisposed = true;
        }

        public event Action<IUiCommand> OnExecuted = delegate { };

        public void AddToExecutionQueue<T>(T command) where T : IUiCommand
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException($"{GetType().Name}: Already disposed!");
            }

            _screenCommands.Enqueue(command);

            if (!IsExecuting)
            {
                ExecuteCommands(_cancellationTokenSource.Token).Forget();
            }
        }

        private async UniTask ExecuteCommands(CancellationToken token)
        {
            try
            {
                ActiveExecutionItem = _screenCommands.Dequeue();
                await ActiveExecutionItem.Execute(_uiDispatcher, token);
                OnExecuted(ActiveExecutionItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                ActiveExecutionItem = null;
            }
        }
    }
}