using SpaceStellar.Common.Ui.Commands;

namespace SpaceStellar.Meta.Ui.MainScreen
{
    public class OpenMainScreenCommand : OpenScreenCommand<IMainScreenPresenter, IMainScreenModel>
    {
        public OpenMainScreenCommand(IMainScreenModel model) : base(model) { }
    }
}