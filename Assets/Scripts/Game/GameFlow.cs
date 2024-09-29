using SpaceStellar.Common.Ui.Commands;
using SpaceStellar.Game.Ui.MainScreen;
using Zenject;

namespace SpaceStellar.Game
{
    public class GameFlow : IInitializable
    {
        private readonly IUiCommandDispatcher _uiCommandDispatcher;

        public GameFlow(IUiCommandDispatcher uiCommandDispatcher)
        {
            _uiCommandDispatcher = uiCommandDispatcher;
        }

        public void Initialize()
        {
            _uiCommandDispatcher.ExecuteCommand<OpenMainScreenCommand>();
        }
    }
}