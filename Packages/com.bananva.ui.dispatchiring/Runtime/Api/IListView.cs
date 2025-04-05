using System;

namespace Bananva.UI.Dispatching.Api
{
    public interface IListView : IView, IViewFactory
    {
        event Action Initialized;

        bool IsInitialized { get; }
        int ItemsCount { get; }
        bool WorkWithIndexSupported { get; }

        void Init();

        void StartWork(Func<int, IView> onBind, Action<int> onUnbind);
        void StopWork();

        void ResetItems(int newCount);

        void InsertItems(int index, int itemsCount);

        void RemoveItems(int index, int itemsCount);
        void ReplaceItem(int index);
    }
}