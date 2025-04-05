using Bananva.UI.Dispatching.Presenters;

namespace Bananva.UI.Dispatching.Tests.PlayMode.Common
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