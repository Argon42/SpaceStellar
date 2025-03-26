using Bananva.UI.Dispatchiring.Presenters;

namespace Tests.PlayMode.Common
{
    internal class ImagePresenter : Presenter<ImageData, ImageView>
    {
        protected override void OnOpen()
        {
            View.SetData(Model.Color, Model.Id);
        }

        protected override void OnClose() { }
    }
}