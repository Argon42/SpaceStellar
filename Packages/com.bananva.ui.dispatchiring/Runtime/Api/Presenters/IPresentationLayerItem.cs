namespace Bananva.UI.Dispatchiring.Api.Presenters
{
    public interface IPresentationLayerItem : IReadonlyPresentationLayerItem
    {
        /// <summary>
        /// Открытие презентера для отображение допустимо только в случае удовлетворения всех его условий.
        /// Это обязательное наличие View и в большинстве случаев Model, а так же условие его текущей закрытости.
        /// </summary>
        void Open();

        /// <summary>
        /// Закрытие презентера доступно только в случае его открытости.
        /// </summary>
        void Close();
    }
}