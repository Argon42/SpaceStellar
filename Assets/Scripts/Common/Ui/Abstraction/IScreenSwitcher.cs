using System;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Ui.Abstraction.Presenters;

namespace SpaceStellar.Common.Ui.Abstraction
{
    public interface IScreenSwitcher
    {
        UniTask SwitchToScreen<TModel>(IScreenPresenter<TModel> screenPresenter, TModel model);
        event Action<IScreenPresenter?, IScreenPresenter> OnSectionChangedEvent;
    }
}
