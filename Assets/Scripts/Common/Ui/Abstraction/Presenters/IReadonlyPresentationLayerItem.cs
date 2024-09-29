namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IReadonlyPresentationLayerItem
    {
        bool IsOpenAvailable { get; }
        bool IsOpened { get; }
    }
}