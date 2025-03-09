using System.Threading;
using Bananva.UI.Dispatchiring.Api;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Bananva.UI.Dispatchiring.Presenters;
using Bananva.UI.Dispatchiring.Presenters.Lists;
using Cysharp.Threading.Tasks;
using SpaceStellar.Meta.Ui.MainScreen.Models;
using SpaceStellar.Meta.Ui.MainScreen.Views;

namespace SpaceStellar.Meta.Ui.MainScreen.Presenters
{
    public interface IMainScreenPresenter : IScreenPresenter<IMainScreenModel> { }

    public class MainScreenPresenter : ScreenPresenter<IMainScreenModel, IMainScreenView>, IMainScreenPresenter
    {
        private readonly IReadOnlyListPresenter<MainMenuTile> _listPresenter;

        public MainScreenPresenter(IViewProvider viewProvider,
            IReadOnlyListPresenter<MainMenuTile> listPresenter)
            : base(viewProvider)
        {
            _listPresenter = listPresenter;
        }

        protected override UniTask OnSetView(IMainScreenView view, CancellationToken token)
        {
            _listPresenter.SetView(view.TilesListView);

            return UniTask.CompletedTask;
        }

        protected override UniTask OnResetView(CancellationToken token)
        {
            _listPresenter.ResetView();

            return UniTask.CompletedTask;
        }

        protected override void OnSetModel()
        {
            base.OnSetModel();
            _listPresenter.SetModel(Model.Tiles);
        }

        protected override void OnResetModel()
        {
            base.OnResetModel();
            _listPresenter.ResetModel();
        }

        protected override void OnOpen()
        {
            _listPresenter.Open();
        }

        protected override void OnClose()
        {
            _listPresenter.Close();
        }
    }
}