namespace Bananva.UI.Dispatching.Api.Presenters
{
    public interface IChangeableModel<TModel> : IChangeableModel
    {
        TModel Model { get; }
        void SetModel(TModel model);
    }

    public interface IChangeableModel
    {
        bool HasModel { get; }
        void ResetModel();
    }
}