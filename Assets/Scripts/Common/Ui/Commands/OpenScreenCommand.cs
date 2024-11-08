using System.Threading;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Commands
{
    public class OpenScreenCommand<TScreenPresenter, TModel> : IUiCommand
        where TScreenPresenter : IPresentationLayerItem
    {
        private readonly TModel _model;

        protected OpenScreenCommand(TModel model)
        {
            _model = model;
        }

        public async UniTask Execute(UiDispatcher uiDispatcher, CancellationToken token)
        {
            OnPreOpenModel(_model);
            await uiDispatcher.Open<TScreenPresenter, TModel>(_model, token);
        }

        public void Clear() { }

        protected virtual void OnPreOpenModel(TModel model) { }
    }
}