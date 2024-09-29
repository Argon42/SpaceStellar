using SpaceStellar.Common.Ui;
using SpaceStellar.Common.Ui.Abstraction;
using SpaceStellar.Common.Ui.Abstraction.Presenters;
using SpaceStellar.Common.Ui.Presenters;

namespace SpaceStellar.Game.Ui.MainScreen
{
    public class MainScreenPresenter : ScreenPresenter<MainScreenModel, IMainScreenView>, IScreenPresenter
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