using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Presenters;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class MainScreenPresenter : ScreenPresenter<IMainScreenModel, IMainScreenView>, IMainScreenPresenter
    {
        public MainScreenPresenter(IViewProvider viewProvider) : base(viewProvider)
        {
        }

        protected override void OnOpen()
        {
        }

        protected override void OnClose()
        {
        }
    }
}