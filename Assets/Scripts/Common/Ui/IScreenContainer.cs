namespace SpaceStellar.Common.Ui
{
    public interface IScreenContainer
    {
        T GetScreen<T>() where T : IScreenView;
    }
}