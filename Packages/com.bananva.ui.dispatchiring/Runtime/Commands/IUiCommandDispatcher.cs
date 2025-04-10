namespace Bananva.UI.Dispatching.Commands
{
    public interface IUiCommandDispatcher
    {
        T GetCommand<T>() where T : IUiCommand;
        void ExecuteCommand<T>() where T : IUiCommand;
        void ExecuteCommand<T>(T command) where T : IUiCommand;
    }
}