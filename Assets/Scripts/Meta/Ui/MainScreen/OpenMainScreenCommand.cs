using Bananva.UI.Dispatching.Commands;
using SpaceStellar.Meta.Ui.MainScreen.Models;
using SpaceStellar.Meta.Ui.MainScreen.Presenters;

namespace SpaceStellar.Meta.Ui.MainScreen
{
    public class OpenMainScreenCommand : OpenScreenCommand<IMainScreenPresenter, IMainScreenModel>
    {
        public OpenMainScreenCommand(IMainScreenModel model) : base(model) { }
    }
}