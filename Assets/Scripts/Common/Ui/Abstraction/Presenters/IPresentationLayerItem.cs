namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IReadonlyPresentationLayerItem
    {
        bool IsOpenAvailable { get; }
        bool IsOpened { get; }
    }

    public interface IPresentationLayerItem : IReadonlyPresentationLayerItem
    {
        void Open();
        void Close();
    }

    public interface IChangeableModel<TModel>
    {
        TModel Model { get; }
        void SetModel(TModel model);
        void ResetModel();
    }

    public interface IChangeableView<TView>
    {
        TView View { get; }
        void SetView(TView view);
        void ResetView();
    }
    
}
