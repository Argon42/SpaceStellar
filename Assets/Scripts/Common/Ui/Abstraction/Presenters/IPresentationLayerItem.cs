namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IPresentationLayerItem : IReadonlyPresentationLayerItem
    {
        void Open();
        void Close();
    }
}