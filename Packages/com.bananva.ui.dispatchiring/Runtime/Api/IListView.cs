using System;

namespace Bananva.UI.Dispatchiring.Api
{
    public interface IListView : IView, IViewFactory
    {
        Func<int, IView>? OnBind { set; }
        Action<int>? OnUnbind { set; }
        event Action Initialized;

        bool IsInitialized { get; }
        bool InsertAtIndexSupported { get; }
        int ItemsCount { get; }

        void Init();

        void ResetItems(int newCount);

        void InsertItems(int index, int itemsCount);
    }
}