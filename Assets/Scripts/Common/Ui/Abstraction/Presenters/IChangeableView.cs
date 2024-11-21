namespace SpaceStellar.Common.Ui.Abstraction.Presenters
{
    public interface IChangeableView<TView> : IChangeableView
    {
        TView View { get; }
        void SetView(TView view);
    }

    public interface IChangeableView
    {
        void ResetView();
    }
}