namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IChangeableModel<TModel> : IChangeableModel
    {
        TModel? Model { get; }
        void SetModel(TModel model);
    }

    public interface IChangeableModel
    {
        void ResetModel();
    }
}