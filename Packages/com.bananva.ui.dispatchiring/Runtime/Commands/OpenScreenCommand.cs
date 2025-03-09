using System.Threading;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Commands
{
    public class OpenScreenCommand<TScreenPresenter, TModel> : IUiCommand
        where TScreenPresenter : IPresentationLayerItem
    {
        private readonly TModel _model;

        protected OpenScreenCommand(TModel model)
        {
            _model = model;
        }

        public async UniTask Execute(IUiDispatcher uiDispatcher, CancellationToken token)
        {
            OnPreOpenModel(_model);
            await uiDispatcher.Open<TScreenPresenter, TModel>(_model, token);
        }

        public void Clear() { }

        protected virtual void OnPreOpenModel(TModel model) { }
    }
}