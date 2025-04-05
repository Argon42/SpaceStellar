namespace Bananva.UI.Dispatching.Api
{
    public interface IView
    {
        bool IsActive { get; }
        bool IsShow { get; }
        void Activate();
        void Deactivate();
    }
}