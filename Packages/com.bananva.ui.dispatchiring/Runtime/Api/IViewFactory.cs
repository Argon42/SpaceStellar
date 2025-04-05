namespace Bananva.UI.Dispatching.Api
{
    public interface IViewFactory
    {
        TView Spawn<TView>() where TView : class, IView;
    }
}