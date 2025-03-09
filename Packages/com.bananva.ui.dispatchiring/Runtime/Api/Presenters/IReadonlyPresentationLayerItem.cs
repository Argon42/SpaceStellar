namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IReadonlyPresentationLayerItem
    {
        bool IsOpenAvailable { get; }
        bool IsOpened { get; }
    }
}