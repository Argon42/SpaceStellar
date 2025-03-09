namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IPresentationLayerItem : IReadonlyPresentationLayerItem
    {
        void Open();
        void Close();
    }
}