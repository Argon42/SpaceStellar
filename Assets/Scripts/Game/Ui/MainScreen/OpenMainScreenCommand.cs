using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui;
using SpaceStellar.Common.Ui.Commands;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class OpenMainScreenCommand : IUiCommand
    {
        private readonly MainScreenModel _model;

        public OpenMainScreenCommand(MainScreenModel model)
        {
            _model = model;
        }

        public async UniTask Execute(UiDispatcher uiDispatcher, CancellationToken token)
        {
            await uiDispatcher.Open<MainScreenPresenter, MainScreenModel>(_model, token);
        }

        public void Clear()
        {
        }
    }
}