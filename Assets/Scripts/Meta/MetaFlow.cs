using Bananva.UI.Dispatchiring.Commands;
using SpaceStellar.Meta.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Meta
{
    public class MetaFlow : IInitializable
    {
        private readonly IUiCommandDispatcher _uiCommandDispatcher;

        public MetaFlow(IUiCommandDispatcher uiCommandDispatcher)
        {
            _uiCommandDispatcher = uiCommandDispatcher;
        }

        public void Initialize()
        {
            _uiCommandDispatcher.ExecuteCommand<OpenMainScreenCommand>();
        }
    }
}