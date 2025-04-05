using Bananva.UI.Dispatching.Presenters;

namespace Bananva.UI.Dispatching.Tests.PlayMode.Common
{
    internal class TextPresenter : Presenter<TextData, TextView>
    {
        protected override void OnOpen() => View.SetText(Model.Prefixfix + Model.Id + Model.Postfix);

        protected override void OnClose() { }
    }
}