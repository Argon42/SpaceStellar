using System;
using Bananva.UI.Dispatching.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatching.Api
{
    public interface IScreenSwitcher
    {
        UniTask SwitchToScreen<TModel>(IScreenPresenter<TModel> screenPresenter, TModel model);
        event Action<IScreenPresenter?, IScreenPresenter> OnSectionChangedEvent;
    }
}