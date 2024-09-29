namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IPresenter
    {
        bool IsOpenAvailable { get; }
        bool IsOpened { get; }
        void Open();
        void Close();
        void ResetModel();
    }

    public interface IPresenter<T> : IPresenter
    {
        void SetModel(T model);
    }
}
