namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IView
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
}
