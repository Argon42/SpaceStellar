namespace Bananva.UI.Dispatchiring.Commands
{
    public interface IUiCommandPool
    {
        void Release(IUiCommand command);
        T Create<T>() where T : IUiCommand;
    }
}