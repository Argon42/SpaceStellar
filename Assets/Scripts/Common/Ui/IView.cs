namespace SpaceStellar.Common.Ui
{
    public interface IView
    {
        void Activate();
        void Deactivate();
        bool IsActive { get; }
    }
}