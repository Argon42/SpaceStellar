using System;
using Bananva.UI.Dispatchiring.Api.Presenters;
using Cysharp.Threading.Tasks;

namespace Bananva.UI.Dispatchiring.Api
{
    public interface IScreenSwitcher
    {
        UniTask SwitchToScreen<TModel>(IScreenPresenter<TModel> screenPresenter, TModel model);
        event Action<IScreenPresenter?, IScreenPresenter> OnSectionChangedEvent;
    }
}