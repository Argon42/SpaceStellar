using System;
using SpaceStellar.Common.Ui.Abstraction;

namespace SpaceStellar.Common.Ui.Views
{

	public interface IListView : IView
	{
		event Action<int, IView> OnBind;
		event Action<int> OnUnbind;
		bool IsInitialized { get; }
		bool InsertAtIndexSupported { get; }
		int ItemsCount { get; }
		event Action Initialized;

		void Init();

		void ResetItems(int newCount, bool freezeEndEdge, bool keepVelocityOnCountChange);

		void InsertItems(int index, int itemsCount, bool contentPanelEndEdgeStationary, bool keepVelocity);
	}

}
