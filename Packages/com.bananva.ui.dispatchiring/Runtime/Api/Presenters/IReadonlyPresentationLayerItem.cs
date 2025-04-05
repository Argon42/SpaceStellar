namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IReadonlyPresentationLayerItem
    {
        bool IsOpenAvailable { get; }
        bool IsOpened { get; }
    }
}