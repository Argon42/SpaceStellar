using SpaceStellar.Common.Ui;
using SpaceStellar.Common.Ui.Abstraction;

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