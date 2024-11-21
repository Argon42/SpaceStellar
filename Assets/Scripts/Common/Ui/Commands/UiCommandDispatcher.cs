namespace SpaceStellar.Common.Ui.Commands
{
    public class UiCommandDispatcher : IUiCommandDispatcher
    {
        private readonly UiCommandExecutor _uiCommandExecutor;
        private readonly IUiCommandPool _uiCommandPool;

        public UiCommandDispatcher(
            UiCommandExecutor uiCommandExecutor,
            IUiCommandPool uiCommandPool)
        {
            _uiCommandExecutor = uiCommandExecutor;
            _uiCommandPool = uiCommandPool;

            _uiCommandExecutor.OnExecuted += OnExecuted;
        }

        public T GetCommand<T>() where T : IUiCommand
        {
            return _uiCommandPool.Create<T>();
        }

        public void ExecuteCommand<T>() where T : IUiCommand
        {
            var command = GetCommand<T>();
            ExecuteCommand(command);
        }

        public void ExecuteCommand<T>(T command) where T : IUiCommand
        {
            if (!_uiCommandExecutor.IsDisposed)
            {
                _uiCommandExecutor.AddToExecutionQueue(command);
            }
        }

        private void OnExecuted(IUiCommand obj)
        {
            _uiCommandPool.Release(obj);
        }
    }
}