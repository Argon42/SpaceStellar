namespace Bananva.UI.Dispatchiring.Api
{
    public interface IView
    {
        bool IsActive { get; }
        bool IsShow { get; }
        void Activate();
        void Deactivate();
    }
}