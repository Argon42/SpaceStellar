using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui.Views
{
    public interface IViewFactory
    {
        TView Spawn<TView>() where TView : class, IView;
    }
}