namespace Bananva.UI.Dispatchiring.Api
{
    public interface IViewFactory
    {
        TView Spawn<TView>() where TView : class, IView;
    }
}